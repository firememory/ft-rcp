using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.AddIn.WinForms;
using Hundsun.Framework.Entity;
using Hundsun.Framework.MVP.SmartParts;

namespace Hundsun.Framework.Client
{
    [SmartPart]
    public partial class FormScreen : Hundsun.Framework.HSControls.Skin.SkinForm
    {
        public FormScreen(AddInController addInController)
        {
            InitializeComponent();
            this.addInController = addInController;

            #region 加载菜单
            this.addInController.CreateMenu(
                delegate()
                {
                    return VersionUtility.Deserialize<Navigation>("MainMenuStrip");
                },
                delegate(MenuStrip menu)
                {
                    try
                    {
                        menu.SuspendLayout();
                        this.addInController.LoadToolStrip(
                            delegate()
                            {
                                return VersionUtility.Deserialize<ToolStripObject>("MenuStripObject");
                            }, menu, VersionUtility.GetObject<Image>);
                    }
                    finally
                    {
                        menu.ResumeLayout(false);
                    }
                }, this.MainMenuStrip, this.tsi_Click,
                delegate(string persistString)
                {
                    return this.addInController.LoadToolStripItem(persistString, false, false) as ToolStripMenuItem;
                }, VersionUtility.GetObject<Image>);
            #endregion

            this.addInController.LoadDocument(deckWorkspace, "K线");
            this.addInController.AddNewPart(deckWorkspace, "分时图");
        }

        private AddInController addInController;

        private void tsi_Click(object sender, EventArgs e)
        {
            //功能CommandName
            string functionID = (sender as ToolStripItem).Tag as string;
            //菜单CommandID
            string commandID = (sender as ToolStripItem).AccessibleName;
            this.addInController.AddNewPart(deckWorkspace, new List<object> { functionID, commandID });
        }
    }
}
