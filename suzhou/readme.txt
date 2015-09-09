2015.08.05
This program is used for monitor data received from serial port.
Implement function:
Add new tab page, rename and delete select page
Add, remove, edit and move label, textbox in each tab.

Incompletements:
Lamp related functions.


2015.09.09
Add new function:
Add lamp control to the wifi monitor, including add remove and property editing.
Modified:
Change the save controls method. Now only change ini file on save button action.In discription, it will delete the context and fill it to new one. On previous version, the file would have change when new controls add and remove. Will cause control load problems when the ini section name is not sequence from 1. Now I think it's quite simple and dramatic things.
