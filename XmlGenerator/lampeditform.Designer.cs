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
            this.textBoxVarName = new System.Windows.Forms.TextBox();
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
            this.radioButtonReadWrite.Location = new System.Drawing.Point(226, 34);
            this.radioButtonReadWrite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonReadWrite.Name = "radioButtonReadWrite";
            this.radioButtonReadWrite.Size = new System.Drawing.Size(62, 21);
            this.radioButtonReadWrite.TabIndex = 23;
            this.radioButtonReadWrite.TabStop = true;
            this.radioButtonReadWrite.Text = "0-读写";
            this.radioButtonReadWrite.UseVisualStyleBackColor = true;
            this.radioButtonReadWrite.CheckedChanged += new System.EventHandler(this.radioButton0_CheckedChanged);
            // 
            // radioBtnReadOnly
            // 
            this.radioBtnReadOnly.AutoSize = true;
            this.radioBtnReadOnly.Location = new System.Drawing.Point(138, 34);
            this.radioBtnReadOnly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioBtnReadOnly.Name = "radioBtnReadOnly";
            this.radioBtnReadOnly.Size = new System.Drawing.Size(62, 21);
            this.radioBtnReadOnly.TabIndex = 22;
            this.radioBtnReadOnly.TabStop = true;
            this.radioBtnReadOnly.Text = "1-只读";
            this.radioBtnReadOnly.UseVisualStyleBackColor = true;
            this.radioBtnReadOnly.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "数据通道：";
            // 
            // textBoxVarName
            // 
            this.textBoxVarName.Enabled = false;
            this.textBoxVarName.Location = new System.Drawing.Point(138, 120);
            this.textBoxVarName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxVarName.Name = "textBoxVarName";
            this.textBoxVarName.ReadOnly = true;
            this.textBoxVarName.Size = new System.Drawing.Size(160, 23);
            this.textBoxVarName.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "变量名称：";
            // 
            // tbLampY
            // 
            this.tbLampY.Location = new System.Drawing.Point(138, 233);
            this.tbLampY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbLampY.Name = "tbLampY";
            this.tbLampY.Size = new System.Drawing.Size(160, 23);
            this.tbLampY.TabIndex = 18;
            // 
            // tbLampX
            // 
            this.tbLampX.Location = new System.Drawing.Point(138, 189);
            this.tbLampX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbLampX.Name = "tbLampX";
            this.tbLampX.Size = new System.Drawing.Size(160, 23);
            this.tbLampX.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "控件位置Y：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "控件位置X：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
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
            this.cbLampVar.Location = new System.Drawing.Point(138, 74);
            this.cbLampVar.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbLampVar.Name = "cbLampVar";
            this.cbLampVar.Size = new System.Drawing.Size(160, 25);
            this.cbLampVar.TabIndex = 13;
            this.cbLampVar.ValueMember = "Value";
            this.cbLampVar.SelectedIndexChanged += new System.EventHandler(this.cbLampVar_SelectedIndexChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(119, 286);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(101, 33);
            this.buttonOk.TabIndex = 12;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // LampEditForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 341);
            this.Controls.Add(this.radioButtonReadWrite);
            this.Controls.Add(this.radioBtnReadOnly);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxVarName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbLampY);
            this.Controls.Add(this.tbLampX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLampVar);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        public System.Windows.Forms.TextBox textBoxVarName;
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