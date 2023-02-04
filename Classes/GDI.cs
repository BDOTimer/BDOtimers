using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BDOtimers
{
    public class GDI
    {
        public static void gradient(Control C, PaintEventArgs e,
                                    Color  ca,
                                    Color  cb)
        {
            int H2 = C.Height / 2;

            Rectangle r1 = new Rectangle(0,  0, C.Width, H2);
            Rectangle r2 = new Rectangle(0, H2, C.Width, C.Height);

            LinearGradientBrush brush = new LinearGradientBrush(
                new Point(0,  0),
                new Point(0, H2),
                ca,
                cb);

            e.Graphics.FillRectangle(brush, r1);

            LinearGradientBrush brush02 = new LinearGradientBrush(
                new Point(0, H2 - 1  ),
                new Point(0, C.Height), 
                cb,
                ca);

            e.Graphics.FillRectangle(brush02, r2);

            brush.Dispose();
        }
    }
}
