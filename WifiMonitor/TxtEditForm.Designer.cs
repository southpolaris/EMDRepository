namespace WifiMonitor
{
    partial class TxtEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TxtEditForm));
            this.btnTxtSav = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageVar = new System.Windows.Forms.TabPage();
            this.cbSlaveAddress = new System.Windows.Forms.ComboBox();
            this.textBoxVarName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxRelateVar = new System.Windows.Forms.TextBox();
            this.checkBoxDB = new System.Windows.Forms.CheckBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTxtVar = new System.Windows.Forms.Label();
            this.tabPageCtrl = new System.Windows.Forms.TabPage();
            this.lblPosY = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.lblPosX = new System.Windows.Forms.Label();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageVar.SuspendLayout();
            this.tabPageCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTxtSav
            // 
            this.btnTxtSav.Location = new System.Drawing.Point(112, 263);
            this.btnTxtSav.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTxtSav.Name = "btnTxtSav";
            this.btnTxtSav.Size = new System.Drawing.Size(87, 33);
            this.btnTxtSav.TabIndex = 2;
            this.btnTxtSav.Text = "确认";
            this.btnTxtSav.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageVar);
            this.tabControl1.Controls.Add(this.tabPageCtrl);
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(294, 250);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPageVar
            // 
            this.tabPageVar.Controls.Add(this.cbSlaveAddress);
            this.tabPageVar.Controls.Add(this.textBoxVarName);
            this.tabPageVar.Controls.Add(this.label3);
            this.tabPageVar.Controls.Add(this.textBoxRelateVar);
            this.tabPageVar.Controls.Add(this.checkBoxDB);
            this.tabPageVar.Controls.Add(this.radioButton4);
            this.tabPageVar.Controls.Add(this.radioButton3);
            this.tabPageVar.Controls.Add(this.label2);
            this.tabPageVar.Controls.Add(this.label1);
            this.tabPageVar.Controls.Add(this.lblTxtVar);
            this.tabPageVar.Location = new System.Drawing.Point(4, 26);
            this.tabPageVar.Name = "tabPageVar";
            this.tabPageVar.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVar.Size = new System.Drawing.Size(286, 220);
            this.tabPageVar.TabIndex = 0;
            this.tabPageVar.Text = "变量属性";
            this.tabPageVar.UseVisualStyleBackColor = true;
            // 
            // cbSlaveAddress
            // 
            this.cbSlaveAddress.FormattingEnabled = true;
            this.cbSlaveAddress.Location = new System.Drawing.Point(129, 57);
            this.cbSlaveAddress.Name = "cbSlaveAddress";
            this.cbSlaveAddress.Size = new System.Drawing.Size(140, 25);
            this.cbSlaveAddress.TabIndex = 29;
            // 
            // textBoxVarName
            // 
            this.textBoxVarName.Location = new System.Drawing.Point(129, 22);
            this.textBoxVarName.Name = "textBoxVarName";
            this.textBoxVarName.Size = new System.Drawing.Size(140, 23);
            this.textBoxVarName.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 27;
            this.label3.Text = "关联变量名称：";
            // 
            // textBoxRelateVar
            // 
            this.textBoxRelateVar.Location = new System.Drawing.Point(129, 125);
            this.textBoxRelateVar.Name = "textBoxRelateVar";
            this.textBoxRelateVar.Size = new System.Drawing.Size(140, 23);
            this.textBoxRelateVar.TabIndex = 26;
            // 
            // checkBoxDB
            // 
            this.checkBoxDB.AutoSize = true;
            this.checkBoxDB.Location = new System.Drawing.Point(129, 162);
            this.checkBoxDB.Name = "checkBoxDB";
            this.checkBoxDB.Size = new System.Drawing.Size(111, 21);
            this.checkBoxDB.TabIndex = 25;
            this.checkBoxDB.Text = "信息存入数据库";
            this.checkBoxDB.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(207, 93);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(62, 21);
            this.radioButton4.TabIndex = 22;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "4-读写";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(129, 93);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(62, 21);
            this.radioButton3.TabIndex = 21;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3-只读";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "设备通信模块IP：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "数据通道：";
            // 
            // lblTxtVar
            // 
            this.lblTxtVar.AutoSize = true;
            this.lblTxtVar.Location = new System.Drawing.Point(12, 128);
            this.lblTxtVar.Name = "lblTxtVar";
            this.lblTxtVar.Size = new System.Drawing.Size(123, 17);
            this.lblTxtVar.TabIndex = 19;
            this.lblTxtVar.Text = "数据地址（0起始）：";
            // 
            // tabPageCtrl
            // 
            this.tabPageCtrl.Controls.Add(this.lblPosY);
            this.tabPageCtrl.Controls.Add(this.txtPosY);
            this.tabPageCtrl.Controls.Add(this.lblPosX);
            this.tabPageCtrl.Controls.Add(this.txtPosX);
            this.tabPageCtrl.Controls.Add(this.lblHeight);
            this.tabPageCtrl.Controls.Add(this.txtHeight);
            this.tabPageCtrl.Controls.Add(this.lblWidth);
            this.tabPageCtrl.Controls.Add(this.txtWidth);
            this.tabPageCtrl.Location = new System.Drawing.Point(4, 26);
            this.tabPageCtrl.Name = "tabPageCtrl";
            this.tabPageCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCtrl.Size = new System.Drawing.Size(286, 220);
            this.tabPageCtrl.TabIndex = 1;
            this.tabPageCtrl.Text = "控件属性";
            this.tabPageCtrl.UseVisualStyleBackColor = true;
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Location = new System.Drawing.Point(12, 128);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(87, 17);
            this.lblPosY.TabIndex = 26;
            this.lblPosY.Text = "文本框坐标Y：";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(129, 125);
            this.txtPosY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(139, 23);
            this.txtPosY.TabIndex = 25;
            this.txtPosY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Location = new System.Drawing.Point(12, 94);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(88, 17);
            this.lblPosX.TabIndex = 24;
            this.lblPosX.Text = "文本框坐标X：";
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(129, 91);
            this.txtPosX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(139, 23);
            this.txtPosX.TabIndex = 23;
            this.txtPosX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(12, 60);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(80, 17);
            this.lblHeight.TabIndex = 22;
            this.lblHeight.Text = "文本框高度：";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(129, 57);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(139, 23);
            this.txtHeight.TabIndex = 21;
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(12, 23);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(80, 17);
            this.lblWidth.TabIndex = 20;
            this.lblWidth.Text = "文本框宽度：";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(129, 22);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(139, 23);
            this.txtWidth.TabIndex = 19;
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtEditForm
            // 
            this.AcceptButton = this.btnTxtSav;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 301);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnTxtSav);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TxtEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数值量选项";
            this.tabControl1.ResumeLayout(false);
            this.tabPageVar.ResumeLayout(false);
            this.tabPageVar.PerformLayout();
            this.tabPageCtrl.ResumeLayout(false);
            this.tabPageCtrl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnTxtSav;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageVar;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.RadioButton radioButton4;
        public System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTxtVar;
        private System.Windows.Forms.TabPage tabPageCtrl;
        private System.Windows.Forms.Label lblPosY;
        private System.Windows.Forms.Label lblPosX;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        internal System.Windows.Forms.TextBox textBoxVarName;
        internal System.Windows.Forms.TextBox textBoxRelateVar;
        internal System.Windows.Forms.CheckBox checkBoxDB;
        internal System.Windows.Forms.TextBox txtPosY;
        internal System.Windows.Forms.TextBox txtPosX;
        internal System.Windows.Forms.TextBox txtHeight;
        internal System.Windows.Forms.TextBox txtWidth;
        internal System.Windows.Forms.ComboBox cbSlaveAddress;
    }
}