using System.Drawing;
using System.Windows.Forms;

namespace ThemeControl {

    public class MyColors : ProfessionalColorTable {
        private bool DarkMode { get; set; }
        // コンストラクタ
        public MyColors(bool mode) => DarkMode = mode;

        public override Color MenuItemSelected            => ColorTranslator.FromHtml(DarkMode ? "#353535" : "#B5D7F3"); // アイテム選択時の背景の色
        public override Color MenuItemBorder              => ColorTranslator.FromHtml(DarkMode ? "#707070" : "#0078D7"); // アイテム選択時の枠の色
        public override Color ToolStripDropDownBackground => ColorTranslator.FromHtml(DarkMode ? "#2B2B2B" : "#FDFDFD"); // アイテム非選択時の色
    }

    // タスクトレイのメニュー項目ダークモード設定
    public class MyToolStripMenuItem : ToolStripProfessionalRenderer {
        private bool DarkMode { get; set; }

        // コンストラクタ
        public MyToolStripMenuItem(bool mode) : base(new MyColors(mode)) => DarkMode = mode;

        // ▷の色設定
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e) {
            var tsMenuItem = e.Item as ToolStripMenuItem;
            //if (!tsMenuItem.Equals(null)) {
            //    e.ArrowColor = DarkMode ? Color.White : Color.Black;
            //    ((ToolStripDropDownMenu)tsMenuItem.DropDown).ShowCheckMargin = false;
            //    ((ToolStripDropDownMenu)tsMenuItem.DropDown).ShowImageMargin = false;
            //}
            e.ArrowColor = DarkMode ? Color.White : Color.Black;
            ((ToolStripDropDownMenu)tsMenuItem?.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)tsMenuItem?.DropDown).ShowImageMargin = false;
            base.OnRenderArrow(e);
        }

        // テキストの色設定
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
            base.OnRenderItemText(e);
            e.Item.ForeColor = DarkMode ? Color.White : Color.Black;
        }

        // セパレータの色設定
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            if ((e.Item as ToolStripSeparator).Equals(null)) {
                base.OnRenderSeparator(e);
                return;
            }
            int width  = e.Item.Width;
            int height = e.Item.Height;

            Color ForeColor = ColorTranslator.FromHtml(DarkMode ? "#3E3E3E" : "#BDBDBD");
            Color BackColor = ColorTranslator.FromHtml(DarkMode ? "#2B2B2B" : "#FDFDFD");

            e.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, width, height);
            e.Graphics.DrawLine(new Pen(ForeColor), -2, height / 2, width, height / 2);
        }
    }
}
