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
            this.lblTxtVar = new System.Windows.Forms.Label();
            this.btnTxtSav = new System.Windows.Forms.Button();
            this.lblWidth = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblPosY = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.lblPosX = new System.Windows.Forms.Label();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.cbbTxtVar = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSlaveAddress = new System.Windows.Forms.TextBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTxtVar
            // 
            this.lblTxtVar.AutoSize = true;
            this.lblTxtVar.Location = new System.Drawing.Point(303, 28);
            this.lblTxtVar.Name = "lblTxtVar";
            this.lblTxtVar.Size = new System.Drawing.Size(68, 17);
            this.lblTxtVar.TabIndex = 1;
            this.lblTxtVar.Text = "通道地址：";
            // 
            // btnTxtSav
            // 
            this.btnTxtSav.Location = new System.Drawing.Point(238, 210);
            this.btnTxtSav.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTxtSav.Name = "btnTxtSav";
            this.btnTxtSav.Size = new System.Drawing.Size(87, 33);
            this.btnTxtSav.TabIndex = 2;
            this.btnTxtSav.Text = "确认";
            this.btnTxtSav.UseVisualStyleBackColor = true;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(9, 25);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(80, 17);
            this.lblWidth.TabIndex = 4;
            this.lblWidth.Text = "文本框宽度：";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(111, 22);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(139, 23);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(9, 56);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(80, 17);
            this.lblHeight.TabIndex = 6;
            this.lblHeight.Text = "文本框高度：";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(111, 53);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(139, 23);
            this.txtHeight.TabIndex = 5;
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Location = new System.Drawing.Point(284, 56);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(87, 17);
            this.lblPosY.TabIndex = 10;
            this.lblPosY.Text = "文本框坐标Y：";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(395, 53);
            this.txtPosY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(139, 23);
            this.txtPosY.TabIndex = 9;
            this.txtPosY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Location = new System.Drawing.Point(284, 25);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(88, 17);
            this.lblPosX.TabIndex = 8;
            this.lblPosX.Text = "文本框坐标X：";
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(395, 22);
            this.txtPosX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(139, 23);
            this.txtPosX.TabIndex = 7;
            this.txtPosX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbbTxtVar
            // 
            this.cbbTxtVar.DisplayMember = "Key";
            this.cbbTxtVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTxtVar.FormattingEnabled = true;
            this.cbbTxtVar.Items.AddRange(new object[] {
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
            this.cbbTxtVar.Location = new System.Drawing.Point(395, 25);
            this.cbbTxtVar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbbTxtVar.Name = "cbbTxtVar";
            this.cbbTxtVar.Size = new System.Drawing.Size(139, 25);
            this.cbbTxtVar.TabIndex = 11;
            this.cbbTxtVar.ValueMember = "Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "通道类型：";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(111, 60);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(62, 21);
            this.radioButton3.TabIndex = 13;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3-只读";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPosY);
            this.groupBox1.Controls.Add(this.txtPosY);
            this.groupBox1.Controls.Add(this.lblPosX);
            this.groupBox1.Controls.Add(this.txtPosX);
            this.groupBox1.Controls.Add(this.lblHeight);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.lblWidth);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Location = new System.Drawing.Point(9, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 90);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控件属性";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSlaveAddress);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblTxtVar);
            this.groupBox2.Controls.Add(this.cbbTxtVar);
            this.groupBox2.Location = new System.Drawing.Point(9, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(548, 94);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "关联变量";
            // 
            // txtSlaveAddress
            // 
            this.txtSlaveAddress.Location = new System.Drawing.Point(111, 25);
            this.txtSlaveAddress.Name = "txtSlaveAddress";
            this.txtSlaveAddress.Size = new System.Drawing.Size(139, 23);
            this.txtSlaveAddress.TabIndex = 16;
            this.txtSlaveAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(189, 60);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(62, 21);
            this.radioButton4.TabIndex = 14;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "4-读写";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "关联设备ID：";
            // 
            // TxtEditForm
            // 
            this.AcceptButton = this.btnTxtSav;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 249);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTxtVar;
        public System.Windows.Forms.Button btnTxtSav;
        private System.Windows.Forms.Label lblWidth;
        public System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lblHeight;
        public System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblPosY;
        public System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.Label lblPosX;
        public System.Windows.Forms.TextBox txtPosX;
        public System.Windows.Forms.ComboBox cbbTxtVar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.RadioButton radioButton3;
        public System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtSlaveAddress;
    }
}