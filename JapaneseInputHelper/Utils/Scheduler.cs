using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using JapaneseInputHelper.Properties;
using TaskScheduler;

namespace Utils {
    internal class Scheduler : IDisposable {
        public string Author;
        public string Description;
        public string Name;
        public string ExecPath;
        public string WorkingDirectory;

        // メンバ変数(Private)
        private readonly ITaskService    TaskService;
        private readonly ITaskDefinition TaskDefinition;
        private          ITaskFolder     TaskFolder;
        private          bool            DisposedValue;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Scheduler() {
            TaskService = new TaskScheduler.TaskScheduler();
            TaskService.Connect(null, null, null, null);
            TaskDefinition = TaskService.NewTask(0);
        }

        /// <summary>
        /// 
        /// </summary>
        private void RegisterDescription() {
            var registrationInfo         = TaskDefinition.RegistrationInfo;
            registrationInfo.Author      = Author;      // 作成者
            registrationInfo.Description = Description; // 説明
        }

        /// <summary>
        /// 
        /// </summary>
        private void RegisterExecAction() {
            var actionCollection        = TaskDefinition.Actions;
            var execAction              = (IExecAction)actionCollection.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC);
            execAction.Path             = ExecPath;         // 動作確認用のバッチファイルを実行するように設定
            execAction.WorkingDirectory = WorkingDirectory; // 作業ディレクトリをexeのあるパスにしておく
        }

        /// <summary>
        /// 
        /// </summary>
        private void RegisterLogon() {
            var triggerCollection = TaskDefinition.Triggers;
            var logonTrigger      = (ILogonTrigger)triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON);
            logonTrigger.Enabled  = true; // トリガータブの設定
            logonTrigger.UserId   = $@"{Environment.UserDomainName}\{Environment.UserName}";
        }

        /// <summary>
        /// 
        /// </summary>
        private void RegisterTaskSettings() {
            var taskSettings                        = TaskDefinition.Settings;
            taskSettings.ExecutionTimeLimit         = "PT0S";       // タスクを終了するまでの時間(無効)
            taskSettings.MultipleInstances          = _TASK_INSTANCES_POLICY.TASK_INSTANCES_IGNORE_NEW;
            taskSettings.IdleSettings.IdleDuration  = string.Empty;
            taskSettings.IdleSettings.WaitTimeout   = string.Empty; // コンピュータをAC電源で使用してる場合のみタスクを開始する
            taskSettings.DisallowStartIfOnBatteries = false;        // コンピュータの電源をバッテリに切り替える場合は停止する。
            taskSettings.StopIfGoingOnBatteries     = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RegisterPrincipal() {
            var principal       = TaskDefinition.Principal;
            principal.UserId    = $"{Environment.UserDomainName}\\{Environment.UserName}"; // タスクの実行時に使うユーザーアカウント
            principal.LogonType = _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN;
            principal.RunLevel  = _TASK_RUNLEVEL.TASK_RUNLEVEL_HIGHEST;
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
                TaskFolder.RegisterTaskDefinition(
                    $"\\{Name}", TaskDefinition, (int)_TASK_CREATION.TASK_CREATE_OR_UPDATE, null, null, _TASK_LOGON_TYPE.TASK_LOGON_NONE, null);

                var message = "タスクスケジューラに登録しました。";
                var caption = Resources.ProgramName;
                var buttons = MessageBoxButtons.OK;
                var icon    = MessageBoxIcon.Information;
                MessageBox.Show(message, caption, buttons, icon);
            }
            catch (Exception ex) {
                var message = $"登録中にエラーが発生しました。\n{ex.Message}";
                var caption = Resources.ProgramName;
                var buttons = MessageBoxButtons.OK;
                var icon    = MessageBoxIcon.Error;
                MessageBox.Show(message, caption, buttons, icon);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (!DisposedValue) {
                if (disposing) {
                    if (TaskService != null) Marshal.ReleaseComObject(TaskService);
                    if (TaskFolder != null)  Marshal.ReleaseComObject(TaskFolder);
                }
                DisposedValue = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
