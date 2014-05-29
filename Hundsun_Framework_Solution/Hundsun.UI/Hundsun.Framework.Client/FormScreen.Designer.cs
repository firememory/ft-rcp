namespace Hundsun.Framework.Client
{
    partial class FormScreen
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
            this.deckWorkspace = new Hundsun.Framework.MVP.WinForms.DeckWorkspace();
            this.SuspendLayout();
            // 
            // deckWorkspace
            // 
            this.deckWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckWorkspace.Location = new System.Drawing.Point(3, 24);
            this.deckWorkspace.Name = "deckWorkspace";
            this.deckWorkspace.Size = new System.Drawing.Size(794, 473);
            this.deckWorkspace.TabIndex = 21;
            this.deckWorkspace.Text = "deckWorkspace1";
            // 
            // FormScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.ColorTable.Back = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.ColorTable.Border = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(126)))), ((int)(((byte)(168)))));
            this.ColorTable.CaptionActive = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(188)))), ((int)(((byte)(254)))));
            this.ColorTable.CaptionDeactive = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.ColorTable.CaptionText = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(111)))), ((int)(((byte)(152)))));
            this.ColorTable.ControlBoxActive = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.ColorTable.ControlBoxDeactive = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(172)))), ((int)(((byte)(218)))));
            this.ColorTable.ControlBoxHover = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(114)))), ((int)(((byte)(151)))));
            this.ColorTable.ControlBoxInnerBorder = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ColorTable.ControlBoxPressed = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(84)))), ((int)(((byte)(111)))));
            this.ColorTable.ControlCloseBoxHover = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(66)))), ((int)(((byte)(22)))));
            this.ColorTable.ControlCloseBoxPressed = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(53)))), ((int)(((byte)(17)))));
            this.ColorTable.InnerBorder = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.deckWorkspace);
            this.Name = "FormScreen";
            this.Text = "FormScreenEx";
            this.ResumeLayout(false);

        }

        #endregion

        private MVP.WinForms.DeckWorkspace deckWorkspace;
    }
}