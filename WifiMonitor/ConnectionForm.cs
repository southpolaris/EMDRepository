using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace WifiMonitor
{
    public partial class ConnectionForm : Form
    {
        private MainForm mMainForm;
        private DataSet ds;
        private DataTable dt;

        public ConnectionForm()
        {
            InitializeComponent();
        }
        public ConnectionForm(MainForm parent)
        {
            InitializeComponent();
            InitialDataGridView();
            mMainForm = parent as MainForm;
            mMainForm.communicate.connectionChange += new ConnectionEventHandler(UpdateNodeTable);
            this.TopMost = true;
        }

        public void InitialDataGridView()
        {
            ds = new DataSet();
            dt = new DataTable("NodeList");
            DataColumn[] keys = new DataColumn[1];
            dt.Columns.Add(new DataColumn("终端名称", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("IP地址", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("当前状态", Type.GetType("System.String")));
            keys[0] = dt.Columns["IP地址"];
            dt.PrimaryKey = keys;
            ds.Tables.Add(dt);

            DataRow dr;
            foreach (var ipnodePair in GlobalVar.ipNodeMapping)
            {
                dr = dt.NewRow();
                dr[0] = ipnodePair.Value.name;
                dr[1] = ipnodePair.Key.ToString();
                dr[2] = "离线";
                dt.Rows.Add(dr);
            }

            dataGridView.CellClick += dataGridView_CellClick;
            dataGridView.DataSource = dt.DefaultView;
        }

        //Select tab when clicked
        void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string selectAddress = dataGridView.CurrentRow.Cells[1].Value.ToString();
            foreach (TabPage page in mMainForm.tabControl.TabPages)
            {
                if (string.Equals(selectAddress, page.ToolTipText.ToString()))
                {
                    mMainForm.tabControl.SelectedTab = page;
                    break;
                }
            }
        }

        public delegate void UpdateDataGridView(object sender, EventArgs e);
        public void UpdateNodeTable(object sender, EventArgs e)
        {
            Slave slave = sender as Slave;
            if (this.InvokeRequired)
            {
                UpdateDataGridView delegateMethod = new UpdateDataGridView(UpdateNodeTable);
                this.Invoke(delegateMethod, new object[] { sender, e });
            }
            else
            {
                DataRow[] selectedRows = dt.Select(string.Format("IP地址 = '{0}'", slave.slaveIP));
                int rowIndex = dt.Rows.IndexOf(selectedRows[0]);
                if (mMainForm.communicate.slaveList.Contains(slave))
                {                    
                    if (slave.fActive)
                    {
                        dt.Rows[rowIndex][2] = "通信正常";
                    }
                    else
                    {
                        dt.Rows[rowIndex][2] = "通信异常";
                    } 
                    return;
                }
                else
                {
                    dt.Rows[rowIndex][2] = "离线";
                }    
            }                              
        }

        //Not close on closing
        private void ConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = dt.DefaultView;
            this.Text = "所有设备 - 监控设备列表";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            DataView view = new DataView(dt);

            view.RowFilter = "当前状态 <> '离线'";
            view.RowStateFilter = DataViewRowState.CurrentRows;
            view.Sort = "当前状态";

            dataGridView.DataSource = view;
            this.Text = "已连接设备 - 监控设备列表";
        }

        private void buttonOffLine_Click(object sender, EventArgs e)
        {
            DataView view = new DataView(dt);

            view.RowFilter = "当前状态 = '离线'";
            view.RowStateFilter = DataViewRowState.CurrentRows;
            view.Sort = "当前状态";

            dataGridView.DataSource = view;
            this.Text = "离线设备 - 监控设备列表";
        }
    }
}
