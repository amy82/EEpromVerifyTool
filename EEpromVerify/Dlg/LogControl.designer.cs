namespace ApsMotionControl.Dlg
{
    partial class LogControl
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
            this.BTN_MANUAL_PCB = new System.Windows.Forms.Button();
            this.BTN_MANUAL_LENS = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BTN_LOG_DUMP_OPEN = new System.Windows.Forms.Button();
            this.BTN_LOG_EQUIP_OPEN = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
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
            this.ManualTitleLabel.Text = "| LOG";
            this.ManualTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ManualPanel
            // 
            this.ManualPanel.Controls.Add(this.groupBox2);
            this.ManualPanel.Controls.Add(this.groupBox1);
            this.ManualPanel.Location = new System.Drawing.Point(21, 97);
            this.ManualPanel.Name = "ManualPanel";
            this.ManualPanel.Size = new System.Drawing.Size(899, 926);
            this.ManualPanel.TabIndex = 4;
            // 
            // BTN_MANUAL_PCB
            // 
            this.BTN_MANUAL_PCB.BackColor = System.Drawing.Color.Tan;
            this.BTN_MANUAL_PCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MANUAL_PCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_MANUAL_PCB.ForeColor = System.Drawing.Color.White;
            this.BTN_MANUAL_PCB.Location = new System.Drawing.Point(579, 14);
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
            this.BTN_MANUAL_LENS.Location = new System.Drawing.Point(748, 16);
            this.BTN_MANUAL_LENS.Name = "BTN_MANUAL_LENS";
            this.BTN_MANUAL_LENS.Size = new System.Drawing.Size(154, 44);
            this.BTN_MANUAL_LENS.TabIndex = 31;
            this.BTN_MANUAL_LENS.Text = "LENS";
            this.BTN_MANUAL_LENS.UseVisualStyleBackColor = false;
            this.BTN_MANUAL_LENS.Visible = false;
            this.BTN_MANUAL_LENS.Click += new System.EventHandler(this.BTN_MANUAL_LENS_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.BTN_LOG_DUMP_OPEN);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(36, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dump File Path";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.BTN_LOG_EQUIP_OPEN);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(36, 231);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 132);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Equip Log Path";
            // 
            // BTN_LOG_DUMP_OPEN
            // 
            this.BTN_LOG_DUMP_OPEN.BackColor = System.Drawing.Color.Tan;
            this.BTN_LOG_DUMP_OPEN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_LOG_DUMP_OPEN.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOG_DUMP_OPEN.ForeColor = System.Drawing.Color.White;
            this.BTN_LOG_DUMP_OPEN.Location = new System.Drawing.Point(293, 28);
            this.BTN_LOG_DUMP_OPEN.Name = "BTN_LOG_DUMP_OPEN";
            this.BTN_LOG_DUMP_OPEN.Size = new System.Drawing.Size(96, 30);
            this.BTN_LOG_DUMP_OPEN.TabIndex = 32;
            this.BTN_LOG_DUMP_OPEN.Text = "FOLDER OPEN";
            this.BTN_LOG_DUMP_OPEN.UseVisualStyleBackColor = false;
            // 
            // BTN_LOG_EQUIP_OPEN
            // 
            this.BTN_LOG_EQUIP_OPEN.BackColor = System.Drawing.Color.Tan;
            this.BTN_LOG_EQUIP_OPEN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_LOG_EQUIP_OPEN.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOG_EQUIP_OPEN.ForeColor = System.Drawing.Color.White;
            this.BTN_LOG_EQUIP_OPEN.Location = new System.Drawing.Point(293, 30);
            this.BTN_LOG_EQUIP_OPEN.Name = "BTN_LOG_EQUIP_OPEN";
            this.BTN_LOG_EQUIP_OPEN.Size = new System.Drawing.Size(96, 30);
            this.BTN_LOG_EQUIP_OPEN.TabIndex = 33;
            this.BTN_LOG_EQUIP_OPEN.Text = "FOLDER OPEN";
            this.BTN_LOG_EQUIP_OPEN.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.Location = new System.Drawing.Point(8, 33);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(272, 21);
            this.textBox1.TabIndex = 34;
            this.textBox1.Text = "D:\\EVMS\\Log\\Step";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox2.Location = new System.Drawing.Point(8, 30);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(272, 22);
            this.textBox2.TabIndex = 35;
            this.textBox2.Text = "D:\\EVMS\\Log\\Dump";
            // 
            // LogControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Controls.Add(this.BTN_MANUAL_LENS);
            this.Controls.Add(this.BTN_MANUAL_PCB);
            this.Controls.Add(this.ManualPanel);
            this.Controls.Add(this.ManualTitleLabel);
            this.Name = "LogControl";
            this.Size = new System.Drawing.Size(940, 1042);
            this.VisibleChanged += new System.EventHandler(this.AlarmControl_VisibleChanged);
            this.ManualPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ManualTitleLabel;
        private System.Windows.Forms.Panel ManualPanel;
        private System.Windows.Forms.Button BTN_MANUAL_PCB;
        private System.Windows.Forms.Button BTN_MANUAL_LENS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BTN_LOG_DUMP_OPEN;
        private System.Windows.Forms.Button BTN_LOG_EQUIP_OPEN;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}
