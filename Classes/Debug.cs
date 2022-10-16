// Debug.Out.add();
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace BDOtimers
{
    public class Debug
    {
        public Debug(TextBox t)
        {   T = t;
        }

        //---------------------|
        // Debug.              |<----------------------------------------------|
        //---------------------:
        public static Debug Out;

        public void add(string s)
        {   push(s);
            on  ( );
        }

        public void add<T_>(string s, T_ n)
        {   push(s + ": " + Convert.ToString(n));
            on();
        }

        private void on( )
        {   if(!T.Visible)
            {   T.Visible = true;
                F.show_debug();
            }
        }

        public TextBox      T;
        public myTimersForm F;

        public void clear()
        {   m.Clear();
            T.Text = "";
        }

        List<string>        m = new List<string>();

        void push(string   s)
        {   if(m.Count ==  7) m.RemoveAt(0);
            m.Add(s + "\r\n");
            load (          );
        }

        void load()
        {   T.Text = "";
            foreach(var s in m) T.Text += s;
        }
    }
}
