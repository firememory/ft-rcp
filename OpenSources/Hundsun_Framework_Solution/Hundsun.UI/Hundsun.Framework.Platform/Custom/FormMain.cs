using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hundsun.Framework.Entity;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using Hundsun.Framework.AddIn.WinForms;
using Hundsun.Framework.MVP.WinForms;

namespace Hundsun.Framework.Platform
{
    public partial class FormMain
    {
        /// <summary>
        /// 构造函数调用用户自定义函数
        /// </summary>
        private void _InitializeComponent()
        {
            //设置顶部菜单是否可见
            this.topMenuStrip.Visible = false;
            //设置主窗体标题
            this.Text = Properties.Resources.FormCaption;
            ProductInfo.PRODUCT_NAME = Properties.Resources.FormCaption;
            this.Icon = VersionUtility.GetPicture<Icon>("logo.ico");
            this.KeyPreview = true;
            formHeight = this.CaptionHeight;
            formFont = this.CaptionFont;
        }

        #region 主界面缩放
        private int formHeight = 0;
        private Font formFont;
        private int menuHeight = 23;
        private int toolbarHeight = 25;
        private int statusHeight = 27;
        /// <summary>
        /// 主界面缩放
        /// </summary>
        /// <param name="control"></param>
        /// <param name="size"></param>
        private void ScaleControl(Control control, SizeF size)
        {
            if (control.Controls.Count > 0)
            {
                foreach (Control ctl in control.Controls)
                {
                    if (ctl is DeckWorkspace || ctl is TabWorkspace)
                        continue;
                    if (ctl is ToolStripContainer)
                    {
                        ((ToolStripContainer)ctl).TopToolStripPanel.Height = (int)(toolbarHeight * size.Width);
                    }
                    if (ctl is MenuStrip)
                    {
                        ctl.Font = new Font(formFont.FontFamily, (float)formFont.Size * size.Width, formFont.Style);
                        ctl.Height = (int)(menuHeight * size.Width);
                    }
                    else if (ctl is StatusStrip)
                    {
                        ctl.Font = new Font(formFont.FontFamily, (float)formFont.Size * size.Width, formFont.Style);
                        ctl.Height = (int)(statusHeight * size.Width);
                        foreach (ToolStripItem item in ((ToolStrip)ctl).Items)
                        {
                            if (item is ToolStripControlHost)
                            {
                                Control c = ((ToolStripControlHost)item).Control;
                                if (c.Controls.Count > 0)
                                {
                                    foreach (Control cl in c.Controls)
                                    {
                                        cl.Height = (int)(statusHeight * size.Width);
                                        cl.Font = new Font(formFont.FontFamily, (float)formFont.Size * size.Width, formFont.Style);
                                    }
                                }
                            }
                        }
                    }
                    else if (ctl is ToolStrip)
                    {
                        ctl.Font = new Font(formFont.FontFamily, (float)formFont.Size * size.Width, formFont.Style);
                        ctl.Height = (int)(toolbarHeight * size.Width);
                        foreach (ToolStripItem item in ((ToolStrip)ctl).Items)
                        {
                            if (item is ToolStripControlHost)
                            {
                                Control c = ((ToolStripControlHost)item).Control;
                                c.Height = (int)(toolbarHeight * size.Width);
                                if (c.Controls.Count > 0)
                                {
                                    foreach (Control cl in c.Controls)
                                    {
                                        cl.Height = (int)(toolbarHeight * size.Width);
                                        cl.Font = new Font(formFont.FontFamily, (float)formFont.Size * size.Width, formFont.Style);
                                        if (cl is ToolStrip)
                                        {
                                            foreach (ToolStripItem tsi in ((ToolStrip)cl).Items)
                                            {
                                                tsi.Font = new Font(formFont.FontFamily, (float)formFont.Size * size.Width, formFont.Style);
                                                if (tsi is ToolStripSplitButton)
                                                {
                                                    ToolStripSplitButton tssb = tsi as ToolStripSplitButton;
                                                    foreach (ToolStripItem i in tssb.DropDownItems)
                                                    {
                                                        i.Font = new Font(formFont.FontFamily, (float)formFont.Size * size.Width, formFont.Style);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (ctl.Controls.Count > 0)
                        ScaleControl(ctl, size);
                }
            }
        }
        #endregion

        /// <summary>
        /// 指定工作区打开相关插件
        /// </summary>
        private void LoadAddInPart()
        {
        }
        /// <summary>
        /// 打开历史工作区
        /// </summary>
        private void LoadDocumentPage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            string configFile = Path.Combine(Application.StartupPath, @"FunctionSchemes\" + Properties.Resources.LastDocument + ".config");
            if (this.addInWorkspace.InvokeRequired)
                this.addInWorkspace.Invoke(new LoadDocumentPageDelegate(this.LoadDocumentPage));
            else
            {
                if (File.Exists(configFile))
                    this.addInController.LoadDocument(this.addInWorkspace, Properties.Resources.LastDocument);
                else
                    this.addInController.LoadDocument(this.addInWorkspace, "HomePage");
            }
            this.SetTheme();
        }
        /// <summary>
        /// 处理主菜单
        /// </summary>
        private void HandleMenu()
        {
        }
        /// <summary>
        /// 是否使用定时锁屏功能
        /// </summary>
        private bool UseLockScreen = false;
        protected override void WndProc(ref Message m)
        {
            if (!this.CanResize)
            {
                if (m.Msg == 0x00A1 || m.Msg == 0x00A2 || m.Msg == 0x00A3)
                {
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
