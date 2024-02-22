using System;
using System.Diagnostics;

namespace JapaneseInputHelper {

	public static class MyDebug {

		/// <summary>
		/// デバッグメッセージ
		/// </summary>
		/// <param name="msg"></param>
		public static void Message(string msg) {
			var date = DateTime.Now;
			var debug_msg = $"[{date:HH:mm:ss.fff}] {msg}";
			Debug.WriteLine(debug_msg);
		}

		/// <summary>
		/// 実行速度を測定
		/// </summary>
		/// <param name="msg"></param>
		public delegate void Action();
		public static void StopWatch(string msg, Action method) {
			Stopwatch Stopwatch = new Stopwatch();
			Stopwatch.Reset();
			Stopwatch.Start();

			method();

			Stopwatch.Stop();
			Message($"{msg} {Stopwatch.ElapsedMilliseconds}ms");
		}
	}
}