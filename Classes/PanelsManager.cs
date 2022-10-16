﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace BDOtimers
{
    class PanelsManager
    {   public PanelsManager(myTimersForm form)
        {   F                   = form;
          //Debug.Out.T.Visible = true;
            
            cursorPanelTime = new CursorPanelTime(form);
            create();
            cursorPanelTime.setPanel(cargo[0].getPanelCT());
        }

        //---------------------------|
        // Поля.                     |
        //---------------------------:
        public
        List<PanelTimer> cargo = new List<PanelTimer>();
        myTimersForm                                  F;
        const int STEPV        =                      2;
        string name            = "BDOtimers  "         ;

        CursorPanelTime cursorPanelTime;

        public void work(ref Timer t)
        {   foreach (PanelTimer p in cargo)
            {   p.work(ref t);
            }
            F.labelNameProgram.Text = name + DateTime.Now.ToLongTimeString();
        }

        public void create()
        {   if(cargo.Count == 12)
            {   Debug.Out.add("ЛИМИТ: 12 таймеров!");
                return;
            }
            cargo.Add(new PanelTimer(F, cursorPanelTime));
            order();
        }

        public void setFocus_for_enable_panel()
        {   foreach (PanelTimer p in cargo)
            {   var R = p.getR();
                if( R.Enabled)
                {   F.ActiveControl = R;
                    break;
                }
            }
        }

        private PanelTimer find(Panel P)
        {   foreach(var O in cargo)
            {   if(O.equals(P))
                {   return O;
                }
            }
            return null;
        }

        public void order()
        {   int H = F.panelModel.Height + STEPV;
            int h = F.panelModel.Top;

            foreach(var pt in  cargo)
            {   var p = pt.getPanel();
                p.Top = h;
                h    += H;
            }

            calcHForm(h);
        }

        void calcHForm(int top)
        {   
            if(            Debug.Out.T.Visible)
            {              Debug.Out.T.Top = top + 5;
                F.Height = Debug.Out.T.Top + Debug.Out.T.Height + STEPV + STEPV;
            }
            else
            {   F.Height = top + 2;
            }
        }
    }
}
