using GTA;
using GTA.Native;
using NAudio.Wave;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer : Script
    {
        public int CellphoneFlashhand { get; private set; }
        public DirectSoundOut MusicPlayer { get; private set; }
        public WaveChannel32 MusicChannel { get; private set; }

        public FreeMusicPlayer()
        {
            CellphoneFlashhand = Function.Call<int>(Hash.GET_HASH_KEY, "cellphone_flashhand");
            InitializeSettings();
            InitilizeMusics();
            InitializeTick();
            InitializeKeyboard();
        }
    }
}