using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

using System.Runtime.InteropServices;

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

        ///-----------------------------|
        /// Переключатель раскладки.    |
        ///-----------------------------:
        ///InputLanguage defLang;
        public static void setLangKeyboard(string Lang = "Eng")
        {
            if(InputLanguage.CurrentInputLanguage
                            .Culture.EnglishName 
                            .Contains(Lang) ) return;

            foreach (InputLanguage il in InputLanguage.InstalledInputLanguages)
            {
                if (il.Culture.EnglishName.Contains(Lang))
                {   InputLanguage.CurrentInputLanguage = il;
                }

                // Debug.Out.add(il.Culture.EnglishName, "");
            }
        }

        ///-----------------------------|
        /// Второй способ(тормознутый). |
        ///-----------------------------:
        static Dictionary<string, string> dic = new Dictionary<string, string>()
        {   {"Eng", "00000409"},
            {"Rus", "00000419"}
        };
        public static void setLangKeyboard_xxx(string Lang = "Eng")
        {   
            /*
            "00000407" Немецкий (стандартный)
            "00000409" Английский (США)
            "0000040C" Французский (стандартный)
            "0000040D" Финский
            "00000410" Итальянский
            "00000415" Польский
            "00000419" Русский
            "00000422" Украинский
            "00000423" Белорусский
            "00000425" Эстонский
            "00000426" Латвийский
            "00000427" Литовский
             */

            var    lang = dic[Lang];
            int    ret  = LoadKeyboardLayout (lang,    1);
            PostMessage(  GetForegroundWindow(), 0x50, 1, ret);
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, int Msg, 
                                       int  wParam, int lParam);

        [DllImport("user32.dll")]
        static extern int LoadKeyboardLayout(string pwszKLID, uint Flags);
    }
}
