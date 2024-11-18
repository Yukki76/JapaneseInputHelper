using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Controller {

    internal static class UnsafeNativeMethods {

        #region  定数

        public const int WH_KEYBOARD_LL = 0x000D;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public const int KEYEVENTF_KEYDOWN = 0x0000;
        public const int KEYEVENTF_KEYUP = 0x0002;
        public const int VK_LCONTROL = 162;    // 左Ctrlキー
        public const int VK_RCONTROL = 163;    // 右Ctrlキー
        public const int VK_OEM_AUTO = 243;    // 半角/全角キー
        //private const int VK_OEM_2 = 191;		// [/]キー
        public const int VK_OEM_5 = 0xDC;      // [\]キー
        public const uint INPUT_KEYBOARD = 1;

        public static int INPUT_SIZE;
        // これをしないとGCにより回収されてしまってCallbackOnCollectedDelegate例外で詰みます。
        public static HOOKPROC HookProc;
        public static IntPtr hookId;


        #endregion

        #region 関数

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr HOOKPROC(int nCode, int wParam,
            [MarshalAs(UnmanagedType.LPStruct), In] KBDLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SetWindowsHookEx(
            int idHook,
            [MarshalAs(UnmanagedType.FunctionPtr)] HOOKPROC lpfn,
            IntPtr hMod,
            uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode,
            int wParam,
            [MarshalAs(UnmanagedType.LPStruct), In] KBDLLHOOKSTRUCT lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr GetModuleHandle(
            [MarshalAs(UnmanagedType.LPWStr), In] string lpModuleName);

        [DllImport("user32.dll")]
        public static extern uint SendInput(uint cInputs, INPUT[] pInputs, int cbSize);

        #endregion

        #region 構造体・クラス

#pragma warning disable IDE0079 // 不要な抑制を削除します
        [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
#pragma warning restore IDE0079 // 不要な抑制を削除します
        [StructLayout(LayoutKind.Sequential)]
        internal class KBDLLHOOKSTRUCT {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public UIntPtr dwExtraInfo;
            public int dummy1;
            public int dummy2;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT {
            public uint type;
            public KEYBDINPUT ki;
        };

        #endregion

        #region インプットテーブル

        public static readonly INPUT[] Inputs = {
            new INPUT {
                type = INPUT_KEYBOARD,
                ki = new KEYBDINPUT() {
                    wVk = VK_LCONTROL,
                    wScan = 0,
                    dwFlags = KEYEVENTF_KEYUP,
                    time = 0,
                    dwExtraInfo = UIntPtr.Zero
                }
            },
            new INPUT {
                type = INPUT_KEYBOARD,
                ki = new KEYBDINPUT() {
                    wVk = VK_OEM_AUTO,
                    wScan = 0,
                    dwFlags = KEYEVENTF_KEYDOWN,
                    time = 0,
                    dwExtraInfo = UIntPtr.Zero
                }
            }
        };

        #endregion

    }
}