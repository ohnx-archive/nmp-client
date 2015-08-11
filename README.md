# nmp-client

c# windows "client" for [node-music-player](https://github.com/ohnx/node-music-player).

## about
nmp-client hooks media keys to control buttons and shows the main browser window.

because this uses IE/Spartan as a browser host, the scrollbar for songs doesn't style correctly.

the only alternative i knew of was to use cef/chromium but that doesn't support mp3 natively, so I decided to go with the native browser host.

## install
all you need is `nmp-client.exe`, .NET 4.0, and love.

## configuration
put a file named `config.txt` in the same directory where `nmp-client.exe` containing a URL to a version of [node-music-player](https://github.com/ohnx/node-music-player).
