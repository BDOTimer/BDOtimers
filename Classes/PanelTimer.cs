using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

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

            init_event();
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

            if(F.WindowState == FormWindowState.Normal)
            {   R.Text = parseReady.ToString();
            }
            else R.Text = "...";
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

        private ParseReady.eMODE timerstart()
        {
            switch(R.Text.ToLower())
            {   case "d" : F.panelsManager_delete(P); return parseReady.mode;
                case "ex": F.exit                ( ); return parseReady.mode;
            }

            string error = ParseTextInput.done(ref parseReady, R.Text);
            {   if(error.Length == 0)
                {    on();
                     R.Text = parseReady.ToString();
                     myTimersForm.sound.play(MySounds.eSND.z1_CLICK_ON);
                }
                else
                {   R.ForeColor = Color.Red;
                    R.Text      = error;
                }
            }

            return parseReady.mode;
        }

        private void timerstart_dream()
        {
            string dreaming = Convert.ToString(parseReady.dreaming);

            R.Text = "Dream: " + dreaming
                   + " +"      + dreaming;

            string error = ParseTextInput.done(ref parseReady, R.Text + " " 
                                                 + parseReady.soundName);
            {   if(error.Length == 0)
                {   R.Text = parseReady.ToString();
                }
                else
                {   R.ForeColor = Color.Red;
                    R.Text      = error;
                }
            }
        }

        ///---------------------|
        /// События.            |---------------------------------------------->
        ///---------------------:
        private void init_event()
        {   R.KeyDown   += new System.Windows.Forms.KeyEventHandler(KeyDown);
          //B.Click     += new System.EventHandler          (buttonOn_Click);
            R.GotFocus  += new System.EventHandler   (this.richTextBoxFocus);
            R.MouseDown += new MouseEventHandler(richTextBoxInput_MouseDown);
            B.MouseDown += new MouseEventHandler(        buttonOn_MouseDown);
            B.KeyDown   += new KeyEventHandler  (        buttonOn_KeyDown  );
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            ///-------------|
            /// e.KeyCode   |
            ///-------------:
            switch(e.KeyCode)
            {   
                case Keys.Enter: ///-------------------------------------------:
                {   e.SuppressKeyPress  = true; // Убрать системный звук.

                    ///-------------------|
                    /// parseReady.mode   |
                    ///-------------------:
                    switch(parseReady.mode)
                    {   
                        case ParseReady.eMODE.ALARM:
                        {   ALARM_stop();

                            if(parseReady.dreaming != 0)
                            {
                                timerstart_dream();
                                R.Enabled = false ;

                                ///----------|
                                /// Свернуть.|
                                ///----------:
                                F.myClose  ();
                            }
                            else off();
                            break;
                        }

                        case ParseReady.eMODE.ERROR:
                        {   off();
                            parseReady.mode = ParseReady.eMODE.XXX;
                            R.ForeColor     = Color.Black;
                            break;
                        }

                        default:
                        {   if(timerstart() != ParseReady.eMODE.ERROR)
                            {   B.Focus();
                            }
                            break;
                        }
                    }
                    break;
                }

                case Keys.Escape: //-------------------------------------------:
                {   
                    ///-------------------|
                    /// parseReady.mode   |
                    ///-------------------:
                    switch(parseReady.mode)
                    {   
                        case ParseReady.eMODE.ALARM:
                            off       ();
                            ALARM_stop();
                            break;

                        default:
                            F.myClose();
                            break;
                    }
                    break;
                }

                case Keys.O: //------------------------------------------------:
                {   Debug.Out.add ("");
                    MySounds.xxxtest();
                    break;
                }
            }
        }

        private void richTextBoxInput_MouseDown(object sender, MouseEventArgs e)
        {   if(parseReady.mode == ParseReady.eMODE.ERROR)
            {   R.Text          = "";
                R.ForeColor     = Color.Black;
                parseReady.mode = ParseReady.eMODE.XXX;
            }
        }

        private void buttonOn_KeyDown(object sender, KeyEventArgs e)
        {   
            switch(e.KeyCode)
            {   case Keys.Space : buttonOn_MouseDown(sender, null); break;
                case Keys.Escape: F.myClose         (            ); break;
            }
        }

        private void buttonOn_MouseDown(object sender, MouseEventArgs e)
        { 
            //----------------------------------|
            // Debug.Out.add("buttonOn_Click"); |
            //----------------------------------:

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
        ///--------------------------------------------------------------------.

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

            if(parseReady.soundName.Length == 0)
            {   myTimersForm.sound.play(parseReady.soundID);
            }
            else
            {   myTimersForm.sound.play(parseReady.soundName);
            }

            F.gif.on();
        }

        private void ALARM_stop()
        {   P.BackColor     = Color.LemonChiffon  ;
            parseReady.mode = ParseReady.eMODE.XXX;

            myTimersForm.sound.stop(MySounds.eSND.ALARM1);

            F.gif.off();
        }
    }
}
