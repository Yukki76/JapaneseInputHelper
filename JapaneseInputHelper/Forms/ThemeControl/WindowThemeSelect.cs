using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Forms;
using Microsoft.Win32;

namespace ThemeControl {
    public class WindowThemeSelect {
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, uint attribute, ref int pvAttribute, uint cbAttribute);

        private static bool CheckDarkMode() {
            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")) {
                var value = regKey.GetValue("AppsUseLightTheme");
                return (Convert.ToUInt32(value) == 0U);
            }
        }

        public static void RefreshTitleBarThemeColor(IntPtr hWnd, bool mode) {
            const uint DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
            int value = mode ? 1 : 0;
            DwmSetWindowAttribute(hWnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref value, (uint)Marshal.SizeOf(typeof(int)));
        }

        public static void ChangeTheme(ContextMenuStrip contextMenuStrip) {
            bool mode = CheckDarkMode();
            contextMenuStrip.Renderer = new MyToolStripMenuItem(mode);
        }

        public static void ChangeTheme(AboutDialog.MyControl Control) {
            bool mode = CheckDarkMode();
            RefreshTitleBarThemeColor(Control.Form.Handle, mode);
            Control.BtnOk.FlatStyle          = mode ? FlatStyle.Flat : FlatStyle.Standard;
            Control.BtnStartup.FlatStyle     = mode ? FlatStyle.Flat : FlatStyle.Standard;
            Control.Form.BackColor           = ColorTranslator.FromHtml(mode ? "#383838" : "#F0F0F0");
            Control.Form.ForeColor           = ColorTranslator.FromHtml(mode ? "#FFFFFF" : "#000000");
            Control.BtnOk.BackColor          = ColorTranslator.FromHtml(mode ? "#333333" : "#FDFDFD");
            Control.BtnOk.ForeColor          = ColorTranslator.FromHtml(mode ? "#FFFFFF" : "#000000");
            Control.BtnStartup.BackColor     = ColorTranslator.FromHtml(mode ? "#333333" : "#FDFDFD");
            Control.BtnStartup.ForeColor     = ColorTranslator.FromHtml(mode ? "#FFFFFF" : "#000000");
            Control.TbDescription.BackColor  = ColorTranslator.FromHtml(mode ? "#383838" : "#F0F0F0");
            Control.TbDescription.ForeColor  = ColorTranslator.FromHtml(mode ? "#FFFFFF" : "#000000");
        }
    }
}
