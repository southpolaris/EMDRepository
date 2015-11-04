using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using System.Threading.Tasks;


namespace WifiMonitor
{
    public delegate void ConnectionEventHandler(object sender, EventArgs e); //连接信息改变
    public delegate void RefreshUserInterfaceEventHandler(object sender, EventArgs e);
    class Communicate : IDisposable
    {
        protected TcpListener tcpListener = null;
        protected NetworkStream ns = null;
        public MainForm.UpdateStatus updateStatus;
        public event ConnectionEventHandler connectionChange;
        public event RefreshUserInterfaceEventHandler refreshDisplay;

        public List<ModbusSlave> moduleList;   //moudle client list
        TaskFactory communicateTask = new TaskFactory();   //Communicate tasks
        public bool fWaiting = true;            //running flag

        public Communicate()
        {
            moduleList = new List<ModbusSlave>();
        }

        //连接状态改变时刷新界面显示
        private void OnConnectionChange()
        {
            updateStatus(string.Format("当前 {0} 台设备已经连接", moduleList.Count));
            if (this.connectionChange != null)
            {
                this.connectionChange(this, new EventArgs()); //更改连接列表
            }    
        }

        public void StartServer()
        {
            fWaiting = true;
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8899);
                tcpListener.Start();
                Thread listenThread = new Thread(ListenClinetConnect);
                updateStatus("启动检测服务，等待模块连接...");
                listenThread.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ListenClinetConnect()
        {
            TcpClient tcpClient;
            while (fWaiting)
            {
                try
                {
                    tcpClient = tcpListener.AcceptTcpClient();
                }
                catch (Exception)
                {
                    break;
                }

                //For every client, create a new receive data thread
                ModbusSlave module = new ModbusSlave(tcpClient);
                moduleList.Add(module);
                OnConnectionChange();
                Task newCommunication = communicateTask.StartNew(() =>
                    {
                        GetModbusData(module);
                    },
                    TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent);
                //for .Net 3.5 or ealer;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(GetModbusData), module);   
            }
        }

        /// <summary>
        /// Sending modbus message to all connected client
        /// </summary>
        /// <param name="module"> Modbus slave module </param>
        protected void GetModbusData(object obj)
        {
            bool successFlag = false;
            ModbusSlave module = obj as ModbusSlave;
            TcpClient client = module.client;
            while (fWaiting)
            {
                successFlag = false;
                try
                {
                    module._mutex.WaitOne(10000);
                    successFlag = module.SendFc1((byte)1, (ushort)0, (ushort)0x10);
                    Thread.Sleep(80); //3.5 times interval to start next modbus message (4.00910405ms for baud rate 9600)
                    successFlag |= module.SendFc2((byte)1, (ushort)0, (ushort)0x10);
                    Thread.Sleep(80);

                    successFlag |= module.SendFc4DB((byte)1, (ushort)0x00, (ushort)0x14);
                    Thread.Sleep(80);
                    successFlag |= module.SendFc4DF((byte)1, (ushort)0x28, (ushort)0x14);
                    Thread.Sleep(80);

                    successFlag |= module.SendFc3DB((byte)1, (ushort)0x00, (ushort)0x14);
                    Thread.Sleep(80);
                    successFlag |= module.SendFc3DF((byte)1, (ushort)0x28, (ushort)0x14);
                    module._mutex.ReleaseMutex();
                    Thread.Sleep(80);                  

                    if (!successFlag)
                    {
                        throw new Exception("Communicate error");
                    }                    
                }
                catch (Exception ex)
                {
                    if (fWaiting)
                    {
                        module._mutex.WaitOne();
                        RemoveModule(module);
                        module._mutex.ReleaseMutex();
                        break;
                    }
                }                
            }
        }

        

