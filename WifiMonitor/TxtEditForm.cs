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
    public partial class TxtEditForm : Form
    {
        //Comm mComm = new Comm();
        public TxtEditForm()
        {
            InitializeComponent();
        }

        public DataInterface modbusInterface;
        public DataType modbusDataType;

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                this.modbusInterface = DataInterface.InputRegister;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                this.modbusInterface = DataInterface.HoldingRegister;
            }
        }
    }        
}
