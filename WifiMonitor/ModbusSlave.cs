using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WifiMonitor
{
    /// <summary>
    /// Modbus slave class, support function code 01, 02, 02, 04, 05, 06, 16
    /// Use for standard modbus slaves: single register 16bits byte order 12, double register 32bits byte order 3412;
    /// </summary>
    class ModbusSlave
    {
        public TcpClient client { get; private set; }
        public NetworkStream ns;
        public string modbusStatus;
        public ushort slaveIndex;

        public Mutex _mutex;

        // [0区] 输出继电器 读取01 写入05、15
        public bool[] DataCoil = new bool[16];                //16路开关量输出

        // [1区] 输入继电器 读取02
        public bool[] DataDiscreteInput = new bool[16];       //16路开关量输入

        // [4区] 输出寄存器 读取03 写入06、16
        internal int[] DataReadWrite = new int[20];           //有符号32位二进制
        internal float[] DataReadWriteDF = new float[20];     //32位浮点数

        // [3区] 输入寄存器 读取04
        internal int[] DataReadOnly = new int[20];            //有符号32位二进制
        internal float[] DataReadOnlyDF = new float[20];      //32位浮点数
 
        public ModbusSlave(TcpClient tcpClient)
        {
            client = tcpClient;
            ns = client.GetStream();
            ns.ReadTimeout = 10000;
            ns.WriteTimeout = 10000;
            slaveIndex = ushort.Parse(client.Client.RemoteEndPoint.ToString().Split('.')[3].Split(':')[0]);

            _mutex = new Mutex();
        }

        public bool IsOnline(TcpClient tcpClient)
        {
            return !((tcpClient.Client.Poll(10000, SelectMode.SelectRead) && tcpClient.Client.Available == 0) || !tcpClient.Client.Connected);
        }

        public void Close()
        {
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

        #region Function 2 - Read Input Discrete
        public bool SendFc2(byte address, ushort start, ushort coils)
        {
            //Ensure port is open
            if (client.Client.Connected)
            {
                //Function 2 request is always 8 bits
                byte[] message = new byte[8];
                byte[] response;
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
                                    DataDiscreteInput[offset + (byteIndex - 3) * 8] = (tempData == 0x01 ? true : false);
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

        #region Function 3 - Read Holding Registers
        /// <summary>
        /// Read 16 bit unsigned binary values
        /// </summary>
        /// <param name="address">Slave address</param>
        /// <param name="start">Start</param>
        /// <param name="registers">Number of registers</param>
        /// <returns>True is success</returns>
        public bool SendFc3(byte address, ushort start, ushort registers, ref short[] data)
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
                        data[i] = response[2 * i + 3];
                        data[i] <<= 8;
                        data[i] += response[2 * i + 4];
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

        /// <summary>
        /// Read 32 bits signed 
        /// </summary>
        /// <param name="address">Slave address</param>
        /// <param name="start">Start</param>
        /// <param name="registers">Number of registers</param>
        /// <returns>True means success</returns>
        public bool SendFc3(byte address, ushort start, ushort registers, ref int[] data)
        {
            //Ensure the port is open
            if (client.Client.Connected)
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
                    //Return request register values:
                    for (int i = 0; i < (response.Length - 5) / 4; i++)
                    {
                        data[i] = response[4 * i + 5];
                        data[i] <<= 8;
                        data[i] += response[4 * i + 6];
                        data[i] <<= 8;
                        data[i] += response[4 * i + 3];
                        data[i] <<= 8;
                        data[i] += response[4 * i + 4];
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

        public bool SendFc3(byte address, ushort start, ushort registers, ref float[] data)
        {
            //Ensure the port is open
            if (client.Client.Connected)
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
                    //Return request register values:
                    byte[] tempByte = new byte[4];
                    for (int i = 0; i < (response.Length - 5) / 4; i++)
                    {
                        tempByte[0] = response[4 * i + 4];
                        tempByte[1] = response[4 * i + 3];
                        tempByte[2] = response[4 * i + 6];
                        tempByte[3] = response[4 * i + 5];
                        data[i] = BitConverter.ToSingle(tempByte, 0);
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
        public bool SendFc4(byte address, ushort start, ushort registers, ref short[] data)
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
                        data[i] = response[2 * i + 3];
                        data[i] <<= 8;
                        data[i] += response[2 * i + 4];
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

        public bool SendFc4(byte address, ushort start, ushort registers, ref int[] data)
        {
            //Ensure the port is open
            if (client.Client.Connected)
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
                    //Return request register values:(sort: 3412)
                    for (int i = 0; i < (response.Length - 5) / 4; i++)
                    {
                        data[i] = response[4 * i + 5];
                        data[i] <<= 8;
                        data[i] += response[4 * i + 6];
                        data[i] <<= 8;
                        data[i] += response[4 * i + 3];
                        data[i] <<= 8;
                        data[i] += response[4 * i + 4];
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


        public bool SendFc4(byte address, ushort start, ushort registers, ref float[] data)
        {
            //Ensure the port is open
            if (client.Client.Connected)
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
                    //Return request register values:
                    byte[] tempByte = new byte[4];
                    for (int i = 0; i < (response.Length - 5) / 4; i++)
                    {
                        tempByte[0] = response[4 * i + 4];
                        tempByte[1] = response[4 * i + 3];
                        tempByte[2] = response[4 * i + 6];
                        tempByte[3] = response[4 * i + 5];
                        data[i] = BitConverter.ToSingle(tempByte, 0);
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

        #region Function 5 - Write Single Coil
        public bool SendFc5(byte address, ushort coilAddress, bool dataValue)
        {
            if (client.Client.Connected)
            {
                //Function 5 request is always 8 bytes
                byte[] message = new byte[8];
                //Fucntion 5 response is always 8 bytes
                byte[] response = new byte[8];
                ushort writeData = (ushort)(dataValue ? 0xff00 : 0x0000);
                //Build outgoing message
                BuildMessage(address, (byte)6, coilAddress, writeData, ref message);

                //Send modbus frame
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

        #region Function 6 - Write Single Register
        public bool SendFc6(byte address, ushort registerAddress, ushort dataValue)
        {
            //Ensure port is open
            if (client.Client.Connected)
            {
                //Function 6 request is always 8 bytes:
                byte[] message = new byte[8];
                //Function 6 response is fixed at 8 bytes, same as request
                byte[] response = new byte[8];

                //Build out going message
                BuildMessage(address, (byte)6, registerAddress, dataValue, ref message);

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
                //Evaluate response
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
        public bool SendFc16(byte address, ushort start, ushort registers, short[] dataValues)
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
                    message[7 + 2 * i] = (byte)(dataValues[i] >> 8);
                    message[8 + 2 * i] = (byte)(dataValues[i]);
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
    }
}
