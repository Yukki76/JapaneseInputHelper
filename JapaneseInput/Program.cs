using JapaneseInput.Properties;
using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows.Forms;

namespace JapaneseInputHelper {
	internal static class Program {

		/// <summary>
		/// タスクトレイ設定
		/// </summary>
		static void CreateNotifyIcon() {
			new NotifyIcon {
				Icon = Resources.MainIcon,
				Text = "Japanese Input Helper",
				ContextMenuStrip = ContextMenu(),
				Visible = true
			};
		}

		/// <summary>
		/// タスクトレイメニュー設定
		/// </summary>
		/// <returns></returns>
		static ContextMenuStrip ContextMenu() {
			var menu = new ContextMenuStrip();
			menu.Items.Add("終了(&X)", null, (s, e) => {
				Application.Exit();
			});
			return menu;
		}

		/// <summary>
		/// Windows終了イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void SysetmEvents_SessionEnding(object sender, SessionEndingEventArgs e) {
			Application.Exit();
		}


		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() {
			// 二重起動禁止
			var app_mutex = new Mutex(false, "JapaneseInputHelper");
			if (!app_mutex.WaitOne(0, false)) return;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// キーボードフック設定
			using (KeyboardHook keyboardHook = new KeyboardHook()) {
				// Windows終了イベント登録
				SystemEvents.SessionEnding += new SessionEndingEventHandler(SysetmEvents_SessionEnding);
				// タスクトレイ設定
				CreateNotifyIcon();
				// アプリケーションメッセージループ実行
				Application.Run();
			}
		}
	}
}
