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
        private string musicsPath;

        private Keys keyMenu;
        private Keys keyReload;
        private Keys keyStop;
        private Keys keyNext;
        private Keys keyPrevious;
        private Keys keyVolumeUp;
        private Keys keyVolumeDown;
        private Keys keyForward;
        private Keys keyBackward;

        private bool IgnorePhone { get; set; }
        private bool IgnoreRadio { get; set; }
        private bool ActiveHotkeys { get; set; }
        private bool Loop { get; set; }
        private long JumpStep { get; set; }

        private void LoadSettings()
        {
            keyMenu       = Settings.GetValue("KEYS", "MENU", Config.KEY_MENU);
            keyReload     = Settings.GetValue("KEYS", "RELOAD", Config.KEY_RELOAD);
            keyStop       = Settings.GetValue("KEYS", "STOP", Config.KEY_STOP);
            keyNext       = Settings.GetValue("KEYS", "NEXT", Config.KEY_NEXT);
            keyPrevious   = Settings.GetValue("KEYS", "PREVIOUS", Config.KEY_PREVIOUS);
            keyVolumeUp   = Settings.GetValue("KEYS", "VOLUME_UP", Config.KEY_VOLUMN_UP);
            keyVolumeDown = Settings.GetValue("KEYS", "VOLUME_DOWN", Config.KEY_VOLUMN_DOWN);
            keyForward    = Settings.GetValue("KEYS", "FORWARD", Config.KEY_FORWARD);
            keyBackward   = Settings.GetValue("KEYS", "BACKWARD", Config.KEY_BACKWARD);
            musicsPath    = Settings.GetValue("OPTIONS", "MUSICS_PATH", Config.MUSICS_PATH);
            musicVolume   = Settings.GetValue("OPTIONS", "VOLUME", Config.VOLUM);
            IgnorePhone   = Settings.GetValue("OPTIONS", "IGNORE_PHONE", Config.IGNORE_PHONE);
            IgnoreRadio   = Settings.GetValue("OPTIONS", "IGNORE_RADIO", Config.IGNORE_RADIO);
            ActiveHotkeys = Settings.GetValue("OPTIONS", "ACTIVE_HOTKEYS", Config.ACTIVE_HOTKEYS);
            Loop          = Settings.GetValue("OPTIONS", "LOOP", Config.LOOP);
            JumpStep = Settings.GetValue("OPTIONS", "LOOP", Config.JUMP_STEP);

            musicsPath = musicsPath.Replace("%GTA%", Environment.CurrentDirectory);
            musicsPath = musicsPath.Replace("/", @"\");
            if (!musicsPath.EndsWith(@"\")) musicsPath += @"\";
            if (musicVolume > 1.0f) musicVolume = 1.0f; else if (musicVolume < 0) musicVolume = 0;
            if (JumpStep < 0) JumpStep = 0;
        }

        private void SaveSettings()
        {
            Settings.SetValue("KEYS", "MENU", keyMenu);
            Settings.SetValue("KEYS", "RELOAD", keyReload);
            Settings.SetValue("KEYS", "STOP", keyStop);
            Settings.SetValue("KEYS", "NEXT", keyNext);
            Settings.SetValue("KEYS", "PREVIOUS", keyPrevious);
            Settings.SetValue("KEYS", "VOLUME_UP", keyVolumeUp);
            Settings.SetValue("KEYS", "VOLUME_DOWN", keyVolumeDown);
            Settings.SetValue("KEYS", "FORWARD", keyForward);
            Settings.SetValue("KEYS", "BACKWARD", keyBackward);
            Settings.SetValue("OPTIONS", "MUSICS_PATH", musicsPath.Replace(Environment.CurrentDirectory, "%GTA%"));
            Settings.SetValue("OPTIONS", "VOLUME", musicVolume);
            Settings.SetValue("OPTIONS", "IGNORE_PHONE", IgnorePhone);
            Settings.SetValue("OPTIONS", "IGNORE_RADIO", IgnoreRadio);
            Settings.SetValue("OPTIONS", "ACTIVE_HOTKEYS", ActiveHotkeys);
            Settings.SetValue("OPTIONS", "LOOP", Loop);
            Settings.SetValue("OPTIONS", "JUMP_STEP", JumpStep);
            Settings.Save();
        }
    }
}