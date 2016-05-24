using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace CompilingPrinciples.Utility
{
    public class WindowThemeListView : ListView
    {
        public WindowThemeListView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        protected override void OnHandleCreated(EventArgs e)
        {
            SetWindowTheme(this.Handle, "Explorer", null);
            base.OnHandleCreated(e);
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);

            foreach (ColumnHeader column in this.Columns)
                column.Width = (int)Math.Round(column.Width * factor.Width);
        }
    }

    public class DoubleBufferedListBox : ListBox
    {
        public DoubleBufferedListBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
    }

    public class DoubleBufferDataGridView : DataGridView
    {
        public DoubleBufferDataGridView()
        {
            DoubleBuffered = true;
        }
    }
}
