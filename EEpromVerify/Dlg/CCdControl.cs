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

namespace ApsMotionControl.Dlg
{
    public partial class CCdControl : UserControl
    {
        //public event delLogSender eLogSender;       //외부에서 호출할때 사용
        //private ManualPcb manualPcb = new ManualPcb();
        //private ManualLens manualLens = new ManualLens();

        Rectangle[] m_clRectROI = new Rectangle[Globalo.CHART_ROI_COUNT];
        Rectangle[] m_clRectCircle = new Rectangle[4];

        public int[] m_iOffsetX_SFR;
        public int[] m_iOffsetY_SFR;

        public int[] m_iSizeX_SFR;
        public int[] m_iSizeY_SFR;
        public int m_iCurNo_SFR;
        public Rectangle[] m_rcRoiBox;						// 원형 마크 검색 영역

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

            //manualPcb.Visible = false;
            //manualLens.Visible = false;
            //ManualPanel.Controls.Add(manualPcb);
            //ManualPanel.Controls.Add(manualLens);
            //ManualBtnChange(eManualBtn.pcbTab);
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

            //BTN_MANUAL_PCB.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#BBBBBB");
            //BTN_MANUAL_LENS.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#BBBBBB");
            //ManualTitleLabel.Text = "MANUAL";
            //ManualTitleLabel.ForeColor = Color.Khaki;     
            //ManualTitleLabel.BackColor = Color.Maroon;
            //ManualTitleLabel.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Regular);
            //ManualTitleLabel.Width = this.Width;
            //ManualTitleLabel.Height = 45;
            //ManualTitleLabel.Location = new Point(0, 0);



            //ManualPanel.Location = new Point(BTN_MANUAL_PCB.Location.X, BTN_MANUAL_PCB.Location.Y + panelYGap);



        }
        public void OnShowWindow(bool bShow)
        {
            if (bShow)
            {
                //Chart roi on
                SetSfrRoi();
                DrawRectSfr(999);
            }
            else
            {

            }
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

            nCount = Globalo.CHART_ROI_COUNT;

            for (i = 0; i < nCount; i++)
            {
                m_iOffsetX_SFR[i] = Globalo.dataManage.workData.m_clSfrInfo.m_clPtOffset[i].X;
                m_iOffsetY_SFR[i] = Globalo.dataManage.workData.m_clSfrInfo.m_clPtOffset[i].Y;
                m_iSizeX_SFR[i] = Globalo.dataManage.workData.m_clSfrInfo.m_nSizeX[i];
                m_iSizeY_SFR[i] = Globalo.dataManage.workData.m_clSfrInfo.m_nSizeY[i];
            }

            for (i = 0; i < 4; i++)
            {
                m_rcRoiBox[i] = Globalo.dataManage.workData.m_CircleP[i];
            }
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
    }
}
