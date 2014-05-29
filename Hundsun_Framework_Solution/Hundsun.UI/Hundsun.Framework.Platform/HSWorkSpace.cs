using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.MVP.WinForms;

namespace Hundsun.Framework.Platform
{
    public class HSWorkspace : DeckWorkspace
    {
        public HSWorkspace()
        {
            this.SetActiveSmartPartBefore += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    if (this.SetIdle != null)
                    {
                        this.SetIdle(this, EventArgs.Empty);
                    }
                }
                );
            this.SetActiveSmartPartOver += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    if (this.SetUsable != null)
                    {
                        this.SetUsable(this, EventArgs.Empty);
                    }
                }
                );
        }
        public event EventHandler SetIdle;
        public event EventHandler SetUsable;
    }
}
