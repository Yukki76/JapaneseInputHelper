using System;
using System.Runtime.InteropServices;

namespace Controller.Native {

	public static class Methods {
		public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll")]
		public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll")]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("user32.dll")]
		public static extern uint SendInput(uint cInputs, INPUT[] pInputs, int cbSize);
	}
}