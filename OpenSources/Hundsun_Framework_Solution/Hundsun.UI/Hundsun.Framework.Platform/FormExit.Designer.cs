namespace Hundsun.Framework.Platform
{
    partial class FormExit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExit));
            this.lblMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExitSystem = new Hundsun.Framework.HSControls.hsButton();
            this.btnHideInTask = new Hundsun.Framework.HSControls.hsButton();
            this.chkNoShowAgain = new Hundsun.Framework.HSControls.hsCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Name = "lblMessage";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Hundsun.Framework.Platform.Properties.Resources.Question;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // btnExitSystem
            // 
            this.btnExitSystem.BackColor = System.Drawing.SystemColors.Control;
            this.btnExitSystem.CustomTheme = false;
            resources.ApplyResources(this.btnExitSystem, "btnExitSystem");
            this.btnExitSystem.Name = "btnExitSystem";
            this.btnExitSystem.UseVisualStyleBackColor = true;
            this.btnExitSystem.Click += new System.EventHandler(this.btnExitSystem_Click);
            // 
            // btnHideInTask
            // 
            this.btnHideInTask.BackColor = System.Drawing.SystemColors.Control;
            this.btnHideInTask.CustomTheme = false;
            resources.ApplyResources(this.btnHideInTask, "btnHideInTask");
            this.btnHideInTask.Name = "btnHideInTask";
            this.btnHideInTask.UseVisualStyleBackColor = true;
            this.btnHideInTask.Click += new System.EventHandler(this.btnHideInTask_Click);
            // 
            // chkNoShowAgain
            // 
            resources.ApplyResources(this.chkNoShowAgain, "chkNoShowAgain");
            this.chkNoShowAgain.CustomTheme = false;
            this.chkNoShowAgain.Name = "chkNoShowAgain";
            this.chkNoShowAgain.UseVisualStyleBackColor = false;
            this.chkNoShowAgain.Click += new System.EventHandler(this.chkShowAgain_Click);
            // 
            // FormExit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BorderWidth = 1;
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
            this.Controls.Add(this.chkNoShowAgain);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnExitSystem);
            this.Controls.Add(this.btnHideInTask);
            this.KeyPreview = true;
            this.Name = "FormExit";
            this.Opacity = 0.98D;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormExit_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Hundsun.Framework.HSControls.hsButton btnExitSystem;
        private Hundsun.Framework.HSControls.hsButton btnHideInTask;
        private HSControls.hsCheckBox chkNoShowAgain;
    }
}