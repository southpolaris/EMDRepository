namespace XMLGenerator
{
    partial class TextEditForm
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
            this.txtVarName = new System.Windows.Forms.TextBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTxtVar = new System.Windows.Forms.Label();
            this.cbbTxtVar = new System.Windows.Forms.ComboBox();
            this.lblPosY = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.lblPosX = new System.Windows.Forms.Label();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.btnTxtSav = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtVarName
            // 
            this.txtVarName.Location = new System.Drawing.Point(115, 139);
            this.txtVarName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVarName.Name = "txtVarName";
            this.txtVarName.ReadOnly = true;
            this.txtVarName.Size = new System.Drawing.Size(161, 23);
            this.txtVarName.TabIndex = 16;
            this.txtVarName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(205, 44);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(62, 21);
            this.radioButton4.TabIndex = 14;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "4-读写";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(126, 44);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(62, 21);
            this.radioButton3.TabIndex = 13;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3-只读";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "变量名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "数据通道：";
            // 
            // lblTxtVar
            // 
            this.lblTxtVar.AutoSize = true;
            this.lblTxtVar.Location = new System.Drawing.Point(21, 92);
            this.lblTxtVar.Name = "lblTxtVar";
            this.lblTxtVar.Size = new System.Drawing.Size(68, 17);
            this.lblTxtVar.TabIndex = 1;
            this.lblTxtVar.Text = "变量地址：";
            // 
            // cbbTxtVar
            // 
            this.cbbTxtVar.DisplayMember = "Key";
            this.cbbTxtVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTxtVar.FormattingEnabled = true;
            this.cbbTxtVar.Location = new System.Drawing.Point(115, 87);
            this.cbbTxtVar.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbbTxtVar.Name = "cbbTxtVar";
            this.cbbTxtVar.Size = new System.Drawing.Size(161, 25);
            this.cbbTxtVar.TabIndex = 11;
            this.cbbTxtVar.ValueMember = "Value";
            this.cbbTxtVar.SelectedIndexChanged += new System.EventHandler(this.cbbTxtVar_SelectedIndexChanged);
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Location = new System.Drawing.Point(13, 172);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(87, 17);
            this.lblPosY.TabIndex = 10;
            this.lblPosY.Text = "文本框坐标Y：";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(115, 169);
            this.txtPosY.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(161, 23);
            this.txtPosY.TabIndex = 9;
            this.txtPosY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Location = new System.Drawing.Point(13, 128);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(88, 17);
            this.lblPosX.TabIndex = 8;
            this.lblPosX.Text = "文本框坐标X：";
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(115, 125);
            this.txtPosX.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(161, 23);
            this.txtPosX.TabIndex = 7;
            this.txtPosX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(13, 75);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(80, 17);
            this.lblHeight.TabIndex = 6;
            this.lblHeight.Text = "文本框高度：";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(115, 71);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(161, 23);
            this.txtHeight.TabIndex = 5;
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(13, 31);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(80, 17);
            this.lblWidth.TabIndex = 4;
            this.lblWidth.Text = "文本框宽度：";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(115, 27);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(161, 23);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnTxtSav
            // 
            this.btnTxtSav.Location = new System.Drawing.Point(105, 294);
            this.btnTxtSav.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btnTxtSav.Name = "btnTxtSav";
            this.btnTxtSav.Size = new System.Drawing.Size(101, 32);
            this.btnTxtSav.TabIndex = 18;
            this.btnTxtSav.Text = "确认";
            this.btnTxtSav.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(304, 274);
            this.tabControl.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtVarName);
            this.tabPage1.Controls.Add(this.lblTxtVar);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.radioButton4);
            this.tabPage1.Controls.Add(this.cbbTxtVar);
            this.tabPage1.Controls.Add(this.radioButton3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(296, 244);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "变量属性";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblPosY);
            this.tabPage2.Controls.Add(this.txtWidth);
            this.tabPage2.Controls.Add(this.txtPosY);
            this.tabPage2.Controls.Add(this.lblWidth);
            this.tabPage2.Controls.Add(this.lblPosX);
            this.tabPage2.Controls.Add(this.txtHeight);
            this.tabPage2.Controls.Add(this.txtPosX);
            this.tabPage2.Controls.Add(this.lblHeight);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(296, 244);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "控件属性";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TextEditForm
            // 
            this.AcceptButton = this.btnTxtSav;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 341);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnTxtSav);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数值量显示选项";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox txtVarName;
        public System.Windows.Forms.RadioButton radioButton4;
        public System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTxtVar;
        public System.Windows.Forms.ComboBox cbbTxtVar;
        private System.Windows.Forms.Label lblPosY;
        public System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.Label lblPosX;
        public System.Windows.Forms.TextBox txtPosX;
        private System.Windows.Forms.Label lblHeight;
        public System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblWidth;
        public System.Windows.Forms.TextBox txtWidth;
        public System.Windows.Forms.Button btnTxtSav;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}