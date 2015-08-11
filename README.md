# nmp-client

windows client for [node-music-player](https://github.com/ohnx/node-music-player)

hooks media keys to control buttons and shows the main browser window.

because this uses IE/Spartan as a browser host, the scrollbar for songs doesn't style correctly.
the alternative was to use cef/chromium but that doesn't support mp3 natively, so I went with this.