using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace WifiMonitor
{
    public class Slave
    {
        public TcpClient client { get; private set; }
        private NetworkStream ns;

        private object lockthis = new object();

        public ushort slaveIndex;
        public IPAddress slaveIP;
        public bool onlineFlag;

        private ICommunicate node;
        private Assembly assembly;
        private Type type;

        public RemoteNode remoteNode;

        public Slave(TcpClient tcpClient)
        {
            client = tcpClient;
            ns = client.GetStream();
            ns.ReadTimeout = 8000;
            ns.WriteTimeout = 8000;
            slaveIndex = ushort.Parse(client.Client.RemoteEndPoint.ToString().Split('.')[3].Split(':')[0]);
            slaveIP = IPAddress.Parse(client.Client.RemoteEndPoint.ToString().Split(':')[0]);            
        }

        public void SetProperty(RemoteNode newNode)
        {
            remoteNode = newNode;
        }

        public void Close()
        {
            ns.Close();
            client.Close();
        }

        public void Dispose()
        {
            Close();
        }

        public bool LoadLibrary(string protocol)
        {
            try
            {
                assembly = Assembly.LoadFile(Environment.CurrentDirectory.ToString() + "\\Library\\" + protocol + ".dll");
                type = assembly.GetType("WifiMonitor." + protocol);
                Object obj = Activator.CreateInstance(type, new object[] { ns, lockthis });
                node = obj as ICommunicate;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get read only data from remote nodes
        /// </summary>
        /// <param name="startAddress">Start address, const 0</param>
        /// <returns>Success or failure</returns>
        public bool ReadData(ushort startAddress)
        {        
            int[] response;
            bool successFlag = false;
            if (remoteNode.varDiscreteInput.Length % 32 != 0)
            {
                response = new int[remoteNode.varDiscreteInput.Length / 32 + 1 + remoteNode.varInputRegister.Length];
            }
            else
            {
                response = new int[remoteNode.varDiscreteInput.Length / 32 + remoteNode.varInputRegister.Length];
            }

            try
            {
                successFlag = node.GetReadOnlyData(startAddress, (ushort)(response.Length * 2), ref response);
            }
            catch (Exception)
            {
                successFlag = false;
            }

            if (!successFlag)
            {
                return successFlag;
            }

            for (int i = 0; i < response.Length; i++)
            {
                //Get bit value
                if (i < response.Length - remoteNode.varInputRegister.Length)
                {
                    int mask = 1;
                    for (int offset = 0; offset < 32; offset++)
                    {
                        mask <<= offset;
                        int tempData = response[i] & mask;
                        tempData >>= offset;
                        if ((offset + i * 32) < remoteNode.varDiscreteInput.Length)
                        {
                            remoteNode.varDiscreteInput[offset + i * 32].boolValue = (tempData == 0x01 ? true : false);
                        }
                    }
                }

                //Get integer value
                else
                {
                    remoteNode.varInputRegister[i - (response.Length - remoteNode.varInputRegister.Length)].intValue = response[i];
                } 
            }
            return successFlag;
        }

        /// <summary>
        /// Get read write data from remote nodes
        /// </summary>
        /// <param name="startAddress">Start address, const 0</param>
        /// <returns>Success or failure</returns>
        public bool GetReadWriteData(ushort startAddress)
        {
            int[] response;
            bool successFlag = false;
            if (remoteNode.varDiscreteOutput.Length % 32 != 0)
            {
                response = new int[remoteNode.varDiscreteOutput.Length / 32 + 1 + remoteNode.varHoldingRegister.Length];
            }
            else
            {
                response = new int[remoteNode.varDiscreteOutput.Length / 32 + remoteNode.varHoldingRegister.Length];
            }

            try
            {
                successFlag = node.GetReadWriteData(startAddress, (ushort)(response.Length * 2), ref response);
            }
            catch (Exception)
            {
                successFlag = false;
            }

            if (!successFlag)
            {
                return successFlag;
            }

            for (int i = 0; i < response.Length; i++)
            {
                //Get bit value
                if (i < response.Length - remoteNode.varHoldingRegister.Length)
                {
                    int mask = 1;
                    for (int offset = 0; offset < 32; offset++)
                    {
                        mask <<= offset;
                        int tempData = response[i] & mask;
                        tempData >>= offset;
                        if ((offset + i * 32) < remoteNode.varDiscreteOutput.Length)
                        {
                            remoteNode.varDiscreteOutput[offset + i * 32].boolValue = (tempData == 0x01 ? true : false);
                        }
                    }
                }

                //Get integer value
                else
                {
                    remoteNode.varHoldingRegister[i - (response.Length - remoteNode.varHoldingRegister.Length)].intValue = response[i];
                }
            }
            return successFlag;
        }

        public bool WriteData(ushort address, int dataValue)
        {
            bool successFlag = false;
            ushort offset = 0;
            if (remoteNode.varDiscreteOutput.Length % 32 != 0)
            {
                offset = (ushort)(remoteNode.varDiscreteOutput.Length / 32 + 1);
            }
            else
            {
                offset = (ushort)(remoteNode.varDiscreteOutput.Length / 32);
            }
            try
            {
                successFlag = node.WriteIntData((ushort)(address + offset * 2), dataValue);
            }
            catch (Exception)
            {
                successFlag = false;
            }
            return successFlag;
        }

        public bool WriteData(ushort address, bool dataValue)
        {
            bool successFlag = false;
            try
            {
                successFlag = node.WriteBoolData(address, dataValue);
            }
            catch (Exception)
            {
                successFlag = false;
            }
            return true;
        }


        ///<summary> 
        /// 序列化 
        /// </summary> 
        /// <param name="data">要序列化的对象</param> 
        /// <returns>返回存放序列化后的数据缓冲区</returns> 
        private static byte[] Serialize(object data)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            MemoryStream rems = new MemoryStream();
            formatter.Serialize(rems, data);
            return rems.GetBuffer();
        }

        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="data">数据缓冲区</param> 
        /// <returns>对象</returns> 
        private static object Deserialize(byte[] data)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            MemoryStream rems = new MemoryStream(data);
            data = null;
            return formatter.Deserialize(rems);
        } 
    }
}
