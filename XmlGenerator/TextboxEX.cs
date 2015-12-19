using System.Drawing;
using System.Windows.Forms;


namespace XMLGenerator
{
    public partial class TextboxEX : TextBox
    {
        public TextboxEX()
        {
            InitializeComponent();
            RelateVar = -1;
            SlaveAddress = 0;
            VarName = "未关联变量";
        }
        private int mPosition = 0;
        private int mSlave = 0;
        private ModbusDataType mbDataType;
        private DataInterface mbInterface;
        private string mName;

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

        public string VarName
        {
            get { return mName; }
            set 
            {
                mName = value;
                this.Text = mName;
            }
        }
        public DataInterface MbInterface //数据通道
        {
            get { return mbInterface; }
            set
            {
                switch (value)
                {
                    case DataInterface.InputRegister:
                        this.ReadOnly = false;
                        this.BackColor = Color.Gainsboro;
                        break;
                    case DataInterface.HoldingRegister:
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
