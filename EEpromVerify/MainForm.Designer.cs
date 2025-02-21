
namespace ApsMotionControl
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopPanel = new System.Windows.Forms.Panel();
            this.BTN_TOP_MES = new System.Windows.Forms.Button();
            this.MainTitlepictureBox = new System.Windows.Forms.PictureBox();
            this.MainTitleLabel = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.panel_ProductionInfo = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_TopLot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_production_total = new System.Windows.Forms.Label();
            this.label_production_ng = new System.Windows.Forms.Label();
            this.label_production_ok = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_LogTitle = new System.Windows.Forms.Label();
            this.listBox_Log = new System.Windows.Forms.ListBox();
            this.labelGuide = new System.Windows.Forms.Label();
            this.BTN_MAIN_START1 = new System.Windows.Forms.Button();
            this.BTN_MAIN_STOP1 = new System.Windows.Forms.Button();
            this.BTN_MAIN_PAUSE1 = new System.Windows.Forms.Button();
            this.BTN_MAIN_READY1 = new System.Windows.Forms.Button();
            this.BTN_MAIN_ORIGIN1 = new System.Windows.Forms.Button();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.BTN_BOTTOM_LOG = new System.Windows.Forms.Button();
            this.label_build = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.BTN_BOTTOM_WALLPAPER = new System.Windows.Forms.Button();
            this.BTN_BOTTOM_EXIT = new System.Windows.Forms.Button();
            this.BTN_BOTTOM_ALARM = new System.Windows.Forms.Button();
            this.BTN_BOTTOM_LIGHT = new System.Windows.Forms.Button();
            this.BTN_TOP_LOG = new System.Windows.Forms.Button();
            this.BTN_BOTTOM_IO = new System.Windows.Forms.Button();
            this.BTN_BOTTOM_CCD = new System.Windows.Forms.Button();
            this.BTN_BOTTOM_TEACH = new System.Windows.Forms.Button();
            this.BTN_BOTTOM_MAIN = new System.Windows.Forms.Button();
            this.BTN_BOTTOM_SETUP = new System.Windows.Forms.Button();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainTitlepictureBox)).BeginInit();
            this.LeftPanel.SuspendLayout();
            this.panel_ProductionInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.SetColumnSpan(this.TopPanel, 2);
            this.TopPanel.Controls.Add(this.BTN_TOP_MES);
            this.TopPanel.Controls.Add(this.MainTitlepictureBox);
            this.TopPanel.Controls.Add(this.MainTitleLabel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1331, 60);
            this.TopPanel.TabIndex = 0;
            // 
            // BTN_TOP_MES
            // 
            this.BTN_TOP_MES.BackColor = System.Drawing.Color.White;
            this.BTN_TOP_MES.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.BTN_TOP_MES.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TOP_MES.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_TOP_MES.Location = new System.Drawing.Point(700, 4);
            this.BTN_TOP_MES.Name = "BTN_TOP_MES";
            this.BTN_TOP_MES.Size = new System.Drawing.Size(73, 53);
            this.BTN_TOP_MES.TabIndex = 11;
            this.BTN_TOP_MES.Text = "MES";
            this.BTN_TOP_MES.UseVisualStyleBackColor = false;
            this.BTN_TOP_MES.Click += new System.EventHandler(this.BTN_TOP_MES_Click);
            // 
            // MainTitlepictureBox
            // 
            this.MainTitlepictureBox.Image = global::ApsMotionControl.Properties.Resources.mainTitle;
            this.MainTitlepictureBox.Location = new System.Drawing.Point(19, 10);
            this.MainTitlepictureBox.Name = "MainTitlepictureBox";
            this.MainTitlepictureBox.Size = new System.Drawing.Size(43, 36);
            this.MainTitlepictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MainTitlepictureBox.TabIndex = 10;
            this.MainTitlepictureBox.TabStop = false;
            // 
            // MainTitleLabel
            // 
            this.MainTitleLabel.AutoSize = true;
            this.MainTitleLabel.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MainTitleLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.MainTitleLabel.Location = new System.Drawing.Point(74, 14);
            this.MainTitleLabel.Name = "MainTitleLabel";
            this.MainTitleLabel.Size = new System.Drawing.Size(244, 26);
            this.MainTitleLabel.TabIndex = 0;
            this.MainTitleLabel.Text = "EEPROM VERIFY TEST";
            // 
            // TimeLabel
            // 
            this.TimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TimeLabel.ForeColor = System.Drawing.SystemColors.Info;
            this.TimeLabel.Location = new System.Drawing.Point(3, 739);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(131, 40);
            this.TimeLabel.TabIndex = 9;
            this.TimeLabel.Text = "00 : 00 : 00";
            this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.Black;
            this.LeftPanel.Controls.Add(this.panel_ProductionInfo);
            this.LeftPanel.Controls.Add(this.labelGuide);
            this.LeftPanel.Controls.Add(this.BTN_MAIN_START1);
            this.LeftPanel.Controls.Add(this.BTN_MAIN_STOP1);
            this.LeftPanel.Controls.Add(this.BTN_MAIN_PAUSE1);
            this.LeftPanel.Controls.Add(this.BTN_MAIN_READY1);
            this.LeftPanel.Controls.Add(this.BTN_MAIN_ORIGIN1);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftPanel.Location = new System.Drawing.Point(0, 60);
            this.LeftPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(700, 740);
            this.LeftPanel.TabIndex = 1;
            // 
            // panel_ProductionInfo
            // 
            this.panel_ProductionInfo.BackColor = System.Drawing.Color.White;
            this.panel_ProductionInfo.Controls.Add(this.groupBox1);
            this.panel_ProductionInfo.Controls.Add(this.label_LogTitle);
            this.panel_ProductionInfo.Controls.Add(this.listBox_Log);
            this.panel_ProductionInfo.Location = new System.Drawing.Point(3, 361);
            this.panel_ProductionInfo.Name = "panel_ProductionInfo";
            this.panel_ProductionInfo.Size = new System.Drawing.Size(694, 367);
            this.panel_ProductionInfo.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_TopLot);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label_production_total);
            this.groupBox1.Controls.Add(this.label_production_ng);
            this.groupBox1.Controls.Add(this.label_production_ok);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(4, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 252);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " PRODUCTION INFO";
            // 
            // textBox_TopLot
            // 
            this.textBox_TopLot.BackColor = System.Drawing.Color.White;
            this.textBox_TopLot.Font = new System.Drawing.Font("나눔고딕", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_TopLot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox_TopLot.Location = new System.Drawing.Point(9, 41);
            this.textBox_TopLot.Name = "textBox_TopLot";
            this.textBox_TopLot.ReadOnly = true;
            this.textBox_TopLot.Size = new System.Drawing.Size(480, 29);
            this.textBox_TopLot.TabIndex = 15;
            this.textBox_TopLot.Text = "0000000000";
            this.textBox_TopLot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 8.999999F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "BARCODE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_production_total
            // 
            this.label_production_total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_production_total.Location = new System.Drawing.Point(572, 77);
            this.label_production_total.Name = "label_production_total";
            this.label_production_total.Size = new System.Drawing.Size(107, 22);
            this.label_production_total.TabIndex = 5;
            this.label_production_total.Text = "0";
            this.label_production_total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_production_ng
            // 
            this.label_production_ng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_production_ng.Location = new System.Drawing.Point(572, 51);
            this.label_production_ng.Name = "label_production_ng";
            this.label_production_ng.Size = new System.Drawing.Size(107, 22);
            this.label_production_ng.TabIndex = 4;
            this.label_production_ng.Text = "0";
            this.label_production_ng.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_production_ok
            // 
            this.label_production_ok.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_production_ok.Location = new System.Drawing.Point(572, 25);
            this.label_production_ok.Name = "label_production_ok";
            this.label_production_ok.Size = new System.Drawing.Size(107, 22);
            this.label_production_ok.TabIndex = 3;
            this.label_production_ok.Text = "0";
            this.label_production_ok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(510, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "TOTAL :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(510, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "NG :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(510, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "OK :";
            // 
            // label_LogTitle
            // 
            this.label_LogTitle.AutoSize = true;
            this.label_LogTitle.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_LogTitle.Location = new System.Drawing.Point(9, 270);
            this.label_LogTitle.Name = "label_LogTitle";
            this.label_LogTitle.Size = new System.Drawing.Size(68, 14);
            this.label_LogTitle.TabIndex = 0;
            this.label_LogTitle.Text = " LOG VIEW";
            // 
            // listBox_Log
            // 
            this.listBox_Log.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listBox_Log.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox_Log.FormattingEnabled = true;
            this.listBox_Log.ItemHeight = 15;
            this.listBox_Log.Location = new System.Drawing.Point(29, 302);
            this.listBox_Log.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_Log.Name = "listBox_Log";
            this.listBox_Log.Size = new System.Drawing.Size(431, 49);
            this.listBox_Log.TabIndex = 0;
            // 
            // labelGuide
            // 
            this.labelGuide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelGuide.BackColor = System.Drawing.Color.Bisque;
            this.labelGuide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelGuide.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelGuide.ForeColor = System.Drawing.Color.Black;
            this.labelGuide.Location = new System.Drawing.Point(456, 279);
            this.labelGuide.Name = "labelGuide";
            this.labelGuide.Size = new System.Drawing.Size(236, 57);
            this.labelGuide.TabIndex = 6;
            this.labelGuide.Text = "설비 정지 상태입니다.";
            this.labelGuide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_MAIN_START1
            // 
            this.BTN_MAIN_START1.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_START1.FlatAppearance.BorderSize = 0;
            this.BTN_MAIN_START1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_START1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.BTN_MAIN_START1.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_START1.Location = new System.Drawing.Point(270, 279);
            this.BTN_MAIN_START1.Name = "BTN_MAIN_START1";
            this.BTN_MAIN_START1.Size = new System.Drawing.Size(180, 57);
            this.BTN_MAIN_START1.TabIndex = 5;
            this.BTN_MAIN_START1.Text = "EEPROM START";
            this.BTN_MAIN_START1.UseVisualStyleBackColor = false;
            this.BTN_MAIN_START1.Click += new System.EventHandler(this.BTN_MAIN_START1_Click);
            // 
            // BTN_MAIN_STOP1
            // 
            this.BTN_MAIN_STOP1.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_STOP1.FlatAppearance.BorderSize = 0;
            this.BTN_MAIN_STOP1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_STOP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.BTN_MAIN_STOP1.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_STOP1.Location = new System.Drawing.Point(138, 279);
            this.BTN_MAIN_STOP1.Name = "BTN_MAIN_STOP1";
            this.BTN_MAIN_STOP1.Size = new System.Drawing.Size(126, 57);
            this.BTN_MAIN_STOP1.TabIndex = 4;
            this.BTN_MAIN_STOP1.Text = "STOP";
            this.BTN_MAIN_STOP1.UseVisualStyleBackColor = false;
            this.BTN_MAIN_STOP1.Click += new System.EventHandler(this.BTN_MAIN_STOP1_Click);
            // 
            // BTN_MAIN_PAUSE1
            // 
            this.BTN_MAIN_PAUSE1.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_PAUSE1.FlatAppearance.BorderSize = 0;
            this.BTN_MAIN_PAUSE1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_PAUSE1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.BTN_MAIN_PAUSE1.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_PAUSE1.Location = new System.Drawing.Point(6, 279);
            this.BTN_MAIN_PAUSE1.Name = "BTN_MAIN_PAUSE1";
            this.BTN_MAIN_PAUSE1.Size = new System.Drawing.Size(126, 57);
            this.BTN_MAIN_PAUSE1.TabIndex = 3;
            this.BTN_MAIN_PAUSE1.Text = "PAUSE";
            this.BTN_MAIN_PAUSE1.UseVisualStyleBackColor = false;
            this.BTN_MAIN_PAUSE1.Click += new System.EventHandler(this.BTN_MAIN_PAUSE1_Click);
            // 
            // BTN_MAIN_READY1
            // 
            this.BTN_MAIN_READY1.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_READY1.FlatAppearance.BorderSize = 0;
            this.BTN_MAIN_READY1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_READY1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.BTN_MAIN_READY1.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_READY1.Location = new System.Drawing.Point(49, 196);
            this.BTN_MAIN_READY1.Name = "BTN_MAIN_READY1";
            this.BTN_MAIN_READY1.Size = new System.Drawing.Size(126, 57);
            this.BTN_MAIN_READY1.TabIndex = 2;
            this.BTN_MAIN_READY1.Text = "READY";
            this.BTN_MAIN_READY1.UseVisualStyleBackColor = false;
            this.BTN_MAIN_READY1.Click += new System.EventHandler(this.BTN_MAIN_READY1_Click);
            // 
            // BTN_MAIN_ORIGIN1
            // 
            this.BTN_MAIN_ORIGIN1.BackColor = System.Drawing.Color.Orange;
            this.BTN_MAIN_ORIGIN1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BTN_MAIN_ORIGIN1.FlatAppearance.BorderSize = 0;
            this.BTN_MAIN_ORIGIN1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_ORIGIN1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_MAIN_ORIGIN1.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_ORIGIN1.Location = new System.Drawing.Point(17, 196);
            this.BTN_MAIN_ORIGIN1.Name = "BTN_MAIN_ORIGIN1";
            this.BTN_MAIN_ORIGIN1.Size = new System.Drawing.Size(126, 57);
            this.BTN_MAIN_ORIGIN1.TabIndex = 1;
            this.BTN_MAIN_ORIGIN1.Text = "ORIGIN";
            this.BTN_MAIN_ORIGIN1.UseVisualStyleBackColor = false;
            this.BTN_MAIN_ORIGIN1.Click += new System.EventHandler(this.BTN_MAIN_ORIGIN1_Click);
            // 
            // RightPanel
            // 
            this.RightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightPanel.Location = new System.Drawing.Point(700, 60);
            this.RightPanel.Margin = new System.Windows.Forms.Padding(0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(631, 740);
            this.RightPanel.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 700F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.TopPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LeftPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.RightPanel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.BottomPanel, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1481, 800);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // BottomPanel
            // 
            this.BottomPanel.BackColor = System.Drawing.Color.Pink;
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_LOG);
            this.BottomPanel.Controls.Add(this.label_build);
            this.BottomPanel.Controls.Add(this.label_version);
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_WALLPAPER);
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_EXIT);
            this.BottomPanel.Controls.Add(this.TimeLabel);
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_ALARM);
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_LIGHT);
            this.BottomPanel.Controls.Add(this.BTN_TOP_LOG);
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_IO);
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_CCD);
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_TEACH);
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_MAIN);
            this.BottomPanel.Controls.Add(this.BTN_BOTTOM_SETUP);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomPanel.Location = new System.Drawing.Point(1331, 0);
            this.BottomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.BottomPanel.Name = "BottomPanel";
            this.tableLayoutPanel1.SetRowSpan(this.BottomPanel, 2);
            this.BottomPanel.Size = new System.Drawing.Size(150, 800);
            this.BottomPanel.TabIndex = 3;
            // 
            // BTN_BOTTOM_LOG
            // 
            this.BTN_BOTTOM_LOG.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_LOG.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_LOG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_LOG.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_LOG.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_LOG.Image = global::ApsMotionControl.Properties.Resources.log;
            this.BTN_BOTTOM_LOG.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_LOG.Location = new System.Drawing.Point(8, 241);
            this.BTN_BOTTOM_LOG.Name = "BTN_BOTTOM_LOG";
            this.BTN_BOTTOM_LOG.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_LOG.TabIndex = 13;
            this.BTN_BOTTOM_LOG.Text = "  LOG";
            this.BTN_BOTTOM_LOG.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_LOG.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_LOG.Click += new System.EventHandler(this.BTN_BOTTOM_LOG_Click);
            // 
            // label_build
            // 
            this.label_build.BackColor = System.Drawing.Color.Transparent;
            this.label_build.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_build.ForeColor = System.Drawing.Color.DimGray;
            this.label_build.Location = new System.Drawing.Point(4, 699);
            this.label_build.Name = "label_build";
            this.label_build.Size = new System.Drawing.Size(144, 42);
            this.label_build.TabIndex = 12;
            this.label_build.Text = "buildInfo : 25.02.11.01";
            this.label_build.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_version
            // 
            this.label_version.BackColor = System.Drawing.Color.Transparent;
            this.label_version.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_version.ForeColor = System.Drawing.Color.DimGray;
            this.label_version.Location = new System.Drawing.Point(4, 645);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(144, 42);
            this.label_version.TabIndex = 11;
            this.label_version.Text = "VersionInfo : V1.1.0";
            this.label_version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BTN_BOTTOM_WALLPAPER
            // 
            this.BTN_BOTTOM_WALLPAPER.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_WALLPAPER.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_WALLPAPER.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_WALLPAPER.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_WALLPAPER.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_WALLPAPER.Image = global::ApsMotionControl.Properties.Resources.Desktop;
            this.BTN_BOTTOM_WALLPAPER.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_WALLPAPER.Location = new System.Drawing.Point(8, 297);
            this.BTN_BOTTOM_WALLPAPER.Name = "BTN_BOTTOM_WALLPAPER";
            this.BTN_BOTTOM_WALLPAPER.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_WALLPAPER.TabIndex = 10;
            this.BTN_BOTTOM_WALLPAPER.Text = "  DESKTOP";
            this.BTN_BOTTOM_WALLPAPER.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_WALLPAPER.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_WALLPAPER.Click += new System.EventHandler(this.BTN_BOTTOM_WALLPAPER_Click);
            // 
            // BTN_BOTTOM_EXIT
            // 
            this.BTN_BOTTOM_EXIT.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_EXIT.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_EXIT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_EXIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_EXIT.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_EXIT.Image = global::ApsMotionControl.Properties.Resources.Exit;
            this.BTN_BOTTOM_EXIT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_EXIT.Location = new System.Drawing.Point(8, 339);
            this.BTN_BOTTOM_EXIT.Name = "BTN_BOTTOM_EXIT";
            this.BTN_BOTTOM_EXIT.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_EXIT.TabIndex = 8;
            this.BTN_BOTTOM_EXIT.Text = "  EXIT";
            this.BTN_BOTTOM_EXIT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_EXIT.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_EXIT.Click += new System.EventHandler(this.BTN_BOTTOM_EXIT_Click);
            // 
            // BTN_BOTTOM_ALARM
            // 
            this.BTN_BOTTOM_ALARM.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_ALARM.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_ALARM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_ALARM.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_ALARM.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_ALARM.Image = global::ApsMotionControl.Properties.Resources.Alarm;
            this.BTN_BOTTOM_ALARM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_ALARM.Location = new System.Drawing.Point(7, 189);
            this.BTN_BOTTOM_ALARM.Name = "BTN_BOTTOM_ALARM";
            this.BTN_BOTTOM_ALARM.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_ALARM.TabIndex = 7;
            this.BTN_BOTTOM_ALARM.Text = "  ALARM";
            this.BTN_BOTTOM_ALARM.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_ALARM.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_ALARM.Click += new System.EventHandler(this.BTN_BOTTOM_ALARM_Click);
            // 
            // BTN_BOTTOM_LIGHT
            // 
            this.BTN_BOTTOM_LIGHT.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_LIGHT.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_LIGHT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_LIGHT.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_LIGHT.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_LIGHT.Image = global::ApsMotionControl.Properties.Resources.Light;
            this.BTN_BOTTOM_LIGHT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_LIGHT.Location = new System.Drawing.Point(6, 465);
            this.BTN_BOTTOM_LIGHT.Name = "BTN_BOTTOM_LIGHT";
            this.BTN_BOTTOM_LIGHT.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_LIGHT.TabIndex = 6;
            this.BTN_BOTTOM_LIGHT.Text = "  LIGHT";
            this.BTN_BOTTOM_LIGHT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_LIGHT.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_LIGHT.Click += new System.EventHandler(this.BTN_BOTTOM_LIGHT_Click);
            // 
            // BTN_TOP_LOG
            // 
            this.BTN_TOP_LOG.BackColor = System.Drawing.Color.LightPink;
            this.BTN_TOP_LOG.FlatAppearance.BorderSize = 0;
            this.BTN_TOP_LOG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TOP_LOG.Font = new System.Drawing.Font("Nirmala UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_TOP_LOG.ForeColor = System.Drawing.Color.White;
            this.BTN_TOP_LOG.Location = new System.Drawing.Point(0, 0);
            this.BTN_TOP_LOG.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_TOP_LOG.Name = "BTN_TOP_LOG";
            this.BTN_TOP_LOG.Size = new System.Drawing.Size(140, 60);
            this.BTN_TOP_LOG.TabIndex = 0;
            this.BTN_TOP_LOG.Text = "LGIT";
            this.BTN_TOP_LOG.UseVisualStyleBackColor = false;
            this.BTN_TOP_LOG.Click += new System.EventHandler(this.BTN_TOP_LOG_Click);
            // 
            // BTN_BOTTOM_IO
            // 
            this.BTN_BOTTOM_IO.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_IO.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_IO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_IO.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_IO.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_IO.Image = global::ApsMotionControl.Properties.Resources.Io;
            this.BTN_BOTTOM_IO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_IO.Location = new System.Drawing.Point(6, 488);
            this.BTN_BOTTOM_IO.Name = "BTN_BOTTOM_IO";
            this.BTN_BOTTOM_IO.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_IO.TabIndex = 5;
            this.BTN_BOTTOM_IO.Text = "  IO";
            this.BTN_BOTTOM_IO.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_IO.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_IO.Click += new System.EventHandler(this.BTN_BOTTOM_IO_Click);
            // 
            // BTN_BOTTOM_CCD
            // 
            this.BTN_BOTTOM_CCD.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_CCD.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_CCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_CCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_CCD.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_CCD.Image = global::ApsMotionControl.Properties.Resources.Ccd;
            this.BTN_BOTTOM_CCD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_CCD.Location = new System.Drawing.Point(7, 105);
            this.BTN_BOTTOM_CCD.Name = "BTN_BOTTOM_CCD";
            this.BTN_BOTTOM_CCD.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_CCD.TabIndex = 4;
            this.BTN_BOTTOM_CCD.Text = "  CCD";
            this.BTN_BOTTOM_CCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_CCD.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_CCD.Click += new System.EventHandler(this.BTN_BOTTOM_CCD_Click);
            // 
            // BTN_BOTTOM_TEACH
            // 
            this.BTN_BOTTOM_TEACH.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_TEACH.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_TEACH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_TEACH.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_TEACH.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_TEACH.Image = global::ApsMotionControl.Properties.Resources.Teaching1;
            this.BTN_BOTTOM_TEACH.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_TEACH.Location = new System.Drawing.Point(5, 511);
            this.BTN_BOTTOM_TEACH.Name = "BTN_BOTTOM_TEACH";
            this.BTN_BOTTOM_TEACH.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_TEACH.TabIndex = 3;
            this.BTN_BOTTOM_TEACH.Text = "  TEACHING";
            this.BTN_BOTTOM_TEACH.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_TEACH.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_TEACH.Click += new System.EventHandler(this.BTN_BOTTOM_TEACH_Click);
            // 
            // BTN_BOTTOM_MAIN
            // 
            this.BTN_BOTTOM_MAIN.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_MAIN.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_MAIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_MAIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_MAIN.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_MAIN.Image = global::ApsMotionControl.Properties.Resources.Manual;
            this.BTN_BOTTOM_MAIN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_MAIN.Location = new System.Drawing.Point(7, 63);
            this.BTN_BOTTOM_MAIN.Name = "BTN_BOTTOM_MAIN";
            this.BTN_BOTTOM_MAIN.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_MAIN.TabIndex = 2;
            this.BTN_BOTTOM_MAIN.Text = "  MAIN";
            this.BTN_BOTTOM_MAIN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_MAIN.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_MAIN.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_MAIN.Click += new System.EventHandler(this.BTN_BOTTOM_MAIN_Click);
            // 
            // BTN_BOTTOM_SETUP
            // 
            this.BTN_BOTTOM_SETUP.BackColor = System.Drawing.Color.Transparent;
            this.BTN_BOTTOM_SETUP.FlatAppearance.BorderSize = 0;
            this.BTN_BOTTOM_SETUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_BOTTOM_SETUP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_BOTTOM_SETUP.ForeColor = System.Drawing.Color.DimGray;
            this.BTN_BOTTOM_SETUP.Image = global::ApsMotionControl.Properties.Resources.Config;
            this.BTN_BOTTOM_SETUP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_BOTTOM_SETUP.Location = new System.Drawing.Point(7, 147);
            this.BTN_BOTTOM_SETUP.Name = "BTN_BOTTOM_SETUP";
            this.BTN_BOTTOM_SETUP.Size = new System.Drawing.Size(133, 36);
            this.BTN_BOTTOM_SETUP.TabIndex = 1;
            this.BTN_BOTTOM_SETUP.Text = "  CONFIG";
            this.BTN_BOTTOM_SETUP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTN_BOTTOM_SETUP.UseVisualStyleBackColor = false;
            this.BTN_BOTTOM_SETUP.Click += new System.EventHandler(this.BTN_BOTTOM_SETUP_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1481, 800);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "eepromVerify";
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainTitlepictureBox)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.panel_ProductionInfo.ResumeLayout(false);
            this.panel_ProductionInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTN_BOTTOM_MAIN;
        private System.Windows.Forms.Button BTN_TOP_LOG;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Button BTN_MAIN_PAUSE1;
        private System.Windows.Forms.Button BTN_MAIN_ORIGIN1;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Button BTN_BOTTOM_WALLPAPER;
        private System.Windows.Forms.Button BTN_BOTTOM_EXIT;
        private System.Windows.Forms.Button BTN_BOTTOM_ALARM;
        private System.Windows.Forms.Button BTN_BOTTOM_LIGHT;
        private System.Windows.Forms.Button BTN_BOTTOM_IO;
        private System.Windows.Forms.Button BTN_BOTTOM_CCD;
        private System.Windows.Forms.Button BTN_BOTTOM_TEACH;
        private System.Windows.Forms.Button BTN_BOTTOM_SETUP;
        private System.Windows.Forms.Label MainTitleLabel;
        public System.Windows.Forms.ListBox listBox_Log;
        public System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.PictureBox MainTitlepictureBox;
        private System.Windows.Forms.Label labelGuide;
        public System.Windows.Forms.Button BTN_MAIN_START1;
        public System.Windows.Forms.Button BTN_MAIN_STOP1;
        public System.Windows.Forms.Button BTN_MAIN_READY1;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.Label label_build;
        private System.Windows.Forms.Button BTN_TOP_MES;
        private System.Windows.Forms.Panel panel_ProductionInfo;
        private System.Windows.Forms.Label label_LogTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_production_total;
        private System.Windows.Forms.Label label_production_ng;
        private System.Windows.Forms.Label label_production_ok;
        private System.Windows.Forms.TextBox textBox_TopLot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_BOTTOM_LOG;
    }
}

