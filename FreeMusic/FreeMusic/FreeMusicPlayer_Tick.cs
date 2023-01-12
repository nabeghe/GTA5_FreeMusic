using GTA;
using NAudio.Wave;
using System;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer
    {
        private void InitializeTick()
        {
            Tick += OnTick;
        }

        /// <summary>
        /// Runs per frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTick(object sender, EventArgs e)
        {
            // If the music playing was finished.
            if (IsFinishedAutomatically())
            {
                // If the loop was enabeld.
                if (Loop)
                {
                    // Play again the current music.
                    Play();
                } else
                {
                    // Go to the next music.
                    Next();
                }
            }

            // If the radio ignoring was enabled.
            if (IgnoreRadio)
            {
                try
                {
                    // Disable the radio of the last vehicle.
                    Game.Player.LastVehicle.IsRadioEnabled = false;
                }
                catch { }
            }

            if (MenusHandler != null)
            {
                try
                {
                    // Proccess the menus.
                    MenusHandler.ProcessMenus();
                }
                catch { }
            }
        }

        /// <summary>
        /// Checks that the music playing finshied automatically or not.
        /// </summary>
        /// <returns></returns>
        private bool IsFinishedAutomatically()
        {
            // If the music player exists and music not stopped handly by gamer and change music was called and current music state was stopped.
            return MusicPlayer != null
               && !HandlyStopped
               && IsChangeMusicCalled
               && MusicPlayer.PlaybackState == PlaybackState.Stopped;
        }
    }
}
