namespace ApsMotionControl.Dlg
{
    partial class CamControl
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
            this.CamPanel = new System.Windows.Forms.Panel();
            this.BTN_CAM_VIEW_CCD = new System.Windows.Forms.Button();
            this.BTN_CAM_VIEW_CAM = new System.Windows.Forms.Button();
            this.CcdPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // CamPanel
            // 
            this.CamPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.CamPanel.Location = new System.Drawing.Point(7, 51);
            this.CamPanel.Name = "CamPanel";
            this.CamPanel.Size = new System.Drawing.Size(354, 260);
            this.CamPanel.TabIndex = 7;
            this.CamPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CamPanel_MouseDown);
            this.CamPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CamPanel_MouseMove);
            this.CamPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CamPanel_MouseUp);
            // 
            // BTN_CAM_VIEW_CCD
            // 
            this.BTN_CAM_VIEW_CCD.Location = new System.Drawing.Point(720, 3);
            this.BTN_CAM_VIEW_CCD.Name = "BTN_CAM_VIEW_CCD";
            this.BTN_CAM_VIEW_CCD.Size = new System.Drawing.Size(56, 38);
            this.BTN_CAM_VIEW_CCD.TabIndex = 8;
            this.BTN_CAM_VIEW_CCD.Text = "CCD";
            this.BTN_CAM_VIEW_CCD.UseVisualStyleBackColor = true;
            this.BTN_CAM_VIEW_CCD.Click += new System.EventHandler(this.BTN_CAM_VIEW_CCD_Click);
            // 
            // BTN_CAM_VIEW_CAM
            // 
            this.BTN_CAM_VIEW_CAM.Location = new System.Drawing.Point(720, 47);
            this.BTN_CAM_VIEW_CAM.Name = "BTN_CAM_VIEW_CAM";
            this.BTN_CAM_VIEW_CAM.Size = new System.Drawing.Size(56, 38);
            this.BTN_CAM_VIEW_CAM.TabIndex = 9;
            this.BTN_CAM_VIEW_CAM.Text = "CAM";
            this.BTN_CAM_VIEW_CAM.UseVisualStyleBackColor = true;
            this.BTN_CAM_VIEW_CAM.Click += new System.EventHandler(this.BTN_CAM_VIEW_CAM_Click);
            // 
            // CcdPanel
            // 
            this.CcdPanel.BackColor = System.Drawing.Color.Red;
            this.CcdPanel.Location = new System.Drawing.Point(0, 0);
            this.CcdPanel.Name = "CcdPanel";
            this.CcdPanel.Size = new System.Drawing.Size(699, 372);
            this.CcdPanel.TabIndex = 8;
            this.CcdPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CcdPanel_MouseDown);
            this.CcdPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CcdPanel_MouseMove);
            this.CcdPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CcdPanel_MouseUp);
            // 
            // CamControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.BTN_CAM_VIEW_CAM);
            this.Controls.Add(this.BTN_CAM_VIEW_CCD);
            this.Controls.Add(this.CamPanel);
            this.Controls.Add(this.CcdPanel);
            this.Name = "CamControl";
            this.Size = new System.Drawing.Size(779, 415);
            this.ResumeLayout(false);

        }

        #endregion
        //private System.Windows.Forms.DataVisualization.Charting.Chart FirstChart;
        //private System.Windows.Forms.DataVisualization.Charting.Chart SecondChart;
        //private System.Windows.Forms.DataVisualization.Charting.Chart BarChart;
        private System.Windows.Forms.Panel CamPanel;
        private System.Windows.Forms.Button BTN_CAM_VIEW_CCD;
        private System.Windows.Forms.Button BTN_CAM_VIEW_CAM;
        private System.Windows.Forms.Panel CcdPanel;
    }
}
