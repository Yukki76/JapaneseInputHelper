using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace JapaneseInputHelper {
    internal static class Program {

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
            if (!app_mutex.WaitOne(0, false)) return;

            // キーボードフック設定
            using (Controller.KeyboardHook keyboardHook = new Controller.KeyboardHook()) {

                // アプリケーションメッセージループ実行
                Task.Run(async () => {
                    using (var stream = new NamedPipeServerStream(programName)) {
                        await stream.WaitForConnectionAsync();
                        using (var reader = new StreamReader(stream)) {
                            while (stream.IsConnected) {
                                await reader.ReadLineAsync();
                            }
                        }
                    }
                }).Wait();
            }
        }
    }
}
