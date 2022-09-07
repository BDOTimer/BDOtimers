using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BDOtimers
{
    public class Mover
    {   public   Mover(Form f, Control c)
        {   F = f;
            C = c;
            C.MouseDown += new MouseEventHandler(this.MouseDown);
            C.MouseMove += new MouseEventHandler(this.MouseMove);
            C.MouseUp   += new MouseEventHandler(this.MouseUp  );
        }

        void MouseDown(object sender, MouseEventArgs e)
        {   mcur      = true           ;
            Cursorpos = Cursor.Position;
            C.Cursor = System.Windows.Forms.Cursors.SizeAll;
        }

        bool mcurdone = true;
        void MouseMove(object sender, MouseEventArgs e)
        {   if (mcur)
            {
                Point cp    = Cursor.Position;
                      cp.X -= Cursorpos.X;
                      cp.Y -= Cursorpos.Y;

                if (  cp.X == 0   && 
                      cp.Y == 0 ) return;
                else              mcurdone = false;

                Point dl    = F.DesktopLocation;
                      dl.X += cp.X;
                      dl.Y += cp.Y;

                Cursorpos   = Cursor.Position;
                F.SetDesktopLocation(dl.X, dl.Y);
            }
        }

        void MouseUp  (object sender, MouseEventArgs e)
        {   mcur     = false;
            C.Cursor = System.Windows.Forms.Cursors.Default;

            if (mcurdone)
            {
               if (((Control)sender).Name == "buttonShow")
                {   
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                        {
                            //sounds.play(MySounds.eSND.SHOW);
                            break;
                        }
                        case MouseButtons.Right:
                        {
                            //sounds.play(MySounds.eSND.SHOWRIGHT);
                            break;
                        }
                    }
                }
            }
            else mcurdone = true;
        }

        Form    F;
        Control C;

        bool  mcur = false;
        Point Cursorpos   ;
    }
}
