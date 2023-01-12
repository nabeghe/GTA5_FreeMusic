using NativeUI;
using System.Collections.Generic;
using System.IO;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer
    {
        private volatile MenuPool _MenusHandler;

        /// <summary>
        /// Menus handler.
        /// </summary>
        public MenuPool MenusHandler
        {
            get
            {
                return _MenusHandler;
            }
            private set
            {
                _MenusHandler = value;
            }
        }

        private volatile UIMenu _Menu;

        /// <summary>
        /// Menu object.
        /// </summary>
        public UIMenu Menu
        {
            get
            {
                return _Menu;
            }
            private set
            {
                _Menu = value;
            }
        }
        public int MenuLastSelected { get; private set; } = 0;

        /// <summary>
        /// Toggle (Open/Close) menu.
        /// </summary>
        public void ToggleMenu()
        {
            // Initialize menus handler and menu.
            if (MenusHandler == null)
            {
                MenusHandler = new MenuPool();
                Menu = new UIMenu("Free Music", $"Verrsion {Config.VERSION}");
                MenusHandler.Add(Menu);

                var musicsMenu = MenusHandler.AddSubMenu(Menu, "Choose from list");
                musicsMenu.OnItemSelect += MusicsMenu_OnItemSelect;
                int musicsCount = Musics.Count;
                if (musicsCount == 0)
                {
                    // musicsMenu.AddItem(new UIMenuItem("Empty"));
                }
                else
                {
                    // add each music to the menu items.
                    for (int i = 0; i < musicsCount; i++)
                    {
                        musicsMenu.AddItem(new UIMenuItem(Path.GetFileNameWithoutExtension(Musics[i])));
                    }
                    // changes the current music to the first music if it's not initialized before.
                    if (CurrentMusicIndex == -1) CurrentMusicIndex = 0;
                    if (CurrentMusicIndex > musicsCount) CurrentMusicIndex = musicsCount - 1;
                    // select the current music item.
                    musicsMenu.CurrentSelection = CurrentMusicIndex;
                }

                /// Options.
                Menu.AddItem(new UIMenuItem("Next Music")); // 1
                Menu.AddItem(new UIMenuItem("Previous Music")); // 2
                Menu.AddItem(new UIMenuItem("Forward")); // 3
                Menu.AddItem(new UIMenuItem("Backward")); // 4
                Menu.AddItem(new UIMenuItem("Stop")); // 5
                Menu.AddItem(new UIMenuListItem("Volume ( " + Volume.ToString("0.00") + " )", new List<dynamic> { "-", "+" }, 0, "Choose Plus or Mines, then press enter")); // 6
                Menu.AddItem(new UIMenuItem("Reload Musics")); // 7

                var optionsMenu = MenusHandler.AddSubMenu(Menu, "Options");
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
                    base.Settings.SetValue("OPTIONS", item.Text.Replace(" ", "_").ToUpper(), checked_);
                    if (item == cbLoop) Loop = checked_;
                    else if (item == cbActiveHotkey) ActiveHotkeys = checked_;
                    else if (item == cbIgnoreRadio) IgnoreRadio = checked_;
                    else if (item == cbIgnorePhone) IgnorePhone = checked_;
                    base.Settings.Save();
                };
                optionsMenu.CurrentSelection = 0;

                Menu.OnItemSelect += (UIMenu sender, UIMenuItem selectedItem, int index) =>
                {
                    switch (index)
                    {
                        case 0:
                            if (CurrentMusicIndex == -1) CurrentMusicIndex = 0;
                            musicsMenu.CurrentSelection = CurrentMusicIndex;
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
                            item.Text = "Volume ( " + Volume.ToString("0.00") + " )";
                            break;
                        case 7:
                            LoadMusics();
                            break;
                    }
                };
                Menu.OnIndexChange += MainMenu_OnIndexChange;

                Menu.MouseEdgeEnabled = false;
                Menu.RefreshIndex();
                Menu.CurrentSelection = MenuLastSelected;
                Menu.Visible = true;
            }
            else
            {
                Menu.Visible = false;
                try { MenusHandler.CloseAllMenus(); } catch { }
                try { Menu = null; } catch { }
                try { MenusHandler = null; } catch { }
            }
        }

        private void MusicsMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            CurrentMusicIndex = index;
            Play();
        }

        private void MainMenu_OnIndexChange(UIMenu sender, int newIndex)
        {
            MenuLastSelected = newIndex;
        }
    }
}
