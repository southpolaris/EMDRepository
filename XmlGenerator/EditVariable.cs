using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace XMLGenerator
{
    public partial class EditVariable : Form
    {
        public EditVariable()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private Generator form1;
        public EditVariable(Generator form1)
        {
             InitializeComponent();
             this.form1 = form1;
             this.ControlBox = false;
        }
       
        //添加和修改监控变量
        private void buttonOK_Click(object sender, EventArgs e)
        {
            string coloumn1 = null;
            string coloumn2 = null;
            string coloumn3 = null;
            string coloumn4 = null;
            string[] allRows = new string[] { coloumn1, coloumn2, coloumn3, coloumn4 }; 

            int rowNum = form1.dataGridView.Rows.Count;
            uint address = 0;

            GVL.dataLength.coil = 0;
            GVL.dataLength.discreteInput = 0;
            GVL.dataLength.inputRegister = 0;
            GVL.dataLength.holdingRegiter = 0;

            //Add new variable to table
            if (GVL.alterornot == 2 && this.textboxName.Text != "" && this.cbDataType.Text!= "" && uint.TryParse(textboxAddress.Text, out address))
            {
                coloumn1 = this.textboxName.Text;
                coloumn2 = this.cbDataType.Text;                
                coloumn3 = this.textboxAddress.Text;
                coloumn4 = this.cbDatabase.Text;

                allRows = new string[] { coloumn1, coloumn2, coloumn3, coloumn4 };
                foreach(DataGridViewRow row in form1.dataGridView.Rows)
                {
                    if (string.Compare(row.Cells[1].Value.ToString(), coloumn2) == 0 && string.Compare(row.Cells[2].Value.ToString(), coloumn3) == 0)
                    {
                        MessageBox.Show("选取地址已有变量定义，请重新选择", "变量已存在", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                }
                form1.dataGridView.Rows.Add(allRows);
                //获取数据读取长度
                switch (cbDataType.Text)
                {
                    case "1 开关量 只读":
                        if (ushort.Parse(coloumn3) > GVL.dataLength.discreteInput)
                            GVL.dataLength.discreteInput = ushort.Parse(coloumn3);
                        break;
                    case "0 开关量 读写":
                        if (ushort.Parse(coloumn3) > GVL.dataLength.coil)
                            GVL.dataLength.coil = ushort.Parse(coloumn3);
                        break;
                    case "3 数值量 只读":
                        if (ushort.Parse(coloumn3) > GVL.dataLength.inputRegister)
                            GVL.dataLength.inputRegister = ushort.Parse(coloumn3);
                        break;
                    case "4 数值量 读写":
                        if (ushort.Parse(coloumn3) > GVL.dataLength.holdingRegiter)
                            GVL.dataLength.holdingRegiter = ushort.Parse(coloumn3);
                        break;
                }
            }
            //Edit exist variable
            else if (GVL.alterornot == 1 && form1.dataGridView.Rows.Count > 0 && textboxName.Text != "" && cbDataType.Text != "" && textboxAddress.Text != "")
            {
                coloumn1 = this.textboxName.Text;
                coloumn2 = this.cbDataType.Text;
                coloumn3 = this.textboxAddress.Text;
                int rowsindex = form1.dataGridView.CurrentRow.Index;
                if (rowsindex >= 0)
                {
                    foreach (DataGridViewRow row in form1.dataGridView.Rows)
                    {
                        if (row == form1.dataGridView.CurrentRow)
                        {
                            continue;
                        }
                        if (string.Compare(row.Cells[1].Value.ToString(), coloumn2) == 0 && string.Compare(row.Cells[2].Value.ToString(), coloumn3) == 0)
                        {
                            MessageBox.Show("选取地址已有变量定义，请重新选择", "变量已存在", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    form1.dataGridView.Rows[rowsindex].Cells[0].Value = this.textboxName.Text;
                    form1.dataGridView.Rows[rowsindex].Cells[1].Value = this.cbDataType.Text;
                    form1.dataGridView.Rows[rowsindex].Cells[2].Value = this.textboxAddress.Text;

                    form1.dataGridView.Rows[rowsindex].Cells[3].Value = this.cbDatabase.Text;
                }
                //获取数据读取长度
                switch (cbDataType.Text)
                {
                    case "1 开关量 只读":
                        if (ushort.Parse(coloumn3) > GVL.dataLength.discreteInput)
                            GVL.dataLength.discreteInput = ushort.Parse(coloumn3);
                        break;
                    case "0 开关量 读写":
                        if (ushort.Parse(coloumn3) > GVL.dataLength.coil)
                            GVL.dataLength.coil = ushort.Parse(coloumn3);
                        break;
                    case "3 数值量 只读":
                        if (ushort.Parse(coloumn3) > GVL.dataLength.inputRegister)
                            GVL.dataLength.inputRegister = ushort.Parse(coloumn3);
                        break;
                    case "4 数值量 读写":
                        if (ushort.Parse(coloumn3) > GVL.dataLength.holdingRegiter)
                            GVL.dataLength.holdingRegiter = ushort.Parse(coloumn3);
                        break;
                }
                this.Close();
            }
            else 
            {
                MessageBox.Show("请检查数据");
            }
        }

        private void editvariable_Load(object sender, EventArgs e)
        {
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            form1.buttonAdd.Enabled = true;
        }        
    }
}
