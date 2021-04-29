using GTA;
using GTA.Native;
using NativeUI;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer : Script
    {

        private int cellphone_flashhand;
        private DirectSoundOut musicPlayer;
        private WaveChannel32 musicChannel;

        public FreeMusicPlayer()
        {
            cellphone_flashhand = Function.Call<int>(Hash.GET_HASH_KEY, "cellphone_flashhand");
            LoadSettings();
            SaveSettings(); // اگه قبلا ذخیره نشده بودن، مطمئن میشیم که کلیدها همگی تو فایل هستن
            LoadMusics();
            Tick    += OnTick;
            KeyDown += OnKeyDown;
        }
    }
}