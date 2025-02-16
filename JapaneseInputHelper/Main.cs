using System;
using System.Windows.Forms;
using Controller;

namespace JapaneseInputHelper {
    internal static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (KeyboardHook keyboardHook = new KeyboardHook())
                    Application.Run(new Forms.MainForm());
            }
            catch (Utils.IsAdministratorException ex) {
                var message = ex.Message;
                var title   = Properties.Resources.ProgramName;
                var buttons = MessageBoxButtons.OK;
                var icon    = MessageBoxIcon.Information;
                MessageBox.Show(message, title, buttons, icon);
            }
            catch (Utils.MultipleRunningException) { }
        }
    }
}
