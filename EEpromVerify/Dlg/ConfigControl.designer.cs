namespace ApsMotionControl.Dlg
{
    partial class ConfigControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.ManualTitleLabel = new System.Windows.Forms.Label();
            this.ManualPanel = new System.Windows.Forms.Panel();
            this.BTN_CONFIG_SAVE = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_Config_Bcr = new System.Windows.Forms.Label();
            this.comboBox_Port_Bcr = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_IdleReportPass = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BTN_MANUAL_PCB = new System.Windows.Forms.Button();
            this.BTN_MANUAL_LENS = new System.Windows.Forms.Button();
            this.checkBox_BcrGo = new System.Windows.Forms.CheckBox();
            this.ManualPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ManualTitleLabel
            // 
            this.ManualTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ManualTitleLabel.Location = new System.Drawing.Point(16, 16);
            this.ManualTitleLabel.Name = "ManualTitleLabel";
            this.ManualTitleLabel.Size = new System.Drawing.Size(250, 42);
            this.ManualTitleLabel.TabIndex = 2;
            this.ManualTitleLabel.Text = "| CONFIG";
            this.ManualTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ManualPanel
            // 
            this.ManualPanel.Controls.Add(this.BTN_CONFIG_SAVE);
            this.ManualPanel.Controls.Add(this.groupBox1);
            this.ManualPanel.Controls.Add(this.groupBox2);
            this.ManualPanel.Location = new System.Drawing.Point(21, 97);
            this.ManualPanel.Name = "ManualPanel";
            this.ManualPanel.Size = new System.Drawing.Size(909, 737);
            this.ManualPanel.TabIndex = 4;
            // 
            // BTN_CONFIG_SAVE
            // 
            this.BTN_CONFIG_SAVE.BackColor = System.Drawing.Color.Tan;
            this.BTN_CONFIG_SAVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_CONFIG_SAVE.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_CONFIG_SAVE.ForeColor = System.Drawing.Color.White;
            this.BTN_CONFIG_SAVE.Location = new System.Drawing.Point(755, 656);
            this.BTN_CONFIG_SAVE.Name = "BTN_CONFIG_SAVE";
            this.BTN_CONFIG_SAVE.Size = new System.Drawing.Size(122, 53);
            this.BTN_CONFIG_SAVE.TabIndex = 28;
            this.BTN_CONFIG_SAVE.Text = "SAVE";
            this.BTN_CONFIG_SAVE.UseVisualStyleBackColor = false;
            this.BTN_CONFIG_SAVE.Click += new System.EventHandler(this.BTN_CONFIG_SAVE_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label_Config_Bcr);
            this.groupBox1.Controls.Add(this.comboBox_Port_Bcr);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(363, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 297);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // label_Config_Bcr
            // 
            this.label_Config_Bcr.BackColor = System.Drawing.SystemColors.Window;
            this.label_Config_Bcr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Config_Bcr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Config_Bcr.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Config_Bcr.ForeColor = System.Drawing.Color.DimGray;
            this.label_Config_Bcr.Location = new System.Drawing.Point(6, 53);
            this.label_Config_Bcr.Name = "label_Config_Bcr";
            this.label_Config_Bcr.Size = new System.Drawing.Size(150, 21);
            this.label_Config_Bcr.TabIndex = 30;
            this.label_Config_Bcr.Text = "BCR";
            this.label_Config_Bcr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_Port_Bcr
            // 
            this.comboBox_Port_Bcr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Port_Bcr.FormattingEnabled = true;
            this.comboBox_Port_Bcr.Location = new System.Drawing.Point(162, 53);
            this.comboBox_Port_Bcr.Name = "comboBox_Port_Bcr";
            this.comboBox_Port_Bcr.Size = new System.Drawing.Size(106, 20);
            this.comboBox_Port_Bcr.TabIndex = 27;
            this.comboBox_Port_Bcr.VisibleChanged += new System.EventHandler(this.comboBox_Port_Bcr_VisibleChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(40, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 23);
            this.label1.TabIndex = 26;
            this.label1.Text = "COM PORT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.checkBox_BcrGo);
            this.groupBox2.Controls.Add(this.checkBox_IdleReportPass);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(28, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 297);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            // 
            // checkBox_IdleReportPass
            // 
            this.checkBox_IdleReportPass.BackColor = System.Drawing.Color.WhiteSmoke;
            this.checkBox_IdleReportPass.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.checkBox_IdleReportPass.FlatAppearance.BorderSize = 2;
            this.checkBox_IdleReportPass.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGray;
            this.checkBox_IdleReportPass.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_IdleReportPass.Location = new System.Drawing.Point(11, 53);
            this.checkBox_IdleReportPass.Name = "checkBox_IdleReportPass";
            this.checkBox_IdleReportPass.Size = new System.Drawing.Size(275, 40);
            this.checkBox_IdleReportPass.TabIndex = 27;
            this.checkBox_IdleReportPass.Text = "IDLE REASON REPORT PASS";
            this.checkBox_IdleReportPass.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(23, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 23);
            this.label3.TabIndex = 26;
            this.label3.Text = "운전 설정";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_MANUAL_PCB
            // 
            this.BTN_MANUAL_PCB.BackColor = System.Drawing.Color.Tan;
            this.BTN_MANUAL_PCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MANUAL_PCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_MANUAL_PCB.ForeColor = System.Drawing.Color.White;
            this.BTN_MANUAL_PCB.Location = new System.Drawing.Point(616, 16);
            this.BTN_MANUAL_PCB.Name = "BTN_MANUAL_PCB";
            this.BTN_MANUAL_PCB.Size = new System.Drawing.Size(154, 44);
            this.BTN_MANUAL_PCB.TabIndex = 30;
            this.BTN_MANUAL_PCB.Text = "PCB";
            this.BTN_MANUAL_PCB.UseVisualStyleBackColor = false;
            this.BTN_MANUAL_PCB.Visible = false;
            this.BTN_MANUAL_PCB.Click += new System.EventHandler(this.BTN_MANUAL_PCB_Click);
            // 
            // BTN_MANUAL_LENS
            // 
            this.BTN_MANUAL_LENS.BackColor = System.Drawing.Color.Tan;
            this.BTN_MANUAL_LENS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MANUAL_LENS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_MANUAL_LENS.ForeColor = System.Drawing.Color.White;
            this.BTN_MANUAL_LENS.Location = new System.Drawing.Point(776, 16);
            this.BTN_MANUAL_LENS.Name = "BTN_MANUAL_LENS";
            this.BTN_MANUAL_LENS.Size = new System.Drawing.Size(154, 44);
            this.BTN_MANUAL_LENS.TabIndex = 31;
            this.BTN_MANUAL_LENS.Text = "LENS";
            this.BTN_MANUAL_LENS.UseVisualStyleBackColor = false;
            this.BTN_MANUAL_LENS.Visible = false;
            this.BTN_MANUAL_LENS.Click += new System.EventHandler(this.BTN_MANUAL_LENS_Click);
            // 
            // checkBox_BcrGo
            // 
            this.checkBox_BcrGo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.checkBox_BcrGo.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.checkBox_BcrGo.FlatAppearance.BorderSize = 2;
            this.checkBox_BcrGo.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGray;
            this.checkBox_BcrGo.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_BcrGo.Location = new System.Drawing.Point(11, 99);
            this.checkBox_BcrGo.Name = "checkBox_BcrGo";
            this.checkBox_BcrGo.Size = new System.Drawing.Size(275, 40);
            this.checkBox_BcrGo.TabIndex = 28;
            this.checkBox_BcrGo.Text = "Start Automation on Barcode";
            this.checkBox_BcrGo.UseVisualStyleBackColor = false;
            // 
            // ConfigControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Controls.Add(this.BTN_MANUAL_LENS);
            this.Controls.Add(this.BTN_MANUAL_PCB);
            this.Controls.Add(this.ManualPanel);
            this.Controls.Add(this.ManualTitleLabel);
            this.Name = "ConfigControl";
            this.Size = new System.Drawing.Size(955, 938);
            this.ManualPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ManualTitleLabel;
        private System.Windows.Forms.Panel ManualPanel;
        private System.Windows.Forms.Button BTN_MANUAL_PCB;
        private System.Windows.Forms.Button BTN_MANUAL_LENS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BTN_CONFIG_SAVE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_Port_Bcr;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label_Config_Bcr;
        private System.Windows.Forms.CheckBox checkBox_IdleReportPass;
        private System.Windows.Forms.CheckBox checkBox_BcrGo;
    }
}
