using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Threading; //用于创建线程
using System.IO.Ports;
using System.Globalization;
using System.Collections; //ArrayList

namespace WifiMonitor
{
    public partial class MainForm : Form
    {
        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);//用于隐藏TextBox的光标

        #region 参数定义
        //定义串口类
        static SerialComm sc = new SerialComm("COM1");

        //鼠标拖动标签时标签位置
        Point lblStart;
        Point lblLocat;

        //当前编辑标签及其编辑框
        Label currLbl;
        LblEditForm mCurrLblEditForm;

        //鼠标拖动文本框时文本框位置
        Point txtStart;
        Point txtLocat;

        //灯的位置
        Point lampStart;
        Point lampLocat;

        //当前编辑文本框及其编辑框
        TextBoxEx currTxt;
        TxtEditForm mCurrTxtEditForm;

        //指示灯
        Lamp currLamp;
        LampEditForm mCurrLampEditForm;

        //标题栏弹出编辑框
        TitleChange mTitleChange;

        //通信线程
        Thread CommThd;

        #endregion

        public MainForm()
        {          
            InitializeComponent();

            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;//取消跨线程检测  最好用Invoke，禁止编译器对跨线程访问作检查，可以实现访问

            #region 加载主窗体参数
            //判断返回值，避免第一次运行时为空出错  
            if ((IniFile.ReadIniData("MainForm", "Width", null, GlobalVar.sIniPath) != "") && (IniFile.ReadIniData("MainForm", "Height", null, GlobalVar.sIniPath) != ""))
            {
                //读取主窗体大小、标题
                GlobalVar.nMainFormWidth = int.Parse(IniFile.ReadIniData("MainForm", "Width", null, GlobalVar.sIniPath));
                GlobalVar.nMainFormHeight = int.Parse(IniFile.ReadIniData("MainForm", "Height", null, GlobalVar.sIniPath));
                GlobalVar.sMainFormTitle = IniFile.ReadIniData("MainForm", "Title", null, GlobalVar.sIniPath);
            }
            #endregion

            //遍历所有节名  用于加载控件
            string[] sSectionName = IniFile.INIGetAllSectionNames(GlobalVar.sIniPath);
            int indexLbl = 1;
            int indexTxt = 1;
            int indexTab = 1;
            int indexLamp = 1;

            ////读取接收的数据个数 RcvCmdNum
            //GlobalVar.RcvCmdNum = int.Parse(IniFile.ReadIniData("CommProperty", "RcvCmdNum", "0", GlobalVar.sIniPath));

            ////读取Label个数
            //GlobalVar.iLabel = int.Parse(IniFile.ReadIniData("LabelProperty", "LabelNum", "0", GlobalVar.sIniPath));
            ////读取TextBox个数
            //GlobalVar.iTextBox = int.Parse(IniFile.ReadIniData("TextBoxProperty", "TextBoxNum", "0", GlobalVar.sIniPath));
            ////读取tab个数，至少为一个
            //GlobalVar.iTab = int.Parse(IniFile.ReadIniData("TabProperty", "TabPageCount", "1", GlobalVar.sIniPath));

            foreach (string str in sSectionName)
            {
                //加载tab控件
                if ("TabPage" + indexTab.ToString() == str)
                {
                    string tabText = IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Text", "New Tab");
                    TabPage tabPage = new TabPage(tabText);
                    tabPage.UseVisualStyleBackColor = true;
                    tabControl1.TabPages.Add(tabPage);
                    indexTab++;
                }
            }

            foreach (string str in sSectionName)
            {
                #region 根据读取的Label个数加载Label控件
                if ("Label"+indexLbl.ToString() == str)
                {       
                    string TempLabelID = IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "LabelID", "NoItem");
                    string TempLabelText = IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "LabelText", "NoItem");
                    int TempPosX = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosX", "NoItem"));
                    int TempPosY = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosY", "NoItem"));
                    int TempWidth = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Width", "NoItem"));
                    int TempHeight = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Height", "NoItem"));
                    int TempAdscription = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Adscription", "NoItem"));

                    Label lbl = new Label();
                    lbl.Location = new Point(TempPosX, TempPosY);
                    lbl.Name = TempLabelID;
                    lbl.Text = TempLabelText;
                    lbl.Width = TempWidth;
                    lbl.Height = TempHeight;
                    lbl.TextAlign = ContentAlignment.MiddleRight;
                    lbl.DoubleClick += new EventHandler(lbl_DoubleClick);
                    lbl.MouseDown += new MouseEventHandler(lbl_MouseDown);
                    lbl.MouseMove += new MouseEventHandler(lbl_MouseMove);
                    this.tabControl1.TabPages[TempAdscription - 1].Controls.Add(lbl);
                    indexLbl++;
                }
                #endregion

