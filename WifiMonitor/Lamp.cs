using System.ComponentModel;
using System.Windows.Forms;

namespace WifiMonitor
{
    /// <summary>
    /// 开关量显示和属性
    /// </summary>
    public partial class Lamp : PictureBox
    {
        public Lamp()
        {
            InitializeComponent();
            this.Image = Properties.Resources.lampoff;
            onFlag = false;
            ReadOnly = true;
            mInDataBase = false;
            mName = "未关联变量";
        }

        private bool mPowerOn;
        private bool mReadOnly;
        private int mRelateVar; //当前关联项
        private int mSlaveAddress;
        public bool mInDataBase;
        public string mName;
        
        /// <summary>
        /// 指示灯亮灭属性
        /// </summary>
        [Description("指示灯控件，为true则亮灯")]
        public bool onFlag
        {
            set 
            { 
                this.mPowerOn = value;
                if (mPowerOn)
                {
                    if (mReadOnly)
                        this.Image = Properties.Resources.lampon;
                    else
                        this.Image = Properties.Resources.buttonDown;
                }
                else
                {
                    if (mReadOnly)
                        this.Image = Properties.Resources.lampoff;
                    else
                        this.Image = Properties.Resources.buttonUp;
                }
            }
            get { return this.mPowerOn; }
        }

        public int SlaveAddress
        {
            set { this.mSlaveAddress = value; }
            get { return this.mSlaveAddress; }
        }
        public int RelateVar
        {
            set { this.mRelateVar = value; }
            get { return this.mRelateVar; }
        }
        public bool ReadOnly
        {
            set
            { 
                this.mReadOnly = value;
                if (mReadOnly)
                {
                    this.Image = Properties.Resources.lampoff;
                }
                else
                {
                    this.Image = Properties.Resources.buttonUp;
                }
            }
            get { return this.mReadOnly; }
        }
    }
}
