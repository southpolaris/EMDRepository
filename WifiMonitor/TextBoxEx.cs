using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Linq.Expressions;

namespace WifiMonitor
{
    public enum ModbusInterface
    {
        DiscretesInputs = 1,    //Single bit read-only
        Coils = 2,              //Single bit read-write
        InputRegister = 3,      //16 bit read-only
        HoldingRegister = 4     //16 bit read-write     
    }

    public partial class TextBoxEx : TextBox
    {
        public TextBoxEx()
        {
            InitializeComponent();
        }

        #region 属性
        private int mPosition = 0;
        private int mSlave = 0;
        private ModbusInterface mbInterface;
        [Description("获取或设置该文本框的关联变量编号")]
        /// <summary>
        /// 获取或设置该文本框的关联变量编号
        /// </summary>
        public int RelateVar
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        public int SlaveAddress
        {
            get { return mSlave; }
            set { mSlave = value; }
        }

        public ModbusInterface MbInterface
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

        
        #endregion
    }
}
