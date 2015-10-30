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
    public partial class LampEditForm : Form
    {
        public LampEditForm()
        {
            InitializeComponent();
        }

        private bool readOnly;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                 readOnly = true;
        }

        private void radioButton0_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton0.Checked)
                readOnly = false;
        }

        public bool ReadOnly
        {
            set
            {
                readOnly = value;
                if (readOnly)
                    radioButton1.Checked = true;
                else
                    radioButton0.Checked = true;
            }
            get { return readOnly; }
        }
    }
}
