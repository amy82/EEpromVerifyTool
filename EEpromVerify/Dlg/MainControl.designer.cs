namespace ApsMotionControl.Dlg
{
    partial class MainControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ManualTitleLabel = new System.Windows.Forms.Label();
            this.ManualPanel = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.BTN_MAIN_RECIPE_VIEW = new System.Windows.Forms.Button();
            this.BTN_MAIN_RECIPE_CHANGE = new System.Windows.Forms.Button();
            this.BTN_MAIN_RECIPE_CREATE = new System.Windows.Forms.Button();
            this.BTN_MAIN_RECIPE_DEL = new System.Windows.Forms.Button();
            this.dataGridView_Recipe = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridView_Model = new System.Windows.Forms.DataGridView();
            this.BTN_MAIN_MODEL_CHANGE = new System.Windows.Forms.Button();
            this.BTN_MAIN_MODEL_LOAD = new System.Windows.Forms.Button();
            this.BTN_MAIN_MODEL_DEL = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.BTN_MAIN_MODEL_ADD = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_MaterialId = new System.Windows.Forms.TextBox();
            this.BTN_MAIN_MATERIAL_ID_REPORT = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_AbortedLot = new System.Windows.Forms.TextBox();
            this.BTN_MAIN_ABORT_LOT = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BTN_MAIN_OPID_SAVE = new System.Windows.Forms.Button();
            this.textBox_OperatorId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_ControlStateVal = new System.Windows.Forms.TextBox();
            this.BTN_MAIN_ONLINE_REMOTE_REQ = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_MAIN_OFFLINE_REQ = new System.Windows.Forms.Button();
            this.BTN_MANUAL_PCB = new System.Windows.Forms.Button();
            this.BTN_MANUAL_LENS = new System.Windows.Forms.Button();
            this.ManualPanel.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Recipe)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Model)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ManualTitleLabel
            // 
            this.ManualTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ManualTitleLabel.Location = new System.Drawing.Point(16, 16);
            this.ManualTitleLabel.Name = "ManualTitleLabel";
            this.ManualTitleLabel.Size = new System.Drawing.Size(250, 42);
            this.ManualTitleLabel.TabIndex = 2;
            this.ManualTitleLabel.Text = "| MAIN";
            this.ManualTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ManualPanel
            // 
            this.ManualPanel.Controls.Add(this.groupBox6);
            this.ManualPanel.Controls.Add(this.groupBox5);
            this.ManualPanel.Controls.Add(this.groupBox4);
            this.ManualPanel.Controls.Add(this.groupBox3);
            this.ManualPanel.Controls.Add(this.groupBox2);
            this.ManualPanel.Controls.Add(this.groupBox1);
            this.ManualPanel.Location = new System.Drawing.Point(21, 97);
            this.ManualPanel.Name = "ManualPanel";
            this.ManualPanel.Size = new System.Drawing.Size(934, 924);
            this.ManualPanel.TabIndex = 4;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.White;
            this.groupBox6.Controls.Add(this.BTN_MAIN_RECIPE_VIEW);
            this.groupBox6.Controls.Add(this.BTN_MAIN_RECIPE_CHANGE);
            this.groupBox6.Controls.Add(this.BTN_MAIN_RECIPE_CREATE);
            this.groupBox6.Controls.Add(this.BTN_MAIN_RECIPE_DEL);
            this.groupBox6.Controls.Add(this.dataGridView_Recipe);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Location = new System.Drawing.Point(512, 348);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(379, 277);
            this.groupBox6.TabIndex = 49;
            this.groupBox6.TabStop = false;
            // 
            // BTN_MAIN_RECIPE_VIEW
            // 
            this.BTN_MAIN_RECIPE_VIEW.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_RECIPE_VIEW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_RECIPE_VIEW.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_RECIPE_VIEW.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_RECIPE_VIEW.Location = new System.Drawing.Point(253, 17);
            this.BTN_MAIN_RECIPE_VIEW.Name = "BTN_MAIN_RECIPE_VIEW";
            this.BTN_MAIN_RECIPE_VIEW.Size = new System.Drawing.Size(97, 32);
            this.BTN_MAIN_RECIPE_VIEW.TabIndex = 36;
            this.BTN_MAIN_RECIPE_VIEW.Text = "VIEW";
            this.BTN_MAIN_RECIPE_VIEW.UseVisualStyleBackColor = false;
            this.BTN_MAIN_RECIPE_VIEW.Click += new System.EventHandler(this.BTN_MAIN_RECIPE_VIEW_Click);
            // 
            // BTN_MAIN_RECIPE_CHANGE
            // 
            this.BTN_MAIN_RECIPE_CHANGE.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_RECIPE_CHANGE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_RECIPE_CHANGE.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_RECIPE_CHANGE.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_RECIPE_CHANGE.Location = new System.Drawing.Point(247, 230);
            this.BTN_MAIN_RECIPE_CHANGE.Name = "BTN_MAIN_RECIPE_CHANGE";
            this.BTN_MAIN_RECIPE_CHANGE.Size = new System.Drawing.Size(103, 39);
            this.BTN_MAIN_RECIPE_CHANGE.TabIndex = 35;
            this.BTN_MAIN_RECIPE_CHANGE.Text = "Change";
            this.BTN_MAIN_RECIPE_CHANGE.UseVisualStyleBackColor = false;
            this.BTN_MAIN_RECIPE_CHANGE.Click += new System.EventHandler(this.BTN_MAIN_RECIPE_CHANGE_Click);
            // 
            // BTN_MAIN_RECIPE_CREATE
            // 
            this.BTN_MAIN_RECIPE_CREATE.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_RECIPE_CREATE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_RECIPE_CREATE.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_RECIPE_CREATE.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_RECIPE_CREATE.Location = new System.Drawing.Point(135, 230);
            this.BTN_MAIN_RECIPE_CREATE.Name = "BTN_MAIN_RECIPE_CREATE";
            this.BTN_MAIN_RECIPE_CREATE.Size = new System.Drawing.Size(110, 39);
            this.BTN_MAIN_RECIPE_CREATE.TabIndex = 34;
            this.BTN_MAIN_RECIPE_CREATE.Text = "Create";
            this.BTN_MAIN_RECIPE_CREATE.UseVisualStyleBackColor = false;
            this.BTN_MAIN_RECIPE_CREATE.Click += new System.EventHandler(this.BTN_MAIN_RECIPE_CREATE_Click);
            // 
            // BTN_MAIN_RECIPE_DEL
            // 
            this.BTN_MAIN_RECIPE_DEL.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_RECIPE_DEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_RECIPE_DEL.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_RECIPE_DEL.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_RECIPE_DEL.Location = new System.Drawing.Point(30, 230);
            this.BTN_MAIN_RECIPE_DEL.Name = "BTN_MAIN_RECIPE_DEL";
            this.BTN_MAIN_RECIPE_DEL.Size = new System.Drawing.Size(103, 39);
            this.BTN_MAIN_RECIPE_DEL.TabIndex = 33;
            this.BTN_MAIN_RECIPE_DEL.Text = "Delete";
            this.BTN_MAIN_RECIPE_DEL.UseVisualStyleBackColor = false;
            this.BTN_MAIN_RECIPE_DEL.Click += new System.EventHandler(this.BTN_MAIN_RECIPE_DEL_Click);
            // 
            // dataGridView_Recipe
            // 
            this.dataGridView_Recipe.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.GhostWhite;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Recipe.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridView_Recipe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Recipe.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridView_Recipe.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView_Recipe.Location = new System.Drawing.Point(30, 55);
            this.dataGridView_Recipe.Name = "dataGridView_Recipe";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.RosyBrown;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Recipe.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridView_Recipe.RowTemplate.Height = 23;
            this.dataGridView_Recipe.Size = new System.Drawing.Size(320, 168);
            this.dataGridView_Recipe.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(27, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 23);
            this.label6.TabIndex = 26;
            this.label6.Text = "Recipe List";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.White;
            this.groupBox5.Controls.Add(this.dataGridView_Model);
            this.groupBox5.Controls.Add(this.BTN_MAIN_MODEL_CHANGE);
            this.groupBox5.Controls.Add(this.BTN_MAIN_MODEL_LOAD);
            this.groupBox5.Controls.Add(this.BTN_MAIN_MODEL_DEL);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.BTN_MAIN_MODEL_ADD);
            this.groupBox5.Location = new System.Drawing.Point(512, 107);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(379, 235);
            this.groupBox5.TabIndex = 48;
            this.groupBox5.TabStop = false;
            // 
            // dataGridView_Model
            // 
            this.dataGridView_Model.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Model.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridView_Model.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Model.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridView_Model.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView_Model.Location = new System.Drawing.Point(17, 46);
            this.dataGridView_Model.Name = "dataGridView_Model";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.RosyBrown;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Model.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridView_Model.RowTemplate.Height = 23;
            this.dataGridView_Model.Size = new System.Drawing.Size(233, 176);
            this.dataGridView_Model.TabIndex = 31;
            // 
            // BTN_MAIN_MODEL_CHANGE
            // 
            this.BTN_MAIN_MODEL_CHANGE.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_MODEL_CHANGE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_MODEL_CHANGE.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_MODEL_CHANGE.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_MODEL_CHANGE.Location = new System.Drawing.Point(270, 172);
            this.BTN_MAIN_MODEL_CHANGE.Name = "BTN_MAIN_MODEL_CHANGE";
            this.BTN_MAIN_MODEL_CHANGE.Size = new System.Drawing.Size(99, 39);
            this.BTN_MAIN_MODEL_CHANGE.TabIndex = 30;
            this.BTN_MAIN_MODEL_CHANGE.Text = "RENAME";
            this.BTN_MAIN_MODEL_CHANGE.UseVisualStyleBackColor = false;
            this.BTN_MAIN_MODEL_CHANGE.Click += new System.EventHandler(this.BTN_MAIN_MODEL_CHANGE_Click);
            // 
            // BTN_MAIN_MODEL_LOAD
            // 
            this.BTN_MAIN_MODEL_LOAD.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_MODEL_LOAD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_MODEL_LOAD.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_MODEL_LOAD.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_MODEL_LOAD.Location = new System.Drawing.Point(270, 130);
            this.BTN_MAIN_MODEL_LOAD.Name = "BTN_MAIN_MODEL_LOAD";
            this.BTN_MAIN_MODEL_LOAD.Size = new System.Drawing.Size(99, 39);
            this.BTN_MAIN_MODEL_LOAD.TabIndex = 29;
            this.BTN_MAIN_MODEL_LOAD.Text = "LOAD";
            this.BTN_MAIN_MODEL_LOAD.UseVisualStyleBackColor = false;
            this.BTN_MAIN_MODEL_LOAD.Click += new System.EventHandler(this.BTN_MAIN_MODEL_LOAD_Click);
            // 
            // BTN_MAIN_MODEL_DEL
            // 
            this.BTN_MAIN_MODEL_DEL.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_MODEL_DEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_MODEL_DEL.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_MODEL_DEL.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_MODEL_DEL.Location = new System.Drawing.Point(270, 88);
            this.BTN_MAIN_MODEL_DEL.Name = "BTN_MAIN_MODEL_DEL";
            this.BTN_MAIN_MODEL_DEL.Size = new System.Drawing.Size(99, 39);
            this.BTN_MAIN_MODEL_DEL.TabIndex = 28;
            this.BTN_MAIN_MODEL_DEL.Text = "DEL";
            this.BTN_MAIN_MODEL_DEL.UseVisualStyleBackColor = false;
            this.BTN_MAIN_MODEL_DEL.Click += new System.EventHandler(this.BTN_MAIN_MODEL_DEL_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(14, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 23);
            this.label5.TabIndex = 26;
            this.label5.Text = "Model List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BTN_MAIN_MODEL_ADD
            // 
            this.BTN_MAIN_MODEL_ADD.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_MODEL_ADD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_MODEL_ADD.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_MODEL_ADD.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_MODEL_ADD.Location = new System.Drawing.Point(270, 46);
            this.BTN_MAIN_MODEL_ADD.Name = "BTN_MAIN_MODEL_ADD";
            this.BTN_MAIN_MODEL_ADD.Size = new System.Drawing.Size(99, 39);
            this.BTN_MAIN_MODEL_ADD.TabIndex = 27;
            this.BTN_MAIN_MODEL_ADD.Text = "ADD";
            this.BTN_MAIN_MODEL_ADD.UseVisualStyleBackColor = false;
            this.BTN_MAIN_MODEL_ADD.Click += new System.EventHandler(this.BTN_MAIN_MODEL_ADD_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.textBox_MaterialId);
            this.groupBox4.Controls.Add(this.BTN_MAIN_MATERIAL_ID_REPORT);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(512, 761);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 124);
            this.groupBox4.TabIndex = 49;
            this.groupBox4.TabStop = false;
            // 
            // textBox_MaterialId
            // 
            this.textBox_MaterialId.Font = new System.Drawing.Font("굴림", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_MaterialId.Location = new System.Drawing.Point(23, 43);
            this.textBox_MaterialId.Name = "textBox_MaterialId";
            this.textBox_MaterialId.Size = new System.Drawing.Size(338, 27);
            this.textBox_MaterialId.TabIndex = 29;
            this.textBox_MaterialId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BTN_MAIN_MATERIAL_ID_REPORT
            // 
            this.BTN_MAIN_MATERIAL_ID_REPORT.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_MATERIAL_ID_REPORT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_MATERIAL_ID_REPORT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_MATERIAL_ID_REPORT.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_MATERIAL_ID_REPORT.Location = new System.Drawing.Point(164, 76);
            this.BTN_MAIN_MATERIAL_ID_REPORT.Name = "BTN_MAIN_MATERIAL_ID_REPORT";
            this.BTN_MAIN_MATERIAL_ID_REPORT.Size = new System.Drawing.Size(197, 39);
            this.BTN_MAIN_MATERIAL_ID_REPORT.TabIndex = 28;
            this.BTN_MAIN_MATERIAL_ID_REPORT.Text = "MATERIAL ID REPORT";
            this.BTN_MAIN_MATERIAL_ID_REPORT.UseVisualStyleBackColor = false;
            this.BTN_MAIN_MATERIAL_ID_REPORT.Click += new System.EventHandler(this.BTN_MAIN_MATERIAL_ID_REPORT_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(20, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 23);
            this.label4.TabIndex = 26;
            this.label4.Text = "Material Data";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.textBox_AbortedLot);
            this.groupBox3.Controls.Add(this.BTN_MAIN_ABORT_LOT);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(512, 631);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(379, 124);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            // 
            // textBox_AbortedLot
            // 
            this.textBox_AbortedLot.Font = new System.Drawing.Font("굴림", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_AbortedLot.Location = new System.Drawing.Point(23, 43);
            this.textBox_AbortedLot.Name = "textBox_AbortedLot";
            this.textBox_AbortedLot.Size = new System.Drawing.Size(338, 27);
            this.textBox_AbortedLot.TabIndex = 29;
            this.textBox_AbortedLot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BTN_MAIN_ABORT_LOT
            // 
            this.BTN_MAIN_ABORT_LOT.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_ABORT_LOT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_ABORT_LOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_ABORT_LOT.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_ABORT_LOT.Location = new System.Drawing.Point(219, 76);
            this.BTN_MAIN_ABORT_LOT.Name = "BTN_MAIN_ABORT_LOT";
            this.BTN_MAIN_ABORT_LOT.Size = new System.Drawing.Size(142, 39);
            this.BTN_MAIN_ABORT_LOT.TabIndex = 28;
            this.BTN_MAIN_ABORT_LOT.Text = "LOT CANCEL";
            this.BTN_MAIN_ABORT_LOT.UseVisualStyleBackColor = false;
            this.BTN_MAIN_ABORT_LOT.Click += new System.EventHandler(this.BTN_MAIN_ABORT_LOT_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(20, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 23);
            this.label3.TabIndex = 26;
            this.label3.Text = "Lot Abord";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.BTN_MAIN_OPID_SAVE);
            this.groupBox2.Controls.Add(this.textBox_OperatorId);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(512, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 100);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            // 
            // BTN_MAIN_OPID_SAVE
            // 
            this.BTN_MAIN_OPID_SAVE.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_OPID_SAVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_OPID_SAVE.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_OPID_SAVE.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_OPID_SAVE.Location = new System.Drawing.Point(251, 20);
            this.BTN_MAIN_OPID_SAVE.Name = "BTN_MAIN_OPID_SAVE";
            this.BTN_MAIN_OPID_SAVE.Size = new System.Drawing.Size(99, 32);
            this.BTN_MAIN_OPID_SAVE.TabIndex = 32;
            this.BTN_MAIN_OPID_SAVE.Text = "SAVE";
            this.BTN_MAIN_OPID_SAVE.UseVisualStyleBackColor = false;
            this.BTN_MAIN_OPID_SAVE.Click += new System.EventHandler(this.BTN_MAIN_OPID_SAVE_Click);
            // 
            // textBox_OperatorId
            // 
            this.textBox_OperatorId.Font = new System.Drawing.Font("굴림", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_OperatorId.Location = new System.Drawing.Point(30, 58);
            this.textBox_OperatorId.Name = "textBox_OperatorId";
            this.textBox_OperatorId.Size = new System.Drawing.Size(320, 27);
            this.textBox_OperatorId.TabIndex = 27;
            this.textBox_OperatorId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(27, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 23);
            this.label2.TabIndex = 26;
            this.label2.Text = "Operator ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.textBox_ControlStateVal);
            this.groupBox1.Controls.Add(this.BTN_MAIN_ONLINE_REMOTE_REQ);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BTN_MAIN_OFFLINE_REQ);
            this.groupBox1.Location = new System.Drawing.Point(14, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 139);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            // 
            // textBox_ControlStateVal
            // 
            this.textBox_ControlStateVal.Font = new System.Drawing.Font("굴림", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_ControlStateVal.Location = new System.Drawing.Point(23, 45);
            this.textBox_ControlStateVal.Name = "textBox_ControlStateVal";
            this.textBox_ControlStateVal.Size = new System.Drawing.Size(338, 27);
            this.textBox_ControlStateVal.TabIndex = 29;
            this.textBox_ControlStateVal.Text = "EquipmentOffline";
            this.textBox_ControlStateVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BTN_MAIN_ONLINE_REMOTE_REQ
            // 
            this.BTN_MAIN_ONLINE_REMOTE_REQ.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_ONLINE_REMOTE_REQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_ONLINE_REMOTE_REQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_MAIN_ONLINE_REMOTE_REQ.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_ONLINE_REMOTE_REQ.Location = new System.Drawing.Point(196, 81);
            this.BTN_MAIN_ONLINE_REMOTE_REQ.Name = "BTN_MAIN_ONLINE_REMOTE_REQ";
            this.BTN_MAIN_ONLINE_REMOTE_REQ.Size = new System.Drawing.Size(165, 39);
            this.BTN_MAIN_ONLINE_REMOTE_REQ.TabIndex = 28;
            this.BTN_MAIN_ONLINE_REMOTE_REQ.Text = "Online Change";
            this.BTN_MAIN_ONLINE_REMOTE_REQ.UseVisualStyleBackColor = false;
            this.BTN_MAIN_ONLINE_REMOTE_REQ.Click += new System.EventHandler(this.BTN_MAIN_ONLINE_REMOTE_REQ_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 23);
            this.label1.TabIndex = 26;
            this.label1.Text = "Control State";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BTN_MAIN_OFFLINE_REQ
            // 
            this.BTN_MAIN_OFFLINE_REQ.BackColor = System.Drawing.Color.Tan;
            this.BTN_MAIN_OFFLINE_REQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_OFFLINE_REQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_MAIN_OFFLINE_REQ.ForeColor = System.Drawing.Color.White;
            this.BTN_MAIN_OFFLINE_REQ.Location = new System.Drawing.Point(23, 81);
            this.BTN_MAIN_OFFLINE_REQ.Name = "BTN_MAIN_OFFLINE_REQ";
            this.BTN_MAIN_OFFLINE_REQ.Size = new System.Drawing.Size(165, 39);
            this.BTN_MAIN_OFFLINE_REQ.TabIndex = 27;
            this.BTN_MAIN_OFFLINE_REQ.Text = "Offline Change";
            this.BTN_MAIN_OFFLINE_REQ.UseVisualStyleBackColor = false;
            this.BTN_MAIN_OFFLINE_REQ.Click += new System.EventHandler(this.BTN_MAIN_OFFLINE_REQ_Click);
            // 
            // BTN_MANUAL_PCB
            // 
            this.BTN_MANUAL_PCB.BackColor = System.Drawing.Color.Tan;
            this.BTN_MANUAL_PCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MANUAL_PCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_MANUAL_PCB.ForeColor = System.Drawing.Color.White;
            this.BTN_MANUAL_PCB.Location = new System.Drawing.Point(748, 14);
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
            // MainControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Controls.Add(this.BTN_MANUAL_LENS);
            this.Controls.Add(this.BTN_MANUAL_PCB);
            this.Controls.Add(this.ManualPanel);
            this.Controls.Add(this.ManualTitleLabel);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(955, 1024);
            this.VisibleChanged += new System.EventHandler(this.MainControl_VisibleChanged);
            this.ManualPanel.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Recipe)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Model)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ManualTitleLabel;
        private System.Windows.Forms.Panel ManualPanel;
        private System.Windows.Forms.Button BTN_MANUAL_PCB;
        private System.Windows.Forms.Button BTN_MANUAL_LENS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BTN_MAIN_ONLINE_REMOTE_REQ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_MAIN_OFFLINE_REQ;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_OperatorId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ControlStateVal;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox_AbortedLot;
        private System.Windows.Forms.Button BTN_MAIN_ABORT_LOT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox_MaterialId;
        private System.Windows.Forms.Button BTN_MAIN_MATERIAL_ID_REPORT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button BTN_MAIN_MODEL_DEL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BTN_MAIN_MODEL_ADD;
        private System.Windows.Forms.Button BTN_MAIN_MODEL_CHANGE;
        private System.Windows.Forms.Button BTN_MAIN_MODEL_LOAD;
        private System.Windows.Forms.DataGridView dataGridView_Model;
        private System.Windows.Forms.Button BTN_MAIN_OPID_SAVE;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dataGridView_Recipe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BTN_MAIN_RECIPE_CHANGE;
        private System.Windows.Forms.Button BTN_MAIN_RECIPE_CREATE;
        private System.Windows.Forms.Button BTN_MAIN_RECIPE_DEL;
        private System.Windows.Forms.Button BTN_MAIN_RECIPE_VIEW;
    }
}
