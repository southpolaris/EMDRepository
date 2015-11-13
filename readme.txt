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


2015.10.26 ver0.61
Impelement single register write function (function code 16), add to wifi monitor.

2015.10.27 ver0.62
Pass double MCGS tests for 16bits unsigned number read and write, maybe some problems exists in TCP connection.

2015.10.30 ver0.7
Add support to 32bit integer and single float number read and write.
Problems
thread syncronazition, need optimize in efficiency.
not test fc2 and write coil because of not finish mcgs problem.

2015.11.04 ver0.8
Complie with Visual Sutdio 2013, update .Net framework to 4.0;
Using taskfactory replace threadpool, and some other efficient improvements.
Bug fixes in modbusslave.cs;
Maybe still some problem in data read and write with different tasks.
Main function is totally implement.

2015.11.13 ver0.81
Bug fix and performance improvement.
Fix the add lamp button not connect with event handler.
Timer refersh will only change the controls in selected tab page.
Remove float data in registers.
Last version use ini configure file, from now on it will change into xml format.

Things need to do：
界面优化:
是否可以标签透明或自动更改长度;
Set the limitation of the control move area and prevent controls overlap.(Important)
