using System.ComponentModel;
using System.Windows.Forms;

namespace WifiMonitor
{
    public partial class Lamp : PictureBox
    {
        public Lamp()
        {
            InitializeComponent();
            this.Image = Properties.Resources.lampoff;
            onFlag = false;
            ReadOnly = true;
        }

        private bool powerOn;
        private bool readOnly;
        private int relateVar; //当前关联项
        private int slaveAddress;
        
        /// <summary>
        /// 指示灯亮灭属性
        /// </summary>
        [Description("指示灯控件，为true则亮灯")]
        public bool onFlag
        {
            set 
            { 
                this.powerOn = value;
                if (powerOn)
                {
                    if (readOnly)
                        this.Image = Properties.Resources.lampon;
                    else
                        this.Image = Properties.Resources.buttonDown;
                }
                else
                {
                    if (readOnly)
                        this.Image = Properties.Resources.lampoff;
                    else
                        this.Image = Properties.Resources.buttonUp;
                }
            }
            get { return this.powerOn; }
        }

        public int SlaveAddress
        {
            set { this.slaveAddress = value; }
            get { return this.slaveAddress; }
        }
        public int RelateVar
        {
            set { this.relateVar = value; }
            get { return this.relateVar; }
        }
        public bool ReadOnly
        {
            set
            { 
                this.readOnly = value;
                if (readOnly)
                {
                    this.Image = Properties.Resources.lampoff;
                }
                else
                {
                    this.Image = Properties.Resources.buttonUp;
                }
            }
            get { return this.readOnly; }
        }
    }
}
