namespace DNF_Utils
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.UtilsGroup = new System.Windows.Forms.GroupBox();
            this.Button_Cleaner = new System.Windows.Forms.Button();
            this.Button_MeltdownSpectre = new System.Windows.Forms.Button();
            this.Button_BlueScreen = new System.Windows.Forms.Button();
            this.Button_TXBucket = new System.Windows.Forms.Button();
            this.PatchGroup = new System.Windows.Forms.GroupBox();
            this.Button_PatchTheme = new System.Windows.Forms.Button();
            this.Button_PatchMisc = new System.Windows.Forms.Button();
            this.Button_PatchOptimized = new System.Windows.Forms.Button();
            this.Button_PatchCharacter = new System.Windows.Forms.Button();
            this.Button_Repair = new System.Windows.Forms.Button();
            this.Button_BlackScreen = new System.Windows.Forms.Button();
            this.Button_FullScreen = new System.Windows.Forms.Button();
            this.Button_FileAccess = new System.Windows.Forms.Button();
            this.UtilsGroup.SuspendLayout();
            this.PatchGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // UtilsGroup
            // 
            this.UtilsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UtilsGroup.BackColor = System.Drawing.Color.Transparent;
            this.UtilsGroup.Controls.Add(this.Button_FileAccess);
            this.UtilsGroup.Controls.Add(this.Button_FullScreen);
            this.UtilsGroup.Controls.Add(this.Button_BlackScreen);
            this.UtilsGroup.Controls.Add(this.Button_Repair);
            this.UtilsGroup.Controls.Add(this.Button_Cleaner);
            this.UtilsGroup.Controls.Add(this.Button_MeltdownSpectre);
            this.UtilsGroup.Controls.Add(this.Button_BlueScreen);
            this.UtilsGroup.Controls.Add(this.Button_TXBucket);
            this.UtilsGroup.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.UtilsGroup.ForeColor = System.Drawing.Color.Gold;
            this.UtilsGroup.Location = new System.Drawing.Point(12, 10);
            this.UtilsGroup.Name = "UtilsGroup";
            this.UtilsGroup.Padding = new System.Windows.Forms.Padding(5);
            this.UtilsGroup.Size = new System.Drawing.Size(500, 150);
            this.UtilsGroup.TabIndex = 0;
            this.UtilsGroup.TabStop = false;
            this.UtilsGroup.Text = "实用工具";
            // 
            // Button_Cleaner
            // 
            this.Button_Cleaner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cleaner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_Cleaner.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_Cleaner.FlatAppearance.BorderSize = 0;
            this.Button_Cleaner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_Cleaner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_Cleaner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Cleaner.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_Cleaner.Location = new System.Drawing.Point(8, 115);
            this.Button_Cleaner.Name = "Button_Cleaner";
            this.Button_Cleaner.Size = new System.Drawing.Size(235, 25);
            this.Button_Cleaner.TabIndex = 3;
            this.Button_Cleaner.TabStop = false;
            this.Button_Cleaner.Text = "删除更新残留";
            this.Button_Cleaner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_Cleaner.UseVisualStyleBackColor = true;
            this.Button_Cleaner.Click += new System.EventHandler(this.Button_Cleaner_Click);
            // 
            // Button_MeltdownSpectre
            // 
            this.Button_MeltdownSpectre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_MeltdownSpectre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_MeltdownSpectre.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_MeltdownSpectre.FlatAppearance.BorderSize = 0;
            this.Button_MeltdownSpectre.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_MeltdownSpectre.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_MeltdownSpectre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_MeltdownSpectre.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_MeltdownSpectre.Location = new System.Drawing.Point(8, 84);
            this.Button_MeltdownSpectre.Name = "Button_MeltdownSpectre";
            this.Button_MeltdownSpectre.Size = new System.Drawing.Size(235, 25);
            this.Button_MeltdownSpectre.TabIndex = 2;
            this.Button_MeltdownSpectre.TabStop = false;
            this.Button_MeltdownSpectre.Text = "幽灵熔断漏洞";
            this.Button_MeltdownSpectre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_MeltdownSpectre.UseVisualStyleBackColor = true;
            this.Button_MeltdownSpectre.Click += new System.EventHandler(this.Button_MeltdownSpectre_Click);
            // 
            // Button_BlueScreen
            // 
            this.Button_BlueScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_BlueScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_BlueScreen.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_BlueScreen.FlatAppearance.BorderSize = 0;
            this.Button_BlueScreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_BlueScreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_BlueScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_BlueScreen.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_BlueScreen.Location = new System.Drawing.Point(8, 53);
            this.Button_BlueScreen.Name = "Button_BlueScreen";
            this.Button_BlueScreen.Size = new System.Drawing.Size(235, 25);
            this.Button_BlueScreen.TabIndex = 1;
            this.Button_BlueScreen.TabStop = false;
            this.Button_BlueScreen.Text = "挂机蓝屏修复";
            this.Button_BlueScreen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_BlueScreen.UseVisualStyleBackColor = true;
            this.Button_BlueScreen.Click += new System.EventHandler(this.Button_BlueScreen_Click);
            // 
            // Button_TXBucket
            // 
            this.Button_TXBucket.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_TXBucket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_TXBucket.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_TXBucket.FlatAppearance.BorderSize = 0;
            this.Button_TXBucket.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_TXBucket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_TXBucket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_TXBucket.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.Button_TXBucket.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_TXBucket.Location = new System.Drawing.Point(8, 22);
            this.Button_TXBucket.Name = "Button_TXBucket";
            this.Button_TXBucket.Size = new System.Drawing.Size(235, 25);
            this.Button_TXBucket.TabIndex = 0;
            this.Button_TXBucket.TabStop = false;
            this.Button_TXBucket.Text = "干掉垃圾插件";
            this.Button_TXBucket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_TXBucket.UseVisualStyleBackColor = true;
            this.Button_TXBucket.Click += new System.EventHandler(this.Button_TXBucket_Click);
            // 
            // PatchGroup
            // 
            this.PatchGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PatchGroup.BackColor = System.Drawing.Color.Transparent;
            this.PatchGroup.Controls.Add(this.Button_PatchTheme);
            this.PatchGroup.Controls.Add(this.Button_PatchMisc);
            this.PatchGroup.Controls.Add(this.Button_PatchOptimized);
            this.PatchGroup.Controls.Add(this.Button_PatchCharacter);
            this.PatchGroup.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.PatchGroup.ForeColor = System.Drawing.Color.Orange;
            this.PatchGroup.Location = new System.Drawing.Point(12, 170);
            this.PatchGroup.Name = "PatchGroup";
            this.PatchGroup.Padding = new System.Windows.Forms.Padding(5);
            this.PatchGroup.Size = new System.Drawing.Size(500, 150);
            this.PatchGroup.TabIndex = 1;
            this.PatchGroup.TabStop = false;
            this.PatchGroup.Text = "补丁管理";
            // 
            // Button_PatchTheme
            // 
            this.Button_PatchTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_PatchTheme.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_PatchTheme.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_PatchTheme.FlatAppearance.BorderSize = 0;
            this.Button_PatchTheme.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_PatchTheme.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_PatchTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_PatchTheme.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_PatchTheme.Location = new System.Drawing.Point(8, 115);
            this.Button_PatchTheme.Name = "Button_PatchTheme";
            this.Button_PatchTheme.Size = new System.Drawing.Size(484, 25);
            this.Button_PatchTheme.TabIndex = 4;
            this.Button_PatchTheme.TabStop = false;
            this.Button_PatchTheme.Text = "主题补丁";
            this.Button_PatchTheme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_PatchTheme.UseVisualStyleBackColor = true;
            this.Button_PatchTheme.Click += new System.EventHandler(this.Button_PatchTheme_Click);
            // 
            // Button_PatchMisc
            // 
            this.Button_PatchMisc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_PatchMisc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_PatchMisc.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_PatchMisc.FlatAppearance.BorderSize = 0;
            this.Button_PatchMisc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_PatchMisc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_PatchMisc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_PatchMisc.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_PatchMisc.Location = new System.Drawing.Point(8, 84);
            this.Button_PatchMisc.Name = "Button_PatchMisc";
            this.Button_PatchMisc.Size = new System.Drawing.Size(484, 25);
            this.Button_PatchMisc.TabIndex = 3;
            this.Button_PatchMisc.TabStop = false;
            this.Button_PatchMisc.Text = "其他补丁";
            this.Button_PatchMisc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_PatchMisc.UseVisualStyleBackColor = true;
            this.Button_PatchMisc.Click += new System.EventHandler(this.Button_PatchMisc_Click);
            // 
            // Button_PatchOptimized
            // 
            this.Button_PatchOptimized.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_PatchOptimized.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_PatchOptimized.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_PatchOptimized.FlatAppearance.BorderSize = 0;
            this.Button_PatchOptimized.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_PatchOptimized.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_PatchOptimized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_PatchOptimized.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_PatchOptimized.Location = new System.Drawing.Point(8, 53);
            this.Button_PatchOptimized.Name = "Button_PatchOptimized";
            this.Button_PatchOptimized.Size = new System.Drawing.Size(484, 25);
            this.Button_PatchOptimized.TabIndex = 2;
            this.Button_PatchOptimized.TabStop = false;
            this.Button_PatchOptimized.Text = "优化补丁";
            this.Button_PatchOptimized.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_PatchOptimized.UseVisualStyleBackColor = true;
            this.Button_PatchOptimized.Click += new System.EventHandler(this.Button_PatchOptimized_Click);
            // 
            // Button_PatchCharacter
            // 
            this.Button_PatchCharacter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_PatchCharacter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_PatchCharacter.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_PatchCharacter.FlatAppearance.BorderSize = 0;
            this.Button_PatchCharacter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_PatchCharacter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_PatchCharacter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_PatchCharacter.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_PatchCharacter.Location = new System.Drawing.Point(8, 22);
            this.Button_PatchCharacter.Name = "Button_PatchCharacter";
            this.Button_PatchCharacter.Size = new System.Drawing.Size(484, 25);
            this.Button_PatchCharacter.TabIndex = 1;
            this.Button_PatchCharacter.TabStop = false;
            this.Button_PatchCharacter.Text = "角色补丁";
            this.Button_PatchCharacter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_PatchCharacter.UseVisualStyleBackColor = true;
            this.Button_PatchCharacter.Click += new System.EventHandler(this.Button_PatchCharacter_Click);
            // 
            // Button_Repair
            // 
            this.Button_Repair.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Repair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_Repair.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_Repair.FlatAppearance.BorderSize = 0;
            this.Button_Repair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_Repair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_Repair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Repair.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.Button_Repair.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_Repair.Location = new System.Drawing.Point(257, 22);
            this.Button_Repair.Name = "Button_Repair";
            this.Button_Repair.Size = new System.Drawing.Size(235, 25);
            this.Button_Repair.TabIndex = 4;
            this.Button_Repair.TabStop = false;
            this.Button_Repair.Text = "启动游戏修复";
            this.Button_Repair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button_Repair.UseVisualStyleBackColor = true;
            this.Button_Repair.Click += new System.EventHandler(this.Button_Repair_Click);
            // 
            // Button_BlackScreen
            // 
            this.Button_BlackScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_BlackScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_BlackScreen.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_BlackScreen.FlatAppearance.BorderSize = 0;
            this.Button_BlackScreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_BlackScreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_BlackScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_BlackScreen.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_BlackScreen.Location = new System.Drawing.Point(257, 53);
            this.Button_BlackScreen.Name = "Button_BlackScreen";
            this.Button_BlackScreen.Size = new System.Drawing.Size(235, 25);
            this.Button_BlackScreen.TabIndex = 5;
            this.Button_BlackScreen.TabStop = false;
            this.Button_BlackScreen.Text = "修复换线黑屏/掉线";
            this.Button_BlackScreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button_BlackScreen.UseVisualStyleBackColor = true;
            this.Button_BlackScreen.Click += new System.EventHandler(this.Button_BlackScreen_Click);
            // 
            // Button_FullScreen
            // 
            this.Button_FullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_FullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_FullScreen.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_FullScreen.FlatAppearance.BorderSize = 0;
            this.Button_FullScreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_FullScreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_FullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_FullScreen.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_FullScreen.Location = new System.Drawing.Point(257, 84);
            this.Button_FullScreen.Name = "Button_FullScreen";
            this.Button_FullScreen.Size = new System.Drawing.Size(235, 25);
            this.Button_FullScreen.TabIndex = 6;
            this.Button_FullScreen.TabStop = false;
            this.Button_FullScreen.Text = "修复全屏黑屏/卡全屏";
            this.Button_FullScreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button_FullScreen.UseVisualStyleBackColor = true;
            this.Button_FullScreen.Click += new System.EventHandler(this.Button_FullScreen_Click);
            // 
            // Button_FileAccess
            // 
            this.Button_FileAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_FileAccess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_FileAccess.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_FileAccess.FlatAppearance.BorderSize = 0;
            this.Button_FileAccess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_FileAccess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_FileAccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_FileAccess.ForeColor = System.Drawing.Color.DarkViolet;
            this.Button_FileAccess.Location = new System.Drawing.Point(257, 115);
            this.Button_FileAccess.Name = "Button_FileAccess";
            this.Button_FileAccess.Size = new System.Drawing.Size(235, 25);
            this.Button_FileAccess.TabIndex = 7;
            this.Button_FileAccess.TabStop = false;
            this.Button_FileAccess.Text = "修复更新失败/获取权限";
            this.Button_FileAccess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button_FileAccess.UseVisualStyleBackColor = true;
            this.Button_FileAccess.Click += new System.EventHandler(this.Button_FileAccess_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::DNF_Utils.Properties.Resources.BladeDancer;
            this.ClientSize = new System.Drawing.Size(524, 331);
            this.Controls.Add(this.PatchGroup);
            this.Controls.Add(this.UtilsGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DNF 实用工具集";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.UtilsGroup.ResumeLayout(false);
            this.PatchGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox UtilsGroup;
        private System.Windows.Forms.Button Button_TXBucket;
        private System.Windows.Forms.Button Button_BlueScreen;
        private System.Windows.Forms.Button Button_Cleaner;
        private System.Windows.Forms.Button Button_MeltdownSpectre;
        private System.Windows.Forms.GroupBox PatchGroup;
        private System.Windows.Forms.Button Button_PatchCharacter;
        private System.Windows.Forms.Button Button_PatchTheme;
        private System.Windows.Forms.Button Button_PatchMisc;
        private System.Windows.Forms.Button Button_PatchOptimized;
        private System.Windows.Forms.Button Button_Repair;
        private System.Windows.Forms.Button Button_FullScreen;
        private System.Windows.Forms.Button Button_BlackScreen;
        private System.Windows.Forms.Button Button_FileAccess;
    }
}

