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
                    {   Button     b        = new Button();
                                   b.Cursor = Cursors.Hand;
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
                    case "System.Windows.Forms.Panel":
                    {   Panel       b = new Panel ( );
                                    d.Controls.Add(b);
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

        public static string from_string(string s, char from, char to)
        {   int a  =  s.IndexOf(from, 0);
            if( a == -1) return "";
            int b  =  s.IndexOf(to, a);
            if( b == -1) return "";
            return    s.Substring(a, b - a + 1);
        }

        public static bool isexist(string s, string name)
        {   return -1 != s.IndexOf(name, 0);
        }

        public static int find_index(string[] s, string name)
        {   for(int i  = 0; i < s.Length; ++i)
            {   if(-1 !=  s[i].IndexOf(name, 0)) return i;
            }
            return 0;
        }

        public static int find_index(List<string> s, string name)
        {   for(int i  = 0; i < s.Count; ++i)
            {   if(-1 !=  s[i].IndexOf(name, 0)) return i;
            }
            return 0;
        }

        public static int xxxfind_index(List<string> s, string name)
        {   string res = s.FirstOrDefault(n => n.IndexOf(name, 0)  != -1);
            int     i  = s.IndexOf(res);
            return  i != -1 ? i : 0;
        }
    }
}
