using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.AddIn;
using Hundsun.Framework.IoC;
using Hundsun.Framework.Communication;
using Hundsun.Framework.Client;

namespace Hundsun.Framework.AddIns
{
    [AddInToolStripItem("Password")]
    public partial class PasswordToolStripItem : ToolStripMenuItem
    {
        public PasswordToolStripItem()
        {
            InitializeComponent();
        }

        private ICommunication communication;
        [Dependency]
        public ICommunication Communication
        {
            set { this.communication = value; }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            new FormPassword(this.communication).ShowDialog();
        }
    }
}
