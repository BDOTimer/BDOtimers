using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace BDOtimers
{
    class MyLib
    {
        //--------------------------------------------------------|
        // Debug.Out.add("s.Controls.Count", s.Controls.Count);   |
        //--------------------------------------------------------:
        public static void copy(Control d, Control s)
        {
            d.Width     = s.Width    ;
            d.Height    = s.Height   ;
            d.Left      = s.Left     ;
            d.Top       = s.Top      ;
            d.Name      = s.Name     ;
            d.BackColor = s.BackColor;
            d.Text      = s.Text     ;
            d.TabIndex  = s.TabIndex ;

            foreach (Control O in s.Controls)
            {   Type   tp =  O.GetType();

                switch(tp.ToString())
                {
                    case "System.Windows.Forms.Button":
                    {   Button     b = new Button( );
                                   d.Controls.Add(b);
                                   b.UseVisualStyleBackColor = true;
                        copy      (b, O);
                        break;
                    }
                    case "System.Windows.Forms.RichTextBox":
                    {   RichTextBox b = new RichTextBox( );
                                    d.Controls.Add     (b);
                        copy       (b, O);
                        break;
                    }
                }
            }
        }

        public static Color rgb(int r, int g, int b)
        {   return Color.FromArgb(
                ((int)(((byte)(r)))),
                ((int)(((byte)(g)))),
                ((int)(((byte)(b))))
            );
        }
    }
}
