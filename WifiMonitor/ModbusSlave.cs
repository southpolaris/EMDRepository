using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WifiMonitor
{
    /// <summary>
    /// 串口wifi模块(作为client)
    /// </summary>
    class ModbusSlave
    {
        public TcpClient client { get; private set; }
        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }
        public NetworkStream ns;
        public string modbusStatus;
        public ushort slaveIndex;

     /*   AutoResetEvent writeEvent = new AutoResetEvent();*/

        public ushort pendingWrite = 0;   // 写入输出寄存器缓存区（无符号16位）

        public bool[] DataCoil = new bool[10];           // [0区] 输出继电器
        public ushort[] DataReadWrite = new ushort[10];  // [4区] 输出寄存器 读取03 写入06、16
        public ushort[] DataReadOnly = new ushort[10];   // [3区] 输入寄存器 读取04
 
        public ModbusSlave(TcpClient tcpClient)
        {
            client = tcpClient;
            ns = client.GetStream();    
            br = new BinaryReader(ns);
            bw = new BinaryWriter(ns);
            slaveIndex = ushort.Parse(client.Client.RemoteEndPoint.ToString().Split('.')[3].Split(':')[0]);
        }

        public void Close()
        {
            bw.Close();
            br.Close();
            client.Close();
            modbusStatus = "Client closed";
        }

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

        #region Function 6 - Write Single Registers
        public bool SendFc6(byte address, ushort registerAddress, ushort value)
        {
            //Ensure port is open
            if (client.Client.Connected)
            {
                //Function 6 request is always 8 bytes:
                byte[] message = new byte[8];
                //Function 6 response is fixed at 8 bytes, same as request
                byte[] response = new byte[8];

                //Build out going message
                BuildMessage(address, (byte)6, registerAddress, value, ref message);

                //Send Modbus frame through TCP
                try
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in write event: " + err.Message;
                    return false;
                }
                if (CheckResponse(response))
                {
                    modbusStatus = "Write successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Connection lost";
                return false;
            }
        }
        #endregion

        #region Function 16 - Write Multiple Registers
        public bool SendFc16(byte address, ushort start, ushort registers, short[] values)
        {
            //Ensure port is open:  
            if (client.Client.Connected)
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
                    message[7 + 2 * i] = (byte)(values[i] >> 8);
                    message[8 + 2 * i] = (byte)(values[i]);
                }
                //Build outgoing message:
                BuildMessage(address, (byte)16, start, registers, ref message);

                //Send Modbus message to Serial Port:
                try
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in write event: " + err.Message;
                    return false;
                }
                //Evaluate message:
                if (CheckResponse(response))
                {
                    modbusStatus = "Write successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Connection lost";
                return false;
            }
        }
        #endregion

        #region Function 3 - Read Holding Registers
        public bool SendFc3(byte address, ushort start, ushort registers)
        {
            //Ensure port is open:
            if (client.Client.Connected)
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
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
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
                        DataReadWrite[i] = response[2 * i + 3];
                        DataReadWrite[i] <<= 8;
                        DataReadWrite[i] += response[2 * i + 4];
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
            else
            {
                modbusStatus = "Connection lost";
                return false;
            }

        }
        #endregion

        #region Function 4 - Read Input Registers
        public bool SendFc4(byte address, ushort start, ushort registers)
        {
            //Ensure port is open
            if (client.Client.Connected)
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
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
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
                        DataReadOnly[i] = response[2 * i + 3];
                        DataReadOnly[i] <<= 8;
                        DataReadOnly[i] += response[2 * i + 4];
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
            else
            {
                modbusStatus = "Connection lost";
                return false;
            }
        }
        #endregion

        #region Function 1 - Read Coil (Single bit read-write)
        public bool SendFc1(byte address, ushort start, ushort coils)
        {
            //Ensure port is open
            if (client.Client.Connected)
            {
                //Function 1 request is always 8 bits
                byte[] message = new byte[8];
                byte[] response;
                //Function 4 reponse buffer
                if (coils % 8 != 0)
                {
                    response = new byte[5 + coils / 8 + 1];
                }
                else
                {
                    response = new byte[5 + coils / 8];
                }                
                //Build outgoing modbus message
                BuildMessage(address, (byte)1, start, coils, ref message);
                //Send modbus message to slaves
                try
                {
                    ns.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in read event: " + err.Message;
                    return false;
                }
                //Evaluate message
                if (CheckResponse(response))
                {
                    //Return request register values:
                    ushort mask = 0x01;
                    try
                    {
                        for (int byteIndex = 3; byteIndex < response.Length - 2; byteIndex++)
                        {
                            for (int offset = 0; offset < 8; offset++)
                            {
                                mask <<= offset;
                                ushort tempData = (ushort)(response[byteIndex] & mask);
                                tempData >>= offset;
                                if ((offset + (byteIndex - 3) * 8) < coils)
                                {
                                    DataCoil[offset + (byteIndex - 3) * 8] = (tempData == 0x01 ? true : false);
                                }
                            }
                        }

                        modbusStatus = "Read successful";
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Connection lost";
                return false;
            }
        }
        #endregion
    }
}
