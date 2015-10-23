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
            this.cbLampVar = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLampX = new System.Windows.Forms.TextBox();
            this.tbLampY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(153, 195);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(87, 32);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // cbLampVar
            // 
            this.cbLampVar.DisplayMember = "Key";
            this.cbLampVar.FormattingEnabled = true;
            this.cbLampVar.Location = new System.Drawing.Point(102, 53);
            this.cbLampVar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbLampVar.Name = "cbLampVar";
            this.cbLampVar.Size = new System.Drawing.Size(138, 25);
            this.cbLampVar.TabIndex = 1;
            this.cbLampVar.ValueMember = "Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "关联变量地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "指示灯位置X：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "指示灯位置Y：";
            // 
            // tbLampX
            // 
            this.tbLampX.Location = new System.Drawing.Point(102, 111);
            this.tbLampX.Name = "tbLampX";
            this.tbLampX.Size = new System.Drawing.Size(138, 23);
            this.tbLampX.TabIndex = 5;
            // 
            // tbLampY
            // 
            this.tbLampY.Location = new System.Drawing.Point(102, 154);
            this.tbLampY.Name = "tbLampY";
            this.tbLampY.Size = new System.Drawing.Size(138, 23);
            this.tbLampY.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "关联变量类型";
            // 
            // LampEditForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 240);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbLampY);
            this.Controls.Add(this.tbLampX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLampVar);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LampEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑指示灯";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tbLampX;
        public System.Windows.Forms.TextBox tbLampY;
        public System.Windows.Forms.ComboBox cbLampVar;
        public System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label4;
    }
}