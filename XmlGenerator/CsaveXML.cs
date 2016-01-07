using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XMLGenerator
{
    class CsaveXML
    {
        public void SaveXml(string filename , Generator form1)
        {           
            int rowNumber = form1.dataGridView.Rows.Count;
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration dec = xDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xDoc.AppendChild(dec);
            XmlElement root = xDoc.CreateElement("Root");
            xDoc.AppendChild(root);
            XmlElement paraElement = xDoc.CreateElement("Parameter");

            paraElement.SetAttribute("Name", form1.textBoxName.Text);
            paraElement.SetAttribute("Model", form1.textBoxModel.Text);
            paraElement.SetAttribute("Protocol", form1.cbProtocol.Text);

            int boolReadOnlyCount = 0;
            int boolReadWriteCount = 0;
            int intReadOnlyCount = 0;
            int intReadWriteCount = 0;

            
            XmlElement boolReadOnlyElement = xDoc.CreateElement("BoolReadOnly");
            XmlElement boolReadWriteElement = xDoc.CreateElement("BoolReadWrite");
            XmlElement intReadOnlyElement = xDoc.CreateElement("IntReadOnly");
            XmlElement intReadWriteElement = xDoc.CreateElement("IntReadWrite");
            for (int i = 0; i < rowNumber; i++)
            {
                switch(form1.dataGridView.Rows[i].Cells[1].Value.ToString())
                {
                    case "1 开关量 只读":
                        boolReadOnlyCount++;
                        XmlElement variables1 = xDoc.CreateElement("Variable");
                        variables1.SetAttribute(form1.dataGridView.Columns[0].Name, form1.dataGridView.Rows[i].Cells[0].Value.ToString());
                        variables1.SetAttribute(form1.dataGridView.Columns[2].Name, (string)form1.dataGridView.Rows[i].Cells[2].Value);
                        variables1.SetAttribute(form1.dataGridView.Columns[3].Name, (string)form1.dataGridView.Rows[i].Cells[3].Value);
                        boolReadOnlyElement.AppendChild(variables1);
                        if (Convert.ToUInt16(form1.dataGridView.Rows[i].Cells[2].Value) > GVL.dataLength.discreteInput)
                        {
                            GVL.dataLength.discreteInput = Convert.ToUInt16(form1.dataGridView.Rows[i].Cells[2].Value);
                        }
                        break;
                    case "2 开关量 读写":
                        boolReadWriteCount++;
                        XmlElement variables2 = xDoc.CreateElement("Variable");
                        variables2.SetAttribute(form1.dataGridView.Columns[0].Name, (string)form1.dataGridView.Rows[i].Cells[0].Value);
                        variables2.SetAttribute(form1.dataGridView.Columns[2].Name, (string)form1.dataGridView.Rows[i].Cells[2].Value);
                        variables2.SetAttribute(form1.dataGridView.Columns[3].Name, (string)form1.dataGridView.Rows[i].Cells[3].Value);
                        boolReadWriteElement.AppendChild(variables2);
                        if (Convert.ToUInt16(form1.dataGridView.Rows[i].Cells[2].Value) > GVL.dataLength.coil)
                        {
                            GVL.dataLength.coil = Convert.ToUInt16(form1.dataGridView.Rows[i].Cells[2].Value);
                        }
                        break;
                    case "3 数值量 只读":
                        intReadOnlyCount++;
                        XmlElement variables3 = xDoc.CreateElement("Variable");
                        variables3.SetAttribute(form1.dataGridView.Columns[0].Name, (string)form1.dataGridView.Rows[i].Cells[0].Value);
                        variables3.SetAttribute(form1.dataGridView.Columns[2].Name, (string)form1.dataGridView.Rows[i].Cells[2].Value);
                        variables3.SetAttribute(form1.dataGridView.Columns[3].Name, (string)form1.dataGridView.Rows[i].Cells[3].Value);
                        intReadOnlyElement.AppendChild(variables3);
                        if (Convert.ToUInt16(form1.dataGridView.Rows[i].Cells[2].Value) > GVL.dataLength.inputRegister)
                        {
                            GVL.dataLength.inputRegister = Convert.ToUInt16(form1.dataGridView.Rows[i].Cells[2].Value);
                        }
                        break;
                    case "4 数值量 读写":
                        intReadWriteCount++;
                        XmlElement variables4 = xDoc.CreateElement("Variable");
                        variables4.SetAttribute(form1.dataGridView.Columns[0].Name, (string)form1.dataGridView.Rows[i].Cells[0].Value);
                        variables4.SetAttribute(form1.dataGridView.Columns[2].Name, (string)form1.dataGridView.Rows[i].Cells[2].Value);
                        variables4.SetAttribute(form1.dataGridView.Columns[3].Name, (string)form1.dataGridView.Rows[i].Cells[3].Value);
                        intReadWriteElement.AppendChild(variables4);
                        if (Convert.ToUInt16(form1.dataGridView.Rows[i].Cells[2].Value) > GVL.dataLength.holdingRegiter)
                        {
                            GVL.dataLength.holdingRegiter = Convert.ToUInt16(form1.dataGridView.Rows[i].Cells[2].Value);
                        }
                        break;
                    default:
                        break;
                }
            }
            int totalNumber = boolReadOnlyCount + boolReadWriteCount + intReadOnlyCount + intReadWriteCount;
            boolReadOnlyElement.SetAttribute("Length", GVL.dataLength.discreteInput.ToString());
            boolReadWriteElement.SetAttribute("Length", GVL.dataLength.coil.ToString());
            intReadOnlyElement.SetAttribute("Length", GVL.dataLength.inputRegister.ToString());
            intReadWriteElement.SetAttribute("Length", GVL.dataLength.holdingRegiter.ToString());

            paraElement.AppendChild(boolReadOnlyElement);
            paraElement.AppendChild(boolReadWriteElement);
            paraElement.AppendChild(intReadOnlyElement);
            paraElement.AppendChild(intReadWriteElement);
            paraElement.SetAttribute("Count", totalNumber.ToString()); //变量总数

            root.AppendChild(paraElement);

            //对第二页进行保存
            XmlElement UIelements = xDoc.CreateElement("UI");
            int labelIndex = 1;
            int txtIndex = 1;
            int lampIndex = 1;
            TabPage page;
            page = form1.tabControl.TabPages[1];
            XmlElement labelElement = xDoc.CreateElement("Labels");
            XmlElement textElement =  xDoc.CreateElement("TextBoxes");
            XmlElement lampElement = xDoc.CreateElement("Lamps");

            foreach (Control ctrl in page.Controls)
            {
                if (ctrl is Label)                   // 判断是不是LABEl  
                {
                    string lblKnot = "";
                    lblKnot = "Label" + "_" + labelIndex.ToString();//节名 
                    ctrl.Name = lblKnot;//重新分配标签标识  以节名命名
                  
                    XmlElement lbls = xDoc.CreateElement(lblKnot);

                    lbls.SetAttribute("ID",ctrl.Name);
                    lbls.SetAttribute("Text", ctrl.Text);
                    lbls.SetAttribute("PosX", ctrl.Location.X.ToString());
                    lbls.SetAttribute("PosY", ctrl.Location.Y.ToString());
                    lbls.SetAttribute("Width", ctrl.Width.ToString());
                    lbls.SetAttribute("Height", ctrl.Height.ToString());

                    labelElement.AppendChild(lbls);
                    labelIndex++;
                }

                if (ctrl is TextboxEX)
                {
                    string TxtKnot = "";
                    TxtKnot = "TextBox" + "_" + txtIndex.ToString();//节名 
                    ctrl.Name = TxtKnot;//重新分配标识  以节名命名
                   
                    XmlElement texts = xDoc.CreateElement(TxtKnot);

                    texts.SetAttribute("ID", ctrl.Name);
                    texts.SetAttribute("RelateVar", (ctrl as TextboxEX).RelateVar.ToString());
                    texts.SetAttribute("SlaveAddress", (ctrl as TextboxEX).SlaveAddress.ToString());
                    texts.SetAttribute("Interface", ((int)((ctrl as TextboxEX).MbInterface)).ToString());
                    texts.SetAttribute("PosX", ctrl.Location.X.ToString());
                    texts.SetAttribute("PosY", ctrl.Location.Y.ToString());
                    texts.SetAttribute("Width", ctrl.Width.ToString());
                    texts.SetAttribute("Height", ctrl.Height.ToString());

                    textElement.AppendChild(texts);
                    txtIndex++;

                }

                if (ctrl is Lamp)
                {               
                   string lampNot = "";
                   lampNot = "Lamp" + "_" + lampIndex.ToString();
                   ctrl.Name = lampNot;
                   XmlElement lamps = xDoc.CreateElement(lampNot); 
                   lamps.SetAttribute("ID", ctrl.Name);
                   lamps.SetAttribute("RelateVar", (ctrl as Lamp).RelateVar.ToString());
                   lamps.SetAttribute("SlaveAddress", (ctrl as Lamp).SlaveAddress.ToString());
                   lamps.SetAttribute("Interface", ((Lamp)ctrl).ReadOnly == true ? "1" : "0");
                   lamps.SetAttribute("PosX", ctrl.Location.X.ToString());
                   lamps.SetAttribute("PosY", ctrl.Location.Y.ToString());

                   lampElement.AppendChild(lamps);
                   lampIndex++;
                }              
            }
            labelElement.SetAttribute("Count", (labelIndex - 1).ToString());
            textElement.SetAttribute("Count", (txtIndex - 1).ToString());
            lampElement.SetAttribute("Count", (lampIndex - 1).ToString());
            UIelements.AppendChild(labelElement);
            UIelements.AppendChild(textElement);
            UIelements.AppendChild(lampElement);
            root.AppendChild(UIelements);
            xDoc.Save(filename);                
        }

        //解析XML
        public void ParaseXML(string filename, Generator mForm1)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlElement paraElement = doc["Root"]["Parameter"];
            XmlElement UIElement = doc["Root"]["UI"];

            mForm1.Text = paraElement.GetAttribute("Model") + " - 配置文件编辑器";
            mForm1.textBoxName.Text = paraElement.GetAttribute("Name");
            mForm1.textBoxModel.Text = paraElement.GetAttribute("Model");
            mForm1.cbProtocol.Text = paraElement.GetAttribute("Protocol");

            XmlElement boolReadOnlyElement = paraElement["BoolReadOnly"];
            XmlElement boolReadWriteElement = paraElement["BoolReadWrite"];
            XmlElement intReadOnlyElement = paraElement["IntReadOnly"];
            XmlElement intReadWriteElement = paraElement["IntReadWrite"];
            GVL.dataLength.discreteInput = Convert.ToUInt16(boolReadOnlyElement.GetAttribute("Length"));
            GVL.dataLength.coil = Convert.ToUInt16(boolReadWriteElement.GetAttribute("Length"));
            GVL.dataLength.inputRegister = Convert.ToUInt16(intReadOnlyElement.GetAttribute("Length"));
            GVL.dataLength.holdingRegiter = Convert.ToUInt16(intReadWriteElement.GetAttribute("Length"));

            int rowNumber = Convert.ToInt32(paraElement.GetAttribute("Count"));
            int rowIndex = 0;

            #region Load data grid view

            foreach (XmlNode node in boolReadOnlyElement.ChildNodes)
            {
                mForm1.dataGridView.Rows.Add();
                mForm1.dataGridView.Rows[rowIndex].Cells[0].Value = node.Attributes[mForm1.dataGridView.Columns[0].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[2].Value = node.Attributes[mForm1.dataGridView.Columns[2].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[3].Value = node.Attributes[mForm1.dataGridView.Columns[3].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[1].Value = "1 开关量 只读";
                rowIndex++;
            }
            foreach (XmlNode node in boolReadWriteElement.ChildNodes)
            {
                mForm1.dataGridView.Rows.Add();
                mForm1.dataGridView.Rows[rowIndex].Cells[0].Value = node.Attributes[mForm1.dataGridView.Columns[0].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[2].Value = node.Attributes[mForm1.dataGridView.Columns[2].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[3].Value = node.Attributes[mForm1.dataGridView.Columns[3].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[1].Value = "2 开关量 读写";
                rowIndex++;
            }
            foreach (XmlNode node in intReadOnlyElement.ChildNodes)
            {
                mForm1.dataGridView.Rows.Add();
                mForm1.dataGridView.Rows[rowIndex].Cells[0].Value = node.Attributes[mForm1.dataGridView.Columns[0].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[2].Value = node.Attributes[mForm1.dataGridView.Columns[2].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[3].Value = node.Attributes[mForm1.dataGridView.Columns[3].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[1].Value = "3 数值量 只读";
                rowIndex++;
            }
            foreach (XmlNode node in intReadOnlyElement.ChildNodes)
            {
                mForm1.dataGridView.Rows.Add();
                mForm1.dataGridView.Rows[rowIndex].Cells[0].Value = node.Attributes[mForm1.dataGridView.Columns[0].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[2].Value = node.Attributes[mForm1.dataGridView.Columns[2].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[3].Value = node.Attributes[mForm1.dataGridView.Columns[3].Name].Value;
                mForm1.dataGridView.Rows[rowIndex].Cells[1].Value = "4 数值量 读写";
                rowIndex++;
            }
            #endregion

            #region Load controls
            try
            {
                for (int labelIndex = 1; labelIndex <= Convert.ToInt32(UIElement["Labels"].GetAttribute("Count")); labelIndex++)
                {
                    XmlElement tempLabelElement = UIElement["Labels"]["Label" + "_" + labelIndex.ToString()];

                    string TempLabelID = tempLabelElement.GetAttribute("ID");
                    string TempLabelText = tempLabelElement.GetAttribute("Text");
                    int TempPosX = int.Parse(tempLabelElement.GetAttribute("PosX"));
                    int TempPosY = int.Parse(tempLabelElement.GetAttribute("PosY"));
                    int TempWidth = int.Parse(tempLabelElement.GetAttribute("Width"));
                    int TempHeight = int.Parse(tempLabelElement.GetAttribute("Height"));

                    Label lbl = new Label();
                    lbl.Location = new Point(TempPosX, TempPosY);
                    lbl.Name = TempLabelID;
                    lbl.Text = TempLabelText;
                    lbl.Width = TempWidth;
                    lbl.Height = TempHeight;
                    lbl.BackColor = Color.Transparent;
                    lbl.TextAlign = ContentAlignment.MiddleRight;
                    lbl.DoubleClick += new EventHandler(mForm1.lbl_DoubleClick);
                    lbl.MouseDown += new MouseEventHandler(mForm1.lbl_MouseDown);
                    lbl.MouseMove += new MouseEventHandler(mForm1.lbl_MouseMove);
                    mForm1.tabControl.TabPages[1].Controls.Add(lbl);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error in loading labels.", "Error");
            }

            try
            {
                for (int textIndex = 1; textIndex <= Convert.ToInt32(UIElement["TextBoxes"].GetAttribute("Count")); textIndex++)
                {
                    XmlElement tempTextElement = UIElement["TextBoxes"]["TextBox" + "_" + textIndex.ToString()];

                    string TempTextBoxID = tempTextElement.GetAttribute("ID");
                    int TempPosX = int.Parse(tempTextElement.GetAttribute("PosX"));
                    int TempPosY = int.Parse(tempTextElement.GetAttribute("PosY"));
                    int TempWidth = int.Parse(tempTextElement.GetAttribute("Width"));
                    int TempHeight = int.Parse(tempTextElement.GetAttribute("Height"));
                    int tempTextBoxSlaveAddress = Convert.ToInt32(tempTextElement.GetAttribute("SlaveAddress"));  //从站地址
                    int TempTextBoxRelateVar = Convert.ToInt32(tempTextElement.GetAttribute("RelateVar"));        //变量地址
                    int TempTextBoxMBInterface = Convert.ToInt32(tempTextElement.GetAttribute("Interface"));      //变量通道

                    TextboxEX txt = new TextboxEX();
                    txt.Location = new Point(TempPosX, TempPosY);
                    txt.Name = TempTextBoxID;
                    txt.Width = TempWidth;
                    txt.Height = TempHeight;
                    txt.TextAlign = HorizontalAlignment.Right;
                    txt.ReadOnly = true;
                    txt.BackColor = Color.White;
                    txt.SlaveAddress = tempTextBoxSlaveAddress;
                    txt.RelateVar = TempTextBoxRelateVar;

                    switch (TempTextBoxMBInterface)
                    {
                        case 3:
                            txt.MbInterface = DataInterface.InputRegister;
                            foreach (XmlNode node in intReadOnlyElement)
                            {
                                if (node.Attributes["varAddress"].Value == txt.RelateVar.ToString())
                                {
                                    txt.VarName = node.Attributes["varName"].Value;
                                }
                            }
                            break;
                        case 4:
                            txt.MbInterface = DataInterface.HoldingRegister;
                            foreach (XmlNode node in intReadWriteElement)
                            {
                                if (node.Attributes["varAddress"].Value == txt.RelateVar.ToString())
                                {
                                    txt.VarName = node.Attributes["varName"].Value;
                                }
                            }
                            txt.Cursor = Cursors.Hand;
                            break;
                        default:
                            break;
                    }

                    txt.DoubleClick += new EventHandler(mForm1.Text_DoubleClick);
                    txt.MouseDown += new MouseEventHandler(mForm1.Text_MouseDown);
                    txt.MouseMove += new MouseEventHandler(mForm1.Text_MouseMove);
                    mForm1.tabControl.TabPages[1].Controls.Add(txt);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error in loading textbox");
            }


            try
            {
                for (int lampIndex = 1; lampIndex <= Convert.ToInt32(UIElement["Lamps"].GetAttribute("Count")); lampIndex++)
                {
                    XmlElement tempLampElement = UIElement["Lamps"]["Lamp" + "_" + lampIndex.ToString()];

                    string tempLampID = tempLampElement.GetAttribute("ID");
                    int tempPosX = int.Parse(tempLampElement.GetAttribute("PosX"));
                    int tempPosY = int.Parse(tempLampElement.GetAttribute("PosY"));
                    int tempLampVar = int.Parse(tempLampElement.GetAttribute("RelateVar"));
                    int tempSlaveAddress = int.Parse(tempLampElement.GetAttribute("SlaveAddress"));
                    int tempInterface = int.Parse(tempLampElement.GetAttribute("Interface"));

                    Lamp lamp1 = new Lamp();
                    lamp1.Location = new Point(tempPosX, tempPosY);
                    lamp1.Name = tempLampID;
                    lamp1.RelateVar = tempLampVar;
                    lamp1.SlaveAddress = tempSlaveAddress;
                    if (tempInterface == 1)
                    {
                        lamp1.ReadOnly = true;
                        foreach (XmlNode node in boolReadOnlyElement)
                        {
                            if (node.Attributes["varAddress"].Value == lamp1.RelateVar.ToString())
                            {
                                lamp1.varName = node.Attributes["varName"].Value;
                            }
                        }
                    }
                    else
                    {
                        lamp1.ReadOnly = false;
                        lamp1.Cursor = Cursors.Hand;
                        foreach (XmlNode node in boolReadWriteElement)
                        {
                            if (node.Attributes["varAddress"].Value == lamp1.RelateVar.ToString())
                            {
                                lamp1.varName = node.Attributes["varName"].Value;
                            }
                        }
                    }

                    lamp1.DoubleClick += new EventHandler(mForm1.Lamp_DoubleClick);
                    lamp1.MouseDown += new MouseEventHandler(mForm1.Lamp_MouseDown);
                    lamp1.MouseMove += new MouseEventHandler(mForm1.Lamp_MouseMove);
                    mForm1.tabControl.TabPages[1].Controls.Add(lamp1);
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Error in load lamps");
            }

            #endregion 对第二页进行加载
        }
    }
}
