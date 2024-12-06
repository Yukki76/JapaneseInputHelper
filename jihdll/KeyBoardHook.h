#pragma once

using namespace System;
using namespace System::Diagnostics;
using namespace System::Windows::Forms;

namespace jihdll {
	HHOOK g_HookId;
	HOOKPROC g_HookProc;
	bool g_bCtrlFlag;
	bool g_bKanaFlag;
	int InputsNumber;

	INPUT InputsTable[] {
		{ INPUT_KEYBOARD, { VK_LCONTROL, 0x00, KEYEVENTF_KEYUP, 0x00, 0x00L } },
		{ INPUT_KEYBOARD, { VK_OEM_AUTO, 0x00, 0x00, 0x00, 0x00L } }
	};

	// キーボードフックコールバック関数
	LRESULT CALLBACK HookProcedure(int, WPARAM, LPARAM);

	/// <summary>
	/// キーボードフッククラス
	/// </summary>
	public ref class KeyBoardHook {
	public:
		KeyBoardHook();
		~KeyBoardHook();
	};
}
