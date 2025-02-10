namespace ApsMotionControl.Dlg
{
    partial class CCdControl
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BTN_CCD_RAW_SAVE = new System.Windows.Forms.Button();
            this.BTN_CCD_RAW_LOAD = new System.Windows.Forms.Button();
            this.BTN_CCD_BMP_SAVE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_CCD_BMP_LOAD = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BTN_CCD_GABBER_CLOSE = new System.Windows.Forms.Button();
            this.BTN_CCD_GABBER_STOP = new System.Windows.Forms.Button();
            this.BTN_CCD_GABBER_START = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.BTN_CCD_GABBER_OPEN = new System.Windows.Forms.Button();
            this.BTN_MANUAL_PCB = new System.Windows.Forms.Button();
            this.BTN_MANUAL_LENS = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ManualPanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.ManualTitleLabel.Text = "| CCD";
            this.ManualTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ManualPanel
            // 
            this.ManualPanel.Controls.Add(this.groupBox3);
            this.ManualPanel.Controls.Add(this.groupBox1);
            this.ManualPanel.Controls.Add(this.groupBox2);
            this.ManualPanel.Location = new System.Drawing.Point(21, 97);
            this.ManualPanel.Name = "ManualPanel";
            this.ManualPanel.Size = new System.Drawing.Size(934, 737);
            this.ManualPanel.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Location = new System.Drawing.Point(639, 375);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 311);
            this.groupBox3.TabIndex = 46;
            this.groupBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(23, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 23);
            this.label2.TabIndex = 26;
            this.label2.Text = "TEST";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Tan;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(26, 54);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(142, 46);
            this.button4.TabIndex = 27;
            this.button4.Text = "Sensor Id Read";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.BTN_CCD_RAW_SAVE);
            this.groupBox1.Controls.Add(this.BTN_CCD_RAW_LOAD);
            this.groupBox1.Controls.Add(this.BTN_CCD_BMP_SAVE);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BTN_CCD_BMP_LOAD);
            this.groupBox1.Location = new System.Drawing.Point(373, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 311);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // BTN_CCD_RAW_SAVE
            // 
            this.BTN_CCD_RAW_SAVE.BackColor = System.Drawing.Color.Tan;
            this.BTN_CCD_RAW_SAVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_CCD_RAW_SAVE.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_CCD_RAW_SAVE.ForeColor = System.Drawing.Color.White;
            this.BTN_CCD_RAW_SAVE.Location = new System.Drawing.Point(27, 211);
            this.BTN_CCD_RAW_SAVE.Name = "BTN_CCD_RAW_SAVE";
            this.BTN_CCD_RAW_SAVE.Size = new System.Drawing.Size(142, 50);
            this.BTN_CCD_RAW_SAVE.TabIndex = 30;
            this.BTN_CCD_RAW_SAVE.Text = "RAW SAVE";
            this.BTN_CCD_RAW_SAVE.UseVisualStyleBackColor = false;
            this.BTN_CCD_RAW_SAVE.Click += new System.EventHandler(this.BTN_CCD_RAW_SAVE_Click);
            // 
            // BTN_CCD_RAW_LOAD
            // 
            this.BTN_CCD_RAW_LOAD.BackColor = System.Drawing.Color.Tan;
            this.BTN_CCD_RAW_LOAD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_CCD_RAW_LOAD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_CCD_RAW_LOAD.ForeColor = System.Drawing.Color.White;
            this.BTN_CCD_RAW_LOAD.Location = new System.Drawing.Point(26, 160);
            this.BTN_CCD_RAW_LOAD.Name = "BTN_CCD_RAW_LOAD";
            this.BTN_CCD_RAW_LOAD.Size = new System.Drawing.Size(142, 50);
            this.BTN_CCD_RAW_LOAD.TabIndex = 29;
            this.BTN_CCD_RAW_LOAD.Text = "RAW LOAD";
            this.BTN_CCD_RAW_LOAD.UseVisualStyleBackColor = false;
            this.BTN_CCD_RAW_LOAD.Click += new System.EventHandler(this.BTN_CCD_RAW_LOAD_Click);
            // 
            // BTN_CCD_BMP_SAVE
            // 
            this.BTN_CCD_BMP_SAVE.BackColor = System.Drawing.Color.Tan;
            this.BTN_CCD_BMP_SAVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_CCD_BMP_SAVE.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_CCD_BMP_SAVE.ForeColor = System.Drawing.Color.White;
            this.BTN_CCD_BMP_SAVE.Location = new System.Drawing.Point(26, 109);
            this.BTN_CCD_BMP_SAVE.Name = "BTN_CCD_BMP_SAVE";
            this.BTN_CCD_BMP_SAVE.Size = new System.Drawing.Size(142, 50);
            this.BTN_CCD_BMP_SAVE.TabIndex = 28;
            this.BTN_CCD_BMP_SAVE.Text = "BMP SAVE";
            this.BTN_CCD_BMP_SAVE.UseVisualStyleBackColor = false;
            this.BTN_CCD_BMP_SAVE.Click += new System.EventHandler(this.BTN_CCD_BMP_SAVE_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(23, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 23);
            this.label1.TabIndex = 26;
            this.label1.Text = "IMAGE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_CCD_BMP_LOAD
            // 
            this.BTN_CCD_BMP_LOAD.BackColor = System.Drawing.Color.Tan;
            this.BTN_CCD_BMP_LOAD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_CCD_BMP_LOAD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_CCD_BMP_LOAD.ForeColor = System.Drawing.Color.White;
            this.BTN_CCD_BMP_LOAD.Location = new System.Drawing.Point(26, 58);
            this.BTN_CCD_BMP_LOAD.Name = "BTN_CCD_BMP_LOAD";
            this.BTN_CCD_BMP_LOAD.Size = new System.Drawing.Size(142, 50);
            this.BTN_CCD_BMP_LOAD.TabIndex = 27;
            this.BTN_CCD_BMP_LOAD.Text = "BMP LOAD";
            this.BTN_CCD_BMP_LOAD.UseVisualStyleBackColor = false;
            this.BTN_CCD_BMP_LOAD.Click += new System.EventHandler(this.BTN_CCD_BMP_LOAD_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.BTN_CCD_GABBER_CLOSE);
            this.groupBox2.Controls.Add(this.BTN_CCD_GABBER_STOP);
            this.groupBox2.Controls.Add(this.BTN_CCD_GABBER_START);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.BTN_CCD_GABBER_OPEN);
            this.groupBox2.Location = new System.Drawing.Point(639, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 311);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            // 
            // BTN_CCD_GABBER_CLOSE
            // 
            this.BTN_CCD_GABBER_CLOSE.BackColor = System.Drawing.Color.Tan;
            this.BTN_CCD_GABBER_CLOSE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_CCD_GABBER_CLOSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_CCD_GABBER_CLOSE.ForeColor = System.Drawing.Color.White;
            this.BTN_CCD_GABBER_CLOSE.Location = new System.Drawing.Point(27, 211);
            this.BTN_CCD_GABBER_CLOSE.Name = "BTN_CCD_GABBER_CLOSE";
            this.BTN_CCD_GABBER_CLOSE.Size = new System.Drawing.Size(142, 50);
            this.BTN_CCD_GABBER_CLOSE.TabIndex = 30;
            this.BTN_CCD_GABBER_CLOSE.Text = "CLOSE";
            this.BTN_CCD_GABBER_CLOSE.UseVisualStyleBackColor = false;
            this.BTN_CCD_GABBER_CLOSE.Click += new System.EventHandler(this.BTN_CCD_GABBER_CLOSE_Click);
            // 
            // BTN_CCD_GABBER_STOP
            // 
            this.BTN_CCD_GABBER_STOP.BackColor = System.Drawing.Color.Tan;
            this.BTN_CCD_GABBER_STOP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_CCD_GABBER_STOP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_CCD_GABBER_STOP.ForeColor = System.Drawing.Color.White;
            this.BTN_CCD_GABBER_STOP.Location = new System.Drawing.Point(26, 160);
            this.BTN_CCD_GABBER_STOP.Name = "BTN_CCD_GABBER_STOP";
            this.BTN_CCD_GABBER_STOP.Size = new System.Drawing.Size(142, 50);
            this.BTN_CCD_GABBER_STOP.TabIndex = 29;
            this.BTN_CCD_GABBER_STOP.Text = "STOP";
            this.BTN_CCD_GABBER_STOP.UseVisualStyleBackColor = false;
            this.BTN_CCD_GABBER_STOP.Click += new System.EventHandler(this.BTN_CCD_GABBER_STOP_Click);
            // 
            // BTN_CCD_GABBER_START
            // 
            this.BTN_CCD_GABBER_START.BackColor = System.Drawing.Color.Tan;
            this.BTN_CCD_GABBER_START.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_CCD_GABBER_START.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_CCD_GABBER_START.ForeColor = System.Drawing.Color.White;
            this.BTN_CCD_GABBER_START.Location = new System.Drawing.Point(26, 109);
            this.BTN_CCD_GABBER_START.Name = "BTN_CCD_GABBER_START";
            this.BTN_CCD_GABBER_START.Size = new System.Drawing.Size(142, 50);
            this.BTN_CCD_GABBER_START.TabIndex = 28;
            this.BTN_CCD_GABBER_START.Text = "START";
            this.BTN_CCD_GABBER_START.UseVisualStyleBackColor = false;
            this.BTN_CCD_GABBER_START.Click += new System.EventHandler(this.BTN_CCD_GABBER_START_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(23, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 23);
            this.label3.TabIndex = 26;
            this.label3.Text = "GRABBER";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_CCD_GABBER_OPEN
            // 
            this.BTN_CCD_GABBER_OPEN.BackColor = System.Drawing.Color.Tan;
            this.BTN_CCD_GABBER_OPEN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_CCD_GABBER_OPEN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_CCD_GABBER_OPEN.ForeColor = System.Drawing.Color.White;
            this.BTN_CCD_GABBER_OPEN.Location = new System.Drawing.Point(26, 58);
            this.BTN_CCD_GABBER_OPEN.Name = "BTN_CCD_GABBER_OPEN";
            this.BTN_CCD_GABBER_OPEN.Size = new System.Drawing.Size(142, 50);
            this.BTN_CCD_GABBER_OPEN.TabIndex = 27;
            this.BTN_CCD_GABBER_OPEN.Text = "OPEN";
            this.BTN_CCD_GABBER_OPEN.UseVisualStyleBackColor = false;
            this.BTN_CCD_GABBER_OPEN.Click += new System.EventHandler(this.BTN_CCD_GABBER_OPEN_Click);
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Tan;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(27, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 46);
            this.button1.TabIndex = 28;
            this.button1.Text = "EEprom Read";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // CCdControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Controls.Add(this.BTN_MANUAL_LENS);
            this.Controls.Add(this.BTN_MANUAL_PCB);
            this.Controls.Add(this.ManualPanel);
            this.Controls.Add(this.ManualTitleLabel);
            this.Name = "CCdControl";
            this.Size = new System.Drawing.Size(955, 938);
            this.ManualPanel.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.Button BTN_CCD_GABBER_CLOSE;
        private System.Windows.Forms.Button BTN_CCD_GABBER_STOP;
        private System.Windows.Forms.Button BTN_CCD_GABBER_START;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BTN_CCD_GABBER_OPEN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BTN_CCD_RAW_SAVE;
        private System.Windows.Forms.Button BTN_CCD_RAW_LOAD;
        private System.Windows.Forms.Button BTN_CCD_BMP_SAVE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_CCD_BMP_LOAD;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
    }
}
