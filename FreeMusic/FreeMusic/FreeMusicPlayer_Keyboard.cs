using GTA;
using GTA.Native;
using System.Windows.Forms;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer
    {
        private void InitializeKeyboard()
        {
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Ignore the script keys in the some states: phone is open, game paused and loading.
            if ((IgnorePhone && Function.Call<int>(Hash._GET_NUMBER_OF_INSTANCES_OF_STREAMED_SCRIPT, CellphoneFlashhand) > 0)
                || Game.IsPaused
                || Game.IsLoading) return;

            // If wants to open/close the menu.
            if (e.KeyCode == KeyMenu)
            {
                ToggleMenu();
            }

            // If the hotkeys was active and no menu is open.
            else if (ActiveHotkeys && (MenusHandler == null || !MenusHandler.IsAnyMenuOpen()))
            {
                // some hotkeys works with the ctrl key.
                if (e.Control)
                {
                    // Reload musics list.
                    if (e.KeyCode == KeyReload)
                    {
                        LoadMusics();
                    }
                    
                    // Stop.
                    else if (e.KeyCode == KeyStop)
                    {
                        // Stopping is done manually by the gamer.
                        HandlyStopped = true;
                        IsChangeMusicCalled = false;
                        Stop();
                    }

                    // Forward.
                    else if (e.KeyCode == Keys.Oemplus)
                    {
                        Forward();
                    }
                    // Backward.
                    else if (e.KeyCode == Keys.OemMinus)
                    {
                        Backward();
                    }
                }

                // Next.
                else if (e.KeyCode == KeyNext) Next();
                // Prev.
                else if (e.KeyCode == KeyPrevious) Prev();

                // Increase volum.
                else if (e.KeyCode == KeyVolumeUp) IncreaseVol();
                // Decrease volum
                else if (e.KeyCode == KeyVolumeDown) DecreaseVol();
            }
        }
    }
}
