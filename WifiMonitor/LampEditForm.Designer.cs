namespace WifiMonitor
{
    partial class LampEditForm
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageVar = new System.Windows.Forms.TabPage();
            this.textBoxDataSave = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSlaveAddress = new System.Windows.Forms.ComboBox();
            this.textBoxVarName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxLampVar = new System.Windows.Forms.TextBox();
            this.radioButton0 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageCtrl = new System.Windows.Forms.TabPage();
            this.tbLampY = new System.Windows.Forms.TextBox();
            this.tbLampX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageVar.SuspendLayout();
            this.tabPageCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(110, 261);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(87, 32);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageVar);
            this.tabControl1.Controls.Add(this.tabPageCtrl);
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(294, 250);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPageVar
            // 
            this.tabPageVar.Controls.Add(this.textBoxDataSave);
            this.tabPageVar.Controls.Add(this.label7);
            this.tabPageVar.Controls.Add(this.cbSlaveAddress);
            this.tabPageVar.Controls.Add(this.textBoxVarName);
            this.tabPageVar.Controls.Add(this.label6);
            this.tabPageVar.Controls.Add(this.textBoxLampVar);
            this.tabPageVar.Controls.Add(this.radioButton0);
            this.tabPageVar.Controls.Add(this.radioButton1);
            this.tabPageVar.Controls.Add(this.label5);
            this.tabPageVar.Controls.Add(this.label4);
            this.tabPageVar.Controls.Add(this.label1);
            this.tabPageVar.Location = new System.Drawing.Point(4, 26);
            this.tabPageVar.Name = "tabPageVar";
            this.tabPageVar.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVar.Size = new System.Drawing.Size(286, 220);
            this.tabPageVar.TabIndex = 0;
            this.tabPageVar.Text = "变量属性";
            this.tabPageVar.UseVisualStyleBackColor = true;
            // 
            // textBoxDataSave
            // 
            this.textBoxDataSave.Location = new System.Drawing.Point(130, 163);
            this.textBoxDataSave.Name = "textBoxDataSave";
            this.textBoxDataSave.ReadOnly = true;
            this.textBoxDataSave.Size = new System.Drawing.Size(138, 23);
            this.textBoxDataSave.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 17);
            this.label7.TabIndex = 31;
            this.label7.Text = "数据保存形式";
            // 
            // cbSlaveAddress
            // 
            this.cbSlaveAddress.FormattingEnabled = true;
            this.cbSlaveAddress.Location = new System.Drawing.Point(130, 53);
            this.cbSlaveAddress.Name = "cbSlaveAddress";
            this.cbSlaveAddress.Size = new System.Drawing.Size(138, 25);
            this.cbSlaveAddress.TabIndex = 30;
            // 
            // textBoxVarName
            // 
            this.textBoxVarName.Location = new System.Drawing.Point(130, 13);
            this.textBoxVarName.Name = "textBoxVarName";
            this.textBoxVarName.ReadOnly = true;
            this.textBoxVarName.Size = new System.Drawing.Size(138, 23);
            this.textBoxVarName.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "关联变量名称：";
            // 
            // textBoxLampVar
            // 
            this.textBoxLampVar.Location = new System.Drawing.Point(130, 124);
            this.textBoxLampVar.Name = "textBoxLampVar";
            this.textBoxLampVar.Size = new System.Drawing.Size(138, 23);
            this.textBoxLampVar.TabIndex = 27;
            // 
            // radioButton0
            // 
            this.radioButton0.AutoSize = true;
            this.radioButton0.Location = new System.Drawing.Point(206, 90);
            this.radioButton0.Name = "radioButton0";
            this.radioButton0.Size = new System.Drawing.Size(62, 21);
            this.radioButton0.TabIndex = 25;
            this.radioButton0.TabStop = true;
            this.radioButton0.Text = "0-读写";
            this.radioButton0.UseVisualStyleBackColor = true;
            this.radioButton0.CheckedChanged += new System.EventHandler(this.radioButton0_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(130, 90);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 21);
            this.radioButton1.TabIndex = 24;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1-只读";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "数据通道：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "设备通信模块ID：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "数据地址（0起始）：";
            // 
            // tabPageCtrl
            // 
            this.tabPageCtrl.Controls.Add(this.tbLampY);
            this.tabPageCtrl.Controls.Add(this.tbLampX);
            this.tabPageCtrl.Controls.Add(this.label3);
            this.tabPageCtrl.Controls.Add(this.label2);
            this.tabPageCtrl.Location = new System.Drawing.Point(4, 26);
            this.tabPageCtrl.Name = "tabPageCtrl";
            this.tabPageCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCtrl.Size = new System.Drawing.Size(286, 220);
            this.tabPageCtrl.TabIndex = 1;
            this.tabPageCtrl.Text = "控件属性";
            this.tabPageCtrl.UseVisualStyleBackColor = true;
            // 
            // tbLampY
            // 
            this.tbLampY.Location = new System.Drawing.Point(130, 78);
            this.tbLampY.Name = "tbLampY";
            this.tbLampY.Size = new System.Drawing.Size(138, 23);
            this.tbLampY.TabIndex = 10;
            // 
            // tbLampX
            // 
            this.tbLampX.Location = new System.Drawing.Point(130, 41);
            this.tbLampX.Name = "tbLampX";
            this.tbLampX.Size = new System.Drawing.Size(138, 23);
            this.tbLampX.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "控件位置Y：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "控件位置X：";
            // 
            // LampEditForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 301);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LampEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开关量选项";
            this.tabControl1.ResumeLayout(false);
            this.tabPageVar.ResumeLayout(false);
            this.tabPageVar.PerformLayout();
            this.tabPageCtrl.ResumeLayout(false);
            this.tabPageCtrl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageVar;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.RadioButton radioButton0;
        public System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageCtrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox textBoxVarName;
        internal System.Windows.Forms.TextBox textBoxLampVar;
        internal System.Windows.Forms.TextBox tbLampY;
        internal System.Windows.Forms.TextBox tbLampX;
        internal System.Windows.Forms.ComboBox cbSlaveAddress;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox textBoxDataSave;
    }
}