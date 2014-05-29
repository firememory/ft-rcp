namespace Hundsun.Framework.Client
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblKeyBoard = new System.Windows.Forms.Label();
            this.lblCancel = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblSettings = new System.Windows.Forms.LinkLabel();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.progressBar = new Hundsun.Framework.HSControls.hsProgressBar();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.lblKeyBoard);
            this.pnlMain.Controls.Add(this.lblCancel);
            this.pnlMain.Controls.Add(this.txtUserID);
            this.pnlMain.Controls.Add(this.lblSettings);
            this.pnlMain.Controls.Add(this.txtPwd);
            this.pnlMain.Controls.Add(this.lblLogin);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.errorProvider.SetIconAlignment(this.pnlMain, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pnlMain.IconAlignment"))));
            this.pnlMain.Name = "pnlMain";
            // 
            // lblKeyBoard
            // 
            this.lblKeyBoard.Image = global::Hundsun.Framework.Client.Properties.Resources.KeyBoardLeave;
            resources.ApplyResources(this.lblKeyBoard, "lblKeyBoard");
            this.lblKeyBoard.Name = "lblKeyBoard";
            this.lblKeyBoard.Click += new System.EventHandler(this.lblKeyBoard_Click);
            this.lblKeyBoard.DoubleClick += new System.EventHandler(this.lblKeyBoard_Click);
            this.lblKeyBoard.MouseEnter += new System.EventHandler(this.lblKeyBoard_MouseEnter);
            this.lblKeyBoard.MouseLeave += new System.EventHandler(this.lblKeyBoard_MouseLeave);
            // 
            // lblCancel
            // 
            this.lblCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lblCancel, "lblCancel");
            this.lblCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
            this.errorProvider.SetIconAlignment(this.lblCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblCancel.IconAlignment"))));
            this.lblCancel.Name = "lblCancel";
            this.lblCancel.Click += new System.EventHandler(this.btnExit_Click);
            this.lblCancel.MouseLeave += new System.EventHandler(this.lblCancel_MouseLeave);
            this.lblCancel.MouseHover += new System.EventHandler(this.lblCancel_MouseHover);
            // 
            // txtUserID
            // 
            this.txtUserID.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtUserID, "txtUserID");
            this.txtUserID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.errorProvider.SetIconAlignment(this.txtUserID, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtUserID.IconAlignment"))));
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt1_KeyDown);
            // 
            // lblSettings
            // 
            resources.ApplyResources(this.lblSettings, "lblSettings");
            this.lblSettings.ForeColor = System.Drawing.Color.Chocolate;
            this.lblSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lblSettings.LinkColor = System.Drawing.Color.White;
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.TabStop = true;
            this.lblSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtPwd, "txtPwd");
            this.txtPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.errorProvider.SetIconAlignment(this.txtPwd, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtPwd.IconAlignment"))));
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt2_KeyDown);
            // 
            // lblLogin
            // 
            this.lblLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lblLogin, "lblLogin");
            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
            this.errorProvider.SetIconAlignment(this.lblLogin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLogin.IconAlignment"))));
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.lblLogin.MouseLeave += new System.EventHandler(this.lblLogin_MouseLeave);
            this.lblLogin.MouseHover += new System.EventHandler(this.lblLogin_MouseHover);
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(147)))), ((int)(((byte)(196)))));
            this.errorProvider.SetIconAlignment(this.lblVersion, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblVersion.IconAlignment"))));
            this.lblVersion.Name = "lblVersion";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.errorProvider.SetIconAlignment(this.lblMessage, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblMessage.IconAlignment"))));
            this.lblMessage.Name = "lblMessage";
            // 
            // progressBar
            // 
            this.progressBar.CustomTheme = false;
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.ForeColor = System.Drawing.Color.DarkOrange;
            this.progressBar.Name = "progressBar";
            this.progressBar.Step = 20;
            // 
            // FormLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormLogin";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormLogin_KeyDown);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblCancel;
        private System.Windows.Forms.LinkLabel lblSettings;
        private System.Windows.Forms.Label lblMessage;
        private HSControls.hsProgressBar progressBar;
        private System.Windows.Forms.Label lblKeyBoard;
    }
}