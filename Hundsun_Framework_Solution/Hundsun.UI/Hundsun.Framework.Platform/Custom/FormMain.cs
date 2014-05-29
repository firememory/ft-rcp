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
            this.Icon = VersionUtility.GetObject<Icon>("logo");
            this.KeyPreview = true;
        }
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
            string configFile = Path.Combine(Environment.CurrentDirectory, @"FunctionSchemes\" + Properties.Resources.LastDocument + ".config");
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
