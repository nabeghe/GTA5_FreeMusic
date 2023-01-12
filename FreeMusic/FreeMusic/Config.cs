using System.Windows.Forms;

namespace FuLLKade.FreeMusic
{
    /// <summary>
    /// Configuration
    /// </summary>
    public class Config
    {
        public const string VERSION = "1.6.1";
        public const string SIGNATURE = "ELATEL.IR";

        public const Keys KEY_MENU = Keys.F11;
        public const Keys KEY_RELOAD = Keys.C;
        public const Keys KEY_STOP = Keys.S;
        public const Keys KEY_NEXT = Keys.Right;
        public const Keys KEY_PREVIOUS = Keys.Left;
        public const Keys KEY_VOLUMN_UP = Keys.Oemplus;
        public const Keys KEY_VOLUMN_DOWN = Keys.OemMinus;
        public const Keys KEY_FORWARD = Keys.Oemplus;
        public const Keys KEY_BACKWARD = Keys.OemMinus;

        public const string MUSICS_PATH = @"%GTA%\scripts\FreeMusic\"; // Default Musics Path.
        public const float VOLUM = 0.8f; // Default Volume.
        public const float VOLUME_STEP = 0.05f;
        public const bool IGNORE_PHONE = true;
        public const bool IGNORE_RADIO = true;
        public const bool ACTIVE_HOTKEYS = true;
        public const bool LOOP = false;
        public const long JUMP_STEP = 1000000;
    }
}