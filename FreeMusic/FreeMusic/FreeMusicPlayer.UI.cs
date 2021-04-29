using NativeUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer
    {
        private volatile MenuPool menuPool;
        private volatile UIMenu mainMenu;
        private int mainMenuLastSelected = 0;

        public void ToggleMenu()
        {
            if (menuPool == null)
            {
                menuPool = new MenuPool();
                mainMenu = new UIMenu("Free Music", $"Verrsion {Config.VERSION}");
                menuPool.Add(mainMenu);

                var musicsMenu = menuPool.AddSubMenu(mainMenu, "Choose from list"); // 0
                musicsMenu.OnItemSelect += MusicsMenu_OnItemSelect;
                int musicsCount = musics.Count;
                if (musicsCount == 0)
                {
                    // musicsMenu.AddItem(new UIMenuItem("Empty"));
                }
                else
                {
                    for (int i = 0; i < musicsCount; i++)
                    {
                        musicsMenu.AddItem(new UIMenuItem(Path.GetFileNameWithoutExtension(musics[i])));
                    }
                    if (currentMusicIndex == -1) currentMusicIndex = 0;
                    musicsMenu.CurrentSelection = currentMusicIndex;
                }

                mainMenu.AddItem(new UIMenuItem("Next Music")); // 1
                mainMenu.AddItem(new UIMenuItem("Previous Music")); // 2
                mainMenu.AddItem(new UIMenuItem("Forward")); // 3
                mainMenu.AddItem(new UIMenuItem("Backward")); // 4
                mainMenu.AddItem(new UIMenuItem("Stop")); // 5
                mainMenu.AddItem(new UIMenuListItem("Volume ( " + musicVolume.ToString("0.00") + " )", new List<dynamic> { "-", "+" }, 0, "Choose Plus or Mines, then press enter")); // 6
                mainMenu.AddItem(new UIMenuItem("Reload Musics")); // 7

                var optionsMenu = menuPool.AddSubMenu(mainMenu, "Options");
                var cbLoop = new UIMenuCheckboxItem("Loop", Loop);
                var cbActiveHotkey = new UIMenuCheckboxItem("Active Hotkeys", ActiveHotkeys);
                var cbIgnoreRadio = new UIMenuCheckboxItem("Ignore Radio", IgnoreRadio);
                var cbIgnorePhone = new UIMenuCheckboxItem("Ignore Phone", IgnorePhone);
                optionsMenu.AddItem(cbLoop);
                optionsMenu.AddItem(cbActiveHotkey);
                optionsMenu.AddItem(cbIgnoreRadio);
                optionsMenu.AddItem(cbIgnorePhone);
                optionsMenu.OnCheckboxChange += (sender, item, checked_) =>
                {
                    Settings.SetValue("OPTIONS", item.Text.Replace(" ", "_").ToUpper(), checked_);
                    if (item == cbLoop) Loop = checked_;
                    else if (item == cbActiveHotkey) ActiveHotkeys = checked_;
                    else if (item == cbIgnoreRadio) IgnoreRadio = checked_;
                    else if (item == cbIgnorePhone) IgnorePhone = checked_;
                    Settings.Save();
                };
                optionsMenu.CurrentSelection = 0;

                mainMenu.OnItemSelect += (UIMenu sender, UIMenuItem selectedItem, int index) =>
                {
                    switch (index)
                    {
                        case 0:
                            if (currentMusicIndex == -1) currentMusicIndex = 0;
                            musicsMenu.CurrentSelection = currentMusicIndex;
                            break;
                        case 1:
                            Next();
                            break;
                        case 2:
                            Prev();
                            break;
                        case 3:
                            Forward();
                            break;
                        case 4:
                            Backward();
                            break;
                        case 5:
                            Stop();
                            break;
                        case 6:
                            var item = (UIMenuListItem)selectedItem;
                            ChangeVolume(item.Index == 1);
                            item.Text = "Volume ( " + musicVolume.ToString("0.00") + " )";
                            break;
                        case 7:
                            LoadMusics();
                            break;
                    }
                };
                mainMenu.OnIndexChange += MainMenu_OnIndexChange;

                mainMenu.MouseEdgeEnabled = false;
                mainMenu.RefreshIndex();
                mainMenu.CurrentSelection = mainMenuLastSelected;
                mainMenu.Visible = true;
            }
            else
            {
                mainMenu.Visible = false;
                try { menuPool.CloseAllMenus(); } catch { }
                try { mainMenu = null; } catch { }
                try { menuPool = null; } catch { }
            }
        }

        private void MusicsMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            currentMusicIndex = index;
            Play();
        }

        private void MainMenu_OnIndexChange(UIMenu sender, int newIndex)
        {
            mainMenuLastSelected = newIndex;
        }
    }
}
