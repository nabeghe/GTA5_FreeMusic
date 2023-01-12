# GTA5_FreeMusic
GTA V FreeMusic Mod

This mod let you to Play any own music in the game (like a music player in the background). Support mp3 and wav. 

## REQUIREMENTS
- ScriptHookV
- ScriptHookVDotNet
- Naudio.dll (Included in arhive)
- NativeUI.dll (Included in arhive)

## How to install
Simply drag the scripts folder into your GTA V directory OR drag the files from inside the scripts folder into your scripts folder if you already have one!

## How to use
You can put music files (mp3 or wav) into FreeMusic folder in the scripts folder. Of course: you can edit FreeMusic.ini file to change the default musics path.
Notice: FreeMusic load all music files from all subdirectories.

*F11: Menu*

## Hotkeys:
- Ctrl + C : Reload music list (When your game is running and you have added new files to the Musics path.)
- Top Plus button (+) : Increase volume
- Top Mines button (-) : Decrease volume
- Right Arrow : Play next music
- Left Arrow : Play previous music
- Ctrl + Top Plus button : Forwarding music position
- Ctrl + Top Mines button : Backwarding music position
- Ctrl + S : Stop Music

Key codes: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys

## Changes Log
*v1.6:*
- Added 'VOLUME_STEP' to the config.
- Changed menu key to F11 (F12 used for screenshot in the Steam)
- Fixed 'JUMP_STEP' name in the config.
- Code cleanup.

*v1.5:*
- added Looping mode (Options>Loop)
- added Forward/Backward (Jumping)


*v1.0:*
- added UI Menu [F12] / Under NativeUI.dll
- Can Choose music from UI List
- Can Disable hotkeys


*v0.0.8:*
- added FreeMusic.ini file to change some settings and save last volume
- can set custom folder path in the FreeMusic.ini file
- can load all musics from all sub directories
- disable radio automatically (can change from ini file)


v0.0.3:
- ignore mod keys when phone is bring up
