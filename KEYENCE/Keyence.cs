using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace WifiMonitor
{
    public class KEYENCE : ICommunicate
    {
        private NetworkStream ns { get; set; }
        private object lockthis { get; set; }
        private static bool startFlag = false;

        public KEYENCE(NetworkStream networkStream, object lockObj)
        {
            ns = networkStream;
            lockthis = lockObj;
        }

        /// <summary>
        /// Read data from Keyence PLC DM using KV mode
        /// </summary>
        /// <param name="startAddress">start address must be even number</param>
        /// <param name="length">soft element number(for here equal to data length *2)</param>
        /// <param name="data">get int32 data</param>
        /// <returns></returns>
        public bool ReadData(ushort startAddress, ushort length, ref int[] data)
        {
            int dataLength = length / 2;
            while (startFlag == false) //For the first time send start message
            {
                byte[] startByte = Encoding.ASCII.GetBytes("CR" + "\r");
                byte[] startResponse = new byte[4];
                try
                {
                    ns.Write(startByte, 0, startByte.Length);
                    ns.Read(startResponse, 0, startResponse.Length);
                }
                catch (Exception)
                {
                    return false;
                }

                if (ByteEquals(startResponse, new byte[] {0x43, 0x43, 0x0D, 0x0A}))
                {
                    startFlag = true;
                }
            }

            string messageSend = "RDS DM" + startAddress.ToString() + ".L " + dataLength.ToString() + "\r";
            byte[] message = Encoding.ASCII.GetBytes(messageSend);
            byte[] response = new byte[12 * dataLength + 1];
            string receive;
            lock (lockthis)
            {
                try
                {
                    ns.Write(message, 0, messageSend.Length);
                    GetResponse(ref response);
                    receive = Encoding.ASCII.GetString(response, 0, response.Length);
                }
                catch (Exception)
                {
                    return false;
                } 
            }
              
            if (!receive.Contains("\r\n")) //no ending data error
            {
                return false;
            }

            string[] stringData = receive.Split('\r')[0].Split(' ');
            if (stringData.Length == dataLength) //Read length not equal to request data number
            {
                //Handling receiveData
                for (int index = 0; index < stringData.Length; index++)
                {
                    data[index] = int.Parse(stringData[index]);
                }
                return true;
            }
            else
            {
                return false;  
            }          
        }

        /// <summary>
        /// Single int32 data write
        /// </summary>
        /// <param name="startAddress">data address must be even</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool WriteIntData(ushort startAddress, int data)
        {
            //WRS 软元件类型|软元件标号|数据格式 写入个数 数据1 数据2 ... 数据n|CR
            string messageSend = "WR DM" + startAddress.ToString() + ".L " + data.ToString() + "\r";
            byte[] message = Encoding.ASCII.GetBytes(messageSend);
            byte[] response = new byte[4]; //Return bytes always 4 byte
            lock (lockthis)
            {
                try
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            //Check response OK\r\n
            if (ByteEquals(response, new byte[] { 0x4f, 0x4b, 0x0d, 0x0a }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteBoolData(ushort startAddress, bool data)
        {
            return true;
        }

        #region Get Response
        private void GetResponse(ref byte[] response)
        {
            //There is a bug in .Net 2.0 DataReceived Event that prevents people from using this
            //event as an interrupt to handle data (it doesn't fire all of the time).  Therefore
            //we have to use the ReadByte command for a fixed length as it's been shown to be reliable.
            for (int i = 0; i < response.Length; i++)
            {
                response[i] = (byte)(ns.ReadByte());
            }
        }
        #endregion

        /// <summary>
        /// 比较两个字节数组是否相等
        /// </summary>
        /// <param name="b1">byte数组1</param>
        /// <param name="b2">byte数组2</param>
        /// <returns>是否相等</returns>
        private bool ByteEquals(byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length) return false;
            if (b1 == null || b2 == null) return false;
            for (int i = 0; i < b1.Length; i++)
                if (b1[i] != b2[i])
                    return false;
            return true;
        }
    }
}
