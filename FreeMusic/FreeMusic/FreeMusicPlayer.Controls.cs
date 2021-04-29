using GTA;
using NAudio.Wave;
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
        private volatile float musicVolume;

        private bool handlyStopped;
        private bool isChangeMusicCalled;

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Play()
        {
            try
            {
                Stop();
                var musicPath = musicsPath + musics[currentMusicIndex];
                AudioFileReader audioFile = new AudioFileReader(musicPath);
                musicChannel = new WaveChannel32(audioFile);
                musicChannel.Volume = musicVolume;
                musicChannel.PadWithZeroes = false;
                musicPlayer = new DirectSoundOut();
                musicPlayer.Init(musicChannel);
                musicPlayer.Play();
                UI.ShowSubtitle(Path.GetFileName(musics[currentMusicIndex]));
                handlyStopped = false;
                isChangeMusicCalled = true;
            }
            catch
            {
                Stop();
                UI.Notify("~y~FreeMusic:~y~\n~r~Can't play this music.~r~");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void ChangeMusic(bool next)
        {
            isChangeMusicCalled = false;
            handlyStopped = false;
            var musicsCount = musics.Count;
            if (musicsCount == 0)
            {
                UI.Notify("~y~FreeMusic:~y~\n~r~Your music list is empty.~r~");
                return;
            }
            if (next)
            {
                currentMusicIndex++;
                if (currentMusicIndex >= musicsCount) currentMusicIndex = 0;
            }
            else
            {
                currentMusicIndex--;
                if (currentMusicIndex < 0) currentMusicIndex = musicsCount - 1;
            }
            Play();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void ChangeVolume(bool value)
        {
            if (value)
            {
                musicVolume += Config.VOLUME_STEP;
                if (musicVolume >= 1.0f)
                {
                    musicVolume = 1.0f;
                    //UI.ShowSubtitle("Volume is Maximum");
                }
            }
            else
            {
                musicVolume -= Config.VOLUME_STEP;
                if (musicVolume <= 0)
                {
                    musicVolume = 0;
                    //UI.ShowSubtitle("Volume is Minimum");
                }
            }

            UI.ShowSubtitle("Volume = " + musicVolume.ToString("0.00"));
            if (musicChannel != null) musicChannel.Volume = musicVolume;
            Settings.SetValue("Options", "Volume", musicVolume);
            Settings.Save();
        }

        public void Next()
        {
            ChangeMusic(true);
        }

        public void Prev()
        {
            ChangeMusic(false);
        }
        public void Forward()
        {
            try
            {
                if (musicPlayer.PlaybackState == PlaybackState.Playing)
                    musicChannel.Position += JumpStep;
                else
                    Next();
            }
            catch { }
        }

        public void Backward()
        {
            try
            {
                if (musicPlayer.PlaybackState == PlaybackState.Playing)
                    musicChannel.Position -= JumpStep;
                else
                    Prev();
            }
            catch { }
        }

        public void IncreaseVol()
        {
            ChangeVolume(true);
        }

        public void DecreaseVol()
        {
            ChangeVolume(false);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Stop()
        {
            try { musicChannel.Volume = 0; } catch { }
            try { musicPlayer.Stop(); } catch { }
            try { musicPlayer = null; } catch { }
            try { musicChannel = null; } catch { }
        }

    }
}
