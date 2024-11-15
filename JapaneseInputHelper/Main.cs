using System;
using System.Threading;
using System.Windows.Forms;

namespace JapaneseInputHelper {
    internal static class Program {
        /// <summary>
        /// タスクトレイ設定
        /// </summary>
        static void CreateNotifyIcon() {
            var contextMainMenu = new ContextMenuStrip();
            contextMainMenu.Items.Add("バージョン情報(&A)…", null, (s, e) => {
                if (AboutDlg != null && AboutDlg.IsWindow) {
                    AboutDlg.Activate();
                    return;
                }
                using (AboutDlg = new Forms.ABoutDialog())
                    AboutDlg.ShowDialog();
            });
            contextMainMenu.Items.Add(new ToolStripSeparator());
            contextMainMenu.Items.Add("終了(&X)", null, (s, e) => {
                Application.Exit();
            });

            new NotifyIcon {
                Icon = JapaneseInputHelper.Properties.Resources.MainIcon,
                ContextMenuStrip = contextMainMenu,
                Visible = true
            };
        }

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            string programName = string.Empty;

            // 二重起動禁止
#if DEBUG
            programName = "JapaneseInputHelper_Debug";
#else
            programName = "JapaneseInputHelper";
#endif
            var app_mutex = new Mutex(false, programName);
            if (!app_mutex.WaitOne(0, false))
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // キーボードフック設定
            using (Controller.KeyboardHook keyboardHook = new Controller.KeyboardHook()) {
                // タスクトレイ設定
                CreateNotifyIcon();
                // アプリケーションメッセージループ実行
                Application.Run();
            }
        }

        // メンバ変数(Private)
        private static Forms.ABoutDialog AboutDlg;

    }
}
