using System;
using TaskScheduler;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utils {
    internal class Scheduler : IDisposable {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Scheduler() {
            TaskService = new TaskScheduler.TaskScheduler();
            TaskService.Connect(null, null, null, null);
            TaskDefinition = TaskService.NewTask(0);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        private void RegisterDescription() {
            IRegistrationInfo registrationInfo = TaskDefinition.RegistrationInfo;

            // 作成者
            registrationInfo.Author = this.Author;
            // 説明
            registrationInfo.Description = this.Description;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        private void RegisterExecAction() {
            IActionCollection actionCollection = TaskDefinition.Actions;
            IExecAction execAction = (IExecAction)actionCollection.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC);
            // 動作確認用のバッチファイルを実行するように設定
            execAction.Path = this.ExecPath;
            // 作業ディレクトリをexeのあるパスにしておく
            execAction.WorkingDirectory = this.WorkingDirectory;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        private void RegisterLogon() {
            ITriggerCollection triggerCollection = TaskDefinition.Triggers;
            ILogonTrigger logonTrigger = (ILogonTrigger)triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON);
            // トリガータブの設定
            logonTrigger.Enabled = true;
            logonTrigger.UserId = $@"{Environment.UserDomainName}\{Environment.UserName}";
        }

        ///// <summary>
        ///// 
        ///// </summary>
        private void RegisterTaskSettings() {
            ITaskSettings taskSettings = TaskDefinition.Settings;
            // タスクを終了するまでの時間(無効)
            taskSettings.ExecutionTimeLimit = "PT0S";
            taskSettings.MultipleInstances = _TASK_INSTANCES_POLICY.TASK_INSTANCES_IGNORE_NEW;
            taskSettings.IdleSettings.IdleDuration = string.Empty;
            taskSettings.IdleSettings.WaitTimeout = string.Empty;
            // コンピュータをAC電源で使用してる場合のみタスクを開始する
            taskSettings.DisallowStartIfOnBatteries = false;
            // コンピュータの電源をバッテリに切り替える場合は停止する。
            taskSettings.StopIfGoingOnBatteries = false;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        private void RegisterPrincipal() {
            IPrincipal principal = TaskDefinition.Principal;
            // タスクの実行時に使うユーザーアカウント
            principal.UserId = $@"{Environment.UserDomainName}\{Environment.UserName}";
            principal.LogonType = _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN;
            principal.RunLevel = _TASK_RUNLEVEL.TASK_RUNLEVEL_HIGHEST;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RegisterDefinition() {
            try {
                RegisterDescription();
                RegisterExecAction();
                RegisterLogon();
                RegisterTaskSettings();
                RegisterPrincipal();

                TaskFolder = TaskService.GetFolder("\\");
                TaskFolder.RegisterTaskDefinition($@"\{Name}", TaskDefinition,
                    (int)_TASK_CREATION.TASK_CREATE_OR_UPDATE, null, null,
                    _TASK_LOGON_TYPE.TASK_LOGON_NONE, null);
            }
            catch (Exception ex) {
                if (ex is UnauthorizedAccessException)
                    MessageBox.Show(
                        "管理者モードで実行してください。",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                else
                    MessageBox.Show(
                        $"登録中にエラーが発生しました。\n{ex.Message}",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(
                "タスクスケジューラに登録しました。",
                JapaneseInputHelper.Properties.Resources.ProgramName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (!DisposedValue) {
                if (disposing) {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    if (TaskService != null)
                        Marshal.ReleaseComObject(TaskService);
                    if (TaskFolder != null)
                        Marshal.ReleaseComObject(TaskFolder);
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                DisposedValue = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public string Author = string.Empty;
        public string Description = string.Empty;
        public string Name = string.Empty;
        public string ExecPath = string.Empty;
        public string WorkingDirectory = string.Empty;

        // メンバ変数(Private)
        private readonly ITaskService TaskService;
        private readonly ITaskDefinition TaskDefinition;
        private ITaskFolder TaskFolder;
        private bool DisposedValue;

    }
}
