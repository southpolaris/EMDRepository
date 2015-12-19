using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLGenerator
{
    public partial class TextEditForm : Form
    {
        public TextEditForm()
        {
            InitializeComponent();
        }

        public DataInterface modbusInterface;
        public ModbusDataType modbusDataType;

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                this.modbusInterface = DataInterface.InputRegister;
                cbbTxtVar.Items.Clear();
                for (int address = 1; address <= GVL.dataLength.inputRegister; address++)
                {
                    cbbTxtVar.Items.Add(address);
                }
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                this.modbusInterface = DataInterface.HoldingRegister;
                cbbTxtVar.Items.Clear();
                for (int address = 1; address <= GVL.dataLength.holdingRegiter; address++)
                {
                    cbbTxtVar.Items.Add(address);
                }
            }
        }

        public void Initial()
        {
            if (radioButton3.Checked)
            {
                this.modbusInterface = DataInterface.InputRegister;
                cbbTxtVar.Items.Clear();
                for (int address = 1; address <= GVL.dataLength.inputRegister; address++)
                {
                    cbbTxtVar.Items.Add(address);
                }
            }
            if (radioButton4.Checked)
            {
                this.modbusInterface = DataInterface.HoldingRegister;
                cbbTxtVar.Items.Clear();
                for (int address = 1; address <= GVL.dataLength.holdingRegiter; address++)
                {
                    cbbTxtVar.Items.Add(address);
                }
            }
        }
    }
}
