#pragma once

using namespace System;
using namespace System::Diagnostics;
using namespace System::Windows::Forms;

namespace jihdll {
	HHOOK g_HookId;
	HOOKPROC g_HookProc;
	bool g_bCtrlPressed;
	bool g_bEnteringKana;
	int InputsNumber;

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
