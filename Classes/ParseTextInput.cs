using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDOtimers
{
    class       ParseTextInput
    {   public  ParseTextInput()
        {   
        }

        static char[] separ = new char[2]{' ', ':'};

        public static string done(ref ParseReady parseReady, string S)
        {   
            parseReady.usertext = "";
            parseReady.dreaming =  0;
            parseReady.mode     = ParseReady.eMODE.ERROR;

            string [] s = S.Split(separ, StringSplitOptions.RemoveEmptyEntries);
            List<int> N = new List<int>();

            parseReady.soundID   = getSpecLoudlySound(ref s);
            parseReady.soundName = MyLib.from_string(S, '[', ']');

            //---------------------------|
            // SECUNDOMER                |
            //---------------------------:
            if( (s.Length == 1 && s[0] == "s")         ||
                (s.Length >  0 && s[0] == "Секундомер" ))
            {   parseReady.usertext     = "Секундомер: ";
                return parseReady.set_secundomer      ();
            }

            for(int i = 0; i < s.Length; ++i)
            {
                int n = isdigital(s[i]);

                     if( n       <   0 ) parseReady.usertext += s[i] + " ";
                else if( s[i][0] == '+') parseReady.dreaming = n;
                else                     N.Add(n);
            }

            //if(parseReady.dreaming != 0)
            //    Debug.Out.add("dreaming", parseReady.dreaming);

            switch(N.Count)
            {   case  0: return "ERROR: мало данных.";
                case  1: return parseReady.set_time(N[0]      );
                case  2: return parseReady.set_time(N[0], N[1]);
                default: break;
            }

            return "ERROR: много чисел.";
        }

        private static int isdigital(string s)
        {   try  { return Convert.ToInt32  (s); }
            catch{                              }
            return -888;
        }
        
        private static MySounds.eSND getSpecLoudlySound(ref string[] s)
        {   foreach(var tokken in  s  )
            {   if     (tokken == "!" ) return MySounds.eSND.z4_DjTiesto;
                if     (tokken == "!!") return MySounds.eSND.z5_Zvonok  ;
            }
            return MySounds.eSND.ALARM1;
        }
    }
}
