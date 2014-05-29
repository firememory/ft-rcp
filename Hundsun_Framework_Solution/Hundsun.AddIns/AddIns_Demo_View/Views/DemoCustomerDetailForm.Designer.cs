namespace Hundsun.Framework.AddIns
{
    partial class DemoCustomerDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoCustomerDetailForm));
            this.pnlMain = new Hundsun.Framework.HSControls.hsPanel();
            this.btnReset = new Hundsun.Framework.HSControls.hsButton();
            this.btnClose = new Hundsun.Framework.HSControls.hsButton();
            this.btnOK = new Hundsun.Framework.UIFrame.hsFunctionButton();
            this.dtpTradeDate = new Hundsun.Framework.HSControls.hsLabelDateTimePicker();
            this.numTradeMoney = new Hundsun.Framework.HSControls.hsLabelNumericEditor();
            this.cmbLevel = new Hundsun.Framework.HSControls.hsLabelComboBox();
            this.cmbType = new Hundsun.Framework.HSControls.hsLabelComboBox();
            this.txtEmail = new Hundsun.Framework.HSControls.hsLabelTextBox();
            this.txtPhone = new Hundsun.Framework.HSControls.hsLabelTextBox();
            this.txtTel = new Hundsun.Framework.HSControls.hsLabelTextBox();
            this.txtAddr = new Hundsun.Framework.HSControls.hsLabelTextBox();
            this.txtName = new Hundsun.Framework.HSControls.hsLabelTextBox();
            this.txtId = new Hundsun.Framework.HSControls.hsLabelTextBox();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnReset);
            this.pnlMain.Controls.Add(this.btnClose);
            this.pnlMain.Controls.Add(this.btnOK);
            this.pnlMain.Controls.Add(this.dtpTradeDate);
            this.pnlMain.Controls.Add(this.numTradeMoney);
            this.pnlMain.Controls.Add(this.cmbLevel);
            this.pnlMain.Controls.Add(this.cmbType);
            this.pnlMain.Controls.Add(this.txtEmail);
            this.pnlMain.Controls.Add(this.txtPhone);
            this.pnlMain.Controls.Add(this.txtTel);
            this.pnlMain.Controls.Add(this.txtAddr);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.txtId);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(3, 30);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(739, 263);
            this.pnlMain.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.ClearData;
            this.btnReset.CustomTheme = false;
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReset.Location = new System.Drawing.Point(385, 221);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 29);
            this.btnReset.TabIndex = 13;
            this.btnReset.Text = " 重置";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnClose
            // 
            this.btnClose.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.Close;
            this.btnClose.CustomTheme = false;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(629, 221);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 29);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = " 关闭";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.CommandType = Hundsun.Framework.HSControls.HSButton_CommandType.Save;
            this.btnOK.CustomTheme = false;
            this.btnOK.EnableAutoValidate = true;
            this.btnOK.FunctionID = null;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(495, 221);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(99, 29);
            this.btnOK.TabIndex = 12;
            this.btnOK.TargetValidateContainer = this.pnlMain;
            this.btnOK.Text = " 保存";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.AfterAutoValidate += new System.EventHandler<Hundsun.Framework.HSControls.AutoValidateResultEventArgs>(this.btnOK_AfterAutoValidate);
            this.btnOK.BeforeAutoValidate += new System.EventHandler<Hundsun.Framework.HSControls.AutoValidateCustomEventArgs>(this.btnOK_BeforeAutoValidate);
            // 
            // dtpTradeDate
            // 
            this.dtpTradeDate.LabelText = "客户交易日期";
            this.dtpTradeDate.LabelWidth = 100;
            this.dtpTradeDate.Location = new System.Drawing.Point(383, 188);
            this.dtpTradeDate.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dtpTradeDate.Name = "dtpTradeDate";
            this.dtpTradeDate.Size = new System.Drawing.Size(320, 25);
            this.dtpTradeDate.TabIndex = 10;
            this.dtpTradeDate.Value = new System.DateTime(2014, 5, 28, 14, 40, 55, 629);
            // 
            // numTradeMoney
            // 
            this.numTradeMoney.DecimalPlaces = 2;
            this.numTradeMoney.LabelText = "客户交易总金额";
            this.numTradeMoney.LabelWidth = 100;
            this.numTradeMoney.Location = new System.Drawing.Point(371, 138);
            this.numTradeMoney.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.numTradeMoney.Name = "numTradeMoney";
            this.numTradeMoney.Size = new System.Drawing.Size(320, 25);
            this.numTradeMoney.TabIndex = 9;
            // 
            // cmbLevel
            // 
            this.cmbLevel.ComboBoxBackColor = System.Drawing.Color.White;
            this.cmbLevel.ComboBoxForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbLevel.CustomTheme = false;
            this.cmbLevel.DisabledComboBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.cmbLevel.DisabledComboBoxForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.cmbLevel.EnableLostFocusValidate = true;
            this.cmbLevel.Excludes = null;
            this.cmbLevel.Includes = null;
            this.cmbLevel.InsertNullItem = true;
            this.cmbLevel.KeyName = "0";
            this.cmbLevel.LabelForeColor = System.Drawing.Color.Black;
            this.cmbLevel.LabelText = "客户级别";
            this.cmbLevel.LabelWidth = 100;
            this.cmbLevel.Location = new System.Drawing.Point(379, 96);
            this.cmbLevel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(320, 25);
            this.cmbLevel.TabIndex = 8;
            // 
            // cmbType
            // 
            this.cmbType.AllowEmpty = false;
            this.cmbType.ComboBoxBackColor = System.Drawing.Color.White;
            this.cmbType.ComboBoxForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbType.ComboBoxSelectionModel = Hundsun.Framework.HSControls.ComboBoxSelectionModel.Multiple;
            this.cmbType.CustomTheme = false;
            this.cmbType.DisabledComboBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.cmbType.DisabledComboBoxForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.cmbType.EnableLostFocusValidate = true;
            this.cmbType.Excludes = null;
            this.cmbType.Includes = null;
            this.cmbType.KeyName = "0";
            this.cmbType.LabelForeColor = System.Drawing.Color.Black;
            this.cmbType.LabelText = "客户类型";
            this.cmbType.LabelWidth = 100;
            this.cmbType.Location = new System.Drawing.Point(360, 61);
            this.cmbType.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(320, 25);
            this.cmbType.TabIndex = 7;
            this.cmbType.HSValidated += new System.EventHandler<Hundsun.Framework.HSControls.HSValidatedEventArgs>(this.cmbType_HSValidated);
            // 
            // txtEmail
            // 
            this.txtEmail.AutoCompleteItems = new string[0];
            this.txtEmail.AutoCompleteMaximumSize = new System.Drawing.Size(180, 200);
            this.txtEmail.CustomTheme = false;
            this.txtEmail.LabelText = "客户Email";
            this.txtEmail.LabelUnitForeColor = System.Drawing.Color.Black;
            this.txtEmail.LabelWidth = 100;
            this.txtEmail.Location = new System.Drawing.Point(348, 15);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.RegularMode = Hundsun.Framework.HSControls.RegularValidationMode.Email;
            this.txtEmail.Size = new System.Drawing.Size(320, 25);
            this.txtEmail.TabIndex = 6;
            this.txtEmail.TextBoxBackColor = System.Drawing.Color.White;
            this.txtEmail.TextBoxForeColor = System.Drawing.Color.Black;
            // 
            // txtPhone
            // 
            this.txtPhone.AutoCompleteItems = new string[0];
            this.txtPhone.AutoCompleteMaximumSize = new System.Drawing.Size(180, 200);
            this.txtPhone.CustomTheme = false;
            this.txtPhone.LabelText = "客户手机";
            this.txtPhone.LabelUnitForeColor = System.Drawing.Color.Black;
            this.txtPhone.LabelWidth = 100;
            this.txtPhone.Location = new System.Drawing.Point(16, 188);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.RegularMode = Hundsun.Framework.HSControls.RegularValidationMode.Phone;
            this.txtPhone.Size = new System.Drawing.Size(320, 25);
            this.txtPhone.TabIndex = 5;
            this.txtPhone.TextBoxBackColor = System.Drawing.Color.White;
            this.txtPhone.TextBoxForeColor = System.Drawing.Color.Black;
            // 
            // txtTel
            // 
            this.txtTel.AutoCompleteItems = new string[0];
            this.txtTel.AutoCompleteMaximumSize = new System.Drawing.Size(180, 200);
            this.txtTel.CustomTheme = false;
            this.txtTel.LabelText = "客户电话";
            this.txtTel.LabelUnitForeColor = System.Drawing.Color.Black;
            this.txtTel.LabelWidth = 100;
            this.txtTel.Location = new System.Drawing.Point(16, 138);
            this.txtTel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtTel.Name = "txtTel";
            this.txtTel.RegularMode = Hundsun.Framework.HSControls.RegularValidationMode.Tel;
            this.txtTel.Size = new System.Drawing.Size(320, 25);
            this.txtTel.TabIndex = 4;
            this.txtTel.TextBoxBackColor = System.Drawing.Color.White;
            this.txtTel.TextBoxForeColor = System.Drawing.Color.Black;
            // 
            // txtAddr
            // 
            this.txtAddr.AutoCompleteItems = new string[0];
            this.txtAddr.AutoCompleteMaximumSize = new System.Drawing.Size(180, 200);
            this.txtAddr.CustomTheme = false;
            this.txtAddr.LabelText = "客户地址";
            this.txtAddr.LabelUnitForeColor = System.Drawing.Color.Black;
            this.txtAddr.LabelWidth = 100;
            this.txtAddr.Location = new System.Drawing.Point(16, 82);
            this.txtAddr.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(320, 25);
            this.txtAddr.TabIndex = 3;
            this.txtAddr.TextBoxBackColor = System.Drawing.Color.White;
            this.txtAddr.TextBoxForeColor = System.Drawing.Color.Black;
            // 
            // txtName
            // 
            this.txtName.AllowEmpty = false;
            this.txtName.AutoCompleteItems = new string[0];
            this.txtName.AutoCompleteMaximumSize = new System.Drawing.Size(180, 200);
            this.txtName.CustomTheme = false;
            this.txtName.LabelText = "名称";
            this.txtName.LabelUnitForeColor = System.Drawing.Color.Black;
            this.txtName.LabelWidth = 100;
            this.txtName.Location = new System.Drawing.Point(16, 49);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtName.MaxLength = 20;
            this.txtName.MinLength = 1;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(320, 25);
            this.txtName.TabIndex = 2;
            this.txtName.TextBoxBackColor = System.Drawing.Color.White;
            this.txtName.TextBoxForeColor = System.Drawing.Color.Black;
            this.txtName.HSValidated += new System.EventHandler<Hundsun.Framework.HSControls.HSValidatedEventArgs>(this.txtName_HSValidated);
            this.txtName.HSValidating += new System.EventHandler<Hundsun.Framework.HSControls.HSValidatingEventArgs>(this.txtName_HSValidating);
            // 
            // txtId
            // 
            this.txtId.AutoCompleteItems = new string[0];
            this.txtId.AutoCompleteMaximumSize = new System.Drawing.Size(180, 200);
            this.txtId.CustomTheme = false;
            this.txtId.LabelText = "编号";
            this.txtId.LabelUnitForeColor = System.Drawing.Color.Black;
            this.txtId.LabelWidth = 100;
            this.txtId.Location = new System.Drawing.Point(16, 15);
            this.txtId.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(320, 25);
            this.txtId.TabIndex = 1;
            this.txtId.TextBoxBackColor = System.Drawing.Color.White;
            this.txtId.TextBoxForeColor = System.Drawing.Color.Black;
            // 
            // DemoCustomerDetailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(745, 295);
            this.ColorTable.Back = System.Drawing.Color.WhiteSmoke;
            this.ColorTable.Border = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(126)))), ((int)(((byte)(168)))));
            this.ColorTable.CaptionActive = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(167)))), ((int)(((byte)(233)))));
            this.ColorTable.CaptionDeactive = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(167)))), ((int)(((byte)(233)))));
            this.ColorTable.CaptionText = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(111)))), ((int)(((byte)(152)))));
            this.ColorTable.ControlBoxActive = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(136)))), ((int)(((byte)(204)))));
            this.ColorTable.ControlBoxDeactive = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(172)))), ((int)(((byte)(218)))));
            this.ColorTable.ControlBoxHover = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(107)))), ((int)(((byte)(151)))));
            this.ColorTable.ControlBoxInnerBorder = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ColorTable.ControlBoxPressed = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(76)))), ((int)(((byte)(111)))));
            this.ColorTable.ControlCloseBoxHover = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(66)))), ((int)(((byte)(22)))));
            this.ColorTable.ControlCloseBoxPressed = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(53)))), ((int)(((byte)(17)))));
            this.ColorTable.InnerBorder = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlMain);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "DemoCustomerDetailForm";
            this.ParentContainer = this.components;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "客户基本信息";
            this.Load += new System.EventHandler(this.CustomerDetailForm_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HSControls.hsPanel pnlMain;
        private HSControls.hsButton btnReset;
        private HSControls.hsButton btnClose;
        private UIFrame.hsFunctionButton btnOK;
        private HSControls.hsLabelDateTimePicker dtpTradeDate;
        private HSControls.hsLabelNumericEditor numTradeMoney;
        private HSControls.hsLabelComboBox cmbLevel;
        private HSControls.hsLabelComboBox cmbType;
        private HSControls.hsLabelTextBox txtEmail;
        private HSControls.hsLabelTextBox txtPhone;
        private HSControls.hsLabelTextBox txtTel;
        private HSControls.hsLabelTextBox txtAddr;
        private HSControls.hsLabelTextBox txtName;
        private HSControls.hsLabelTextBox txtId;

    }
}