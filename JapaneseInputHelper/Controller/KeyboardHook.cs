using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Native = Controller.UnsafeNativeMethods;

namespace Controller {
    public class KeyboardHook : IDisposable {
        private static bool CtrlFlag = false;
        private static bool KanaFlag = false;
        private bool disposedValue;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KeyboardHook() {
            if (Native.hookId == IntPtr.Zero) {
                // ※ この時に地味に重要なのがデリゲートをフィールド変数に置くことです。
                // これをしないとGCにより回収されてしまってCallbackOnCollectedDelegate例外で詰みます。
                // 【以下参照】
                // https://aonasuzutsuki.hatenablog.jp/entry/2018/10/15/170958
                // https://lets-csharp.com/mouse-hook/
                Native.HookProc = HookProcedure;
                Native.INPUT_SIZE = Marshal.SizeOf(typeof(Native.INPUT));

                using (var curProcess = Process.GetCurrentProcess()) {
                    using (ProcessModule curModule = curProcess.MainModule) {
                        Native.hookId = Native.SetWindowsHookEx(
                            Native.WH_KEYBOARD_LL, Native.HookProc,
                            Native.GetModuleHandle(curModule.ModuleName), 0);
                    }
                }
            }
        }

        /// <summary>
        /// ウィンドウプロシージャ
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr HookProcedure(int nCode, int wParam, Native.KBDLLHOOKSTRUCT lParam) {

            if (nCode >= 0 && (wParam == Native.WM_KEYDOWN || wParam == Native.WM_SYSKEYDOWN)) {
                var vkCode = lParam.vkCode;
#if DEBUG
                Utils.Debug.Message($"vkCode: {vkCode}");
#endif

                // Ctrlキーが押された
                if (vkCode == Native.VK_LCONTROL || vkCode == Native.VK_RCONTROL) {
                    KanaFlag = false;
                    CtrlFlag = true;
                }
                // [/]キーが押された
                else if (CtrlFlag && vkCode == Native.VK_OEM_5) {
                    KanaFlag = true;
                    uint errRet = Native.SendInput((uint)Native.Inputs.Length, Native.Inputs, Native.INPUT_SIZE);
                    int errCode = Marshal.GetLastWin32Error();
                    if (errCode != 0)
                        System.Windows.Forms.MessageBox.Show($"エラーが発生しました\nエラーコード: {errCode}");

                    return new IntPtr(1);
                }
            }
            else if (nCode >= 0 && (wParam == Native.WM_KEYUP || wParam == Native.WM_SYSKEYUP)) {
                var vkCode = lParam.vkCode;

                if (vkCode == Native.VK_LCONTROL || vkCode == Native.VK_RCONTROL) {
                    if (!KanaFlag)
                        CtrlFlag = false;
                }
                else if (vkCode == Native.VK_OEM_5)
                    KanaFlag = false;
            }
            return Native.CallNextHookEx(Native.hookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// リソース破棄
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    Native.UnhookWindowsHookEx(Native.hookId);
                    Native.hookId = IntPtr.Zero;
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、
                // ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        /// <summary>
        /// リソース破棄
        /// </summary>
        public void Dispose() {
            // このコードを変更しないでください。クリーンアップ コードを
            // 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}