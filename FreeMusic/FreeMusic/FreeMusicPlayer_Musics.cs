using GTA;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer
    {
        public List<string> Musics { get; private set; } = new List<string>();
        public int CurrentMusicIndex { get; private set; } = -1;

        private void InitilizeMusics()
        {
            LoadMusics();
        }

        /// <summary>
        /// Reloads the all musics from the musics path.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void LoadMusics()
        {
            try
            {
                CurrentMusicIndex = -1;

                Musics.Clear();
                var files = Directory.GetFiles(MusicsPath, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav"));

                // Remove the root path from each music file path.
                foreach (string file in files) Musics.Add(file.Replace(MusicsPath, ""));

                // Show success message.
                GTA.UI.Notify($"~y~FreeMusic:~y~\n~g~Loaded Successfully.~g~\n\n~b~{Config.SIGNATURE}~b~");
            }
            catch
            {
                // Show unsuccess message
                UI.Notify($"~y~FreeMusic:~y~\n~r~Error on Initializing~r~\n\n~y~Contact Us:~y~ ~b~{Config.SIGNATURE}~b~");
            }
        }
    }
}