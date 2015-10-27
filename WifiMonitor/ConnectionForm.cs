using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WifiMonitor
{
    public delegate void ConnectionEventHandler(object sender, EventArgs e); //连接信息改变
    public partial class ConnectionForm : Form
    {
        private MainForm mMainForm;

        public ConnectionForm()
        {
            InitializeComponent();
        }
        public ConnectionForm(MainForm parent)
        {
            InitializeComponent();
            InitialDataGridView();
            mMainForm = parent as MainForm;
            mMainForm.communicate.connectionChange += new ConnectionEventHandler(updateList);
        }

        public void InitialDataGridView()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            DataGridViewCell textCell = new DataGridViewTextBoxCell();
            DataGridViewColumn timeColumn = new DataGridViewColumn();
            timeColumn.HeaderText = "记录时间";
            timeColumn.Width = 160;
            timeColumn.CellTemplate = textCell;
            DataGridViewColumn ipColumn = new DataGridViewColumn();
            ipColumn.HeaderText = "IP地址";
            ipColumn.Width = 120;
            ipColumn.CellTemplate = textCell;
            DataGridViewColumn portColumn = new DataGridViewColumn();
            portColumn.HeaderText = "端口号";
            portColumn.Width = 80;
            portColumn.CellTemplate = textCell;
            DataGridViewColumn statusColumn = new DataGridViewColumn();
            statusColumn.HeaderText = "工作状态";
            statusColumn.Width = 180;
            statusColumn.CellTemplate = textCell;
            dataGridView.Columns.Add(timeColumn);
            dataGridView.Columns.Add(ipColumn);
            dataGridView.Columns.Add(portColumn);
            dataGridView.Columns.Add(statusColumn);
        }

        public delegate void UpdateDataGridView(object sender, EventArgs e);
        public void updateList(object sender, EventArgs e)
        {
            Communicate communicate = sender as Communicate;
            int index = 0;
            if (this.InvokeRequired)
            {
                UpdateDataGridView delegateMethod = new UpdateDataGridView(updateList);
                this.Invoke(delegateMethod, new object[] { sender, e });
            }
            else
            {
                InitialDataGridView();

                if (communicate.moduleList.Count != 0)
                {
                    try
                    {
                        foreach (var slave in communicate.moduleList)
                        {
                            index = dataGridView.Rows.Add();
                            dataGridView.Rows[index].Cells[0].Value = System.DateTime.Now;
                            dataGridView.Rows[index].Cells[1].Value = slave.client.Client.RemoteEndPoint.ToString().Split(':')[0];
                            dataGridView.Rows[index].Cells[2].Value = slave.client.Client.RemoteEndPoint.ToString().Split(':')[1];
                            dataGridView.Rows[index].Cells[3].Value = slave.modbusStatus;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }                    
                }
            }
               
        }

        //Not close on closing
        private void ConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
