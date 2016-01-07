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
            object[] allRows = new object[] { coloumn1, coloumn2, coloumn3, coloumn4 }; 

            int rowNum = form1.dataGridView.Rows.Count;
            uint address = 0;

            //添加的数据必须每一项都不为空，否则不会添加
            if (GVL.alterornot == 2 && this.textboxName.Text != "" && this.cbDataType.Text!= "" && uint.TryParse(textboxAddress.Text, out address))
            {
                coloumn1 = this.textboxName.Text;
                coloumn2 = this.cbDataType.Text;                
                coloumn3 = this.textboxAddress.Text;

                if (checkBoxDataBase.CheckState == CheckState.Checked)
                {
                    coloumn4 = "true";    //添加入数据库为true,不添加为false
                }
                else
                {
                    coloumn4 = "false";
                }
                allRows = new object[] { coloumn1, coloumn2,coloumn3, coloumn4 };
                form1.dataGridView.Rows.Add(allRows);
            }
            else if (GVL.alterornot == 1 && form1.dataGridView.Rows.Count > 0 && textboxName.Text != "" && cbDataType.Text != "" && textboxAddress.Text != "")
            {
                coloumn1 = this.textboxName.Text;
                coloumn2 = this.cbDataType.Text;
                coloumn3 = this.textboxAddress.Text;
                int rowsindex = form1.dataGridView.CurrentRow.Index;
                if (rowsindex >= 0)
                {
                    form1.dataGridView.Rows[rowsindex].Cells[0].Value = this.textboxName.Text;
                    form1.dataGridView.Rows[rowsindex].Cells[1].Value = this.cbDataType.Text;
                    form1.dataGridView.Rows[rowsindex].Cells[2].Value = this.textboxAddress.Text;

                    if (checkBoxDataBase.CheckState == CheckState.Checked)
                    {
                        form1.dataGridView.Rows[rowsindex].Cells[3].Value = "true";
                    }
                    else
                    {
                        form1.dataGridView.Rows[rowsindex].Cells[3].Value = "false";
                    }
                    //获取数据读取长度
                    switch (cbDataType.Text)
                    {
                        case "1 开关量 只读":
                            if (ushort.Parse(coloumn3) > GVL.dataLength.discreteInput)
                                GVL.dataLength.discreteInput = ushort.Parse(coloumn3);
                            break;
                        case "2 开关量 读写":
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
