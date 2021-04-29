using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer
    {
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((IgnorePhone && Function.Call<int>(Hash._GET_NUMBER_OF_INSTANCES_OF_STREAMED_SCRIPT, cellphone_flashhand) > 0) || Game.IsPaused || Game.IsLoading) return;

            if (e.KeyCode == keyMenu)
            {
                ToggleMenu();
            }
            else if (ActiveHotkeys && (menuPool == null || !menuPool.IsAnyMenuOpen()))
            {
                
                if (e.Control)
                {
                    if (e.KeyCode == keyReload) LoadMusics();
                    else if (e.KeyCode == keyStop)
                    {
                        handlyStopped = true;
                        isChangeMusicCalled = false;
                        Stop();
                    } else if (e.KeyCode == Keys.Oemplus)
                    {
                        Forward();
                    }
                    else if (e.KeyCode == Keys.OemMinus)
                    {
                        Backward();
                    }
                }
                else if (e.KeyCode == keyNext) Next();
                else if (e.KeyCode == keyPrevious) Prev();
                else if (e.KeyCode == keyVolumeUp) IncreaseVol();
                else if (e.KeyCode == keyVolumeDown) DecreaseVol();
            }
        }
    }
}
