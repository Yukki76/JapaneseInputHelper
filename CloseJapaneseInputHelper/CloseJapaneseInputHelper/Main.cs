using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace CloseJapaneseInputHelper {
    class Program {
        static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine("引数にreleaseかdebugかのどちらかを指定してください。");
                goto EndProcessing;
            }

            string pipeName = string.Empty;
            if (args[0] == "release") {
                pipeName = "JapaneseInputHelper";
            }
            else if (args[0] == "debug") {
                pipeName = "JapaneseInputHelper_Debug";
            }
            else {
                Console.WriteLine("引数にreleaseかdebugかのどちらかを指定してください。");
                goto EndProcessing;
            }

            bool error = false;
            Task.Run(async () => {
                try {
                    using (var stream = new NamedPipeClientStream(pipeName)) {
                        await stream.ConnectAsync(5000);

                        using (var writer = new StreamWriter(stream)) {
                            writer.AutoFlush = true;
                            await writer.WriteLineAsync("Dummy");
                        }
                    }
                }
                catch (TimeoutException e) {
                    Console.WriteLine($"{e.Message}");
                    error = true;
                }
            }).Wait();

            if (!error)
                Console.WriteLine("JapaneseInputHelperを終了させました。");

            EndProcessing:
            Console.ReadLine();
        }
    }
}
