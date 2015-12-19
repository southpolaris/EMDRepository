namespace XMLGenerator
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
            this.radioButtonReadWrite = new System.Windows.Forms.RadioButton();
            this.radioBtnReadOnly = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSlaveAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbLampY = new System.Windows.Forms.TextBox();
            this.tbLampX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLampVar = new System.Windows.Forms.ComboBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButtonReadWrite
            // 
            this.radioButtonReadWrite.AutoSize = true;
            this.radioButtonReadWrite.Location = new System.Drawing.Point(194, 53);
            this.radioButtonReadWrite.Name = "radioButtonReadWrite";
            this.radioButtonReadWrite.Size = new System.Drawing.Size(59, 16);
            this.radioButtonReadWrite.TabIndex = 23;
            this.radioButtonReadWrite.TabStop = true;
            this.radioButtonReadWrite.Text = "0-读写";
            this.radioButtonReadWrite.UseVisualStyleBackColor = true;
            this.radioButtonReadWrite.CheckedChanged += new System.EventHandler(this.radioButton0_CheckedChanged);
            // 
            // radioBtnReadOnly
            // 
            this.radioBtnReadOnly.AutoSize = true;
            this.radioBtnReadOnly.Location = new System.Drawing.Point(118, 53);
            this.radioBtnReadOnly.Name = "radioBtnReadOnly";
            this.radioBtnReadOnly.Size = new System.Drawing.Size(59, 16);
            this.radioBtnReadOnly.TabIndex = 22;
            this.radioBtnReadOnly.TabStop = true;
            this.radioBtnReadOnly.Text = "1-只读";
            this.radioBtnReadOnly.UseVisualStyleBackColor = true;
            this.radioBtnReadOnly.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "数据通道：";
            // 
            // textBoxSlaveAddress
            // 
            this.textBoxSlaveAddress.Enabled = false;
            this.textBoxSlaveAddress.Location = new System.Drawing.Point(118, 16);
            this.textBoxSlaveAddress.Name = "textBoxSlaveAddress";
            this.textBoxSlaveAddress.Size = new System.Drawing.Size(138, 21);
            this.textBoxSlaveAddress.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "关联设备ID：";
            // 
            // tbLampY
            // 
            this.tbLampY.Location = new System.Drawing.Point(118, 196);
            this.tbLampY.Name = "tbLampY";
            this.tbLampY.Size = new System.Drawing.Size(138, 21);
            this.tbLampY.TabIndex = 18;
            // 
            // tbLampX
            // 
            this.tbLampX.Location = new System.Drawing.Point(118, 159);
            this.tbLampX.Name = "tbLampX";
            this.tbLampX.Size = new System.Drawing.Size(138, 21);
            this.tbLampX.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "控件位置Y：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "控件位置X：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "变量地址：";
            // 
            // cbLampVar
            // 
            this.cbLampVar.DisplayMember = "Key";
            this.cbLampVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLampVar.FormattingEnabled = true;
            this.cbLampVar.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cbLampVar.Location = new System.Drawing.Point(118, 87);
            this.cbLampVar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbLampVar.Name = "cbLampVar";
            this.cbLampVar.Size = new System.Drawing.Size(138, 20);
            this.cbLampVar.TabIndex = 13;
            this.cbLampVar.ValueMember = "Value";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(169, 237);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(87, 32);
            this.buttonOk.TabIndex = 12;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // LampEditForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 283);
            this.Controls.Add(this.radioButtonReadWrite);
            this.Controls.Add(this.radioBtnReadOnly);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxSlaveAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbLampY);
            this.Controls.Add(this.tbLampX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLampVar);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LampEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开关量选项";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RadioButton radioButtonReadWrite;
        public System.Windows.Forms.RadioButton radioBtnReadOnly;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBoxSlaveAddress;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox tbLampY;
        public System.Windows.Forms.TextBox tbLampX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbLampVar;
        public System.Windows.Forms.Button buttonOk;
    }
}