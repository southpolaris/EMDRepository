namespace WifiMonitor
{
    partial class ToolForm
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
            this.btnAddLbl = new System.Windows.Forms.Button();
            this.lblAddLbl = new System.Windows.Forms.Label();
            this.lblAddText = new System.Windows.Forms.Label();
            this.btnAddText = new System.Windows.Forms.Button();
            this.lblAddLamp = new System.Windows.Forms.Label();
            this.btnAddLamp = new System.Windows.Forms.Button();
            this.lblDataNum = new System.Windows.Forms.Label();
            this.btnSavNum = new System.Windows.Forms.Button();
            this.btnCreateTab = new System.Windows.Forms.Button();
            this.numLabel = new System.Windows.Forms.NumericUpDown();
            this.numText = new System.Windows.Forms.NumericUpDown();
            this.btnRemoveTab = new System.Windows.Forms.Button();
            this.btnChangeTabName = new System.Windows.Forms.Button();
            this.numLamp = new System.Windows.Forms.NumericUpDown();
            this.numVar = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLamp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddLbl
            // 
            this.btnAddLbl.Location = new System.Drawing.Point(9, 35);
            this.btnAddLbl.Name = "btnAddLbl";
            this.btnAddLbl.Size = new System.Drawing.Size(127, 30);
            this.btnAddLbl.TabIndex = 0;
            this.btnAddLbl.Text = "添加标签";
            this.btnAddLbl.UseVisualStyleBackColor = false;
            this.btnAddLbl.Click += new System.EventHandler(this.btnAddLbl_Click);
            // 
            // lblAddLbl
            // 
            this.lblAddLbl.AutoSize = true;
            this.lblAddLbl.Location = new System.Drawing.Point(7, 12);
            this.lblAddLbl.Name = "lblAddLbl";
            this.lblAddLbl.Size = new System.Drawing.Size(65, 12);
            this.lblAddLbl.TabIndex = 1;
            this.lblAddLbl.Text = "标签数量：";
            // 
            // lblAddText
            // 
            this.lblAddText.AutoSize = true;
            this.lblAddText.Location = new System.Drawing.Point(7, 76);
            this.lblAddText.Name = "lblAddText";
            this.lblAddText.Size = new System.Drawing.Size(77, 12);
            this.lblAddText.TabIndex = 4;
            this.lblAddText.Text = "文本框数量：";
            // 
            // btnAddText
            // 
            this.btnAddText.Location = new System.Drawing.Point(9, 99);
            this.btnAddText.Name = "btnAddText";
            this.btnAddText.Size = new System.Drawing.Size(127, 30);
            this.btnAddText.TabIndex = 3;
            this.btnAddText.Text = "添加文本框";
            this.btnAddText.UseVisualStyleBackColor = false;
            this.btnAddText.Click += new System.EventHandler(this.btnAddText_Click);
            // 
            // lblAddLamp
            // 
            this.lblAddLamp.AutoSize = true;
            this.lblAddLamp.Location = new System.Drawing.Point(7, 141);
            this.lblAddLamp.Name = "lblAddLamp";
            this.lblAddLamp.Size = new System.Drawing.Size(77, 12);
            this.lblAddLamp.TabIndex = 7;
            this.lblAddLamp.Text = "指示灯数量：";
            // 
            // btnAddLamp
            // 
            this.btnAddLamp.Location = new System.Drawing.Point(9, 164);
            this.btnAddLamp.Name = "btnAddLamp";
            this.btnAddLamp.Size = new System.Drawing.Size(127, 30);
            this.btnAddLamp.TabIndex = 6;
            this.btnAddLamp.Text = "添加指示灯";
            this.btnAddLamp.UseVisualStyleBackColor = false;
            // 
            // lblDataNum
            // 
            this.lblDataNum.AutoSize = true;
            this.lblDataNum.Location = new System.Drawing.Point(7, 207);
            this.lblDataNum.Name = "lblDataNum";
            this.lblDataNum.Size = new System.Drawing.Size(77, 12);
            this.lblDataNum.TabIndex = 10;
            this.lblDataNum.Text = "监视变量数：";
            // 
            // btnSavNum
            // 
            this.btnSavNum.Location = new System.Drawing.Point(9, 230);
            this.btnSavNum.Name = "btnSavNum";
            this.btnSavNum.Size = new System.Drawing.Size(127, 30);
            this.btnSavNum.TabIndex = 9;
            this.btnSavNum.Text = "保存数量值";
            this.btnSavNum.UseVisualStyleBackColor = false;
            this.btnSavNum.Click += new System.EventHandler(this.btnSavNum_Click);
            // 
            // btnCreateTab
            // 
            this.btnCreateTab.Location = new System.Drawing.Point(9, 405);
            this.btnCreateTab.Name = "btnCreateTab";
            this.btnCreateTab.Size = new System.Drawing.Size(127, 30);
            this.btnCreateTab.TabIndex = 12;
            this.btnCreateTab.Text = "新建标签页";
            this.btnCreateTab.UseVisualStyleBackColor = false;
            this.btnCreateTab.Click += new System.EventHandler(this.btnCreateTab_Click);
            // 
            // numLabel
            // 
            this.numLabel.Location = new System.Drawing.Point(76, 8);
            this.numLabel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(58, 21);
            this.numLabel.TabIndex = 13;
            this.numLabel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numText
            // 
            this.numText.Location = new System.Drawing.Point(78, 72);
            this.numText.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numText.Name = "numText";
            this.numText.Size = new System.Drawing.Size(58, 21);
            this.numText.TabIndex = 14;
            this.numText.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnRemoveTab
            // 
            this.btnRemoveTab.Location = new System.Drawing.Point(9, 441);
            this.btnRemoveTab.Name = "btnRemoveTab";
            this.btnRemoveTab.Size = new System.Drawing.Size(127, 30);
            this.btnRemoveTab.TabIndex = 17;
            this.btnRemoveTab.Text = "删除当前标签页";
            this.btnRemoveTab.UseVisualStyleBackColor = false;
            // 
            // btnChangeTabName
            // 
            this.btnChangeTabName.Location = new System.Drawing.Point(9, 368);
            this.btnChangeTabName.Name = "btnChangeTabName";
            this.btnChangeTabName.Size = new System.Drawing.Size(127, 30);
            this.btnChangeTabName.TabIndex = 18;
            this.btnChangeTabName.Text = "更改当前标签名称";
            this.btnChangeTabName.UseVisualStyleBackColor = false;
            this.btnChangeTabName.Click += new System.EventHandler(this.btnChangeTabName_Click);
            // 
            // numLamp
            // 
            this.numLamp.Location = new System.Drawing.Point(78, 137);
            this.numLamp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLamp.Name = "numLamp";
            this.numLamp.Size = new System.Drawing.Size(58, 21);
            this.numLamp.TabIndex = 19;
            this.numLamp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numVar
            // 
            this.numVar.Location = new System.Drawing.Point(76, 203);
            this.numVar.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numVar.Name = "numVar";
            this.numVar.Size = new System.Drawing.Size(58, 21);
            this.numVar.TabIndex = 20;
            this.numVar.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 474);
            this.Controls.Add(this.numVar);
            this.Controls.Add(this.numLamp);
            this.Controls.Add(this.btnChangeTabName);
            this.Controls.Add(this.btnRemoveTab);
            this.Controls.Add(this.btnCreateTab);
            this.Controls.Add(this.numText);
            this.Controls.Add(this.numLabel);
            this.Controls.Add(this.lblDataNum);
            this.Controls.Add(this.btnSavNum);
            this.Controls.Add(this.lblAddLamp);
            this.Controls.Add(this.btnAddLamp);
            this.Controls.Add(this.lblAddText);
            this.Controls.Add(this.btnAddText);
            this.Controls.Add(this.lblAddLbl);
            this.Controls.Add(this.btnAddLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ToolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "工具箱";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ToolForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.numLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLamp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddLbl;
        private System.Windows.Forms.Label lblAddLbl;
        private System.Windows.Forms.Label lblAddText;
        private System.Windows.Forms.Button btnAddText;
        private System.Windows.Forms.Label lblAddLamp;
        private System.Windows.Forms.Button btnAddLamp;
        private System.Windows.Forms.Label lblDataNum;
        private System.Windows.Forms.Button btnSavNum;
        private System.Windows.Forms.Button btnCreateTab;
        private System.Windows.Forms.NumericUpDown numLabel;
        private System.Windows.Forms.NumericUpDown numText;
        private System.Windows.Forms.Button btnRemoveTab;
        private System.Windows.Forms.Button btnChangeTabName;
        private System.Windows.Forms.NumericUpDown numLamp;
        private System.Windows.Forms.NumericUpDown numVar;
    }
}