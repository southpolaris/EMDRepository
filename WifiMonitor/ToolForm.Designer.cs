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
            this.btnCreateTab = new System.Windows.Forms.Button();
            this.numLabel = new System.Windows.Forms.NumericUpDown();
            this.numText = new System.Windows.Forms.NumericUpDown();
            this.btnRemoveTab = new System.Windows.Forms.Button();
            this.btnChangeTabName = new System.Windows.Forms.Button();
            this.numLamp = new System.Windows.Forms.NumericUpDown();
            this.lblAddLamp = new System.Windows.Forms.Label();
            this.btnAddLamp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLamp)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddLbl
            // 
            this.btnAddLbl.Location = new System.Drawing.Point(6, 35);
            this.btnAddLbl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddLbl.Name = "btnAddLbl";
            this.btnAddLbl.Size = new System.Drawing.Size(131, 29);
            this.btnAddLbl.TabIndex = 0;
            this.btnAddLbl.Text = "添加文本标签";
            this.btnAddLbl.UseVisualStyleBackColor = false;
            this.btnAddLbl.Click += new System.EventHandler(this.btnAddLbl_Click);
            // 
            // lblAddLbl
            // 
            this.lblAddLbl.AutoSize = true;
            this.lblAddLbl.Location = new System.Drawing.Point(4, 9);
            this.lblAddLbl.Name = "lblAddLbl";
            this.lblAddLbl.Size = new System.Drawing.Size(68, 17);
            this.lblAddLbl.TabIndex = 1;
            this.lblAddLbl.Text = "添加数量：";
            // 
            // lblAddText
            // 
            this.lblAddText.AutoSize = true;
            this.lblAddText.Location = new System.Drawing.Point(1, 94);
            this.lblAddText.Name = "lblAddText";
            this.lblAddText.Size = new System.Drawing.Size(68, 17);
            this.lblAddText.TabIndex = 4;
            this.lblAddText.Text = "添加数量：";
            // 
            // btnAddText
            // 
            this.btnAddText.Location = new System.Drawing.Point(4, 123);
            this.btnAddText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddText.Name = "btnAddText";
            this.btnAddText.Size = new System.Drawing.Size(133, 30);
            this.btnAddText.TabIndex = 3;
            this.btnAddText.Text = "添加数值量显示框";
            this.btnAddText.UseVisualStyleBackColor = false;
            this.btnAddText.Click += new System.EventHandler(this.btnAddText_Click);
            // 
            // btnCreateTab
            // 
            this.btnCreateTab.Location = new System.Drawing.Point(6, 327);
            this.btnCreateTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreateTab.Name = "btnCreateTab";
            this.btnCreateTab.Size = new System.Drawing.Size(131, 32);
            this.btnCreateTab.TabIndex = 12;
            this.btnCreateTab.Text = "新建标签页";
            this.btnCreateTab.UseVisualStyleBackColor = false;
            this.btnCreateTab.Click += new System.EventHandler(this.btnCreateTab_Click);
            // 
            // numLabel
            // 
            this.numLabel.Location = new System.Drawing.Point(87, 7);
            this.numLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numLabel.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numLabel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(48, 23);
            this.numLabel.TabIndex = 13;
            this.numLabel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numText
            // 
            this.numText.Location = new System.Drawing.Point(87, 92);
            this.numText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numText.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numText.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numText.Name = "numText";
            this.numText.Size = new System.Drawing.Size(50, 23);
            this.numText.TabIndex = 14;
            this.numText.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnRemoveTab
            // 
            this.btnRemoveTab.Location = new System.Drawing.Point(6, 367);
            this.btnRemoveTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveTab.Name = "btnRemoveTab";
            this.btnRemoveTab.Size = new System.Drawing.Size(131, 33);
            this.btnRemoveTab.TabIndex = 17;
            this.btnRemoveTab.Text = "删除当前标签页";
            this.btnRemoveTab.UseVisualStyleBackColor = false;
            this.btnRemoveTab.Click += new System.EventHandler(this.btnRemoveTab_Click);
            // 
            // btnChangeTabName
            // 
            this.btnChangeTabName.Location = new System.Drawing.Point(4, 286);
            this.btnChangeTabName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnChangeTabName.Name = "btnChangeTabName";
            this.btnChangeTabName.Size = new System.Drawing.Size(133, 33);
            this.btnChangeTabName.TabIndex = 18;
            this.btnChangeTabName.Text = "更改当前标签名称";
            this.btnChangeTabName.UseVisualStyleBackColor = false;
            this.btnChangeTabName.Click += new System.EventHandler(this.btnChangeTabName_Click);
            // 
            // numLamp
            // 
            this.numLamp.Location = new System.Drawing.Point(87, 188);
            this.numLamp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numLamp.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numLamp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLamp.Name = "numLamp";
            this.numLamp.Size = new System.Drawing.Size(50, 23);
            this.numLamp.TabIndex = 22;
            this.numLamp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblAddLamp
            // 
            this.lblAddLamp.AutoSize = true;
            this.lblAddLamp.Location = new System.Drawing.Point(4, 190);
            this.lblAddLamp.Name = "lblAddLamp";
            this.lblAddLamp.Size = new System.Drawing.Size(68, 17);
            this.lblAddLamp.TabIndex = 21;
            this.lblAddLamp.Text = "添加数量：";
            // 
            // btnAddLamp
            // 
            this.btnAddLamp.Location = new System.Drawing.Point(4, 219);
            this.btnAddLamp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddLamp.Name = "btnAddLamp";
            this.btnAddLamp.Size = new System.Drawing.Size(133, 31);
            this.btnAddLamp.TabIndex = 20;
            this.btnAddLamp.Text = "添加开关量显示";
            this.btnAddLamp.UseVisualStyleBackColor = false;
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(151, 443);
            this.ControlBox = false;
            this.Controls.Add(this.numLamp);
            this.Controls.Add(this.lblAddLamp);
            this.Controls.Add(this.btnAddLamp);
            this.Controls.Add(this.btnChangeTabName);
            this.Controls.Add(this.btnRemoveTab);
            this.Controls.Add(this.btnCreateTab);
            this.Controls.Add(this.numText);
            this.Controls.Add(this.numLabel);
            this.Controls.Add(this.lblAddText);
            this.Controls.Add(this.btnAddText);
            this.Controls.Add(this.lblAddLbl);
            this.Controls.Add(this.btnAddLbl);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "工具箱";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ToolForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.numLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLamp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddLbl;
        private System.Windows.Forms.Label lblAddLbl;
        private System.Windows.Forms.Label lblAddText;
        private System.Windows.Forms.Button btnAddText;
        private System.Windows.Forms.Button btnCreateTab;
        private System.Windows.Forms.NumericUpDown numLabel;
        private System.Windows.Forms.NumericUpDown numText;
        private System.Windows.Forms.Button btnRemoveTab;
        private System.Windows.Forms.Button btnChangeTabName;
        private System.Windows.Forms.NumericUpDown numLamp;
        private System.Windows.Forms.Label lblAddLamp;
        private System.Windows.Forms.Button btnAddLamp;
    }
}