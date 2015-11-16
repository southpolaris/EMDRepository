using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace WifiMonitor
{
    public partial class MainForm : Form
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("user32", EntryPoint = "HideCaret")]
        static extern bool HideCaret(IntPtr hWnd);//用于隐藏TextBox的光标

        #region 参数定义
        //鼠标拖动标签时标签位置
        private Point lblStart;
        private Point lblLocat;

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

        private void mTitleChange_btnSave_Click(object sender, EventArgs e)
        {
            IniFile.WriteIniData("MainForm", "Title", mTitleChange.txtTitle.Text, GlobalVar.sIniPath);
            GlobalVar.sMainFormTitle = mTitleChange.txtTitle.Text;
            this.Text = mTitleChange.txtTitle.Text;
            mTitleChange.Close();
        }

        #region Label options

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
        public void Text_Click(object sender, EventArgs e)
        {
            HideCaret((sender as TextBoxEx).Handle);
            if (GlobalVar.runningFlag)
            {
                currTxt = new TextBoxEx();
                if (sender is TextBoxEx)
                {
                    currTxt = sender as TextBoxEx;
                    if (!currTxt.ReadOnly)
                    {
                        mTitleChange = new TitleChange();
                        mTitleChange.Text = "保持寄存器——数值量写入";
                        mTitleChange.txtTitle.Text = currTxt.Text;
                        mTitleChange.btnSave.Click += new EventHandler(WriteInt);
                            mTitleChange.lblTitle.Text = "写入32位整形：";
                        mTitleChange.ShowDialog();
                    }                    
                }                
            }
        }

        void WriteFloat(object sender, EventArgs e)
        {
            int slaveIndex = currTxt.SlaveAddress;
            float pendingWrite;
            if (Single.TryParse(mTitleChange.txtTitle.Text, out pendingWrite))
            {
                mTitleChange.Close();
                communicate.SendModbusData(currTxt.SlaveAddress, (ushort)(currTxt.RelateVar * 2 + 40), pendingWrite);
            }
            else
            {
                mTitleChange.Close();
                MessageBox.Show("输入的值无效");
            }
        }

        public void WriteInt(object sender, EventArgs e)
        {
            int slaveIndex = currTxt.SlaveAddress;
            int pendingWrite;
            if (Int32.TryParse(mTitleChange.txtTitle.Text, out pendingWrite))
            {
                mTitleChange.Close();
                communicate.SendModbusData(currTxt.SlaveAddress, (ushort)(currTxt.RelateVar * 2), pendingWrite);
            }
            else
            {
                mTitleChange.Close();
                MessageBox.Show("输入的值无效");
            }           
        }

        //鼠标按下
        public void Text_MouseDown(object sender, MouseEventArgs e)
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
        public void Text_MouseMove(object sender, MouseEventArgs e)
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
                    Point destiny = new Point(txtLocat.X + temp.X - txtStart.X, txtLocat.Y + temp.Y - txtStart.Y);
                    if (destiny.X >= 0 && destiny.X <= tabControl.Width && destiny.Y >= 0 && destiny.Y <= tabControl.Height)
                    {
                        txt.Location = destiny;
                    }                    
                }
            }           
        }

        //打开文本框编辑对话框
        public void Text_DoubleClick(object sender, EventArgs e)
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
            currTxt.Text = mCurrTxtEditForm.txtSlaveAddress.Text + "_";

            try
            {               
                currTxt.RelateVar = int.Parse(mCurrTxtEditForm.cbbTxtVar.SelectedIndex.ToString());//将该文本框关联的变量标号赋给该文本框属性
                currTxt.MbInterface = mCurrTxtEditForm.modbusInterface;  //变量通道
                currTxt.SlaveAddress = int.Parse(mCurrTxtEditForm.txtSlaveAddress.Text); //modbus slave 对应 id
            }
            catch (System.Exception)
            {
                MessageBox.Show("请完整设置从站地址、变量位置、变量通道和变量类型。", "注意");  
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
        public void Text_GotFocus(object sender, EventArgs e)
        {
            HideCaret((sender as TextBox).Handle);
        }
        #endregion 添加文本部分

        #region Lamp Options
        public void Lamp_Click(object sender, EventArgs e)
        {
            if (GlobalVar.runningFlag)
            {
                currLamp = new Lamp();
                if (sender is Lamp)
                {
                    currLamp = sender as Lamp;
                    if (!currLamp.ReadOnly)
                    {
                        currLamp.onFlag = !currLamp.onFlag;
                        int slaveIndex = currLamp.SlaveAddress;
                        UseWaitCursor = true;
                        communicate.SendModbusData(slaveIndex, (ushort)currLamp.RelateVar, currLamp.onFlag);
                        UseWaitCursor = false;
                    }
                }
            }
        }
        public void Lamp_MouseDown(object sender, MouseEventArgs e)
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
        public void Lamp_MouseMove(object sender, MouseEventArgs e)
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
        public void Lamp_DoubleClick(object sender, EventArgs e)
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
                mCurrLampEditForm.ReadOnly = currLamp.ReadOnly;
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
            currLamp.ReadOnly = mCurrLampEditForm.ReadOnly;
            mCurrLampEditForm.Close();
        }

        private void DelLampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab.Controls.Remove(currLamp);
        }
        #endregion

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            GlobalVar.nMainFormHeight = this.Height;
            GlobalVar.nMainFormWidth = this.Width;
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            GlobalVar.nMainFormPosX = this.Location.X;
            GlobalVar.nMainFormPosY = this.Location.Y;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            mToolForm = new ToolForm(this);

            //读取改变后的主窗体坐标和大小
            GlobalVar.nMainFormWidth = this.Width;
            GlobalVar.nMainFormHeight = this.Height;
            GlobalVar.nMainFormPosX = this.Location.X;
            GlobalVar.nMainFormPosY = this.Location.Y;

            if (false == GlobalVar.editFlag)
            {
                btnEdit.Enabled = false;//禁用编辑按钮
                btnStart.Enabled = false; //编辑时禁止启动检测
                btnSavEdit.Visible = true;
                btnEditStop.Visible = true;
                GlobalVar.editFlag = true;
                mToolForm.Location = new Point(GlobalVar.nMainFormPosX + GlobalVar.nMainFormWidth + 2, GlobalVar.nMainFormPosY);
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
                timerRefresh.Start();
                try
                {
                    mConnectionForm = new ConnectionForm(this);
                    communicate.StartServer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                GlobalVar.runningFlag = true;
                btnStart.BackColor = Color.Tomato;
                btnStart.Text = "■ 停止";
            }
            else
            {
                this.btnEdit.Visible = true;
                this.btnConnectionList.Visible = false;
                timerRefresh.Stop();
                try
                {
                    communicate.Dispose();
                    mConnectionForm.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                GlobalVar.runningFlag = false;
                btnStart.BackColor = Color.LightGreen;
                btnStart.Text = " 启动";
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
        /// 保存当前窗体控件信息到xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavEdit_Click(object sender, EventArgs e)
        {
            int tabIndex = 1;


            //遍历INI文件中所有lbl节名
            string[] sSectionName = IniFile.INIGetAllSectionNames(GlobalVar.sIniPath);
            foreach (string str in sSectionName) //删除所有空间信息
            {
                if (str.Contains("Label") || str.Contains("TextBox") || str.Contains("Lamp") || str.Contains("TabPage"))
                {
                    IniFile.INIDeleteSection(GlobalVar.sIniPath, str);
                }
            }
            //清除原有信息
            XDocument xDoc = XDocument.Load(GlobalVar.xmlPath);
            XElement controlElement = xDoc.Root.Element("Tabpages");
            xDoc.Root.Element("MainForm").RemoveAll();
            controlElement.RemoveAll();

            //重新写入控件信息
            foreach (TabPage page in tabControl.TabPages)
            {
                controlElement.Add(new XElement("Tabpage" + tabIndex.ToString(), new XAttribute("Text", page.Text),
                    new XElement("Labels"),
                    new XElement("TextBoxes"), 
                    new XElement("Lamps")));

                XElement labelElement = controlElement.Element("Tabpage" + tabIndex.ToString()).Element("Labels");
                XElement textElement = controlElement.Element("Tabpage" + tabIndex.ToString()).Element("TextBoxes");
                XElement lampElement = controlElement.Element("Tabpage" + tabIndex.ToString()).Element("Lamps");

                int labelIndex = 1;
                int txtIndex = 1;
                int lampIndex = 1;

                foreach (Control ctrl in page.Controls)
                {
                    if (ctrl is Label)
                    {
                        string lblKnot = "";
                        lblKnot = "Label" + tabIndex.ToString() + "_" + labelIndex.ToString();//节名 
                        ctrl.Name = lblKnot;//重新分配标签标识  以节名命名
                        labelElement.Add(new XElement(lblKnot, 
                            new XAttribute("ID", ctrl.Name),
                            new XAttribute("Text", ctrl.Text),
                            new XAttribute("PosX", ctrl.Location.X.ToString()),
                            new XAttribute("PosY", ctrl.Location.Y.ToString()),
                            new XAttribute("Width", ctrl.Width.ToString()),
                            new XAttribute("Height", ctrl.Height.ToString()) ));

                        labelIndex++;
                    }
                    if (ctrl is TextBoxEx)
                    {
                        string TxtKnot = "";
                        TxtKnot = "TextBox" + tabIndex.ToString() + "_" + txtIndex.ToString();//节名 
                        ctrl.Name = TxtKnot;//重新分配标识  以节名命名
                        textElement.Add(new XElement(TxtKnot,
                            new XAttribute("ID", ctrl.Name),
                            new XElement("RelateVar", (ctrl as TextBoxEx).RelateVar.ToString()),
                            new XElement("SlaveAddress", (ctrl as TextBoxEx).SlaveAddress.ToString()),
                            new XElement("Interface", ((int)((ctrl as TextBoxEx).MbInterface)).ToString()),
                            new XAttribute("PosX", ctrl.Location.X.ToString()),
                            new XAttribute("PosY", ctrl.Location.Y.ToString()),
                            new XAttribute("Width", ctrl.Width.ToString()),
                            new XAttribute("Height", ctrl.Height.ToString())
                           ));

                        txtIndex++;
                    }
                    if (ctrl is Lamp)
                    {
                        string lampNot = "";
                        currLamp = new Lamp();

                        lampNot = "Lamp" + tabIndex.ToString() + "_" + lampIndex.ToString();
                        ctrl.Name = lampNot;

                        lampElement.Add(new XElement(lampNot,
                            new XAttribute("ID", ctrl.Name),
                            new XElement("RelateVar", (ctrl as Lamp).RelateVar.ToString()),
                            new XElement("SlaveAddress", (ctrl as Lamp).SlaveAddress.ToString()),
                            new XElement("Interface", ((Lamp)ctrl).ReadOnly == true ? "1" : "0"),
                            new XAttribute("PosX", ctrl.Location.X.ToString()),
                            new XAttribute("PosY", ctrl.Location.Y.ToString())
                            ));
                        
                        lampIndex++;
                    }
                }
                labelElement.Add(new XAttribute("Count", labelIndex - 1));
                textElement.Add(new XAttribute("Count", txtIndex - 1));
                lampElement.Add(new XAttribute("Count", lampIndex - 1));

                tabIndex++;//标签页索引
            }
            controlElement.Add(new XAttribute("Count", tabControl.TabPages.Count));

            xDoc.Root.Element("MainForm").Add(new XAttribute("Text", this.Text),
                new XAttribute("PosX", this.Location.X.ToString()),
                new XAttribute("PosY", this.Location.Y.ToString()),
                new XAttribute("Width", this.Width.ToString()),
                new XAttribute("Height", this.Height.ToString()));
            xDoc.Root.Save(GlobalVar.xmlPath);
        }

        //Monitor connection
        private void btnConnectionList_Click(object sender, EventArgs e)
        {
            mConnectionForm.Location = new Point(GlobalVar.nMainFormPosX, GlobalVar.nMainFormPosY + GlobalVar.nMainFormHeight + 12);
            mConnectionForm.Show();
        }

        /// <summary>
        /// 主界面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            communicate.updateStatus += new UpdateStatus(SetStatusLable);

            FileInfo fileInfo = new FileInfo(GlobalVar.xmlPath);
            if (!fileInfo.Exists)
            {
                XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Root", 
                    new XElement("Tabpages"), new XElement("MainForm")));
                xDoc.Save(GlobalVar.xmlPath);
            }

            //遍历所有节名  用于加载控件
            string[] sSectionName = IniFile.INIGetAllSectionNames(GlobalVar.sIniPath);
            XElement tabElement = XDocument.Load(GlobalVar.xmlPath).Root.Element("Tabpages");
            XElement formElement = XDocument.Load(GlobalVar.xmlPath).Root.Element("MainForm");

            #region Load controls
            try
            {
                for (int tabIndex = 1; tabIndex <= int.Parse(tabElement.Attribute("Count").Value); tabIndex++)
                {
                    XElement tempTabElement = tabElement.Element("Tabpage" + tabIndex.ToString());
                    string tabText = tempTabElement.Attribute("Text").Value;
                    TabPage tabPage = new TabPage(tabText);
                    tabPage.UseVisualStyleBackColor = true;
                    tabControl.TabPages.Add(tabPage);
                    try
                    {
                        for (int labelIndex = 1; labelIndex <= int.Parse(tempTabElement.Element("Labels").Attribute("Count").Value); labelIndex++)
                        {
                            XElement tempLabelElement = tempTabElement.Element("Labels").Element("Label" + tabIndex.ToString() + "_" + labelIndex.ToString());
                            string TempLabelID = tempLabelElement.Attribute("ID").Value;
                            string TempLabelText = tempLabelElement.Attribute("Text").Value;
                            int TempPosX = int.Parse(tempLabelElement.Attribute("PosX").Value);
                            int TempPosY = int.Parse(tempLabelElement.Attribute("PosY").Value);
                            int TempWidth = int.Parse(tempLabelElement.Attribute("Width").Value);
                            int TempHeight = int.Parse(tempLabelElement.Attribute("Height").Value);

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
                            this.tabControl.TabPages[tabIndex - 1].Controls.Add(lbl);
                        }
                    }
                    catch (Exception)
                    {
                        ;
                    }
                    try
                    {
                        for (int textIndex = 1; textIndex <= int.Parse(tempTabElement.Element("TextBoxes").Attribute("Count").Value); textIndex++)
                        {
                            XElement tempTextElement = tempTabElement.Element("TextBoxes").Element("TextBox" + tabIndex.ToString() + "_" + textIndex.ToString());
                            string TempTextBoxID = tempTextElement.Attribute("ID").Value;
                            int TempPosX = int.Parse(tempTextElement.Attribute("PosX").Value);
                            int TempPosY = int.Parse(tempTextElement.Attribute("PosY").Value);
                            int TempWidth = int.Parse(tempTextElement.Attribute("Width").Value);
                            int TempHeight = int.Parse(tempTextElement.Attribute("Height").Value);
                            int tempTextBoxSlaveAddress = int.Parse(tempTextElement.Element("SlaveAddress").Value);  //从站地址
                            int TempTextBoxRelateVar = int.Parse(tempTextElement.Element("RelateVar").Value);        //变量地址
                            int TempTextBoxMBInterface = int.Parse(tempTextElement.Element("Interface").Value);      //变量通道

                            TextBoxEx txt = new TextBoxEx();
                            txt.Location = new Point(TempPosX, TempPosY);
                            txt.Name = TempTextBoxID;
                            txt.Width = TempWidth;
                            txt.Height = TempHeight;
                            txt.TextAlign = HorizontalAlignment.Right;//文本内容位置靠右
                            txt.ReadOnly = true;
                            txt.BackColor = Color.White;
                            txt.SlaveAddress = tempTextBoxSlaveAddress;
                            txt.RelateVar = TempTextBoxRelateVar;
                            switch (TempTextBoxMBInterface)
                            {
                                case 3:
                                    txt.MbInterface = ModbusInterface.InputRegister;
                                    break;
                                case 4:
                                    txt.MbInterface = ModbusInterface.HoldingRegister;
                                    txt.Cursor = Cursors.Hand;
                                    break;
                                default:
                                    break;
                            }

                            txt.Click += new EventHandler(Text_Click);
                            txt.DoubleClick += new EventHandler(Text_DoubleClick);
                            txt.MouseDown += new MouseEventHandler(Text_MouseDown);
                            txt.MouseMove += new MouseEventHandler(Text_MouseMove);
                            this.tabControl.TabPages[tabIndex - 1].Controls.Add(txt);
                        }
                    }
                    catch (Exception)
                    {
                        ;
                    }
                    try
                    {
                        for (int lampIndex = 1; lampIndex <= int.Parse(tempTabElement.Element("Lamps").Attribute("Count").Value); lampIndex++)
                        {
                            XElement tempLampElement = tempTabElement.Element("Lamps").Element("Lamp" + tabIndex.ToString() + "_" + lampIndex.ToString());
                            string tempLampID = tempLampElement.Attribute("ID").Value;
                            int tempPosX = int.Parse(tempLampElement.Attribute("PosX").Value);
                            int tempPosY = int.Parse(tempLampElement.Attribute("PosY").Value);
                            int tempLampVar = int.Parse(tempLampElement.Element("RelateVar").Value);
                            int tempSlaveAddress = int.Parse(tempLampElement.Element("SlaveAddress").Value);
                            int tempInterface = int.Parse(tempLampElement.Element("Interface").Value);

                            Lamp lamp = new Lamp();
                            lamp.Location = new Point(tempPosX, tempPosY);
                            lamp.Name = tempLampID;
                            lamp.RelateVar = tempLampVar;
                            lamp.SlaveAddress = tempSlaveAddress;
                            if (tempInterface == 1)
                            {
                                lamp.ReadOnly = true;
                            }
                            else
                            {
                                lamp.ReadOnly = false;
                                lamp.Cursor = Cursors.Hand;
                            }

                            lamp.Click += new EventHandler(Lamp_Click);
                            lamp.DoubleClick += new EventHandler(Lamp_DoubleClick);
                            lamp.MouseDown += new MouseEventHandler(Lamp_MouseDown);
                            lamp.MouseMove += new MouseEventHandler(Lamp_MouseMove);
                            this.tabControl.TabPages[tabIndex - 1].Controls.Add(lamp);
                        }
                    }
                    catch (Exception)
                    {
                        ;
                    }
                }

                //读取主窗体大小、标题
                GlobalVar.nMainFormWidth = int.Parse(formElement.Attribute("Width").Value);
                GlobalVar.nMainFormHeight = int.Parse(formElement.Attribute("Height").Value);
                GlobalVar.sMainFormTitle = formElement.Attribute("Text").Value;
            }
            catch (Exception)
            {
                ;
            }
            #endregion

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

        /// <summary>
        /// Refersh user interface display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            foreach (Control ctrl in tabControl.SelectedTab.Controls)
            {
                for (int i = 0; i < communicate.moduleList.Count; i++)
                {
                    ModbusSlave module = communicate.moduleList[i];
                    if (ctrl is TextBoxEx)
                    {
                        TextBoxEx textBox = ctrl as TextBoxEx;
                        if (textBox.SlaveAddress == module.slaveIP)
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
                    else if (ctrl is Lamp)
                    {
                        Lamp lamp = ctrl as Lamp;
                        if (lamp.SlaveAddress == module.slaveIP)
                        {
                            if (!lamp.ReadOnly)
                                lamp.onFlag = module.DataCoil[lamp.RelateVar];
                            else
                                lamp.onFlag = module.DataDiscreteInput[lamp.RelateVar];
                        }
                    }
                }
            }
        }
    }
}
