using System;

namespace DNF_Utils
{
    partial class ThemeForm
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
            this.ThemeSelector = new System.Windows.Forms.ComboBox();
            this.HandleGroup = new System.Windows.Forms.GroupBox();
            this.Progressbar = new System.Windows.Forms.ProgressBar();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.Label_Kxnrl = new System.Windows.Forms.Label();
            this.Button_Action = new System.Windows.Forms.Button();
            this.Button_Verify = new System.Windows.Forms.Button();
            this.Label_Title = new System.Windows.Forms.Label();
            this.Label_Verify = new System.Windows.Forms.Label();
            this.HandleGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ThemeSelector
            // 
            this.ThemeSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ThemeSelector.Enabled = false;
            this.ThemeSelector.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ThemeSelector.FormattingEnabled = true;
            this.ThemeSelector.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ThemeSelector.Location = new System.Drawing.Point(12, 12);
            this.ThemeSelector.Name = "ThemeSelector";
            this.ThemeSelector.Size = new System.Drawing.Size(360, 24);
            this.ThemeSelector.TabIndex = 0;
            this.ThemeSelector.TabStop = false;
            this.ThemeSelector.SelectedIndexChanged += new System.EventHandler(this.ThemeSelector_SelectedIndexChanged);
            // 
            // HandleGroup
            // 
            this.HandleGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HandleGroup.Controls.Add(this.Progressbar);
            this.HandleGroup.Controls.Add(this.ActionLabel);
            this.HandleGroup.Location = new System.Drawing.Point(10, 90);
            this.HandleGroup.Name = "HandleGroup";
            this.HandleGroup.Size = new System.Drawing.Size(360, 90);
            this.HandleGroup.TabIndex = 9;
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
            this.Progressbar.Size = new System.Drawing.Size(340, 30);
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
            this.ActionLabel.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ActionLabel.Location = new System.Drawing.Point(10, 20);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(340, 20);
            this.ActionLabel.TabIndex = 0;
            this.ActionLabel.Text = "Loading...";
            this.ActionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Kxnrl
            // 
            this.Label_Kxnrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Kxnrl.Font = new System.Drawing.Font("微软雅黑 Light", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Kxnrl.ForeColor = System.Drawing.Color.Magenta;
            this.Label_Kxnrl.Location = new System.Drawing.Point(275, 185);
            this.Label_Kxnrl.Name = "Label_Kxnrl";
            this.Label_Kxnrl.Size = new System.Drawing.Size(110, 15);
            this.Label_Kxnrl.TabIndex = 10;
            this.Label_Kxnrl.Text = "Made with ❤ by Kyle";
            this.Label_Kxnrl.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Label_Kxnrl.DoubleClick += new System.EventHandler(this.Label_Kxnrl_DoubleClick);
            // 
            // Button_Action
            // 
            this.Button_Action.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Action.Enabled = false;
            this.Button_Action.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.Button_Action.Location = new System.Drawing.Point(12, 50);
            this.Button_Action.Name = "Button_Action";
            this.Button_Action.Size = new System.Drawing.Size(75, 25);
            this.Button_Action.TabIndex = 11;
            this.Button_Action.Text = "安装";
            this.Button_Action.UseVisualStyleBackColor = true;
            this.Button_Action.Click += new System.EventHandler(this.Button_Action_Click);
            // 
            // Button_Verify
            // 
            this.Button_Verify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Verify.Enabled = false;
            this.Button_Verify.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.Button_Verify.Location = new System.Drawing.Point(93, 50);
            this.Button_Verify.Name = "Button_Verify";
            this.Button_Verify.Size = new System.Drawing.Size(75, 25);
            this.Button_Verify.TabIndex = 12;
            this.Button_Verify.Text = "验证";
            this.Button_Verify.UseVisualStyleBackColor = true;
            this.Button_Verify.Click += new System.EventHandler(this.Button_Verify_Click);
            // 
            // Label_Title
            // 
            this.Label_Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Title.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Title.Location = new System.Drawing.Point(244, 50);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(60, 25);
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "完整性:";
            this.Label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_Verify
            // 
            this.Label_Verify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Verify.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Verify.Location = new System.Drawing.Point(310, 50);
            this.Label_Verify.Name = "Label_Verify";
            this.Label_Verify.Size = new System.Drawing.Size(60, 25);
            this.Label_Verify.TabIndex = 0;
            this.Label_Verify.Text = "未知";
            this.Label_Verify.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ThemeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 201);
            this.Controls.Add(this.Label_Verify);
            this.Controls.Add(this.Label_Title);
            this.Controls.Add(this.Button_Verify);
            this.Controls.Add(this.Button_Action);
            this.Controls.Add(this.Label_Kxnrl);
            this.Controls.Add(this.HandleGroup);
            this.Controls.Add(this.ThemeSelector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "自定义主题补丁管理";
            this.Load += new System.EventHandler(this.ThemeForm_Load);
            this.HandleGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ComboBox ThemeSelector;
        private System.Windows.Forms.GroupBox HandleGroup;
        private System.Windows.Forms.ProgressBar Progressbar;
        private System.Windows.Forms.Label ActionLabel;
        private System.Windows.Forms.Label Label_Kxnrl;
        private System.Windows.Forms.Button Button_Action;
        private System.Windows.Forms.Button Button_Verify;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.Label Label_Verify;
    }
}