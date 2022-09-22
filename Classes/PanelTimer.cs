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

            Control[] m = P.Controls.Find("buttonOn", false);

            if (m.Length > 0)
            {   // m[0].BackColor = MyLib.rgb(0, 0, 255);
                B = (Button)m[0];
            }
            else
            {   Debug.Out.add("ERROR: PanelTimer(.)"); return;
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
            {   Debug.Out.add("ERROR: PanelTimer.work(.)"); return;
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
        myTimersForm F;
        Panel        P;
        Button       B;
        RichTextBox  R;

        ParseReady parseReady = new ParseReady();

        //-----------------------------|
        // ФАСАД.                      |<<<------------------------------------:
        //-----------------------------|

        public Panel getPanel(       ){ return P     ; }
        public bool  equals  (Panel p){ return P == p; }

        public void work(ref Timer t)
        {  
            switch(parseReady.mode)
            {   case ParseReady.eMODE. BACKTIME:
                case ParseReady.eMODE.POINTTIME:
                {   if(parseReady.is_alarm())
                    {   ALARM_start();
                        R.Focus    ();
                        F.WindowState = FormWindowState.Normal;
                    }
                    break;
                }
                default: return;
            }
            
            R.Text = parseReady.getready();
        }

        //-----------------------------|
        // ПОДВАЛ.                     |<<<------------------------------------:
        //-----------------------------|

        string mem;
        private void on()
        {   P.BackColor = Color.PaleGreen;
            mem         = R.Text;
            R.Enabled   = false ;
        }

        private void off()
        {   P.BackColor = Color.LemonChiffon;
            R.Text      = mem ;
            R.Enabled   = true;
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress  =  true; // Убрать системный звук.

                if(parseReady.mode == ParseReady.eMODE.ALARM)
                {   off       ();
                    ALARM_stop();
                }
                else if(parseReady.mode == ParseReady.eMODE.ERROR)
                {   off       ();
                    parseReady.mode = ParseReady.eMODE.XXX;
                }
                else timerstart();
            }
            else if(e.KeyCode       == Keys.Escape         &&
                    parseReady.mode == ParseReady.eMODE.ALARM)
            {   
                off       ();
                ALARM_stop();
            }
            else if(e.KeyCode       == Keys.O)
            {   Debug.Out.add("");
            }
        }

        private void initevent()
        {   
            B.Click   += new System.EventHandler          (buttonOn_Click);
            R.KeyDown += new System.Windows.Forms.KeyEventHandler(KeyDown);
        }

        private void timerstart()
        {
            switch(R.Text.ToLower())
            {   case "d" : F.panelsManager_delete(P); return;
                case "ex": F.exit                ( ); return;
            }

            string error = ParseTextInput.done(ref parseReady, R.Text);
            {   if(error.Length == 0)
                {    on();
                     R.Text = parseReady.getready();
                }
                else R.Text = error;
            }
        }

        private void buttonOn_Click(object sender, EventArgs e)
        { 
          //Debug.Out.add("buttonOn_Click");

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
                case ParseReady.eMODE.ERROR:
                {   off       ();
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
