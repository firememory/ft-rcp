using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Hundsun.Framework.AddIn;
using System.Windows.Forms;
using Hundsun.Framework.IoC;
using Hundsun.Framework.HSControls;

namespace Hundsun.Framework.AddIns
{
    [AddInToolStripItem("Lock")]
    public partial class LockToolStripItem : hsToolStripMenuItem
    {
        public LockToolStripItem()
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
            this.addInContainer.Lock();
        }
    }
}