        //Write single register (ushort)
        public void SendModbusData(int slaveIndex, ushort registerAddress, ushort dataValue)
        {
            foreach (ModbusSlave module in moduleList)
            {
                if (module.slaveIndex == slaveIndex)
                {
                    bool successFlag = false;
                    short maxWiteTimes = 5; //超过5次未成功则写入失败

                    communicateTask.StartNew(() =>
                    {
                        while (!successFlag && maxWiteTimes > 0)
                        {
                            module._mutex.WaitOne();
                            successFlag = module.SendFc6((byte)1, registerAddress, dataValue);
                            module._mutex.ReleaseMutex();
                            maxWiteTimes--;
                        }
                    });                        
                    
                    return;
                }
            }                       
        }

        //Write multiple register - 32 bits signed integer
        public void SendModbusData(int slaveIndex, ushort registerAddress, int dataValue)
        {
            foreach (ModbusSlave module in moduleList)
            {
                if (module.slaveIndex == slaveIndex)
                {
                    bool successFlag = false;
                    short maxWriteTimes = 5;
                    short[] tempValue = new short[2];
                    byte[] tempByte = BitConverter.GetBytes(dataValue);
                    //System default 4321 to standard modbus Little endian 3412
                    tempValue[0] = (short)((tempByte[1] << 8) + tempByte[0]);
                    tempValue[1] = (short)((tempByte[3] << 8) + tempByte[2]);

                    Task writeTask = communicateTask.StartNew(() =>
                    {
                        while (!successFlag && maxWriteTimes > 0)
                        {
                            module._mutex.WaitOne();
                            successFlag = module.SendFc16((byte)1, registerAddress, (ushort)2, tempValue);
                            module._mutex.ReleaseMutex();
                            maxWriteTimes--;
                        }
                    });
                    
                    return;
                }
            }
        }

        //Write multiple registers 32-bit floating-point values
        public void SendModbusData(int slaveIndex, ushort registerAddress, float dataValue)
        {
            foreach (ModbusSlave module in moduleList)
            {
                if (module.slaveIndex == slaveIndex)
                {
                    bool successFlag = false;
                    short maxWriteTimes = 5;
                    byte[] tempByte = BitConverter.GetBytes(dataValue);
                    short[] tempShort = new short[2];
                    tempShort[0] = (short)((tempByte[1] << 8) + tempByte[0]);
                    tempShort[1] = (short)((tempByte[3] << 8) + tempByte[2]);

                    Task writeTask = communicateTask.StartNew(() =>
                    {
                        while (!successFlag && maxWriteTimes > 0)
                        {
                            module._mutex.WaitOne();
                            successFlag = module.SendFc16((byte)1, registerAddress, (ushort)2, tempShort);
                            module._mutex.ReleaseMutex();
                            maxWriteTimes--;
                        }
                    });

                    return;
                }
            }
        }

        //Set single coil
        public void SendModbusData(int slaveIndex, ushort coilAddress, bool dataValue)
        {
            foreach (ModbusSlave module in moduleList)
            {
                if (module.slaveIndex == slaveIndex)
                {
                    bool successFlag = false;
                    short maxWiteTimes = 5; //超过5次未成功则写入失败
                    communicateTask.StartNew(() =>
                    {
                        while (!successFlag && maxWiteTimes > 0)
                        {
                            module._mutex.WaitOne();
                            successFlag = module.SendFc5((byte)1, coilAddress, dataValue);
                            module._mutex.ReleaseMutex();
                            maxWiteTimes--;
                        }
                    });
                    
                    return;
                }
            }
        }

        private void RemoveModule(ModbusSlave module)
        {
            moduleList.Remove(module);
            module.Dispose();
            
            OnConnectionChange();
        }

        private void StopCommunication()
        {
            fWaiting = false;
            for (int i = moduleList.Count - 1; i >= 0; i--)
            {
                RemoveModule(moduleList[i]);
            }
            tcpListener.Stop();
            updateStatus("已停止");
        }

        public void Dispose()
        {
            StopCommunication();
            refreshDisplay = null;
            connectionChange = null;
        }
    }
}
