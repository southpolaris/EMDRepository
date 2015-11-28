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
    public partial class NewMachine : Form
    {
        MainForm mMainForm;
        private string configFilePath = "";
        public NewMachine()
        {
            InitializeComponent();
        }
        public NewMachine(MainForm parent)
        {
            mMainForm = parent;
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory.ToString();
            openFileDialog.Filter = "XML files (*.xml)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxXml.Text = openFileDialog.FileName;
            }
        }
    }
}
