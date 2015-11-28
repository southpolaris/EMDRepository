using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace WifiMonitor
{
    public interface ICommunicate
    {
        /// <summary>
        /// Read 32 bits data from PLC memory
        /// </summary>
        /// <param name="startAddress">start soft element location (data location * 2, must be even)</param>
        /// <param name="length">soft element number (data length * 2)</param>
        /// <param name="data"></param>
        /// <returns>True means success</returns>
        bool ReadData(ushort startAddress, ushort count, ref int[] data);
        bool WriteIntData(ushort startAddress, int data);
        bool WriteBoolData(ushort startAddress, bool data);
    }
}
