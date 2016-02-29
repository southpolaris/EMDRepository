using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;

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
        internal DataBase dataBase = new DataBase();
        #endregion

        public delegate void UpdateStatus(string str);//更改界面状态显示
        public delegate void LostConnectionEventHandler(int slaveIndex);

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
                    mCurrLblEditForm.btnLblSav.Click += new EventHandler(mCurrLblEditForm_btnLblSav_Click);
                    mCurrLblEditForm.ShowDialog();
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
                        mTitleChange.Text = currTxt.VarName;
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
            IPAddress slaveIndex = currTxt.SlaveAddress;
            float pendingWrite;
            if (Single.TryParse(mTitleChange.txtTitle.Text, out pendingWrite))
            {
                mTitleChange.Close();
                //communicate.SendModbusData(currTxt.SlaveAddress, (ushort)(currTxt.RelateVar * 2 + 40), pendingWrite);
            }
            else
            {
                mTitleChange.Close();
                MessageBox.Show("输入的值无效");
            }
        }

        public void WriteInt(object sender, EventArgs e)
        {
            IPAddress slaveIndex = currTxt.SlaveAddress;
            int pendingWrite;
            if (Int32.TryParse(mTitleChange.txtTitle.Text, out pendingWrite))
            {
                mTitleChange.Close();
                communicate.SendSingleValue(slaveIndex, (ushort)currTxt.RelateVar, pendingWrite);
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
                    mCurrTxtEditForm.textBoxVarName.Text = currTxt.VarName;
                    mCurrTxtEditForm.txtWidth.Text = currTxt.Width.ToString();
                    mCurrTxtEditForm.txtHeight.Text = currTxt.Height.ToString();
                    mCurrTxtEditForm.txtPosX.Text = currTxt.Location.X.ToString();
                    mCurrTxtEditForm.txtPosY.Text = currTxt.Location.Y.ToString();                    
                    foreach(IPAddress ip in GlobalVar.ipNodeMapping.Keys)
                    {
                        mCurrTxtEditForm.cbSlaveAddress.Items.Add(ip);
                    }
                    mCurrTxtEditForm.cbSlaveAddress.Text = currTxt.SlaveAddress.ToString();
                    switch (currTxt.MbInterface)
                    {
                        case DataInterface.InputRegister:
                            mCurrTxtEditForm.radioButton3.Checked = true;
                            break;
                        case DataInterface.HoldingRegister:
                            mCurrTxtEditForm.radioButton4.Checked = true;
                            break;
                        default:
                            break;
                    }
                    
                    mCurrTxtEditForm.textBoxRelateVar.Text = currTxt.RelateVar.ToString();
                    mCurrTxtEditForm.textBoxDataSave.Text = GlobalVar.strDataSave[(int)currTxt.mInDataBase];
                    mCurrTxtEditForm.btnTxtSav.Click += new EventHandler(mCurrTxtEditForm_btnTxtSav_Click);
                    mCurrTxtEditForm.ShowDialog();
                }
            }            
        }

        //保存当前编辑文本框的属性
        public void mCurrTxtEditForm_btnTxtSav_Click(object sender, EventArgs e)//标签编辑对话框保存按钮事件
        {
            currTxt.Width = int.Parse(mCurrTxtEditForm.txtWidth.Text);
            currTxt.Height = int.Parse(mCurrTxtEditForm.txtHeight.Text);
            currTxt.VarName = mCurrTxtEditForm.textBoxVarName.Text;

            try
            {
                currTxt.RelateVar = int.Parse(mCurrTxtEditForm.textBoxRelateVar.Text);//将该文本框关联的变量标号赋给该文本框属性
                currTxt.MbInterface = mCurrTxtEditForm.modbusInterface;  //变量通道
                currTxt.SlaveAddress = IPAddress.Parse(mCurrTxtEditForm.cbSlaveAddress.Text); //modbus slave 对应 id
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
                        IPAddress slaveIP = currLamp.SlaveAddress;
                        UseWaitCursor = true;
                        //write data
                        communicate.SendSingleValue(slaveIP, (ushort)currLamp.RelateVar, currLamp.onFlag);
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
                mCurrLampEditForm.textBoxVarName.Text = currLamp.mName;
                mCurrLampEditForm.tbLampX.Text = currLamp.Location.X.ToString();
                mCurrLampEditForm.tbLampY.Text = currLamp.Location.Y.ToString();
                foreach(IPAddress ip in GlobalVar.ipNodeMapping.Keys)
                {
                    mCurrLampEditForm.cbSlaveAddress.Items.Add(ip);
                }
                mCurrLampEditForm.cbSlaveAddress.Text = currLamp.SlaveAddress.ToString();
                mCurrLampEditForm.textBoxLampVar.Text = currLamp.RelateVar.ToString();
                mCurrLampEditForm.ReadOnly = currLamp.ReadOnly;
                mCurrLampEditForm.textBoxDataSave.Text = GlobalVar.strDataSave[(int)currLamp.mInDataBase];
                mCurrLampEditForm.buttonOk.Click += new EventHandler(LampEditbuttonOk_Click);
                mCurrLampEditForm.ShowDialog();
            }
        }

        //保存当前指示灯属性
        void LampEditbuttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                int posX = int.Parse(mCurrLampEditForm.tbLampX.Text);
                int posY = int.Parse(mCurrLampEditForm.tbLampY.Text);
                currLamp.Location = new Point(posX, posY);
                currLamp.SlaveAddress = IPAddress.Parse(mCurrLampEditForm.cbSlaveAddress.Text);
                currLamp.RelateVar = int.Parse(mCurrLampEditForm.textBoxLampVar.Text);
                currLamp.ReadOnly = mCurrLampEditForm.ReadOnly;
                currLamp.mName = mCurrLampEditForm.textBoxVarName.Text;
                mCurrLampEditForm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存时发生错误");
            }
            
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
                btnStart.BackgroundImage = null;
                btnStart.BackgroundImage = Properties.Resources.Reset;
                btnStart.ImageAlign = ContentAlignment.MiddleLeft;
                btnStart.BackgroundImageLayout = ImageLayout.None;
                btnStart.Text = "停止";
                btnStart.TextAlign = ContentAlignment.MiddleRight;
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
                btnStart.TextAlign = ContentAlignment.MiddleRight;
                btnStart.BackgroundImage = Properties.Resources.Start;
                btnStart.ImageAlign = ContentAlignment.MiddleLeft;
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
        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            int tabIndex = 1;

            //清除原有信息
            XDocument xDoc = XDocument.Load(GlobalVar.xmlPath);
            XElement controlElement = xDoc.Root.Element("MainForm");
            xDoc.Root.Element("MainForm").RemoveAll();
            xDoc.Root.Element("IPMapping").RemoveAll();
            controlElement.RemoveAll();

            //重新写入控件信息
            foreach (TabPage page in tabControl.TabPages)
            {
                controlElement.Add(new XElement("Tabpage" + tabIndex.ToString(), new XAttribute("Text", page.Text), new XAttribute("Tip", page.ToolTipText),
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
                        labelElement.Add(new XElement("Label", 
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
                        textElement.Add(new XElement("TextBox",
                            new XAttribute("ID", ctrl.Name),
                            new XElement("RelateVar", (ctrl as TextBoxEx).RelateVar.ToString()),
                            new XElement("SlaveAddress", (ctrl as TextBoxEx).SlaveAddress.ToString()),
                            new XElement("Interface", ((int)((ctrl as TextBoxEx).MbInterface)).ToString()),
                            new XElement("IsInDataBase", (ctrl as TextBoxEx).mInDataBase),
                            new XElement("VarName", (ctrl as TextBoxEx).VarName),
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

                        lampElement.Add(new XElement("Lamp",
                            new XAttribute("ID", ctrl.Name),
                            new XElement("RelateVar", (ctrl as Lamp).RelateVar.ToString()),
                            new XElement("SlaveAddress", (ctrl as Lamp).SlaveAddress.ToString()),
                            new XElement("Interface", ((Lamp)ctrl).ReadOnly == true ? "1" : "0"),
                            new XElement("IsInDataBase", (ctrl as Lamp).mInDataBase),
                            new XElement("VarName", (ctrl as Lamp).mName),
                            new XAttribute("PosX", ctrl.Location.X.ToString()),
                            new XAttribute("PosY", ctrl.Location.Y.ToString())
                            ));
                        
                        lampIndex++;
                    }
                }

                tabIndex++;//标签页索引
            }
            controlElement.Add(new XAttribute("Count", tabControl.TabPages.Count));

            xDoc.Root.Element("MainForm").Add(new XAttribute("Text", this.Text),
                new XAttribute("PosX", this.Location.X.ToString()),
                new XAttribute("PosY", this.Location.Y.ToString()),
                new XAttribute("Width", this.Width.ToString()),
                new XAttribute("Height", this.Height.ToString()));

            //Save ip slave mapping
            foreach(KeyValuePair<IPAddress, RemoteNode> ipMapping in GlobalVar.ipNodeMapping)
            {
                xDoc.Root.Element("IPMapping").Add(new XElement("Node",
                    new XAttribute("IP", ipMapping.Key.ToString()),
                    new XElement("Protocol", ipMapping.Value.protocolType),
                    new XElement("Name", ipMapping.Value.name),
                    new XElement("DataLength",
                        new XAttribute("DiscreteInput", ipMapping.Value.dataCount.discreteInput.ToString()),
                        new XAttribute("DiscreteOutput", ipMapping.Value.dataCount.coil.ToString()),
                        new XAttribute("InputRegister", ipMapping.Value.dataCount.inputRegister.ToString()),
                        new XAttribute("HoldingRegister", ipMapping.Value.dataCount.holdingRegiter.ToString())
                        )));
            }

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
            communicate.insertDataBase += new DataBaseEventHandler(dataBase.InsertNewRecord);

            LoadControls(GlobalVar.xmlPath);
            LoadVariables(GlobalVar.xmlPath);
            DataBaseInitial();

            //显示配置文件中设定的主窗体大小、标题
            this.Width = GlobalVar.nMainFormWidth;
            this.Height = GlobalVar.nMainFormHeight;
            this.Text = GlobalVar.sMainFormTitle;
        }

        //加载界面控件信息
        private void LoadControls(string xmlPath)
        {
            FileInfo fileInfo = new FileInfo(xmlPath);
            if (!fileInfo.Exists)
            {
                XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Root",
                    new XElement("MainForm", new XAttribute("Count", 0)), new XElement("IPMaping")));
                xDoc.Save(xmlPath);
            }

            //遍历所有节名  用于加载控件
            XElement formElement = XDocument.Load(xmlPath).Root.Element("MainForm");

            try
            {
                for (int tabIndex = 1; tabIndex <= int.Parse(formElement.Attribute("Count").Value); tabIndex++)
                {
                    XElement tempTabElement = formElement.Element("Tabpage" + tabIndex.ToString());
                    string tabText = tempTabElement.Attribute("Text").Value;
                    TabPage tabPage = new TabPage(tabText);
                    tabPage.UseVisualStyleBackColor = true;
                    tabPage.ToolTipText = tempTabElement.Attribute("Tip").Value;
                    tabControl.TabPages.Add(tabPage);
                    try
                    {
                        IEnumerable<XElement> labelList = from el in tempTabElement.Element("Labels").Elements() select el;
                        
                        foreach (XElement labelElement in labelList)
                        {
                            string TempLabelID = labelElement.Attribute("ID").Value;
                            string TempLabelText = labelElement.Attribute("Text").Value;
                            int TempPosX = int.Parse(labelElement.Attribute("PosX").Value);
                            int TempPosY = int.Parse(labelElement.Attribute("PosY").Value);
                            int TempWidth = int.Parse(labelElement.Attribute("Width").Value);
                            int TempHeight = int.Parse(labelElement.Attribute("Height").Value);

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
                        MessageBox.Show("加载标签文本控件错误", "错误",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    try
                    {
                        IEnumerable<XElement> textBoxList = from el in tempTabElement.Element("TextBoxes").Elements() select el;

                        foreach (XElement textBoxElement in textBoxList)
                        {
                            string TempTextBoxID = textBoxElement.Attribute("ID").Value;
                            int TempPosX = int.Parse(textBoxElement.Attribute("PosX").Value);
                            int TempPosY = int.Parse(textBoxElement.Attribute("PosY").Value);
                            int TempWidth = int.Parse(textBoxElement.Attribute("Width").Value);
                            int TempHeight = int.Parse(textBoxElement.Attribute("Height").Value);
                            IPAddress tempTextBoxSlaveAddress = IPAddress.Parse(textBoxElement.Element("SlaveAddress").Value);  //从站地址
                            int TempTextBoxRelateVar = int.Parse(textBoxElement.Element("RelateVar").Value);        //变量地址
                            int TempTextBoxMBInterface = int.Parse(textBoxElement.Element("Interface").Value);      //变量通道
                            string tempVarName = textBoxElement.Element("VarName").Value;                           //变量名称                            

                            TextBoxEx txt = new TextBoxEx();
                            if (Enum.IsDefined(typeof(DataSave), textBoxElement.Element("IsInDataBase").Value))
                            {
                                txt.mInDataBase = (DataSave)Enum.Parse(typeof(DataSave), textBoxElement.Element("IsInDataBase").Value);//数据保存形式
                            }
                            else
                                throw new Exception("Data base not defined!");
                            txt.Location = new Point(TempPosX, TempPosY);
                            txt.Name = TempTextBoxID;
                            txt.Width = TempWidth;
                            txt.Height = TempHeight;
                            txt.TextAlign = HorizontalAlignment.Right;//文本内容位置靠右
                            txt.ReadOnly = true;
                            txt.BackColor = Color.White;
                            txt.SlaveAddress = tempTextBoxSlaveAddress;
                            txt.RelateVar = TempTextBoxRelateVar;
                            txt.VarName = tempVarName;
                            switch (TempTextBoxMBInterface)
                            {
                                case 3:
                                    txt.MbInterface = DataInterface.InputRegister;
                                    break;
                                case 4:
                                    txt.MbInterface = DataInterface.HoldingRegister;
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
                        MessageBox.Show("加载数值显示控件错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    try
                    {
                        IEnumerable<XElement> lampList = from el in tempTabElement.Element("Lamps").Elements() select el;

                        foreach(XElement lampElement in lampList)
                        {
                            string tempLampID = lampElement.Attribute("ID").Value;
                            int tempPosX = int.Parse(lampElement.Attribute("PosX").Value);
                            int tempPosY = int.Parse(lampElement.Attribute("PosY").Value);
                            int tempLampVar = int.Parse(lampElement.Element("RelateVar").Value);
                            IPAddress tempSlaveAddress = IPAddress.Parse(lampElement.Element("SlaveAddress").Value);
                            int tempInterface = int.Parse(lampElement.Element("Interface").Value);
                            string tempVarName = lampElement.Element("VarName").Value;

                            Lamp lamp = new Lamp();
                            if (Enum.IsDefined(typeof(DataSave), lampElement.Element("IsInDataBase").Value))
                            {
                                lamp.mInDataBase = (DataSave)Enum.Parse(typeof(DataSave), lampElement.Element("IsInDataBase").Value);//数据保存形式
                            }
                            else
                                throw new Exception("Data base not defined!");
                            lamp.Location = new Point(tempPosX, tempPosY);
                            lamp.Name = tempLampID;
                            lamp.RelateVar = tempLampVar;
                            lamp.SlaveAddress = tempSlaveAddress;
                            lamp.mName = tempVarName;
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
                        MessageBox.Show("加载开关显示控件错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //读取主窗体大小、标题
                GlobalVar.nMainFormWidth = int.Parse(formElement.Attribute("Width").Value);
                GlobalVar.nMainFormHeight = int.Parse(formElement.Attribute("Height").Value);
                GlobalVar.sMainFormTitle = formElement.Attribute("Text").Value;
            }
            catch (Exception)
            {
                return;
            }
        }

        //加载变量信息
        private void LoadVariables(string xmlPath)
        {
            //遍历所有节名  用于加载控件
            XElement formElement = XDocument.Load(xmlPath).Root.Element("MainForm");
            XElement nodeElement = XDocument.Load(xmlPath).Root.Element("IPMapping");

            try
            {
                IEnumerable<XElement> childNodeList = from el in nodeElement.Elements() select el;

                foreach (XElement childNode in childNodeList)
                {
                    DataCount dataCount = new DataCount();
                    string protocolType = childNode.Element("Protocol").Value;
                    string nodeName = childNode.Element("Name").Value;
                    dataCount.discreteInput = (ushort)(UInt16.Parse(childNode.Element("DataLength").Attribute("DiscreteInput").Value) + 1);
                    dataCount.coil = (ushort)(UInt16.Parse(childNode.Element("DataLength").Attribute("DiscreteOutput").Value) + 1);
                    dataCount.inputRegister = (ushort)(UInt16.Parse(childNode.Element("DataLength").Attribute("InputRegister").Value) + 1);
                    dataCount.holdingRegiter = (ushort)(UInt16.Parse(childNode.Element("DataLength").Attribute("HoldingRegister").Value) + 1);
                    RemoteNode tempNode = new RemoteNode(protocolType, dataCount, nodeName);
                    GlobalVar.ipNodeMapping.Add(IPAddress.Parse(childNode.Attribute("IP").Value), tempNode);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("监控终端节点配置错误", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            for (int tabIndex = 1; tabIndex <= int.Parse(formElement.Attribute("Count").Value); tabIndex++)
            {
                XElement tempTabElement = formElement.Element("Tabpage" + tabIndex.ToString());

                try
                {
                    IEnumerable<XElement> textBoxList = from el in tempTabElement.Element("TextBoxes").Elements() select el;

                    foreach (XElement textBoxElement in textBoxList)
                    {
                        IPAddress tempVarSlaveAddress = IPAddress.Parse(textBoxElement.Element("SlaveAddress").Value);  //从站地址
                        int tempVarRelateVar = int.Parse(textBoxElement.Element("RelateVar").Value);        //变量地址
                        int tempVarMBInterface = int.Parse(textBoxElement.Element("Interface").Value);      //变量通道
                        string tempVarName = textBoxElement.Element("VarName").Value;                       //变量名称
                        DataSave tempVarInDataBase = DataSave.DoNotSave;
                        if (Enum.IsDefined(typeof(DataSave), textBoxElement.Element("IsInDataBase").Value))
                        {
                            tempVarInDataBase = (DataSave)Enum.Parse(typeof(DataSave), textBoxElement.Element("IsInDataBase").Value);//数据保存形式
                        }
                        else
                            throw new Exception("Data base not defined!");

                        switch (tempVarMBInterface)
                        {
                            case 3:
                                GlobalVar.ipNodeMapping[tempVarSlaveAddress].varInputRegister[tempVarRelateVar].varName = tempVarName;
                                GlobalVar.ipNodeMapping[tempVarSlaveAddress].varInputRegister[tempVarRelateVar].inDataBase = tempVarInDataBase;
                                break;
                            case 4:
                                GlobalVar.ipNodeMapping[tempVarSlaveAddress].varHoldingRegister[tempVarRelateVar].varName = tempVarName;
                                GlobalVar.ipNodeMapping[tempVarSlaveAddress].varHoldingRegister[tempVarRelateVar].inDataBase = tempVarInDataBase;
                                break;
                            default:
                                break;
                        }   
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "加载数值变量信息错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                try
                {
                    IEnumerable<XElement> lampList = from el in tempTabElement.Element("Lamps").Elements() select el;

                    foreach (XElement lampElement in lampList)
                    {
                        int tempLampVar = int.Parse(lampElement.Element("RelateVar").Value);
                        IPAddress tempSlaveAddress = IPAddress.Parse(lampElement.Element("SlaveAddress").Value);
                        int tempInterface = int.Parse(lampElement.Element("Interface").Value);
                        string tempVarName = lampElement.Element("VarName").Value;
                        DataSave tempVarInDataBase = DataSave.DoNotSave;
                        if (Enum.IsDefined(typeof(DataSave), lampElement.Element("IsInDataBase").Value))
                        {
                            tempVarInDataBase = (DataSave)Enum.Parse(typeof(DataSave), lampElement.Element("IsInDataBase").Value);//数据保存形式
                        }
                        else
                            throw new Exception("Data base not defined!");

                        if (tempInterface == 1)
                        {
                            GlobalVar.ipNodeMapping[tempSlaveAddress].varDiscreteInput[tempLampVar].varName = tempVarName;
                            GlobalVar.ipNodeMapping[tempSlaveAddress].varDiscreteInput[tempLampVar].inDataBase = tempVarInDataBase;
                        }
                        else
                        {
                            GlobalVar.ipNodeMapping[tempSlaveAddress].varDiscreteOutput[tempLampVar].varName = tempVarName;
                            GlobalVar.ipNodeMapping[tempSlaveAddress].varDiscreteOutput[tempLampVar].inDataBase = tempVarInDataBase;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "加载开关变量信息错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }   
        }

        /// <summary>
        /// Create table and triggers in database
        /// </summary>
        private void DataBaseInitial()
        {
            foreach (var node in GlobalVar.ipNodeMapping)
            {
                dataBase.CreateMappingTable();
                dataBase.CreateDataTable(node.Key, node.Value);
                dataBase.CreateDataLogTable(node.Key, node.Value);
                dataBase.CreateTrigger(node.Key, node.Value);
            }
        }


        /// <summary>
        /// Load tab page control from template config file
        /// </summary>
        /// <param name="filePath">Machinetool configure file</param>
        /// <param name="slaveAddress">Link module IP</param>
        public void LoadTemplate(string filePath, IPAddress slaveIP)
        {
            XDocument xDoc = new XDocument();
            xDoc = XDocument.Load(filePath);
            XElement UIElement = xDoc.Root.Element("UI");
            XElement variableElement = xDoc.Root.Element("Parameter");

            #region load controls
            try //labels
            {
                for (int labelIndex = 1; labelIndex <= int.Parse(UIElement.Element("Labels").Attribute("Count").Value); labelIndex++)
                {
                    XElement tempLabelElement = UIElement.Element("Labels").Element("Label" + "_" + labelIndex.ToString());
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
                    this.tabControl.SelectedTab.Controls.Add(lbl);
                }
            }
            catch (Exception)
            {
                return;
            }

            try //TextBoxes
            {
                for (int textIndex = 1; textIndex <= int.Parse(UIElement.Element("TextBoxes").Attribute("Count").Value); textIndex++)
                {
                    XElement tempTextElement = UIElement.Element("TextBoxes").Element("TextBox" + "_" + textIndex.ToString());
                    string tempTextBoxID = tempTextElement.Attribute("ID").Value;
                    int tempPosX = int.Parse(tempTextElement.Attribute("PosX").Value);
                    int tempPosY = int.Parse(tempTextElement.Attribute("PosY").Value);
                    int tempWidth = int.Parse(tempTextElement.Attribute("Width").Value);
                    int tempHeight = int.Parse(tempTextElement.Attribute("Height").Value);
                    int tempTextBoxRelateVar = int.Parse(tempTextElement.Attribute("RelateVar").Value);        //变量地址
                    int tempTextBoxMBInterface = int.Parse(tempTextElement.Attribute("Interface").Value);      //变量通道
                    DataSave tempVarInDataBase = DataSave.DoNotSave;
                    string tempVarName = "未关联变量";
                    if (tempTextBoxMBInterface == 4) //Interface 4
                    {
                        foreach(XElement element in variableElement.Element("IntReadWrite").Elements())
                        {
                            if ((string)element.Attribute("varAddress") == tempTextBoxRelateVar.ToString())
                            {
                                tempVarName = element.Attribute("varName").Value;
                                for (int i = 0; i < GlobalVar.strDataSave.Length; i++)
                                {
                                    if (GlobalVar.strDataSave[i] == element.Attribute("varInDataBase").Value)
                                    {
                                        if (Enum.IsDefined(typeof(DataSave), i))
                                        {
                                            tempVarInDataBase = (DataSave)i;//数据保存形式
                                        }
                                        else
                                            throw new Exception(i + "is not defined.");
                                    }
                                }
                            }
                        }                        
                    }
                    else //Interface 3
                    {
                        foreach (XElement element in variableElement.Element("IntReadOnly").Elements())
                        {
                            if ((string)element.Attribute("varAddress") == tempTextBoxRelateVar.ToString())
                            {
                                tempVarName = element.Attribute("varName").Value;
                                for (int i = 0; i < GlobalVar.strDataSave.Length; i++)
                                {
                                    if (GlobalVar.strDataSave[i] == element.Attribute("varInDataBase").Value)
                                    {
                                        if (Enum.IsDefined(typeof(DataSave), i))
                                        {
                                            tempVarInDataBase = (DataSave)i;//数据保存形式
                                        }
                                        else
                                            throw new Exception(i + "is not defined.");
                                    }
                                }                                
                            }
                        }
                    }

                    TextBoxEx txt = new TextBoxEx();
                    txt.Location = new Point(tempPosX, tempPosY);
                    txt.Name = tempTextBoxID;
                    txt.Width = tempWidth;
                    txt.Height = tempHeight;
                    txt.TextAlign = HorizontalAlignment.Right;//文本内容位置靠右
                    txt.ReadOnly = true;
                    txt.BackColor = Color.White;
                    txt.SlaveAddress = slaveIP;
                    txt.RelateVar = tempTextBoxRelateVar;
                    txt.mInDataBase = tempVarInDataBase;
                    txt.VarName = tempVarName;
                    txt.Text = tempVarName;
                    switch (tempTextBoxMBInterface)
                    {
                        case 3:
                            txt.MbInterface = DataInterface.InputRegister;
                            break;
                        case 4:
                            txt.MbInterface = DataInterface.HoldingRegister;
                            txt.Cursor = Cursors.Hand;
                            break;
                        default:
                            break;
                    }

                    txt.Click += new EventHandler(Text_Click);
                    txt.DoubleClick += new EventHandler(Text_DoubleClick);
                    txt.MouseDown += new MouseEventHandler(Text_MouseDown);
                    txt.MouseMove += new MouseEventHandler(Text_MouseMove);
                    this.tabControl.SelectedTab.Controls.Add(txt);
                }
            }
            catch (Exception)
            {
                return;
            }
            try
            {
                for (int lampIndex = 1; lampIndex <= int.Parse(UIElement.Element("Lamps").Attribute("Count").Value); lampIndex++)
                {
                    XElement tempLampElement = UIElement.Element("Lamps").Element("Lamp" + "_" + lampIndex.ToString());
                    string tempLampID = tempLampElement.Attribute("ID").Value;
                    int tempPosX = int.Parse(tempLampElement.Attribute("PosX").Value);
                    int tempPosY = int.Parse(tempLampElement.Attribute("PosY").Value);
                    int tempLampRelateVar = int.Parse(tempLampElement.Attribute("RelateVar").Value);
                    int tempInterface = int.Parse(tempLampElement.Attribute("Interface").Value);
                    DataSave tempVarInDataBase = DataSave.DoNotSave;
                    string tempVarName = "未关联变量";
                    if (tempInterface == 2) //Interface 2
                    {
                        foreach (XElement element in variableElement.Element("BoolReadWrite").Elements())
                        {
                            if ((string)element.Attribute("varAddress") == tempLampRelateVar.ToString())
                            {
                                tempVarName = element.Attribute("varName").Value;
                                for (int i = 0; i < GlobalVar.strDataSave.Length; i++)
                                {
                                    if (GlobalVar.strDataSave[i] == element.Attribute("varInDataBase").Value)
                                    {
                                        if (Enum.IsDefined(typeof(DataSave), i))
                                        {
                                            tempVarInDataBase = (DataSave)i;//数据保存形式
                                        }
                                        else
                                            throw new Exception(i + "is not defined.");
                                    }
                                }                                
                            }
                        }
                    }
                    else //Interface 1
                    {
                        foreach (XElement element in variableElement.Element("BoolReadOnly").Elements())
                        {
                            if ((string)element.Attribute("varAddress") == tempLampRelateVar.ToString())
                            {
                                tempVarName = element.Attribute("varName").Value;
                                for (int i = 0; i < GlobalVar.strDataSave.Length; i++)
                                {
                                    if (GlobalVar.strDataSave[i] == element.Attribute("varInDataBase").Value)
                                    {
                                        if (Enum.IsDefined(typeof(DataSave), i))
                                        {
                                            tempVarInDataBase = (DataSave)i;//数据保存形式
                                        }
                                        else
                                            throw new Exception(i + "is not defined.");
                                    }
                                }  
                            }
                        }
                    }

                    Lamp lamp = new Lamp();
                    lamp.Location = new Point(tempPosX, tempPosY);
                    lamp.Name = tempLampID;
                    lamp.RelateVar = tempLampRelateVar;
                    lamp.SlaveAddress = slaveIP;
                    if (tempInterface == 1)
                    {
                        lamp.ReadOnly = true;
                    }
                    else
                    {
                        lamp.ReadOnly = false;
                        lamp.Cursor = Cursors.Hand;
                    }
                    lamp.mInDataBase = tempVarInDataBase;
                    lamp.Name = tempVarName;

                    lamp.Click += new EventHandler(Lamp_Click);
                    lamp.DoubleClick += new EventHandler(Lamp_DoubleClick);
                    lamp.MouseDown += new MouseEventHandler(Lamp_MouseDown);
                    lamp.MouseMove += new MouseEventHandler(Lamp_MouseMove);
                    this.tabControl.SelectedTab.Controls.Add(lamp);
                }
            }
            catch (Exception)
            {
                return;
            }
            #endregion

            #region Load variables
            string nodeProtocolType = variableElement.Attribute("Protocol").Value;
            string nodeName = tabControl.SelectedTab.Text;
            DataCount dataCount = new DataCount();
            dataCount.discreteInput = Convert.ToUInt16(variableElement.Element("BoolReadOnly").Attribute("Length").Value);
            dataCount.coil = Convert.ToUInt16(variableElement.Element("BoolReadWrite").Attribute("Length").Value);
            dataCount.inputRegister = Convert.ToUInt16(variableElement.Element("IntReadOnly").Attribute("Length").Value);
            dataCount.holdingRegiter = Convert.ToUInt16(variableElement.Element("IntReadWrite").Attribute("Length").Value);
            RemoteNode tempNode = new RemoteNode(nodeProtocolType, dataCount, nodeName);

            if (GlobalVar.ipNodeMapping.ContainsKey(slaveIP))
            {                
                GlobalVar.ipNodeMapping[slaveIP] = tempNode;
            }
            else
            {
                GlobalVar.ipNodeMapping.Add(slaveIP, tempNode);
            }
            #endregion
        }

        private void btnEditStop_Click(object sender, EventArgs e)
        {
            GlobalVar.editFlag = false;
            btnEdit.Enabled = true;//启用编辑按钮
            btnStart.Enabled = true; //启用启动按钮
            btnSavEdit.Visible = false; //隐藏保存按钮
            btnEditStop.Visible = false;
            mToolForm.Close();
            tabControl.TabPages.Clear();
            GlobalVar.ipNodeMapping.Clear();
            LoadControls(GlobalVar.xmlPath);
            LoadVariables(GlobalVar.xmlPath);
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
                if (ctrl is TextBoxEx)
                {
                    TextBoxEx textBox = ctrl as TextBoxEx;
                    for (int i = 0; i < communicate.slaveList.Count; i++)
                    {
                        Slave slave = communicate.slaveList[i];
                        if (textBox.SlaveAddress.Equals(slave.slaveIP))
                        {
                            try
                            {
                                if (textBox.MbInterface == DataInterface.HoldingRegister)
                                {
                                    textBox.Text = slave.remoteNode.varHoldingRegister[textBox.RelateVar].intValue.ToString();
                                }
                                if (textBox.MbInterface == DataInterface.InputRegister)
                                {
                                    textBox.Text = slave.remoteNode.varInputRegister[textBox.RelateVar].intValue.ToString();
                                }
                            }
                            catch (System.IndexOutOfRangeException)
                            {
                                textBox.Text = "节点数据未定义";
                            }
                            break;
                        }
                    }                    
                }
                else if (ctrl is Lamp)
                {
                    Lamp lamp = ctrl as Lamp;
                    for (int i = 0; i < communicate.slaveList.Count; i++)
                    {
                        Slave slave = communicate.slaveList[i];
                        if (lamp.SlaveAddress.Equals(slave.slaveIP)) 
                        {
                            try
                            {
                                if (!lamp.ReadOnly)
                                    lamp.onFlag = slave.remoteNode.varDiscreteOutput[lamp.RelateVar].boolValue;
                                else
                                    lamp.onFlag = slave.remoteNode.varDiscreteInput[lamp.RelateVar].boolValue;
                            }
                            catch (Exception)
                            {
                                lamp.onFlag = false;
                            }
                            break;
                        }
                    }                   
                }                
            }
        }

        public void OnOffline(IPAddress slaveIP)
        {
            foreach (TabPage page in tabControl.TabPages)
            {
                foreach (Control ctrl in page.Controls)
                {
                    if (ctrl is TextBoxEx)
                    {
                        if (ctrl.InvokeRequired)
                        {
                            Action<IPAddress> actionDelegate = (IPAddress ip) =>
                            {
                                TextBoxEx textBox = ctrl as TextBoxEx;
                                if (textBox.SlaveAddress == ip)
                                {
                                    textBox.Text = "离线";
                                }
                            };
                            ctrl.Invoke(actionDelegate, new object[] { slaveIP });
                        }
                        else
                        {
                            TextBoxEx textBox = ctrl as TextBoxEx;
                            if (textBox.SlaveAddress == slaveIP)
                            {
                                textBox.Text = "离线";
                            }
                        }                        
                    }
                }
            }
        }

    }
}
