using System;
using System.Runtime.InteropServices;

namespace JapaneseInputHelper {

	[StructLayout(LayoutKind.Sequential)]
	public struct KBDLLHOOKSTRUCT {
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
		public INPUT_TYPE type;
		public KEYBDINPUT ki;
	};
}