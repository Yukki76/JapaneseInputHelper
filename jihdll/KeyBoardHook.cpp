#include "pch.h"
#include "KeyBoardHook.h"

/// <summary>
/// コンストラクタ
/// </summary>
jihdll::KeyBoardHook::KeyBoardHook() {
	g_HookProc = HookProcedure;

	pin_ptr<const wchar_t> moduleName = PtrToStringChars(Process::GetCurrentProcess()->MainModule->ModuleName);
	g_HookId = SetWindowsHookEx(WH_KEYBOARD_LL, g_HookProc, GetModuleHandle(moduleName), 0);
}

/// <summary>
/// デストラクタ
/// </summary>
jihdll::KeyBoardHook::~KeyBoardHook() {
	if (g_HookId != nullptr)
		UnhookWindowsHookEx(g_HookId);
}

/// <summary>
/// キーボードフックコールバック関数
/// </summary>
/// <param name=""></param>
/// <param name=""></param>
/// <param name=""></param>
/// <returns></returns>
LRESULT jihdll::HookProcedure(int nCode, WPARAM wParam, LPARAM lParam) {
	if (nCode < 0)
		return CallNextHookEx(g_HookId, nCode, wParam, lParam);

	switch (wParam) {
	case WM_KEYDOWN:
		// Ctrlキーが押された
		if (LPKBDLLHOOKSTRUCT(lParam)->vkCode == VK_LCONTROL) {
			g_bEnteringKana = true;
			g_bCtrlPressed = false;
		}
		// [/]キーが押された
		else if (LPKBDLLHOOKSTRUCT(lParam)->vkCode == VK_OEM_5) {
			if (!g_bCtrlPressed) break;

			g_bEnteringKana = true;

			INPUT InputsTable[2] = { 0 };
			InputsTable[0].type = INPUT_KEYBOARD;
			InputsTable[0].ki.wVk = VK_LCONTROL;
			InputsTable[0].ki.dwFlags = KEYEVENTF_KEYUP;
			InputsTable[1].type = INPUT_KEYBOARD;
			InputsTable[1].ki.wVk = VK_OEM_AUTO;
			SendInput(ARRAYSIZE(InputsTable), InputsTable, sizeof(INPUT));

			return true;
		}
	case WM_KEYUP:
		if (LPKBDLLHOOKSTRUCT(lParam)->vkCode == VK_LCONTROL)
			g_bCtrlPressed = g_bEnteringKana;
		else if (LPKBDLLHOOKSTRUCT(lParam)->vkCode == VK_OEM_5)
			g_bEnteringKana = false;
	}
	return CallNextHookEx(g_HookId, nCode, wParam, lParam);
}
