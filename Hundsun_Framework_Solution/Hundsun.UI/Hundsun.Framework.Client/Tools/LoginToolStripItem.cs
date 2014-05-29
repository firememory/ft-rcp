using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.AddIn;
using Hundsun.Framework.IoC;
using Hundsun.Framework.Client;

namespace Hundsun.Framework.AddIns
{
    [AddInToolStripItem("Login")]
    public partial class LoginToolStripItem : ToolStripMenuItem
    {
        public LoginToolStripItem()
        {
            InitializeComponent();
        }

        private IBusiness business;
        [Dependency]
        public IBusiness Business
        {
            set { this.business = value; }
        }

        private IExecuter executer;
        [Dependency]
        public IExecuter Executer
        {
            set { this.executer = value; }
        }

        private IAddInContainer addInContainer;
        [Dependency]
        public IAddInContainer AddInContainer
        {
            set { this.addInContainer = value; }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (this.addInContainer.Args == null)
                return;
            this.executer.UnLock(this.addInContainer.Args.ToString());
        }
    }
}
