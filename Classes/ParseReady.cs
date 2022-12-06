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

        private string        Usertext;
        private int           Dreaming;
        private MySounds.eSND  SoundID;
        private string       SoundName;

        ///---------------------------|
        /// Свойства.                 |
        ///---------------------------:
        public int dreaming
        {   get{    return Dreaming ; }
            set{    Dreaming = value; }
        }

        public string usertext
        {   get{    return Usertext ; }
            set{    Usertext = value; }
        }

        public MySounds.eSND  soundID
        {   get{    return SoundID  ; }
            set{    SoundID = value ; }
        }

        public string  soundName
        {   get{    return SoundName  ; }
            set{    SoundName = value ; }
        }

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
            if(minutes > 9999)
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

        public override string ToString()
        {   switch(mode)
            {   case ParseReady.eMODE.BACKTIME  :
                case ParseReady.eMODE.POINTTIME : 
                case ParseReady.eMODE.SECUNDOMER: return usertext + calcTime() ;
                case ParseReady.eMODE.ALARM     : return usertext + "ALARM"    ;
                case ParseReady.eMODE.XXX       : return "ParseReady.eMODE.XXX";
            }
            return "";
        }

        private string calcTime()
        {   
            TimeSpan t;

            switch(mode)
            {   
                case ParseReady.eMODE.SECUNDOMER:
                    t = DateTime.Now.Subtract(Dalrm); break;

                default:
                    t = Dalrm.Subtract(DateTime.Now); break;
            }

            string days = t.Days == 0 ?  "" : t.Days.ToString() + " days ";

            return  String.Format("{0}{1:D2}:{2:D2}:{3:D2}",
                    days      ,
                    t.Hours   ,
                    t.Minutes ,
               (int)t.Seconds);
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
