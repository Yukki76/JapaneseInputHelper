using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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

            logoPictureBox.Image = JapaneseInputHelper.Properties.Resources.MainIcon.ToBitmap();

            // バージョン情報表示
            LblVersion.Text = $"{JapaneseInputHelper.Properties.Resources.ProgramName} ver. {AssemblyVersion}";

            // Copyright情報取得
            LblCopyright.Text = AssemblyCopyright;

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
                scheduler.Description = AssemblyDescription;
                scheduler.Name = AssemblyProduct;
                scheduler.ExecPath = Application.ExecutablePath;
                scheduler.WorkingDirectory = Application.StartupPath;

                scheduler.RegisterDefinition();
            }
        }

        #region プロパティ(Private)

        /// <summary>
        /// プログラムの詳細な説明
        /// </summary>
        private string AssemblyDescription {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
                    typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// 著作権情報
        /// </summary>
        private string AssemblyCopyright {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
                    typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// 製品名情報
        /// </summary>
        private string AssemblyProduct {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
                    typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// 製品のバージョン情報
        /// </summary>
        private string AssemblyVersion {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        #endregion

    }
}
