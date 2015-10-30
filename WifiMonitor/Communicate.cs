using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
            catch (Exception ex)
            {
                throw ex;
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
                ThreadPool.SetMaxThreads(100, 100);
                ThreadPool.QueueUserWorkItem(new WaitCallback(GetModbusData), module);   
                //Thread communicateThread = new Thread(GetModbusData);
                //communicateThread.Start(module);
            }
        }

        /// <summary>
        /// Sending modbus message to all connected client
        /// </summary>
        /// <param name="obj">
        /// Modbus slave module
        /// </param>
        protected void GetModbusData(object obj)
        {
            bool successFlag = false;
            ModbusSlave module = (ModbusSlave)obj;
            TcpClient client = module.client;
            while (fWaiting)
            {
                try
                {
                    module._mutex.WaitOne();
                    successFlag = module.SendFc1((byte)1, (ushort)0, (ushort)0x10);
                    module._mutex.ReleaseMutex();
                    Thread.Sleep(80); //3.5 times interval to start next modbus message (4.00910405ms for baud rate 9600)
                    module._mutex.WaitOne();
                    successFlag = module.SendFc2((byte)1, (ushort)0, (ushort)0x10);
                    module._mutex.ReleaseMutex();
                    Thread.Sleep(80);

                    module._mutex.WaitOne();
                    successFlag = module.SendFc4((byte)1, (ushort)0x00, (ushort)0x14, ref module.DataReadOnly);
                    module._mutex.ReleaseMutex();
                    Thread.Sleep(80);
                    module._mutex.WaitOne();
                    successFlag = module.SendFc4((byte)1, (ushort)0x28, (ushort)0x14, ref module.DataReadOnlyDF);
                    module._mutex.ReleaseMutex();
                    Thread.Sleep(80);

                    module._mutex.WaitOne();
                    successFlag = module.SendFc3((byte)1, (ushort)0x00, (ushort)0x14, ref module.DataReadWrite);
                    module._mutex.ReleaseMutex();
                    Thread.Sleep(80);
                    module._mutex.WaitOne();
                    successFlag = module.SendFc3((byte)1, (ushort)0x28, (ushort)0x14, ref module.DataReadWriteDF);
                    module._mutex.ReleaseMutex();
                    Thread.Sleep(80);
                    if (refreshDisplay != null)
                    {
                        refreshDisplay(module, new EventArgs());
                    }

                    

                    if (string.Compare(module.modbusStatus, "Connection lost") == 0)
                    {
                        module._mutex.WaitOne();
                        RemoveModule(module);
                        module._mutex.ReleaseMutex();
                        break;
                    }
                    
                }
                catch (Exception ex)
                {
                    if (fWaiting)
                    {
                        module._mutex.WaitOne();
                        RemoveModule(module);
                        module._mutex.ReleaseMutex();
                        throw ex;

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
                    while (!successFlag && maxWiteTimes > 0)
                    {
                        module._mutex.WaitOne();
                        successFlag = module.SendFc6((byte)1, registerAddress, dataValue);
                        module._mutex.ReleaseMutex();
                        maxWiteTimes--;
                    }
                    if (!successFlag) //5次写入未成功，断开连接
                    {
                        module._mutex.WaitOne();
                        RemoveModule(module);
                        module._mutex.ReleaseMutex();
                        return;
                    }
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
                    //4321 to Little endian 3412
                    tempValue[0] = (short)((tempByte[1] << 8) + tempByte[0]);
                    tempValue[1] = (short)((tempByte[3] << 8) + tempByte[2]);
                    
                    while (!successFlag && maxWriteTimes > 0)
                    {
                        module._mutex.WaitOne();
                        successFlag = module.SendFc16((byte)1, registerAddress, (ushort)2, tempValue);
                        module._mutex.ReleaseMutex();
                        maxWriteTimes--;
                    }
                    if (!successFlag) //5次写入未成功，断开连接
                    {
                        RemoveModule(module);
                        return;
                    }
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

                    while (!successFlag && maxWriteTimes > 0)
                    {
                        module._mutex.WaitOne();
                        successFlag = module.SendFc16((byte)1, registerAddress, (ushort)2, tempShort);
                        module._mutex.ReleaseMutex();
                        maxWriteTimes--;
                    }
                    if (!successFlag) //5次写入未成功，断开连接
                    {
                        RemoveModule(module);
                        return;
                    }
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
                    while (!successFlag && maxWiteTimes > 0)
                    {
                        module._mutex.WaitOne();
                        successFlag = module.SendFc5((byte)1, coilAddress, dataValue);
                        module._mutex.ReleaseMutex();
                        maxWiteTimes--;
                    }
                    if (!successFlag) //5次写入未成功，断开连接
                    {
                        RemoveModule(module);
                        return;
                    }
                    return;
                }
            }
        }

        private void RemoveModule(ModbusSlave module)
        {
            moduleList.Remove(module);
            module.Close();
            
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
