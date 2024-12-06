#include "pch.h"

#include "KeyBoardHook.h"

/// <summary>
/// コンストラクタ
/// </summary>
jihdll::KeyBoardHook::KeyBoardHook() {
	g_HookProc = HookProcedure;

	pin_ptr<const wchar_t> moduleName = PtrToStringChars(Process::GetCurrentProcess()->MainModule->ModuleName);
	g_HookId = SetWindowsHookEx(WH_KEYBOARD_LL, g_HookProc, GetModuleHandle(moduleName), 0);

	InputsNumber = sizeof(InputsTable) / sizeof(INPUT);
}

/// <summary>
/// デストラクタ
/// </summary>
jihdll::KeyBoardHook::~KeyBoardHook() {
	if (g_HookId != nullptr) {
		UnhookWindowsHookEx(g_HookId);
		g_HookId = nullptr;
	}
}

/// <summary>
/// キーボードフックコールバック関数
/// </summary>
/// <param name=""></param>
/// <param name=""></param>
/// <param name=""></param>
/// <returns></returns>
LRESULT jihdll::HookProcedure(int nCode, WPARAM wParam, LPARAM lParam) {
	DWORD vkCode = reinterpret_cast<LPKBDLLHOOKSTRUCT>(lParam)->vkCode;

	if (nCode >= 0 && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)) {

		// Ctrlキーが押された
		if (vkCode == VK_LCONTROL || vkCode == VK_RCONTROL)
			g_bCtrlFlag = true;

		// [/]キーが押された
		else if (g_bCtrlFlag && vkCode == VK_OEM_5) {
			g_bKanaFlag = true;
			unsigned int errRet = SendInput(InputsNumber, InputsTable, sizeof(INPUT));
			int errCode = GetLastError();
			if (errCode != 0)
				MessageBox::Show(L"エラーが発生しました\nエラーコード:" + errCode);
			return TRUE;
		}
	}
	else if (nCode >= 0 && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP)) {
		if (vkCode == VK_LCONTROL || vkCode == VK_RCONTROL) {
			if (!g_bKanaFlag)
				g_bCtrlFlag = false;
		}
		else if (vkCode == VK_OEM_5)
			g_bKanaFlag = false;
	}
	return CallNextHookEx(g_HookId, nCode, wParam, lParam);
}
