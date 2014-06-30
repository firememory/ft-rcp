using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.AddIn;
using System.Reflection;
using Hundsun.Framework.IoC;

namespace Hundsun.Framework.AddIns
{
    [AddInToolStripItem("Logout")]
    public partial class LogoutToolStripItem : ToolStripMenuItem
    {
        public LogoutToolStripItem()
        {
            InitializeComponent();
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
            //强制退出系统
            if (this.addInContainer != null)
                this.addInContainer.CloseApplication();
        }
    }
}
