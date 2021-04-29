using GTA;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuLLKade.FreeMusic
{
    public partial class FreeMusicPlayer
    {
        private void OnTick(object sender, EventArgs e)
        {
            if (IsFinishedAutomatically())
            {
                if (Loop)
                {
                    Play();
                } else
                {
                    Next();
                }
            }
            if (IgnoreRadio) // اگه قراره رادیو غیرفعال باشه
            {
                try
                {
                    // رادیوی آخرین ماشین کاربر رو غیرفعال کن
                    Game.Player.LastVehicle.IsRadioEnabled = false;
                }
                catch { }
            }

            if (menuPool != null)
            {
                try
                {
                    menuPool.ProcessMenus();
                }
                catch { }
            }


        }

        /// <summary>
        /// بررسی اینکه آهنگ به اتمام رسیده و بایستی به آهنگ بعدی بره
        /// </summary>
        /// <returns></returns>
        private bool IsFinishedAutomatically()
        {
            // اگه موزیک پلیر وجود داشت
            // اگه وضعیت پخش موزیک متوقف شده بود
            // اگه موزیک توسط کاربر متوقف نشده بود - پس پخشش اتمام رسیده
            // اگه تغییر موزیک صدا زده شده بود
            return musicPlayer != null
               && !handlyStopped
               && isChangeMusicCalled
               && musicPlayer.PlaybackState == PlaybackState.Stopped;
        }
    }
}
