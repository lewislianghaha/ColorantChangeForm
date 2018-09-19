namespace ColorantChangeForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tmUpdateKey = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmUpdateSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tmChina = new System.Windows.Forms.ToolStripMenuItem();
            this.tmOutside = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tmCSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tmClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tmUpLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tmUpLoadColorant = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmUploadColorCode = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtColorant = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gvdtl = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAkzoColorant = new System.Windows.Forms.TextBox();
            this.tmSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem1,
            this.tmClose,
            this.tmUpLoad});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(972, 25);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "Demo";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmUpdateKey,
            this.toolStripSeparator1,
            this.tmUpdateSearch});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(80, 21);
            this.toolStripMenuItem1.Text = "更新及同步";
            // 
            // tmUpdateKey
            // 
            this.tmUpdateKey.Name = "tmUpdateKey";
            this.tmUpdateKey.Size = new System.Drawing.Size(152, 22);
            this.tmUpdateKey.Text = "更新至录入端";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // tmUpdateSearch
            // 
            this.tmUpdateSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmChina,
            this.tmOutside});
            this.tmUpdateSearch.Name = "tmUpdateSearch";
            this.tmUpdateSearch.Size = new System.Drawing.Size(152, 22);
            this.tmUpdateSearch.Text = "同步至查询端";
            // 
            // tmChina
            // 
            this.tmChina.Name = "tmChina";
            this.tmChina.Size = new System.Drawing.Size(152, 22);
            this.tmChina.Text = "国内";
            // 
            // tmOutside
            // 
            this.tmOutside.Name = "tmOutside";
            this.tmOutside.Size = new System.Drawing.Size(152, 22);
            this.tmOutside.Text = "海外";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmSearch,
            this.toolStripSeparator3,
            this.tmCSearch});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem4.Text = "查询";
            // 
            // tmCSearch
            // 
            this.tmCSearch.Name = "tmCSearch";
            this.tmCSearch.Size = new System.Drawing.Size(152, 22);
            this.tmCSearch.Text = "色母对照表";
            // 
            // tmClose
            // 
            this.tmClose.Name = "tmClose";
            this.tmClose.Size = new System.Drawing.Size(44, 21);
            this.tmClose.Text = "关闭";
            // 
            // tmUpLoad
            // 
            this.tmUpLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmUpLoadColorant,
            this.toolStripSeparator2,
            this.tmUploadColorCode});
            this.tmUpLoad.Name = "tmUpLoad";
            this.tmUpLoad.Size = new System.Drawing.Size(44, 21);
            this.tmUpLoad.Text = "导入";
            // 
            // tmUpLoadColorant
            // 
            this.tmUpLoadColorant.Name = "tmUpLoadColorant";
            this.tmUpLoadColorant.Size = new System.Drawing.Size(152, 22);
            this.tmUpLoadColorant.Text = "色母对照表";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // tmUploadColorCode
            // 
            this.tmUploadColorCode.Name = "tmUploadColorCode";
            this.tmUploadColorCode.Size = new System.Drawing.Size(152, 22);
            this.tmUploadColorCode.Text = "色号对照表";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtAkzoColorant);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.txtValue);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtColorant);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(972, 39);
            this.panel1.TabIndex = 1;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(567, 10);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "计算色母量";
            this.btnGenerate.UseVisualStyleBackColor = true;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(466, 11);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 21);
            this.txtValue.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "浓度系数";
            // 
            // txtColorant
            // 
            this.txtColorant.Enabled = false;
            this.txtColorant.Location = new System.Drawing.Point(91, 11);
            this.txtColorant.Name = "txtColorant";
            this.txtColorant.Size = new System.Drawing.Size(100, 21);
            this.txtColorant.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "三华色母编号";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gvdtl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 64);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(972, 495);
            this.panel3.TabIndex = 3;
            // 
            // gvdtl
            // 
            this.gvdtl.AllowUserToAddRows = false;
            this.gvdtl.AllowUserToDeleteRows = false;
            this.gvdtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvdtl.Location = new System.Drawing.Point(0, 0);
            this.gvdtl.Name = "gvdtl";
            this.gvdtl.ReadOnly = true;
            this.gvdtl.RowTemplate.Height = 23;
            this.gvdtl.Size = new System.Drawing.Size(972, 495);
            this.gvdtl.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "对应Akzo色母编号";
            // 
            // txtAkzoColorant
            // 
            this.txtAkzoColorant.Enabled = false;
            this.txtAkzoColorant.Location = new System.Drawing.Point(302, 11);
            this.txtAkzoColorant.Name = "txtAkzoColorant";
            this.txtAkzoColorant.Size = new System.Drawing.Size(100, 21);
            this.txtAkzoColorant.TabIndex = 7;
            // 
            // tmSearch
            // 
            this.tmSearch.Name = "tmSearch";
            this.tmSearch.Size = new System.Drawing.Size(152, 22);
            this.tmSearch.Text = "三华色母明细";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 559);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Akzo色母量转换";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tmUpdateKey;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tmUpdateSearch;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tmCSearch;
        private System.Windows.Forms.ToolStripMenuItem tmClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtColorant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView gvdtl;
        private System.Windows.Forms.ToolStripMenuItem tmChina;
        private System.Windows.Forms.ToolStripMenuItem tmOutside;
        private System.Windows.Forms.ToolStripMenuItem tmUpLoad;
        private System.Windows.Forms.ToolStripMenuItem tmUpLoadColorant;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tmUploadColorCode;
        private System.Windows.Forms.TextBox txtAkzoColorant;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem tmSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

