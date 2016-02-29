using System.Drawing;
using System.Windows.Forms;
using System.Net;

namespace WifiMonitor
{
    /// <summary>
    /// 数值量属性及显示
    /// </summary>
    public partial class TextBoxEx : TextBox
    {
        public TextBoxEx()
        {
            InitializeComponent();
            MbInterface = DataInterface.HoldingRegister;
            RelateVar = 0;
            mInDataBase = DataSave.DoNotSave;
            VarName = "未关联变量";
        }

        private int mPosition = 0;
        private IPAddress mSlave = IPAddress.None;
        private DataType mbDataType;
        private DataInterface mbInterface;
        public DataSave mInDataBase;
        private string mName;


        #region 属性
        public int RelateVar //数据所在位置
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        public IPAddress SlaveAddress //子节点地址（IP末位）
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
                        this.ReadOnly = true;
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

        public DataType MbDataType //数据类型
        {
            get { return mbDataType; }
            set { mbDataType = value; }
        }
        #endregion
    }
}
