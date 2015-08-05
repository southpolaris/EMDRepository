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
    public partial class TextBoxEx : TextBox
    {
        public TextBoxEx()
        {
            InitializeComponent();
        }

//         public TextBoxEx(IContainer container)
//         {
//             container.Add(this);
// 
//             InitializeComponent();
//         }

        #region 属性
        private int mRelateVar = 0;
        [Description("获取或设置该文本框的关联变量编号")]
        /// <summary>
        /// 获取或设置该文本框的关联变量编号
        /// </summary>
        public int RelateVar
        {
            get { return mRelateVar; }
            set { mRelateVar = value; }
        }
        #endregion
    }
}
