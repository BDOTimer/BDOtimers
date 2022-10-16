using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace BDOtimers
{
    class CursorPanelTime
    {   public CursorPanelTime(myTimersForm form)
        {   F = form;
        }

        myTimersForm F;
        Panel        P;

        public void setFocusCursor(Panel p)
        {   
          //if(p != null)
            {   P.Visible = false;
                P         = p    ;
                P.Visible = true ;
            }
        }

        public void setPanel(Panel p){ P = p; P.Visible = true ; }
    }
}
