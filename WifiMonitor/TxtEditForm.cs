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

        public ModbusInterface modbusInterface;
        public ModbusDataType modbusDataType;

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                this.modbusInterface = ModbusInterface.InputRegister;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                this.modbusInterface = ModbusInterface.HoldingRegister;
            }
        }

        private void cbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbDataType.SelectedIndex)
            {
                case 0:
                    cbbTxtVar.Items.Clear();
                    cbbTxtVar.Items.AddRange(new object[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39});
                    modbusDataType = ModbusDataType.SignedInt;
                    break;
                case 1:
                    cbbTxtVar.Items.Clear();
                    cbbTxtVar.Items.AddRange(new object[] { 41, 43, 45, 47, 49, 51, 53, 55, 57, 59, 61, 63, 65, 67, 69, 71, 73, 75, 77, 79 });
                    modbusDataType = ModbusDataType.Float;
                    break;
                case 2:
                    cbbTxtVar.Items.Clear();
                    cbbTxtVar.Items.AddRange(new object[] { 0 });
                    modbusDataType = ModbusDataType.UnsignedShort;
                    break;
                default:
                    break;
            }
        }
    }        
}
