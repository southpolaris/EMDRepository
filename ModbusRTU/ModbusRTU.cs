using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Sockets;
using System.Threading;

namespace WifiMonitor
{
    public class ModbusRTU : ICommunicate
    {
        private NetworkStream ns { get; set; }
        private object lockthis { get; set; }

        private string modbusStatus;

        //一般指令地址（0-6535）和设备地址（1-65536）存在差异
        public ModbusRTU(NetworkStream stream, object lockObj)
        {
            ns = stream;
            lockthis = lockObj;
        }

        #region Bulid Message
        /// <summary>
        /// Build modbus message
        /// </summary>
        /// <param name="address">slave address</param>
        /// <param name="type">function code</param>
        /// <param name="start">start address</param>
        /// <param name="registers">number of registers</param>
        /// <param name="message">completement message</param>
        private void BuildMessage(byte address, byte type, ushort start, ushort registers, ref byte[] message)
        {
            //Array to receive CRC bytes:
            byte[] CRC = new byte[2];

            message[0] = address;
            message[1] = type;
            message[2] = (byte)(start >> 8); //Hi first
            message[3] = (byte)start; //Lo second
            message[4] = (byte)(registers >> 8);
            message[5] = (byte)registers;

            GetCRC(message, ref CRC);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];
        }
        #endregion

