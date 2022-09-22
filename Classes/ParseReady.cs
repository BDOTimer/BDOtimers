using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDOtimers
{
    public class ParseReady
    {   public   ParseReady()
        {   reset();
            Dalrm = DateTime.Now;
          //Debug.Out.add("TIME: ", DateTime.Now.ToString());
        }

        private DateTime Dalrm = new DateTime();

        public string usertext;
        public int    dreaming;

        public enum eMODE
        {   BACKTIME ,
            POINTTIME,
            ALARM    ,
            ERROR    ,
            XXX
        }
        public eMODE mode;

        public void reset()
        {   mode     = eMODE.XXX;
            usertext = "";
        }

        public string check_time(int minutes)
        {   
            if(minutes > 1440)
            {   return "ERROR: много минут!";
            }
            
            Dalrm = DateTime.Now.AddMinutes(minutes);

            mode = ParseReady.eMODE.BACKTIME;
            return "";
        }    

        public string check_time(int hours, int minutes)
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
        {   bool   b = Dalrm.Subtract(DateTime.Now).TotalSeconds < 0.0;
            if    (b) mode = ParseReady.eMODE.ALARM;
            return b;
        }

        public string getready()
        {   switch(mode)
            {   case ParseReady.eMODE.BACKTIME : return usertext + gettime();
                case ParseReady.eMODE.POINTTIME: return usertext + gettime();
                case ParseReady.eMODE.ALARM    : return usertext + "ALARM";
                case ParseReady.eMODE.XXX      : return "ParseReady.eMODE.XXX";
            }
            return "";
        }

        private string gettime()
        {   TimeSpan t = Dalrm.Subtract(DateTime.Now);

            return   t.Hours   .ToString() + ":" +
                     t.Minutes .ToString() + ":" +
               ((int)t.Seconds).ToString();
        }

        public void debug()
        {   Debug.Out.add("gettime() ", gettime());
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
