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
    public partial class LampEditForm : Form
    {
        public LampEditForm()
        {
            InitializeComponent();
        }
        private bool readOnly;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnReadOnly.Checked)
            {
                ReadOnly = true;
                cbLampVar.Items.Clear();
                for(int address = 1; address <= GVL.dataLength.discreteInput; address++)
                {
                    cbLampVar.Items.Add(address);
                }
            }
        }

        private void radioButton0_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonReadWrite.Checked)
            {
                readOnly = false;
                cbLampVar.Items.Clear();
                for (int address = 1; address <= GVL.dataLength.coil; address++)
                {
                    cbLampVar.Items.Add(address);
                }
            }
        }

        public bool ReadOnly
        {
            set
            {
                readOnly = value;
                if (readOnly)
                    radioBtnReadOnly.Checked = true;
                else
                    radioButtonReadWrite.Checked = true;
            }
            get { return readOnly; }
        }       
    }
}
