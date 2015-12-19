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
    public partial class ToolForm : Form
    {
        public ToolForm()
        {
            InitializeComponent();
        }

        private Generator parentForm;

         public ToolForm(Generator form1)
        {
             InitializeComponent();
             this.parentForm = form1;
        }

        private void TollForm_Load(object sender, EventArgs e)
        {

        }


        #region 添加标签
        private void Baddlab_Click(object sender, EventArgs e)
        {
            int tabIndex =  1; //获取控件所在标签页，从1开始
            for (int index = 1; index <= numericUpDown1.Value; index++)
            {
                Label lbl = new Label();
                lbl.Location = new Point(10, 20 * index * 2);
                lbl.BackColor = Color.Transparent;
                lbl.Name = "lbl" + tabIndex + "_" + index;
                lbl.Text = "标签" + index;
                lbl.TextAlign = ContentAlignment.MiddleRight;//标签文本位置靠右

                lbl.DoubleClick += new EventHandler(parentForm.lbl_DoubleClick);
                lbl.MouseDown += new MouseEventHandler(parentForm.lbl_MouseDown);
                lbl.MouseMove += new MouseEventHandler(parentForm.lbl_MouseMove);
                try
                {
                    
                  parentForm.tabControl.TabPages[1].Controls.Add(lbl);  // 在第二个page中添加控件
                  lbl.BringToFront();
                }
                catch (Exception)
                {
                    MessageBox.Show("请至少添加一个选项卡页面！");
                }

            }
        }
        #endregion 添加标签


        #region 添加变量
        private void BaddnumF_Click(object sender, EventArgs e)
        {
            int tabIndex = 1; //获取控件所在标签页，从1开始
            for (int index = 1; index <= numericUpDown2.Value; index++)
            {
                TextboxEX txt = new TextboxEX();
                txt.Location = new Point(10, 20 * index * 2);
                txt.Name = "txt" + tabIndex + "_" + index;
                txt.TextAlign = HorizontalAlignment.Right;//文本内容位置靠右
                txt.ReadOnly = true;
                txt.Cursor = Cursors.Hand;
                txt.BackColor = Color.White;
                txt.GotFocus += new EventHandler(parentForm.Text_GotFocus);

                txt.DoubleClick += new EventHandler(parentForm.Text_DoubleClick);
                txt.MouseDown += new MouseEventHandler(parentForm.Text_MouseDown);
                txt.MouseMove += new MouseEventHandler(parentForm.Text_MouseMove);
                parentForm.tabControl.TabPages[1].Controls.Add(txt);
                txt.BringToFront();
            }
        }
        #endregion 添加变量


        #region 添加开关量
        private void Baddswitch_Click(object sender, EventArgs e)
        {
            int tabIndex =  1; //获取控件所在标签页，从1开始
            for (int index = 1; index <= numericUpDown3.Value; index++)
            {
                Lamp lamp1 = new Lamp();
                lamp1.Location = new Point(10, 20 * index * 2);
                lamp1.Name = "lamp" + tabIndex + "_" + index;
                lamp1.ReadOnly = true;

                lamp1.MouseDoubleClick += new MouseEventHandler(parentForm.Lamp_DoubleClick);
                lamp1.MouseDown += new MouseEventHandler(parentForm.Lamp_MouseDown);
                lamp1.MouseMove += new MouseEventHandler(parentForm.Lamp_MouseMove);
                parentForm.tabControl.TabPages[1].Controls.Add(lamp1);
                lamp1.BringToFront();
            }
        }
        #endregion 添加开关量

    }
}
