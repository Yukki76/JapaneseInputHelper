namespace Forms {
	partial class ABoutDialog {
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
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.LblVersion = new System.Windows.Forms.Label();
            this.LblCopyright = new System.Windows.Forms.Label();
            this.LblRuntimeInfo = new System.Windows.Forms.Label();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnStartup = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Location = new System.Drawing.Point(12, 12);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(40, 40);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 0;
            this.logoPictureBox.TabStop = false;
            // 
            // LblVersion
            // 
            this.LblVersion.AutoSize = true;
            this.LblVersion.Location = new System.Drawing.Point(59, 12);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(38, 20);
            this.LblVersion.TabIndex = 1;
            this.LblVersion.Text = "Title";
            // 
            // LblCopyright
            // 
            this.LblCopyright.AutoSize = true;
            this.LblCopyright.Location = new System.Drawing.Point(59, 36);
            this.LblCopyright.Name = "LblCopyright";
            this.LblCopyright.Size = new System.Drawing.Size(110, 20);
            this.LblCopyright.TabIndex = 2;
            this.LblCopyright.Text = "CopyrightLabel";
            // 
            // LblRuntimeInfo
            // 
            this.LblRuntimeInfo.AutoSize = true;
            this.LblRuntimeInfo.Location = new System.Drawing.Point(59, 64);
            this.LblRuntimeInfo.Name = "LblRuntimeInfo";
            this.LblRuntimeInfo.Size = new System.Drawing.Size(50, 20);
            this.LblRuntimeInfo.TabIndex = 3;
            this.LblRuntimeInfo.Text = "label1";
            // 
            // BtnOk
            // 
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnOk.Location = new System.Drawing.Point(379, 98);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(100, 35);
            this.BtnOk.TabIndex = 4;
            this.BtnOk.Text = "&OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnStartup
            // 
            this.BtnStartup.Enabled = false;
            this.BtnStartup.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnStartup.Location = new System.Drawing.Point(141, 98);
            this.BtnStartup.Name = "BtnStartup";
            this.BtnStartup.Size = new System.Drawing.Size(232, 35);
            this.BtnStartup.TabIndex = 5;
            this.BtnStartup.Text = "タスクスケジューラにこのアプリを登録";
            this.BtnStartup.UseVisualStyleBackColor = true;
            this.BtnStartup.Click += new System.EventHandler(this.BtnStartup_Click);
            // 
            // ABoutDialogNormal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnOk;
            this.ClientSize = new System.Drawing.Size(491, 145);
            this.Controls.Add(this.BtnStartup);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.LblRuntimeInfo);
            this.Controls.Add(this.LblCopyright);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.logoPictureBox);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ABoutDialogNormal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "バージョン情報";
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox logoPictureBox;
		private System.Windows.Forms.Label LblVersion;
		private System.Windows.Forms.Label LblCopyright;
		private System.Windows.Forms.Label LblRuntimeInfo;
		private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnStartup;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}