using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WifiMonitor
{
    public partial class SettingForm : Form
    {
        MainForm mMainForm;

        public SettingForm()
        {
            InitializeComponent();
        }

        public SettingForm(MainForm parent)
        {
            mMainForm = parent;
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            textBoxUsername.Text = GlobalVar.userID;
            ipBoxHost.Text = GlobalVar.dataSource;
            numPort.Value = GlobalVar.portNumber;
            textBoxSchema.Text = GlobalVar.schemaName;

            numCycleTime.Value = GlobalVar.cycleTime;
            textBoxSysID.Text = GlobalVar.subSysID.ToString();
        }

        private void buttonTestConnect_Click(object sender, EventArgs e)
        {
            string connectionString = string.Format("Database='{0}';Data Source='{1}';User Id='{2}';Password='{3}';charset='utf8';pooling=true",
            textBoxSchema.Text, ipBoxHost.Text, textBoxUsername.Text, textBoxPassword.Text);
            if (MySQLHelper.TestCommunicate(connectionString))
            {
                labelTestResult.Text = "成功连接数据库";
                labelTestResult.ForeColor = Color.DarkGreen;
            }
            else
            {
                labelTestResult.Text = "连接失败!";
                labelTestResult.ForeColor = Color.DarkRed;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //Save parameter
            buttonApply.PerformClick();
            this.Close();
        }

        //sava parameter
        private void buttonApply_Click(object sender, EventArgs e)
        {
            XDocument xDoc = XDocument.Load(GlobalVar.xmlPath);
            XElement settingElement = xDoc.Root.Element("Settings");

            //清除原有信息
            settingElement.RemoveAll();

            //保存设置信息
            settingElement.Add(new XElement("DataSource", ipBoxHost.Text));
            settingElement.Add(new XElement("PortNum", numPort.Value));
            settingElement.Add(new XElement("UserID", textBoxUsername.Text));
            settingElement.Add(new XElement("UserPwd", textBoxPassword.Text));

            settingElement.Add(new XElement("Cycletime", numCycleTime.Value));
            settingElement.Add(new XElement("SubSystemID", textBoxSysID.Text));

            xDoc.Save(GlobalVar.xmlPath);
        }

        private void buttonInitial_Click(object sender, EventArgs e)
        {
            mMainForm.DataBaseInitial();
        }
    }
}
