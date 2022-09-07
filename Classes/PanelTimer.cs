using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace BDOtimers
{
    class PanelTimer
    {
        public PanelTimer(myTimersForm form)
        {   F = form;
            P = form.PanelClone();

            Control[] 
            m = P.Controls.Find("buttonOn", false);

            if (m.Length > 0)
            {   
              //m[0].BackColor = MyLib.rgb(255, 0, 0);
                B = (Button)m[0];
            }
            else
            {   Debug.Out.add("ERROR: PanelTimer(.)");
            }

            m = P.Controls.Find("richTextBoxInput", false);

            if (m.Length > 0)
            {   R           = (RichTextBox)m[0];
                R.MaxLength = 22;
                R.Font = new System.Drawing.Font("Times", 8.2F, 
                             System.Drawing.FontStyle.Regular, 
                             System.Drawing.GraphicsUnit.Point, ((byte)(2))
                );
                R.Multiline = false;
                
            }
            else
            {   Debug.Out.add("ERROR: PanelTimer.work(.)");
            }

            initevent();
            off      ();
        }

        enum eWork
        {   STOP,
            WORK,
            PAUSE
        }

        //---------------------------|
        // Поля.                     |
        //---------------------------:
        myTimersForm            F;
        Panel                   P;

        Button                  B;
        RichTextBox             R;

        ParseReady parseReady = new ParseReady();

        public Panel getPanel(){ return P; }

        public void work(ref Timer t)
        {
            if( parseReady.mode == ParseReady.eMODE.ALARM    ||
                parseReady.mode == ParseReady.eMODE.XXX) return;

            switch(parseReady.mode)
            {   case ParseReady.eMODE.BACKTIME :
                case ParseReady.eMODE.POINTTIME:
                {   if(parseReady.is_alarm())
                    {   ALARM_start();
                    }
                    break;
                }
            }
            
            R.Text = parseReady.getready();
        }

        string mem;
        void on()
        {   P.BackColor = Color.PaleGreen;
            mem         = R.Text;
            R.Enabled   = false ;
        }

        void off()
        {   P.BackColor = Color.LemonChiffon;
            R.Text      = mem ;
            R.Enabled   = true;
        }

        public bool equals(Panel p) { return p == P; }

        public void onoff ()
        {      R.Enabled = !R.Enabled;
            if(R.Enabled) on ();
            else          off();
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {   
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Убрать системный звук.
                timerstart();
            }
            else if(e.KeyCode       == Keys.Escape && 
                    parseReady.mode == ParseReady.eMODE.ALARM)
            {   
                off       ();
                ALARM_stop();
            }
            else if(e.KeyCode      == Keys.F)
            {   
                Debug.Out.add("F");
            }
        }

        public void KeyPress(object sender, KeyPressEventArgs e)
        {   
            char c = e.KeyChar;
            if (c == 13)
            {   e.Handled = true;
                
            }
            Debug.Out.add("KeyPress");
        }

        private void initevent()
        {   
            B.Click    += new System.EventHandler          (buttonOn_Click);
            R.KeyDown  += new System.Windows.Forms.KeyEventHandler(KeyDown);
          //R.KeyPress += 
              //new System.Windows.Forms.KeyPressEventHandler(KeyPress);
        }

        private void timerstart()
        {
            switch(R.Text)
            {   case "del":
                {   F.panelsManager_delete(P); return;
                }
                case "exit":
                {   F.exit();                  return;
                }
            }

            string error = ParseTextInput.done(ref parseReady, R.Text);
            {   if(error.Length == 0)
                {   on();
                    R.Text  = parseReady.getready();
                }
                else R.Text = error;
            }
        }

        public void buttonOn_Click(object sender, EventArgs e)
        { //Debug.Out.add("buttonOn_Click");

            switch(parseReady.mode)
            {   case ParseReady.eMODE.BACKTIME :
                case ParseReady.eMODE.POINTTIME:
                {   off();
                    parseReady.mode = ParseReady.eMODE.XXX;
                    break;
                }
                case ParseReady.eMODE.ALARM:
                {   ALARM_stop();
                    off       ();
                    break;
                }
                case ParseReady.eMODE.XXX:
                {   timerstart();
                    break;
                }
            }
        }

        private void ALARM_start()
        {   Debug.Out.add("ALARM", parseReady.usertext);
            P.BackColor = Color.Salmon;
            R.Enabled   = true;
            R.Focus();

            myTimersForm.sound.play(MySounds.eSND.ALARM1);
        }

        private void ALARM_stop()
        {   P.BackColor     = Color.LemonChiffon  ;
            parseReady.mode = ParseReady.eMODE.XXX;

            myTimersForm.sound.stop(MySounds.eSND.ALARM1);
        }
    }
}
