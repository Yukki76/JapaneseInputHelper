using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Controller {
    public class KeyboardHook : IDisposable {
        private const int WH_KEYBOARD_LL = 0x000D;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int KEYEVENTF_KEYDOWN = 0x0000;
        private const int KEYEVENTF_KEYUP = 0x0002;
        private const int VK_LCONTROL = 162;    // 左Ctrlキー
        private const int VK_RCONTROL = 163;    // 右Ctrlキー
        private const int VK_OEM_AUTO = 243;    // 半角/全角キー
        //private const int VK_OEM_2 = 191;		// [/]キー
        private const int VK_OEM_5 = 0xDC;      // [\]キー

        private static readonly int INPUT_SIZE = Marshal.SizeOf(typeof(Native.INPUT));
        private static readonly IntPtr TRUE = new IntPtr(1);
        private IntPtr hookId = IntPtr.Zero;
        // これをしないとGCにより回収されてしまってCallbackOnCollectedDelegate例外で詰みます。
        private readonly Native.Methods.HOOKPROC HookProc = null;

        private static readonly Native.INPUT[] Inputs = {
            new Native.INPUT {type = Native.INPUT_TYPE.INPUT_KEYBOARD,
                ki = new Native.KEYBDINPUT() {wVk = VK_LCONTROL, wScan = 0, dwFlags = KEYEVENTF_KEYUP, time = 0, dwExtraInfo = UIntPtr.Zero}},
            new Native.INPUT {type = Native.INPUT_TYPE.INPUT_KEYBOARD,
                ki = new Native.KEYBDINPUT() {wVk = VK_OEM_AUTO, wScan = 0, dwFlags = KEYEVENTF_KEYDOWN, time = 0, dwExtraInfo = UIntPtr.Zero}}
        };

        private static bool CtrlFlag = false;
        private static bool KanaFlag = false;
        private bool disposedValue;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KeyboardHook() {
            if (hookId == IntPtr.Zero) {
                // ※ この時に地味に重要なのがデリゲートをフィールド変数に置くことです。
                // これをしないとGCにより回収されてしまってCallbackOnCollectedDelegate例外で詰みます。
                // 【以下参照】
                // https://aonasuzutsuki.hatenablog.jp/entry/2018/10/15/170958
                // https://lets-csharp.com/mouse-hook/
                HookProc = HookProcedure;
                using (var curProcess = Process.GetCurrentProcess()) {
                    using (ProcessModule curModule = curProcess.MainModule) {
                        hookId = Native.Methods.SetWindowsHookEx(
                            WH_KEYBOARD_LL, HookProc, Native.Methods.GetModuleHandle(curModule.ModuleName), 0);
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
        public IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam) {

            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)) {
                var kb = (Native.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Native.KBDLLHOOKSTRUCT));
                var vkCode = (int)kb.vkCode;

#if DEBUG
                Utils.Debug.Message($"vkCode: {vkCode}");
#endif

                // Ctrlキーが押された
                if (vkCode == VK_LCONTROL || vkCode == VK_RCONTROL) {
                    KanaFlag = false;
                    CtrlFlag = true;
                }
                // [/]キーが押された
                else if (CtrlFlag && vkCode == VK_OEM_5) {
                    KanaFlag = true;
                    Native.Methods.SendInput((uint)Inputs.Length, Inputs, INPUT_SIZE);
                    return TRUE;
                }
            }
            else if (nCode >= 0 && (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)) {
                var kb = (Native.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Native.KBDLLHOOKSTRUCT));
                var vkCode = (int)kb.vkCode;

                if (vkCode == VK_LCONTROL || vkCode == VK_RCONTROL) {
                    if (!KanaFlag)
                        CtrlFlag = false;
                }
                else if (vkCode == VK_OEM_5) {
                    KanaFlag = false;
                }
            }
            return Native.Methods.CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// リソース破棄
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    Native.Methods.UnhookWindowsHookEx(hookId);
                    hookId = IntPtr.Zero;
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        /// <summary>
        /// リソース破棄
        /// </summary>
        public void Dispose() {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}