                #region 根据读取的TextBox个数加载TextBox控件
                if ("TextBox" + indexTxt.ToString() == str)
                {
                    string TempTextBoxID = IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "TextBoxID", "NoItem");
                    int TempTextBoxRelateVar = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "RelateVar", "NoItem"));
                    int TempPosX = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosX", "NoItem"));
                    int TempPosY = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosY", "NoItem"));
                    int TempWidth = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Width", "NoItem"));
                    int TempHeight = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Height", "NoItem"));
                    int TempAdscription = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Adscription", "NoItem"));


                    TextBoxEx txt = new TextBoxEx();
                    txt.Location = new Point(TempPosX, TempPosY);
                    txt.Name = TempTextBoxID;
                    txt.Width = TempWidth;
                    txt.Height = TempHeight;
                    txt.TextAlign = HorizontalAlignment.Right;//文本内容位置靠右
                    txt.ReadOnly = true;
                    txt.Cursor = Cursors.Hand;
                    txt.BackColor = Color.White;
                    txt.RelateVar = TempTextBoxRelateVar;
                    try
                    {
                        txt.Text = GlobalVar.RcvCmdLst[txt.RelateVar].ToString();
                    }
                    catch (System.Exception ex)
                    {
                    	
                    }
                    txt.Click += new EventHandler(txt_Click);
                    txt.DoubleClick += new EventHandler(txt_DoubleClick);
                    txt.MouseDown += new MouseEventHandler(txt_MouseDown);
                    txt.MouseMove += new MouseEventHandler(txt_MouseMove);
                    this.tabControl1.TabPages[TempAdscription - 1].Controls.Add(txt);
                    indexTxt++;
                }
                #endregion

                #region 根据配置加载指示灯控件
                if ("Lamp" + indexLamp.ToString() == str)
                {
                    string tempLampID = IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "LampID", "NoItem");
                    int tempPosX = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosX", "NoItem"));
                    int tempPosY = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosY", "NoItem"));
                    bool tempLampVar = bool.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "RelateVar", "False"));
                    int tempAdscription = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Adscription", "NoItem"));

                    Lamp lamp = new Lamp();
                    lamp.Location = new Point(tempPosX, tempPosY);
                    lamp.Name = tempLampID;
                    lamp.onFlag = tempLampVar;
                    try
                    {
                        //lamp.onFlag = GlobalVar.RcvCmdLst[]
                    }
                    catch (Exception)
                    {
                        
                    }
                    //lamp.Click += new EventHandler(lamp_Click);
                    lamp.DoubleClick += new EventHandler(lamp_DoubleClick);
                    lamp.MouseDown += new MouseEventHandler(lamp_MouseDown);
                    lamp.MouseMove += new MouseEventHandler(lamp_MouseMove);
                    this.tabControl1.TabPages[tempAdscription - 1].Controls.Add(lamp);
                    indexLamp++;
                }
                #endregion
            }

            //显示配置文件中设定的主窗体大小、标题
            this.Width = GlobalVar.nMainFormWidth;
            this.Height = GlobalVar.nMainFormHeight;
            this.Text = GlobalVar.sMainFormTitle;
        }

        //重写标题栏双击事件
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_NCLBUTTONDBLCLK = 0xA3;

            switch (m.Msg)
            {
                case WM_NCLBUTTONDBLCLK:
                    mTitleChange = new TitleChange();
                    mTitleChange.txtTitle.Text = GlobalVar.sMainFormTitle;
                    mTitleChange.Text = "更改窗口名称";
                    mTitleChange.btnSave.Click += new EventHandler(mTitleChange_btnSave_Click);
                    mTitleChange.Show();
                    break;
                default:
                    base.WndProc(ref m);   // 调用基类函数处理其他消息
                    break;
            }           
        }

        public void mTitleChange_btnSave_Click(object sender, EventArgs e)
        {
            IniFile.WriteIniData("MainForm", "Title", mTitleChange.txtTitle.Text, GlobalVar.sIniPath);
            GlobalVar.sMainFormTitle = mTitleChange.txtTitle.Text;
            this.Text = mTitleChange.txtTitle.Text;
            mTitleChange.Close();
        }

        #region 添加标签部分
        //标签单击事件
        public void lbl_Click(object sender, EventArgs e)
        {

        }

        //鼠标按下
        public void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            currLbl = new Label();
            if (sender is Label)
            {
                currLbl = sender as Label;
            }
            //记下控件坐标及鼠标坐标
            lblStart = Control.MousePosition;
            lblLocat = currLbl.Location;
            //右键弹出菜单
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripLbl.Show(Control.MousePosition.X, Control.MousePosition.Y);
                //ItemDelLbl_Click(sender, e);
            }
        }

        //鼠标拖动 实时改变控件坐标
        public void lbl_MouseMove(object sender, MouseEventArgs e)
        {
            Label lbl = new Label();
            if (sender is Label)
            {
                lbl = (Label)sender;
            }
            if (e.Button == MouseButtons.Left)
            {
                Point temp = Control.MousePosition;

                lbl.Location = new Point(lblLocat.X + temp.X - lblStart.X, lblLocat.Y + temp.Y - lblStart.Y);
            }
        }

        //打开标签编辑对话框
        public void lbl_DoubleClick(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                currLbl = (Label)sender;
                mCurrLblEditForm = new LblEditForm();
                mCurrLblEditForm.txtName.Text = currLbl.Text;
                mCurrLblEditForm.txtWidth.Text = currLbl.Width.ToString();
                mCurrLblEditForm.txtHeight.Text = currLbl.Height.ToString();
                mCurrLblEditForm.txtPosX.Text = currLbl.Location.X.ToString();
                mCurrLblEditForm.txtPosY.Text = currLbl.Location.Y.ToString();
                mCurrLblEditForm.Show();
                mCurrLblEditForm.btnLblSav.Click += new EventHandler(mCurrLblEditForm_btnLblSav_Click);
            }
        }

        //保存当前编辑标签的属性
        public void mCurrLblEditForm_btnLblSav_Click(object sender, EventArgs e)//标签编辑对话框保存按钮事件
        {
            currLbl.Text = mCurrLblEditForm.txtName.Text;
            currLbl.Width = int.Parse(mCurrLblEditForm.txtWidth.Text);
            currLbl.Height = int.Parse(mCurrLblEditForm.txtHeight.Text);

            mCurrLblEditForm.Close();//保存完成后关闭
        }

        //右键菜单 删除标签
        public void ItemDelLbl_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab.Controls.Remove(currLbl);
        }
        #endregion 添加标签部分

        #region 添加文本部分
        //文本单击事件
        public void txt_Click(object sender, EventArgs e)
        {

        }

        //鼠标按下
        public void txt_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret((sender as TextBoxEx).Handle);//取消光标显示

            currTxt = new TextBoxEx();
            if (sender is TextBoxEx)
            {
                currTxt = (TextBoxEx)sender;
            }
            //记下控件坐标及鼠标坐标
            txtStart = Control.MousePosition;
            txtLocat = currTxt.Location;
            //右键弹出菜单
            if (e.Button == MouseButtons.Right)
            {
                currTxt.ContextMenuStrip = contextMenuStripTxt;
                contextMenuStripTxt.Show(MousePosition.X, MousePosition.Y);
            }
        }

        //鼠标拖动 实时改变控件坐标
        public void txt_MouseMove(object sender, MouseEventArgs e)
        {
            TextBoxEx txt = new TextBoxEx();
            if (sender is TextBoxEx)
            {
                txt = (TextBoxEx)sender;
            }
            if (e.Button == MouseButtons.Left)
            {
                Point temp = Control.MousePosition;

                txt.Location = new Point(txtLocat.X + temp.X - txtStart.X, txtLocat.Y + temp.Y - txtStart.Y);
            }
        }

        //打开文本框编辑对话框
        public void txt_DoubleClick(object sender, EventArgs e)
        {
            if (sender is TextBoxEx)
            {
                currTxt = (TextBoxEx)sender;
                mCurrTxtEditForm = new TxtEditForm();
                mCurrTxtEditForm.txtWidth.Text = currTxt.Width.ToString();
                mCurrTxtEditForm.txtHeight.Text = currTxt.Height.ToString();
                mCurrTxtEditForm.txtPosX.Text = currTxt.Location.X.ToString();
                mCurrTxtEditForm.txtPosY.Text = currTxt.Location.Y.ToString();
                mCurrTxtEditForm.Show();
                mCurrTxtEditForm.btnTxtSav.Click += new EventHandler(mCurrTxtEditForm_btnTxtSav_Click);

                //给下拉框赋值
                GlobalVar.TempCmdLst.Clear();
                for (int i = 1; i <= GlobalVar.RcvCmdNum;i++ )
                {
                    GlobalVar.TempCmdLst.Add(new DictionaryEntry("监控变量" + i.ToString(), i.ToString()));
                }

                mCurrTxtEditForm.cbbTxtVar.DataSource = GlobalVar.TempCmdLst;
                mCurrTxtEditForm.cbbTxtVar.DisplayMember = "Key";
                mCurrTxtEditForm.cbbTxtVar.ValueMember = "Value";
                try
                {
                    mCurrTxtEditForm.cbbTxtVar.SelectedIndex = currTxt.RelateVar - 1;//下拉框显示当前关联变量项
                }
                catch (Exception)
                {
                    
                }
                
            }
        }

        //保存当前编辑文本框的属性
        public void mCurrTxtEditForm_btnTxtSav_Click(object sender, EventArgs e)//标签编辑对话框保存按钮事件
        {
            currTxt.Width = int.Parse(mCurrTxtEditForm.txtWidth.Text);
            currTxt.Height = int.Parse(mCurrTxtEditForm.txtHeight.Text);

            try
            {
                currTxt.Text = GlobalVar.RcvCmdLst[int.Parse(mCurrTxtEditForm.cbbTxtVar.SelectedValue.ToString()) - 1].ToString();//for test  need refresh               
                currTxt.RelateVar = int.Parse(mCurrTxtEditForm.cbbTxtVar.SelectedValue.ToString());//将该文本框关联的变量标号赋给该文本框属性
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置变量个数和实际读取个数不符，请点击启动按钮读取数据，重新检查后再关联。", "注意");
                currTxt.Text = "";
                currTxt.RelateVar = 0;//将该文本框关联的变量标号赋给该文本框属性
            }

            mCurrTxtEditForm.Close();//保存完成后关闭
        }

        //右键菜单 删除文本框
        private void ItemDelTxt_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab.Controls.Remove(currTxt);
        }

        //取消光标显示
        public void txt_GotFocus(object sender, EventArgs e)
        {
            HideCaret((sender as TextBox).Handle);
        }
        #endregion 添加文本部分

        #region 添加指示灯
        public void lamp_MouseDown(object sender, MouseEventArgs e)
        {
            currLamp = new Lamp();
            if (sender is Lamp)
            {
                currLamp = sender as Lamp;
            }
            //记下位置坐标
            lampStart = Control.MousePosition;
            lampLocat = currLamp.Location;
            if (e.Button == MouseButtons.Right)
            {
                currLamp.ContextMenuStrip = contextMenuStripLamp;
                contextMenuStripLamp.Show(MousePosition);
            }
        }

        //drag to move
        public void lamp_MouseMove(object sender, MouseEventArgs e)
        {
            Lamp lamp = new Lamp();
            if (sender is Lamp)
            {
                lamp = sender as Lamp;
            }
            if (e.Button == MouseButtons.Left)
            {
                Point temp = Control.MousePosition;
                lamp.Location = new Point(lampLocat.X + temp.X - lampStart.X, lampLocat.Y + temp.Y - lampStart.Y);
            }
        }

        //double click open edit form
        public void lamp_DoubleClick(object sender, EventArgs e)
        {
            if (sender is Lamp)
            {
                currLamp = sender as Lamp;
                mCurrLampEditForm = new LampEditForm();
                mCurrLampEditForm.tbLampX.Text = currLamp.Location.X.ToString();
                mCurrLampEditForm.tbLampY.Text = currLamp.Location.Y.ToString();
                mCurrLampEditForm.buttonOk.Click += new EventHandler(LampEditbuttonOk_Click);
                //给下拉框赋值
                GlobalVar.TempCmdLst.Clear();
                for (int i = 1; i <= GlobalVar.RcvCmdNum; i++)
                {
                    GlobalVar.TempCmdLst.Add(new DictionaryEntry("监控变量" + i.ToString(), i.ToString()));
                }
                mCurrLampEditForm.Show();
                mCurrLampEditForm.cbLampVar.DataSource = GlobalVar.TempCmdLst;
                mCurrLampEditForm.cbLampVar.DisplayMember = "Key";
                mCurrLampEditForm.cbLampVar.ValueMember = "Value";
                try
                {
                    //下拉框显示当前关联变量项，变量在接受数据中的位置。
                    mCurrLampEditForm.cbLampVar.SelectedIndex = currLamp.RelateVar - 1;
                }
                catch (Exception)
                {
                }
            }
        }

        //保存当前指示灯属性
        void LampEditbuttonOk_Click(object sender, EventArgs e)
        {
            int posX = int.Parse(mCurrLampEditForm.tbLampX.Text);
            int posY = int.Parse(mCurrLampEditForm.tbLampY.Text);
            currLamp.Location = new Point(posX, posY);
            try
            {
                string receive = GlobalVar.RcvCmdLst[int.Parse(mCurrLampEditForm.cbLampVar.SelectedIndex.ToString()) - 1].ToString();
                currLamp.onFlag = (int.Parse(receive) == 0) ? false : true;
            }
            catch (Exception)
            {
                MessageBox.Show("设置变量个数和实际读取个数不符，请点击启动按钮读取数据，重新检查后再关联。", "注意");
                currLamp.onFlag = false;
            }
            mCurrLampEditForm.Close();
        }

        private void DelLampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab.Controls.Remove(currLamp);
        }
        #endregion

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //主窗体尺寸改变后  记入INI
            IniFile.WriteIniData("MainForm", "Width", this.Width.ToString(), GlobalVar.sIniPath);
            IniFile.WriteIniData("MainForm", "Height", this.Height.ToString(), GlobalVar.sIniPath);
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            //主窗体坐标改变后  记入INI
            IniFile.WriteIniData("MainForm", "PosX", this.Location.X.ToString(), GlobalVar.sIniPath);
            IniFile.WriteIniData("MainForm", "PosY", this.Location.Y.ToString(), GlobalVar.sIniPath);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ToolForm mToolForm = new ToolForm(this);

            if ((IniFile.ReadIniData("MainForm", "Width", null, GlobalVar.sIniPath) != "") && (IniFile.ReadIniData("MainForm", "Height", null, GlobalVar.sIniPath) != "")
                &&(int.Parse(IniFile.ReadIniData("MainForm", "PosX", null, GlobalVar.sIniPath))!= 0)&&(int.Parse(IniFile.ReadIniData("MainForm", "PosY", null, GlobalVar.sIniPath))!=0))
            {
                //读取改变后的主窗体坐标和大小
                GlobalVar.nMainFormWidth = int.Parse(IniFile.ReadIniData("MainForm", "Width", null, GlobalVar.sIniPath));
                GlobalVar.nMainFormHeight = int.Parse(IniFile.ReadIniData("MainForm", "Height", null, GlobalVar.sIniPath));

                GlobalVar.nMainFormPosX = int.Parse(IniFile.ReadIniData("MainForm", "PosX", null, GlobalVar.sIniPath));
                GlobalVar.nMainFormPosY = int.Parse(IniFile.ReadIniData("MainForm", "PosY", null, GlobalVar.sIniPath));
            }

            if (false == GlobalVar.bEdit)
            {
                btnEdit.Enabled = false;//禁用编辑按钮
                //btnSavEdit.Enabled = true;//启用保存按钮
                GlobalVar.bEdit = true;
                mToolForm.Location = new Point(GlobalVar.nMainFormPosX + GlobalVar.nMainFormWidth + 12,GlobalVar.nMainFormPosY);
                mToolForm.Show();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                CommThd = new Thread(new ThreadStart(GetConn));
                CommThd.Start();// 线程启动
                GlobalVar.runningFlag = true;
            }
            catch
            {
                MessageBox.Show("未正常建立通信");
            }
        }

        //  连接赋值函数
        void GetConn()
        {
            sc.openPort();
            sc.DataReceived += new SerialComm.SerialPortDataReceiveEventArgs(DataReceivedCallback);
            while (GlobalVar.runningFlag)
            {
                //串口读数，赋值
                //sc.SendData("RDS 3000 U");
                Thread.Sleep(200);
            }          
        }

        void TxtTimer_Start(object source, System.Timers.ElapsedEventArgs e)
        {
            foreach (Control c in Controls)
            {
                if (c is TextBoxEx)
                {
                    TextBoxEx txt = (TextBoxEx)c;
                    txt.Text = GlobalVar.RcvCmdLst[txt.RelateVar-1].ToString();
                }
            }
        }

        /// <summary>
        /// 串口数据接收回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bits">接收到的数据</param>
        void DataReceivedCallback(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            string[] ArrSerialCommand = Encoding.Default.GetString(bits).Split(' ');
            GlobalVar.RcvCmdLst.Clear();
            for (int i = 0; i < ArrSerialCommand.Length ;i++ )
            {
                GlobalVar.RcvCmdLst.Add(ArrSerialCommand[i]);
                GlobalVar.RcvCmdNum++;
            }

            foreach (TabPage pages in tabControl1.TabPages)
            {
                foreach (Control ctrl in pages.Controls)
                {
                    if (ctrl is TextBoxEx)
                    {
                        TextBoxEx txt = (TextBoxEx)ctrl;
                        if (txt.RelateVar != 0) //判断变量已经关联
                            txt.Text = GlobalVar.RcvCmdLst[txt.RelateVar - 1].ToString();
                        else
                            txt.Text = "未关联变量";
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("退出前是否保存更改", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (result) 
            {
                case DialogResult.Yes:
                    btnSavEdit.PerformClick();
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;       
            }
            sc.closePort();
            if (GlobalVar.runningFlag)
            {
                 CommThd.Abort();
                 GlobalVar.runningFlag = false;
            }
            this.Dispose();  
        }

        /// <summary>
        /// 保存当前窗体控件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavEdit_Click(object sender, EventArgs e)
        {
            int tabIndex = 1;
            int labelIndex = 1;
            int txtIndex = 1;
            int lampIndex = 1;

            //遍历INI文件中所有lbl节名
            string[] sSectionName = IniFile.INIGetAllSectionNames(GlobalVar.sIniPath);
            foreach (string str in sSectionName) //删除所有空间信息
            {
                if (str.Contains("Label") || str.Contains("TextBox") || str.Contains("Lamp") || str.Contains("TabPage"))
                {
                    IniFile.INIDeleteSection(GlobalVar.sIniPath, str);
                }
            }

            //重新写入控件信息
            foreach (TabPage page in tabControl1.TabPages)
            {
                foreach (Control ctrl in page.Controls)
                {
                    if (ctrl is Label)
                    {
                        string lblKnot = "";
                        lblKnot = "Label" + labelIndex.ToString();//节名 
                        ctrl.Name = lblKnot;//重新分配标签标识  以节名命名

                        IniFile.WriteIniData(lblKnot, "LabelID", ctrl.Name, GlobalVar.sIniPath);
                        IniFile.WriteIniData(lblKnot, "LabelText", ctrl.Text, GlobalVar.sIniPath);
                        IniFile.WriteIniData(lblKnot, "PosX", ctrl.Location.X.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(lblKnot, "PosY", ctrl.Location.Y.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(lblKnot, "Width", ctrl.Width.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(lblKnot, "Height", ctrl.Height.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(lblKnot, "Adscription", tabIndex.ToString(), GlobalVar.sIniPath); //所在标签页索引

                        labelIndex++;
                    }
                    if (ctrl is TextBoxEx)
                    {
                        string TxtKnot = "";
                        currTxt = new TextBoxEx();

                        TxtKnot = "TextBox" + txtIndex.ToString();//节名 
                        ctrl.Name = TxtKnot;//重新分配标识  以节名命名

                        IniFile.WriteIniData(TxtKnot, "PosX", ctrl.Location.X.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(TxtKnot, "PosY", ctrl.Location.Y.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(TxtKnot, "Width", ctrl.Width.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(TxtKnot, "Height", ctrl.Height.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(TxtKnot, "TextBoxID", ctrl.Name, GlobalVar.sIniPath);
                        IniFile.WriteIniData(TxtKnot, "RelateVar", ((TextBoxEx)ctrl).RelateVar.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(TxtKnot, "Adscription", tabIndex.ToString(), GlobalVar.sIniPath); //所在标签页索引

                        txtIndex++;
                    }
                    if (ctrl is Lamp)
                    {
                        string lampNot = "";
                        currLamp = new Lamp();

                        lampNot = "Lamp" + lampIndex.ToString();
                        ctrl.Name = lampNot;                        

                        IniFile.WriteIniData(lampNot, "PosX", ctrl.Location.X.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(lampNot, "PosY", ctrl.Location.Y.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(lampNot, "LampID", ctrl.Name, GlobalVar.sIniPath);
                        IniFile.WriteIniData(lampNot, "RelateVar", (ctrl as Lamp).onFlag.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(lampNot, "Adscription", tabIndex.ToString(), GlobalVar.sIniPath);

                        lampIndex++;
                    }
                }
                IniFile.WriteIniData("TabPage" + tabIndex.ToString(), "Text", tabControl1.TabPages[tabIndex - 1].Text, GlobalVar.sIniPath);

                tabIndex++;//标签页索引
            }

            IniFile.WriteIniData("LabelProperty", "LabelNum", (labelIndex - 1).ToString(), GlobalVar.sIniPath); //标签总数
            IniFile.WriteIniData("TabProperty", "TabPageCount", this.tabControl1.TabCount.ToString(), GlobalVar.sIniPath); //标签页总数
            IniFile.WriteIniData("TextBoxProperty", "TextBoxNum", (txtIndex - 1).ToString(), GlobalVar.sIniPath); //当前TextBox控件的个数
            IniFile.WriteIniData("LampProperty", "LampNum", (lampIndex -1).ToString(), GlobalVar.sIniPath);
        }


    }
}
