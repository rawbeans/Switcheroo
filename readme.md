<img src="logo.png" alt="Switcheroo" width="48px" height="48px"> Switcheroo + mods
==========

This is my modded version of [Switcheroo](https://github.com/kvakulo/Switcheroo) branched after the maintainer became unresponsive.
It is distributed as-is with no warranty or guarantees whatsoever. Your patches are welcome.

Download
--------

Grab the binaries **[here](https://github.com/elig0n/Switcheroo/releases)**

Custom Features
-------
- *Numerical quick access* - Alt+digit for easy switching to any of the first 10 applications (also hinted on-screen).
- *Light/Dark Theme*
- *Sort* list by process name or title via tray icon menu or shortcut keys in-live list
- *Tray* icon single click open, extended context menu actions.
- Windows list context menu *actions*: close, run, clone instance (duplicate), bring to front
- Home/End/PageUp/PageDown *keys* navigation in the list
- VIM-like navigation *keys* Alt+j Alt+k. Alt+Up/down is working now too for when you opened the application with alt+ shortcut pressed down.
- Export list to JSON

*Planned:*
-------
- Performance improvements
- Code cleaning and refactoring, 
- Custom hotkeys configuration

Shortcuts
-----

Action                         | Shortcut        | Remarks
------------------------------ | --------------- | ----------
Activate Switcheroo            | `Alt + Space`   | This shortcut can be customized in _Options_
Activate Switcheroo            | `Alt + Tab`     | Only works if enabled under _Options_
Activate secondary switcher    | `Alt + `\`      | Switch between windows of foreground application
_When Switcheroo is open_      |                 |
Switch to selected window      | `Enter`,`Alt`   |
Select next/previous           | `Tab`/`Shift + Tab`, `Alt + J`/`Alt + K` | 
Selection jumps                | `Home`, `End`, `PageUp`, `PageDown`| First, Last, Page up, Page down
Switch to n-th window          | `Alt + 1..0`    | For first ten on the list. 0 for tenth.
Close selected window          | `Ctrl + W`,`Alt + X`|
Options                        | `Alt + O`       |
Toggle sort by process name    | `Alt + S`       |
Dismiss Switcheroo             | `Esc`           |

Discord
------
Please join our [Discord channel](https://discord.gg/8zMj7fz) for further discussions.
