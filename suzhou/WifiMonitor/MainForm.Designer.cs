namespace WifiMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSavEdit = new System.Windows.Forms.Button();
            this.contextMenuStripLbl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ItemDelLbl = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTxt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ItemDelTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuStripLbl.SuspendLayout();
            this.contextMenuStripTxt.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(3, 256);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "启用编辑";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSavEdit
            // 
            this.btnSavEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSavEdit.Enabled = false;
            this.btnSavEdit.Location = new System.Drawing.Point(84, 256);
            this.btnSavEdit.Name = "btnSavEdit";
            this.btnSavEdit.Size = new System.Drawing.Size(75, 23);
            this.btnSavEdit.TabIndex = 1;
            this.btnSavEdit.Text = "保存编辑";
            this.btnSavEdit.UseVisualStyleBackColor = false;
            this.btnSavEdit.Click += new System.EventHandler(this.btnSavEdit_Click);
            // 
            // contextMenuStripLbl
            // 
            this.contextMenuStripLbl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemDelLbl});
            this.contextMenuStripLbl.Name = "contextMenuStripLbl";
            this.contextMenuStripLbl.Size = new System.Drawing.Size(125, 26);
            // 
            // ItemDelLbl
            // 
            this.ItemDelLbl.Name = "ItemDelLbl";
            this.ItemDelLbl.Size = new System.Drawing.Size(124, 22);
            this.ItemDelLbl.Text = "删除标签";
            this.ItemDelLbl.Click += new System.EventHandler(this.ItemDelLbl_Click);
            // 
            // contextMenuStripTxt
            // 
            this.contextMenuStripTxt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemDelTxt});
            this.contextMenuStripTxt.Name = "contextMenuStripLbl";
            this.contextMenuStripTxt.Size = new System.Drawing.Size(137, 26);
            // 
            // ItemDelTxt
            // 
            this.ItemDelTxt.Name = "ItemDelTxt";
            this.ItemDelTxt.Size = new System.Drawing.Size(136, 22);
            this.ItemDelTxt.Text = "删除文本框";
            this.ItemDelTxt.Click += new System.EventHandler(this.ItemDelTxt_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(297, 256);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(374, 245);
            this.tabControl1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(379, 286);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSavEdit);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主界面";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStripLbl.ResumeLayout(false);
            this.contextMenuStripTxt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnEdit;
        public System.Windows.Forms.Button btnSavEdit;
        public System.Windows.Forms.ContextMenuStrip contextMenuStripLbl;
        public System.Windows.Forms.ToolStripMenuItem ItemDelLbl;
        public System.Windows.Forms.ContextMenuStrip contextMenuStripTxt;
        public System.Windows.Forms.ToolStripMenuItem ItemDelTxt;
        private System.Windows.Forms.Button btnStart;
        public System.Windows.Forms.TabControl tabControl1;

    }
}

