﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using System.Threading.Tasks;


namespace WifiMonitor
{
    public delegate void ConnectionEventHandler(object sender, EventArgs e); //连接信息改变
    public delegate int DataBaseEventHandler(Slave slave);

    class Communicate : IDisposable
    {
        protected TcpListener tcpListener = null;
        protected NetworkStream ns = null;
        public MainForm.UpdateStatus updateStatus;
        public event MainForm.LostConnectionEventHandler lostConnection;
        public event ConnectionEventHandler connectionChange;
        public event DataBaseEventHandler insertDataBase;

        public List<Slave> slaveList;
        public List<Slave> lowPriortyList;
        TaskFactory communicateTask = new TaskFactory();   //Communicate tasks


        public Communicate()
        {
            slaveList = new List<Slave>();
            lowPriortyList = new List<Slave>();
        }

        //连接状态改变时刷新界面显示
        private void OnConnectionChange()
        {
            updateStatus(string.Format("当前 {0} 台设备已经连接", slaveList.Count));
            if (this.connectionChange != null)
            {
                this.connectionChange(this, new EventArgs()); //更改连接列表
            }    
        }

        public void StartServer()
        {
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
            while (GlobalVar.runningFlag)
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
                Slave slave = new Slave(tcpClient);
                slave.SetProperty(GlobalVar.ipNodeMapping[slave.slaveIP]);
                slaveList.Add(slave);
                OnConnectionChange();

                Action<object> readAction;
                readAction = ReadTask;

                Task readTask = communicateTask.StartNew(
                    readAction, slave,
                    TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent);
            }
        }

        private void ReadTask(object obj)
        {
            Slave slave = obj as Slave;
            short maxTimes = 10;    
            RemoteNode node = GlobalVar.ipNodeMapping[slave.slaveIP];
            slave.LoadLibrary(node.protocolType);
            
            //while (!slave.GetReadWriteData((ushort)0) && maxTimes > 0)
            //{
            //    maxTimes--;
            //}

            while (GlobalVar.runningFlag)
            {
                Thread.Sleep(60);
                try
                {
                    if (!slave.ReadData((ushort)0) || !slave.GetReadWriteData((ushort)0))
                    {
                        throw new Exception("Read data error");
                    }
                    if (insertDataBase != null)
                    {
                        insertDataBase(slave);
                    }
                }
                catch (Exception)
                {
                    maxTimes--;
                    if (maxTimes == 0)
                    {
                        RemoveSlave(slave);
                        break;
                    }
                }
            }
        }

        public void SendSingleValue(IPAddress slaveAddress, ushort address, int dataValue)
        {
            foreach (Slave slave in slaveList)
            {
                if (slave.slaveIP.Equals(slaveAddress))
                {
                    bool successFlag = false;
                    short maxWriteTimes = 5;  //超过5次未成功则写入失败
                    communicateTask.StartNew(() =>
                        {
                            while (!successFlag && maxWriteTimes > 0)
                            {
                                successFlag = slave.WriteData((ushort)(address * 2), dataValue);
                                //Thread.Sleep(60);
                                //successFlag = slave.GetReadWriteData((ushort)0);
                                maxWriteTimes--;
                            }
                        });
                }
            }
        }

        public void SendSingleValue(IPAddress slaveAddress, ushort address, bool dataValue)
        {
            foreach (Slave slave in slaveList)
            {
                if (slave.slaveIP.Equals(slaveAddress))
                {
                    bool successFlag = false;
                    short maxWriteTimes = 5;  //超过5次未成功则写入失败
                    communicateTask.StartNew(() =>
                    {
                        while (!successFlag && maxWriteTimes > 0)
                        {
                            successFlag = slave.WriteData(address, dataValue);
                            //Thread.Sleep(60);
                            //successFlag = slave.GetReadWriteData((ushort)0);
                            maxWriteTimes--;
                        }
                    });
                }
            }
        }

        private void RemoveSlave(Slave slave)
        {
            slaveList.Remove(slave);
            if (lostConnection != null)
            {
                this.lostConnection(slave.slaveIndex);
            }
            slave.Dispose();
            OnConnectionChange();
        }

        private void StopCommunication()
        {
            for (int i = slaveList.Count - 1; i >= 0; i--)
            {
                RemoveSlave(slaveList[i]);
            }
            tcpListener.Stop();
            updateStatus("已停止");
        }

        public void Dispose()
        {
            StopCommunication();
            connectionChange = null;
        }

    }
}
