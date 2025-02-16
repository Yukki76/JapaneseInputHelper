using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using JapaneseInputHelper.Properties;
using Propeerties;
using ThemeControl;

namespace Forms {
    public partial class AboutDialog : Form {
        public class MyControl {
            public Form             Form;
            public Button           BtnOk;
            public Button           BtnStartup;
            public TextBox          TbDescription;
        }

        private readonly MyControl Control;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AboutDialog(bool admin) {
            InitializeComponent();
            LblVersion.Text     = $"{Resources.ProgramName} ver. {AssemblyInfo.AssemblyVersion}"; // バージョン情報表示
            LblVersion.Text    += admin ? " (管理者モード)" : "";
            LblCopyright.Text   = AssemblyInfo.AssemblyCopyright;                                 // Copyright情報取得
            TbDescription.Text  = AssemblyInfo.AssemblyDescription;                               // 説明情報
            LblRuntimeInfo.Text = RuntimeInformation.FrameworkDescription;                        // 実行環境のランタイム情報

            this.Control = new MyControl {
                TbDescription   = TbDescription,
                BtnOk           = BtnOk,
                BtnStartup      = BtnStartup,
                Form            = this,
            };
            WindowThemeSelect.ChangeTheme(this.Control);
        }

        /// <summary>
        /// OKボタン押下
        /// </summary>
        /// <param name="sender">イベントのソース</param>
        /// <param name="e">イベント データを格納していないオブジェクト</param>
        private void BtnOk_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// タスクスケジューラにこのアプリを登録する
        /// </summary>
        /// <param name="sender">イベントのソース</param>
        /// <param name="e">イベント データを格納していないオブジェクト</param>
        private void BtnStartup_Click(object sender, EventArgs e) {
            using (var scheduler = new Utils.Scheduler()) {
                scheduler.Author           = "Yukki";
                scheduler.Description      = AssemblyInfo.AssemblyDescription;
                scheduler.Name             = AssemblyInfo.AssemblyProduct;
                scheduler.ExecPath         = Application.ExecutablePath;
                scheduler.WorkingDirectory = Application.StartupPath;
                scheduler.RegisterDefinition();
            }
        }

    }
}
