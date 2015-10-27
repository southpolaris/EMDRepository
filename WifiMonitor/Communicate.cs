using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WifiMonitor
{
    class Communicate
    {
        protected TcpListener tcpListener = null;
        protected TcpClient tcpClient = null;
        protected NetworkStream ns = null;
        public MainForm.UpdateStatus updateStatus;
        public event ConnectionEventHandler connectionChange;

        public List<ModbusSlave> moduleList;//moudle client list
        public bool fWaiting = true;//running flag

        public Communicate()
        {
            moduleList = new List<ModbusSlave>();
        }

        //连接状态改变时刷新界面显示
        public void OnConnectionChange()
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
                listenThread.Start();
                updateStatus("服务器启动，等待连接...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListenClinetConnect()
        {
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
                Thread communicateThread = new Thread(GetModbusData);                              
                communicateThread.Start(module);
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
            while (fWaiting)
            {
                ModbusSlave module = (ModbusSlave)obj;
                TcpClient client = module.client;
                try
                {
                    module._mutex.WaitOne();
                    successFlag = module.SendFc1((byte)1, (ushort)0, (ushort)0x0a);
                    Thread.Sleep(100);
                    successFlag = module.SendFc4((byte)1, (ushort)0, (ushort)0x0a);
                    Thread.Sleep(100); //3.5 times interval to start next modbus message (4.00910405ms for baud rate 9600)
                    successFlag = module.SendFc3((byte)1, (ushort)0, (ushort)0x0a);
                    module._mutex.ReleaseMutex();
                    Thread.Sleep(300);
                    if (string.Compare(module.modbusStatus, "Connection lost") == 0)
                    {
                        RemoveModule(module);
                        break;
                    }                    
                }
                catch (Exception ex)
                {
                    if (fWaiting)
                    {
                        RemoveModule(module);
                        break;
                    }
                    throw ex;
                }                
            }
        }

        public void SendModbusData(int slaveIndex, ushort registerAddress, ushort dataValue)
        {
            foreach (ModbusSlave module in moduleList)
            {
                if (module.slaveIndex == slaveIndex)
                {
                    bool successFlag = false;
                    short maxWiteTimes = 10; //超过10次未成功则写入失败
                    while (!successFlag && maxWiteTimes > 0)
                    {
                        module._mutex.WaitOne();
                        successFlag = module.SendFc6((byte)1, registerAddress, dataValue);
                        module._mutex.ReleaseMutex();
                        maxWiteTimes--;
                    }
                    if (!successFlag) //10次写入未成功，断开连接
                    {
                        RemoveModule(module);
                        return;
                    }
                }
            }                       
        }


        public void RemoveModule(ModbusSlave module)
        {
            moduleList.Remove(module);
            module.Close();
            
            OnConnectionChange();
        }

        public void StopCommunication()
        {
            fWaiting = false;
            for (int i = moduleList.Count - 1; i >= 0; i--)
            {
                RemoveModule(moduleList[i]);
            }
            tcpListener.Stop();
            updateStatus("已停止");
        }
    }
}
