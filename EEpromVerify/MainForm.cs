using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ApsMotionControl
{
    public partial class MainForm : Form
    {
        public const int PG_WIDTH = 1920;
        public const int PG_HEIGHT = 1080;

        public const int RunGuideWidth = 330;//164;
        public const int RunButtonWidth = 130;//164;
        public const int RunButtonHeight = 60;

        public const int CamHeight = 400;
        public const int ProductionHeight = 100;
        public const int LogViewHeight = 300;
        public Button[] BtnArr = new Button[6];
        public Button[] RunBtnArr = new Button[3];
        public int[] RunBtnSizeArr = { 120, 120, 200 };

        private System.Windows.Forms.Timer _timerRunButton;

        private bool ReadyBtnOn = false;
        private bool isTimerRunning = false;  // 타이머 시작 시 실행 중으로 설정
        public MainForm()
        {
            InitializeComponent();
            //this.TopMost = true;
            //BTN_BOTTOM_MANUAL
            //BTN_BOTTOM_TEACH
            BtnArr[0] = BTN_BOTTOM_MAIN;
            BtnArr[1] = BTN_BOTTOM_CCD;
            BtnArr[2] = BTN_BOTTOM_SETUP;
            BtnArr[3] = BTN_BOTTOM_ALARM;
            BtnArr[4] = BTN_BOTTOM_WALLPAPER;
            BtnArr[5] = BTN_BOTTOM_EXIT;
            BTN_BOTTOM_TEACH.Visible = false;
            BTN_BOTTOM_IO.Visible = false;
            BTN_BOTTOM_LIGHT.Visible = false;

            //BtnArr[1] = BTN_BOTTOM_TEACH;
            //BtnArr[3] = BTN_BOTTOM_IO;
            //BtnArr[4] = BTN_BOTTOM_LIGHT;
            //RunBtnArr[0] = BTN_MAIN_ORIGIN1;
            //RunBtnArr[1] = BTN_MAIN_READY1;
            //RunBtnArr[2] = BTN_MAIN_PAUSE1;
            //RunBtnArr[0] = BTN_MAIN_READY1;
            RunBtnArr[0] = BTN_MAIN_PAUSE1;
            RunBtnArr[1] = BTN_MAIN_STOP1;
            RunBtnArr[2] = BTN_MAIN_START1;
            BTN_MAIN_READY1.Visible = false;
            BTN_MAIN_ORIGIN1.Visible = false;
            //this.TopLevel = true;

            Globalo.MainForm = this;
            Globalo.threadControl = new ThreadControl();    //<--log Thread 생성후 로그 출력 가능
            //Globalo.LogPrint("ManualControl", "gggggggggggg");
            //
            int dLeftTopPanelW = LeftPanel.Width;
            int dLeftTopPanelH = CamHeight;

            Globalo.camControl = new Dlg.CamControl(dLeftTopPanelW, dLeftTopPanelH);


            Globalo.dataManage.teachingData.eLogSender += eLogPrint;
            Globalo.motorControl.eLogSender += eLogPrint;
            Globalo.dIoControl.eLogSender += eLogPrint;
            // 다이얼로그
            //
            //mTeachPanel = new Dlg.TeachingControl();
            //mManualPanel = new Dlg.ManualControl();
            // Thread Main
            //

            //Globalo.threadControl.autoRunthread.eLogSender += eLogPrint;
            //Globalo.threadControl.ccdColorThread.eLogSender += eLogPrint;


            Globalo.mLaonGrabberClass.eLogSender += eLogPrint;
            Globalo.threadControl.AllThreadStart();
            ///Data Load
            //

            //string excelPath = string.Format(@"{0}\30.csv", Application.StartupPath); //file path
            //Globalo.dataManage.eepromData.ReadExcelData(excelPath);
            Globalo.dataManage.eepromData.LoadExcelData();


            Globalo.yamlManager.configDataLoad();
            Globalo.yamlManager.imageDataLoad();
            Globalo.dataManage.teachingData.DataLoad();
            Globalo.yamlManager.RecipeYamlListLoad();
            Globalo.yamlManager.UgcLoad();
            Globalo.yamlManager.MesLoad();
            Globalo.yamlManager.AlarmLoad();
            Globalo.yamlManager.vPPRecipeSpecEquip = Globalo.yamlManager.RecipeLoad(Globalo.dataManage.mesData.m_sMesPPID);
            


            string fileName = string.Format(@"{0}\iomap.xlsx", Application.StartupPath); //file path

            //Globalo.dataManage.ioData.ReadExcelData(fileName);

            Globalo.dataManage.ioData.ReadEpplusData(fileName);


            //모터 초기화
            //
            Globalo.motorControl.Motor_Init();
           

            if (ProgramState.ON_LINE_MOTOR)
            {
                Globalo.motorControl.Axl_Init();
                Globalo.dIoControl.DioInit();

            }
            Globalo.mLaonGrabberClass.M_GrabDllLoadComplete = false;
            Globalo.mLaonGrabberClass.M_GrabDllLoadComplete = Globalo.GrabberDll.dllLoad();
            if (Globalo.mLaonGrabberClass.M_GrabDllLoadComplete)
            {
                Globalo.mLaonGrabberClass.SetUnit(0);
                Globalo.mLaonGrabberClass.UiconfigLoad();
                Globalo.mLaonGrabberClass.SelectSensor();
                Globalo.mLaonGrabberClass.AllocImageBuff();
                //
                if (ProgramState.ON_LINE_MIL)
                {
                    Globalo.threadControl.ccdColorThread.Start();
                    Globalo.threadControl.ccdGrabThread.Start();
                }
                    
            }

            if (ProgramState.ON_LINE_MIL)
            {
                InitMilLib();
            }
            this.Size = new System.Drawing.Size(PG_WIDTH, PG_HEIGHT);
            this.Padding = new Padding(0); // 부모 컨트롤의 여백 제거
            this.Location = new System.Drawing.Point(0, 0);

            


            int dRightPanelW = RightPanel.Width;
            int dRightPanelH = RightPanel.Height;

            Globalo.mTeachPanel = new Dlg.TeachingControl(dRightPanelW, dRightPanelH);
            Globalo.mMainPanel = new Dlg.MainControl(dRightPanelW, dRightPanelH);
            Globalo.mCCdPanel = new Dlg.CCdControl(dRightPanelW, dRightPanelH);
            Globalo.mConfigPanel = new Dlg.ConfigControl(dRightPanelW, dRightPanelH);
            Globalo.mAlarmPanel = new Dlg.AlarmControl(dRightPanelW, dRightPanelH);
            Globalo.ubisamForm = new Ubisam.UbisamForm();
            Globalo.ubisamForm.UbisamUgcLoad();
            Globalo.ubisamForm.Visible = false;
            Globalo.mTeachPanel.eLogSender += eLogPrint;
            //Globalo.mManualPanel.eLogSender += eLogPrint;



            Globalo.mTeachPanel.BackColor = ColorTranslator.FromHtml("#F8F3F0");
            Globalo.mMainPanel.BackColor = ColorTranslator.FromHtml("#F8F3F0");
            Globalo.mCCdPanel.BackColor = ColorTranslator.FromHtml("#F8F3F0");
            Globalo.mConfigPanel.BackColor = ColorTranslator.FromHtml("#F8F3F0");
            Globalo.mAlarmPanel.BackColor = ColorTranslator.FromHtml("#F8F3F0");
            //Globalo.mIoPanel.eLogSender += eLogPrint;


            MainUiSet();
            AutoButtonSet(ProgramState.CurrentState);
            MenuButtonSet(0);

            SerialConnect();

            TopPanel.Paint += new PaintEventHandler(Form_Paint);


            

            eLogPrint("Main", "PG START");
            //eLogPrint("Main", "자동운전 중 진행할 수 없습니다.", Globalo.eMessageName.M_INFO);
        }
        public void InitMilLib()
        {
            Globalo.vision.UISet(Globalo.camControl.CcdPanel.Width, Globalo.camControl.CcdPanel.Height);
            //
            Globalo.vision.AllocMilApplication();
            Globalo.vision.AllocMilCamBuffer();
            Globalo.vision.AllocMilCCdBuffer(0, Globalo.mLaonGrabberClass.m_nWidth, Globalo.mLaonGrabberClass.m_nHeight);

            Globalo.vision.AllocMilCcdDisplay(Globalo.camControl.CcdPanel.Handle);
            Globalo.vision.AllocMilCamDisplay(Globalo.camControl.CamPanel.Handle);

            Globalo.vision.EnableCamOverlay();
            Globalo.vision.EnableCcdOverlay();
            Globalo.vision.DrawOverlay();
            Globalo.vision.GrabRun();
        }

        private void SerialConnect()
        {
            // 바코드 리더기 Serial Port 설정
            string portData = "";
            bool connectRtn = false;
            


            if(Globalo.yamlManager.configData != null)
            {
                portData = Globalo.yamlManager.configData.SerialPort.Bcr;
            }
            else
            {
                portData = "COM1";
            }

            Globalo.serialPortManager.Barcode = new Serial.SerialCommunicator(portData);
            Globalo.serialPortManager.Barcode.myName = "Bcr";
;            //barcodePort.DataReceived += (sender, data) =>
            //{
            //    Console.WriteLine("Barcode Reader Data: " + data);
            //};
            connectRtn = Globalo.serialPortManager.Barcode.Open();



        }
        private void AutoRunBtnUiTimer(int Mode, int interval = 300)
        {
            if (isTimerRunning)
            {
                Console.WriteLine("Timer is already running.");
                return;  // 이미 타이머가 실행 중이면 실행하지 않음
            }
            isTimerRunning = true;  // 타이머 시작 시 실행 중으로 설정
            ReadyBtnOn = false;
            _timerRunButton = new System.Windows.Forms.Timer();
            _timerRunButton.Interval = interval;
            _timerRunButton.Tick += (s, e) => RunButtonUITimerFn(Mode); // 실행할 함수 지정
            _timerRunButton.Start();
        }
        private void AutoRunTimerStop()
        {
            if(_timerRunButton != null)
            {
                _timerRunButton.Stop();
                _timerRunButton.Dispose();
                _timerRunButton = null;
            }
            
            isTimerRunning = false;
        }
        private void RunButtonUITimerFn(int Mode)
        {
            if(Mode == 1)
            {
                if (ProgramState.CurrentState == OperationState.Preparing)
                {
                    if (ReadyBtnOn)
                    {
                        Globalo.MainForm.BTN_MAIN_READY1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_OFF);
                    }
                    else
                    {
                        Globalo.MainForm.BTN_MAIN_READY1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_ON);
                    }
                    ReadyBtnOn = !ReadyBtnOn;
                }
            }
            


            if (ProgramState.CurrentState == OperationState.PreparationComplete)
            {
                Globalo.MainForm.BTN_MAIN_READY1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_ON);

                labelGuide.Text = "운전준비 완료!";
                AutoRunTimerStop();
            }
            if (ProgramState.CurrentState == OperationState.Stopped)
            {

                labelGuide.Text = "설비 정지 상태입니다.";
                AutoRunTimerStop();
            }
        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            int lineStartY = TopPanel.Height - 1;
            // Graphics 객체 가져오기
            Graphics g = e.Graphics;

            // Pen 객체 생성 (색상과 두께 설정)
            Color color = Color.FromArgb(75, 75, 75);//(86, 86, 86);
            Pen pen = new Pen(color, 2);

            // 라인 그리기 (시작점과 끝점 설정)
            g.DrawLine(pen, 0, lineStartY, TopPanel.Width, lineStartY);

            // 리소스 해제
            pen.Dispose();
        }
        private void MainUiSet()
        {
            int i = 0;


            int MainBtnWGap = 2;
            int MainBtnHGap = 2;
            //int MainBtnStartX = 1;
            int BtnPosX = 0;


            //-----------------------------------------------
            //상단 패널
            //-----------------------------------------------
            TopPanel.BackColor = ColorTranslator.FromHtml("#FAFAFA");
            MainTitleLabel.ForeColor = ColorTranslator.FromHtml("#8F949F");
            MainTitleLabel.BackColor = Color.Transparent;
            MainTitleLabel.Text = "EEprom Verify";

            //-----------------------------------------------
            int MidPanelHeight = LeftPanel.Height;          //Left Middle 패널 높이
            int ViewPanelHeight =  LeftPanel.Height - CamHeight - MainBtnHGap - RunButtonHeight - MainBtnHGap;// - ProductionHeight;      // 전체 높이에서 -카메라높이 - 버튼 높이 - 생상정보       //로그창 높이
            int nBottomPanelY = BottomPanel.Location.Y;     //Bottom 패널 Position Y


            //-----------------------------------------------
            //우측 패널
            //-----------------------------------------------
            BottomPanel.BackColor = ColorTranslator.FromHtml("#4C4743");


            //-----------------------------------------------
            //중단 우 패널
            //-----------------------------------------------

            RightPanel.BackColor = ColorTranslator.FromHtml("#F8F3F0");
            

            //-----------------------------------------------
            //좌측 패널
            //-----------------------------------------------
            LeftPanel.Controls.Add(Globalo.camControl);
            Globalo.camControl.Location = new System.Drawing.Point(0, 0);
            Globalo.camControl.Width = LeftPanel.Width;
            Globalo.camControl.Height = CamHeight;// MidPanelHeight - LogBoxHeight - RunButtonHeight + 4;

            //-----------------------------------------------
            //운전 버튼
            //-----------------------------------------------

            

            //labelGuide.Width = LeftPanel.Width - (RunBtnArr.Length * RunBtnSizeArr[RunBtnArr.Length -1]) - (RunBtnArr.Length * MainBtnWGap) - MainBtnWGap;


            for (i = RunBtnArr.Length - 1; i > -1; i--)
            {
                RunBtnArr[i].BackColor = Color.ForestGreen;
                RunBtnArr[i].Width = RunBtnSizeArr[i];
                RunBtnArr[i].Height = RunButtonHeight;

                BtnPosX += RunBtnArr[i].Width + MainBtnWGap;

                RunBtnArr[i].Location = new System.Drawing.Point((LeftPanel.Width - BtnPosX), CamHeight + MainBtnHGap);

            }

            //for (i = 0; i < RunBtnArr.Length; i++)
            //{
            //    RunBtnArr[i].BackColor = Color.ForestGreen;
            //    RunBtnArr[i].Width = RunBtnSizeArr[i];// RunButtonWidth;
            //    RunBtnArr[i].Height = RunButtonHeight;

            //    //RunBtnArr[i].Location = new System.Drawing.Point((MainBtnWGap + RunBtnArr[i].Width) * i, listBox_Log.Location.Y - RunBtnArr[i].Height - MainBtnHGap);
            //    RunBtnArr[i].Location = new System.Drawing.Point((MainBtnWGap + BtnPosX + RunGuideWidth), CamHeight + MainBtnHGap);

            //    BtnPosX += RunBtnArr[i].Width + MainBtnWGap;
            //    //
            //    //RunBtnArr[i].ForeColor = Color.Purple;
            //    // RunBtnArr[i].Font = new Font("HY수평선M", 15, FontStyle.Regular);
            //    //RunBtnArr[i].FlatStyle = FlatStyle.Popup;
            //}

            labelGuide.Width = LeftPanel.Width - (BtnPosX) - MainBtnWGap;// - (RunBtnArr.Length * MainBtnWGap) + MainBtnWGap;
            labelGuide.Height = RunButtonHeight;
            //labelGuide.Location = new System.Drawing.Point((MainBtnWGap + RunBtnSizeArr[RunBtnArr.Length - 1]) * i, CamHeight + MainBtnHGap);
            labelGuide.Location = new System.Drawing.Point(0, CamHeight + MainBtnHGap);


            //-----------------------------------------------
            //생상 정보
            //-----------------------------------------------
            panel_ProductionInfo.Width = LeftPanel.Width - 1;
            panel_ProductionInfo.Height = ViewPanelHeight;
            panel_ProductionInfo.Location = new System.Drawing.Point(0, CamHeight + MainBtnHGap + RunButtonHeight  + MainBtnHGap);
            //-----------------------------------------------
            //로그창
            //-----------------------------------------------
            listBox_Log.Width = LeftPanel.Width;
            listBox_Log.Height = LogViewHeight;
            
            listBox_Log.Margin = new Padding(0);
            listBox_Log.Location = new System.Drawing.Point(0, ViewPanelHeight - LogViewHeight+10);
            int parentHeight = listBox_Log.Parent.Height; // 실제 부모 컨트롤의 높이 사용
            int clientHeight = listBox_Log.Parent.ClientRectangle.Height;
            //listBox_Log.Location = new System.Drawing.Point(0, CamHeight+ MainBtnHGap + RunButtonHeight + MainBtnHGap + ProductionHeight +  MainBtnHGap);// 2);// + 9);

            label_LogTitle.Location = new System.Drawing.Point(0, listBox_Log.Location.Y - label_LogTitle.Height - 5);

            //
            for (i = 0; i < 1; i++)
            {
                //Log(enLogLevel.Info, $"{LogListBox.Text} LogListBox Set");
                eLogPrint("test", $"LogListBox Set" + (i + 1).ToString());
            }

            //-----------------------------------------------
            //우측 버튼 
            //-----------------------------------------------
            int BottomBtnWidth = 120;       //Panel = 140
            int BottomBtnHeight = 50;
            //Button[] BtnArr = { BTN_BOTTOM_MAIN, BTN_BOTTOM_SETUP, BTN_BOTTOM_MANUAL, BTN_BOTTOM_TEACH, 
            //    BTN_BOTTOM_CCD, BTN_BOTTOM_IO, BTN_BOTTOM_LIGHT, BTN_BOTTOM_ALARM, BTN_BOTTOM_WALLPAPER, BTN_BOTTOM_EXIT };


            //int BottomBtnX = 10;
            int BottomBtnY = (BottomPanel.Height - BTN_TOP_LOG.Height) / 2;
            int BottomBtnStartX = 0;
            int BottomBtnStartY = BTN_TOP_LOG.Height + 0;
            //int BottomBtnWGap = 5;
            int BottomBtnHGap = 0;



            for (i = 0; i < BtnArr.Length; i++)
            {
                BtnArr[i].Width = BottomBtnWidth;
                BtnArr[i].Height = BottomBtnHeight;
                BtnArr[i].BackColor = Color.Transparent;
                BtnArr[i].ForeColor = ColorTranslator.FromHtml("#979591");


                //BtnArr[i].FlatStyle = FlatStyle.Flat;//FlatStyle.Popup;
                //BtnArr[i].Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);


                BtnArr[i].Location = new System.Drawing.Point(BottomBtnStartX, BottomBtnStartY + (BottomBtnHeight + BottomBtnHGap) * i);
                BtnArr[i].Width = BottomPanel.Width;
                BtnArr[i].Height = BottomBtnHeight;
            }

            BTN_TOP_LOG.Width = BottomPanel.Width;
            BTN_TOP_LOG.Height = TopPanel.Height-1;
            BTN_TOP_LOG.BackColor = ColorTranslator.FromHtml("#ED6C44");

            //TIME SET
            //TimeLabel.ForeColor = Color.White;
            //TimeLabel.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Regular);
            //TimeLabel.Location = new System.Drawing.Point(TopPanel.Width - 180, 10);
            TimeLabel.Width = BottomPanel.Width;
            TimeLabel.ForeColor = ColorTranslator.FromHtml(ButtonColor.BTN_ON);
            TimeLabel.Location = new System.Drawing.Point(0, BottomPanel.Height - TimeLabel.Height - 10);



            //VERSION SET
            label_version.Location = new System.Drawing.Point(0, TimeLabel.Location.Y - label_version.Height - 10);
            label_build.Location = new System.Drawing.Point(0, label_version.Location.Y - label_build.Height - 5);
            //
            //


            Globalo.mCCdPanel.Visible = false;
            Globalo.mConfigPanel.Visible = false;
            Globalo.mAlarmPanel.Visible = false;
            RightPanel.Controls.Add(Globalo.mMainPanel);
            RightPanel.Controls.Add(Globalo.mCCdPanel);
            RightPanel.Controls.Add(Globalo.mConfigPanel);
            RightPanel.Controls.Add(Globalo.mAlarmPanel);

        }
        private void eLogPrint(object oSender, string LogDesc, Globalo.eMessageName bPopUpView = Globalo.eMessageName.M_NULL)
        {
            DateTime dTime = DateTime.Now;
            string LogInfo = $"[{dTime:hh:mm:ss.f}] {LogDesc}";
            Globalo.threadControl.logThread.logQueue.Enqueue(LogInfo);

            if (bPopUpView != Globalo.eMessageName.M_NULL)
            {
                MessagePopUpForm messagePopUp3 = new MessagePopUpForm();
                messagePopUp3.MessageSet(Globalo.eMessageName.M_ERROR, LogDesc);
                messagePopUp3.Show();
            }
        }

        private void BTN_BOTTOM_EXIT_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //MenuButtonSet(9);
            eLogPrint("ExitBtn", $"{btn.Text} 버튼 Click");


            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");
            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, "프로그램 종료 하시겠습니까?");

            DialogResult result = messagePopUp3.ShowDialog();
            if (result == DialogResult.Yes)
            {
                FuncExit();
            }
            



        }
        public void FuncExit()
        {
            //Time Thread End
            //oGlobal.mDioControl.DioEnd();

            //oGlobal.mtimeThread.mTimeThreadRun = false;
            //if (oGlobal.mtimeThread != null)
            //{
            //oGlobal.mtimeThread.Interrupt();   //스레도 실행 정지
            //oGlobal.mtimeThread.Join();
            //}
            //Vision End
            //oGlobal.vision.ThreadEnd();


            Globalo.threadControl.AllClose();

            if (ProgramState.ON_LINE_MOTOR)
            {
                Globalo.motorControl.Axl_Close();
            }

            Globalo.ubisamForm.UbisamClose();
            Globalo.GrabberDll = null;
            //
            //foreach (Form form in Application.OpenForms)
            //{
            //    form.Close();
            //}
            Globalo.dataManage.teachingData.eLogSender -= eLogPrint;
            Globalo.motorControl.eLogSender -= eLogPrint;
            Globalo.dIoControl.eLogSender -= eLogPrint;
            // 다이얼로그
            //
            //mTeachPanel = new Dlg.TeachingControl();
            //mManualPanel = new Dlg.ManualControl();
            // Thread Main
            //

            //Globalo.threadControl.autoRunthread.eLogSender -= eLogPrint;
            Globalo.mLaonGrabberClass.eLogSender -= eLogPrint;
            Globalo.mLaonGrabberClass.Dispose();
            foreach (var thread in System.Diagnostics.Process.GetCurrentProcess().Threads)
            {
                ((System.Diagnostics.ProcessThread)thread).Dispose();
            }
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();

        }

        private void BTN_BOTTOM_WALLPAPER_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            eLogPrint("AlarmBtn",  $"{btn.Text} 버튼 Click");
            this.WindowState = FormWindowState.Minimized;

            //MenuButtonSet(8);
        }

        private void BTN_BOTTOM_TEACH_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            eLogPrint("TeachBtn", $"{btn.Text} 버튼 Click");

            //Globalo.mTeachPanel.Visible = true;
            //RightPanel.Controls.Add(Globalo.mTeachPanel);
            //foreach (UserControl item in RightPanel.Controls)
            //{
            //    if (item.Name != Globalo.mTeachPanel.Name)
            //    {
            //        RightPanel.Controls.Remove(item);
            //    }
            //}
            //RightPanel.Controls.Clear();
            //RightPanel.Controls.Add(Globalo.mTeachPanel);
            MenuButtonSet(1);
        }

        private void BTN_TOP_LOG_Click(object sender, EventArgs e)
        {
            //Button btn = sender as Button;
            //eLogPrint("Log", $"{btn.Text} 버튼 Click");

            if (Debugger.IsAttached)
            {
                //string parameter = "111,222,333,444,555,";
                //string[] values = parameter.Split(',');

                //int cont = values.Length;                           //6
                //int commaCount = parameter.Count(c => c == ',');    //5
                //MessagePopUpForm messageForm = new MessagePopUpForm();
                //int testNum = 10;
                //string LogInfo = $"[{testNum}] Message Test";

                //messageForm.MessageSet(Globalo.eMessageName.M_OP_CALL, LogInfo, "LGIT OP CALL", $"OPCALL CODE :{testNum} ");
                //messageForm.Show(); // 새로운 창 계속 생성



                uint testio = 0xFF;

                testio &= ~((uint)DioDefine.DIO_OUT_ADDR.BUZZER1 | (uint)DioDefine.DIO_OUT_ADDR.BUZZER3);       //testio에서 BUZZER1 하고 BUZZER3을 끄다.
                testio |= ((uint)DioDefine.DIO_OUT_ADDR.BUZZER1 | (uint)DioDefine.DIO_OUT_ADDR.BUZZER3);        //testio에서 BUZZER1 하고 BUZZER3 만 켜다.

                testio = 0x00;

                Globalo.LogPrint("MainControl", "ALARM TEST 111", Globalo.eMessageName.M_WARNING);

            }
            else
            {
                //Form messageForm = new Form
                //{
                //    Text = "이벤트 발생!",
                //    Size = new System.Drawing.Size(300, 200)
                //};

                //Label label = new Label
                //{
                //    Text = "이벤트가 발생했습니다!",
                //    AutoSize = true,
                //    Location = new System.Drawing.Point(50, 50)
                //};

                //messageForm.Controls.Add(label);
                //messageForm.Show(); // 새로운 창 계속 생성
            }

            
        }

        private void BTN_BOTTOM_IO_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            eLogPrint("ManualBtn", $"{btn.Text} 버튼 Click");

            //Globalo.mIoPanel.Visible = true;


            //RightPanel.Controls.Clear();
            //RightPanel.Controls.Add(Globalo.mIoPanel);
            //foreach (UserControl item in RightPanel.Controls)
            //{
            //    if (item.Name != Globalo.mIoPanel.Name)
            //    {
            //        RightPanel.Controls.Remove(item);
            //    }
            //}
            MenuButtonSet(3);
        }
        /// <summary>
        /// 자동 운전
        /// </summary>
        private void BTN_MAIN_START1_Click(object sender, EventArgs e)
        {
            if (ProgramState.CurrentState == OperationState.AutoRunning)
            {
                eLogPrint("MainForm", "[INFO] 자동 운전 중 사용 불가", Globalo.eMessageName.M_WARNING);
                return;
            }

            if (ProgramState.CurrentState == OperationState.Stopped)
            {
                Globalo.LogPrint("MainForm", "[INFO] 운전준비가 완료되지 않았습니다.", Globalo.eMessageName.M_WARNING);
                return;
            }

            string logStr = "자동운전 진행 하시겠습니까 ?";
            if (ProgramState.CurrentState == OperationState.Paused)
            {
                if (Math.Abs(Globalo.taskWork.m_nCurrentStep) < 30000 || Math.Abs(Globalo.taskWork.m_nCurrentStep) >= 90000)
                {
                    eLogPrint("MainForm", "[INFO] 운전 준비 상태가 아닙니다.", Globalo.eMessageName.M_WARNING);
                    return;
                }
                logStr = "자동운전 재개 하시겠습니까 ?";
                //if (Globalo.taskWork.m_nCurrentStep >= 20000 && Globalo.taskWork.m_nCurrentStep < 30000)
            }
            else
            {
                if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
                {
                    eLogPrint("MainForm", "[INFO] 자동 운전 중 사용 불가", Globalo.eMessageName.M_WARNING);
                    return;
                }
            }

            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");

            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, logStr);
            DialogResult result = messagePopUp.ShowDialog();


            if (result == DialogResult.Yes)
            {
                StartAutoProcess();     //자동 운전 시작
            }
            


        }
        /// <summary>
        /// 정지
        /// </summary>
        private void BTN_MAIN_STOP1_Click(object sender, EventArgs e)
        {
            StopAutoProcess();
            
            Globalo.LogPrint("MainForm", "[AUTO] AUTO RUN STOP.");
        }
        /// <summary>
        /// 원점 잡기
        /// </summary>
        private void BTN_MAIN_ORIGIN1_Click(object sender, EventArgs e)
        {
            if (ProgramState.CurrentState == OperationState.Originning)
            {
                eLogPrint("MainForm", "[INFO] 원점 동작 중 사용 불가", Globalo.eMessageName.M_WARNING);
                return;
            }
            if (ProgramState.CurrentState == OperationState.AutoRunning)
            {
                eLogPrint("MainForm", "[INFO] 자동 운전 중 사용 불가", Globalo.eMessageName.M_WARNING);
                return;
            }
            if (ProgramState.CurrentState == OperationState.Paused)
            {
                eLogPrint("ManualCMainFormontrol", "[INFO] 일시 정지 중 사용 불가", Globalo.eMessageName.M_WARNING);
                return;
            }

            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");
            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, "전체 원점복귀 하시겠습니까 ?");
            DialogResult result = messagePopUp.ShowDialog();
            if (result == DialogResult.Yes)
            {
                StartHomeProcess();
            }
            //DialogResult.Cancel


        }
        /// <summary>
        /// 운전준비
        /// </summary>
        private void BTN_MAIN_READY1_Click(object sender, EventArgs e)
        {
            if (ProgramState.CurrentState == OperationState.Originning)
            {
                eLogPrint("MainForm", "[INFO] 원점 동작 중 사용 불가", Globalo.eMessageName.M_WARNING);
                return;
            }
            if (ProgramState.CurrentState == OperationState.AutoRunning)
            {
                eLogPrint("MainForm", "[INFO] 자동 운전 중 사용 불가", Globalo.eMessageName.M_WARNING);
                return;
            }
            if (ProgramState.CurrentState == OperationState.Preparing)
            {
                eLogPrint("MainForm", "[INFO] 운전 준비 중 사용 불가", Globalo.eMessageName.M_WARNING);
                return;
            }
            //if (ProgramState.CurrentState == OperationState.Paused)
            //{
            //    eLogPrint("ManualCMainFormontrol", "[INFO] 일시 정지 중 사용 불가", Globalo.eMessageName.M_WARNING);
            //    return;
            //}

            string logStr = "운전준비 하시겠습니까 ?";

            if(ProgramState.CurrentState == OperationState.Paused)
            {
                if(Math.Abs(Globalo.taskWork.m_nCurrentStep) < 20000 || Math.Abs(Globalo.taskWork.m_nCurrentStep) >= 30000)
                {
                    eLogPrint("MainForm", "[INFO] 설비 정지 상태가 아닙니다.", Globalo.eMessageName.M_WARNING);
                    return;
                }
                logStr = "운전준비 재개 하시겠습니까 ?";    //이때 스텝이 20000보다 작아야된다.
                //if (Globalo.taskWork.m_nCurrentStep >= 20000 && Globalo.taskWork.m_nCurrentStep < 30000)
            }
            else
            {
                if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
                {
                    eLogPrint("MainForm", "[INFO] 자동 운전 중 사용 불가", Globalo.eMessageName.M_WARNING);
                    return;
                }
            }
            
            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");

            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, logStr);
            DialogResult result = messagePopUp.ShowDialog();


            if (result == DialogResult.Yes)
            {
                StartAutoReadyProcess();
            }
            
        }
        /// <summary>
        /// 일시정지
        /// </summary>
        private void BTN_MAIN_PAUSE1_Click(object sender, EventArgs e)
        {
            if (ProgramState.CurrentState == OperationState.Stopped)
            {
                eLogPrint("ManualCMainFormontrol", "[INFO] 설비 정지 상태입니다.", Globalo.eMessageName.M_WARNING);
                return;
            }
            if (ProgramState.CurrentState == OperationState.Paused)
            {
                eLogPrint("ManualCMainFormontrol", "[INFO] 일시 정지 상태입니다.", Globalo.eMessageName.M_WARNING);
                return;
            }
            if (ProgramState.CurrentState == OperationState.PreparationComplete)
            {
                eLogPrint("ManualCMainFormontrol", "[INFO] 자동 운전 중이 아닙니다..", Globalo.eMessageName.M_WARNING);
                return;
            }
            


            PauseAutoProcess();
        }


        public bool StartHomeProcess()
        {
            int i = 0;
            for (i = 0; i < MotorControl.PCB_UNIT_COUNT; i++)
            {
                if (Globalo.motorControl.PcbMotorAxis[i].AmpEnable() == false)
                {
                    eLogPrint("ManualCMainFormontrol", $"[ORIGIN] {Globalo.motorControl.PcbMotorAxis[i].Name} AXIS SERVO ON FAIL", Globalo.eMessageName.M_WARNING);
                    return false;
                }
                if (Globalo.motorControl.PcbMotorAxis[i].GetStopAxis() == false)
                {
                    eLogPrint("ManualCMainFormontrol", $"[ORIGIN] {Globalo.motorControl.PcbMotorAxis[i].Name} AXIS 구동중입니다.", Globalo.eMessageName.M_WARNING);
                    return false;
                }
            }

            for (i = 0; i < MotorControl.LENS_UNIT_COUNT; i++)
            {
                if (Globalo.motorControl.LensMotorAxis[i].AmpEnable() == false)
                {
                    eLogPrint("ManualCMainFormontrol", $"[ORIGIN] {Globalo.motorControl.LensMotorAxis[i].Name} AXIS SERVO ON FAIL", Globalo.eMessageName.M_WARNING);
                    return false;
                }
                if (Globalo.motorControl.LensMotorAxis[i].GetStopAxis() == false)
                {
                    eLogPrint("ManualCMainFormontrol", $"[ORIGIN] {Globalo.motorControl.LensMotorAxis[i].Name} AXIS 구동중입니다.", Globalo.eMessageName.M_WARNING);
                    return false;
                }
            }
            if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
            {
                if (Globalo.threadControl.autoRunthread.GetThreadPause() == true)
                {
                    //eLogPrint("ManualCMainFormontrol", "[ORIGIN] 일시 정지 중 사용 불가", Globalo.eMessageName.M_WARNING);
                    //g_clTaskWork[nUnit].m_nCurrentStep = abs(g_clTaskWork[nUnit].m_nCurrentStep);

                    // g_clTaskWork[nUnit].m_nAutoFlag = MODE_AUTO;
                    eLogPrint("ManualCMainFormontrol", "[ORIGIN] ORIGIN PAUSE RELEASE");

                    Globalo.taskWork.m_nCurrentStep = Math.Abs(Globalo.taskWork.m_nCurrentStep);
                    ProgramState.CurrentState = OperationState.Originning;
                    //Globalo.threadControl.readyRunthread.Pause();
                    return true;
                }



                eLogPrint("ManualCMainFormontrol", "[ORIGIN] 동작중 사용 불가", Globalo.eMessageName.M_WARNING);
                return false;
            }

            Globalo.taskWork.m_nStartStep = 10000;
            Globalo.taskWork.m_nEndStep = 20000;
            Globalo.taskWork.m_nCurrentStep = 10000;

            ProgramState.CurrentState = OperationState.Originning;
            bool bRtn = Globalo.threadControl.autoRunthread.Start();
            if (bRtn == false)
            {
                ProgramState.CurrentState = OperationState.Stopped;
            }

            eLogPrint("ManualCMainFormontrol", "[ORIGIN] ORIGIN RUN START");
            AutoButtonSet(ProgramState.CurrentState);

            return true;
        }

        
        public void StartAutoReadyProcess()
        {
            if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
            {
                if (ProgramState.CurrentState == OperationState.Paused)
                {
                    Globalo.taskWork.m_nCurrentStep = Math.Abs(Globalo.taskWork.m_nCurrentStep);
                    Globalo.LogPrint("MainForm", "[AUTO] AUTO RUN RESUME");
                }
                else
                {
                    Globalo.LogPrint("MainForm", "[AUTO] AUTO RUN START FAIL");
                    return;
                }
            }
            else
            {
                Globalo.taskWork.m_nCurrentStep = 20000;

                if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
                {
                    Globalo.LogPrint("MainForm", "[AUTO] AUTO RUN START FAIL");
                    return;
                }
                Globalo.LogPrint("MainForm", "[AUTO] AUTO RUN START");
            }

           
            Globalo.taskWork.m_nStartStep = 20000;
            Globalo.taskWork.m_nEndStep = 30000;
            

            ProgramState.CurrentState = OperationState.Preparing;


            
            bool bRtn = Globalo.threadControl.autoRunthread.Start();

            if (bRtn == false)
            {
                Globalo.LogPrint("MainForm", "[AUTO] AUTO RUN START FAIL");
                ProgramState.CurrentState = OperationState.Stopped;
                return;

            }
            AutoRunBtnUiTimer(1);

            labelGuide.Text = "설비 운전준비중 입니다.";

            AutoButtonSet(ProgramState.CurrentState);

        }
        public void PaustReadyProcess()
        {

            AutoButtonSet(ProgramState.CurrentState);
        }
        public void PauseAutoProcess()
        {
            ProgramState.CurrentState = OperationState.Paused;
            //labelGuide.Text = "설비 일시정지 상태입니다.";

            if (labelGuide.InvokeRequired)
            {
                labelGuide.BeginInvoke(new Action(() => labelGuide.Text = "설비 일시정지 상태입니다."));
            }
            else
            {
                labelGuide.Text = "설비 일시정지 상태입니다.";
            }

            Globalo.threadControl.autoRunthread.Pause();

            AutoRunTimerStop();

            AutoButtonSet(ProgramState.CurrentState);
        }
        public bool StartAutoProcess()
        {
            //모터 구동중 체크
            //운전준비 체크
            if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
            {
                if (ProgramState.CurrentState == OperationState.Paused)
                {
                    Globalo.taskWork.m_nCurrentStep = Math.Abs(Globalo.taskWork.m_nCurrentStep);
                    Globalo.LogPrint("MainForm", "[AUTO] AUTO RUN RESUME");
                }
                else
                {
                    Globalo.LogPrint("MainForm", "[AUTO] AUTO RUN START FAIL");
                    return false;
                }
            }
            else
            {
                Globalo.taskWork.m_nCurrentStep = 30000;
            }
            //
            //

            //if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
            //{
            //    if (Globalo.threadControl.autoRunthread.GetThreadPause() == true)
            //    {
            //        Globalo.taskWork.m_nCurrentStep = Math.Abs(Globalo.taskWork.m_nCurrentStep);

            //        ProgramState.CurrentState = OperationState.AutoRunning;

            //        return false;
            //    }
            //    else
            //    {

            //        return false;
            //    }
            //}


            Globalo.taskWork.m_nStartStep = 30000;
            Globalo.taskWork.m_nEndStep = 40000;

            ProgramState.CurrentState = OperationState.AutoRunning;
            bool bRtn = Globalo.threadControl.autoRunthread.Start();
            if (bRtn == false)
            {
                ProgramState.CurrentState = OperationState.Stopped;
                return false;

            }

            labelGuide.Text = "자동 운전 중입니다.";
            AutoButtonSet(ProgramState.CurrentState);


            Globalo.LogPrint("MainForm", "[AUTO] AUTO RUN START");
            return true;
        }
        public void StopAutoProcess()
        {
            AutoRunTimerStop();

            ProgramState.CurrentState = OperationState.Stopped;

            labelGuide.Text = "설비 정지 상태입니다.";

            if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
            {
                Globalo.threadControl.autoRunthread.Stop();
            }
            if (ProgramState.ON_LINE_MOTOR)
            {
                Globalo.motorControl.StopAxisAll(0);
            }
            AutoButtonSet(ProgramState.CurrentState);
        }
        private void MenuButtonSet(int index)
        {
            int i = 0;

            //초기화
            for (i = 0; i < BtnArr.Length; i++)
            {
                BtnArr[i].BackColor = Color.Transparent;
                BtnArr[i].ForeColor = ColorTranslator.FromHtml("#979591");
            }



            BtnArr[index].BackColor = ColorTranslator.FromHtml("#F8F3F0");
            BtnArr[index].ForeColor = ColorTranslator.FromHtml("#D7C1A6");
        }
        public void AutoButtonSet(OperationState operation)
        {
            BTN_MAIN_ORIGIN1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_OFF); //C3A279
            BTN_MAIN_PAUSE1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_OFF);
            BTN_MAIN_READY1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_OFF);
            BTN_MAIN_STOP1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_OFF);
            BTN_MAIN_START1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_OFF);
            switch (operation)
            {
                //case OperationState.Originning:
                //    BTN_MAIN_ORIGIN1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_ON);//FFB230
                //    break;
                case OperationState.AutoRunning:
                    BTN_MAIN_START1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_ON);//FFB230
                    break;
                case OperationState.Paused:
                    BTN_MAIN_PAUSE1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_ON);
                    break;
                case OperationState.PreparationComplete:
                    BTN_MAIN_READY1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_ON);
                    break;
                case OperationState.Stopped:
                    BTN_MAIN_STOP1.BackColor = ColorTranslator.FromHtml(ButtonColor.BTN_ON);
                    break;
            }
        }

        private void BTN_BOTTOM_MAIN_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            eLogPrint("MainBtn", $"{btn.Text} 버튼 Click");
            Globalo.mMainPanel.Visible = true;
            Globalo.mCCdPanel.Visible = false;
            Globalo.mConfigPanel.Visible = false;
            Globalo.mAlarmPanel.Visible = false;

            MenuButtonSet(0);
        }
        private void BTN_BOTTOM_CCD_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            eLogPrint("CCdBtn", $"{btn.Text} 버튼 Click");
            Globalo.mCCdPanel.Visible = true;
            Globalo.mMainPanel.Visible = false;
            Globalo.mConfigPanel.Visible = false;
            Globalo.mAlarmPanel.Visible = false;
            MenuButtonSet(1);
        }
        private void BTN_BOTTOM_SETUP_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            eLogPrint("ConfigBtn", $"{btn.Text} 버튼 Click");

            //Console.WriteLine($"부모 컨트롤 Visible 상태: {Globalo.mCCdPanel.Parent?.Visible}");
            //Console.WriteLine($"mCCdPanel 컨트롤 Visible 상태: {Globalo.mCCdPanel?.Visible}");
            Globalo.mConfigPanel.Visible = true;
            Globalo.mCCdPanel.Visible = false;
            Globalo.mMainPanel.Visible = false;
            Globalo.mAlarmPanel.Visible = false;

            MenuButtonSet(2);


        }
        private void BTN_BOTTOM_ALARM_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            eLogPrint("AlarmBtn", $"{btn.Text} 버튼 Click");
            Globalo.mAlarmPanel.Visible = true;
            Globalo.mConfigPanel.Visible = false;
            Globalo.mCCdPanel.Visible = false;
            Globalo.mMainPanel.Visible = false;

            MenuButtonSet(3);
        }


        private void BTN_BOTTOM_LIGHT_Click(object sender, EventArgs e)
        {
            MenuButtonSet(4);
        }

        private void BTN_TOP_MES_Click(object sender, EventArgs e)
        {
            Globalo.ubisamForm.Visible = true;
        }
    }
}
