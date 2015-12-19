using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.IO;

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

        private object obj = null;
        private Assembly assembly;
        private Type type;
        private MethodInfo getSlaveData;
        private MethodInfo sendIntData;
        private MethodInfo sendBoolData;

        private bool[] dataDiscreteOutput;
        public bool[] DataDiscreteOutput         //开关量读写
        {
            set { dataDiscreteOutput = value; }
            get { return dataDiscreteOutput; }
        }

        private bool[] dataDiscreateInput;
        public bool[] DataDiscreteInput          //开关量只读
        {
            set { dataDiscreateInput = value; }
            get { return dataDiscreateInput; }
        }

        private int[] dataReadWrite;
        public int[] DataReadWrite              //有符号32位二进制
        {
            set { dataReadWrite = value; }
            get { return dataReadWrite; }
        }

        private int[] dataReadOnly;
        internal int[] DataReadOnly            //有符号32位二进制
        {
            set { dataReadOnly = value; }
            get { return dataReadOnly; }
        }

        public Slave(TcpClient tcpClient)
        {
            client = tcpClient;
            ns = client.GetStream();
            ns.ReadTimeout = 8000;
            ns.WriteTimeout = 8000;
            slaveIndex = ushort.Parse(client.Client.RemoteEndPoint.ToString().Split('.')[3].Split(':')[0]);
            slaveIP = IPAddress.Parse(client.Client.RemoteEndPoint.ToString().Split(':')[0]);
        }

        public void SetDataLength(DataCount dataLength)
        {
            dataDiscreateInput = new bool[dataLength.discreteInput];
            dataDiscreteOutput = new bool[dataLength.coil];
            dataReadOnly = new int[dataLength.inputRegister];
            dataReadWrite = new int[dataLength.holdingRegiter];
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
                obj = Activator.CreateInstance(type, new object[] { ns, lockthis });
                getSlaveData = type.GetMethod("ReadData");
                sendBoolData = type.GetMethod("WriteBoolData");
                sendIntData = type.GetMethod("WriteIntData");
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool ReadData(ushort startAddress)
        {        
            int[] response;
            bool successFlag = false;
            if (dataDiscreateInput.Length % 32 != 0)
            {
                response = new int[dataDiscreteOutput.Length / 32 + 1 + dataReadWrite.Length];
            }
            else
            {
                response = new int[dataDiscreteOutput.Length / 32 + dataReadWrite.Length];
            }
            object[] parameters = new object[] { startAddress, (ushort)(response.Length * 2), response };
            try
            {
                successFlag = (bool)(getSlaveData.Invoke(obj, parameters));
            }
            catch (Exception)
            {
                successFlag = false;
            }

            if (!successFlag)
            {
                return successFlag;
            }

            response = parameters[2] as int[];
            for (int i = 0; i < response.Length; i++)
            {
                //Get bit value
                if (i < response.Length - dataReadWrite.Length)
                {
                    int mask = 1;
                    for (int offset = 0; offset < 32; offset++)
                    {
                        mask <<= offset;
                        int tempData = response[i] & mask;
                        tempData >>= offset;
                        if ((offset + i * 32) < dataDiscreteOutput.Length)
                        {
                            dataDiscreteOutput[offset + i * 32] = (tempData == 0x01 ? true : false);
                        }
                    }
                }

                //Get integer value
                else
                {
                    dataReadWrite[i - (response.Length - dataReadWrite.Length)] = response[i];
                } 
            }
            return successFlag;
        }

        public bool WriteData(ushort address, int dataValue)
        {
            bool successFlag = false;
            try
            {
               successFlag = (bool)(sendIntData.Invoke(obj, new object[] { address, dataValue }));
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
                successFlag = (bool)(sendBoolData.Invoke(obj, new object[] { address, dataValue }));
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
