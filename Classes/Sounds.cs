﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Media;

using System.Windows.Media; // WindowsBase.dll, PresentationCore.dll

namespace BDOtimers
{
    //-------------------------------------------------------------------------|
    //  MySounds
    //-------------------------------------------------------------------------:
    public class MySounds
    {
        public MySounds()
        {   load_sounds();
        }

        public enum eSND //  sounds.play(MySounds.eSND.MOVE);
        {   ALARM0,
            ALARM1,
            // ...
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
                    select Path.GetFileName(a)
                ).ToList();
            sp = new SoundPlayer[files.Count];
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

        public void play      (MySounds.eSND I)
        {   int i = (int)I;
            sp [i] = new System.Media.SoundPlayer("snd/" + files[(int)i]);
            sp [i].Play();
        }
        public void stop(MySounds.eSND I)
        {   sp[(int)I].Stop();
        }

        public void play_sync (MySounds.eSND I)
        {   int i = (int)I;
            sp [i] = new System.Media.SoundPlayer("snd/" + files[i]);
            sp [i].PlaySync();
        }

        public void xplay     (MySounds.eSND i)
        {   my[(int)i].Play();
        }
        public void xplay_sync(MySounds.eSND i)
        {   my[(int)i].PlaySync();
        }

        // Для работы необходимо подключить WindowsBase.dll PresentationCore.dll
        // [System.Runtime.InteropServices.DllImport("winmm.dll")]
        // private static extern
        // Boolean PlaySound(string lpszName, int hModule, int dwFlags);
        static MediaPlayer player = new MediaPlayer();
        public static void xxxtest()
        {   player .Open(new Uri("snd\\boyz.mp3", UriKind.Relative));
            player .Play   ();
          //player .Close  ();
        }
    }
}