using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace BDOtimers
{
    class PanelsManager
    {   public PanelsManager(myTimersForm form)
        {   F = form;
          //Debug.Out.T.Visible = true;
          //create();
        }

        //---------------------------|
        // Поля.                     |
        //---------------------------:
        public
        List<PanelTimer> cargo = new List<PanelTimer>();
        myTimersForm                                  F;
        const int STEPV =                             2;

        public void work(ref Timer t)
        {   foreach (PanelTimer p in cargo)
            {   p.work(ref t);
            }
        }

        public void create()
        {   if(cargo.Count  == 12)
            {   Debug.Out.add("ЛИМИТ: 12 таймеров!");
                return;
            }
            cargo.Add(new PanelTimer(F));
            order();
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
