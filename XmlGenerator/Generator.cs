using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace XMLGenerator
{
    public delegate string GetVarName(string varInterface, int varAddress);
    public partial class Generator : Form
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("user32", EntryPoint = "HideCaret")]
        static extern bool HideCaret(IntPtr hWnd);//用于隐藏TextBox的光标

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
        TextboxEX currTxt;
        TextEditForm mCurrTxtEditForm;

        //指示灯
        Lamp currLamp;
        LampEditForm mCurrLampEditForm;
        
        ToolForm mToolForm;
        string filePath = "";

        public Generator()
        {
            InitializeComponent();
        }

        //添加数据项
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.tabControl.SelectedTab = pageVariable;
            EditVariable edivariable;
            edivariable = new EditVariable(this);
            edivariable.cbDatabase.SelectedIndex = 0;
            edivariable.Show();
            buttonAdd.Enabled = false;
            GVL.alterornot = 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo dllDirectory = new DirectoryInfo(Environment.CurrentDirectory + "\\Library");
            foreach (FileInfo fi in dllDirectory.GetFiles("*.dll"))
            {
                if (fi.Name == "ICommunicate.dll")
                {
                    continue;
                }
                cbProtocol.Items.Add(fi.Name.Split('.')[0]);
            }
        }
        //修改数据项
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.tabControl.SelectedTab = pageVariable;
            if (dataGridView.SelectedRows.Count > 0)
            {
                EditVariable edivariable;
                edivariable = new EditVariable(this);
                edivariable.textboxAddress.Text = dataGridView.SelectedRows[0].Cells[2].Value.ToString();
                edivariable.textboxName.Text = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                edivariable.cbDataType.Text = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
                edivariable.cbDatabase.Text = dataGridView.SelectedRows[0].Cells[3].Value.ToString();
                GVL.alterornot = 1;
                edivariable.ShowDialog();
            }      
         
        }

        //删除数据项
        private void buttonDel_Click(object sender, EventArgs e)
        {
            this.tabControl.SelectedTab = pageVariable;
            if (dataGridView.Rows.Count > 1)
            {
                dataGridView.Rows.RemoveAt(dataGridView.SelectedRows[0].Index);
            }
            else
            {
                dataGridView.Rows.Clear();
            }
        }
        //打开现有XML文件
        private void buttonOpen_Click(object sender, EventArgs e)
        {           
            if (GVL.IFornotload ==true)
            {
                // 清空界面信息
                this.textBoxName.Text = null;
                this.textBoxModel.Text = null;
                this.cbProtocol.Text = null;
                this.dataGridView.Rows.Clear();
                this.tabControl.TabPages[1].Controls.Clear();

                //clear data lenth
                GVL.dataLength.discreteInput = 0;
                GVL.dataLength.coil = 0;
                GVL.dataLength.inputRegister = 0;
                GVL.dataLength.holdingRegiter = 0;

                CSaveXML saveXml = new CSaveXML();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML files (*.xml)|*.xml|All Files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\Config";
                try
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;

                        if (openFileDialog.FileName != null)
                        {
                            saveXml.ParaseXML(filePath, this);
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("文件存在错误", err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

        //保存XML
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == " ")
            {
                MessageBox.Show("没有定义机器名称");
                return;
            }
            else if (textBoxModel.Text == "")
            {
                MessageBox.Show("没有定义设备监控号");
                return;
            }
            else if (cbProtocol.Text == "")
            {
                MessageBox.Show("没有定义数据通道协议");
                return;
            }
           
            CSaveXML save = new CSaveXML();
            if (filePath == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\Config";
                saveFileDialog.FileName = textBoxModel.Text;
                try
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = saveFileDialog.FileName.ToString();
                        save.SaveXml(filePath, this);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("保存失败。");
                }
            }
            else
            {
                save.SaveXml(filePath, this);
            }
        }

        #region Label options

        //鼠标按下
        public void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            if (GVL.editFlag)
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
            if (GVL.editFlag)
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
            if (GVL.editFlag)
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
        private void delLabel_Click(object sender, EventArgs e)
        {
            this.tabControl.SelectedTab.Controls.Remove(currLbl);
        }
        #endregion 添加标签部分

        #region TextBox Options
        //鼠标按下
        public void Text_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret((sender as TextboxEX).Handle);//取消光标显示
            if (GVL.editFlag)
            {
                currTxt = new TextboxEX();
                if (sender is TextboxEX)
                {
                    currTxt = (TextboxEX)sender;
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
            if (GVL.editFlag)
            {
                TextboxEX txt = new TextboxEX();
                if (sender is TextboxEX)
                {
                    txt = (TextboxEX)sender;
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
            if (GVL.editFlag)
            {
                if (sender is TextboxEX)
                {
                    currTxt = (TextboxEX)sender;
                    mCurrTxtEditForm = new TextEditForm();

                    mCurrTxtEditForm.getVarName += new GetVarName(FindVarName);

                    mCurrTxtEditForm.txtWidth.Text = currTxt.Width.ToString();
                    mCurrTxtEditForm.txtHeight.Text = currTxt.Height.ToString();
                    mCurrTxtEditForm.txtPosX.Text = currTxt.Location.X.ToString();
                    mCurrTxtEditForm.txtPosY.Text = currTxt.Location.Y.ToString();
                    mCurrTxtEditForm.txtVarName.Text = currTxt.VarName;

                    mCurrTxtEditForm.txtVarName.Text = currTxt.SlaveAddress.ToString();
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
                    mCurrTxtEditForm.btnTxtSav.Click += new EventHandler(mCurrTxtEditForm_btnTxtSav_Click);

                    mCurrTxtEditForm.cbbTxtVar.SelectedIndex = currTxt.RelateVar;
                    mCurrTxtEditForm.ShowDialog();

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
                currTxt.RelateVar = mCurrTxtEditForm.cbbTxtVar.SelectedIndex;//将该文本框关联的变量标号赋给该文本框属性
                currTxt.MbInterface = mCurrTxtEditForm.modbusInterface;  //变量通道
                currTxt.SlaveAddress = 0; //modbus slave 对应 id
                currTxt.VarName = mCurrTxtEditForm.txtVarName.Text;
            }
            catch (System.Exception)
            {
                MessageBox.Show("请完整设置变量名称、变量位置、变量通道和变量类型。", "注意");
                currTxt.Text = "";
                currTxt.RelateVar = 0;//将该文本框关联的变量标号赋给该文本框属性
            }
            foreach(DataGridViewRow row in dataGridView.Rows)
            {
                if (currTxt.MbInterface == DataInterface.InputRegister)
                {
                    if (row.Cells[1].Value.ToString() == "3 数值量 只读" && row.Cells[2].Value.ToString() == (currTxt.RelateVar + 1).ToString())
                    {
                        row.Cells[0].Value = currTxt.VarName;
                    }
                }
                else
                {
                    if (row.Cells[1].Value.ToString() == "4 数值量 读写" && row.Cells[2].Value.ToString() == (currTxt.RelateVar + 1).ToString())
                    {
                        row.Cells[0].Value = currTxt.VarName;
                    }
                }                
            }
            mCurrTxtEditForm.Close();//保存完成后关闭
        }

        //右键菜单 删除文本框    
        private void deltextbox_Click(object sender, EventArgs e)
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
        public void Lamp_MouseDown(object sender, MouseEventArgs e)
        {
            if (GVL.editFlag)
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
            if (GVL.editFlag)
            {
                Lamp lamp1 = new Lamp();
                if (sender is Lamp)
                {
                    lamp1 = sender as Lamp;
                }
                if (e.Button == MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    lamp1.Location = new Point(lampLocat.X + temp.X - lampStart.X, lampLocat.Y + temp.Y - lampStart.Y);
                }
            }
        }

        //double click open edit form
        public void Lamp_DoubleClick(object sender, EventArgs e)
        {
            if (GVL.editFlag && sender is Lamp)
            {
                currLamp = new Lamp();
                if (sender is Lamp)
                {
                    currLamp = sender as Lamp;
                }
                mCurrLampEditForm = new LampEditForm();
                mCurrLampEditForm.getVarName = new GetVarName(FindVarName);
                mCurrLampEditForm.tbLampX.Text = currLamp.Location.X.ToString();
                mCurrLampEditForm.tbLampY.Text = currLamp.Location.Y.ToString();
                mCurrLampEditForm.textBoxVarName.Text = currLamp.SlaveAddress.ToString();
                mCurrLampEditForm.ReadOnly = currLamp.ReadOnly;
                mCurrLampEditForm.cbLampVar.SelectedIndex = currLamp.RelateVar;
                mCurrLampEditForm.Text = currLamp.varName + " - 开关量选项";

                mCurrLampEditForm.buttonOk.Click += new EventHandler(LampEditbuttonOk_Click);
                mCurrLampEditForm.ShowDialog();
            }
        }

        //保存当前指示灯属性
        void LampEditbuttonOk_Click(object sender, EventArgs e)
        {
            int posX = int.Parse(mCurrLampEditForm.tbLampX.Text);
            int posY = int.Parse(mCurrLampEditForm.tbLampY.Text);
            currLamp.Location = new Point(posX, posY);
            currLamp.SlaveAddress = int.Parse(mCurrLampEditForm.textBoxVarName.Text);
            currLamp.RelateVar = mCurrLampEditForm.cbLampVar.SelectedIndex;
            currLamp.ReadOnly = mCurrLampEditForm.ReadOnly;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (currLamp.ReadOnly)
                {
                    if (row.Cells[1].Value.ToString() == "1 开关量 只读" && row.Cells[2].Value.ToString() == (currLamp.RelateVar + 1).ToString())
                    {
                        currLamp.varName = row.Cells[0].Value.ToString();
                    }
                }
                else
                {
                    if (row.Cells[1].Value.ToString() == "2 开关量 读写" && row.Cells[2].Value.ToString() == (currLamp.RelateVar + 1).ToString())
                    {
                        currLamp.varName = row.Cells[0].Value.ToString();
                    }
                }
            }
            mCurrLampEditForm.Close();
        }

      
        private void delLamp_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab.Controls.Remove(currLamp);
        }
        #endregion


        public string FindVarName(string varInterface, int varAddress)
        {
            string varName = "未设置变量";
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[1].Value.ToString() == varInterface && row.Cells[2].Value.ToString() == varAddress.ToString())
                {
                    varName = row.Cells[0].Value.ToString();
                }
            }
            return varName;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == pageUI)
            {
                mToolForm = new ToolForm(this);
                mToolForm.Location = new Point(this.Location.X + this.Width + 2, this.Location.Y);
                mToolForm.Show();

                //全局变量，true时机床界面进入可编辑状态
                GVL.editFlag = true;
            }
            else
            {
                GVL.editFlag = false;
                mToolForm.Close();
            }
        }

    }
}


 