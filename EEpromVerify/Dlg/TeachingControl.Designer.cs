namespace ApsMotionControl.Dlg
{
    partial class TeachingControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BTN_TEACH_SPEED_LOW = new System.Windows.Forms.Button();
            this.BTN_TEACH_SPEED_MID = new System.Windows.Forms.Button();
            this.BTN_TEACH_SPEED_HIGH = new System.Windows.Forms.Button();
            this.BTN_TEACH_JOG_MINUS = new System.Windows.Forms.Button();
            this.BTN_TEACH_JOG_STOP = new System.Windows.Forms.Button();
            this.BTN_TEACH_JOG_PLUS = new System.Windows.Forms.Button();
            this.BTN_TEACH_MOVE_MINUS = new System.Windows.Forms.Button();
            this.BTN_TEACH_MOVE_PLUS = new System.Windows.Forms.Button();
            this.LABEL_TEACH_MOVE_VALUE = new System.Windows.Forms.Label();
            this.BTN_TEACH_DATA_SAVE = new System.Windows.Forms.Button();
            this.TeachingTitleLabel = new System.Windows.Forms.Label();
            this.TeachingPanel = new System.Windows.Forms.Panel();
            this.BTN_TEACH_LENS = new System.Windows.Forms.Button();
            this.BTN_TEACH_PCB = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(77, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 23);
            this.label2.TabIndex = 18;
            this.label2.Text = "MOTOR SPEED";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(77, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 23);
            this.label3.TabIndex = 19;
            this.label3.Text = "JOG MOVE";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(437, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(261, 23);
            this.label4.TabIndex = 20;
            this.label4.Text = "VALUE MOVE";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_TEACH_SPEED_LOW
            // 
            this.BTN_TEACH_SPEED_LOW.BackColor = System.Drawing.Color.PeachPuff;
            this.BTN_TEACH_SPEED_LOW.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_SPEED_LOW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_SPEED_LOW.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_SPEED_LOW.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_SPEED_LOW.Location = new System.Drawing.Point(79, 42);
            this.BTN_TEACH_SPEED_LOW.Name = "BTN_TEACH_SPEED_LOW";
            this.BTN_TEACH_SPEED_LOW.Size = new System.Drawing.Size(85, 59);
            this.BTN_TEACH_SPEED_LOW.TabIndex = 21;
            this.BTN_TEACH_SPEED_LOW.Text = "LOW";
            this.BTN_TEACH_SPEED_LOW.UseVisualStyleBackColor = false;
            this.BTN_TEACH_SPEED_LOW.Click += new System.EventHandler(this.BTN_TEACH_SPEED_LOW_Click);
            // 
            // BTN_TEACH_SPEED_MID
            // 
            this.BTN_TEACH_SPEED_MID.BackColor = System.Drawing.Color.PeachPuff;
            this.BTN_TEACH_SPEED_MID.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_SPEED_MID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_SPEED_MID.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_SPEED_MID.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_SPEED_MID.Location = new System.Drawing.Point(165, 42);
            this.BTN_TEACH_SPEED_MID.Name = "BTN_TEACH_SPEED_MID";
            this.BTN_TEACH_SPEED_MID.Size = new System.Drawing.Size(85, 59);
            this.BTN_TEACH_SPEED_MID.TabIndex = 22;
            this.BTN_TEACH_SPEED_MID.Text = "MID";
            this.BTN_TEACH_SPEED_MID.UseVisualStyleBackColor = false;
            this.BTN_TEACH_SPEED_MID.Click += new System.EventHandler(this.BTN_TEACH_SPEED_MID_Click);
            // 
            // BTN_TEACH_SPEED_HIGH
            // 
            this.BTN_TEACH_SPEED_HIGH.BackColor = System.Drawing.Color.PeachPuff;
            this.BTN_TEACH_SPEED_HIGH.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_SPEED_HIGH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_SPEED_HIGH.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_SPEED_HIGH.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_SPEED_HIGH.Location = new System.Drawing.Point(251, 42);
            this.BTN_TEACH_SPEED_HIGH.Name = "BTN_TEACH_SPEED_HIGH";
            this.BTN_TEACH_SPEED_HIGH.Size = new System.Drawing.Size(85, 59);
            this.BTN_TEACH_SPEED_HIGH.TabIndex = 23;
            this.BTN_TEACH_SPEED_HIGH.Text = "HIGH";
            this.BTN_TEACH_SPEED_HIGH.UseVisualStyleBackColor = false;
            this.BTN_TEACH_SPEED_HIGH.Click += new System.EventHandler(this.BTN_TEACH_SPEED_HIGH_Click);
            // 
            // BTN_TEACH_JOG_MINUS
            // 
            this.BTN_TEACH_JOG_MINUS.BackColor = System.Drawing.Color.PeachPuff;
            this.BTN_TEACH_JOG_MINUS.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_JOG_MINUS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_JOG_MINUS.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_JOG_MINUS.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_JOG_MINUS.Location = new System.Drawing.Point(79, 144);
            this.BTN_TEACH_JOG_MINUS.Name = "BTN_TEACH_JOG_MINUS";
            this.BTN_TEACH_JOG_MINUS.Size = new System.Drawing.Size(85, 59);
            this.BTN_TEACH_JOG_MINUS.TabIndex = 24;
            this.BTN_TEACH_JOG_MINUS.Text = "JOG -";
            this.BTN_TEACH_JOG_MINUS.UseVisualStyleBackColor = false;
            this.BTN_TEACH_JOG_MINUS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BTN_TEACH_JOG_MINUS_MouseDown);
            this.BTN_TEACH_JOG_MINUS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BTN_TEACH_JOG_MINUS_MouseUp);
            // 
            // BTN_TEACH_JOG_STOP
            // 
            this.BTN_TEACH_JOG_STOP.BackColor = System.Drawing.Color.PeachPuff;
            this.BTN_TEACH_JOG_STOP.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_JOG_STOP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_JOG_STOP.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_JOG_STOP.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_JOG_STOP.Location = new System.Drawing.Point(165, 144);
            this.BTN_TEACH_JOG_STOP.Name = "BTN_TEACH_JOG_STOP";
            this.BTN_TEACH_JOG_STOP.Size = new System.Drawing.Size(85, 59);
            this.BTN_TEACH_JOG_STOP.TabIndex = 25;
            this.BTN_TEACH_JOG_STOP.Text = "STOP";
            this.BTN_TEACH_JOG_STOP.UseVisualStyleBackColor = false;
            this.BTN_TEACH_JOG_STOP.Click += new System.EventHandler(this.BTN_TEACH_JOG_STOP_Click);
            // 
            // BTN_TEACH_JOG_PLUS
            // 
            this.BTN_TEACH_JOG_PLUS.BackColor = System.Drawing.Color.PeachPuff;
            this.BTN_TEACH_JOG_PLUS.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_JOG_PLUS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_JOG_PLUS.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_JOG_PLUS.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_JOG_PLUS.Location = new System.Drawing.Point(251, 144);
            this.BTN_TEACH_JOG_PLUS.Name = "BTN_TEACH_JOG_PLUS";
            this.BTN_TEACH_JOG_PLUS.Size = new System.Drawing.Size(85, 59);
            this.BTN_TEACH_JOG_PLUS.TabIndex = 26;
            this.BTN_TEACH_JOG_PLUS.Text = "JOG +";
            this.BTN_TEACH_JOG_PLUS.UseVisualStyleBackColor = false;
            this.BTN_TEACH_JOG_PLUS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BTN_TEACH_JOG_PLUS_MouseDown);
            this.BTN_TEACH_JOG_PLUS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BTN_TEACH_JOG_PLUS_MouseUp);
            // 
            // BTN_TEACH_MOVE_MINUS
            // 
            this.BTN_TEACH_MOVE_MINUS.BackColor = System.Drawing.Color.PeachPuff;
            this.BTN_TEACH_MOVE_MINUS.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_MOVE_MINUS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_MOVE_MINUS.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_MOVE_MINUS.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_MOVE_MINUS.Location = new System.Drawing.Point(439, 42);
            this.BTN_TEACH_MOVE_MINUS.Name = "BTN_TEACH_MOVE_MINUS";
            this.BTN_TEACH_MOVE_MINUS.Size = new System.Drawing.Size(85, 59);
            this.BTN_TEACH_MOVE_MINUS.TabIndex = 27;
            this.BTN_TEACH_MOVE_MINUS.Text = "- MOVE";
            this.BTN_TEACH_MOVE_MINUS.UseVisualStyleBackColor = false;
            this.BTN_TEACH_MOVE_MINUS.Click += new System.EventHandler(this.BTN_TEACH_MOVE_MINUS_Click);
            // 
            // BTN_TEACH_MOVE_PLUS
            // 
            this.BTN_TEACH_MOVE_PLUS.BackColor = System.Drawing.Color.PeachPuff;
            this.BTN_TEACH_MOVE_PLUS.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_MOVE_PLUS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_MOVE_PLUS.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_MOVE_PLUS.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_MOVE_PLUS.Location = new System.Drawing.Point(613, 42);
            this.BTN_TEACH_MOVE_PLUS.Name = "BTN_TEACH_MOVE_PLUS";
            this.BTN_TEACH_MOVE_PLUS.Size = new System.Drawing.Size(85, 59);
            this.BTN_TEACH_MOVE_PLUS.TabIndex = 28;
            this.BTN_TEACH_MOVE_PLUS.Text = "+ MOVE";
            this.BTN_TEACH_MOVE_PLUS.UseVisualStyleBackColor = false;
            this.BTN_TEACH_MOVE_PLUS.Click += new System.EventHandler(this.BTN_TEACH_MOVE_PLUS_Click);
            // 
            // LABEL_TEACH_MOVE_VALUE
            // 
            this.LABEL_TEACH_MOVE_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.LABEL_TEACH_MOVE_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LABEL_TEACH_MOVE_VALUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LABEL_TEACH_MOVE_VALUE.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 15F, System.Drawing.FontStyle.Bold);
            this.LABEL_TEACH_MOVE_VALUE.ForeColor = System.Drawing.Color.DimGray;
            this.LABEL_TEACH_MOVE_VALUE.Location = new System.Drawing.Point(526, 42);
            this.LABEL_TEACH_MOVE_VALUE.Name = "LABEL_TEACH_MOVE_VALUE";
            this.LABEL_TEACH_MOVE_VALUE.Size = new System.Drawing.Size(86, 59);
            this.LABEL_TEACH_MOVE_VALUE.TabIndex = 29;
            this.LABEL_TEACH_MOVE_VALUE.Text = "2.0";
            this.LABEL_TEACH_MOVE_VALUE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LABEL_TEACH_MOVE_VALUE.Click += new System.EventHandler(this.LABEL_TEACH_MOVE_VALUE_Click);
            // 
            // BTN_TEACH_DATA_SAVE
            // 
            this.BTN_TEACH_DATA_SAVE.BackColor = System.Drawing.Color.PeachPuff;
            this.BTN_TEACH_DATA_SAVE.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_DATA_SAVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_DATA_SAVE.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_DATA_SAVE.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_DATA_SAVE.Location = new System.Drawing.Point(744, 144);
            this.BTN_TEACH_DATA_SAVE.Name = "BTN_TEACH_DATA_SAVE";
            this.BTN_TEACH_DATA_SAVE.Size = new System.Drawing.Size(140, 57);
            this.BTN_TEACH_DATA_SAVE.TabIndex = 30;
            this.BTN_TEACH_DATA_SAVE.Text = "SAVE";
            this.BTN_TEACH_DATA_SAVE.UseVisualStyleBackColor = false;
            this.BTN_TEACH_DATA_SAVE.Click += new System.EventHandler(this.BTN_TEACH_DATA_SAVE_Click);
            // 
            // TeachingTitleLabel
            // 
            this.TeachingTitleLabel.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TeachingTitleLabel.Location = new System.Drawing.Point(16, 16);
            this.TeachingTitleLabel.Name = "TeachingTitleLabel";
            this.TeachingTitleLabel.Size = new System.Drawing.Size(250, 42);
            this.TeachingTitleLabel.TabIndex = 31;
            this.TeachingTitleLabel.Text = "| TEACHING";
            this.TeachingTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TeachingPanel
            // 
            this.TeachingPanel.Location = new System.Drawing.Point(21, 97);
            this.TeachingPanel.Name = "TeachingPanel";
            this.TeachingPanel.Size = new System.Drawing.Size(934, 678);
            this.TeachingPanel.TabIndex = 32;
            // 
            // BTN_TEACH_LENS
            // 
            this.BTN_TEACH_LENS.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_LENS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_LENS.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_TEACH_LENS.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_LENS.Location = new System.Drawing.Point(777, 16);
            this.BTN_TEACH_LENS.Name = "BTN_TEACH_LENS";
            this.BTN_TEACH_LENS.Size = new System.Drawing.Size(154, 44);
            this.BTN_TEACH_LENS.TabIndex = 34;
            this.BTN_TEACH_LENS.Text = "LENS";
            this.BTN_TEACH_LENS.UseVisualStyleBackColor = false;
            this.BTN_TEACH_LENS.Click += new System.EventHandler(this.BTN_TEACH_LENS_Click);
            // 
            // BTN_TEACH_PCB
            // 
            this.BTN_TEACH_PCB.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_PCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_PCB.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_TEACH_PCB.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_PCB.Location = new System.Drawing.Point(616, 16);
            this.BTN_TEACH_PCB.Name = "BTN_TEACH_PCB";
            this.BTN_TEACH_PCB.Size = new System.Drawing.Size(154, 44);
            this.BTN_TEACH_PCB.TabIndex = 33;
            this.BTN_TEACH_PCB.Text = "PCB";
            this.BTN_TEACH_PCB.UseVisualStyleBackColor = false;
            this.BTN_TEACH_PCB.Click += new System.EventHandler(this.BTN_TEACH_PCB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.BTN_TEACH_SPEED_LOW);
            this.groupBox1.Controls.Add(this.BTN_TEACH_SPEED_MID);
            this.groupBox1.Controls.Add(this.BTN_TEACH_SPEED_HIGH);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BTN_TEACH_DATA_SAVE);
            this.groupBox1.Controls.Add(this.BTN_TEACH_JOG_MINUS);
            this.groupBox1.Controls.Add(this.LABEL_TEACH_MOVE_VALUE);
            this.groupBox1.Controls.Add(this.BTN_TEACH_JOG_STOP);
            this.groupBox1.Controls.Add(this.BTN_TEACH_MOVE_PLUS);
            this.groupBox1.Controls.Add(this.BTN_TEACH_JOG_PLUS);
            this.groupBox1.Controls.Add(this.BTN_TEACH_MOVE_MINUS);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(21, 784);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(908, 223);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // TeachingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BTN_TEACH_LENS);
            this.Controls.Add(this.BTN_TEACH_PCB);
            this.Controls.Add(this.TeachingPanel);
            this.Controls.Add(this.TeachingTitleLabel);
            this.Name = "TeachingControl";
            this.Size = new System.Drawing.Size(955, 1018);
            this.VisibleChanged += new System.EventHandler(this.TeachingControl_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BTN_TEACH_SPEED_LOW;
        private System.Windows.Forms.Button BTN_TEACH_SPEED_MID;
        private System.Windows.Forms.Button BTN_TEACH_SPEED_HIGH;
        private System.Windows.Forms.Button BTN_TEACH_JOG_MINUS;
        private System.Windows.Forms.Button BTN_TEACH_JOG_STOP;
        private System.Windows.Forms.Button BTN_TEACH_JOG_PLUS;
        private System.Windows.Forms.Button BTN_TEACH_MOVE_MINUS;
        private System.Windows.Forms.Button BTN_TEACH_MOVE_PLUS;
        public System.Windows.Forms.Label LABEL_TEACH_MOVE_VALUE;
        private System.Windows.Forms.Button BTN_TEACH_DATA_SAVE;
        private System.Windows.Forms.Label TeachingTitleLabel;
        private System.Windows.Forms.Panel TeachingPanel;
        private System.Windows.Forms.Button BTN_TEACH_LENS;
        private System.Windows.Forms.Button BTN_TEACH_PCB;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
