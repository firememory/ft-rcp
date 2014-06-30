namespace Hundsun.Framework.Platform
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.addInWorkspace = new Hundsun.Framework.Platform.HSWorkspace();
            this.topMenuStrip = new System.Windows.Forms.MenuStrip();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.BackColor = System.Drawing.Color.Black;
            this.toolStripContainer.ContentPanel.Controls.Add(this.addInWorkspace);
            resources.ApplyResources(this.toolStripContainer.ContentPanel, "toolStripContainer.ContentPanel");
            resources.ApplyResources(this.toolStripContainer, "toolStripContainer");
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.RightToolStripPanelVisible = false;
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripContainer.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // addInWorkspace
            // 
            this.addInWorkspace.BackColor = System.Drawing.Color.White;
            this.addInWorkspace.BackgroundImage = global::Hundsun.Framework.Platform.Properties.Resources.Background;
            resources.ApplyResources(this.addInWorkspace, "addInWorkspace");
            this.addInWorkspace.Name = "addInWorkspace";
            // 
            // topMenuStrip
            // 
            resources.ApplyResources(this.topMenuStrip, "topMenuStrip");
            this.topMenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.topMenuStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.topMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.topMenuStrip.Name = "topMenuStrip";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpen,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Name = "tsmiOpen";
            resources.ApplyResources(this.tsmiOpen, "tsmiOpen");
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            resources.ApplyResources(this.tsmiExit, "tsmiExit");
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.BorderWidth = 2;
            resources.ApplyResources(this, "$this");
            this.ColorTable.Back = System.Drawing.Color.White;
            this.ColorTable.Border = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(126)))), ((int)(((byte)(168)))));
            this.ColorTable.CaptionActive = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(167)))), ((int)(((byte)(233)))));
            this.ColorTable.CaptionDeactive = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(167)))), ((int)(((byte)(233)))));
            this.ColorTable.CaptionText = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(111)))), ((int)(((byte)(152)))));
            this.ColorTable.ControlBoxActive = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(136)))), ((int)(((byte)(204)))));
            this.ColorTable.ControlBoxDeactive = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(156)))), ((int)(((byte)(204)))));
            this.ColorTable.ControlBoxHover = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(107)))), ((int)(((byte)(151)))));
            this.ColorTable.ControlBoxInnerBorder = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ColorTable.ControlBoxPressed = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(76)))), ((int)(((byte)(111)))));
            this.ColorTable.ControlCloseBoxHover = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(66)))), ((int)(((byte)(22)))));
            this.ColorTable.ControlCloseBoxPressed = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(53)))), ((int)(((byte)(17)))));
            this.ColorTable.InnerBorder = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.topMenuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.topMenuStrip;
            this.Name = "FormMain";
            this.Radius = 4;
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip topMenuStrip;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private HSWorkspace addInWorkspace;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}