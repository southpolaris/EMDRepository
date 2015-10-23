namespace WifiMonitor
{
    partial class ConnectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTime,
            this.ColumnIP,
            this.ColumnPort,
            this.ColumnStatus});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(628, 299);
            this.dataGridView1.TabIndex = 0;
            // 
            // ColumnTime
            // 
            this.ColumnTime.HeaderText = "建立时间";
            this.ColumnTime.Name = "ColumnTime";
            this.ColumnTime.ReadOnly = true;
            this.ColumnTime.Width = 180;
            // 
            // ColumnIP
            // 
            this.ColumnIP.HeaderText = "IP地址";
            this.ColumnIP.Name = "ColumnIP";
            this.ColumnIP.ReadOnly = true;
            this.ColumnIP.Width = 160;
            // 
            // ColumnPort
            // 
            this.ColumnPort.HeaderText = "端口号";
            this.ColumnPort.Name = "ColumnPort";
            this.ColumnPort.ReadOnly = true;
            this.ColumnPort.Width = 80;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.HeaderText = "工作状态";
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.Width = 200;
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 299);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ConnectionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "当前连接的模块";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
    }
}