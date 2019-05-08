namespace DNF_Utils
{
    partial class PatchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PatchList = new System.Windows.Forms.DataGridView();
            this.pID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.HandleGroup = new System.Windows.Forms.GroupBox();
            this.Progressbar = new System.Windows.Forms.ProgressBar();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.Label_Kxnrl = new System.Windows.Forms.Label();
            this.Button_Clear = new System.Windows.Forms.Button();
            this.Button_Dir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PatchList)).BeginInit();
            this.HandleGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // PatchList
            // 
            this.PatchList.AllowUserToAddRows = false;
            this.PatchList.AllowUserToDeleteRows = false;
            this.PatchList.AllowUserToResizeColumns = false;
            this.PatchList.AllowUserToResizeRows = false;
            this.PatchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatchList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pID,
            this.pName,
            this.pDesc,
            this.pAction});
            this.PatchList.Location = new System.Drawing.Point(12, 12);
            this.PatchList.Name = "PatchList";
            this.PatchList.RowHeadersVisible = false;
            this.PatchList.RowTemplate.Height = 23;
            this.PatchList.Size = new System.Drawing.Size(600, 183);
            this.PatchList.TabIndex = 0;
            this.PatchList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PatchList_CellContentClick);
            // 
            // pID
            // 
            this.pID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.pID.HeaderText = "ID";
            this.pID.Name = "pID";
            this.pID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pID.Visible = false;
            this.pID.Width = 5;
            // 
            // pName
            // 
            this.pName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.pName.HeaderText = "名称";
            this.pName.MaxInputLength = 128;
            this.pName.Name = "pName";
            this.pName.ReadOnly = true;
            this.pName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pName.ToolTipText = "补丁的名称";
            this.pName.Width = 180;
            // 
            // pDesc
            // 
            this.pDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.pDesc.HeaderText = "描述";
            this.pDesc.MaxInputLength = 256;
            this.pDesc.Name = "pDesc";
            this.pDesc.ReadOnly = true;
            this.pDesc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pDesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pDesc.Width = 300;
            // 
            // pAction
            // 
            this.pAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.pAction.HeaderText = "操作";
            this.pAction.Name = "pAction";
            this.pAction.ReadOnly = true;
            this.pAction.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pAction.Text = "安装";
            // 
            // HandleGroup
            // 
            this.HandleGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HandleGroup.Controls.Add(this.Progressbar);
            this.HandleGroup.Controls.Add(this.ActionLabel);
            this.HandleGroup.Location = new System.Drawing.Point(12, 210);
            this.HandleGroup.Name = "HandleGroup";
            this.HandleGroup.Size = new System.Drawing.Size(600, 90);
            this.HandleGroup.TabIndex = 8;
            this.HandleGroup.TabStop = false;
            this.HandleGroup.Text = "Handle";
            // 
            // Progressbar
            // 
            this.Progressbar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Progressbar.Location = new System.Drawing.Point(10, 50);
            this.Progressbar.Name = "Progressbar";
            this.Progressbar.Size = new System.Drawing.Size(580, 30);
            this.Progressbar.Step = 1;
            this.Progressbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Progressbar.TabIndex = 1;
            this.Progressbar.Value = 100;
            // 
            // ActionLabel
            // 
            this.ActionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActionLabel.Font = new System.Drawing.Font("微软雅黑 Light", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ActionLabel.Location = new System.Drawing.Point(10, 20);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(580, 20);
            this.ActionLabel.TabIndex = 0;
            this.ActionLabel.Text = "Loading...";
            this.ActionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Kxnrl
            // 
            this.Label_Kxnrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Kxnrl.Font = new System.Drawing.Font("微软雅黑 Light", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Kxnrl.ForeColor = System.Drawing.Color.Magenta;
            this.Label_Kxnrl.Location = new System.Drawing.Point(515, 305);
            this.Label_Kxnrl.Name = "Label_Kxnrl";
            this.Label_Kxnrl.Size = new System.Drawing.Size(110, 15);
            this.Label_Kxnrl.TabIndex = 0;
            this.Label_Kxnrl.Text = "Made with ❤ by Kyle";
            this.Label_Kxnrl.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Label_Kxnrl.DoubleClick += new System.EventHandler(this.Label_Kxnrl_DoubleClick);
            // 
            // Button_Clear
            // 
            this.Button_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_Clear.BackColor = System.Drawing.Color.Transparent;
            this.Button_Clear.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Button_Clear.FlatAppearance.BorderSize = 0;
            this.Button_Clear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_Clear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Clear.Font = new System.Drawing.Font("微软雅黑 Light", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Clear.Location = new System.Drawing.Point(-1, 300);
            this.Button_Clear.Name = "Button_Clear";
            this.Button_Clear.Size = new System.Drawing.Size(90, 22);
            this.Button_Clear.TabIndex = 0;
            this.Button_Clear.TabStop = false;
            this.Button_Clear.Text = "清空所有补丁";
            this.Button_Clear.UseVisualStyleBackColor = false;
            this.Button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // Button_Dir
            // 
            this.Button_Dir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_Dir.BackColor = System.Drawing.Color.Transparent;
            this.Button_Dir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Button_Dir.FlatAppearance.BorderSize = 0;
            this.Button_Dir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_Dir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_Dir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Dir.Font = new System.Drawing.Font("微软雅黑 Light", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Dir.Location = new System.Drawing.Point(95, 300);
            this.Button_Dir.Name = "Button_Dir";
            this.Button_Dir.Size = new System.Drawing.Size(90, 22);
            this.Button_Dir.TabIndex = 9;
            this.Button_Dir.TabStop = false;
            this.Button_Dir.Text = "打开补丁目录";
            this.Button_Dir.UseVisualStyleBackColor = false;
            this.Button_Dir.Click += new System.EventHandler(this.Button_Dir_Click);
            // 
            // PatchForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 321);
            this.Controls.Add(this.Button_Dir);
            this.Controls.Add(this.Button_Clear);
            this.Controls.Add(this.Label_Kxnrl);
            this.Controls.Add(this.HandleGroup);
            this.Controls.Add(this.PatchList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PatchForm";
            this.Load += new System.EventHandler(this.PatchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PatchList)).EndInit();
            this.HandleGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PatchList;
        private System.Windows.Forms.GroupBox HandleGroup;
        private System.Windows.Forms.ProgressBar Progressbar;
        private System.Windows.Forms.Label ActionLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn pID;
        private System.Windows.Forms.DataGridViewTextBoxColumn pName;
        private System.Windows.Forms.DataGridViewTextBoxColumn pDesc;
        private System.Windows.Forms.DataGridViewButtonColumn pAction;
        private System.Windows.Forms.Label Label_Kxnrl;
        private System.Windows.Forms.Button Button_Clear;
        private System.Windows.Forms.Button Button_Dir;
    }
}