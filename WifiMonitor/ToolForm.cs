using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WifiMonitor
{
    public partial class ToolForm : Form
    {
        public ToolForm()
        {
            InitializeComponent();
        }
        
        private MainForm mMainForm;
        public ToolForm(MainForm mainForm)
        {
             InitializeComponent();
             this.mMainForm = mainForm;
        }
        
        private void ToolForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalVar.editFlag = false;
        }

        #region 添加标签部分
        private void btnAddLbl_Click(object sender, EventArgs e)
        {
            int tabIndex = mMainForm.tabControl.SelectedIndex + 1; //获取控件所在标签页，从1开始
            for (int index = 1; index <= numLabel.Value; index++)
            {
                Label lbl = new Label();
                lbl.Location = new Point(10, 20 * index * 2);
                lbl.BackColor = Color.Transparent;
                lbl.Name = "lbl" + tabIndex + "_" + index;
                lbl.Text = "标签" + index;
                lbl.TextAlign = ContentAlignment.MiddleRight;//标签文本位置靠右
                //lbl.Click += new EventHandler(mMainForm.lbl_Click);
                lbl.DoubleClick += new EventHandler(mMainForm.lbl_DoubleClick);
                lbl.MouseDown += new MouseEventHandler(mMainForm.lbl_MouseDown);
                lbl.MouseMove += new MouseEventHandler(mMainForm.lbl_MouseMove);
                try
                {
                    mMainForm.tabControl.SelectedTab.Controls.Add(lbl);
                }
                catch (Exception)
                {
                    MessageBox.Show("请至少添加一个选项卡页面！");
                }

            }
        }
        #endregion 添加标签部分

        #region 添加文本框部分
        private void btnAddText_Click(object sender, EventArgs e)
        {
            int tabIndex = mMainForm.tabControl.SelectedIndex + 1; //获取控件所在标签页，从1开始
            for (int index = 1; index <= numText.Value; index++)
            {
                TextBoxEx txt = new TextBoxEx();
                txt.Location = new Point(10, 20 * index * 2);
                txt.Name = "txt" + tabIndex + "_" + index;
                txt.TextAlign = HorizontalAlignment.Right;//文本内容位置靠右
                txt.ReadOnly = true;
                txt.Cursor = Cursors.Hand;
                txt.BackColor = Color.White;
                txt.GotFocus += new EventHandler(mMainForm.Text_GotFocus);
                txt.Click += new EventHandler(mMainForm.Text_Click);
                txt.DoubleClick += new EventHandler(mMainForm.Text_DoubleClick);
                txt.MouseDown += new MouseEventHandler(mMainForm.Text_MouseDown);
                txt.MouseMove += new MouseEventHandler(mMainForm.Text_MouseMove);
                mMainForm.tabControl.SelectedTab.Controls.Add(txt);
            }
        }
        #endregion 添加文本框部分


        //添加指示灯
        private void btnAddLamp_Click(object sender, EventArgs e)
        {
            int tabIndex = mMainForm.tabControl.SelectedIndex + 1; //获取控件所在标签页，从1开始
            for (int index = 1; index <= numLamp.Value; index++)
            {
                Lamp lamp = new Lamp();
                lamp.Location = new Point(10, 20 * index * 2);
                lamp.Name = "lamp" + tabIndex + "_" + index;
                lamp.MouseDoubleClick += new MouseEventHandler(mMainForm.Lamp_DoubleClick);
                lamp.MouseDown += new MouseEventHandler(mMainForm.Lamp_MouseDown);
                lamp.MouseMove += new MouseEventHandler(mMainForm.Lamp_MouseMove);
                mMainForm.tabControl.SelectedTab.Controls.Add(lamp);
            }
        }
        
        private void btnCreateTab_Click(object sender, EventArgs e)
        {
            NewMachine newMachine = new NewMachine(mMainForm);
            //Not finished
            newMachine.Show();

            int index = mMainForm.tabControl.TabCount + 1;
            string pageText = "新标签页" + index;
            string pageName = "TabPage" + index;
            TabPage tabPage = new TabPage(pageText);
            tabPage.UseVisualStyleBackColor = true;
            mMainForm.tabControl.TabPages.Add(tabPage);
        }

        TitleChange tabTitleChange;
        private void btnChangeTabName_Click(object sender, EventArgs e)
        {
            tabTitleChange = new TitleChange();
            tabTitleChange.Text = "更改标签页名称";
            tabTitleChange.txtTitle.Text = mMainForm.tabControl.SelectedTab.Text;
            tabTitleChange.btnSave.Click += new EventHandler(tabTitleChangebtnSave_Click);
            tabTitleChange.Show();
        }

        private void tabTitleChangebtnSave_Click(object o, EventArgs e)
        {
            mMainForm.tabControl.SelectedTab.Text = tabTitleChange.txtTitle.Text;
            tabTitleChange.Dispose();
        }

        //删除选中标签页及其上所有控件(界面)
        private void btnRemoveTab_Click(object sender, EventArgs e)
        {
            int currentIndex = mMainForm.tabControl.SelectedIndex;
            mMainForm.tabControl.TabPages.RemoveAt(currentIndex);
        }
    }
}
