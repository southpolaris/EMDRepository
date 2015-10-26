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



2015.10.23 ver 0.5
Significant modified:
1.Replace the former PLC serial communication to a ModbusRTU slave(MCGS) through TCP communication.
2.A new ModbusRTU slave class to implement some modbus function code.
3.Change the textboxEx class, now it have three property for a number: slave address, modbus interface and data address.
4.Some optimize in UI.
Procedure:
Now impelement the function code 3 and 4, test with a single wifi232 module, can read 16-bits integer.
Not compelement:
1.Write data function, mainly is function 16;
2.Read bool data, function 1 and 2;
3.Read float data using function 3 and 4;
4.One more wifi232 module communication test;


2015.10.24 ver0.6
Add function code 1 and complete lamp display code, now can read coils of modbusslave.
