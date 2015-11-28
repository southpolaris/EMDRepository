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
            if (GlobalVar.runningFlag)
                 communicate.Dispose();
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSavEdit = new System.Windows.Forms.Button();
            this.contextMenuStripLbl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ItemDelLbl = new System.Windows.Forms.ToolStripMenuItem();
            this.hideLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTxt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ItemDelTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.hideTextboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.contextMenuStripLamp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DelLampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideLampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelInfo = new System.Windows.Forms.Label();
            this.btnConnectionList = new System.Windows.Forms.Button();
            this.btnEditStop = new System.Windows.Forms.Button();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripLbl.SuspendLayout();
            this.contextMenuStripTxt.SuspendLayout();
            this.contextMenuStripLamp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEdit.Location = new System.Drawing.Point(3, 385);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(84, 26);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "开始编辑...";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSavEdit
            // 
            this.btnSavEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSavEdit.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSavEdit.Location = new System.Drawing.Point(96, 385);
            this.btnSavEdit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSavEdit.Name = "btnSavEdit";
            this.btnSavEdit.Size = new System.Drawing.Size(65, 26);
            this.btnSavEdit.TabIndex = 1;
            this.btnSavEdit.Text = "保存编辑";
            this.btnSavEdit.UseVisualStyleBackColor = true;
            this.btnSavEdit.Visible = false;
            this.btnSavEdit.Click += new System.EventHandler(this.btnSaveEdit_Click);
            // 
            // contextMenuStripLbl
            // 
            this.contextMenuStripLbl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemDelLbl,
            this.hideLabelToolStripMenuItem});
            this.contextMenuStripLbl.Name = "contextMenuStripLbl";
            this.contextMenuStripLbl.Size = new System.Drawing.Size(125, 48);
            // 
            // ItemDelLbl
            // 
            this.ItemDelLbl.Name = "ItemDelLbl";
            this.ItemDelLbl.Size = new System.Drawing.Size(124, 22);
            this.ItemDelLbl.Text = "删除标签";
            this.ItemDelLbl.Click += new System.EventHandler(this.ItemDelLbl_Click);
            // 
            // hideLabelToolStripMenuItem
            // 
            this.hideLabelToolStripMenuItem.Name = "hideLabelToolStripMenuItem";
            this.hideLabelToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.hideLabelToolStripMenuItem.Text = "隐藏标签";
            // 
            // contextMenuStripTxt
            // 
            this.contextMenuStripTxt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemDelTxt,
            this.hideTextboxToolStripMenuItem});
            this.contextMenuStripTxt.Name = "contextMenuStripLbl";
            this.contextMenuStripTxt.Size = new System.Drawing.Size(137, 48);
            // 
            // ItemDelTxt
            // 
            this.ItemDelTxt.Name = "ItemDelTxt";
            this.ItemDelTxt.Size = new System.Drawing.Size(136, 22);
            this.ItemDelTxt.Text = "删除文本框";
            this.ItemDelTxt.Click += new System.EventHandler(this.ItemDelTxt_Click);
            // 
            // hideTextboxToolStripMenuItem
            // 
            this.hideTextboxToolStripMenuItem.Name = "hideTextboxToolStripMenuItem";
            this.hideTextboxToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.hideTextboxToolStripMenuItem.Text = "隐藏文本框";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.LightGreen;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(542, 385);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(99, 26);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始(&S)";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Location = new System.Drawing.Point(3, 2);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(638, 378);
            this.tabControl.TabIndex = 3;
            // 
            // contextMenuStripLamp
            // 
            this.contextMenuStripLamp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DelLampToolStripMenuItem,
            this.hideLampToolStripMenuItem});
            this.contextMenuStripLamp.Name = "contextMenuStripLamp";
            this.contextMenuStripLamp.Size = new System.Drawing.Size(137, 48);
            // 
            // DelLampToolStripMenuItem
            // 
            this.DelLampToolStripMenuItem.Name = "DelLampToolStripMenuItem";
            this.DelLampToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.DelLampToolStripMenuItem.Text = "删除指示灯";
            this.DelLampToolStripMenuItem.Click += new System.EventHandler(this.DelLampToolStripMenuItem_Click);
            // 
            // hideLampToolStripMenuItem
            // 
            this.hideLampToolStripMenuItem.Name = "hideLampToolStripMenuItem";
            this.hideLampToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.hideLampToolStripMenuItem.Text = "隐藏指示灯";
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInfo.Location = new System.Drawing.Point(1, 390);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(43, 17);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = "label1";
            // 
            // btnConnectionList
            // 
            this.btnConnectionList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnectionList.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConnectionList.Location = new System.Drawing.Point(420, 385);
            this.btnConnectionList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConnectionList.Name = "btnConnectionList";
            this.btnConnectionList.Size = new System.Drawing.Size(115, 26);
            this.btnConnectionList.TabIndex = 6;
            this.btnConnectionList.Text = "查看连接信息...";
            this.btnConnectionList.UseVisualStyleBackColor = true;
            this.btnConnectionList.Visible = false;
            this.btnConnectionList.Click += new System.EventHandler(this.btnConnectionList_Click);
            // 
            // btnEditStop
            // 
            this.btnEditStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditStop.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEditStop.Location = new System.Drawing.Point(167, 385);
            this.btnEditStop.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnEditStop.Name = "btnEditStop";
            this.btnEditStop.Size = new System.Drawing.Size(78, 26);
            this.btnEditStop.TabIndex = 7;
            this.btnEditStop.Text = "退出编辑";
            this.btnEditStop.UseVisualStyleBackColor = true;
            this.btnEditStop.Visible = false;
            this.btnEditStop.Click += new System.EventHandler(this.btnEditStop_Click);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 80;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(645, 415);
            this.Controls.Add(this.btnEditStop);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnConnectionList);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.btnSavEdit);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.contextMenuStripLbl.ResumeLayout(false);
            this.contextMenuStripTxt.ResumeLayout(false);
            this.contextMenuStripLamp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnEdit;
        public System.Windows.Forms.Button btnSavEdit;
        public System.Windows.Forms.ContextMenuStrip contextMenuStripLbl;
        public System.Windows.Forms.ToolStripMenuItem ItemDelLbl;
        public System.Windows.Forms.ContextMenuStrip contextMenuStripTxt;
        public System.Windows.Forms.ToolStripMenuItem ItemDelTxt;
        public System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLamp;
        private System.Windows.Forms.ToolStripMenuItem DelLampToolStripMenuItem;
        public System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button btnConnectionList;
        public System.Windows.Forms.Button btnStart;
        protected System.Windows.Forms.Button btnEditStop;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ToolStripMenuItem hideLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideTextboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideLampToolStripMenuItem;

    }
}

