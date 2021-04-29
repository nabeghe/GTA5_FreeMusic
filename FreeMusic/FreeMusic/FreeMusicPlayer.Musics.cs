using GTA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer
    {
        private List<string> musics = new List<string>(); // name of loaded musics files
        private int currentMusicIndex = -1;

        /// <summary>
        /// ریلود کردن موزیک ها از مسیر تنظیم شده
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void LoadMusics()
        {
            // if (!File.Exists(Environment.CurrentDirectory + @"\scripts\FreeMusic.FuLLKade.dll")) return;
            try
            {
                currentMusicIndex = -1;
                musics.Clear();
                var files = Directory.GetFiles(musicsPath, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav"));
                foreach (string file in files)
                {
                    musics.Add(file.Replace(musicsPath, ""));
                }
                GTA.UI.Notify("~y~FreeMusic:~y~\n~g~Loaded Successfully.~g~\n\n~b~WWW.FuLLKade.COM~b~");
            }
            catch
            {
                UI.Notify("~y~FreeMusic:~y~\n~r~Error on Initializing~r~\n\n~y~Contact Us:~y~ ~b~WWW.FuLLKade.COM~b~");
            }
        }
    }
}
