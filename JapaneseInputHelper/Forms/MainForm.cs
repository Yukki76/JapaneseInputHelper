using System;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using JapaneseInputHelper.Properties;
using Propeerties;

namespace Forms {
    public partial class MainForm : Form {
        private static Mutex AppMutex;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm() {
#if (DEBUG == null)
            if (!IsAdministrator())
                throw new Utils.IsAdministratorException("管理者権限で実行して下さい。");
#endif
            if (IsRunning())
                throw new Utils.MultipleRunningException();

            InitializeComponent();

            var debug = string.Empty;
#if DEBUG
            debug = " (Debug Running)";
#endif
            NotifyIcon.Text = $"{Resources.ProgramName} ver. {AssemblyInfo.AssemblyVersion} {debug}";
            ThemeControl.WindowThemeSelect.ChangeTheme(ContextMainMenu);
        }

        /// <summary>
        /// バージョン情報
        /// </summary>
        /// <param name="sender">イベントのソース</param>
        /// <param name="e">イベント データを格納していないオブジェクト</param>
        private void AboutBoxMenuItem_Click(object sender, EventArgs e) {
            using (var AboutDlg = new AboutDialog(IsAdministrator())) {
                AboutBoxMenuItem.Enabled = false;
                AboutDlg.ShowDialog();
                AboutBoxMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// [終了]処理
        /// </summary>
        /// <param name="sender">イベントのソース</param>
        /// <param name="e">イベント データを格納していないオブジェクト</param>
        private void ExitMenuItem_Click(object sender, EventArgs e) {
            AppMutex.ReleaseMutex();
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m) {
            const int WM_SETTINGCHANGE = 0x001A;
            if (m.Msg == WM_SETTINGCHANGE)
                ThemeControl.WindowThemeSelect.ChangeTheme(ContextMainMenu);
            base.WndProc(ref m);
        }

        /// <summary>
        /// 多重起動チェック
        /// </summary>
        /// true:  既に起動中<br/>
        /// false: 起動してない
        private bool IsRunning() {
            var debug = string.Empty;
#if DEBUG
            debug = "Debug";
#endif
            var programName = Resources.MutexName + debug;
            AppMutex = new Mutex(true, programName, out bool IsMutex);
            return !IsMutex;
        }

        /// <summary>
        /// 管理者モードで実行しているか確認
        /// </summary>
        /// <returns>
        /// true:  管理者モード<br/>
        /// false: 管理者モードでない
        /// </returns>
        public bool IsAdministrator()
            => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

    }
}
