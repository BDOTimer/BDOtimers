using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Media;

using System.Windows.Media; // WindowsBase.dll, PresentationCore.dll
using System.Threading;
using System.Threading.Tasks;


namespace BDOtimers
{
    //-------------------------------------------------------------------------|
    //  MySounds
    //-------------------------------------------------------------------------:
    public class MySounds
    {
        public MySounds()
        {   load_sounds();
            //player .Open(new Uri("snd\\001_yes.mp3", UriKind.Relative));
            xxxtest    ();
        }

        public enum eSND //  sounds.play(MySounds.eSND.z1_CLICK_ON);
        {   ALARM0,
            ALARM1,
            // ...
            z1_CLICK_ON ,
            z2_CLICK_OFF,
            z3_CLOSE    ,
            z4_DjTiesto ,
            z5_Zvonok   ,
            end
        }

        private SoundPlayer[]   sp;
        private List<string> files;
        private void load_sounds()
        {
            files =
                (   from a in Directory.GetFiles(
                    "./snd",
                    "*.wav",
                    SearchOption.TopDirectoryOnly)
                    select "snd/" + Path.GetFileName(a)
                ).ToList();
            sp = new SoundPlayer[files.Count];

            for(int i  = 0; i < sp.Length; ++i)
            {   sp [i] = new SoundPlayer     ();
                sp [i].SoundLocation = files[i];
              //sp [i].Load     ();
                sp [i].LoadAsync();
              //sp [i].Stream = new FileStream( files[i],
              //                                FileMode.Open,
              //                                FileAccess.Read );
            }
        }

        private List<SoundPlayer> my;
        private void xxxload_sounds()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("./snd");
            my = new List<SoundPlayer>();
            foreach (var file in directoryInfo.GetFiles())
            {
                if (Path.GetExtension(file.FullName) == ".wav")
                {   my.Add(new SoundPlayer(file.FullName));
                    my[my.Count - 1].Load();
                }
            }
        }

        private void play_t (MySounds.eSND I)
        {   
            if(I == MySounds.eSND.z5_Zvonok)
            {   playloop(I);
                return;
            }
            
            int i = (int)I;
          //sp [i] = new System.Media.SoundPlayer("snd/" + files[(int)i]);
            sp [i].Play();
        }
        public void playloop  (MySounds.eSND I)
        {   sp[(int)I].PlayLooping();
        }
        public void stop(MySounds.eSND I)
        {   sp[(int)I].Stop();
        }

        /// test
        ///-------------------------|
        /// По имени фрагмента.     |
        ///-------------------------:
        private void play_t(string name)
        {   
            int a = name.IndexOf("!");

            if(a != -1)
            {   name = name.Remove(a, 1);

                int i = MyLib.find_index(files, name);
                sp [i].PlayLooping();
            }
            else 
            {   int i = MyLib.find_index(files, name);
                sp [i].Play();
            }
        }

        public void play_sync (MySounds.eSND I)
        {   int i = (int)I;
          //sp [i] = new System.Media.SoundPlayer("snd/" + files[i]);
            sp [i].PlaySync();
        }

        public void xplay     (MySounds.eSND i)
        {   my[(int)i].Play();
        }
        public void xplay_sync(MySounds.eSND i)
        {   my[(int)i].PlaySync();
        }

        public static void xxxtest()
        {   xxxtest01();
          //xxxtest02();
        }
        
        static MediaPlayer player = new MediaPlayer();
        private static void xxxtest01()
        { //player.Open (new Uri("snd\\001_yes.mp3", UriKind.Relative));
            if(!player.IsMuted) player.Stop();
            player.Play ();
          //player.Close();
        }

        // Для работы необходимо подключить WindowsBase.dll PresentationCore.dll
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern
        Boolean PlaySound(string lpszName, int hModule, int dwFlags);

        private static void xxxtest02()
        {   PlaySound("snd\\001_yes.mp3", 4, 255*255);
        }

        ///-------------------|
        /// Thread.           |
        ///-------------------:
        MySounds.eSND id_sound;
        public void play(MySounds.eSND n)
        {   id_sound = n;
            Thread 
            t = new Thread(new ThreadStart(this.foofoo));
            t.Start();
        }

        private void foofoo()
        {   play_t (id_sound);
            //Debug.Out.add("Sound::foofoo - ", id_sound.ToString());
        }

        string name_sound;
        public void play(string s)
        {   name_sound = s;
            Thread
            t = new Thread(new ThreadStart(this.foofoo_name));
            t.Start();
        }

        private void foofoo_name()
        {   play_t (name_sound);
            //Debug.Out.add("Sound::foofoo_name - ", name_sound);
        }
    }
}
