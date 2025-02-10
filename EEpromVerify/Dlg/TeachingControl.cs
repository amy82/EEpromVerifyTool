using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ApsMotionControl.Dlg
{
    public partial class TeachingControl : UserControl
    {
        private MotorControl motorControl;
        public event delLogSender eLogSender;       //외부에서 호출할때 사용
        private eTeachingBtn TeachCurrentTab;

        //private int dPcbMotorCount = 0;

        private double m_dJogSpeed = 0.1;
        
        private bool m_bJogPosRunCheck = false;
        private bool m_bJogNegRunCheck = false;
        private bool m_bJogPosDir = false;
        private bool m_bJogNegDir = false;

        //public int SelectPcbAxis = -1;

        //public int SelectLensAxis = -1;

        //private TeachingPcb teachingPcb = new TeachingPcb();
        //private TeachingLens teachingLens = new TeachingLens();
        private TeachingPcb teachingPcb;
        private TeachingLens teachingLens;
        private enum eTeachingBtn : int
        {
            pcbTab = 0, lensTab
        };

        private Timer JogTimer;
        public TeachingControl(int _w , int _h)
        {
            InitializeComponent();

            teachingPcb = new TeachingPcb();
            teachingLens = new TeachingLens();
            this.Paint += new PaintEventHandler(Form_Paint);

            motorControl = Globalo.motorControl;

            JogTimer = new Timer();
            JogTimer.Interval = 50; // 1초 (1000밀리초) 간격 설정
            JogTimer.Tick += new EventHandler(JogTimer_Tick);

            //motorControl = motor;
            this.Width = _w;
            this.Height = _h;


            teachingPcb.Visible = false;
            teachingLens.Visible = false;
            TeachingPanel.Controls.Add(teachingPcb);
            TeachingPanel.Controls.Add(teachingLens);

            setInterface();

            changeSpeedNo(0);

            TeachingBtnChange(eTeachingBtn.pcbTab);
        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            int lineStartY = TeachingTitleLabel.Location.Y + 60;
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

            TeachingTitleLabel.ForeColor = ColorTranslator.FromHtml("#6F6F6F");

            BTN_TEACH_PCB.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#BBBBBB");
            BTN_TEACH_LENS.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#BBBBBB");


            BTN_TEACH_SPEED_LOW.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_SPEED_LOW.ForeColor = Color.White;

            BTN_TEACH_SPEED_MID.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_SPEED_MID.ForeColor = Color.White;

            BTN_TEACH_SPEED_HIGH.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_SPEED_HIGH.ForeColor = Color.White;

            BTN_TEACH_JOG_MINUS.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_JOG_MINUS.ForeColor = Color.White;

            BTN_TEACH_JOG_STOP.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_JOG_STOP.ForeColor = Color.White;

            BTN_TEACH_JOG_PLUS.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_JOG_PLUS.ForeColor = Color.White;

            BTN_TEACH_MOVE_MINUS.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_MOVE_MINUS.ForeColor = Color.White;

            BTN_TEACH_MOVE_PLUS.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_MOVE_PLUS.ForeColor = Color.White;

            BTN_TEACH_DATA_SAVE.BackColor = ColorTranslator.FromHtml("#FFB230");
            BTN_TEACH_DATA_SAVE.ForeColor = Color.White;


            //TeachingTitleLabel.Text = "TEACHING";
            //TeachingTitleLabel.ForeColor = Color.Khaki;
            //TeachingTitleLabel.BackColor = Color.Maroon;
            //TeachingTitleLabel.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Regular);
            //TeachingTitleLabel.Width = this.Width;
            //TeachingTitleLabel.Height = 45;
            //TeachingTitleLabel.Location = new Point(0, 0);


            //TeachPcbGridInit();
            //TeachLensGridInit();

            //TeachTabControl.Location = new Point(200, 70);
            //TeachTabControl.Width = 150 + (80 * 6) + 12;
            //TeachTabControl.Height = dRowHeight * (nGridRowCount + 2) - 7;// 560;
        }
        
        private void TeachTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 현재 선택된 탭의 인덱스
            //currentTabIndex = TeachTabControl.SelectedIndex;
            //if (currentTabIndex == 0)
            //{
            //    //[0] Pcb Unit
            //    changeMotorNo(m_nSelectPcbAxis);  //Teaching Dlg Visible on
            //    TeachTabControl.Width = 150 + (80 * MotorControl.PCB_UNIT_COUNT) + 12;
            //    TeachTabControl.Height = dRowHeight * (nGridRowCount + 2) - 7;// 560;
            //}
            //else
            //{
            //    //[1] Lens Unit
            //    changeMotorNo(m_nSelectLensAxis);
            //    TeachTabControl.Width = 150 + (80 * MotorControl.LENS_UNIT_COUNT) + 12;
            //    TeachTabControl.Height = dRowHeight * (nGridRowCount + 2) - 7;// 560;
            //}

            //changeSpeedNo(0);

            

            //ShowTeachingData();

            ///MessageBox.Show($"Selected Tab Index: {selectedIndex}");
        }
        private void JogTimer_Tick(object sender, EventArgs e)
        {
            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                if (m_bJogPosDir == true)
                {
                    motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].JogMove(1, m_dJogSpeed);
                }
                else if (m_bJogPosDir == false && m_bJogPosRunCheck == true)
                {
                    motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].JogStop();
                }
                m_bJogPosRunCheck = m_bJogPosDir;
                if (m_bJogNegDir == true)
                {
                    motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].JogMove(-1, m_dJogSpeed);
                }
                else if (m_bJogNegDir == false && m_bJogNegRunCheck == true)
                {
                    motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].JogStop();
                }
            }
            else if (TeachCurrentTab == eTeachingBtn.lensTab)
            {
                if (m_bJogPosDir == true)
                {
                    motorControl.LensMotorAxis[teachingLens.SelectLensAxis].JogMove(1, m_dJogSpeed);
                }
                else if (m_bJogPosDir == false && m_bJogPosRunCheck == true)
                {
                    motorControl.LensMotorAxis[teachingLens.SelectLensAxis].JogStop();
                }
                m_bJogPosRunCheck = m_bJogPosDir;
                if (m_bJogNegDir == true)
                {
                    motorControl.LensMotorAxis[teachingLens.SelectLensAxis].JogMove(-1, m_dJogSpeed);
                }
                else if (m_bJogNegDir == false && m_bJogNegRunCheck == true)
                {
                    motorControl.LensMotorAxis[teachingLens.SelectLensAxis].JogStop();
                }
            }
                
            
            m_bJogNegRunCheck = m_bJogNegDir;
        }
            
     

        

        private void TeachingControl_Load(object sender, EventArgs e)
        {
            changeSpeedNo(0);

            //changeMotorNo(m_nSelectPcbAxis);      //Teaching Dlg Load

            //ShowTeachingData();

            eLogSender("CTeachingControl", "Teach Visible True raised!!!");
            if (ProgramState.ON_LINE_MOTOR)
            {
                //TeachingTimer.Start(); // 타이머 시작
                JogTimer.Start();
            }
        }
        private void TeachingControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                

                eLogSender("CTeachingControl", "Teach Visible True raised!!!");
                if (ProgramState.ON_LINE_MOTOR)
                {
                    //TeachingTimer.Start(); // 타이머 시작
                    JogTimer.Start();
                }
            }
            else
            {
                eLogSender("CTeachingControl", "Teach Visible False raised!!!");
                //TeachingTimer.Stop(); // 타이머 정지
                JogTimer.Stop();
            }

            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                teachingLens.hidePanel();
                teachingPcb.showPanel();
            }
            else if (TeachCurrentTab == eTeachingBtn.lensTab)
            {
                teachingPcb.hidePanel();
                teachingLens.showPanel();
                //teachingLens.Show();

            }


        }

        private void BTN_TEACH_SERVO_ON_Click(object sender, EventArgs e)
        {
            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].AmpEnable();
            }
            else if (TeachCurrentTab == eTeachingBtn.lensTab)
            {
                motorControl.LensMotorAxis[teachingLens.SelectLensAxis].AmpEnable();
            }
                
        }

        private void BTN_TEACH_SERVO_OFF_Click(object sender, EventArgs e)
        {
            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].AmpDisable();
            }
            else if (TeachCurrentTab == eTeachingBtn.lensTab)
            {
                motorControl.LensMotorAxis[teachingLens.SelectLensAxis].AmpDisable();
            }
            
        }

        private void BTN_TEACH_SERVO_RESET_Click(object sender, EventArgs e)
        {
            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].AmpFaultReset();
            }
            else if (TeachCurrentTab == eTeachingBtn.lensTab)
            {
                motorControl.LensMotorAxis[teachingLens.SelectLensAxis].AmpFaultReset();
            }
            
            
        }

        private void BTN_TEACH_MOVE_MINUS_Click(object sender, EventArgs e)
        {
            if (ProgramState.ON_LINE_MOTOR == false)
            {
                return;
            }
            double dMovePos = double.Parse(LABEL_TEACH_MOVE_VALUE.Text);
            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                if (teachingPcb.SelectPcbAxis < 0)
                {
                    return;
                }
                motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].MoveFromAbsRel(dMovePos, false);
            }
            else if (TeachCurrentTab == eTeachingBtn.lensTab)
            {
                if (teachingLens.SelectLensAxis < 0)
                {
                    return;
                }
                motorControl.LensMotorAxis[teachingLens.SelectLensAxis].MoveFromAbsRel(dMovePos, false);
            }
            
        }

        private void BTN_TEACH_MOVE_PLUS_Click(object sender, EventArgs e)
        {
            if (ProgramState.ON_LINE_MOTOR == false)
            {
                return;
            }
            double dMovePos = double.Parse(LABEL_TEACH_MOVE_VALUE.Text);

            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                if (teachingPcb.SelectPcbAxis < 0)
                {
                    return;
                }
                motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].MoveFromAbsRel(dMovePos, true);
            }
            else if (TeachCurrentTab == eTeachingBtn.lensTab)
            {
                if (teachingLens.SelectLensAxis < 0)
                {
                    return;
                }
                motorControl.LensMotorAxis[teachingLens.SelectLensAxis].MoveFromAbsRel(dMovePos, true);
            }
            
        }

        private void LABEL_TEACH_MOVE_VALUE_Click(object sender, EventArgs e)
        {
            string labelValue = LABEL_TEACH_MOVE_VALUE.Text;
            decimal decimalValue = 0;
            if (decimal.TryParse(labelValue, out decimalValue))
            {
                // 소수점 형식으로 변환
                string formattedValue = decimalValue.ToString("0.0##");
                NumPadForm popupForm = new NumPadForm(formattedValue);
                //popupForm.ShowDialog();
                DialogResult dialogResult = popupForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    double dNumData = popupForm.Result;
                    if (dNumData > 5.0)
                    {
                        dNumData = 5.0;
                    }
                    if (dNumData < -5.0)
                    {
                        dNumData = -5.0;
                    }
                    LABEL_TEACH_MOVE_VALUE.Text = dNumData.ToString("0.0##");
                }
                // popupForm.Show(); // 비모달로 팝업 폼 표시
            }
            //popupForm.NumberEntered += PopupForm_NumberEntered; // 이벤트 핸들러 등록

            
        }
        private void PopupForm_NumberEntered(object sender, double number , bool bUser)
        {
            MessageBox.Show($"Entered number: {number.ToString("0.0##")}");
            //LABEL_TEACH_MOVE_VALUE.Text = number.ToString();
        }
        private void changeSpeedNo(int nSpeedNo)
        {
            BTN_TEACH_SPEED_LOW.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_SPEED_MID.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_SPEED_HIGH.BackColor = ColorTranslator.FromHtml("#C3A279");

            switch (nSpeedNo)
            {
                case 0:
                    BTN_TEACH_SPEED_LOW.BackColor = ColorTranslator.FromHtml("#4C4743");
                    m_dJogSpeed = 0.1;
                    break;
                case 1:
                    BTN_TEACH_SPEED_MID.BackColor = ColorTranslator.FromHtml("#4C4743");
                    m_dJogSpeed = 0.5;
                    break;
                case 2:
                    BTN_TEACH_SPEED_HIGH.BackColor = ColorTranslator.FromHtml("#4C4743");
                    m_dJogSpeed = 1.0;
                    break;
            }
        }
        //private void BTN_TEACH_PCB_X_Click(object sender, EventArgs e)
        //{
        //    changeMotorNo((int)MotorDefine.eMotorAxis.MOTOR_PCB_X);  //BTN 1
        //}

        //private void BTN_TEACH_PCB_Y_Click(object sender, EventArgs e)
        //{
        //    changeMotorNo((int)MotorDefine.eMotorAxis.MOTOR_PCB_Y);  //BTN 2
        //}

        //private void BTN_TEACH_PCB_Z_Click(object sender, EventArgs e)
        //{
        //    changeMotorNo((int)MotorDefine.eMotorAxis.MOTOR_PCB_Z);  //BTN 3
        //}

        //private void BTN_TEACH_PCB_TH_Click(object sender, EventArgs e)
        //{
        //    changeMotorNo((int)MotorDefine.eMotorAxis.MOTOR_PCB_TH);  //BTN 4
        //}

        //private void BTN_TEACH_PCB_TX_Click(object sender, EventArgs e)
        //{
        //    changeMotorNo((int)MotorDefine.eMotorAxis.MOTOR_PCB_TX);  //BTN 5
        //}

        //private void BTN_TEACH_PCB_TY_Click(object sender, EventArgs e)
        //{
        //    changeMotorNo((int)MotorDefine.eMotorAxis.MOTOR_PCB_TY);  //BTN 6
        //}

        private void BTN_TEACH_JOG_MINUS_MouseDown(object sender, MouseEventArgs e)
        {
            if (teachingPcb.SelectPcbAxis < 0)
            {
                return;
            }
            if (teachingLens.SelectLensAxis < 0)
            {
                return;
            }
            m_bJogNegDir = true;
            //motorControl.PcbMotorAxis[m_nSelectAxis].JogMove(-1, m_dJogSpeed);
        }

        private void BTN_TEACH_JOG_MINUS_MouseUp(object sender, MouseEventArgs e)
        {
            if (teachingPcb.SelectPcbAxis < 0)
            {
                return;
            }
            if (teachingLens.SelectLensAxis < 0)
            {
                return;
            }
            m_bJogNegDir = false;
            //motorControl.PcbMotorAxis[m_nSelectAxis].JogStop();
        }

        private void BTN_TEACH_SPEED_LOW_Click(object sender, EventArgs e)
        {
            changeSpeedNo(0);
        }

        private void BTN_TEACH_SPEED_MID_Click(object sender, EventArgs e)
        {
            changeSpeedNo(1);
        }

        private void BTN_TEACH_SPEED_HIGH_Click(object sender, EventArgs e)
        {
            changeSpeedNo(2);
        }

        private void BTN_TEACH_JOG_PLUS_MouseDown(object sender, MouseEventArgs e)
        {
            if (teachingPcb.SelectPcbAxis < 0)
            {
                return;
            }
            if (teachingLens.SelectLensAxis < 0)
            {
                return;
            }
            m_bJogPosDir = true;
            //eLogSender("CTeachingControl", LogDefine.enLogLevel.Info, "Jog Plus Move Press");
            //motorControl.PcbMotorAxis[m_nSelectAxis].JogMove(1, m_dJogSpeed);
        }

        private void BTN_TEACH_JOG_PLUS_MouseUp(object sender, MouseEventArgs e)
        {
            if (teachingPcb.SelectPcbAxis < 0)
            {
                return;
            }
            if (teachingLens.SelectLensAxis < 0)
            {
                return;
            }
            m_bJogPosDir = false;
            //eLogSender("CTeachingControl", LogDefine.enLogLevel.Info, "Jog Plus Move Stop");
            //motorControl.PcbMotorAxis[m_nSelectAxis].JogStop();
        }

        private void BTN_TEACH_JOG_STOP_Click(object sender, EventArgs e)
        {
            if (teachingPcb.SelectPcbAxis < 0)
            {
                return;
            }
            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                motorControl.PcbMotorAxis[teachingPcb.SelectPcbAxis].JogStop();
            }
            else if (TeachCurrentTab == eTeachingBtn.lensTab)
            {
                if (teachingLens.SelectLensAxis < 0)
                {
                    return;
                }
                motorControl.LensMotorAxis[teachingLens.SelectLensAxis].JogStop();
            }
                
        }

        private void BTN_TEACH_DATA_SAVE_Click(object sender, EventArgs e)
        {
            //GetTeachData();
            eLogSender("CTeachingControl", "Teach Data Save.");
            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                teachingPcb.getDataPanel();
            }
            else if (TeachCurrentTab == eTeachingBtn.lensTab)
            {
                if (teachingLens.SelectLensAxis < 0)
                {
                    return;
                }
                teachingLens.getDataPanel();
            }
                
            Globalo.dataManage.teachingData.DataSave();
        }

        
        //컨트롤 지워질때 동작
        protected override void OnHandleDestroyed(EventArgs e)
        {
            //TeachingTimer.Stop();
            JogTimer.Stop();
        }
       
        private void TeachingBtnChange(eTeachingBtn index)
        {
            BTN_TEACH_PCB.BackColor = ColorTranslator.FromHtml("#E1E0DF");
            BTN_TEACH_LENS.BackColor = ColorTranslator.FromHtml("#E1E0DF");

            TeachCurrentTab = index;

            if (TeachCurrentTab == eTeachingBtn.pcbTab)
            {
                BTN_TEACH_PCB.BackColor = ColorTranslator.FromHtml("#FFB230");
                teachingPcb.Visible = true;
                teachingLens.Visible = false;

                teachingLens.hidePanel();
                teachingPcb.showPanel();
            }
            else
            {
                BTN_TEACH_LENS.BackColor = ColorTranslator.FromHtml("#FFB230");
                teachingLens.Visible = true;
                teachingPcb.Visible = false;

                teachingPcb.hidePanel();
                teachingLens.showPanel();
            }
        }
        private void BTN_TEACH_PCB_Click(object sender, EventArgs e)
        {
            TeachingBtnChange(eTeachingBtn.pcbTab);
        }

        private void BTN_TEACH_LENS_Click(object sender, EventArgs e)
        {
            TeachingBtnChange(eTeachingBtn.lensTab);
        }
    }
}
