using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDOtimers
{
    public class ParseReady
    {   public   ParseReady()
        {   reset();
          //test ();
        }

        private DateTime Dalrm = new DateTime();

        public string usertext;
        public int    dreaming;

        public enum eMODE
        {   BACKTIME  ,
            POINTTIME ,
            ALARM     ,
            ERROR     ,
            SECUNDOMER,
            XXX
        }
        public eMODE mode;

        public void reset()
        {   mode     = eMODE.XXX;
            usertext = "";
        }

        //---------------------------|
        // set 1.                    }<---| 1.
        //---------------------------:
        public string set_secundomer()
        {   Dalrm = DateTime.Now;
            mode  = ParseReady.eMODE.SECUNDOMER;
            return "";
        } 

        //---------------------------|
        // set 2.                    |<---| 2.
        //---------------------------:
        public string set_time(int minutes)
        {   
            if(minutes > 1440)
            {   return "ERROR: много минут!";
            }
            
            Dalrm = DateTime.Now.AddMinutes(minutes);

            mode  = ParseReady.eMODE.BACKTIME;
            return "";
        }    

        //---------------------------|
        // set 3.                    |<---| 3.
        //---------------------------:
        public string set_time(int hours, int minutes)
        {   
            if (hours   > 23) return "ERROR: много часов!";
            if (minutes > 59) return "ERROR: много минут!";

            Dalrm = new DateTime  (
                DateTime.Now.Year ,
                DateTime.Now.Month,
                DateTime.Now.Day  ,
                hours             ,
                minutes           ,
                DateTime.Now.Second
            );

            if(Dalrm.Subtract(DateTime.Now).TotalSeconds < 0.0)
            {   Dalrm = Dalrm.AddDays(1.0);
            }

            mode = ParseReady.eMODE.POINTTIME;
            return "";
        }

        public bool is_alarm()
        {   if(mode == ParseReady.eMODE.SECUNDOMER) return false;
            
            bool   b = Dalrm.Subtract(DateTime.Now).TotalSeconds < 0.0;
            if    (b) mode = ParseReady.eMODE.ALARM;
            return b;
        }

        public string getready()
        {   switch(mode)
            {   case ParseReady.eMODE.SECUNDOMER:
                case ParseReady.eMODE.BACKTIME  :
                case ParseReady.eMODE.POINTTIME : return usertext + calcTime() ;
                case ParseReady.eMODE.ALARM     : return usertext + "ALARM"    ;
                case ParseReady.eMODE.XXX       : return "ParseReady.eMODE.XXX";
            }
            return "";
        }

        private string calcTime()
        {   TimeSpan t;

            if(mode == ParseReady.eMODE.SECUNDOMER)
            {      t = DateTime.Now.Subtract(Dalrm);
            }
            else   t = Dalrm.Subtract(DateTime.Now);

          //return t.ToString().Split('.')  [0];
            return t.ToString().Substring(0, 8);

            /*
            return t.Hours   .ToString() + ":" +
                   t.Minutes .ToString() + ":" +
             ((int)t.Seconds).ToString() ;
            */
        }

        public void debug()
        {   Debug.Out.add("gettime() ", calcTime());
        }

        public void test()
        {   reset();
          //D = D.AddSeconds(1.0);
          //TimeSpan d = Dnow.Subtract(D);
          //Debug.Out.add("TIME: ", Dnow.ToLongTimeString());
          //Debug.Out.add("TIME: ", Dnow.Subtract(D).ToString());
            Debug.Out.add("TIME: ", 
                (int)Dalrm.Subtract(DateTime.Now).TotalSeconds);
        }
    }
}
