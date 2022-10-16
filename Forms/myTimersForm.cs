using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BDOtimers
{
    public partial class myTimersForm : Form
    {
        public myTimersForm()
        {
            InitializeComponent();

            Debug.Out     = new Debug(textBoxDebug);
            Debug.Out.F   = this;
            panelsManager = new PanelsManager(this);
            mover         = new Mover(this, labelNameProgram);

            timerMain.Start();

            //-------|
            // Test. |
            //-------:
            //Panel p = PanelClone();
            // 

            panelModel.Visible = false;

            //ParseTextInput.test();
        }

        //---------------------------|
        // Поля.                     |
        //---------------------------:
        PanelsManager   panelsManager;
        Mover                   mover;
        public static MySounds  sound = new MySounds();

        public void panelsManager_delete(Control C)
        {   if(panelsManager.cargo.Count == 1)
            {   Debug.Out.add("Этот таймер нужен", "...");
                return;
            }

            if (C.Name != "panelModel")
            {   Debug.Out.add("ERROR: PanelsManager.delete(.)");
                return;
            }
            C.Visible = false;
            this.Controls.Remove(C);

            int i = 0;
            {   foreach(var pt in panelsManager.cargo)
                {   if(pt.getPanel() == (Panel)C) break;
                    i++;
                }
            }

            panelsManager.cargo.RemoveAt(i);
            panelsManager.order         ( );

            panelsManager.setFocus_for_enable_panel();

            Debug.Out.add("Таймер удалён", "...");
        }

        public Panel PanelClone()
        {   Panel             p = new Panel();
            MyLib.copy       (p,  panelModel);
            this.Controls.Add(p);
            return            p;
        }

        public Panel get_panelModel() { return panelModel; }

        private void Form1_Load(object sender, EventArgs e)
        {   Debug.Out.T.Visible = false;

            this        .Controls.Remove(buttonDebugClose);
            textBoxDebug.Controls.Add   (buttonDebugClose);

            int w = Debug.Out.T.Width  - buttonDebugClose.Width ;
            int h = Debug.Out.T.Height - buttonDebugClose.Height;
            buttonDebugClose.Top  = h - 3;
            buttonDebugClose.Left = w - 3;
        }

        public void show_debug ()
        {   panelsManager.order();
        }

        private void buttonMin_Click(object sender, EventArgs e)
        {   this.Close();
        }

        FormHelp formhelp;
        private void buttonHelp_Click(object sender, EventArgs e)
        {   formhelp       = new FormHelp();
            formhelp.Owner = this;
            formhelp.Show();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {   panelsManager.work(ref timerMain);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {   panelsManager.create();
        }

        private void xxxrichTextBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {   richTextBoxInput.Text = "Enter";
                e.SuppressKeyPress    = true   ; // Убрать системный звук.
            }
        }

        private void buttonDebugClose_MouseUp(object sender, MouseEventArgs e)
        {   Debug.Out.T.Visible = false;
            Debug.Out.T.Text    =    "";
            panelsManager.order      ();
        }

        public static bool is_close = false;
        private void myTimersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (is_close)
            {   // EXIT!
                this.Visible = false;
                //sounds.play_sync(MySounds.eSND.EXIT);
            }
            else
            {   e   .Cancel      = true; //<<<---...
                this.WindowState = FormWindowState.Minimized;
            }
        }

        public void exit ()
        {   is_close = true;
            this.Close   ();
        }
    }
}
