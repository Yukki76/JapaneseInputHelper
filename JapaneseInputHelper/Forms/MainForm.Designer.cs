namespace Forms {
    partial class MainForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AboutBoxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.ContextMainMenu;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "notifyIcon1";
            this.NotifyIcon.Visible = true;
            // 
            // ContextMainMenu
            // 
            this.ContextMainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ContextMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutBoxMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.ContextMainMenu.Name = "ContextMainMenu";
            this.ContextMainMenu.ShowImageMargin = false;
            this.ContextMainMenu.ShowItemToolTips = false;
            this.ContextMainMenu.Size = new System.Drawing.Size(184, 74);
            // 
            // AboutBoxMenuItem
            // 
            this.AboutBoxMenuItem.Name = "AboutBoxMenuItem";
            this.AboutBoxMenuItem.Size = new System.Drawing.Size(183, 32);
            this.AboutBoxMenuItem.Text = "バージョン情報(&A)";
            this.AboutBoxMenuItem.Click += new System.EventHandler(this.AboutBoxMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(183, 32);
            this.ExitMenuItem.Text = "終了(&A)";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 80);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Japanese Input Helper";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ContextMainMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip ContextMainMenu;
        private System.Windows.Forms.ToolStripMenuItem AboutBoxMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
    }
}