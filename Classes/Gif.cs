/// TODO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BDOtimers
{
    public class Gif
    {
        public Gif(myTimersForm form)
        {   F = form;
            P = create();
        }

        myTimersForm F;
        PictureBox   P;
        Image        I;

        private PictureBox create()
        {   Size SIZE   = new Size(80, 80);
            
            PictureBox
            p           = new PictureBox();
            p.Size      = SIZE;
            p.Left      = F.textBoxDebug.Size.Width - SIZE.Width -1;
            p.Top       = 2;
            p.BackColor = Color.Coral;

            F.textBoxDebug.Controls.Add (p);

            p.Image = I = Image.FromFile("img/1.gif");
            p.BackgroundImageLayout = ImageLayout.Stretch;

            p.Visible = false;

          //Debug.Out.add (">", f.Length);

            return p;
        }

        public void on (){   P.Visible = true ; }
        public void off(){   P.Visible = false; }
    }
}