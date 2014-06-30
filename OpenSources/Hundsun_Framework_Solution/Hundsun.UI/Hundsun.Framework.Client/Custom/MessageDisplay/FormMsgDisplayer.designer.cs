namespace Hundsun.Framework.Client
{
    partial class FormMsgDisplayer
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
            this.hsContextMenuStrip1 = new Hundsun.Framework.HSControls.hsContextMenuStrip();
            this.itemViewMsg = new System.Windows.Forms.ToolStripMenuItem();
            this.itemViewAllMsg = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMarkMsgRead = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMarkAllMsgRead = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSpeedSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSpeedSlow = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSpeedNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSpeedFast = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFontSize = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFontColor = new System.Windows.Forms.ToolStripMenuItem();
            this.itemHide = new System.Windows.Forms.ToolStripMenuItem();
            this.itemOpenMainForm = new System.Windows.Forms.ToolStripMenuItem();
            this.itemExitSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.scrollingText = new Hundsun.Framework.HSControls.hsScrollingTextEx();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.hsContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollingText)).BeginInit();
            this.SuspendLayout();
            // 
            // hsContextMenuStrip1
            // 
            this.hsContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemViewMsg,
            this.itemViewAllMsg,
            this.itemMarkMsgRead,
            this.itemMarkAllMsgRead,
            this.toolStripSeparator1,
            this.itemSettings,
            this.itemHide,
            this.itemOpenMainForm,
            this.itemExitSystem});
            this.hsContextMenuStrip1.Name = "hsContextMenuStrip1";
            this.hsContextMenuStrip1.Size = new System.Drawing.Size(201, 186);
            this.hsContextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.hsContextMenuStrip1_Opening);
            // 
            // itemViewMsg
            // 
            this.itemViewMsg.Name = "itemViewMsg";
            this.itemViewMsg.Size = new System.Drawing.Size(200, 22);
            this.itemViewMsg.Text = "查看消息(&V)";
            this.itemViewMsg.ToolTipText = "查看消息";
            this.itemViewMsg.Click += new System.EventHandler(this.itemViewMsg_Click);
            // 
            // itemViewAllMsg
            // 
            this.itemViewAllMsg.Name = "itemViewAllMsg";
            this.itemViewAllMsg.Size = new System.Drawing.Size(200, 22);
            this.itemViewAllMsg.Text = "查看所有消息(&A)";
            this.itemViewAllMsg.ToolTipText = "查看所有消息";
            this.itemViewAllMsg.Click += new System.EventHandler(this.itemViewAllMsg_Click);
            // 
            // itemMarkMsgRead
            // 
            this.itemMarkMsgRead.Name = "itemMarkMsgRead";
            this.itemMarkMsgRead.Size = new System.Drawing.Size(200, 22);
            this.itemMarkMsgRead.Text = "标记为已读(&R)";
            this.itemMarkMsgRead.ToolTipText = "标记为已读";
            this.itemMarkMsgRead.Click += new System.EventHandler(this.itemMarkMsgRead_Click);
            // 
            // itemMarkAllMsgRead
            // 
            this.itemMarkAllMsgRead.Name = "itemMarkAllMsgRead";
            this.itemMarkAllMsgRead.Size = new System.Drawing.Size(200, 22);
            this.itemMarkAllMsgRead.Text = "标记所有消息为已读(&C)";
            this.itemMarkAllMsgRead.ToolTipText = "标记所有消息为已读";
            this.itemMarkAllMsgRead.Click += new System.EventHandler(this.itemMarkAllMsgRead_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // itemSettings
            // 
            this.itemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSpeedSettings,
            this.itemFontSize,
            this.itemFontColor});
            this.itemSettings.Name = "itemSettings";
            this.itemSettings.Size = new System.Drawing.Size(200, 22);
            this.itemSettings.Text = "设置(&S)";
            this.itemSettings.ToolTipText = "设置";
            // 
            // itemSpeedSettings
            // 
            this.itemSpeedSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSpeedSlow,
            this.itemSpeedNormal,
            this.itemSpeedFast});
            this.itemSpeedSettings.Name = "itemSpeedSettings";
            this.itemSpeedSettings.Size = new System.Drawing.Size(140, 22);
            this.itemSpeedSettings.Text = "速度(&S)";
            this.itemSpeedSettings.ToolTipText = "速度";
            // 
            // itemSpeedSlow
            // 
            this.itemSpeedSlow.Name = "itemSpeedSlow";
            this.itemSpeedSlow.Size = new System.Drawing.Size(106, 22);
            this.itemSpeedSlow.Text = "慢(&S)";
            this.itemSpeedSlow.ToolTipText = "慢";
            this.itemSpeedSlow.Click += new System.EventHandler(this.itemSpeedSlow_Click);
            // 
            // itemSpeedNormal
            // 
            this.itemSpeedNormal.Name = "itemSpeedNormal";
            this.itemSpeedNormal.Size = new System.Drawing.Size(106, 22);
            this.itemSpeedNormal.Text = "中(&N)";
            this.itemSpeedNormal.ToolTipText = "中";
            this.itemSpeedNormal.Click += new System.EventHandler(this.itemSpeedNormal_Click);
            // 
            // itemSpeedFast
            // 
            this.itemSpeedFast.Name = "itemSpeedFast";
            this.itemSpeedFast.Size = new System.Drawing.Size(106, 22);
            this.itemSpeedFast.Text = "快(&F)";
            this.itemSpeedFast.ToolTipText = "快";
            this.itemSpeedFast.Click += new System.EventHandler(this.itemSpeedFast_Click);
            // 
            // itemFontSize
            // 
            this.itemFontSize.Name = "itemFontSize";
            this.itemFontSize.Size = new System.Drawing.Size(140, 22);
            this.itemFontSize.Text = "字体大小(&F)";
            this.itemFontSize.ToolTipText = "字体大小";
            this.itemFontSize.Click += new System.EventHandler(this.itemFontSize_Click);
            // 
            // itemFontColor
            // 
            this.itemFontColor.Name = "itemFontColor";
            this.itemFontColor.Size = new System.Drawing.Size(140, 22);
            this.itemFontColor.Text = "字体颜色(&C)";
            this.itemFontColor.ToolTipText = "字体颜色";
            this.itemFontColor.Click += new System.EventHandler(this.itemFontColor_Click);
            // 
            // itemHide
            // 
            this.itemHide.Name = "itemHide";
            this.itemHide.Size = new System.Drawing.Size(200, 22);
            this.itemHide.Text = "隐藏(&H)";
            this.itemHide.ToolTipText = "隐藏";
            this.itemHide.Click += new System.EventHandler(this.itemHide_Click);
            // 
            // itemOpenMainForm
            // 
            this.itemOpenMainForm.Name = "itemOpenMainForm";
            this.itemOpenMainForm.Size = new System.Drawing.Size(200, 22);
            this.itemOpenMainForm.Text = "打开主窗口(&O)";
            this.itemOpenMainForm.ToolTipText = "打开主窗口";
            this.itemOpenMainForm.Click += new System.EventHandler(this.itemOpenMainForm_Click);
            // 
            // itemExitSystem
            // 
            this.itemExitSystem.Name = "itemExitSystem";
            this.itemExitSystem.Size = new System.Drawing.Size(200, 22);
            this.itemExitSystem.Text = "退出系统(&X)";
            this.itemExitSystem.ToolTipText = "退出系统";
            this.itemExitSystem.Click += new System.EventHandler(this.itemExitSystem_Click);
            // 
            // scrollingText
            // 
            this.scrollingText.ContextMenuStrip = this.hsContextMenuStrip1;
            this.scrollingText.Cursor = System.Windows.Forms.Cursors.Default;
            this.scrollingText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollingText.Location = new System.Drawing.Point(0, 0);
            this.scrollingText.Name = "scrollingText";
            this.scrollingText.Size = new System.Drawing.Size(300, 40);
            this.scrollingText.TabIndex = 1;
            this.scrollingText.Text = "hsScrollingTextEx1";
            // 
            // FormMsgDisplayer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(300, 40);
            this.Controls.Add(this.scrollingText);
            this.Name = "FormMsgDisplayer";
            this.Opacity = 0.8D;
            this.Load += new System.EventHandler(this.HsFormFloating_Load);
            this.hsContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scrollingText)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HSControls.hsContextMenuStrip hsContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem itemViewMsg;
        private System.Windows.Forms.ToolStripMenuItem itemViewAllMsg;
        private System.Windows.Forms.ToolStripMenuItem itemMarkMsgRead;
        private System.Windows.Forms.ToolStripMenuItem itemMarkAllMsgRead;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem itemSettings;
        private System.Windows.Forms.ToolStripMenuItem itemSpeedSettings;
        private System.Windows.Forms.ToolStripMenuItem itemSpeedSlow;
        private System.Windows.Forms.ToolStripMenuItem itemSpeedNormal;
        private System.Windows.Forms.ToolStripMenuItem itemSpeedFast;
        private System.Windows.Forms.ToolStripMenuItem itemFontSize;
        private System.Windows.Forms.ToolStripMenuItem itemOpenMainForm;
        private System.Windows.Forms.ToolStripMenuItem itemHide;
        private System.Windows.Forms.ToolStripMenuItem itemExitSystem;
        private System.Windows.Forms.FontDialog fontDialog1;
        private HSControls.hsScrollingTextEx scrollingText;
        private System.Windows.Forms.ToolStripMenuItem itemFontColor;
        private System.Windows.Forms.ColorDialog colorDialog1;

    }
}