        #region CRC Computation
        private void GetCRC(byte[] message, ref byte[] CRC)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }
        #endregion

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

        #region Check Response
        private bool CheckResponse(byte[] response)
        {
            //Perform a basic CRC check:
            byte[] CRC = new byte[2];
            GetCRC(response, ref CRC);
            if (CRC[0] == response[response.Length - 2] && CRC[1] == response[response.Length - 1])
                return true;
            else
                return false;
        }
        #endregion

        #region interface impelement
        /// <summary>
        /// Read 32 bits data from Modbus slave
        /// </summary>
        /// <param name="start">start address</param>
        /// <param name="registers">32 bits data length * 2</param>
        /// <returns></returns>
        public bool ReadData(ushort start, ushort registers, ref int[] dataReadWrite)
        {
            ushort length = registers;
            int index = 0;
            while (length > 120) //每帧长度最大256字节，每次查询120个寄存器
            {
                //Function 3 request is always 8 bits
                byte[] tempMessage = new byte[8];
                //Function 3 response buffer
                byte[] tempResponse = new byte[5 + 2 * 120];
                //Buile outgoing modbus message
                BuildMessage((byte)1, (byte)3, start, 120, ref tempMessage);
                //Send modbus message to Slaves
                try
                {
                    lock (lockthis)
                    {
                        ns.Write(tempMessage, 0, tempMessage.Length);
                        GetResponse(ref tempResponse);
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                //Evaluate message:
                if (CheckResponse(tempResponse))
                {
                    //Return request register values:
                    for (int tempIndex = 0; tempIndex < (tempResponse.Length - 5) / 4; tempIndex++, index++)
                    {
                        dataReadWrite[index] = tempResponse[4 * tempIndex + 5];
                        dataReadWrite[index] <<= 8;
                        dataReadWrite[index] += tempResponse[4 * tempIndex + 6];
                        dataReadWrite[index] <<= 8;
                        dataReadWrite[index] += tempResponse[4 * tempIndex + 3];
                        dataReadWrite[index] <<= 8;
                        dataReadWrite[index] += tempResponse[4 * tempIndex + 4];
                    }
                    length = (ushort)(length - 120);
                    Thread.Sleep(60);
                }
                else
                {
                    return false;
                }
            }
            //Function 3 request is always 8 bits
            byte[] message = new byte[8];
            //Function 3 response buffer
            byte[] response = new byte[5 + 2 * length];
            //Buile outgoing modbus message
            BuildMessage((byte)1, (byte)3, start, length, ref message);
            //Send modbus message to Slaves
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception)
            {
                return false;
            }
            //Evaluate message:
            if (CheckResponse(response))
            {
                //Return request register values:
                for (int tempIndex = 0; tempIndex < (response.Length - 5) / 4; tempIndex++, index++)
                {
                    dataReadWrite[index] = response[4 * tempIndex + 5];
                    dataReadWrite[index] <<= 8;
                    dataReadWrite[index] += response[4 * tempIndex + 6];
                    dataReadWrite[index] <<= 8;
                    dataReadWrite[index] += response[4 * tempIndex + 3];
                    dataReadWrite[index] <<= 8;
                    dataReadWrite[index] += response[4 * tempIndex + 4];
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Write single 32 bits value to Modbus slave
        /// </summary>
        /// <param name="address">register address (data address * 2)</param>
        /// <param name="data">pending write data</param>
        /// <returns></returns>
        public bool WriteIntData(ushort registerAddress, int data)
        {
            bool successFlag = false;
            byte[] tempByte = BitConverter.GetBytes(data);
            short[] tempValue = new short[2];
            tempValue[0] = (short)((tempByte[1] << 8) + tempByte[0]);
            tempValue[1] = (short)((tempByte[3] << 8) + tempByte[2]);
            successFlag = SendFc16((byte)1, registerAddress, (ushort)2, tempValue);
            return successFlag;
        }

        public bool WriteBoolData(ushort address, bool data)
        {
            bool successFlag = false;
            //读取数据，目前只使用4区
            //Read 32 bits long data pending write
            ushort offset = (ushort)(address % 32);
            ushort startAddress = 0;
            startAddress = (ushort)(address / 32);
 
            int[] originalValue = new int[1];
            if (!SendFc3(1, startAddress, 2, ref originalValue))
            {
                return false;
            }

            int pendingWrite = originalValue[0];
            if (data)
            {
                pendingWrite = pendingWrite | 0x01 << offset;
            }
            else
            {
                int mask = 0x01 << offset;
                mask = ~mask;
                pendingWrite = pendingWrite & mask;
            }

            Thread.Sleep(200);

            //Write data
            successFlag = WriteIntData(startAddress, pendingWrite);

            return successFlag;
        }
        #endregion

        #region Function 3 - Read Holding Registers
        /// <summary>
        /// Read 16 bit unsigned binary values
        /// </summary>
        /// <param name="address">Slave address</param>
        /// <param name="start">Start</param>
        /// <param name="registers">Number of registers</param>
        /// <returns>True is success</returns>
        public bool SendFc3(byte address, ushort start, ushort registers, ref ushort[] dataValue)
        {
            //Function 3 request is always 8 bytes:
            byte[] message = new byte[8];
            //Function 3 response buffer:
            byte[] response = new byte[5 + 2 * registers];
            //Build outgoing modbus message:
            BuildMessage(address, (byte)3, start, registers, ref message);
            //Send modbus message to Serial Port:
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception err)
            {
                modbusStatus = "Error in read event: " + err.Message;
                return false;
            }
            //Evaluate message:
            if (CheckResponse(response))
            {
                //Return requested register values:
                for (int i = 0; i < (response.Length - 5) / 2; i++)
                {
                    dataValue[i] = response[2 * i + 3];
                    dataValue[i] <<= 8;
                    dataValue[i] += response[2 * i + 4];
                }
                modbusStatus = "Read successful";
                return true;
            }
            else
            {
                modbusStatus = "CRC error";
                return false;
            }
        }

        /// <summary>
        /// Read 32 bits signed 
        /// </summary>
        /// <param name="address">Slave address</param>
        /// <param name="start">Start</param>
        /// <param name="registers">Number of registers</param>
        /// <returns>True means success</returns>
        public bool SendFc3(byte address, ushort start, ushort registers, ref int[] dataValue)
        {
            //Function 3 request is always 8 bits
            byte[] message = new byte[8];
            //Function 3 response buffer
            byte[] response = new byte[5 + 2 * registers];
            //Buile outgoing modbus message
            BuildMessage(address, (byte)3, start, registers, ref message);
            //Send modbus message to Slaves
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception err)
            {
                modbusStatus = "Error in read event: " + err.Message;
                return false;
            }
            //Evaluate message:
            if (CheckResponse(response))
            {
                //Return request register values:
                for (int i = 0; i < (response.Length - 5) / 4; i++)
                {
                    dataValue[i] = response[4 * i + 5];
                    dataValue[i] <<= 8;
                    dataValue[i] += response[4 * i + 6];
                    dataValue[i] <<= 8;
                    dataValue[i] += response[4 * i + 3];
                    dataValue[i] <<= 8;
                    dataValue[i] += response[4 * i + 4];
                }
                modbusStatus = "Read successful";
                return true;
            }
            else
            {
                modbusStatus = "CRC error";
                return false;
            }  
        }

        public bool SendFc3(byte address, ushort start, ushort registers, ref float[] dataValue)
        {
            //Function 3 request is always 8 bits
            byte[] message = new byte[8];
            //Function 3 response buffer
            byte[] response = new byte[5 + 2 * registers];
            //Buile outgoing modbus message
            BuildMessage(address, (byte)3, start, registers, ref message);
            //Send modbus message to Slaves
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception err)
            {
                modbusStatus = "Error in read event: " + err.Message;
                return false;
            }
            //Evaluate message:
            if (CheckResponse(response))
            {
                //Return request register values:
                byte[] tempByte = new byte[4];
                for (int i = 0; i < (response.Length - 5) / 4; i++)
                {
                    tempByte[0] = response[4 * i + 4];
                    tempByte[1] = response[4 * i + 3];
                    tempByte[2] = response[4 * i + 6];
                    tempByte[3] = response[4 * i + 5];
                    dataValue[i] = BitConverter.ToSingle(tempByte, 0);  
                }
                modbusStatus = "Read successful";
                return true;
            }
            else
            {
                modbusStatus = "CRC error";
                return false;
            }
        }
        #endregion

        #region Function 4 - Read Input Registers
        public bool SendFc4(byte address, ushort start, ushort registers, ref ushort[] dataValue)
        {
            //Function 4 request is always 8 bits
            byte[] message = new byte[8];
            //Function 4 response buffer
            byte[] response = new byte[5 + 2 * registers];
            //Build outgoing modbus message
            BuildMessage(address, (byte)4, start, registers, ref message);
            //Send modbus message to serial port through tcp
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception err)
            {
                modbusStatus = "Error in read event: " + err.Message;
                return false;
            }
            //Evaluate message
            if (CheckResponse(response))
            {
                //Return requested register values:
                for (int i = 0; i < (response.Length - 5) / 2; i++)
                {
                    dataValue[i] = response[2 * i + 3];
                    dataValue[i] <<= 8;
                    dataValue[i] += response[2 * i + 4];
                }
                modbusStatus = "Read successful";
                return true;
            }
            else
            {
                modbusStatus = "CRC error";
                return false;
            }
        }

        public bool SendFc4(byte address, ushort start, ushort registers, ref int[] dataValue)
        {
            //Function 3 request is always 8 bits
            byte[] message = new byte[8];
            //Function 3 response buffer
            byte[] response = new byte[5 + 2 * registers];
            //Buile outgoing modbus message
            BuildMessage(address, (byte)4, start, registers, ref message);
            //Send modbus message to Slaves
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception err)
            {
                modbusStatus = "Error in read event: " + err.Message;
                return false;
            }
            //Evaluate message:
            if (CheckResponse(response))
            {
                //Return request register values:(sort: 3412)
                for (int i = 0; i < (response.Length - 5) / 4; i++)
                {
                    dataValue[i] = response[4 * i + 5];
                    dataValue[i] <<= 8;
                    dataValue[i] += response[4 * i + 6];
                    dataValue[i] <<= 8;
                    dataValue[i] += response[4 * i + 3];
                    dataValue[i] <<= 8;
                    dataValue[i] += response[4 * i + 4];
                }
                modbusStatus = "Read successful";
                return true;
            }
            else
            {
                modbusStatus = "CRC error";
                return false;
            }
        }


        public bool SendFc4(byte address, ushort start, ushort registers, ref float[] dataValue)
        {
            //Function 3 request is always 8 bits
            byte[] message = new byte[8];
            //Function 3 response buffer
            byte[] response = new byte[5 + 2 * registers];
            //Buile outgoing modbus message
            BuildMessage(address, (byte)4, start, registers, ref message);
            //Send modbus message to Slaves
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception err)
            {
                modbusStatus = "Error in read event: " + err.Message;
                return false;
            }
            //Evaluate message:
            if (CheckResponse(response))
            {
                //Return request register values:
                byte[] tempByte = new byte[4];
                for (int i = 0; i < (response.Length - 5) / 4; i++)
                {
                    tempByte[0] = response[4 * i + 4];
                    tempByte[1] = response[4 * i + 3];
                    tempByte[2] = response[4 * i + 6];
                    tempByte[3] = response[4 * i + 5];
                    dataValue[i] = BitConverter.ToSingle(tempByte, 0);
                }
                modbusStatus = "Read successful";
                return true;
            }
            else
            {
                modbusStatus = "CRC error";
                return false;
            }
        }
        #endregion

        #region Function 5 - Write Single Coil
        public bool SendFc5(byte address, ushort coilAddress, bool dataValue)
        {
            //Function 5 request is always 8 bytes
            byte[] message = new byte[8];
            //Fucntion 5 response is always 8 bytes
            byte[] response = new byte[8];
            ushort writeData = (ushort)(dataValue ? 0xff00 : 0x0000);
            //Build outgoing message
            BuildMessage(address, (byte)5, coilAddress, writeData, ref message);

            //Send modbus frame
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception)
            {
                return false;
            }
            if (CheckResponse(response))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Function 6 - Write Single Register
        public bool SendFc6(byte address, ushort registerAddress, ushort dataValue)
        {
            //Ensure port is open
            //Function 6 request is always 8 bytes:
            byte[] message = new byte[8];
            //Function 6 response is fixed at 8 bytes, same as request
            byte[] response = new byte[8];

            //Build out going message
            BuildMessage(address, (byte)6, registerAddress, dataValue, ref message);

            //Send Modbus frame through TCP
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception)
            {
                return false;
            }
            //Evaluate response
            if (CheckResponse(response))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Function 16 - Write Multiple Registers
        public bool SendFc16(byte address, ushort start, ushort registers, short[] dataValues)
        {
            //Message is 1 addr + 1 fcn + 2 start + 2 reg + 1 count + 2 * reg vals + 2 CRC
            byte[] message = new byte[9 + 2 * registers];
            //Function 16 response is fixed at 8 bytes
            byte[] response = new byte[8];

            //Add byte count to message:
            message[6] = (byte)(registers * 2);
            //Put write values into message prior to sending:
            for (int i = 0; i < registers; i++)
            {
                message[7 + 2 * i] = (byte)(dataValues[i] >> 8);
                message[8 + 2 * i] = (byte)(dataValues[i]);
            }
            //Build outgoing message:
            BuildMessage(address, (byte)16, start, registers, ref message);

            //Send Modbus message to Serial Port:
            try
            {
                lock (lockthis)
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
            }
            catch (Exception)
            {
                return false;
            }
            //Evaluate message:
            if (CheckResponse(response))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
