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

        static char[] separ = new char[2]{' ', ','};

        public static string done(ref ParseReady parseReady, string s)
        {   
            string[] mm = s.Split(separ, StringSplitOptions.RemoveEmptyEntries);

            parseReady.usertext = "";
            parseReady.dreaming =  0;
            parseReady.mode     = ParseReady.eMODE.ERROR;

            List<int> m = new List<int>();

            for(int i = 0; i < mm.Length; ++i)
            {   
                int n = isdigital(mm[i]);
                if( n == -1)
                {   parseReady.usertext += mm[i] + " ";
                }
                else
                {   if(mm[i][0] == '+') parseReady.dreaming = n;
                    else                m.Add(n);
                }
            }

            //if(parseReady.dreaming != 0)
            //    Debug.Out.add("dreaming", parseReady.dreaming);

            switch(m.Count)
            {   case  0: return "Мало данных.";
                case  1: return parseReady.check_time(m[0]      ); 
                case  2: return parseReady.check_time(m[0], m[1]);
                default: break;
            }

            return "ERROR: много чисел.";
        }

        private static int isdigital(string s)
        {
            try
            {   int    n = Convert.ToInt32(s);
                return n;
            }
            catch  {}
            return -1;
        }
    }
}
