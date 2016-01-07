using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml.Linq;

namespace WifiMonitor
{
    public partial class TabEditForm : Form
    {
        MainForm mMainForm;

        public TabEditForm()
        {
            InitializeComponent();
        }
        public TabEditForm(MainForm parent)
        {
            mMainForm = parent;
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择要加载的监控设备模板";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory.ToString() + "\\Config";
            openFileDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxXml.Text = openFileDialog.FileName;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            IPAddress slaveIP;
            if (IPAddress.TryParse(mMainForm.tabControl.SelectedTab.ToolTipText, out slaveIP))
            {
                GlobalVar.ipNodeMapping.Remove(slaveIP);
            }
            if (checkBoxBlank.Checked)
            {
                mMainForm.tabControl.SelectedTab.Text = textBoxTitle.Text;
                mMainForm.tabControl.SelectedTab.Controls.Clear();
                mMainForm.tabControl.SelectedTab.ToolTipText = "";
                this.Close();
            }
            else
            {
                mMainForm.tabControl.SelectedTab.Text = textBoxTitle.Text;

                try
                {
                     slaveIP = IPAddress.Parse(textBoxIP.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("IP地址无效", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                mMainForm.tabControl.SelectedTab.Controls.Clear();
                mMainForm.tabControl.SelectedTab.ToolTipText = textBoxIP.Text;

                try
                {
                    //Read from xml config file
                    mMainForm.LoadTemplate(textBoxXml.Text, slaveIP);
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件格式或路径无效", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Close();
            }
        }

        private void checkBoxBlank_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBlank.Checked)
            {
                textBoxXml.Enabled = false;
                textBoxIP.Enabled = false;
                buttonBrowse.Enabled = false;
            }
            else
            {
                textBoxIP.Enabled = true;
                textBoxXml.Enabled = true;
                buttonBrowse.Enabled = true;
            }
        }
    }
}

