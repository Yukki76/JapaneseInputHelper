using Microsoft.Win32;
using Propeerties;
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
                Text = $"{AssemblyInfo.ProgramName} ver. {AssemblyInfo.Version}"
#if DEBUG
                + " (Debug Running)"
#endif
                ,
                Icon = AssemblyInfo.MainIcon,
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

            // Windowsがログオフやシャットダウンをしようとしているときに
            // このアプリを事前に終了する。

            // キーボードフック設定
            using (new jihdll.KeyBoardHook()) {

                SystemEvents.SessionEnding += sessionEnding;

                // タスクトレイ設定
                CreateNotifyIcon();
                // アプリケーションメッセージループ実行
                Application.Run();

                // Windows終了イベントをここで解除する(念の為)
                SystemEvents.SessionEnding -= sessionEnding;
            }

            void sessionEnding(object s, SessionEndingEventArgs e) { Application.Exit(); }
        }

        // メンバ変数(Private)
        private static Forms.ABoutDialog AboutDlg;

    }
}
