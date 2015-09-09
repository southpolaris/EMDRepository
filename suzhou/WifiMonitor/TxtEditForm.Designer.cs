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
            this.colorBoxLblTxt = new System.Windows.Forms.ColorDialog();
            this.lblPosY = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.lblPosX = new System.Windows.Forms.Label();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.cbbTxtVar = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblTxtVar
            // 
            this.lblTxtVar.AutoSize = true;
            this.lblTxtVar.Location = new System.Drawing.Point(10, 34);
            this.lblTxtVar.Name = "lblTxtVar";
            this.lblTxtVar.Size = new System.Drawing.Size(104, 17);
            this.lblTxtVar.TabIndex = 1;
            this.lblTxtVar.Text = "文本框关联变量：";
            // 
            // btnTxtSav
            // 
            this.btnTxtSav.Location = new System.Drawing.Point(178, 230);
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
            this.lblWidth.Location = new System.Drawing.Point(10, 72);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(80, 17);
            this.lblWidth.TabIndex = 4;
            this.lblWidth.Text = "文本框宽度：";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(126, 67);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(139, 23);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(10, 110);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(80, 17);
            this.lblHeight.TabIndex = 6;
            this.lblHeight.Text = "文本框高度：";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(126, 105);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(139, 23);
            this.txtHeight.TabIndex = 5;
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Location = new System.Drawing.Point(10, 190);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(87, 17);
            this.lblPosY.TabIndex = 10;
            this.lblPosY.Text = "文本框坐标Y：";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(126, 184);
            this.txtPosY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(139, 23);
            this.txtPosY.TabIndex = 9;
            this.txtPosY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Location = new System.Drawing.Point(10, 152);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(88, 17);
            this.lblPosX.TabIndex = 8;
            this.lblPosX.Text = "文本框坐标X：";
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(126, 146);
            this.txtPosX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(139, 23);
            this.txtPosX.TabIndex = 7;
            this.txtPosX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbbTxtVar
            // 
            this.cbbTxtVar.DisplayMember = "Key";
            this.cbbTxtVar.FormattingEnabled = true;
            this.cbbTxtVar.Location = new System.Drawing.Point(126, 30);
            this.cbbTxtVar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbbTxtVar.Name = "cbbTxtVar";
            this.cbbTxtVar.Size = new System.Drawing.Size(139, 25);
            this.cbbTxtVar.TabIndex = 11;
            this.cbbTxtVar.ValueMember = "Value";
            // 
            // TxtEditForm
            // 
            this.AcceptButton = this.btnTxtSav;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 278);
            this.Controls.Add(this.cbbTxtVar);
            this.Controls.Add(this.lblPosY);
            this.Controls.Add(this.txtPosY);
            this.Controls.Add(this.lblPosX);
            this.Controls.Add(this.txtPosX);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.btnTxtSav);
            this.Controls.Add(this.lblTxtVar);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "TxtEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文本框编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTxtVar;
        public System.Windows.Forms.Button btnTxtSav;
        private System.Windows.Forms.Label lblWidth;
        public System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lblHeight;
        public System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.ColorDialog colorBoxLblTxt;
        private System.Windows.Forms.Label lblPosY;
        public System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.Label lblPosX;
        public System.Windows.Forms.TextBox txtPosX;
        public System.Windows.Forms.ComboBox cbbTxtVar;
    }
}