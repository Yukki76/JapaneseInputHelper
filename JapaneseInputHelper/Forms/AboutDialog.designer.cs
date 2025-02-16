namespace Forms {
    partial class AboutDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.LblVersion = new System.Windows.Forms.Label();
            this.LblCopyright = new System.Windows.Forms.Label();
            this.LblRuntimeInfo = new System.Windows.Forms.Label();
            this.TbDescription = new System.Windows.Forms.TextBox();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnStartup = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 288F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 278F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LblVersion, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LblCopyright, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LblRuntimeInfo, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.TbDescription, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.BtnOk, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.BtnStartup, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(674, 382);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.logoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = global::JapaneseInputHelper.Properties.Resources.Logo;
            this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel1.SetRowSpan(this.logoPictureBox, 5);
            this.logoPictureBox.Size = new System.Drawing.Size(282, 376);
            this.logoPictureBox.TabIndex = 2;
            this.logoPictureBox.TabStop = false;
            // 
            // LblVersion
            // 
            this.LblVersion.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.LblVersion, 2);
            this.LblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblVersion.Location = new System.Drawing.Point(291, 0);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(380, 30);
            this.LblVersion.TabIndex = 3;
            this.LblVersion.Text = "製品名";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblCopyright
            // 
            this.LblCopyright.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.LblCopyright, 2);
            this.LblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblCopyright.Location = new System.Drawing.Point(291, 30);
            this.LblCopyright.Name = "LblCopyright";
            this.LblCopyright.Size = new System.Drawing.Size(380, 30);
            this.LblCopyright.TabIndex = 3;
            this.LblCopyright.Text = "著作権";
            this.LblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblRuntimeInfo
            // 
            this.LblRuntimeInfo.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.LblRuntimeInfo, 2);
            this.LblRuntimeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblRuntimeInfo.Location = new System.Drawing.Point(291, 60);
            this.LblRuntimeInfo.Name = "LblRuntimeInfo";
            this.LblRuntimeInfo.Size = new System.Drawing.Size(380, 30);
            this.LblRuntimeInfo.TabIndex = 3;
            this.LblRuntimeInfo.Text = ".NETのバージョン";
            this.LblRuntimeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TbDescription
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TbDescription, 2);
            this.TbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbDescription.Enabled = false;
            this.TbDescription.Location = new System.Drawing.Point(291, 93);
            this.TbDescription.Multiline = true;
            this.TbDescription.Name = "TbDescription";
            this.TbDescription.ReadOnly = true;
            this.TbDescription.Size = new System.Drawing.Size(380, 245);
            this.TbDescription.TabIndex = 4;
            // 
            // BtnOk
            // 
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnOk.Location = new System.Drawing.Point(569, 344);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(102, 35);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "&OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnStartup
            // 
            this.BtnStartup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnStartup.Location = new System.Drawing.Point(291, 344);
            this.BtnStartup.Name = "BtnStartup";
            this.BtnStartup.Size = new System.Drawing.Size(272, 35);
            this.BtnStartup.TabIndex = 1;
            this.BtnStartup.Text = "タスクスケジューラにアプリを登録";
            this.BtnStartup.UseVisualStyleBackColor = true;
            this.BtnStartup.Click += new System.EventHandler(this.BtnStartup_Click);
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnOk;
            this.ClientSize = new System.Drawing.Size(698, 402);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "バージョン情報";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button BtnStartup;
        private System.Windows.Forms.Label LblVersion;
        private System.Windows.Forms.Label LblCopyright;
        private System.Windows.Forms.Label LblRuntimeInfo;
        private System.Windows.Forms.TextBox TbDescription;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.PictureBox logoPictureBox;
    }
}