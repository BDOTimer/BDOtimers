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
        public PanelTimer(myTimersForm form, CursorPanelTime cursorPT)
        {   F               = form             ;
            cursorPanelTime = cursorPT         ;
            P               = form.PanelClone();

            B       = (Button     )getControl("buttonOn"        );
            R       = (RichTextBox)getControl("richTextBoxInput");
            panelCT = (Panel      )getControl("panelCT"         );

            if (R != null)
            {   R.MaxLength = 22;
                R.Font = new System.Drawing.Font("Times"      , 8.2F,
                             System.Drawing.FontStyle.Regular , 
                             System.Drawing.GraphicsUnit.Point, ((byte)(2))
                );
                R.Multiline = false;
            }
            else
            {   Debug.Out.add("ERROR: PanelTimer.work(.)"); return;
            }

            initevent();
            off      ();
            
            F.ActiveControl = R;
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
        Panel  panelCT;

        CursorPanelTime cursorPanelTime;
        ParseReady      parseReady = new ParseReady();

        Control getControl(string name)
        {   Control[] m  = P.Controls.Find(name, false);
            if(m.Length != 0)
            {   return m[0];
            }   return null;
        }

        //-----------------------------|
        // ФАСАД.                      |<<<------------------------------------:
        //-----------------------------|

        public Panel       getPanel     (){ return P      ; }
        public Panel       getPanelCT   (){ return panelCT; }
        public RichTextBox getR         (){ return R      ; }
        public bool        equals(Panel p){ return P == p ; }

        public void work(ref Timer t)
        {  
            switch(parseReady.mode)
            {   case ParseReady.eMODE.SECUNDOMER: break;
                case ParseReady.eMODE.BACKTIME  :
                case ParseReady.eMODE.POINTTIME :
                {   if(parseReady.is_alarm())
                    {   F.WindowState = FormWindowState.Normal;
                        ALARM_start();
                        R.Focus    ();
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
        {   
            switch(parseReady.mode)
            {   case ParseReady.eMODE.SECUNDOMER : 
                    P.BackColor = Color.LightBlue;
                    break;
                default:
                    P.BackColor = Color.PaleGreen;
                    break;
            }

            mem       = R.Text;
            R.Enabled = false ;
        }

        private void off()
        {   P.BackColor = Color.LemonChiffon;
            R.Text      = mem ;
            R.Enabled   = true;

            if(parseReady.mode != ParseReady.eMODE.ERROR)
            {   myTimersForm.sound.play(MySounds.eSND.z2_CLICK_OFF);
            }
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress  =  true; // Убрать системный звук.

                if(parseReady.mode == ParseReady.eMODE.ALARM)
                {
                    ALARM_stop();

                    if(parseReady.dreaming != 0)
                    {
                        timerstart_dream();
                        R.Enabled = false ;
                    }
                    else off();
                }
                else if(parseReady.mode == ParseReady.eMODE.ERROR)
                {   off       ();
                    parseReady.mode = ParseReady.eMODE.XXX;
                    R.ForeColor     = Color.Black;
                }
                else 
                {   timerstart();
                }
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
        {   B.Click    += new System.EventHandler          (buttonOn_Click);
            R.KeyDown  += new System.Windows.Forms.KeyEventHandler(KeyDown);
            R.GotFocus += new System.EventHandler   (this.richTextBoxFocus);
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
                     myTimersForm.sound.play(MySounds.eSND.z1_CLICK_ON);
                }
                else
                {   R.ForeColor = Color.Red;
                    R.Text      = error;
                }
            }
        }

        private void timerstart_dream()
        {
            string dreaming = Convert.ToString(parseReady.dreaming);

            R.Text = "Dream: " + dreaming
                   + " +"      + dreaming;

            string error = ParseTextInput.done(ref parseReady, R.Text);
            {   if(error.Length == 0)
                {   R.Text = parseReady.getready();
                }
                else
                {   R.ForeColor = Color.Red;
                    R.Text      = error;
                }
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
                {   
                    timerstart();
                  //Debug.Out.add("dreaming: ", parseReady.dreaming);
                    break;
                }
                case ParseReady.eMODE.ERROR:
                {   off       ();
                    parseReady.mode = ParseReady.eMODE.XXX;
                    R.ForeColor     = Color.Black;
                    break;
                }
                case ParseReady.eMODE.SECUNDOMER:
                {   string s = R.Text;
                    off();     R.Text = s;

                    parseReady.mode = ParseReady.eMODE.XXX;
                    R.ForeColor     = Color.Black;
                    break;
                }
            }
            F.ActiveControl = R;

            cursorPanelTime.setFocusCursor(panelCT);
        }

        private void richTextBoxFocus(object sender, EventArgs e)
        {   Panel  p = (Panel)(((RichTextBox)sender).Parent);
            cursorPanelTime.setFocusCursor(panelCT);
        }

        private void ALARM_start()
        {   
            if( parseReady.usertext == "")
            {   parseReady.usertext =  "Просыпайся!";
            }
            
            Debug.Out.clear();
            Debug.Out.add("ALARM", parseReady.usertext);
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
