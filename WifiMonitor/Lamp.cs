using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WifiMonitor
{
    public partial class Lamp : PictureBox
    {
        public Lamp()
        {
            InitializeComponent();
            powerOn = false;
        }

        public Lamp(IContainer container)
        {
            container.Add(this);
            powerOn = false;
            InitializeComponent();
        }

        private bool powerOn;
        public int RelateVar; //当前关联项
        
        /// <summary>
        /// 指示灯亮灭属性
        /// </summary>
        [Description("指示灯控件，为true则亮灯")]
        public bool onFlag
        {
            set 
            { 
                this.powerOn = value;
                if (powerOn == true)
                {
                    this.Image = Properties.Resources.lampon;
                }
                else
                {
                    this.Image = Properties.Resources.lampoff;
                }
            }
            get { return this.powerOn; }
        }

    }
}
