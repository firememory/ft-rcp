namespace Hundsun.Framework.Client
{
    partial class FormPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPassword));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtOld = new System.Windows.Forms.TextBox();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.txtRepeat = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = global::Hundsun.Framework.Client.Properties.Resources.updateOK1;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(158, 223);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 22);
            this.btnOK.TabIndex = 3;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnOK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnOK.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnOK.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::Hundsun.Framework.Client.Properties.Resources.updateCancel1;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(253, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 22);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblMessage.Location = new System.Drawing.Point(0, 265);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(427, 43);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOld
            // 
            this.txtOld.Location = new System.Drawing.Point(158, 119);
            this.txtOld.Name = "txtOld";
            this.txtOld.PasswordChar = '#';
            this.txtOld.Size = new System.Drawing.Size(177, 21);
            this.txtOld.TabIndex = 0;
            this.txtOld.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOld_KeyPress);
            // 
            // txtNew
            // 
            this.txtNew.Location = new System.Drawing.Point(158, 151);
            this.txtNew.Name = "txtNew";
            this.txtNew.PasswordChar = '#';
            this.txtNew.Size = new System.Drawing.Size(177, 21);
            this.txtNew.TabIndex = 1;
            this.txtNew.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNew_KeyPress);
            // 
            // txtRepeat
            // 
            this.txtRepeat.Location = new System.Drawing.Point(158, 183);
            this.txtRepeat.Name = "txtRepeat";
            this.txtRepeat.PasswordChar = '#';
            this.txtRepeat.Size = new System.Drawing.Size(177, 21);
            this.txtRepeat.TabIndex = 2;
            this.txtRepeat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRepeat_KeyPress);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FormPassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(427, 308);
            this.Controls.Add(this.txtRepeat);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.txtOld);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPassword";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormPassword_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPassword_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormPassword_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtOld;
        private System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.TextBox txtRepeat;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}