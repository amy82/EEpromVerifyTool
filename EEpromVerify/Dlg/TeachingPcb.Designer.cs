
namespace ApsMotionControl.Dlg
{
    partial class TeachingPcb
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
            this.groupTeachPcb = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PcbTeachGridView = new System.Windows.Forms.DataGridView();
            this.BTN_TEACH_PCB_Y = new System.Windows.Forms.Button();
            this.BTN_TEACH_PCB_TY = new System.Windows.Forms.Button();
            this.BTN_TEACH_SERVO_ON = new System.Windows.Forms.Button();
            this.BTN_TEACH_PCB_TX = new System.Windows.Forms.Button();
            this.BTN_TEACH_SERVO_OFF = new System.Windows.Forms.Button();
            this.BTN_TEACH_PCB_TH = new System.Windows.Forms.Button();
            this.BTN_TEACH_SERVO_RESET = new System.Windows.Forms.Button();
            this.BTN_TEACH_PCB_Z = new System.Windows.Forms.Button();
            this.BTN_TEACH_PCB_X = new System.Windows.Forms.Button();
            this.groupTeachPcb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PcbTeachGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupTeachPcb
            // 
            this.groupTeachPcb.BackColor = System.Drawing.Color.White;
            this.groupTeachPcb.Controls.Add(this.label4);
            this.groupTeachPcb.Controls.Add(this.label3);
            this.groupTeachPcb.Controls.Add(this.PcbTeachGridView);
            this.groupTeachPcb.Controls.Add(this.BTN_TEACH_PCB_Y);
            this.groupTeachPcb.Controls.Add(this.BTN_TEACH_PCB_TY);
            this.groupTeachPcb.Controls.Add(this.BTN_TEACH_SERVO_ON);
            this.groupTeachPcb.Controls.Add(this.BTN_TEACH_PCB_TX);
            this.groupTeachPcb.Controls.Add(this.BTN_TEACH_SERVO_OFF);
            this.groupTeachPcb.Controls.Add(this.BTN_TEACH_PCB_TH);
            this.groupTeachPcb.Controls.Add(this.BTN_TEACH_SERVO_RESET);
            this.groupTeachPcb.Controls.Add(this.BTN_TEACH_PCB_Z);
            this.groupTeachPcb.Controls.Add(this.BTN_TEACH_PCB_X);
            this.groupTeachPcb.Location = new System.Drawing.Point(0, 3);
            this.groupTeachPcb.Name = "groupTeachPcb";
            this.groupTeachPcb.Size = new System.Drawing.Size(908, 670);
            this.groupTeachPcb.TabIndex = 45;
            this.groupTeachPcb.TabStop = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(17, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 23);
            this.label4.TabIndex = 33;
            this.label4.Text = "MOTOR SELECT";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(15, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 23);
            this.label3.TabIndex = 32;
            this.label3.Text = "MOTOR SET";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PcbTeachGridView
            // 
            this.PcbTeachGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PcbTeachGridView.Location = new System.Drawing.Point(185, 28);
            this.PcbTeachGridView.Name = "PcbTeachGridView";
            this.PcbTeachGridView.RowTemplate.Height = 23;
            this.PcbTeachGridView.Size = new System.Drawing.Size(240, 150);
            this.PcbTeachGridView.TabIndex = 28;
            // 
            // BTN_TEACH_PCB_Y
            // 
            this.BTN_TEACH_PCB_Y.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_PCB_Y.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_PCB_Y.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_PCB_Y.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_PCB_Y.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_PCB_Y.Location = new System.Drawing.Point(17, 282);
            this.BTN_TEACH_PCB_Y.Name = "BTN_TEACH_PCB_Y";
            this.BTN_TEACH_PCB_Y.Size = new System.Drawing.Size(138, 47);
            this.BTN_TEACH_PCB_Y.TabIndex = 23;
            this.BTN_TEACH_PCB_Y.Text = "PCB Y";
            this.BTN_TEACH_PCB_Y.UseVisualStyleBackColor = false;
            this.BTN_TEACH_PCB_Y.Click += new System.EventHandler(this.BTN_TEACH_PCB_Y_Click);
            // 
            // BTN_TEACH_PCB_TY
            // 
            this.BTN_TEACH_PCB_TY.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_PCB_TY.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_PCB_TY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_PCB_TY.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_PCB_TY.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_PCB_TY.Location = new System.Drawing.Point(17, 474);
            this.BTN_TEACH_PCB_TY.Name = "BTN_TEACH_PCB_TY";
            this.BTN_TEACH_PCB_TY.Size = new System.Drawing.Size(138, 47);
            this.BTN_TEACH_PCB_TY.TabIndex = 27;
            this.BTN_TEACH_PCB_TY.Text = "PCB TY";
            this.BTN_TEACH_PCB_TY.UseVisualStyleBackColor = false;
            this.BTN_TEACH_PCB_TY.Click += new System.EventHandler(this.BTN_TEACH_PCB_TY_Click);
            // 
            // BTN_TEACH_SERVO_ON
            // 
            this.BTN_TEACH_SERVO_ON.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_SERVO_ON.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_SERVO_ON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_SERVO_ON.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_SERVO_ON.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_SERVO_ON.Location = new System.Drawing.Point(15, 54);
            this.BTN_TEACH_SERVO_ON.Name = "BTN_TEACH_SERVO_ON";
            this.BTN_TEACH_SERVO_ON.Size = new System.Drawing.Size(138, 45);
            this.BTN_TEACH_SERVO_ON.TabIndex = 18;
            this.BTN_TEACH_SERVO_ON.Text = "SERVO ON";
            this.BTN_TEACH_SERVO_ON.UseVisualStyleBackColor = false;
            this.BTN_TEACH_SERVO_ON.Click += new System.EventHandler(this.BTN_TEACH_SERVO_ON_Click);
            // 
            // BTN_TEACH_PCB_TX
            // 
            this.BTN_TEACH_PCB_TX.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_PCB_TX.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_PCB_TX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_PCB_TX.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_PCB_TX.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_PCB_TX.Location = new System.Drawing.Point(17, 426);
            this.BTN_TEACH_PCB_TX.Name = "BTN_TEACH_PCB_TX";
            this.BTN_TEACH_PCB_TX.Size = new System.Drawing.Size(138, 47);
            this.BTN_TEACH_PCB_TX.TabIndex = 26;
            this.BTN_TEACH_PCB_TX.Text = "PCB TX";
            this.BTN_TEACH_PCB_TX.UseVisualStyleBackColor = false;
            this.BTN_TEACH_PCB_TX.Click += new System.EventHandler(this.BTN_TEACH_PCB_TX_Click);
            // 
            // BTN_TEACH_SERVO_OFF
            // 
            this.BTN_TEACH_SERVO_OFF.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_SERVO_OFF.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_SERVO_OFF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_SERVO_OFF.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_SERVO_OFF.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_SERVO_OFF.Location = new System.Drawing.Point(15, 100);
            this.BTN_TEACH_SERVO_OFF.Name = "BTN_TEACH_SERVO_OFF";
            this.BTN_TEACH_SERVO_OFF.Size = new System.Drawing.Size(138, 45);
            this.BTN_TEACH_SERVO_OFF.TabIndex = 19;
            this.BTN_TEACH_SERVO_OFF.Text = "SERVO OFF";
            this.BTN_TEACH_SERVO_OFF.UseVisualStyleBackColor = false;
            this.BTN_TEACH_SERVO_OFF.Click += new System.EventHandler(this.BTN_TEACH_SERVO_OFF_Click);
            // 
            // BTN_TEACH_PCB_TH
            // 
            this.BTN_TEACH_PCB_TH.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_PCB_TH.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_PCB_TH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_PCB_TH.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_PCB_TH.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_PCB_TH.Location = new System.Drawing.Point(17, 378);
            this.BTN_TEACH_PCB_TH.Name = "BTN_TEACH_PCB_TH";
            this.BTN_TEACH_PCB_TH.Size = new System.Drawing.Size(138, 47);
            this.BTN_TEACH_PCB_TH.TabIndex = 25;
            this.BTN_TEACH_PCB_TH.Text = "PCB TH";
            this.BTN_TEACH_PCB_TH.UseVisualStyleBackColor = false;
            this.BTN_TEACH_PCB_TH.Click += new System.EventHandler(this.BTN_TEACH_PCB_TH_Click);
            // 
            // BTN_TEACH_SERVO_RESET
            // 
            this.BTN_TEACH_SERVO_RESET.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_SERVO_RESET.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_SERVO_RESET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_SERVO_RESET.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_SERVO_RESET.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_SERVO_RESET.Location = new System.Drawing.Point(15, 146);
            this.BTN_TEACH_SERVO_RESET.Name = "BTN_TEACH_SERVO_RESET";
            this.BTN_TEACH_SERVO_RESET.Size = new System.Drawing.Size(138, 45);
            this.BTN_TEACH_SERVO_RESET.TabIndex = 20;
            this.BTN_TEACH_SERVO_RESET.Text = "SERVO RESET";
            this.BTN_TEACH_SERVO_RESET.UseVisualStyleBackColor = false;
            this.BTN_TEACH_SERVO_RESET.Click += new System.EventHandler(this.BTN_TEACH_SERVO_RESET_Click);
            // 
            // BTN_TEACH_PCB_Z
            // 
            this.BTN_TEACH_PCB_Z.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_PCB_Z.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_PCB_Z.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_PCB_Z.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_PCB_Z.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_PCB_Z.Location = new System.Drawing.Point(17, 330);
            this.BTN_TEACH_PCB_Z.Name = "BTN_TEACH_PCB_Z";
            this.BTN_TEACH_PCB_Z.Size = new System.Drawing.Size(138, 47);
            this.BTN_TEACH_PCB_Z.TabIndex = 24;
            this.BTN_TEACH_PCB_Z.Text = "PCB Z";
            this.BTN_TEACH_PCB_Z.UseVisualStyleBackColor = false;
            this.BTN_TEACH_PCB_Z.Click += new System.EventHandler(this.BTN_TEACH_PCB_Z_Click);
            // 
            // BTN_TEACH_PCB_X
            // 
            this.BTN_TEACH_PCB_X.BackColor = System.Drawing.Color.Tan;
            this.BTN_TEACH_PCB_X.FlatAppearance.BorderSize = 0;
            this.BTN_TEACH_PCB_X.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TEACH_PCB_X.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11F, System.Drawing.FontStyle.Bold);
            this.BTN_TEACH_PCB_X.ForeColor = System.Drawing.Color.White;
            this.BTN_TEACH_PCB_X.Location = new System.Drawing.Point(17, 234);
            this.BTN_TEACH_PCB_X.Name = "BTN_TEACH_PCB_X";
            this.BTN_TEACH_PCB_X.Size = new System.Drawing.Size(138, 47);
            this.BTN_TEACH_PCB_X.TabIndex = 22;
            this.BTN_TEACH_PCB_X.Text = "PCB X";
            this.BTN_TEACH_PCB_X.UseVisualStyleBackColor = false;
            this.BTN_TEACH_PCB_X.Click += new System.EventHandler(this.BTN_TEACH_PCB_X_Click);
            // 
            // TeachingPcb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupTeachPcb);
            this.Name = "TeachingPcb";
            this.Size = new System.Drawing.Size(952, 724);
            this.groupTeachPcb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PcbTeachGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupTeachPcb;
        private System.Windows.Forms.Button BTN_TEACH_PCB_Y;
        private System.Windows.Forms.Button BTN_TEACH_PCB_TY;
        private System.Windows.Forms.Button BTN_TEACH_SERVO_ON;
        private System.Windows.Forms.Button BTN_TEACH_PCB_TX;
        private System.Windows.Forms.Button BTN_TEACH_SERVO_OFF;
        private System.Windows.Forms.Button BTN_TEACH_PCB_TH;
        private System.Windows.Forms.Button BTN_TEACH_SERVO_RESET;
        private System.Windows.Forms.Button BTN_TEACH_PCB_Z;
        private System.Windows.Forms.Button BTN_TEACH_PCB_X;
        private System.Windows.Forms.DataGridView PcbTeachGridView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}
