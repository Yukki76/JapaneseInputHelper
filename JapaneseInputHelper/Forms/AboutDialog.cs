using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Propeerties;

namespace Forms {
    public partial class ABoutDialog : Form {

        /// <summary>
        /// ダイアログ表示フラグ
        /// 表示された：true
        /// 閉じている：false
        /// </summary>
        public bool IsWindow = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ABoutDialog() {
            InitializeComponent();

            logoPictureBox.Image = AssemblyInfo.MainIcon.ToBitmap();

            // バージョン情報表示
            LblVersion.Text = $"{AssemblyInfo.ProgramName} ver. {AssemblyInfo.Version}";

            // Copyright情報取得
            LblCopyright.Text = AssemblyInfo.Copyright;

            // 実行環境のランタイム情報
            LblRuntimeInfo.Text = RuntimeInformation.FrameworkDescription;
        }

        /// <summary>
        /// フォーム読み込み時
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            if (Utils.Common.IsAdministrator()) {
                LblVersion.Text += " " + "(管理者モード)";
                BtnStartup.Enabled = true;
            }

            IsWindow = true;
        }

        /// <summary>
        /// フォームを閉じる時
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);

            IsWindow = false;
        }

        /// <summary>
        /// OKボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e) {
            Close();
        }

        /// <summary>
        /// スタートアップにこのアプリを登録する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartup_Click(object sender, EventArgs e) {
            using (var scheduler = new Utils.Scheduler()) {
                scheduler.Author = $@"{Environment.UserDomainName}\{Environment.UserName}";
                scheduler.Description = AssemblyInfo.Description;
                scheduler.Name = AssemblyInfo.Product;
                scheduler.ExecPath = Application.ExecutablePath;
                scheduler.WorkingDirectory = Application.StartupPath;

                scheduler.RegisterDefinition();
            }
        }
    }
}
