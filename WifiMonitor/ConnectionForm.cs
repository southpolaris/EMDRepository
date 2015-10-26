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
            mMainForm = parent as MainForm;
            mMainForm.communicate.connectionChange += new ConnectionEventHandler(updateList);
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
                dataGridView1.Rows.Clear();
                if (communicate.moduleList.Count != 0)
                {
                    try
                    {
                        foreach (var slave in communicate.moduleList)
                        {
                            index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = System.DateTime.Now;
                            dataGridView1.Rows[index].Cells[1].Value = slave.client.Client.RemoteEndPoint.ToString().Split(':')[0];
                            dataGridView1.Rows[index].Cells[2].Value = slave.client.Client.RemoteEndPoint.ToString().Split(':')[1];
                            dataGridView1.Rows[index].Cells[3].Value = slave.modbusStatus;
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
