using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.HSControls.Skin;
using Hundsun.Framework.AddIn.WinForms;

namespace Hundsun.Framework.Platform
{
    public partial class FormExit : UIFrame.AddInFormBase
    {
        public FormExit()
        {
            InitializeComponent();
            //默认，用户单击提示窗体的关闭按钮，只是关闭当前提示窗体而已
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// 隐藏到托盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHideInTask_Click(object sender, EventArgs e)
        {
            //如果下次不显示该提示框
            if (this.chkNoShowAgain.Checked)
                AddInUtility.IsHideSystemInNotify = true;
            //隐藏到托盘
            this.DialogResult = DialogResult.Yes;
        }
        /// <summary>
        /// 关闭系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExitSystem_Click(object sender, EventArgs e)
        {
            //如果下次不显示该提示框
            if (this.chkNoShowAgain.Checked)
                AddInUtility.IsHideSystemInNotify = false;
            //关闭系统
            this.DialogResult = DialogResult.No;
        }
        /// <summary>
        /// 下次还显示该提示框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowAgain_Click(object sender, EventArgs e)
        {
            //如果下次还显示该提示框
            if (!chkNoShowAgain.Checked)
                AddInUtility.IsHideSystemInNotify = null;
        }
        /// <summary>
        /// 退出键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormExit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }
    } 
}
