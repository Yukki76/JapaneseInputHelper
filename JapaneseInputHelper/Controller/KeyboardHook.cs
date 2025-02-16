using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Controller {
    abstract class Native {

        #region 関数

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate IntPtr HOOKPROC(int nCode, int wParam,
            [MarshalAs(UnmanagedType.LPStruct), In] KBDLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern IntPtr SetWindowsHookEx(
            int idHook,
            [MarshalAs(UnmanagedType.FunctionPtr)] HOOKPROC lpfn,
            IntPtr hMod,
            uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        protected static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        protected static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode,
            int wParam,
            [MarshalAs(UnmanagedType.LPStruct), In] KBDLLHOOKSTRUCT lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        protected static extern IntPtr GetModuleHandle(
            [MarshalAs(UnmanagedType.LPWStr), In] string lpModuleName);

        [DllImport("user32.dll")]
        protected static extern uint SendInput(uint cInputs, INPUT[] pInputs, int cbSize);

        #endregion

        #region  定数

        protected const int  WH_KEYBOARD_LL    = 0x000D;
        protected const int  WM_KEYDOWN        = 0x0100;
        protected const int  WM_KEYUP          = 0x0101;
        protected const int  KEYEVENTF_KEYDOWN = 0x0000;
        protected const int  KEYEVENTF_KEYUP   = 0x0002;
        protected const int  VK_LCONTROL       = 162;       // 左Ctrlキー
        protected const int  VK_OEM_AUTO       = 243;       // 半角/全角キー
        protected const int  VK_OEM_5          = 0xDC;      // [\]キー
        protected const uint INPUT_KEYBOARD    = 1;

        protected static int INPUT_SIZE;
        // これをしないとGCにより回収されてしまってCallbackOnCollectedDelegate例外で詰みます。
        protected static HOOKPROC HookProc;
        protected static IntPtr hookId;

        #endregion

        #region 構造体・クラス

        [StructLayout(LayoutKind.Sequential)]
        protected class KBDLLHOOKSTRUCT {
            public uint    vkCode;
            public uint    scanCode;
            public uint    flags;
            public uint    time;
            public UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct KEYBDINPUT {
            public ushort  wVk;
            public ushort  wScan;
            public uint    dwFlags;
            public uint    time;
            public UIntPtr dwExtraInfo;
            public int     dummy1;
            public int     dummy2;
        };

        [StructLayout(LayoutKind.Sequential)]
        protected struct INPUT {
            public uint       type;
            public KEYBDINPUT ki;
        };

        #endregion

        #region インプットテーブル

        protected static readonly INPUT[] Inputs = {
                new INPUT {
                    type            = INPUT_KEYBOARD,
                    ki = new KEYBDINPUT() {
                        wVk         = VK_LCONTROL,
                        wScan       = 0,
                        dwFlags     = KEYEVENTF_KEYUP,
                        time        = 0,
                        dwExtraInfo = UIntPtr.Zero
                    }
                },
                new INPUT {
                    type            = INPUT_KEYBOARD,
                    ki = new KEYBDINPUT() {
                        wVk         = VK_OEM_AUTO,
                        wScan       = 0,
                        dwFlags     = KEYEVENTF_KEYDOWN,
                        time        = 0,
                        dwExtraInfo = UIntPtr.Zero
                    }
                }
            };

        #endregion

    }

    internal class KeyboardHook : Native, IDisposable {
        private static bool CtrlPressed = false;
        private static bool EnteringKana = false;
        private bool        disposedValue;

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
                INPUT_SIZE = Marshal.SizeOf(typeof(INPUT));

                using (var curProcess = Process.GetCurrentProcess()) {
                    using (ProcessModule curModule = curProcess.MainModule) {
                        hookId = SetWindowsHookEx(
                            WH_KEYBOARD_LL, HookProc,
                            GetModuleHandle(curModule.ModuleName), 0);
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
            if (nCode < 0)
                return CallNextHookEx(hookId, nCode, wParam, lParam);

            switch (wParam) {
                // キーが押された
                case WM_KEYDOWN:
                    // Ctrlキーが押された
                    if (lParam.vkCode == VK_LCONTROL) {
                        EnteringKana = false;
                        CtrlPressed = true;
                    }
                    // \キーが押された
                    else if (lParam.vkCode == VK_OEM_5) {
                        if (!CtrlPressed)
                            break;
                        EnteringKana = true;
                        SendInput((uint)Inputs.Length, Inputs, INPUT_SIZE);
                        return new IntPtr(1);
                    }
                    break;
                // キーが離れた
                case WM_KEYUP:
                    // Ctrlキーが離れた
                    if (lParam.vkCode == VK_LCONTROL)
                        CtrlPressed = EnteringKana;
                    // \キーが離れた
                    else if (lParam.vkCode == VK_OEM_5)
                        EnteringKana = false;
                    break;
            }
            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// リソース破棄
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    UnhookWindowsHookEx(hookId);
                    hookId = IntPtr.Zero;
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