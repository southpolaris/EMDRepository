using System.Drawing;
using System.Windows.Forms;

namespace WifiMonitor
{
    public enum ModbusInterface
    {
        DiscretesInputs = 1,    //Single bit read-only
        Coils = 2,              //Single bit read-write
        InputRegister = 3,      //16 bit read-only
        HoldingRegister = 4     //16 bit read-write     
    }

    public enum ModbusDataType
    {
        UnsignedShort = 0,
        SignedInt = 1,
        Float = 2
    }

    public partial class TextBoxEx : TextBox
    {
        public TextBoxEx()
        {
            InitializeComponent();
        }

        private int mPosition = 0;
        private int mSlave = 0;
        private ModbusDataType mbDataType;
        private ModbusInterface mbInterface;

        #region 属性
        public int RelateVar //数据所在位置
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        public int SlaveAddress //子节点地址（IP末位）
        {
            get { return mSlave; }
            set { mSlave = value; }
        }

        public ModbusInterface MbInterface //数据通道
        {
            get { return mbInterface; }
            set
            {
                switch (value)
                {
                    case ModbusInterface.InputRegister:
                        this.ReadOnly = true;
                        this.BackColor = Color.Gainsboro;
                        break;
                    case ModbusInterface.HoldingRegister:
                        this.ReadOnly = false;
                        this.BackColor = Color.White;
                        break;
                    default:
                        break;
                }
                mbInterface = value;
            }
        }

        public ModbusDataType MbDataType //数据类型
        {
            get { return mbDataType; }
            set { mbDataType = value; }
        }
        #endregion
    }
}
