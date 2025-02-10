using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UbiGEM.Net.Structure;
using System.Diagnostics;

namespace ApsMotionControl.Dlg
{
    public partial class MainControl : UserControl
    {
        //public event delLogSender eLogSender;       //외부에서 호출할때 사용
        //private ManualPcb manualPcb = new ManualPcb();
        //private ManualLens manualLens = new ManualLens();

        private const int ModelGridRowViewCount = 10;
        private const int RecipeGridRowViewCount = 5;
        private int[] GridColWidth = { 40, 160, 210, 70, 270, 50, 50, 1 };
        private int RecipeGridWidth = 0;
        private int GridRowHeight = 30;
        private int GridHeaderHeight = 30;
        private int GridInitWidth = 0;
        private int SelectedCellRow = 0;
        private int SelectedCellCol = 0;


        private int SelectedRecipeRow = 0;
        private int currentRecipeNo = 0;


        private enum eManualBtn : int
        {
            pcbTab = 0, lensTab
        };
        public MainControl(int _w, int _h)
        {
            InitializeComponent();
            
            this.Paint += new PaintEventHandler(Form_Paint);
            
            this.Width = _w;
            this.Height = _h;

            setInterface();
            InitModelGrid();
            InitRecipeListGrid();

        }
        public void RefreshMain()
        {
            ShowModelGrid();
            ShowOpid();

            ShowRecipeList();
        }
        
