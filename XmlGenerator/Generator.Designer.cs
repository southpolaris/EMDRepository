namespace XMLGenerator
{
    partial class Generator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Generator));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.pageVariable = new System.Windows.Forms.TabPage();
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.pageUI = new System.Windows.Forms.TabPage();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.contextMenuStripLbl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dellab = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTxt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deltextbox = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripLamp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dellamp = new System.Windows.Forms.ToolStripMenuItem();
            this.varName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varInterface = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varInDataBase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.pageVariable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStripLbl.SuspendLayout();
            this.contextMenuStripTxt.SuspendLayout();
            this.contextMenuStripLamp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.pageVariable);
            this.tabControl.Controls.Add(this.pageUI);
            this.tabControl.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl.Location = new System.Drawing.Point(3, 2);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(830, 428);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // pageVariable
            // 
            this.pageVariable.Controls.Add(this.cbProtocol);
            this.pageVariable.Controls.Add(this.textBoxModel);
            this.pageVariable.Controls.Add(this.textBoxName);
            this.pageVariable.Controls.Add(this.label3);
            this.pageVariable.Controls.Add(this.label2);
            this.pageVariable.Controls.Add(this.label1);
            this.pageVariable.Controls.Add(this.dataGridView);
            this.pageVariable.Location = new System.Drawing.Point(4, 29);
            this.pageVariable.Margin = new System.Windows.Forms.Padding(0);
            this.pageVariable.Name = "pageVariable";
            this.pageVariable.Size = new System.Drawing.Size(822, 395);
            this.pageVariable.TabIndex = 0;
            this.pageVariable.Text = "1. 编辑监控变量";
            // 
            // cbProtocol
            // 
            this.cbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtocol.FormattingEnabled = true;
            this.cbProtocol.Location = new System.Drawing.Point(157, 60);
            this.cbProtocol.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(147, 28);
            this.cbProtocol.TabIndex = 7;
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(157, 22);
            this.textBoxModel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(147, 26);
            this.textBoxModel.TabIndex = 6;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(545, 22);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(147, 29);
            this.textBoxName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据通信协议";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "设备型号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(404, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "设备名称";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.varName,
            this.varInterface,
            this.varAddress,
            this.varInDataBase});
            this.dataGridView.Location = new System.Drawing.Point(3, 100);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(814, 288);
            this.dataGridView.TabIndex = 0;
            // 
            // pageUI
            // 
            this.pageUI.Location = new System.Drawing.Point(4, 29);
            this.pageUI.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pageUI.Name = "pageUI";
            this.pageUI.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pageUI.Size = new System.Drawing.Size(822, 395);
            this.pageUI.TabIndex = 1;
            this.pageUI.Text = "2. 绘制监控界面";
            this.pageUI.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAdd.Location = new System.Drawing.Point(6, 432);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(112, 32);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "添加数据";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonEdit.Location = new System.Drawing.Point(125, 432);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(112, 32);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "修改数据";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonDel.Location = new System.Drawing.Point(243, 432);
            this.buttonDel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(112, 32);
            this.buttonDel.TabIndex = 3;
            this.buttonDel.Text = "删除数据";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpen.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOpen.Location = new System.Drawing.Point(511, 432);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(158, 32);
            this.buttonOpen.TabIndex = 5;
            this.buttonOpen.Text = "打开配置文件";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSave.Location = new System.Drawing.Point(675, 432);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(153, 32);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "保存配置文件";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // contextMenuStripLbl
            // 
            this.contextMenuStripLbl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dellab});
            this.contextMenuStripLbl.Name = "contextMenuStripLbl";
            this.contextMenuStripLbl.Size = new System.Drawing.Size(125, 26);
            // 
            // dellab
            // 
            this.dellab.Name = "dellab";
            this.dellab.Size = new System.Drawing.Size(124, 22);
            this.dellab.Text = "删除标签";
            this.dellab.Click += new System.EventHandler(this.delLabel_Click);
            // 
            // contextMenuStripTxt
            // 
            this.contextMenuStripTxt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deltextbox});
            this.contextMenuStripTxt.Name = "contextMenuStrip1";
            this.contextMenuStripTxt.Size = new System.Drawing.Size(137, 26);
            // 
            // deltextbox
            // 
            this.deltextbox.Name = "deltextbox";
            this.deltextbox.Size = new System.Drawing.Size(136, 22);
            this.deltextbox.Text = "删除文本框";
            this.deltextbox.Click += new System.EventHandler(this.deltextbox_Click);
            // 
            // contextMenuStripLamp
            // 
            this.contextMenuStripLamp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dellamp});
            this.contextMenuStripLamp.Name = "contextMenuStrip1";
            this.contextMenuStripLamp.Size = new System.Drawing.Size(137, 26);
            // 
            // dellamp
            // 
            this.dellamp.Name = "dellamp";
            this.dellamp.Size = new System.Drawing.Size(136, 22);
            this.dellamp.Text = "删除指示灯";
            this.dellamp.Click += new System.EventHandler(this.delLamp_Click);
            // 
            // varName
            // 
            this.varName.HeaderText = "变量名";
            this.varName.Name = "varName";
            this.varName.ReadOnly = true;
            // 
            // varInterface
            // 
            this.varInterface.HeaderText = "数据通道";
            this.varInterface.Name = "varInterface";
            this.varInterface.ReadOnly = true;
            // 
            // varAddress
            // 
            this.varAddress.HeaderText = "变量地址";
            this.varAddress.Name = "varAddress";
            this.varAddress.ReadOnly = true;
            // 
            // varInDataBase
            // 
            this.varInDataBase.HeaderText = "是否存入数据库";
            this.varInDataBase.Name = "varInDataBase";
            this.varInDataBase.ReadOnly = true;
            // 
            // Generator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 466);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Generator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置文件编辑器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.pageVariable.ResumeLayout(false);
            this.pageVariable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStripLbl.ResumeLayout(false);
            this.contextMenuStripTxt.ResumeLayout(false);
            this.contextMenuStripLamp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage pageUI;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataGridView;
        public System.Windows.Forms.Button buttonAdd;
        public System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLbl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTxt;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLamp;
        private System.Windows.Forms.ToolStripMenuItem dellab;
        private System.Windows.Forms.ToolStripMenuItem deltextbox;
        private System.Windows.Forms.ToolStripMenuItem dellamp;
        public System.Windows.Forms.TabPage pageVariable;
        public System.Windows.Forms.ComboBox cbProtocol;
        public System.Windows.Forms.TextBox textBoxName;
        public System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn varName;
        private System.Windows.Forms.DataGridViewTextBoxColumn varInterface;
        private System.Windows.Forms.DataGridViewTextBoxColumn varAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn varInDataBase;
    }
}

