using System;
using System.Windows.Forms;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer
    {
        private string _MusicsPath;
        public string MusicsPath
        {
            get
            {
                return _MusicsPath;
            }
            set
            {
                _MusicsPath = value.Replace("%GTA%", Environment.CurrentDirectory).Replace("/", @"\");
                if (!_MusicsPath.EndsWith(@"\")) _MusicsPath += @"\";
            }
        }


        private volatile float _Volume;
        public float Volume
        {
            get
            {
                return _Volume;
            }
            set
            {
                if (value > 1.0f) value = 1.0f; else if (value < 0) value = 0;
                _Volume = value;
                if (MusicChannel != null) MusicChannel.Volume = _Volume;
            }
        }

        private float _VolumeStep;
        public float VolumeStep
        {
            get
            {
                return _VolumeStep;
            }
            set
            {
                if (value > 1f) value = 1f;
                if (value <= 0) value = 0.01f;
                _VolumeStep = value;
            }
        }

        public Keys KeyMenu { get; set; }
        public Keys KeyReload { get; set; }
        public Keys KeyStop { get; set; }
        public Keys KeyNext { get; set; }
        public Keys KeyPrevious { get; set; }
        public Keys KeyVolumeUp { get; set; }
        public Keys KeyVolumeDown { get; set; }
        public Keys KeyForward { get; set; }
        public Keys KeyBackward { get; set; }

        public bool IgnorePhone { get; set; }
        public bool IgnoreRadio { get; set; }
        public bool ActiveHotkeys { get; set; }
        public bool Loop { get; set; }
        public long _JumpStep;
        public long JumpStep
        {
            get
            {
                return _JumpStep;
            }
            set
            {
                if (value < 0) value = 0;
                _JumpStep = value;
            }
        }

        /// <summary>
        /// The Initilizer.
        /// </summary>
        private void InitializeSettings()
        {
            LoadSettings();
            SaveSettings(); // Save settings to ensure that all keys are present in the ini config file.
        }

        /// <summary>
        /// Loads all settings from ini file.
        /// </summary>
        public void LoadSettings()
        {
            /// KEYS:
            KeyMenu = base.Settings.GetValue("KEYS", "MENU", Config.KEY_MENU);
            KeyReload = base.Settings.GetValue("KEYS", "RELOAD", Config.KEY_RELOAD);
            KeyStop = base.Settings.GetValue("KEYS", "STOP", Config.KEY_STOP);
            KeyNext = base.Settings.GetValue("KEYS", "NEXT", Config.KEY_NEXT);
            KeyPrevious = base.Settings.GetValue("KEYS", "PREVIOUS", Config.KEY_PREVIOUS);
            KeyVolumeUp = base.Settings.GetValue("KEYS", "VOLUME_UP", Config.KEY_VOLUMN_UP);
            KeyVolumeDown = base.Settings.GetValue("KEYS", "VOLUME_DOWN", Config.KEY_VOLUMN_DOWN);
            KeyForward = base.Settings.GetValue("KEYS", "FORWARD", Config.KEY_FORWARD);
            KeyBackward = base.Settings.GetValue("KEYS", "BACKWARD", Config.KEY_BACKWARD);

            /// OPTIONS:
            MusicsPath = base.Settings.GetValue("OPTIONS", "MUSICS_PATH", Config.MUSICS_PATH);
            Volume = base.Settings.GetValue("OPTIONS", "VOLUME", Config.VOLUM);
            VolumeStep = base.Settings.GetValue("OPTIONS", "VOLUME_STEP", Config.VOLUME_STEP);
            IgnorePhone = base.Settings.GetValue("OPTIONS", "IGNORE_PHONE", Config.IGNORE_PHONE);
            IgnoreRadio = base.Settings.GetValue("OPTIONS", "IGNORE_RADIO", Config.IGNORE_RADIO);
            ActiveHotkeys = base.Settings.GetValue("OPTIONS", "ACTIVE_HOTKEYS", Config.ACTIVE_HOTKEYS);
            Loop = base.Settings.GetValue("OPTIONS", "LOOP", Config.LOOP);
            JumpStep = base.Settings.GetValue("OPTIONS", "JUMP_STEP", Config.JUMP_STEP);
        }

        /// <summary>
        /// Saves all settings in the ini file.
        /// </summary>
        public void SaveSettings()
        {
            base.Settings.SetValue("KEYS", "MENU", KeyMenu);
            base.Settings.SetValue("KEYS", "RELOAD", KeyReload);
            base.Settings.SetValue("KEYS", "STOP", KeyStop);
            base.Settings.SetValue("KEYS", "NEXT", KeyNext);
            base.Settings.SetValue("KEYS", "PREVIOUS", KeyPrevious);
            base.Settings.SetValue("KEYS", "VOLUME_UP", KeyVolumeUp);
            base.Settings.SetValue("KEYS", "VOLUME_DOWN", KeyVolumeDown);
            base.Settings.SetValue("KEYS", "FORWARD", KeyForward);
            base.Settings.SetValue("KEYS", "BACKWARD", KeyBackward);
            base.Settings.SetValue("OPTIONS", "MUSICS_PATH", MusicsPath.Replace(Environment.CurrentDirectory, "%GTA%"));
            base.Settings.SetValue("OPTIONS", "VOLUME", Volume);
            base.Settings.SetValue("OPTIONS", "IGNORE_PHONE", IgnorePhone);
            base.Settings.SetValue("OPTIONS", "IGNORE_RADIO", IgnoreRadio);
            base.Settings.SetValue("OPTIONS", "ACTIVE_HOTKEYS", ActiveHotkeys);
            base.Settings.SetValue("OPTIONS", "LOOP", Loop);
            base.Settings.SetValue("OPTIONS", "JUMP_STEP", JumpStep);
            base.Settings.Save();
        }
    }
}