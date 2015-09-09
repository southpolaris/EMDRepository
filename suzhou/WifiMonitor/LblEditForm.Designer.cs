namespace WifiMonitor
{
    partial class LblEditForm
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
            this.lblWidth = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.colorBoxLblTxt = new System.Windows.Forms.ColorDialog();
            this.lblPosY = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.lblPosX = new System.Windows.Forms.Label();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnLblSav = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(22, 72);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(68, 17);
            this.lblWidth.TabIndex = 4;
            this.lblWidth.Text = "标签宽度：";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(106, 68);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(134, 23);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(22, 110);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(68, 17);
            this.lblHeight.TabIndex = 6;
            this.lblHeight.Text = "标签高度：";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(106, 106);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(134, 23);
            this.txtHeight.TabIndex = 5;
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Location = new System.Drawing.Point(22, 190);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(75, 17);
            this.lblPosY.TabIndex = 10;
            this.lblPosY.Text = "标签坐标Y：";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(106, 186);
            this.txtPosY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(134, 23);
            this.txtPosY.TabIndex = 9;
            this.txtPosY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Location = new System.Drawing.Point(22, 152);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(76, 17);
            this.lblPosX.TabIndex = 8;
            this.lblPosX.Text = "标签坐标X：";
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(106, 147);
            this.txtPosX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(134, 23);
            this.txtPosX.TabIndex = 7;
            this.txtPosX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(22, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 17);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "标签名：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(106, 29);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(134, 23);
            this.txtName.TabIndex = 11;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnLblSav
            // 
            this.btnLblSav.Location = new System.Drawing.Point(153, 236);
            this.btnLblSav.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLblSav.Name = "btnLblSav";
            this.btnLblSav.Size = new System.Drawing.Size(87, 33);
            this.btnLblSav.TabIndex = 13;
            this.btnLblSav.Text = "确认";
            this.btnLblSav.UseVisualStyleBackColor = true;
            // 
            // LblEditForm
            // 
            this.AcceptButton = this.btnLblSav;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 286);
            this.Controls.Add(this.btnLblSav);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPosY);
            this.Controls.Add(this.txtPosY);
            this.Controls.Add(this.lblPosX);
            this.Controls.Add(this.txtPosX);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.txtWidth);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "LblEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标签编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWidth;
        public System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lblHeight;
        public System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.ColorDialog colorBoxLblTxt;
        private System.Windows.Forms.Label lblPosY;
        public System.Windows.Forms.TextBox txtPosY;
        public System.Windows.Forms.Label lblPosX;
        public System.Windows.Forms.TextBox txtPosX;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.Button btnLblSav;
    }
}