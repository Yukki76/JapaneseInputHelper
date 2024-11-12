using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows.Forms;

namespace JapaneseInputHelper {
    internal static class Program {

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() {
			// 二重起動禁止
			var app_mutex = new Mutex(false, "JapaneseInputHelper");
			if (!app_mutex.WaitOne(0, false)) return;

			// キーボードフック設定
			using (KeyboardHook keyboardHook = new KeyboardHook()) {

				// Windows終了イベント登録
				SystemEvents.SessionEnding += (sender, e) => { Application.Exit(); };

				// アプリケーションメッセージループ実行
				Application.Run();
			}
		}
	}
}