        private void MainControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                RefreshMain();
            }
        }
        public void setControlState(int communicationState , int controlState)
        {
            string stateStr = "";
            BTN_MAIN_OFFLINE_REQ.BackColor = ColorTranslator.FromHtml("#C3A279"); //C3A279
            BTN_MAIN_ONLINE_REMOTE_REQ.BackColor = ColorTranslator.FromHtml("#C3A279"); //C3A279
            if(communicationState == (int)CommunicationState.Communicating)
            {
                switch (controlState)
                {
                    case 1:
                        stateStr = "Connected / EquipmentOffline";
                        BTN_MAIN_OFFLINE_REQ.BackColor = ColorTranslator.FromHtml("#FFB230");
                        break;
                    case 2:
                        stateStr = "Connected / AttemptOnline";
                        BTN_MAIN_OFFLINE_REQ.BackColor = ColorTranslator.FromHtml("#FFB230");
                        break;
                    case 3:
                        stateStr = "Disconnected / AttemptOnline";
                        BTN_MAIN_OFFLINE_REQ.BackColor = ColorTranslator.FromHtml("#FFB230");
                        break;
                    case 4:
                        stateStr = "Connected / OnlineLocal";
                        BTN_MAIN_ONLINE_REMOTE_REQ.BackColor = ColorTranslator.FromHtml("#FFB230");
                        break;
                    case 5:
                        stateStr = "Connected / OnlineRemote";
                        BTN_MAIN_ONLINE_REMOTE_REQ.BackColor = ColorTranslator.FromHtml("#FFB230");
                        break;
                    default:
                        stateStr = "Disconnected / EquipmentOffline";
                        break;
                }
            }
            else
            {
                stateStr = "Disconnected / EquipmentOffline";
            }
            if (textBox_ControlStateVal.InvokeRequired)
            {
                // UI 스레드가 아니므로 Invoke를 사용
                textBox_ControlStateVal.Invoke(new Action(() => textBox_ControlStateVal.Text = stateStr));
            }
            else
            {
                // 이미 UI 스레드이므로 바로 업데이트
                textBox_ControlStateVal.Text = stateStr;
            }
            /*
             public enum ControlState
    {
        EquipmentOffline = 1,
        AttemptOnline = 2,
        HostOffline = 3,
        OnlineLocal = 4,
        OnlineRemote = 5
    }
             */

            /*
             public enum CommunicationState
    {
        Disabled = 1,
        Enabled = 2,
        NotCommunication = 3,
        WaitCRFromHost = 4,
        WaitDelay = 5,
        WaitCRA = 6,
        Communicating = 7
    }
             */

        }


        public void ShowRecipeList()
        {
            int i = 0;

            dataGridView_Recipe.Rows.Clear();

            int recipeCount = Globalo.yamlManager.recipeYamlFiles.Count();
            int gridViewCount = recipeCount;
            if (gridViewCount < RecipeGridRowViewCount)
            {
                gridViewCount = RecipeGridRowViewCount;
            }
            for (i = 0; i < gridViewCount; i++)
            {
                
                if (i < recipeCount)
                {
                    dataGridView_Recipe.Rows.Add((i + 1).ToString(), Globalo.yamlManager.recipeYamlFiles[i]);


                    if (Globalo.dataManage.mesData.m_sMesPPID == Globalo.yamlManager.recipeYamlFiles[i])
                    {
                        SelectedRecipeRow = i;
                        dataGridView_Recipe.Rows[i].Cells[1].Style.BackColor = Color.YellowGreen; // 1번 열
                        dataGridView_Recipe.Rows[i].Cells[1].Style.ForeColor = Color.Black; // 1번 열
                        dataGridView_Recipe.Rows[i].Cells[1].Style.Font = new Font(dataGridView_Recipe.DefaultCellStyle.Font, FontStyle.Bold);
                    }
                    else
                    {
                        dataGridView_Recipe.Rows[i].Cells[1].Style.BackColor = Color.White; // 1번 열
                        dataGridView_Recipe.Rows[i].Cells[1].Style.ForeColor = Color.Black; // 1번 열
                        dataGridView_Recipe.Rows[i].Cells[1].Style.Font = new Font(dataGridView_Recipe.DefaultCellStyle.Font, FontStyle.Regular);
                    }
                    
                }
                else
                {
                    dataGridView_Recipe.Rows.Add("", ""); // 행 추가
                    dataGridView_Recipe.Rows[i].Cells[1].Style.BackColor = Color.White; // 1번 열
                    dataGridView_Recipe.Rows[i].Cells[1].Style.ForeColor = Color.Black; // 1번 열
                    dataGridView_Recipe.Rows[i].Cells[1].Style.Font = new Font(dataGridView_Recipe.DefaultCellStyle.Font, FontStyle.Regular);
                }
            }

            currentRecipeNo = SelectedRecipeRow;
            if (gridViewCount > RecipeGridRowViewCount)
            {
                dataGridView_Recipe.Width = RecipeGridWidth + 20; //스크롤 추가시 grid Width 조정
            }
            dataGridView_Recipe.ClearSelection();

        }
        private int GetRecipeList()
        {
            int nRtn = 0; //0 = 은 변경이 없는 경우
            int i = 0;
            //string selectedItem = comboBox_RecipeList.SelectedItem.ToString();

            string selectedItem = dataGridView_Recipe.Rows[currentRecipeNo].Cells[1].Value.ToString();

            Globalo.dataManage.mesData.m_sMesPPID = selectedItem;

            if(Globalo.yamlManager.vPPRecipeSpecEquip != null)
            {
                Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.Ppid = selectedItem;
            }
            Globalo.dataManage.mesData.m_sMesRecipeRevision = Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.Version;


            int nGridRow = dataGridView_Recipe.RowCount;
            int getCount = Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap.Count();

            string sName = "";
            string sValue = "";
            bool bCheck = false;


            for (i = 0; i < getCount; i++)
            {
                //strData = CardGrid.Rows[i].Cells[j].Value.ToString();
                sName = dataGridView_Recipe.Rows[i].Cells[1].Value.ToString();
                sValue = dataGridView_Recipe.Rows[i].Cells[2].Value.ToString();

                bCheck = Convert.ToBoolean(dataGridView_Recipe.Rows[i].Cells[0].Value);
                if (Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap.TryGetValue(sName, out var value))
                {
                    if (bCheck != Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap[sName].use)
                    {
                        nRtn = (int)Ubisam.ePP_CHANGE_STATE.eUploadListChanged;
                    }

                    Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap[sName].use = bCheck;
                    if (bCheck)
                    {
                        if (sValue != Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap[sName].value)
                        {
                            nRtn = (int)Ubisam.ePP_CHANGE_STATE.eEdited;
                        }
                    }
                    Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap[sName].value = sValue;
                }
                

            }
            return nRtn;
        }
        
        public void ShowOpid()
        {
            textBox_OperatorId.Text = Globalo.yamlManager.MesData.SecGemData.OperatorId;
        }
        public void ShowModelGrid()
        {
            int i = 0;  //옆

            int nCol = dataGridView_Model.ColumnCount;         //7 옆으로 행
            int nRow = dataGridView_Model.RowCount;        //0 아래로 열 빈칸 -1

            int dataCount = Globalo.yamlManager.MesData.SecGemData.Modellist.Count();

            dataGridView_Model.Rows.Clear();
            //dataGridView_Model.AutoGenerateColumns = true;
            //private List<CardItem> GridCardata = new List<CardItem>();
            //if (dataGridView_Model.RowCount < GridCardata.Count)
            //{
            //    int nAddCount = GridCardata.Count - dataGridView_Model.RowCount;
            //    int startIndex = dataGridView_Model.RowCount;
            //    for (i = 0; i < nAddCount; i++)
            //    {
            //        dataGridView_Model.Rows.Add("1", "model1");
            //    }
            //}

            int gridViewCount = dataCount;
            if (gridViewCount < ModelGridRowViewCount)
            {
                gridViewCount = ModelGridRowViewCount;
            }
            for (i = 0; i < gridViewCount; i++)
            {
                if (i < dataCount)
                {
                    dataGridView_Model.Rows.Add((i + 1).ToString(), Globalo.yamlManager.MesData.SecGemData.Modellist[i]);

                    if(i == Globalo.yamlManager.MesData.SecGemData.ModelNo)
                    {
                        SelectedCellRow = i;

                        dataGridView_Model.Rows[i].Cells[1].Style.BackColor = Color.YellowGreen; // 1번 열
                        dataGridView_Model.Rows[i].Cells[1].Style.ForeColor = Color.Black; // 1번 열
                        dataGridView_Model.Rows[i].Cells[1].Style.Font = new Font(dataGridView_Model.DefaultCellStyle.Font, FontStyle.Bold);
                    }
                    else
                    {
                        //dataGridView_Model.Rows[i].DefaultCellStyle.BackColor = Color.White; // 배경색
                        //dataGridView_Model.Rows[i].DefaultCellStyle.ForeColor = Color.Black; // 텍스트 색상
                        //dataGridView_Model.Rows[i].DefaultCellStyle.Font = new Font(dataGridView_Model.DefaultCellStyle.Font, FontStyle.Regular);

                        dataGridView_Model.Rows[i].Cells[1].Style.BackColor = Color.White; // 1번 열
                        dataGridView_Model.Rows[i].Cells[1].Style.ForeColor = Color.Black; // 1번 열
                        dataGridView_Model.Rows[i].Cells[1].Style.Font = new Font(dataGridView_Model.DefaultCellStyle.Font, FontStyle.Regular);
                    }
                }
                else
                {
                    dataGridView_Model.Rows.Add("", "");
                    dataGridView_Model.Rows[i].Cells[1].Style.BackColor = Color.White; // 1번 열
                    dataGridView_Model.Rows[i].Cells[1].Style.ForeColor = Color.Black; // 1번 열
                    dataGridView_Model.Rows[i].Cells[1].Style.Font = new Font(dataGridView_Model.DefaultCellStyle.Font, FontStyle.Regular);
                }

            }

            //dataGridView_Model.Columns[0].ReadOnly = true; // 읽기 전용
            //dataGridView_Model.Columns[0].DefaultCellStyle.BackColor = Color.LightGray; // 배경색 설정
            //dataGridView_Model.Columns[0].DefaultCellStyle.Font = new Font("나눔고딕", 10F, FontStyle.Bold); // 굵은 글씨
            //dataGridView_Model.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 가운데 정렬
            //dataGridView_Model.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Yellow;
            //dataGridView_Model.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow;

            //dataGridView_Model.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Yellow;
            //dataGridView_Model.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow;

            dataGridView_Model.ClearSelection();
            
            // 선택된 행 배경색을 기본 색으로 설정
            //dataGridView_Model.DefaultCellStyle.SelectionBackColor = dataGridView_Model.DefaultCellStyle.BackColor;
           // dataGridView_Model.DefaultCellStyle.SelectionForeColor = dataGridView_Model.DefaultCellStyle.ForeColor;

            // RowHeader의 선택 색상도 기본으로 설정
           // dataGridView_Model.RowHeadersDefaultCellStyle.SelectionBackColor = dataGridView_Model.DefaultCellStyle.BackColor;
           // dataGridView_Model.RowHeadersDefaultCellStyle.SelectionForeColor = dataGridView_Model.DefaultCellStyle.ForeColor;

        }
        private void InitRecipeListGrid()
        {
            //BankGrid
            int i = 0;
            int j = 0;
            // 열 추가
            // 행 헤더 숨기기
            dataGridView_Recipe.RowHeadersVisible = false;
            //사이즈 조절 막기
            dataGridView_Recipe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // 행 높이 자동 조정
            dataGridView_Recipe.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; // 모든 셀에 맞게 자동 조정

            // 열 자동 크기 조정
            dataGridView_Recipe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // 또는
            // 셀 내용 줄바꿈
            dataGridView_Recipe.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // 헤더 폰트 설정
            dataGridView_Recipe.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 10F, FontStyle.Bold);

            // 헤더 배경색 설정
            dataGridView_Recipe.ColumnHeadersDefaultCellStyle.BackColor = Color.GhostWhite;// LightGray;// Color.FromArgb(94, 129, 244); //Color.LightBlue;
            // 헤더 폰트 색
            dataGridView_Recipe.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;// .Gray;

            //dataGridView_Recipe.RowsDefaultCellStyle.BackColor = Color.White;
            //dataGridView_Recipe.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridView_Recipe.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            dataGridView_Recipe.RowsDefaultCellStyle.ForeColor = Color.Gray;

            // Set the selection background color for all the cells.
            //dataGridView_Recipe.DefaultCellStyle.SelectionBackColor = Color.White;
            // dataGridView_Recipe.DefaultCellStyle.SelectionForeColor = Color.Black;
            // dataGridView_Recipe.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Empty;
            // dataGridView_Recipe.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

            //dataGridView_Recipe.DefaultCellStyle.SelectionForeColor = Color.Empty;

            //dataGridView_Recipe.DefaultCellStyle.SelectionBackColor = Color.Empty;
            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            // dataGridView_Recipe.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            // dataGridView_Recipe.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Empty;

            dataGridView_Recipe.EnableHeadersVisualStyles = false;
            // 열 헤더 가운데 정렬
            dataGridView_Recipe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //운영 체제의 기본 시각적 스타일을 무시
            dataGridView_Recipe.AllowUserToResizeRows = false;

            dataGridView_Recipe.ReadOnly = true;
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            cellStyle.Font = new Font("나눔고딕", 10, FontStyle.Regular); // Change font and size
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center align text
            cellStyle.SelectionBackColor = Color.LightBlue;
            //cellStyle.SelectionForeColor = Color.Empty;

            dataGridView_Recipe.DefaultCellStyle = cellStyle;

            //DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn[] textColumns = new DataGridViewTextBoxColumn[2];

            for (i = 0; i < 2; i++)
            {
                textColumns[i] = new DataGridViewTextBoxColumn();
            }

            //DataGridView
            textColumns[0].HeaderText = "No";
            textColumns[1].HeaderText = "Name";

            //textColumns[0].Name = "No";
            //textColumns[1].Name = "Model";




            dataGridView_Recipe.Columns.Add(textColumns[0]);
            dataGridView_Recipe.Columns.Add(textColumns[1]);

            for (i = 0; i < dataGridView_Recipe.ColumnCount; i++)
            {
                dataGridView_Recipe.Columns[i].Resizable = DataGridViewTriState.False;
            }
            int gridWidth = dataGridView_Recipe.Width;
            dataGridView_Recipe.Columns[0].Width = GridColWidth[0];
            dataGridView_Recipe.Columns[1].Width = gridWidth - GridColWidth[0];




            // 행 높이 조정
            dataGridView_Recipe.RowTemplate.Height = GridRowHeight; // 자동 추가되는 행 높이 설정
            dataGridView_Recipe.ColumnHeadersHeight = GridHeaderHeight;
            //dataGridView_Recipe.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;

            for (i = 0; i < RecipeGridRowViewCount; i++)
            {

                string text = $"예시 텍스트 {i}"; // 예시 텍스트 생성
                //bool isChecked = (i % 2 == 0); // 짝수인 경우 체크박스가 체크됨
                dataGridView_Recipe.Rows.Add(true, "", ""); // 행 추가
                dataGridView_Recipe.Rows[i].Height = GridRowHeight;

                for (j = 0; j < dataGridView_Recipe.ColumnCount; j++)
                {
                    //dataGridView.Columns[i].Resizable = DataGridViewTriState.False;
                    //dataGridView_Recipe.Columns[j].Width = GridColWidth[j];
                    dataGridView_Recipe.Columns[j].Resizable = DataGridViewTriState.False;

                }
            }

            dataGridView_Recipe.Height = RecipeGridRowViewCount * GridRowHeight + GridRowHeight + 2;
            if (dataGridView_Recipe.AllowUserToAddRows == true)
            {
                //dataGridView_Model.Rows[GridRowCount].Height = GridRowHeight;
            }


            dataGridView_Recipe.MultiSelect = false; // 여러 개 선택 불가능
            dataGridView_Recipe.AllowUserToAddRows = false; // 빈 행 추가 방지
            dataGridView_Recipe.ScrollBars = ScrollBars.Vertical;      //가로 스크롤 안보이게 설정

            dataGridView_Recipe.CellContentClick += new DataGridViewCellEventHandler(RecipeGridView_CellContentClick);     //삭제 버튼 클릭시 사용
            // 버튼 클릭 이벤트 등록
            dataGridView_Recipe.CellClick += new DataGridViewCellEventHandler(RecipeGridView_CellClick); //textbox 한번 클릭으로 바로 수정되게 추가
            dataGridView_Recipe.SelectionChanged += dataGridView_Recipe_SelectionChanged;
            // 이벤트 핸들러 추가
            //CardGrid.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(CardGrid_EditingControlShowing);
            //dataGridView_Recipe.CellFormatting += dataGridView_Model_CellFormatting;

            RecipeGridWidth = dataGridView_Recipe.Width;     //<---스크롤 생겼을때 사이즈 조절위해 초기 Grid 넓이 저장

            // 각 컬럼의 헤더 텍스트 정렬 설정
            foreach (DataGridViewColumn column in dataGridView_Recipe.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //dataGridView_Model.Columns[0].ReadOnly = true; // 읽기 전용
            dataGridView_Recipe.Columns[0].DefaultCellStyle.BackColor = Color.LightGray; // 배경색 설정
            dataGridView_Recipe.Columns[0].DefaultCellStyle.ForeColor = Color.Yellow; // 배경색 설정
            dataGridView_Recipe.Columns[0].DefaultCellStyle.Font = new Font("나눔고딕", 10F, FontStyle.Bold); // 굵은 글씨
            dataGridView_Recipe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Recipe.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            dataGridView_Recipe.ClearSelection();
        }
        private void InitModelGrid()
        {
            //BankGrid
            int i = 0;
            int j = 0;
            // 열 추가
            // 행 헤더 숨기기
            dataGridView_Model.RowHeadersVisible = false;
            //사이즈 조절 막기
            dataGridView_Model.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // 행 높이 자동 조정
            dataGridView_Model.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; // 모든 셀에 맞게 자동 조정

            // 열 자동 크기 조정
            dataGridView_Model.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // 또는
            // 셀 내용 줄바꿈
            dataGridView_Model.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // 헤더 폰트 설정
            dataGridView_Model.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 10F, FontStyle.Bold);

            // 헤더 배경색 설정
            dataGridView_Model.ColumnHeadersDefaultCellStyle.BackColor = Color.GhostWhite;// Color.FromArgb(94, 129, 244); //Color.LightBlue;
            // 헤더 폰트 색
            dataGridView_Model.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dataGridView_Model.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            dataGridView_Model.RowsDefaultCellStyle.ForeColor = Color.Gray;

            // Set the selection background color for all the cells.
            //dataGridView_Model.DefaultCellStyle.SelectionBackColor = Color.White;
            // dataGridView_Model.DefaultCellStyle.SelectionForeColor = Color.Black;
            // dataGridView_Model.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Empty;
            // dataGridView_Model.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

            //dataGridView_Model.DefaultCellStyle.SelectionForeColor = Color.Empty;

            //dataGridView_Model.DefaultCellStyle.SelectionBackColor = Color.Empty;
            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            // dataGridView_Model.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            // dataGridView_Model.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Empty;

            dataGridView_Model.EnableHeadersVisualStyles = false;
            // 열 헤더 가운데 정렬
            dataGridView_Model.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //운영 체제의 기본 시각적 스타일을 무시
            dataGridView_Model.AllowUserToResizeRows = false;

            dataGridView_Model.ReadOnly = true;
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            cellStyle.Font = new Font("나눔고딕", 10, FontStyle.Regular); // Change font and size
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center align text
            cellStyle.SelectionBackColor = Color.LightBlue;
            //cellStyle.SelectionForeColor = Color.Empty;

            dataGridView_Model.DefaultCellStyle = cellStyle;

            DataGridViewTextBoxColumn[] textColumns = new DataGridViewTextBoxColumn[2];

            for (i = 0; i < 2; i++)
            {
                textColumns[i] = new DataGridViewTextBoxColumn();
            }

            //DataGridView

            textColumns[0].HeaderText = "No";
            textColumns[1].HeaderText = "Model";

            textColumns[0].Name = "No";
            textColumns[1].Name = "Model";


            

            dataGridView_Model.Columns.Add(textColumns[0]);
            dataGridView_Model.Columns.Add(textColumns[1]);

            for (i = 0; i < dataGridView_Model.ColumnCount; i++)
            {
                dataGridView_Model.Columns[i].Resizable = DataGridViewTriState.False;
            }
            int gridWidth = dataGridView_Model.Width;
            dataGridView_Model.Columns[0].Width = GridColWidth[0];
            dataGridView_Model.Columns[1].Width = gridWidth - GridColWidth[0];



            // 행 높이 조정
            dataGridView_Model.RowTemplate.Height = GridRowHeight; // 자동 추가되는 행 높이 설정
            dataGridView_Model.ColumnHeadersHeight = GridHeaderHeight;
            //dataGridView_Model.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;

            for (i = 0; i < ModelGridRowViewCount; i++)
            {

                string text = $"예시 텍스트 {i}"; // 예시 텍스트 생성
                //bool isChecked = (i % 2 == 0); // 짝수인 경우 체크박스가 체크됨
                dataGridView_Model.Rows.Add(""); // 행 추가
                dataGridView_Model.Rows[i].Height = GridRowHeight;

                for (j = 0; j < dataGridView_Model.ColumnCount; j++)
                {
                    //dataGridView.Columns[i].Resizable = DataGridViewTriState.False;
                    //dataGridView_Model.Columns[j].Width = GridColWidth[j];
                    dataGridView_Model.Columns[j].Resizable = DataGridViewTriState.False;
                    dataGridView_Model.Rows[i].Cells[j].Value = "";
                }
            }
            dataGridView_Model.Height = ModelGridRowViewCount * GridRowHeight + GridRowHeight + 2;
            if (dataGridView_Model.AllowUserToAddRows == true)
            {
                //dataGridView_Model.Rows[GridRowCount].Height = GridRowHeight;
            }


            dataGridView_Model.MultiSelect = false; // 여러 개 선택 불가능
            dataGridView_Model.AllowUserToAddRows = false; // 빈 행 추가 방지
            dataGridView_Model.ScrollBars = ScrollBars.Vertical;      //가로 스크롤 안보이게 설정

            dataGridView_Model.CellContentClick += ModelGridView_CellContentClick;     //삭제 버튼 클릭시 사용
            // 버튼 클릭 이벤트 등록
            dataGridView_Model.CellClick += new DataGridViewCellEventHandler(ModelGridView_CellClick); //textbox 한번 클릭으로 바로 수정되게 추가
            dataGridView_Model.SelectionChanged += dataGridView1_SelectionChanged;
            // 이벤트 핸들러 추가
            //CardGrid.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(CardGrid_EditingControlShowing);
            //dataGridView_Model.CellFormatting += dataGridView_Model_CellFormatting;

            GridInitWidth = dataGridView_Model.Width;     //<---스크롤 생겼을때 사이즈 조절위해 초기 Grid 넓이 저장

            // 각 컬럼의 헤더 텍스트 정렬 설정
            foreach (DataGridViewColumn column in dataGridView_Model.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //dataGridView_Model.Columns[0].ReadOnly = true; // 읽기 전용


            dataGridView_Model.Columns[0].DefaultCellStyle.BackColor = Color.LightGray; // 배경색 설정
            dataGridView_Model.Columns[0].DefaultCellStyle.ForeColor = Color.Yellow; // 배경색 설정
            dataGridView_Model.Columns[0].DefaultCellStyle.Font = new Font("나눔고딕", 10F, FontStyle.Bold); // 굵은 글씨
            dataGridView_Model.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 가운데 정렬

            dataGridView_Model.ClearSelection();
        }
        private void RecipeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRecipeRow = e.RowIndex;           //세로

            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 0)//dataGridView.Columns["Selected"].Index)
            {
                var cell = dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                if (cell != null)
                {
                    cell.Value = !(bool)cell.Value; // 체크 상태를 반전
                }
            }
            if (e.ColumnIndex == 2) //value
            {
                string sValue = dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                NumPadForm popupForm = new NumPadForm(sValue);
                
                DialogResult dialogResult = popupForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    double dNumData = popupForm.Result;
                    dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dNumData.ToString();
                    //LABEL_TEACH_MOVE_VALUE.Text = dNumData.ToString("0.0##");
                }
            }
        }
        private void RecipeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            //Grid 안에 값을 클릭해야 들어옴

            //int rowCount = dataGridView_Recipe.RowCount - 1;
            //if (e.ColumnIndex == 0)//dataGridView.Columns["Selected"].Index)
            //{
            //    var cell = dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
            //    if (cell != null)
            //    {
            //        cell.Value = !(bool)cell.Value; // 체크 상태를 반전
            //    }
            //}
            //if (e.ColumnIndex == 2) //value
            //{
            //    string sValue = dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //    sValue = "";
            //}

        }

        private void HeaderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = sender as CheckBox;

            // 모든 행의 체크박스 상태 변경
            foreach (DataGridViewRow row in dataGridView_Recipe.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell;
                checkBoxCell.Value = headerCheckBox.Checked;
                dataGridView_Recipe.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            // DataGridView 업데이트
            dataGridView_Recipe.RefreshEdit();
        }
        private void dataGridView_Model_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 셀 선택 시 색상이 변경되지 않도록 설정
            //if (dataGridView_Model.IsCellSelected(e.RowIndex, e.ColumnIndex))
            if (dataGridView_Model.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
            {
                //e.CellStyle.BackColor = dataGridView_Model.DefaultCellStyle.BackColor;
               // e.CellStyle.ForeColor = dataGridView_Model.DefaultCellStyle.ForeColor;
            }
        }
        private void dataGridView_Recipe_SelectionChanged(object sender, EventArgs e)
        {
            // 현재 선택된 행의 인덱스 가져오기
            if (dataGridView_Recipe.SelectedCells.Count > 0)
            {
                // 첫 번째 선택된 셀을 가져오기
                DataGridViewCell selectedCell = dataGridView_Recipe.SelectedCells[0];

                // 행(Row) 인덱스
                int selectedRowIndex = selectedCell.RowIndex;

                // 열(Column) 인덱스
                int selectedColumnIndex = selectedCell.ColumnIndex;
                if (selectedColumnIndex == 0 || selectedRowIndex == currentRecipeNo)//Globalo.yamlManager.MesData.SecGemData.ModelNo)
                {
                    dataGridView_Recipe.ClearSelection();
                }

            }
        }
        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // 현재 선택된 행의 인덱스 가져오기
            if (dataGridView_Model.SelectedCells.Count > 0)
            {
                // 첫 번째 선택된 셀을 가져오기
                DataGridViewCell selectedCell = dataGridView_Model.SelectedCells[0];

                // 행(Row) 인덱스
                int selectedRowIndex = selectedCell.RowIndex;

                // 열(Column) 인덱스
                int selectedColumnIndex = selectedCell.ColumnIndex;
                if(selectedColumnIndex == 0 || selectedRowIndex == Globalo.yamlManager.MesData.SecGemData.ModelNo)
                {
                    dataGridView_Model.ClearSelection();
                }

            }
        }
        
        private void ModelGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {

            }
            //if (e.ColumnIndex == 6)// && e.RowIndex < BankGrid.RowCount - 1) // 버튼 열 인덱스가 2인 경우
            //{
            //    DialogResult result = MessageBox.Show($"{ dataGridView_Model.Rows[e.RowIndex].Cells[1].Value.ToString()}[{e.RowIndex + 1}] 카드정보 삭제 하시겠습니까?", "Yes", MessageBoxButtons.YesNo);
            //    if (result == DialogResult.Yes)
            //    {
            //        //마지막 하나 남았을 때는 삭제하고 초기화 해야된다.
            //        //e.RowIndex 로 지우면안되고 id로 검색해서 죽여야된다.
            //        string GridId = dataGridView_Model.Rows[e.RowIndex].Cells[7].Value.ToString();

                //        for (int i = 0; i < dataGridView_Model.RowCount; i++)
                //        {
                //            if (GridId == LiteDbManager.cardData.CardAllData[i].Id.ToString())
                //            {
                //                Console.WriteLine($"Card Remove ID[{i}]  = {GridId}");

                //                LiteDbManager.cardData.Remove(i);
                //                break;
                //            }
                //        }
                //        dataGridView_Model.Rows.Remove(dataGridView_Model.Rows[e.RowIndex]);
                //        GridCardata.RemoveAt(e.RowIndex);
                //        if (dataGridView_Model.RowCount == 0)
                //        {
                //            GridCardata.Add(LiteDbManager.cardData.CardAllData[0].DeepCopy());
                //            dataGridView_Model.Rows.Add(
                //                CardData.CreditCardCompany[0],
                //                "",
                //                "선택없음",
                //                "1일",
                //                RtnPeriod(0),
                //                false,
                //                "x",
                //                ObjectId.NewObjectId());

                //            Console.WriteLine($"Card Gird Row Count: {dataGridView_Model.RowCount}");
                //        }

                //        if ((GridRowHeight * dataGridView_Model.RowCount + GridHeaderHeight) < dataGridView_Model.Height)
                //        {
                //            dataGridView_Model.Width = GridInitWidth;
                //        }

                //    }
                //}
        }
        private void ModelGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Console.WriteLine($"DataGridView_CellClick");
            SelectedCellCol = e.ColumnIndex;
            SelectedCellRow = e.RowIndex;           //세로
            if (e.RowIndex < 0) return;


            //if (e.ColumnIndex == 0) // 1번 열 클릭 시
            //{
            //    dataGridView_Model.ClearSelection(); // 선택 해제
            //}
            //foreach (DataGridViewRow selectedRow in dataGridView_Model.SelectedRows)
            //{
            //    int rowIndex = selectedRow.Index; // 선택된 행의 인덱스
            //    Console.WriteLine($"선택된 행의 인덱스: {rowIndex}");
            //}

            //dataGridView_Model.Rows[SelectedCellRow].Cells[0].Style.BackColor = Color.LightBlue;
            //dataGridView_Model.Rows[SelectedCellRow].Cells[1].Style.BackColor = Color.LightBlue;
            int nRow = dataGridView_Model.RowCount;
            int dataCount = Globalo.yamlManager.MesData.SecGemData.Modellist.Count();
            if (e.ColumnIndex == 1 && SelectedCellRow < dataCount)
            {
                // 셀이 클릭되었을 때 편집 모드로 전환
                dataGridView_Model.CurrentCell = dataGridView_Model.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridView_Model.BeginEdit(true);
            }
            //콤보 박스 클릭시 이벤트 가장 먼저 발생
            //if (e.ColumnIndex == 0 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
            //{
            //    //통장 data 확인후 목록 항상 갱신
            //    dataGridView_Model.BeginEdit(true);
            //    var editingControl = this.dataGridView_Model.EditingControl as DataGridViewComboBoxEditingControl;
            //    //if (editingControl != null)
            //    if (dataGridView_Model.EditingControl is ComboBox comboBox)
            //    {
            //        editingControl.DroppedDown = true;
            //        //comboBox.DropDownClosed -= ComboBox_DropDownClosed;
            //        //comboBox.DropDownClosed += ComboBox_DropDownClosed;

            //    }
            //}
            //if (e.ColumnIndex == 4)     //이용기간
            //{

            //    int posX = 0;
            //    var cellBounds = dataGridView_Model.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            //    posX = cellBounds.X - cardPopup.Width + cellBounds.Width + 1;// + 4;
            //    var buttonLocation = new System.Drawing.Point(posX, dataGridView_Model.Location.Y + cellBounds.Y + GridRowHeight + 0);
            //    cardPopup.Location = buttonLocation;
            //    cardPopup.BringToFront();
            //    cardPopup.onShow(true, GridCardata[SelectedCellRow].UsagePeriod);

            //}
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
        }
        public void setInterface()
        {

            ManualTitleLabel.ForeColor = ColorTranslator.FromHtml("#6F6F6F");


            BTN_MANUAL_PCB.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#BBBBBB");
            BTN_MANUAL_LENS.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#BBBBBB");

            BTN_MAIN_OFFLINE_REQ.BackColor = ColorTranslator.FromHtml("#FFB230");
            BTN_MAIN_ONLINE_REMOTE_REQ.BackColor = ColorTranslator.FromHtml("#FFB230"); //C3A279


            //ManualTitleLabel.Text = "MANUAL";
            //ManualTitleLabel.ForeColor = Color.Khaki;     
            //ManualTitleLabel.BackColor = Color.Maroon;
            //ManualTitleLabel.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Regular);
            //ManualTitleLabel.Width = this.Width;
            //ManualTitleLabel.Height = 45;
            //ManualTitleLabel.Location = new Point(0, 0);



            //ManualPanel.Location = new Point(BTN_MANUAL_PCB.Location.X, BTN_MANUAL_PCB.Location.Y + panelYGap);



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

        private void BTN_MAIN_MODEL_ADD_Click(object sender, EventArgs e)
        {
            //dataGridView_Model.Rows.Add("1", "model1");
            //System.Diagnostics.Process.Start("osk.exe");

            KeyBoardForm keyBoardForm = new KeyBoardForm();

            // 모달로 폼을 띄우고, 사용자가 OK를 클릭했을 때 KeyValue 값을 받음
            if (keyBoardForm.ShowDialog() == DialogResult.OK)
            {
                // KeyBoardForm에서 선택된 키 값을 받아옴
                string selectedKey = keyBoardForm.KeyValue;
                int addCount = Globalo.yamlManager.MesData.SecGemData.Modellist.Count();
                Globalo.yamlManager.MesData.SecGemData.Modellist.Add(selectedKey);

                Globalo.yamlManager.MesSave();

                RefreshMain();
                //MessageBox.Show("선택된 키: " + selectedKey);
            }



            
        }

        private void BTN_MAIN_MODEL_DEL_Click(object sender, EventArgs e)
        {
            if(SelectedCellRow < 0)
            {
                return;
            }
            if(SelectedCellRow < Globalo.yamlManager.MesData.SecGemData.Modellist.Count())
            {
                Globalo.yamlManager.MesData.SecGemData.Modellist.RemoveAt(SelectedCellRow);

                if (Globalo.yamlManager.MesData.SecGemData.ModelNo >= SelectedCellRow)
                {
                    string strData = dataGridView_Model.Rows[SelectedCellRow].Cells[1].Value.ToString();
                    Globalo.yamlManager.MesData.SecGemData.ModelNo--;
                }
                

                Globalo.yamlManager.MesSave();

                RefreshMain();
            }
        }

        private void BTN_MAIN_MODEL_LOAD_Click(object sender, EventArgs e)
        {
            if (SelectedCellRow < 0)
            {
                return;
            }
            if (SelectedCellRow < Globalo.yamlManager.MesData.SecGemData.Modellist.Count())
            {
                string strData = dataGridView_Model.Rows[SelectedCellRow].Cells[1].Value.ToString();
                Globalo.yamlManager.MesData.SecGemData.ModelNo = SelectedCellRow;
                Globalo.yamlManager.MesSave();
            }

            Globalo.yamlManager.MesLoad();

            RefreshMain();
        }

        private void BTN_MAIN_MODEL_CHANGE_Click(object sender, EventArgs e)
        {
            if (SelectedCellRow < 0)
            {
                return;
            }
            if (SelectedCellRow < Globalo.yamlManager.MesData.SecGemData.Modellist.Count())
            {
                string strData = dataGridView_Model.Rows[SelectedCellRow].Cells[1].Value.ToString();
                KeyBoardForm keyBoardForm = new KeyBoardForm(strData);

                // 모달로 폼을 띄우고, 사용자가 OK를 클릭했을 때 KeyValue 값을 받음
                if (keyBoardForm.ShowDialog() == DialogResult.OK)
                {
                    // KeyBoardForm에서 선택된 키 값을 받아옴
                    string selectedKey = keyBoardForm.KeyValue;
                    if(selectedKey.Length > 0)
                    {
                        Globalo.yamlManager.MesData.SecGemData.Modellist[SelectedCellRow] = selectedKey;
                        Globalo.yamlManager.MesSave();
                        Globalo.yamlManager.MesLoad();
                        RefreshMain();
                    }
                    
                }
            }
                
        }

        private void BTN_MAIN_OPID_SAVE_Click(object sender, EventArgs e)
        {
            if (textBox_OperatorId.Text.Length < 1)
            {
                Globalo.LogPrint("MainControl", "OPERATOR ID 입력해주세요.", Globalo.eMessageName.M_WARNING);
                return;
            }

            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");
            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, "OPERATOR ID 저장하시겠습니까?");

            DialogResult result = messagePopUp3.ShowDialog();
            if (result == DialogResult.Yes)
            {
                Globalo.yamlManager.MesData.SecGemData.OperatorId = textBox_OperatorId.Text;
                Globalo.dataManage.mesData.m_sMesOperatorID = textBox_OperatorId.Text;
                Globalo.yamlManager.MesSave();
            }
            else
            {
                textBox_OperatorId.Text = Globalo.yamlManager.MesData.SecGemData.OperatorId;
            }

        }

        private void BTN_MAIN_OFFLINE_REQ_Click(object sender, EventArgs e)
        {
            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");
            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, "설비 오프라인 전환하시겠습니까?");

            DialogResult result = messagePopUp3.ShowDialog();
            if (result == DialogResult.Yes)
            {
                Globalo.ubisamForm.RequestOfflineFn();
            }
        }

        private void BTN_MAIN_ONLINE_REMOTE_REQ_Click(object sender, EventArgs e)
        {
            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");
            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, "설비 온라인 전환하시겠습니까?");

            DialogResult result = messagePopUp3.ShowDialog();
            if (result == DialogResult.Yes)
            {
                Globalo.ubisamForm.RequestOnlineRemoteFn();
            }
        }

        private void BTN_MAIN_ABORT_LOT_Click(object sender, EventArgs e)
        {
            //g_pCarAABonderDlg->m_clUbiGemDlg.EventReportSendFn(ABORTED_REPORT_10712);
            string strData = textBox_AbortedLot.Text;

            if(strData.Length < 1)
            {
                Globalo.LogPrint("MainControl", "LOT 입력해 주세요.", Globalo.eMessageName.M_WARNING);
                return;
            }

            string logData = $"({strData})LOT ABORT?";

            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");
            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, logData);

            DialogResult result = messagePopUp3.ShowDialog();
            if (result == DialogResult.Yes)
            {
                Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.ABORTED_REPORT_10712, strData);
            }
        }

        private void BTN_MAIN_MATERIAL_ID_REPORT_Click(object sender, EventArgs e)
        {
            //g_pCarAABonderDlg->m_clUbiGemDlg.EventReportSendFn(MATERIAL_ID_REPORT_10713);

            string strData = textBox_MaterialId.Text;

            if (strData.Length < 1)
            {
                Globalo.LogPrint("MainControl", "MATERIAL ID 입력해 주세요.", Globalo.eMessageName.M_WARNING);
                return;
            }

            string logData = $"({strData})MATERIAL ID REPORT?";

            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");
            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, logData);

            DialogResult result = messagePopUp3.ShowDialog();
            if (result == DialogResult.Yes)
            {
                Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.MATERIAL_ID_REPORT_10713);
            }
        }



        private void BTN_MAIN_RECIPE_DEL_Click(object sender, EventArgs e)
        {
            //Delete
            //string selectedItem = comboBox_RecipeList.SelectedItem.ToString();
            string selectedItem = dataGridView_Recipe.Rows[SelectedRecipeRow].Cells[1].Value.ToString();

            if (selectedItem == Globalo.dataManage.mesData.m_sMesPPID)
            {
                Globalo.LogPrint("MainControl", "사용중인 RECIPE ID 입니다.", Globalo.eMessageName.M_WARNING);
                return;
            }


            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");

            string logData = $"({selectedItem}) RECIPE ID 삭제하시겠습니까?";
            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, logData);

            DialogResult result = messagePopUp3.ShowDialog();
            if (result == DialogResult.Yes)
            {
                Globalo.dataManage.mesData.m_sMesRecipeRevision = Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.Version;

                Globalo.dataManage.mesData.m_dPPChangeArr[0] = (int)Ubisam.ePP_CHANGE_STATE.eDeleted;            //3 Deleted
                Globalo.dataManage.mesData.m_dPPChangeArr[1] = (int)Ubisam.ePP_CHANGE_ORDER_TYPE.eOperator;      //1 = Host, 2 = Operator


                Globalo.LogPrint("MainControl", "[Rerpot] Process Program State Changed Report - Deleted.");

                //g_pCarAABonderDlg->m_clUbiGemDlg.EventReportSendFn(PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601, sData);	//----Del button 사용안함 xxxx
                Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601, selectedItem);


                
                Globalo.yamlManager.RecipeYamlFileDel(selectedItem);

                Globalo.yamlManager.RecipeYamlListLoad();

                ShowRecipeList();
            }
        }

        private void BTN_MAIN_RECIPE_CREATE_Click(object sender, EventArgs e)
        {
            //Create
            //string selectedItem = comboBox_RecipeList.SelectedItem.ToString();
            string selectedItem = dataGridView_Recipe.Rows[SelectedRecipeRow].Cells[1].Value.ToString();
            KeyBoardForm keyBoardForm = new KeyBoardForm();

            // 모달로 폼을 띄우고, 사용자가 OK를 클릭했을 때 KeyValue 값을 받음
            if (keyBoardForm.ShowDialog() == DialogResult.OK)
            {
                // KeyBoardForm에서 선택된 키 값을 받아옴
                string selectedKey = keyBoardForm.KeyValue;

                MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");

                string logData = $"({selectedKey}) RECIPE 생성하시겠습니까?";

                messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, logData);

                DialogResult result = messagePopUp3.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    Globalo.yamlManager.RecipeYamlFileCopy(selectedItem, selectedKey);

                    Data.RootRecipe ppRs = Globalo.yamlManager.RecipeLoad(selectedKey);     //Recipe Load

                    if(ppRs == null)
                    {
                        Globalo.LogPrint("MainControl", "[INFO] Recipe Create Fail", Globalo.eMessageName.M_ERROR);
                    }
                    ppRs.RECIPE.Ppid = selectedKey;
                    ppRs.RECIPE.Version = "1";

                    Globalo.yamlManager.RecipeSave(ppRs);       //Recipe Save

                    Globalo.yamlManager.RecipeYamlListLoad();

                    ShowRecipeList();

                    Globalo.dataManage.mesData.m_dPPChangeArr[0] = (int)Ubisam.ePP_CHANGE_STATE.eCreated;            //1 = Created
                    Globalo.dataManage.mesData.m_dPPChangeArr[1] = (int)Ubisam.ePP_CHANGE_ORDER_TYPE.eOperator;      //1 = Host, 2 = Operator

                    Globalo.LogPrint("MainControl", "[Rerpot] Process Program State Changed Report - Created");

                    Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601, ppRs.RECIPE.Ppid);
                }
            }

        }

        private void BTN_MAIN_RECIPE_CHANGE_Click(object sender, EventArgs e)
        {
            //Change
            //string selectedItem = comboBox_RecipeList.SelectedItem.ToString();
            string selectedItem = dataGridView_Recipe.Rows[SelectedRecipeRow].Cells[1].Value.ToString();

            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");

            string logData = $"({selectedItem}) RECIPE ID로 변경 하시겠습니까?";

            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, logData);

            DialogResult result = messagePopUp3.ShowDialog();
            if (result == DialogResult.Yes)
            {
                Globalo.LogPrint("MainControl", "[RECIPE] CURRENT : ({selectedItem})");

                Globalo.yamlManager.vPPRecipeSpecEquip = Globalo.yamlManager.RecipeLoad(selectedItem);
                if(Globalo.yamlManager.vPPRecipeSpecEquip == null)
                {
                    Globalo.LogPrint("MainControl", "({selectedItem}) RECIPE CHANGE FAIL", Globalo.eMessageName.M_ERROR);
                    return;
                }


                currentRecipeNo = SelectedRecipeRow;// comboBox_RecipeList.SelectedIndex;


                Globalo.dataManage.mesData.m_sMesPPID = selectedItem;
                Globalo.dataManage.mesData.m_sRecipeId = selectedItem;
                Globalo.dataManage.mesData.m_sMesRecipeRevision = Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.Version;

                Globalo.yamlManager.MesData.SecGemData.RecipeId = Globalo.dataManage.mesData.m_sMesPPID;

                
                Globalo.yamlManager.MesSave();

                ShowRecipeList();

                
            }
        }

        private void BTN_MAIN_RECIPE_VIEW_Click(object sender, EventArgs e)
        {
            string selectedItem = dataGridView_Recipe.Rows[SelectedRecipeRow].Cells[1].Value.ToString();
            RecipePopup recipePopup = new RecipePopup(selectedItem);
            recipePopup.ShowDialog();
        }
    }
}
