using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrox.MatroxImagingLibrary;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using OpenCvSharp;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ApsMotionControl.Dlg
{
    public partial class CCdControl : UserControl
    {
        //public event delLogSender eLogSender;       //외부에서 호출할때 사용
        //private ManualPcb manualPcb = new ManualPcb();
        //private ManualLens manualLens = new ManualLens();
        private const int EEpromGridRowViewCount = 10;

        private int[] GridColWidth = { 50, 80, 65, 65, 70, 270, 50, 50, 1 };
        private int GridRowHeight = 30;
        private int GridHeaderHeight = 30;
        private int GridInitWidth = 0;
        private int SelectedCellRow = 0;
        private int SelectedCellCol = 0;

        Rectangle[] m_clRectROI = new Rectangle[Globalo.CHART_ROI_COUNT];
        Rectangle[] m_clRectCircle = new Rectangle[4];

        public int[] m_iOffsetX_SFR;
        public int[] m_iOffsetY_SFR;

        public int[] m_iSizeX_SFR;
        public int[] m_iSizeY_SFR;
        public int m_iCurNo_SFR;
        public Rectangle[] m_rcRoiBox;						// 원형 마크 검색 영역


        List<byte> CcdEEpromReadData = new List<byte>();
        private enum eManualBtn : int
        {
            pcbTab = 0, lensTab
        };
        public CCdControl(int _w, int _h)
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(Form_Paint);
            
            this.Width = _w;
            this.Height = _h;

            setInterface();
            InitEEpromGrid();
        }

        private void InitEEpromGrid()
        {
            int i = 0;
            int j = 0;
            // 열 추가
            // 행 헤더 숨기기
            dataGridView_EEpromData.RowHeadersVisible = false;
            //사이즈 조절 막기
            dataGridView_EEpromData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // 행 높이 자동 조정
            dataGridView_EEpromData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; // 모든 셀에 맞게 자동 조정

            // 열 자동 크기 조정
            dataGridView_EEpromData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // 또는
            // 셀 내용 줄바꿈
            dataGridView_EEpromData.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // 헤더 폰트 설정
            dataGridView_EEpromData.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 9F, FontStyle.Bold);

            // 헤더 배경색 설정
            dataGridView_EEpromData.ColumnHeadersDefaultCellStyle.BackColor = Color.GhostWhite;// Color.FromArgb(94, 129, 244); //Color.LightBlue;
            // 헤더 폰트 색
            dataGridView_EEpromData.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dataGridView_EEpromData.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            dataGridView_EEpromData.RowsDefaultCellStyle.ForeColor = Color.Gray;

            // Set the selection background color for all the cells.
            //dataGridView_EEpromData.DefaultCellStyle.SelectionBackColor = Color.White;
            // dataGridView_EEpromData.DefaultCellStyle.SelectionForeColor = Color.Black;
            // dataGridView_EEpromData.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Empty;
            // dataGridView_EEpromData.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

            //dataGridView_EEpromData.DefaultCellStyle.SelectionForeColor = Color.Empty;

            //dataGridView_EEpromData.DefaultCellStyle.SelectionBackColor = Color.Empty;
            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            // dataGridView_EEpromData.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            // dataGridView_EEpromData.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Empty;

            dataGridView_EEpromData.EnableHeadersVisualStyles = false;
            // 열 헤더 가운데 정렬
            dataGridView_EEpromData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //운영 체제의 기본 시각적 스타일을 무시
            dataGridView_EEpromData.AllowUserToResizeRows = false;

            dataGridView_EEpromData.ReadOnly = true;
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            cellStyle.Font = new Font("나눔고딕", 10, FontStyle.Regular); // Change font and size
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center align text
            cellStyle.SelectionBackColor = Color.LightBlue;
            //cellStyle.SelectionForeColor = Color.Empty;

            dataGridView_EEpromData.DefaultCellStyle = cellStyle;

            DataGridViewTextBoxColumn[] textColumns = new DataGridViewTextBoxColumn[4];

            for (i = 0; i < 4; i++)
            {
                textColumns[i] = new DataGridViewTextBoxColumn();
            }

            //DataGridView
            textColumns[0].HeaderText = "No";
            textColumns[1].HeaderText = "Addr";
            textColumns[2].HeaderText = "Hex";
            textColumns[3].HeaderText = "ASCII";

            textColumns[0].Name = "No";
            textColumns[1].Name = "Addr";
            textColumns[2].Name = "Hex";
            textColumns[3].Name = "ASCII";




            dataGridView_EEpromData.Columns.Add(textColumns[0]);
            dataGridView_EEpromData.Columns.Add(textColumns[1]);
            dataGridView_EEpromData.Columns.Add(textColumns[2]);
            dataGridView_EEpromData.Columns.Add(textColumns[3]);

            for (i = 0; i < dataGridView_EEpromData.ColumnCount; i++)
            {
                dataGridView_EEpromData.Columns[i].Resizable = DataGridViewTriState.False;
            }

            dataGridView_EEpromData.Width = GridColWidth[0] + GridColWidth[1] + GridColWidth[2] + GridColWidth[3];

            int gridWidth = dataGridView_EEpromData.Width;

            dataGridView_EEpromData.Columns[0].Width = GridColWidth[0];
            dataGridView_EEpromData.Columns[1].Width = GridColWidth[1];
            dataGridView_EEpromData.Columns[2].Width = GridColWidth[2];
            dataGridView_EEpromData.Columns[3].Width = GridColWidth[3];



            // 행 높이 조정
            dataGridView_EEpromData.RowTemplate.Height = GridRowHeight; // 자동 추가되는 행 높이 설정
            dataGridView_EEpromData.ColumnHeadersHeight = GridHeaderHeight;

            //dataGridView_EEpromData.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;

            for (i = 0; i < EEpromGridRowViewCount; i++)
            {
                string text = $"예시 텍스트 {i}"; // 예시 텍스트 생성
                //bool isChecked = (i % 2 == 0); // 짝수인 경우 체크박스가 체크됨
                dataGridView_EEpromData.Rows.Add(""); // 행 추가
                dataGridView_EEpromData.Rows[i].Height = GridRowHeight;

                for (j = 0; j < dataGridView_EEpromData.ColumnCount; j++)
                {
                    //dataGridView.Columns[i].Resizable = DataGridViewTriState.False;
                    //dataGridView_EEpromData.Columns[j].Width = GridColWidth[j];
                    dataGridView_EEpromData.Columns[j].Resizable = DataGridViewTriState.False;
                    dataGridView_EEpromData.Rows[i].Cells[j].Value = "";
                }
            }
            dataGridView_EEpromData.Height = EEpromGridRowViewCount * GridRowHeight + GridRowHeight + 2;
            if (dataGridView_EEpromData.AllowUserToAddRows == true)
            {
                //dataGridView_Model.Rows[GridRowCount].Height = GridRowHeight;
            }


            dataGridView_EEpromData.MultiSelect = false; // 여러 개 선택 불가능
            dataGridView_EEpromData.AllowUserToAddRows = false; // 빈 행 추가 방지
            dataGridView_EEpromData.ScrollBars = ScrollBars.Vertical;      //가로 스크롤 안보이게 설정

            //dataGridView_EEpromData.CellContentClick += ModelGridView_CellContentClick;     //삭제 버튼 클릭시 사용
            // 버튼 클릭 이벤트 등록
            dataGridView_EEpromData.CellClick += new DataGridViewCellEventHandler(GridView_CellClick); //textbox 한번 클릭으로 바로 수정되게 추가
            //dataGridView_EEpromData.SelectionChanged += dataGridView1_SelectionChanged;
            // 이벤트 핸들러 추가
            //CardGrid.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(CardGrid_EditingControlShowing);
            //dataGridView_EEpromData.CellFormatting += dataGridView_Model_CellFormatting;

            GridInitWidth = dataGridView_EEpromData.Width;     //<---스크롤 생겼을때 사이즈 조절위해 초기 Grid 넓이 저장

            // 각 컬럼의 헤더 텍스트 정렬 설정
            foreach (DataGridViewColumn column in dataGridView_EEpromData.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //dataGridView_EEpromData.Columns[0].ReadOnly = true; // 읽기 전용


            dataGridView_EEpromData.Columns[0].DefaultCellStyle.BackColor = Color.LightGray; // 배경색 설정
            dataGridView_EEpromData.Columns[0].DefaultCellStyle.ForeColor = Color.Yellow; // 배경색 설정
            dataGridView_EEpromData.Columns[0].DefaultCellStyle.Font = new Font("나눔고딕", 10F, FontStyle.Bold); // 굵은 글씨
            dataGridView_EEpromData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 가운데 정렬

            dataGridView_EEpromData.ClearSelection();
        }

        private void GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Console.WriteLine($"DataGridView_CellClick");
            SelectedCellCol = e.ColumnIndex;
            SelectedCellRow = e.RowIndex;           //세로
        }
        
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            int lineStartY = ManualTitleLabel.Location.Y + 60;
            // Graphics 객체 가져오기
            Graphics g = e.Graphics;

            // Pen 객체 생성 (색상과 두께 설정)
            Color color = Color.FromArgb(175, 175, 175);//Color.FromArgb(151, 149, 145);
            Pen pen = new Pen(color, 1);

            // 라인 그리기 (시작점과 끝점 설정)
            g.DrawLine(pen, 0, lineStartY, this.Width, lineStartY);

            // 리소스 해제
            pen.Dispose();



           // Graphics g = this.CreateGraphics();
            // 지정된 펜츠로 폼에 사각형은 그립니다.
            //Pen pen1 = new Pen(Color.Red, 1);
            //Pen pen2 = new Pen(Color.Blue, 2);
            //Pen pen3 = new Pen(Color.Magenta, 10);

            //g.DrawLine(pen1, 10, 300, 100, 10);
            //g.DrawLine(pen2, new Point(10, 400), new Point(100, 400));
            //g.DrawLine(pen3, new Point(10, 500), new Point(150, 500));

            //pen1.Dispose();
            //pen2.Dispose();
            //pen3.Dispose();
        }
        public void setInterface()
        {

            ManualTitleLabel.ForeColor = ColorTranslator.FromHtml("#6F6F6F");

            m_iOffsetX_SFR = new int[Globalo.CHART_ROI_COUNT];
            m_iOffsetY_SFR = new int[Globalo.CHART_ROI_COUNT];
            m_iSizeX_SFR = new int[Globalo.CHART_ROI_COUNT];
            m_iSizeY_SFR = new int[Globalo.CHART_ROI_COUNT];

            m_rcRoiBox = new Rectangle[4];



        }
        public void DrawRectSfr(int mSelectIndex)
        {
            int i = 0;
            string mStrLog = "ccdtest";
            Globalo.vision.ClearOverlay(0, 1);

            for (i = 0; i < Globalo.CHART_ROI_COUNT; i++)
            {
                m_clRectROI[i].X = m_iOffsetX_SFR[i];
                m_clRectROI[i].Y = m_iOffsetY_SFR[i];
                m_clRectROI[i].Width = m_iSizeX_SFR[i];
                m_clRectROI[i].Height = m_iSizeY_SFR[i];

                if (mSelectIndex == i)
                {
                    Globalo.vision.DrawMOverlayBox(0, 0, m_clRectROI[i], Color.Moccasin, 1, false, DashStyle.Solid, 1);
                }
                else
                {
                    Globalo.vision.DrawMOverlayBox(0, 0, m_clRectROI[i], Color.Blue, 1, false, DashStyle.Solid, 1);
                }
                mStrLog = i.ToString();

                Globalo.vision.DrawMOverlayText(0, m_clRectROI[i].X, m_clRectROI[i].Y, mStrLog, Color.GreenYellow, "Arial", 12, false, 1);
            }
            for (i = 0; i < 4; i++)
            {
                //원형마크
                m_rcRoiBox[i] = m_rcRoiBox[i];
                if ((mSelectIndex - Globalo.CHART_ROI_COUNT) == i)
                {
                    Globalo.vision.DrawMOverlayBox(0, 0, m_rcRoiBox[i], Color.Green, 2, false, DashStyle.DashDotDot, 1);
                }
                else
                {
                    Globalo.vision.DrawMOverlayBox(0, 0, m_rcRoiBox[i], Color.Green, 1, false, DashStyle.DashDotDot, 1);
                }

            }
            //Cross
            System.Drawing.Point clPos = new System.Drawing.Point();
            clPos.X = Globalo.mLaonGrabberClass.m_nWidth / 2;
            clPos.Y = Globalo.mLaonGrabberClass.m_nHeight / 2;

            Globalo.vision.DrawMOverlayCross(0, 0, clPos, 300, Color.Gray, 1, false, 0);



            Globalo.vision.DrawOverlayAll(0, 1);
        }
        //-----------------------------------------------------------------------------
        //
        //	모델 데이터의 ROI 영역을 내부에 저장
        //
        //-----------------------------------------------------------------------------
        public void SetSfrRoi()
        {
            int nCount = 0;
            int i = 0;

            nCount = Globalo.yamlManager.imageData.chartData.SfrPosX.Count;
            for (i = 0; i < nCount; i++)
            {
                m_iOffsetX_SFR[i] = Globalo.yamlManager.imageData.chartData.SfrPosX[i];
            }

            nCount = Globalo.yamlManager.imageData.chartData.SfrPosY.Count;
            for (i = 0; i < nCount; i++)
            {
                m_iOffsetY_SFR[i] = Globalo.yamlManager.imageData.chartData.SfrPosY[i];
            }

            nCount = Globalo.yamlManager.imageData.chartData.SfrSizeX.Count;
            for (i = 0; i < nCount; i++)
            {
                m_iSizeX_SFR[i] = Globalo.yamlManager.imageData.chartData.SfrSizeX[i];
            }

            nCount = Globalo.yamlManager.imageData.chartData.SfrSizeY.Count;
            for (i = 0; i < nCount; i++)
            {
                m_iSizeY_SFR[i] = Globalo.yamlManager.imageData.chartData.SfrSizeY[i];
            }

            for (i = 0; i < 4; i++)
            {
                m_rcRoiBox[i].X = Globalo.yamlManager.imageData.chartData.CirClePosX[i];
                m_rcRoiBox[i].Y = Globalo.yamlManager.imageData.chartData.CirClePosY[i];
                m_rcRoiBox[i].Width = Globalo.yamlManager.imageData.chartData.CirCleSizeX[i];
                m_rcRoiBox[i].Height = Globalo.yamlManager.imageData.chartData.CirCleSizeY[i];
            }

            //nCount = Globalo.CHART_ROI_COUNT;
            //for (i = 0; i < nCount; i++)
            //{
            //    m_iOffsetX_SFR[i] = Globalo.dataManage.workData.m_clSfrInfo.m_clPtOffset[i].X;
            //    m_iOffsetY_SFR[i] = Globalo.dataManage.workData.m_clSfrInfo.m_clPtOffset[i].Y;
            //    m_iSizeX_SFR[i] = Globalo.dataManage.workData.m_clSfrInfo.m_nSizeX[i];
            //    m_iSizeY_SFR[i] = Globalo.dataManage.workData.m_clSfrInfo.m_nSizeY[i];
            //}

            //for (i = 0; i < 4; i++)
            //{
            //    m_rcRoiBox[i] = Globalo.dataManage.workData.m_CircleP[i];
            //}
        }
        public int checkNoSFR(System.Drawing.Point point)
        {
            double dExpandFactorX = 0.0;
            double dExpandFactorY = 0.0;
            dExpandFactorX = Globalo.vision.M_CcdExpandFactorX;
            dExpandFactorY = Globalo.vision.M_CcdExpandFactorY;

            Rectangle rcTemp = new Rectangle();
            System.Drawing.Point p = new System.Drawing.Point();

            p.X = (int)(point.X * dExpandFactorX + 0.5);
            p.Y = (int)(point.Y * dExpandFactorY + 0.5);

            int i = 0;
            int iGap = (int)(dExpandFactorX * 3);

            for (i = 0; i < Globalo.CHART_ROI_COUNT; i++)
            {
                rcTemp.X = m_iOffsetX_SFR[i];// - iGap;
                rcTemp.Y = m_iOffsetY_SFR[i];// - iGap;
                rcTemp.Width = m_iSizeX_SFR[i] + iGap;
                rcTemp.Height = m_iSizeY_SFR[i] + iGap;

                if (rcTemp.Contains(p))//if (PtInRect(rcTemp, p))
                {
                    m_iCurNo_SFR = i;
                    return i;
                }
            }
            for (i = 0; i < 4; i++)
            {
                rcTemp.X = m_rcRoiBox[i].X;
                rcTemp.Y = m_rcRoiBox[i].Y;
                rcTemp.Width = m_rcRoiBox[i].Width + iGap;
                rcTemp.Height = m_rcRoiBox[i].Height + iGap;

                if (rcTemp.Contains(p))
                {
                    m_iCurNo_SFR = i + Globalo.CHART_ROI_COUNT;
                    return m_iCurNo_SFR;
                }
            }

            return -1;
        }
        private void ManualBtnChange(eManualBtn index)
        {
            BTN_MANUAL_PCB.BackColor = ColorTranslator.FromHtml("#E1E0DF");
            BTN_MANUAL_LENS.BackColor = ColorTranslator.FromHtml("#E1E0DF");

            

            //if (index == eManualBtn.pcbTab)
            //{
            //    BTN_MANUAL_PCB.BackColor = ColorTranslator.FromHtml("#FFB230");
            //    manualPcb.Visible = true;
            //    manualLens.Visible = false;
            //}
            //else
            //{
            //    BTN_MANUAL_LENS.BackColor = ColorTranslator.FromHtml("#FFB230");
            //    manualLens.Visible = true;
            //    manualPcb.Visible = false;
            //}
        }
        private void BTN_MANUAL_PCB_Click(object sender, EventArgs e)
        {
            ManualBtnChange(eManualBtn.pcbTab);
        }

        private void BTN_MANUAL_LENS_Click(object sender, EventArgs e)
        {
            ManualBtnChange(eManualBtn.lensTab);
        }

        private void BTN_CCD_GABBER_OPEN_Click(object sender, EventArgs e)
        {
            Globalo.LogPrint("", "[CCD] MANUAL CCD OPEN");
            Globalo.mLaonGrabberClass.OpenDevice();
        }

        private void BTN_CCD_GABBER_START_Click(object sender, EventArgs e)
        {
            Globalo.mLaonGrabberClass.StartGrab();
        }

        private void BTN_CCD_GABBER_STOP_Click(object sender, EventArgs e)
        {
            Globalo.mLaonGrabberClass.StopGrab();
        }

        private void BTN_CCD_GABBER_CLOSE_Click(object sender, EventArgs e)
        {
            Globalo.mLaonGrabberClass.CloseDevice();
        }

        private void BTN_CCD_BMP_LOAD_Click(object sender, EventArgs e)
        {

        }

        private void BTN_CCD_BMP_SAVE_Click(object sender, EventArgs e)
        {

        }

        private void BTN_CCD_RAW_LOAD_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "d:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    byte[] m_pImgBuff;


                    //Read the contents of the file into a stream
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        int fileSize = (int)fileStream.Length;
                        m_pImgBuff = new byte[fileSize];
                        fileStream.Read(m_pImgBuff, 0, fileSize);

                        //화면에 보여주기
                        CCARawImageLoad(ref m_pImgBuff, 0, false);
                    }

                }
            }
        }

        private void BTN_CCD_RAW_SAVE_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "raw Image|*.raw";
            saveFileDialog1.Title = "Save an Image Raw File";
            saveFileDialog1.ShowDialog();
            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                fs.Write(Globalo.mLaonGrabberClass.m_pFrameRawBuffer, 0, Globalo.mLaonGrabberClass.nFrameRawSize);
            }
        }
        public void CCARawImageLoad(ref byte[] LoadImg, int index, bool autoRun)
        {
            double dZoomX = 0.0;
            double dZoomY = 0.0;

            dZoomX = Globalo.vision.M_CcdReduceFactorX;
            dZoomY = Globalo.vision.M_CcdReduceFactorY;

            //oGlobal.mLaonGrabberClass.m_pFrameRawBuffer = LoadImg;
            int rSize = Globalo.mLaonGrabberClass.nFrameRawSize;// MIU.m_pBoard->GetFrameRawSize();
            Array.Copy(LoadImg, Globalo.mLaonGrabberClass.m_pFrameRawBuffer, rSize);


            //TDATASPEC tspec;
            //tspec.eOutMode = gMIUDevice.dTDATASPEC_n.eOutMode; ;
            //tspec.eDataFormat = gMIUDevice.dTDATASPEC_n.eDataFormat; ;
            //tspec.eSensorType = gMIUDevice.dTDATASPEC_n.eSensorType; ;



            //memcpy(MIU.m_pFrameRawBuffer, LoadImg, rSize);


            IntPtr RawPtr = Marshal.UnsafeAddrOfPinnedArrayElement(LoadImg, 0);
            IntPtr BmpPtr = Marshal.UnsafeAddrOfPinnedArrayElement(Globalo.mLaonGrabberClass.m_pFrameBMPBuffer, 0);
            //ACMISSoftISP::xMakeBMP(MIU.m_pFrameRawBuffer, (byte*)MIU.m_pFrameBMPBuffer, gMIUDevice.nWidth, gMIUDevice.nHeight, tspec);
            //if (Globalo.mLaonGrabberClass.GrabberDll.mGetFrame((byte*)RawPtr.ToPointer(), (byte*)BmpPtr.ToPointer()) == true)
            unsafe
            {
                TDATASPEC tSpec = new TDATASPEC();
                tSpec.eDataFormat = Globalo.mLaonGrabberClass.mGrabSpec.eDataFormat;    // EDATAFORMAT.DATAFORMAT_BAYER_12BIT;     
                tSpec.eSensorType = Globalo.mLaonGrabberClass.mGrabSpec.eSensorType;    //ESENSORTYPE.SENSORTYPE_RGGB;       
                tSpec.eOutMode = Globalo.mLaonGrabberClass.mGrabSpec.eOutMode;          //EOUTMODE.OUTMODE_BAYER_BGGR;
                tSpec.nBlackLevel = 0;

                IntPtr structPtr = Marshal.AllocHGlobal(Marshal.SizeOf<TDATASPEC>());
                Marshal.StructureToPtr(tSpec, structPtr, false);

                Globalo.GrabberDll.mxMakeBMP((byte*)RawPtr.ToPointer(), (byte*)BmpPtr.ToPointer(), Globalo.mLaonGrabberClass.m_nWidth, Globalo.mLaonGrabberClass.m_nHeight, structPtr);
            }


            Globalo.mLaonGrabberClass.imageItp = new Mat(Globalo.mLaonGrabberClass.m_nHeight, Globalo.mLaonGrabberClass.m_nWidth, MatType.CV_8UC3);//, Globalo.mLaonGrabberClass.m_pFrameBMPBuffer);
            Globalo.mLaonGrabberClass.imageItp.SetArray<byte>(Globalo.mLaonGrabberClass.m_pFrameBMPBuffer); // 배열 데이터를 Mat에 복사


            Cv2.ExtractChannel(Globalo.mLaonGrabberClass.imageItp, Globalo.mLaonGrabberClass.m_pImageBuff[0], 0);
            Cv2.ExtractChannel(Globalo.mLaonGrabberClass.imageItp, Globalo.mLaonGrabberClass.m_pImageBuff[1], 1);
            Cv2.ExtractChannel(Globalo.mLaonGrabberClass.imageItp, Globalo.mLaonGrabberClass.m_pImageBuff[2], 2);

            byte[] bytes2 = new byte[Globalo.mLaonGrabberClass.m_nHeight * Globalo.mLaonGrabberClass.m_nWidth];

            Marshal.Copy(Globalo.mLaonGrabberClass.m_pImageBuff[2].Data, bytes2, 0, bytes2.Length); // Mat 데이터를 바이트 배열로 복사
            MIL.MbufPut(Globalo.vision.m_MilCcdProcChild[0, 0], bytes2);
            Marshal.Copy(Globalo.mLaonGrabberClass.m_pImageBuff[1].Data, bytes2, 0, bytes2.Length); // Mat 데이터를 바이트 배열로 복사
            MIL.MbufPut(Globalo.vision.m_MilCcdProcChild[0, 1], bytes2);
            Marshal.Copy(Globalo.mLaonGrabberClass.m_pImageBuff[0].Data, bytes2, 0, bytes2.Length); // Mat 데이터를 바이트 배열로 복사
            MIL.MbufPut(Globalo.vision.m_MilCcdProcChild[0, 2], bytes2);

            //Globalo.mLaonGrabberClass.imageItp.SaveImage("d:\\imageItp.jpg");
            //Globalo.mLaonGrabberClass.m_pImageBuff[2].SaveImage("d:\\m_pImageBuff.jpg");
            MIL.MimResize(Globalo.vision.m_MilCcdProcImage[0], Globalo.vision.m_MilSmallImage[0], dZoomX, dZoomY, MIL.M_DEFAULT);
        }
        public bool _GetMtf()
        {
            int i = 0;
            GetChartRoiSet();

            double[] mydouble = new double[4];

            //mydouble[2] = pixelsize
            //mydouble[3] = LinePulse
            //mydouble[4] = Direction
            //mydouble[5] = SmallSfrRoiWidth
            //mydouble[6] = SmallSfrRoiWidth
            //mydouble[7] = SmallSfrRoiWidth
            mydouble[0] = 50.0;  //SmallSfrRoiWidth
            mydouble[1] = 40.0; //SmallSfrRoiHeight
            mydouble[2] = 3.75; //pixelsize
            mydouble[3] = 0.225; //LinePulse
            //
            //roi start
            double[] mRoix = new double[Globalo.MTF_ROI_COUNT];
            double[] mRoiy = new double[Globalo.MTF_ROI_COUNT];

            for (i = 0; i < Globalo.MTF_ROI_COUNT; i++)
            {
                mRoix[i] = (int)Globalo.dataManage.TaskWork.rtSfrSmallRect[i].X;
                mRoiy[i] = (int)Globalo.dataManage.TaskWork.rtSfrSmallRect[i].Y;
            }

            //
            //

            IntPtr RawPtr = Marshal.UnsafeAddrOfPinnedArrayElement(Globalo.mLaonGrabberClass.m_pFrameRawBuffer, 0);
            IntPtr doublePtr = Marshal.UnsafeAddrOfPinnedArrayElement(mydouble, 0);
            IntPtr doublePtr2 = Marshal.UnsafeAddrOfPinnedArrayElement(mRoix, 0);
            IntPtr doublePtr3 = Marshal.UnsafeAddrOfPinnedArrayElement(mRoiy, 0);
            int mWidth = Globalo.mLaonGrabberClass.m_nWidth;
            int mHeight = Globalo.mLaonGrabberClass.m_nHeight;
            int mRoiCount = Globalo.MTF_ROI_COUNT;
            int mLength = mydouble.Length;

            double[] mGetSfr = new double[Globalo.MTF_ROI_COUNT];
            IntPtr GetPtr = Marshal.UnsafeAddrOfPinnedArrayElement(mGetSfr, 0);
            unsafe
            {
                bool brtn = Globalo.GrabberDll.g_GetSFR((byte*)RawPtr.ToPointer(), mWidth, mHeight, mRoiCount, (double*)doublePtr, mLength, (double*)doublePtr2, (double*)doublePtr3, (double*)GetPtr);
            }
            return true;
        }
        public bool GetChartRoiSet()
        {
            int i = 0;
            int nRtn = -1;
            Globalo.vision.ClearOverlay(0, 1);
            MIL.MbufGet(Globalo.vision.m_MilCcdProcChild[0, 1], Globalo.vision.m_pImgBuff[0][1]);
            nRtn = Globalo.vision.FineCirclePos(ref Globalo.vision.m_pImgBuff[0][1]);
            if (nRtn > 0)
            {
                //원형마크 찾기 실패
                return false;
            }
            //
            //
            //
            for (i = 0; i < Globalo.CHART_ROI_COUNT; i++)
            {
                nRtn = Globalo.vision.FindRectPos(ref Globalo.vision.m_pImgBuff[0][1], i);
                if (nRtn > 0)
                {
                    //차트 찾기 실패
                }
                else
                {
                    Globalo.vision.FindSfrRectPos(i, Globalo.dataManage.TaskWork.rtChartRect[i]);
                }
            }

            Globalo.vision.DrawOverlayAll(0, 1);

            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool kk = CLaonGrabberClass.SensorIdRead_Head_Fn();
        }
        public void ShowEEpromGrid(ushort startAddr = 0, int dataLenght = 0)
        {
            Console.WriteLine("dataLenght: " + dataLenght.ToString());


            int i = 0;  //옆

            int nCol = dataGridView_EEpromData.ColumnCount;         //7 옆으로 행
            int nRow = dataGridView_EEpromData.RowCount;        //0 아래로 열 빈칸 -1

            int dataCount = dataLenght;
            if(dataCount < 1)
            {
                return;
            }
            //Globalo.mCCdPanel.CcdEEpromReadData.AddRange(EEpromReadData);


            dataGridView_EEpromData.Rows.Clear();

            int gridViewCount = dataCount;

            if (gridViewCount < EEpromGridRowViewCount)
            {
                gridViewCount = EEpromGridRowViewCount;
            }

            for (i = 0; i < gridViewCount; i++)
            {
                if(i < dataCount)
                {
                    char displayChar = (char)Globalo.mCCdPanel.CcdEEpromReadData[i];
                    if (displayChar == '\0') // null 문자 처리
                    {
                        displayChar = ' '; // 공백으로 대체
                    }
                    dataGridView_EEpromData.Rows.Add((i + 1).ToString(), "0x" + (startAddr + i).ToString("X2"), "0x" + Globalo.mCCdPanel.CcdEEpromReadData[i].ToString("X2"), displayChar);// (char)Globalo.mCCdPanel.CcdEEpromReadData[i]);

                    //Globalo.mLaonGrabberClass.eepromDicData
                }
                else
                {
                    dataGridView_EEpromData.Rows.Add("", "", "", ""); // 행 추가
                    
                }
                dataGridView_EEpromData.Rows[i].Cells[1].Style.BackColor = Color.White; // 1번 열
                dataGridView_EEpromData.Rows[i].Cells[1].Style.ForeColor = Color.Black; // 1번 열
                dataGridView_EEpromData.Rows[i].Cells[2].Style.BackColor = Color.White; // 1번 열
                dataGridView_EEpromData.Rows[i].Cells[2].Style.ForeColor = Color.Black; // 1번 열
                dataGridView_EEpromData.Rows[i].Cells[3].Style.BackColor = Color.White; // 1번 열
                dataGridView_EEpromData.Rows[i].Cells[3].Style.ForeColor = Color.Black; // 1번 열
                dataGridView_EEpromData.Rows[i].Cells[1].Style.Font = new Font(dataGridView_EEpromData.DefaultCellStyle.Font, FontStyle.Regular);
                dataGridView_EEpromData.Rows[i].Cells[2].Style.Font = new Font(dataGridView_EEpromData.DefaultCellStyle.Font, FontStyle.Regular);
                dataGridView_EEpromData.Rows[i].Cells[3].Style.Font = new Font(dataGridView_EEpromData.DefaultCellStyle.Font, FontStyle.Regular);
            }


            if (gridViewCount > EEpromGridRowViewCount)
            {
                dataGridView_EEpromData.Width = GridInitWidth + 20; //스크롤 추가시 grid Width 조정
            }
            dataGridView_EEpromData.ClearSelection();
        }
        private void BTN_CCD_EEPROM_VERIFY_TEST_Click(object sender, EventArgs e)
        {
            int i = 0;
            //제품에서 읽은 값
            //Globalo.mCCdPanel.CcdEEpromReadData.Clear();

            //MES 에서 받은 값
            //Globalo.dataManage.mesData.VMesEEpromData.Clear();

            //Address 0 / size 1
            //Address 1 / size 1
            //Address 2 / size 4
            //Address 6 / size 4
            //Address 10 / size 1
            //Address 11 / size 6
            //Address 17 / size 14

            //두개 비교
            int eepromCount = Globalo.dataManage.mesData.VMesEEpromData.Count();


            if (Globalo.dataManage.eepromData.dataList == null)
            {
                Console.WriteLine("Globalo.dataManage.eepromData.dataList null");
                return;
            }

            if (CcdEEpromReadData == null)
            {
                Console.WriteLine("CcdEEpromReadData null");
                return;
            }


            


            int TotalCount = Globalo.dataManage.eepromData.dataList.Count;


            string logData = $"csv 에서 로드한 항목 개수:{TotalCount}";
            Globalo.LogPrint("CCdControl", logData);

            logData = $"마지막 Address: {Globalo.dataManage.eepromData.dataList[TotalCount - 1].ADDRESS}";
            Globalo.LogPrint("CCdControl", logData);

            logData = $"마지막 Data Size:{Globalo.dataManage.eepromData.dataList[TotalCount - 1].DATA_SIZE}";
            Globalo.LogPrint("CCdControl", logData);



            //마지막 Address 가 184이고 Data Size 가 2 이면   
            //184 , 185 2개를 읽는 거기 때문에 Address 더하기 Data Size 하면 된다 전체 읽어야될 수 = 184 + 2 = 186
            //string dataHex = BitConverter.ToString(CcdEEpromReadData.GetRange(0, 5).ToArray()).Replace("-", " ");


            //string dataHex = BitConverter.ToString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(0, 5).ToArray()).Replace("-", " ");     //0x10 0x12 -> 10 12
            //string dataDec = string.Join(" ", Globalo.mCCdPanel.CcdEEpromReadData.GetRange(0, 5));     //0x10 0x12 -> 10 12

            //string dataAscii = Encoding.ASCII.GetString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(0, 5).ToArray());     //
            //float dataFloat = BitConverter.ToSingle(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(0, 5).ToArray(), 0);     //float <- Big Endian / Little Endian 여부도 확인해야 해
            //double dataDouble = BitConverter.ToDouble(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(0, 5).ToArray(), 0);
            //Big Endian 상위 바이트(큰 값)가 앞에 저장
            //Little Endian 하위 바이트 (작읍 값) 가 앞에 저장됨
            //byte[] bigEndianBytes = { 0x41, 0x20, 0x00, 0x00 };
            // float value = BitConverter.ToSingle(bigEndianBytes, 0);

            //byte[] littleEndianBytes = { 0x00, 0x00, 0x20, 0x41 };
            //Array.Reverse(littleEndianBytes); // 바이트 순서 변경 (Big → Little)
            //float value = BitConverter.ToSingle(littleEndianBytes, 0);


            //int dataInt16 = BitConverter.ToInt16(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(0, 5).ToArray(), 0);     //
            //int dataInt32 = BitConverter.ToInt32(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(0, 5).ToArray(), 0);     //


            //string data = Globalo.mCCdPanel.CcdEEpromReadData.GetRange(0, 5).ToArray().ToString();

            string readData = "";
            string MES_EEPROM_VALUE = "";
            string EEPROM_READ_VALUE = "";
            int startAddress = 0;
            int readCount = 0;

            //FIX_YN 이 Y 이고 , BYTE_ORDER 이 Little 이면 뒤집어서 비교해야된다.
            //FIX_YN 이 Y면 DATA_FORMAT에 표기된 포맷으로 ITEM_VALUE가 전달된다   DOUBLE = 0
            for (i = 0; i < TotalCount; i++)
            {
                startAddress = Globalo.dataManage.eepromData.dataList[i].ADDRESS;
                readCount = Globalo.dataManage.eepromData.dataList[i].DATA_SIZE;

                if (Globalo.dataManage.eepromData.dataList[i].DATA_FORMAT == Data.CEEpromData.HEX)
                {
                    readData = BitConverter.ToString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray()).Replace("-", "");//Replace("-", " ");
                    Console.WriteLine($"HEX: {readData}");
                }
                else if (Globalo.dataManage.eepromData.dataList[i].DATA_FORMAT == Data.CEEpromData.ASCII)
                {
                    readData = Encoding.ASCII.GetString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray());
                    Console.WriteLine($"ASCII: {readData}");
                }
                else if (Globalo.dataManage.eepromData.dataList[i].DATA_FORMAT == Data.CEEpromData.FLOAT)
                {
                    readData = BitConverter.ToSingle(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray(), 0).ToString();
                    Console.WriteLine($"FLOAT: {readData}");
                }
                else if (Globalo.dataManage.eepromData.dataList[i].DATA_FORMAT == Data.CEEpromData.DOUBLE)
                {
                    readData = BitConverter.ToSingle(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray(), 0).ToString();
                    Console.WriteLine($"DOUBLE: {readData}");
                }
                else if (Globalo.dataManage.eepromData.dataList[i].DATA_FORMAT == Data.CEEpromData.EMPTY)       //0xFF로 채워져서 HEX로 하면 될 듯
                {
                    readData = BitConverter.ToString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray()).Replace("-", " ");
                    Console.WriteLine($"EMPTY: {readData}");
                }
                 
                
                
                if (Globalo.dataManage.eepromData.dataList[i].DATA_FORMAT == Data.CEEpromData.CRC_CRC8_SAE_J1850_ZERO)
                {
                    //값을 읽어서 계산 필요
                    //byte[] fordData = { 0x01, 0x00, 0x67, 0xAD, 0x57, 0xE9, 0xFF, 0xFF, 0xFF, 0xFF };
                    //byte fordcrc8_sae_j1850_zero = ComputeCRC8(fordData, 0x1D, 0x00, 0x00); // CRC8_SAE_J1850_ZERO
                    startAddress = int.Parse(Globalo.dataManage.eepromData.dataList[i].CRC_START);
                    readCount = int.Parse(Globalo.dataManage.eepromData.dataList[i].CRC_END);

                    byte fordcrc8_sae_j1850_zero = Data.CEEpromData.ComputeCRC8(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount - startAddress + 1).ToArray(), 0x1D, 0x00, 0x00);
                    Console.WriteLine($"CRC_CRC8_SAE_J1850_ZERO: {fordcrc8_sae_j1850_zero}");

                    EEPROM_READ_VALUE = fordcrc8_sae_j1850_zero.ToString("X4");
                }
                else if (Globalo.dataManage.eepromData.dataList[i].DATA_FORMAT == Data.CEEpromData.CRC_CHECKSUM_RFC1071)
                {
                    //값을 읽어서 계산 필요
                    startAddress = int.Parse(Globalo.dataManage.eepromData.dataList[i].CRC_START);
                    readCount = int.Parse(Globalo.dataManage.eepromData.dataList[i].CRC_END);

                    ushort checksum_rf1071 = Data.CEEpromData.ComputeRFC1071Checksum(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount - startAddress + 1).ToArray());

                    Console.WriteLine($"CRC_CHECKSUM_RFC1071: {checksum_rf1071}");


                    // TODO: LGIT AM Paju		Verify	CRC_FOR_SERIAL_NUMBER	66	2	CRC_CHECKSUM_RFC1071	Little	N	EEP_CAL_CRC_SN	0x10FC
                    //checksum_rf1071 = 0xFC10 으로 계산되어 나온다 , Little 면 뒤집어서 비교해야된다.
                    EEPROM_READ_VALUE = checksum_rf1071.ToString("X4");
                }
                else
                {
                    EEPROM_READ_VALUE = BitConverter.ToString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray()).Replace("-", "");
                }



                //FIX_YN 이 Y 이고 , BYTE_ORDER 이 Little 이면 뒤집어서 비교해야된다.

                //FIX_YN 이 Y면 DATA_FORMAT에 표기된 포맷으로 ITEM_VALUE가 전달된다   DOUBLE = 0

                



                //EEPROM_READ_VALUE = Globalo.mCCdPanel.CcdEEpromReadData 에서 변환해야된다.

                if (Globalo.dataManage.eepromData.dataList[i].FIX_YN == "Y")
                {
                    //FIX_YN 이 Y 이고 , DATA_FORMAT 이 HEX 가 아니면 HEX로 변환해서 비교해야된다.

                    //ASCII   -> HEX
                    //FLOAT   -> HEX
                    //DOUBLE  -> HEX
                    //DEC     -> HEX
                    //ASCIIToHex
                    
                }

                //Globalo.dataManage.mesData.VMesEEpromData.Add(tempData);


                //eeprom에서 읽은값 전부 hex라서 변활할 필요가 없다.
                MES_EEPROM_VALUE = Data.CEEpromData.StringToHex(Globalo.dataManage.eepromData.dataList[i].ITEM_VALUE,
                    Globalo.dataManage.eepromData.dataList[i].DATA_FORMAT, 
                    Globalo.dataManage.eepromData.dataList[i].BYTE_ORDER, 
                    Globalo.dataManage.eepromData.dataList[i].FIX_YN);

                //Globalo.mLaonGrabberClass.eepromDicData
                //mes를 뒤집어야지 eeprom 읽은값은 안 뒤집어도 된다?
                //if(Globalo.dataManage.eepromData.dataList[i].BYTE_ORDER == "Little")
                //{
                //    //뒤집어야된다
                //    EEPROM_READ_VALUE = BitConverter.ToString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray().Reverse().ToArray()).Replace("-", "");
                //}
                //else
                //{
                //    EEPROM_READ_VALUE = BitConverter.ToString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray()).Replace("-", "");
                //}

                
                if (EEPROM_READ_VALUE == MES_EEPROM_VALUE)
                {
                    //OK
                    Console.WriteLine($"{EEPROM_READ_VALUE} == {MES_EEPROM_VALUE} OK");
                }
                else
                {
                    //NG
                    Console.WriteLine($"{EEPROM_READ_VALUE} == {MES_EEPROM_VALUE} NG");
                }

            }
        }
        public void EEpromRead()
        {
            //testEEpromRead();
            if (this.InvokeRequired)
            {
                //this.Invoke(new Action(testEEpromRead));
                bool Rtn = (bool)this.Invoke(new Func<bool>(() =>testEEpromRead()));
            }
            else
            {
                testEEpromRead();
            }

            
        }
        public static unsafe bool testEEpromRead()
        {
            int i = 0;

            Console.WriteLine("testEEpromRead run");

            string slaveAddr = Regex.Replace(Globalo.mCCdPanel.textBox_SlaveAddr.Text, @"\D", "");

            string readAddr = Regex.Replace(Globalo.mCCdPanel.textBox_ReadAddr.Text, @"\D", "");
            //string numericPart = Regex.Replace(input, @"\D", "");  // 숫자가 아닌 부분(\D)을 제거
            ushort readDataLength = Convert.ToUInt16(Globalo.mCCdPanel.textBox_ReadDataLeng.Text);  //읽어야될 길이

            if(readDataLength < 1)
            {
                return false;
            }

            ushort maxReadLength = 90;// CLaonGrabberClass.MAX_READ_WRITE_LENGTH;
            if (maxReadLength > readDataLength)
            {
                maxReadLength = readDataLength;
            }

            
            //Int32.Parse(input);
            int errorCode = 0;


            int endAddress = readDataLength;//// 0xE0;  //       241
            //0x513;     //1299
            ushort SlaveAddr = Convert.ToUInt16(slaveAddr, 16); // 0x50;
            ushort StartAddr = Convert.ToUInt16(readAddr, 16); //0x00;

            //ushort checkAddr = 0x3C06;

            byte[] EEpromReadData = new byte[endAddress]; // EEPROM 데이터 읽기
            //if(Globalo.mLaonGrabberClass.EEpromReadData == null)
            //{
            //    Globalo.mLaonGrabberClass.EEpromReadData = new byte[endAddress + 0];
            //}
            //else
            //{
            //    if (Globalo.mLaonGrabberClass.EEpromReadData == null || Globalo.mLaonGrabberClass.EEpromReadData.Length != (endAddress + 0))
            //    {
            //        Array.Resize(ref Globalo.mLaonGrabberClass.EEpromReadData, endAddress + 0);
            //    }
            //}
            
            //byte[] pReadData = new byte[260]; // MAX_PATH 대신 일반적인 크기(예: 260) 사용

            //Array.Clear(Globalo.mLaonGrabberClass.EEpromReadData, 0, Globalo.mLaonGrabberClass.EEpromReadData.Length);
            //Array.Clear(pReadData, 0, pReadData.Length);



            Globalo.mCCdPanel.CcdEEpromReadData.Clear();
            

            for (i = 0; i < endAddress; i+= maxReadLength)     // 0;  i < 129;  i += 30; 
            {
                fixed (byte* pData = EEpromReadData)
                {
                    if((i + maxReadLength) > endAddress)
                    {
                        //if( ( 0 + 30 ) > 129
                        //if( ( 30 + 30 ) > 129
                        //if( ( 60 + 30 ) > 129
                        //if( ( 90 + 30 ) > 129
                        //if( ( 120 + 30 ) > 129
                        //150

                        maxReadLength = (ushort)((endAddress - i) + 0);    //120 ~ 129 는 10개라서 + 1
                    }
                    errorCode = Globalo.GrabberDll.mReadI2CBurst(SlaveAddr, (ushort)(StartAddr + i), 2, pData + i, (ushort)maxReadLength);
                    if (errorCode != 0)
                    {
                        Console.WriteLine("mReadI2CBurst errorCode");
                        break;
                    }
                }
            }

            Globalo.mLaonGrabberClass.eepromDicData.Clear();
            for (i = 0; i < endAddress; i++)
            {
                Globalo.mLaonGrabberClass.eepromDicData.Add((ushort)i, EEpromReadData[i]);
            }
            Globalo.mCCdPanel.CcdEEpromReadData.AddRange(EEpromReadData);

            Globalo.mCCdPanel.ShowEEpromGrid(StartAddr, Globalo.mCCdPanel.CcdEEpromReadData.Count);

            //string asciiString = "";
            //for (i = 209; i < 225; i ++)
            //{
            //    asciiString += (char)EEpromReadData[i]; // ASCII 문자로 변환 후 문자열 추가
            //}
            //int leng = asciiString.Length;
            return true;
        }
        private void BTN_CCD_EEPROM_READ_Click(object sender, EventArgs e)
        {
            if(Globalo.threadControl.manualThread.GetThreadRun() == false)
            {
                Globalo.threadControl.manualThread.runfn(1);
            }
            

            

            //EEPROM_TotalRead_Type2(0x0000, 0x513, CompareEEpromData, 512);//최대 32씩만	0x512	0x46D
        }
        
        private void CCdControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ShowEEpromGrid();
                //Chart roi on

                SetSfrRoi();
                if (ProgramState.ON_LINE_MIL)
                {
                    DrawRectSfr(999);
                }

            }
            else
            {
                if (Globalo.threadControl.manualThread.GetThreadRun())
                {
                    Globalo.threadControl.manualThread.Stop();
                }
            }
        }

        public void GetSfrRoi()
        {
            int nCount;
            int i;

            nCount = 9;// oGlobal.CHART_ROI_COUNT;

            nCount = Globalo.yamlManager.imageData.chartData.SfrPosX.Count;
            for (i = 0; i < nCount; i++)
            {
                Globalo.yamlManager.imageData.chartData.SfrPosX[i] = m_iOffsetX_SFR[i];
            }
            nCount = Globalo.yamlManager.imageData.chartData.SfrPosY.Count;
            for (i = 0; i < nCount; i++)
            {
                Globalo.yamlManager.imageData.chartData.SfrPosY[i] = m_iOffsetY_SFR[i];
            }

            nCount = Globalo.yamlManager.imageData.chartData.SfrSizeX.Count;
            for (i = 0; i < nCount; i++)
            {
                Globalo.yamlManager.imageData.chartData.SfrSizeX[i] = m_iSizeX_SFR[i];
            }

            nCount = Globalo.yamlManager.imageData.chartData.SfrSizeY.Count;
            for (i = 0; i < nCount; i++)
            {
                Globalo.yamlManager.imageData.chartData.SfrSizeY[i] = m_iSizeY_SFR[i];
            }


            for (i = 0; i < 4; i++)
            {
                Globalo.yamlManager.imageData.chartData.CirClePosX[i] = m_rcRoiBox[i].X;
                Globalo.yamlManager.imageData.chartData.CirClePosY[i] = m_rcRoiBox[i].Y;
                Globalo.yamlManager.imageData.chartData.CirCleSizeX[i] = m_rcRoiBox[i].Width;
                Globalo.yamlManager.imageData.chartData.CirCleSizeY[i] = m_rcRoiBox[i].Height;
            }


        }
        private void BTN_CCD_ROI_SAVE_Click(object sender, EventArgs e)
        {
            GetSfrRoi();

            Globalo.yamlManager.imageDataSave();
        }

        
    }
}
