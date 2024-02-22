using System;
using System.Timers;
using Microsoft.Win32;
using Timer = System.Timers.Timer;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace JapaneseInputHelper {
	internal static class Program {
		private static readonly Timer Timer = new Timer();

		/// <summary>
		/// タイマー設定
		/// </summary>
		private static void SetTimer() {
			Timer.Interval = 100;
			Timer.Elapsed += TimerEvent;
			Timer.Start();
		}

		/// <summary>
		/// タイマーイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void TimerEvent(object sender, ElapsedEventArgs e) {
			string appPath = AppDomain.CurrentDomain.BaseDirectory;
			string filepath = appPath + "endflag";

			if (File.Exists(filepath))
				Application.Exit();
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
			KeyboardHook keyboardHook = new KeyboardHook();
			// Windows終了イベント登録
			SystemEvents.SessionEnding += new SessionEndingEventHandler(SysetmEvents_SessionEnding);
			// タイマー設定
			SetTimer();
			// アプリケーションメッセージループ実行
			Application.Run();
			// フック解除
			keyboardHook.Dispose();
		}
	}
}
