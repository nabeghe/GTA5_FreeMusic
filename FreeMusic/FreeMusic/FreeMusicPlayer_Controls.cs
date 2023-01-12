using GTA;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace FuLLKade.FreeMusic
{
    /// <summary>
    /// Controls.
    /// </summary>
    public partial class FreeMusicPlayer
    {
        public bool HandlyStopped { get; private set; }
        public bool IsChangeMusicCalled { get; private set; }

        /// <summary>
        /// Play the current music.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Play()
        {
            try
            {
                // Ensure stop.
                Stop();

                var musicPath = MusicsPath + Musics[CurrentMusicIndex];

                AudioFileReader audioFile = new AudioFileReader(musicPath);
                MusicChannel = new WaveChannel32(audioFile);
                MusicChannel.Volume = Volume;
                MusicChannel.PadWithZeroes = false;
                MusicPlayer = new DirectSoundOut();
                MusicPlayer.Init(MusicChannel);
                MusicPlayer.Play();

                UI.ShowSubtitle(Path.GetFileName(Musics[CurrentMusicIndex]));
                HandlyStopped = false;
                IsChangeMusicCalled = true;
            }
            catch
            {
                // Ensure stop.
                Stop();
                // Show message.
                UI.Notify("~y~FreeMusic:~y~\n~r~Can't play this music.~r~");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Stop()
        {
            try { MusicChannel.Volume = 0; } catch { }
            try { MusicPlayer.Stop(); } catch { }
            try { MusicPlayer = null; } catch { }
            try { MusicChannel = null; } catch { }
        }

        /// <summary>
        /// Go to the next or previous music in the list.
        /// </summary>
        /// <param name="next"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void ChangeMusic(bool next)
        {
            IsChangeMusicCalled = false;
            HandlyStopped = false;

            // Check exists any music.
            var musicsCount = Musics.Count;
            if (musicsCount == 0)
            {
                UI.Notify("~y~FreeMusic:~y~\n~r~Your music list is empty.~r~");
                return;
            }

            // Go to next music.
            if (next)
            {
                CurrentMusicIndex++;
                // If the next music not found, so starts from the beginning of list.
                if (CurrentMusicIndex >= musicsCount) CurrentMusicIndex = 0;
            }
            // Go to previous music.
            else
            {
                CurrentMusicIndex--;
                // If the previous music not found, so starts from the ends of list.
                if (CurrentMusicIndex < 0) CurrentMusicIndex = musicsCount - 1;
            }

            // Auto play after changed the music.
            Play();
        }

        /// <summary>
        /// Go to the next music in the list.
        /// </summary>
        public void Next()
        {
            ChangeMusic(true);
        }

        /// <summary>
        /// Go to the previous music in the list.
        /// </summary>
        public void Prev()
        {
            ChangeMusic(false);
        }

        /// <summary>
        /// Forwards the playing music.
        /// </summary>
        public void Forward()
        {
            try
            {
                if (MusicPlayer.PlaybackState == PlaybackState.Playing)
                    MusicChannel.Position += JumpStep;
                else
                    Next();
            }
            catch { }
        }

        /// <summary>
        /// Backwards the playing music.
        /// </summary>
        public void Backward()
        {
            try
            {
                if (MusicPlayer.PlaybackState == PlaybackState.Playing)
                    MusicChannel.Position -= JumpStep;
                else
                    Prev();
            }
            catch { }
        }

        /// <summary>
        /// Changes the music volumn.
        /// </summary>
        /// <param name="increase"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void ChangeVolume(bool increase)
        {
            if (increase)
            {
                Volume += VolumeStep;
            }
            else
            {
                Volume -= VolumeStep;
            }

            UI.ShowSubtitle("Volume = " + Volume.ToString("0.00"));

            // Save the music volum in the config file (ini).
            base.Settings.SetValue("Options", "VOLUME", Volume);
            base.Settings.Save();
        }

        /// <summary>
        /// Increase the music volum.
        /// </summary>
        public void IncreaseVol()
        {
            ChangeVolume(true);
        }

        /// <summary>
        /// Decrease the music volum.
        /// </summary>
        public void DecreaseVol()
        {
            ChangeVolume(false);
        }

    }
}
