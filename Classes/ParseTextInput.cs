using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDOtimers
{
    public class ParseTextInput
    {   public   ParseTextInput()
        {   
        }

        static char[] separ = new char[2]{' ', ','};

        public static string done(ref ParseReady parseReady, string s)
        {
            if(s.Length == 0 || s == "empty") return "empty";

            parseReady.reset();
            
            string[] m = s.Split(separ, 4, 
                StringSplitOptions.RemoveEmptyEntries
            );

            if (m.Length > 3) return "ERROR: много значений!";

            int minutes = -1;
            int hours   = -1;

            switch(m.Length)
            {   case 1:
                {   minutes = isdigital(m[0]);
                    if(minutes < 0) return "ERROR: нужно число!";
                    parseReady.start_to_period(minutes);
                    break;
                }
                case 2:
                {   hours   = isdigital(m[0]);
                    if (hours < 0)
                    {   parseReady.usertext = m[0] + "   ";
                        parseReady.mode = ParseReady.eMODE.BACKTIME;
                    }

                    minutes = isdigital(m[1]);
                    if (minutes < 0) return "ERROR2: нужно число!";
                    if(parseReady.mode == ParseReady.eMODE.BACKTIME)
                    {   parseReady.start_to_period(minutes);
                    }
                    else
                    {   parseReady.start_to_period(hours, minutes);
                    }
                    break;
                }
                case 3:
                {   parseReady.usertext = m[0] + "   ";
                    hours    = isdigital(m[1]);
                    if (hours < 0) return "ERROR2: нужно число!";
                    minutes  = isdigital(m[2]);
                    if (minutes < 0) return "ERROR3: нужно число!";

                    parseReady.start_to_period(hours, minutes);
                    break;
                }
            }

            switch(parseReady.mode)
            {   case ParseReady.eMODE.BACKTIME:
                {   
                    if(minutes > 99999) return "ERROR: много минут!";
                    break;
                }
                case ParseReady.eMODE.POINTTIME:
                {   
                    if (hours   > 23) return "ERROR: много часов!";
                    if (minutes > 59) return "ERROR: много минут!";
                    break;
                }
                case ParseReady.eMODE.XXX:
                {   
                    break;
                }
            }
            return "";
        }

        static int isdigital(string s)
        {
            try
            {   int    n = Convert.ToInt32(s);
                return n;
            }
            catch  {}
            return -1;
        }

        public static void test()
        {
            string TEST = "247,,,  ";
            Debug.Out.add("TEST: ", TEST);

            ParseReady parseReady = new ParseReady ();

            string ERROR = done(ref parseReady, TEST);

            if(ERROR.Length == 0)
            {   Debug.Out.add("parseReady: ", parseReady.getready());
            }
            else
            {   Debug.Out.add(ERROR);
            }
        }
    }

    public class ParseReady
    {   public   ParseReady()
        {   reset();
            Dalrm = DateTime.Now;
          //Debug.Out.add("TIME: ", DateTime.Now.ToString());
        }

        private DateTime Dalrm = new DateTime();

        public string usertext;

        public enum eMODE
        {   BACKTIME ,
            POINTTIME,
            ALARM    ,
            XXX
        }
        public eMODE mode;

        public void reset()
        {   mode     = eMODE.XXX;
            usertext = "";
        }

        public bool start_to_period(int minutes)
        {   Dalrm = DateTime.Now.AddMinutes(minutes);
            if(is_alarm()) return true;

            mode = ParseReady.eMODE.BACKTIME;
            return false;
        }    

        public bool start_to_period(int hours, int minutes)
        {   
            Dalrm = new DateTime  (
                DateTime.Now.Year ,
                DateTime.Now.Month,
                DateTime.Now.Day  ,
                hours             ,
                minutes           ,
                DateTime.Now.Second
            );
            
            if    (DateTime.Now.Hour   > hours  )
            {   if(DateTime.Now.Minute > minutes)
                {   Dalrm = Dalrm.AddDays(1.0);
                }
            }

            if(is_alarm()) return true;

            mode = ParseReady.eMODE.POINTTIME;
            return false;
        }

        public bool is_alarm()
        {   bool   b = Dalrm.Subtract(DateTime.Now).TotalSeconds < 0.0;
            if    (b) mode = ParseReady.eMODE.ALARM;
            return b;
        }

        public void xxxtick(){ Dalrm.AddSeconds(1.0); }

        public string getready()
        {   switch(mode)
            {   case ParseReady.eMODE.BACKTIME : return usertext + gettime();
                case ParseReady.eMODE.POINTTIME: return usertext + gettime();
                case ParseReady.eMODE.ALARM    : return usertext + "ALARM"    ;
                case ParseReady.eMODE.XXX      : return "ParseReady.eMODE.XXX";
            }
            return "";
        }

        string gettime()
        {   TimeSpan t = Dalrm.Subtract(DateTime.Now);

            return   t.Hours   .ToString() + ":" +
                     t.Minutes .ToString() + ":" +
               ((int)t.Seconds).ToString();
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
