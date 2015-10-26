using System;
using System.Collections; //ArrayList
using System.Drawing;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading; //用于创建线程
using System.Windows.Forms;

namespace WifiMonitor
{
    public partial class MainForm : Form
    {
        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);//用于隐藏TextBox的光标

        #region 参数定义
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

        ConnectionForm mConnectionForm;
        ToolForm mToolForm;

        //通信类
        internal Communicate communicate = new Communicate();
        public delegate void UpdateStatus(string str);//更改界面状态显示

        #endregion

        public MainForm()
        {          
            InitializeComponent();
            
        }

        //重写标题栏双击事件
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_NCLBUTTONDBLCLK = 0xA3;

            switch (m.Msg)
            {
                case WM_NCLBUTTONDBLCLK:
                    if (GlobalVar.editFlag)
                    {
                        mTitleChange = new TitleChange();
                        mTitleChange.txtTitle.Text = GlobalVar.sMainFormTitle;
                        mTitleChange.Text = "更改窗口名称";
                        mTitleChange.btnSave.Click += new EventHandler(mTitleChange_btnSave_Click);
                        mTitleChange.Show();
                    }                    
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

        #region Label options
        //标签单击事件
        public void lbl_Click(object sender, EventArgs e)
        {

        }

        //鼠标按下
        public void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            if (GlobalVar.editFlag)
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
                }
            }           
        }

        //鼠标拖动 实时改变控件坐标
        public void lbl_MouseMove(object sender, MouseEventArgs e)
        {
            if (GlobalVar.editFlag)
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
        }

        //打开标签编辑对话框
        public void lbl_DoubleClick(object sender, EventArgs e)
        {
            if (GlobalVar.editFlag)
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
            this.tabControl.SelectedTab.Controls.Remove(currLbl);
        }
        #endregion 添加标签部分

        #region TextBox Options
        //文本单击事件
        public void txt_Click(object sender, EventArgs e)
        {
            HideCaret((sender as TextBoxEx).Handle);
            if (!GlobalVar.editFlag)
            {
                currTxt = new TextBoxEx();
                if (sender is TextBoxEx)
                {
                    currTxt = sender as TextBoxEx;
                }
                mTitleChange = new TitleChange();
                mTitleChange.Text = "写入单个变量";
                mTitleChange.lblTitle.Text = "写入新的数值";
                mTitleChange.txtTitle.Text = currTxt.Text;
                mTitleChange.btnSave.Click += new EventHandler(WriteSingleValue);
                mTitleChange.ShowDialog();
            }
        }

        public void WriteSingleValue(object sender, EventArgs e)
        {
            int slaveIndex = currTxt.SlaveAddress;
            ushort value = ushort.Parse(mTitleChange.txtTitle.Text);
            mTitleChange.Close();
            communicate.SendModbusData()
        }

        //鼠标按下
        public void txt_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret((sender as TextBoxEx).Handle);//取消光标显示
            if (GlobalVar.editFlag)
            {
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
        }

        //鼠标拖动 实时改变控件坐标
        public void txt_MouseMove(object sender, MouseEventArgs e)
        {
            if (GlobalVar.editFlag)
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
        }

        //打开文本框编辑对话框
        public void txt_DoubleClick(object sender, EventArgs e)
        {
            if (GlobalVar.editFlag)
            {
                if (sender is TextBoxEx)
                {
                    currTxt = (TextBoxEx)sender;
                    mCurrTxtEditForm = new TxtEditForm();

                    mCurrTxtEditForm.txtWidth.Text = currTxt.Width.ToString();
                    mCurrTxtEditForm.txtHeight.Text = currTxt.Height.ToString();
                    mCurrTxtEditForm.txtPosX.Text = currTxt.Location.X.ToString();
                    mCurrTxtEditForm.txtPosY.Text = currTxt.Location.Y.ToString();

                    mCurrTxtEditForm.txtSlaveAddress.Text = currTxt.SlaveAddress.ToString();
                    switch (currTxt.MbInterface)
                    {
                        case ModbusInterface.InputRegister:
                            mCurrTxtEditForm.radioButton3.Checked = true;
                            break;
                        case ModbusInterface.HoldingRegister:
                            mCurrTxtEditForm.radioButton4.Checked = true;
                            break;
                        default:
                            break;
                    }
                    mCurrTxtEditForm.cbbTxtVar.SelectedIndex = currTxt.RelateVar;

                    mCurrTxtEditForm.Show();
                    mCurrTxtEditForm.btnTxtSav.Click += new EventHandler(mCurrTxtEditForm_btnTxtSav_Click);                    
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
                currTxt.RelateVar = int.Parse(mCurrTxtEditForm.cbbTxtVar.SelectedIndex.ToString());//将该文本框关联的变量标号赋给该文本框属性
                currTxt.MbInterface = mCurrTxtEditForm.modbusInterface;  //变量通道
                currTxt.SlaveAddress = int.Parse(mCurrTxtEditForm.txtSlaveAddress.Text); //modbus slave 对应 id
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请完整设置从站地址、变量位置、变量通道。", "注意");  
                currTxt.Text = "";
                currTxt.RelateVar = 0;//将该文本框关联的变量标号赋给该文本框属性
            }

            mCurrTxtEditForm.Close();//保存完成后关闭
        }

        //右键菜单 删除文本框
        private void ItemDelTxt_Click(object sender, EventArgs e)
        {
            this.tabControl.SelectedTab.Controls.Remove(currTxt);
        }

        //取消光标显示
        public void txt_GotFocus(object sender, EventArgs e)
        {
            HideCaret((sender as TextBox).Handle);
        }
        #endregion 添加文本部分

        #region Lamp Options
        public void lamp_MouseDown(object sender, MouseEventArgs e)
        {
            if (GlobalVar.editFlag)
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
        }

        //drag to move
        public void lamp_MouseMove(object sender, MouseEventArgs e)
        {
            if (GlobalVar.editFlag)
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
        }

        //double click open edit form
        public void lamp_DoubleClick(object sender, EventArgs e)
        {
            if (GlobalVar.editFlag && sender is Lamp)
            {
                currLamp = new Lamp();
                if (sender is Lamp)
                {
                    currLamp = sender as Lamp;
                }
                mCurrLampEditForm = new LampEditForm();
                mCurrLampEditForm.tbLampX.Text = currLamp.Location.X.ToString();
                mCurrLampEditForm.tbLampY.Text = currLamp.Location.Y.ToString();
                mCurrLampEditForm.textBoxSlaveAddress.Text = currLamp.SlaveAddress.ToString();
                mCurrLampEditForm.cbLampVar.SelectedIndex = currLamp.RelateVar;
                mCurrLampEditForm.buttonOk.Click += new EventHandler(LampEditbuttonOk_Click);
                mCurrLampEditForm.Show();
            }
        }

        //保存当前指示灯属性
        void LampEditbuttonOk_Click(object sender, EventArgs e)
        {
            int posX = int.Parse(mCurrLampEditForm.tbLampX.Text);
            int posY = int.Parse(mCurrLampEditForm.tbLampY.Text);
            currLamp.Location = new Point(posX, posY);
            currLamp.SlaveAddress = int.Parse(mCurrLampEditForm.textBoxSlaveAddress.Text);
            currLamp.RelateVar = mCurrLampEditForm.cbLampVar.SelectedIndex;
            mCurrLampEditForm.Close();
        }

        private void DelLampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab.Controls.Remove(currLamp);
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
            mToolForm = new ToolForm(this);

            if ((IniFile.ReadIniData("MainForm", "Width", null, GlobalVar.sIniPath) != "") && (IniFile.ReadIniData("MainForm", "Height", null, GlobalVar.sIniPath) != "")
                &&(int.Parse(IniFile.ReadIniData("MainForm", "PosX", null, GlobalVar.sIniPath))!= 0)&&(int.Parse(IniFile.ReadIniData("MainForm", "PosY", null, GlobalVar.sIniPath))!=0))
            {
                //读取改变后的主窗体坐标和大小
                GlobalVar.nMainFormWidth = int.Parse(IniFile.ReadIniData("MainForm", "Width", null, GlobalVar.sIniPath));
                GlobalVar.nMainFormHeight = int.Parse(IniFile.ReadIniData("MainForm", "Height", null, GlobalVar.sIniPath));

                GlobalVar.nMainFormPosX = int.Parse(IniFile.ReadIniData("MainForm", "PosX", null, GlobalVar.sIniPath));
                GlobalVar.nMainFormPosY = int.Parse(IniFile.ReadIniData("MainForm", "PosY", null, GlobalVar.sIniPath));
            }

            if (false == GlobalVar.editFlag)
            {
                btnEdit.Enabled = false;//禁用编辑按钮
                btnStart.Enabled = false; //编辑时禁止启动检测
                btnSavEdit.Visible = true;
                btnEditStop.Visible = true;
                GlobalVar.editFlag = true;
                mToolForm.Location = new Point(GlobalVar.nMainFormPosX + GlobalVar.nMainFormWidth + 12,GlobalVar.nMainFormPosY);
                mToolForm.Show();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (GlobalVar.runningFlag == false)
            {
                this.btnEdit.Visible = false;
                this.btnSavEdit.Visible = false;
                this.btnConnectionList.Visible = true;
                this.timerRefesh.Enabled = true;
                try
                {
                    communicate.StartServer();
                    mConnectionForm = new ConnectionForm(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                GlobalVar.runningFlag = true;
                btnStart.BackColor = Color.Tomato;
                btnStart.Text = "停止";
            }
            else
            {
                this.btnEdit.Visible = true;
                this.btnConnectionList.Visible = false;
                this.timerRefesh.Enabled = false;
                try
                {
                    communicate.StopCommunication();
                    mConnectionForm.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                GlobalVar.runningFlag = false;
                btnStart.BackColor = Color.LightGreen;
                btnStart.Text = "启动";
            }
        }

        
       //显示提示信息
        private void SetStatusLable(string text)
        {
            if (this.InvokeRequired)
            {
                UpdateStatus delegateMethod = new UpdateStatus(SetStatusLable);
                this.Invoke(delegateMethod, new object[] { text });
            }
            else
                this.labelInfo.Text = text;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalVar.editFlag)
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
            }
            
            if (GlobalVar.runningFlag)
            {
                btnStart.PerformClick();
            }

            this.Dispose();  
        }

        /// <summary>
        /// 保存当前窗体控件信息到ini文件
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
            foreach (TabPage page in tabControl.TabPages)
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
                        IniFile.WriteIniData(TxtKnot, "SlaveAddress", ((TextBoxEx)ctrl).SlaveAddress.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(TxtKnot, "Interface", ((int)((ctrl as TextBoxEx).MbInterface)).ToString(), GlobalVar.sIniPath);
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
                        IniFile.WriteIniData(lampNot, "SlaveAddress", ((Lamp)ctrl).SlaveAddress.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(lampNot, "RelateVar", ((Lamp)ctrl).RelateVar.ToString(), GlobalVar.sIniPath);
                        IniFile.WriteIniData(lampNot, "Adscription", tabIndex.ToString(), GlobalVar.sIniPath);

                        lampIndex++;
                    }
                }
                IniFile.WriteIniData("TabPage" + tabIndex.ToString(), "Text", tabControl.TabPages[tabIndex - 1].Text, GlobalVar.sIniPath);

                tabIndex++;//标签页索引
            }

            IniFile.WriteIniData("TabProperty", "TabPageCount", this.tabControl.TabCount.ToString(), GlobalVar.sIniPath); //标签页总数
        }

        //Monitor connection
        private void btnConnectionList_Click(object sender, EventArgs e)
        {
            mConnectionForm.Location = new Point(GlobalVar.nMainFormPosX, GlobalVar.nMainFormPosY + GlobalVar.nMainFormHeight + 12);
            mConnectionForm.Show();
        }

        /// <summary>
        /// 刷新界面显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRefesh_Tick(object sender, EventArgs e)
        {
            for (int index = 0; index < tabControl.TabPages.Count; index++)
            {                
                foreach (Control ctrl in tabControl.TabPages[index].Controls)
                {
                    foreach (var module in communicate.moduleList)
                    {
                         if (ctrl is TextBoxEx) //Updata textbox data (number)
                         {
                            TextBoxEx textBox = ctrl as TextBoxEx;
                            if (textBox.SlaveAddress == module.slaveIndex)
                            {
                                if (textBox.MbInterface == ModbusInterface.HoldingRegister)
                                {
                                    textBox.Text = module.DataReadWrite[textBox.RelateVar].ToString();
                                }
                                if (textBox.MbInterface == ModbusInterface.InputRegister)
                                {
                                    textBox.Text = module.DataReadOnly[textBox.RelateVar].ToString();
                                }
                            }                            
                        }
                        
                        if (ctrl is Lamp) //Update lamp data (switch)
                        {
                            Lamp lamp = ctrl as Lamp;
                            if (lamp.SlaveAddress == module.slaveIndex)
                            {
                                lamp.onFlag = module.DataCoil[lamp.RelateVar];
                            }
                        }                    
                    }                                   
                }
            }            
        }

        /// <summary>
        /// 主界面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            communicate.updateStatus += new UpdateStatus(SetStatusLable);

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

            foreach (string str in sSectionName)
            {
                //加载tab控件
                if ("TabPage" + indexTab.ToString() == str)
                {
                    string tabText = IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Text", "New Tab");
                    TabPage tabPage = new TabPage(tabText);
                    tabPage.UseVisualStyleBackColor = true;
                    tabControl.TabPages.Add(tabPage);
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
                    lbl.BackColor = Color.Transparent;
                    lbl.TextAlign = ContentAlignment.MiddleRight;
                    lbl.DoubleClick += new EventHandler(lbl_DoubleClick);
                    lbl.MouseDown += new MouseEventHandler(lbl_MouseDown);
                    lbl.MouseMove += new MouseEventHandler(lbl_MouseMove);
                    this.tabControl.TabPages[TempAdscription - 1].Controls.Add(lbl);
                    indexLbl++;
                }
                #endregion

                #region 根据读取的TextBox个数加载TextBox控件
                if ("TextBox" + indexTxt.ToString() == str)
                {
                    string TempTextBoxID = IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "TextBoxID", "NoItem");
                    int tempTextBoxSlaveAddress = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "SlaveAddress", "NoItem"));  //从站地址
                    int TempTextBoxRelateVar = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "RelateVar", "NoItem"));        //变量地址
                    int TempTextBoxMBInterface = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Interface", "NoItem"));      //变量通道
                    int TempPosX = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosX", "NoItem"));
                    int TempPosY = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosY", "NoItem"));
                    int TempWidth = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Width", "NoItem"));
                    int TempHeight = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Height", "NoItem"));
                    int TempAdscription = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Adscription", "NoItem"));//所属标签页

                    TextBoxEx txt = new TextBoxEx();
                    txt.Location = new Point(TempPosX, TempPosY);
                    txt.Name = TempTextBoxID;
                    txt.Width = TempWidth;
                    txt.Height = TempHeight;
                    txt.TextAlign = HorizontalAlignment.Right;//文本内容位置靠右
                    txt.ReadOnly = true;
                    txt.Cursor = Cursors.Hand;
                    txt.BackColor = Color.White;
                    txt.SlaveAddress = tempTextBoxSlaveAddress;
                    txt.RelateVar = TempTextBoxRelateVar;
                    switch(TempTextBoxMBInterface)
                    {
                        case 3:
                            txt.MbInterface = ModbusInterface.InputRegister;
                            break;
                        case 4:
                            txt.MbInterface = ModbusInterface.HoldingRegister;
                            break;
                        default:
                            break;
                    }
                   
                    txt.Click += new EventHandler(txt_Click);
                    txt.DoubleClick += new EventHandler(txt_DoubleClick);
                    txt.MouseDown += new MouseEventHandler(txt_MouseDown);
                    txt.MouseMove += new MouseEventHandler(txt_MouseMove);
                    this.tabControl.TabPages[TempAdscription - 1].Controls.Add(txt);
                    indexTxt++;
                }
                #endregion

                #region 根据配置加载指示灯控件
                if ("Lamp" + indexLamp.ToString() == str)
                {
                    string tempLampID = IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "LampID", "NoItem");
                    int tempPosX = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosX", "NoItem"));
                    int tempPosY = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "PosY", "NoItem"));
                    int tempLampVar = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "RelateVar", "NoItem"));
                    int tempSlaveAddress = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "SlaveAddress", "NoItem"));
                    int tempAdscription = int.Parse(IniFile.INIGetStringValue(GlobalVar.sIniPath, str, "Adscription", "NoItem"));

                    Lamp lamp = new Lamp();
                    lamp.Location = new Point(tempPosX, tempPosY);
                    lamp.Name = tempLampID;
                    lamp.RelateVar = tempLampVar;
                    lamp.SlaveAddress = tempSlaveAddress;
                    
                    //lamp.Click += new EventHandler(lamp_Click);
                    lamp.DoubleClick += new EventHandler(lamp_DoubleClick);
                    lamp.MouseDown += new MouseEventHandler(lamp_MouseDown);
                    lamp.MouseMove += new MouseEventHandler(lamp_MouseMove);
                    this.tabControl.TabPages[tempAdscription - 1].Controls.Add(lamp);
                    indexLamp++;
                }
                #endregion
            }

            //显示配置文件中设定的主窗体大小、标题
            this.Width = GlobalVar.nMainFormWidth;
            this.Height = GlobalVar.nMainFormHeight;
            this.Text = GlobalVar.sMainFormTitle;
        }

        private void btnEditStop_Click(object sender, EventArgs e)
        {
            GlobalVar.editFlag = false;
            btnEdit.Enabled = true;//启用编辑按钮
            btnStart.Enabled = true; //启用启动按钮
            btnSavEdit.Visible = false; //隐藏保存按钮
            btnEditStop.Visible = false;
            mToolForm.Close();
        }
    }
}
