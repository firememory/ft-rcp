namespace Hundsun.Framework.AddIns
{
    partial class DemoCustomerView
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoCustomerView));
            this.hsToolStrip1 = new Hundsun.Framework.HSControls.hsToolStrip();
            this.toolBtnQuery = new Hundsun.Framework.UIFrame.hsFunctionToolStripButton();
            this.toolBtnAdd = new Hundsun.Framework.UIFrame.hsFunctionToolStripButton();
            this.toolBtnEdit = new Hundsun.Framework.UIFrame.hsFunctionToolStripButton();
            this.toolBtnView = new Hundsun.Framework.UIFrame.hsFunctionToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnDelete = new Hundsun.Framework.UIFrame.hsFunctionToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnLayout = new Hundsun.Framework.UIFrame.hsColumnLayoutToolStripButton();
            this.hsDataGridView1 = new Hundsun.Framework.HSControls.hsDataGridView();
            this.btnExport = new Hundsun.Framework.UIFrame.hsFunctionToolStripButton();
            this.btnPrint = new Hundsun.Framework.UIFrame.hsFunctionToolStripButton();
            this.hsPager1 = new Hundsun.Framework.HSControls.hsPager();
            this.hsPanel2 = new Hundsun.Framework.HSControls.hsPanel();
            this.hsLabelComboBox1 = new Hundsun.Framework.HSControls.hsLabelComboBox();
            this.hsLabelTextBox3 = new Hundsun.Framework.HSControls.hsLabelTextBox();
            this.hsLabelTextBox2 = new Hundsun.Framework.HSControls.hsLabelTextBox();
            this.hsLabelTextBox1 = new Hundsun.Framework.HSControls.hsLabelTextBox();
            this.hsPanel1 = new Hundsun.Framework.HSControls.hsPanel();
            this.hsToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hsDataGridView1)).BeginInit();
            this.hsPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hsLabelComboBox1)).BeginInit();
            this.hsPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hsToolStrip1
            // 
            this.hsToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnQuery,
            this.toolBtnAdd,
            this.toolBtnEdit,
            this.toolBtnView,
            this.toolStripSeparator1,
            this.toolBtnDelete,
            this.toolStripSeparator2,
            this.toolBtnLayout,
            this.btnExport,
            this.btnPrint});
            this.hsToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.hsToolStrip1.Name = "hsToolStrip1";
            this.hsToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.hsToolStrip1.Size = new System.Drawing.Size(827, 25);
            this.hsToolStrip1.TabIndex = 0;
            this.hsToolStrip1.Text = "hsToolStrip1";
            // 
            // toolBtnQuery
            // 
            this.toolBtnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.toolBtnQuery.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.Query;
            this.toolBtnQuery.CustomTheme = false;
            this.toolBtnQuery.FunctionID = "5";
            this.toolBtnQuery.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnQuery.Image")));
            this.toolBtnQuery.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnQuery.Name = "toolBtnQuery";
            this.toolBtnQuery.Size = new System.Drawing.Size(52, 22);
            this.toolBtnQuery.Text = "查询";
            this.toolBtnQuery.Click += new System.EventHandler(this.toolBtnQuery_Click);
            // 
            // toolBtnAdd
            // 
            this.toolBtnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.toolBtnAdd.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.New;
            this.toolBtnAdd.CustomTheme = false;
            this.toolBtnAdd.FunctionID = "1";
            this.toolBtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnAdd.Image")));
            this.toolBtnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnAdd.Name = "toolBtnAdd";
            this.toolBtnAdd.Size = new System.Drawing.Size(52, 22);
            this.toolBtnAdd.Text = "新增";
            this.toolBtnAdd.Click += new System.EventHandler(this.toolBtnAdd_Click);
            // 
            // toolBtnEdit
            // 
            this.toolBtnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.toolBtnEdit.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.Modify;
            this.toolBtnEdit.CustomTheme = false;
            this.toolBtnEdit.FunctionID = "2";
            this.toolBtnEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnEdit.Image")));
            this.toolBtnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnEdit.Name = "toolBtnEdit";
            this.toolBtnEdit.Size = new System.Drawing.Size(56, 22);
            this.toolBtnEdit.Text = " 修改";
            this.toolBtnEdit.Click += new System.EventHandler(this.toolBtnEdit_Click);
            // 
            // toolBtnView
            // 
            this.toolBtnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.toolBtnView.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.View;
            this.toolBtnView.CustomTheme = false;
            this.toolBtnView.FunctionID = "3";
            this.toolBtnView.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnView.Image")));
            this.toolBtnView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnView.Name = "toolBtnView";
            this.toolBtnView.Size = new System.Drawing.Size(52, 22);
            this.toolBtnView.Text = "查看";
            this.toolBtnView.Click += new System.EventHandler(this.toolBtnView_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 25);
            // 
            // toolBtnDelete
            // 
            this.toolBtnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.toolBtnDelete.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.Delete;
            this.toolBtnDelete.CustomTheme = false;
            this.toolBtnDelete.FunctionID = "4";
            this.toolBtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnDelete.Image")));
            this.toolBtnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDelete.Name = "toolBtnDelete";
            this.toolBtnDelete.Size = new System.Drawing.Size(52, 22);
            this.toolBtnDelete.Text = "删除";
            this.toolBtnDelete.Click += new System.EventHandler(this.toolBtnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(15, 25);
            // 
            // toolBtnLayout
            // 
            this.toolBtnLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.toolBtnLayout.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.Tool;
            this.toolBtnLayout.CustomTheme = false;
            this.toolBtnLayout.FunctionID = "5";
            this.toolBtnLayout.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnLayout.Image")));
            this.toolBtnLayout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnLayout.Name = "toolBtnLayout";
            this.toolBtnLayout.Size = new System.Drawing.Size(88, 22);
            this.toolBtnLayout.TargetGrid = this.hsDataGridView1;
            this.toolBtnLayout.Text = "列布局设置";
            this.toolBtnLayout.ToolTipText = "列布局设置";
            // 
            // hsDataGridView1
            // 
            this.hsDataGridView1.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.hsDataGridView1.AlternatingForeColor = System.Drawing.Color.Empty;
            this.hsDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.hsDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hsDataGridView1.ColumnHeadersHeight = 4;
            this.hsDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.hsDataGridView1.CustomTheme = false;
            this.hsDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hsDataGridView1.FooterVisible = true;
            this.hsDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.hsDataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.hsDataGridView1.Name = "hsDataGridView1";
            this.hsDataGridView1.ReadOnly = true;
            this.hsDataGridView1.RowHeadersVisible = true;
            this.hsDataGridView1.RowHeadersWidth = 50;
            this.hsDataGridView1.RowTemplate.Height = 23;
            this.hsDataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.hsDataGridView1.Size = new System.Drawing.Size(827, 399);
            this.hsDataGridView1.TabIndex = 1;
            this.hsDataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.hsDataGridView1_CellDoubleClick);
            this.hsDataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.hsDataGridView1_CellFormatting);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.btnExport.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.ExportExcel;
            this.btnExport.CustomTheme = false;
            this.btnExport.FunctionID = null;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(52, 22);
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.btnPrint.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.Print;
            this.btnPrint.CustomTheme = false;
            this.btnPrint.FunctionID = null;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(52, 22);
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // hsPager1
            // 
            this.hsPager1.CustomTheme = false;
            this.hsPager1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hsPager1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.hsPager1.Location = new System.Drawing.Point(0, 464);
            this.hsPager1.Margin = new System.Windows.Forms.Padding(0);
            this.hsPager1.Name = "hsPager1";
            this.hsPager1.RecordCount = 0;
            this.hsPager1.Size = new System.Drawing.Size(827, 23);
            this.hsPager1.TabIndex = 1;
            this.hsPager1.TargetDataSource = null;
            this.hsPager1.TargetGrid = this.hsDataGridView1;
            // 
            // hsPanel2
            // 
            this.hsPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.hsPanel2.Controls.Add(this.hsLabelComboBox1);
            this.hsPanel2.Controls.Add(this.hsLabelTextBox3);
            this.hsPanel2.Controls.Add(this.hsLabelTextBox2);
            this.hsPanel2.Controls.Add(this.hsLabelTextBox1);
            this.hsPanel2.CustomTheme = false;
            this.hsPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.hsPanel2.LayoutIntervalHorizontal = 8;
            this.hsPanel2.LayoutIntervalVertical = 8;
            this.hsPanel2.Location = new System.Drawing.Point(0, 25);
            this.hsPanel2.Name = "hsPanel2";
            this.hsPanel2.Size = new System.Drawing.Size(827, 40);
            this.hsPanel2.TabIndex = 0;
            // 
            // hsLabelComboBox1
            // 
            this.hsLabelComboBox1.ComboBoxBackColor = System.Drawing.Color.White;
            this.hsLabelComboBox1.ComboBoxForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.hsLabelComboBox1.CustomTheme = false;
            this.hsLabelComboBox1.DisabledComboBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.hsLabelComboBox1.DisabledComboBoxForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.hsLabelComboBox1.EnableLostFocusValidate = true;
            this.hsLabelComboBox1.Excludes = null;
            this.hsLabelComboBox1.Includes = null;
            this.hsLabelComboBox1.KeyName = "0";
            this.hsLabelComboBox1.LabelForeColor = System.Drawing.Color.Black;
            this.hsLabelComboBox1.LabelText = "类型";
            this.hsLabelComboBox1.Location = new System.Drawing.Point(617, 8);
            this.hsLabelComboBox1.Name = "hsLabelComboBox1";
            this.hsLabelComboBox1.Size = new System.Drawing.Size(194, 21);
            this.hsLabelComboBox1.TabIndex = 3;
            // 
            // hsLabelTextBox3
            // 
            this.hsLabelTextBox3.CustomTheme = false;
            this.hsLabelTextBox3.LabelText = "手机";
            this.hsLabelTextBox3.LabelUnitForeColor = System.Drawing.Color.Black;
            this.hsLabelTextBox3.Location = new System.Drawing.Point(406, 9);
            this.hsLabelTextBox3.Name = "hsLabelTextBox3";
            this.hsLabelTextBox3.Size = new System.Drawing.Size(196, 21);
            this.hsLabelTextBox3.TabIndex = 2;
            this.hsLabelTextBox3.TextBoxBackColor = System.Drawing.Color.White;
            this.hsLabelTextBox3.TextBoxForeColor = System.Drawing.Color.Black;
            // 
            // hsLabelTextBox2
            // 
            this.hsLabelTextBox2.CustomTheme = false;
            this.hsLabelTextBox2.LabelText = "名称";
            this.hsLabelTextBox2.LabelUnitForeColor = System.Drawing.Color.Black;
            this.hsLabelTextBox2.Location = new System.Drawing.Point(205, 9);
            this.hsLabelTextBox2.Name = "hsLabelTextBox2";
            this.hsLabelTextBox2.Size = new System.Drawing.Size(195, 21);
            this.hsLabelTextBox2.TabIndex = 1;
            this.hsLabelTextBox2.TextBoxBackColor = System.Drawing.Color.White;
            this.hsLabelTextBox2.TextBoxForeColor = System.Drawing.Color.Black;
            // 
            // hsLabelTextBox1
            // 
            this.hsLabelTextBox1.CustomTheme = false;
            this.hsLabelTextBox1.LabelText = "编号";
            this.hsLabelTextBox1.LabelUnitForeColor = System.Drawing.Color.Black;
            this.hsLabelTextBox1.Location = new System.Drawing.Point(7, 9);
            this.hsLabelTextBox1.Name = "hsLabelTextBox1";
            this.hsLabelTextBox1.Size = new System.Drawing.Size(192, 21);
            this.hsLabelTextBox1.TabIndex = 0;
            this.hsLabelTextBox1.TextBoxBackColor = System.Drawing.Color.White;
            this.hsLabelTextBox1.TextBoxForeColor = System.Drawing.Color.Black;
            // 
            // hsPanel1
            // 
            this.hsPanel1.Controls.Add(this.hsDataGridView1);
            this.hsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hsPanel1.Location = new System.Drawing.Point(0, 65);
            this.hsPanel1.Name = "hsPanel1";
            this.hsPanel1.Size = new System.Drawing.Size(827, 399);
            this.hsPanel1.TabIndex = 3;
            // 
            // DemoCustomerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.hsPanel1);
            this.Controls.Add(this.hsPanel2);
            this.Controls.Add(this.hsPager1);
            this.Controls.Add(this.hsToolStrip1);
            this.Name = "DemoCustomerView";
            this.ParentContainer = this.components;
            this.Size = new System.Drawing.Size(827, 487);
            this.Load += new System.EventHandler(this.CustomerView_Load);
            this.hsToolStrip1.ResumeLayout(false);
            this.hsToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hsDataGridView1)).EndInit();
            this.hsPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hsLabelComboBox1)).EndInit();
            this.hsPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HSControls.hsToolStrip hsToolStrip1;
        private HSControls.hsPager hsPager1;
        private HSControls.hsPanel hsPanel2;
        private HSControls.hsDataGridView hsDataGridView1;
        private UIFrame.hsFunctionToolStripButton toolBtnAdd;
        private UIFrame.hsFunctionToolStripButton toolBtnEdit;
        private UIFrame.hsFunctionToolStripButton toolBtnView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private UIFrame.hsFunctionToolStripButton toolBtnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private UIFrame.hsFunctionToolStripButton toolBtnQuery;
        private HSControls.hsLabelTextBox hsLabelTextBox1;
        private HSControls.hsLabelTextBox hsLabelTextBox2;
        private HSControls.hsLabelTextBox hsLabelTextBox3;
        private HSControls.hsLabelComboBox hsLabelComboBox1;
        private UIFrame.hsColumnLayoutToolStripButton toolBtnLayout;
        private UIFrame.hsFunctionToolStripButton btnExport;
        private UIFrame.hsFunctionToolStripButton btnPrint;
        private HSControls.hsPanel hsPanel1;
    }
}
