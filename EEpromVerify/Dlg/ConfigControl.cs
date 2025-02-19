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
    public partial class ConfigControl : UserControl
    {
        //public event delLogSender eLogSender;       //외부에서 호출할때 사용
        //private ManualPcb manualPcb = new ManualPcb();
        //private ManualLens manualLens = new ManualLens();

        private enum eManualBtn : int
        {
            pcbTab = 0, lensTab
        };
        public ConfigControl(int _w, int _h)
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(Form_Paint);
            
            this.Width = _w;
            this.Height = _h;

            setInterface();

        }
        public void RefreshConfig()
        {
            ShowDriveSet();
            ShowComPort();
        }
        public void GetConfigData()
        {
            //운전 설정
            Globalo.yamlManager.configData.DrivingSettings.IdleReportPass = checkBox_IdleReportPass.Checked;
            Globalo.yamlManager.configData.DrivingSettings.EnableAutoStartBcr = checkBox_BcrGo.Checked;
            
            //Serial Port
            Globalo.yamlManager.configData.SerialPort.Bcr = comboBox_Port_Bcr.Text;
        }
        public void ShowDriveSet()
        {
            bool setBool = Globalo.yamlManager.configData.DrivingSettings.IdleReportPass;
            checkBox_IdleReportPass.Checked = Globalo.yamlManager.configData.DrivingSettings.IdleReportPass;
            checkBox_BcrGo.Checked = Globalo.yamlManager.configData.DrivingSettings.EnableAutoStartBcr;

        }
        public void ShowComPort()
        {
            string comData = Globalo.yamlManager.configData.SerialPort.Bcr;
            int index = comboBox_Port_Bcr.Items.IndexOf(comData);
            if (index < 0)
            {
                comboBox_Port_Bcr.SelectedIndex = 0;  // 첫 번째 항목 선택
            }
            else
            {
                comboBox_Port_Bcr.SelectedIndex = index;
            }
        }
        public void setInterface()
        {
            int i = 0;
            ManualTitleLabel.ForeColor = ColorTranslator.FromHtml("#6F6F6F");

            for (i = 0; i < 20; i++)
            {
                comboBox_Port_Bcr.Items.Add("COM" + (i + 1).ToString());
            }

            
            
            //string selectedValue = comboBox_Port_Bcr.SelectedItem.ToString();

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

        private void comboBox_Port_Bcr_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                RefreshConfig();
            }
        }

        private void BTN_CONFIG_SAVE_Click(object sender, EventArgs e)
        {
            //Save

            GetConfigData();
            //
            Globalo.yamlManager.configDataSave();
            //
            RefreshConfig();
        }
    }
}
