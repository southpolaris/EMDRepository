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
            GlobalVar.bEdit = false;
            mMainForm.btnEdit.Enabled = true;//启用编辑按钮
            mMainForm.btnSavEdit.Enabled = false;//禁用保存按钮
        }

        #region 添加标签部分
        private void btnAddLbl_Click(object sender, EventArgs e)
        {
            int tabIndex = mMainForm.tabControl1.SelectedIndex + 1; //获取控件所在标签页，从1开始
            for (int index = 1; index <= numLabel.Value; index++)
            {
                Label lbl = new Label();
                lbl.Location = new Point(10, 20 * index * 2);
                lbl.Name = "lbl" + tabIndex + "_" + index;
                lbl.Text = "标签" + index;
                lbl.TextAlign = ContentAlignment.MiddleRight;//标签文本位置靠右
                lbl.Click += new EventHandler(mMainForm.lbl_Click);
                lbl.DoubleClick += new EventHandler(mMainForm.lbl_DoubleClick);
                lbl.MouseDown += new MouseEventHandler(mMainForm.lbl_MouseDown);
                lbl.MouseMove += new MouseEventHandler(mMainForm.lbl_MouseMove);
                mMainForm.tabControl1.SelectedTab.Controls.Add(lbl);
            }
        }
        #endregion 添加标签部分

        #region 添加文本框部分
        private void btnAddText_Click(object sender, EventArgs e)
        {
            int tabIndex = mMainForm.tabControl1.SelectedIndex + 1; //获取控件所在标签页，从1开始
            for (int index = 1; index <= numText.Value; index++)
            {
                TextBoxEx txt = new TextBoxEx();
                txt.Location = new Point(10, 20 * index * 2);
                txt.Name = "txt" + tabIndex + "_" + index;
                txt.TextAlign = HorizontalAlignment.Right;//文本内容位置靠右
                txt.ReadOnly = true;
                txt.Cursor = Cursors.Hand;
                txt.BackColor = Color.White;
                txt.GotFocus += new EventHandler(mMainForm.txt_GotFocus);
                txt.Click += new EventHandler(mMainForm.txt_Click);
                txt.DoubleClick += new EventHandler(mMainForm.txt_DoubleClick);
                txt.MouseDown += new MouseEventHandler(mMainForm.txt_MouseDown);
                txt.MouseMove += new MouseEventHandler(mMainForm.txt_MouseMove);
                mMainForm.tabControl1.SelectedTab.Controls.Add(txt);
            }
        }
        #endregion 添加文本框部分

        private void btnSavNum_Click(object sender, EventArgs e)
        {
            IniFile.WriteIniData("CommProperty", "RcvCmdNum", txtDataNum.Text, GlobalVar.sIniPath);
            GlobalVar.RcvCmdNum = int.Parse(txtDataNum.Text);
            MessageBox.Show("监视变量数量已保存");
        }

        private void btnCreateTab_Click(object sender, EventArgs e)
        {
            int index = mMainForm.tabControl1.TabCount + 1;
            string pageText = "新标签页" + index;
            string pageName = "TabPage" + index;
            TabPage tabPage = new TabPage(pageText);
            tabPage.UseVisualStyleBackColor = true;
            mMainForm.tabControl1.TabPages.Add(tabPage);
        }

        TitleChange tabTitleChange;
        private void btnChangeTabName_Click(object sender, EventArgs e)
        {
            tabTitleChange = new TitleChange();
            tabTitleChange.txtTitle.Text = mMainForm.tabControl1.SelectedTab.Text;
            tabTitleChange.btnSave.Click += new EventHandler(tabTitleChangebtnSave_Click);
            tabTitleChange.Show();
        }

        private void tabTitleChangebtnSave_Click(object o, EventArgs e)
        {
            mMainForm.tabControl1.SelectedTab.Text = tabTitleChange.txtTitle.Text;
            tabTitleChange.Dispose();
        }
    }
}
