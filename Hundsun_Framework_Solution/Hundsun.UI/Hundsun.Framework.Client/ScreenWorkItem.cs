using System;
using System.Collections.Generic;
using System.Text;
using Hundsun.Framework.AddIn;
using Hundsun.Framework.MVP;
using System.ComponentModel;
using Hundsun.Framework.Communication;
using System.Windows.Forms;
using Hundsun.Framework.IoC;
using Hundsun.Framework.AddIn.WinForms;

namespace Hundsun.Framework.Client
{
    public class ScreenWorkItem : WorkItem, IExecuter
    {
        private AddInController addInController;
        [Dependency]
        public AddInController AddInController
        {
            set
            {
                this.addInController = value;
            }
        }
        private IAddInContainer addInContainer;
        private FormScreen screen = null;
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="invoker"></param>
        public void Start(ISynchronizeInvoke invoker)
        {
            if (invoker is Control || invoker is IAddInContainer)
            {
                addInContainer = invoker as IAddInContainer;
                Form frm = invoker as Form;

                frm.KeyDown += new KeyEventHandler(delegate(object sender, KeyEventArgs e)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.F8:
                            if (screen == null)
                            {
                                screen = new FormScreen(addInController);
                                this.FormStartScreen(0, screen);
                                screen.Show();
                            }
                            break;
                    }
                }
                );
            }
        }
        /// <summary>
        /// 设置在第几个屏幕上启动
        /// </summary>
        /// <param name="screen">屏幕(从0开始)</param>
        /// <param name="form">要启动的程序。</param>
        private void FormStartScreen(int screen, Form form)
        {
            if (Screen.AllScreens.Length <= 1)
                return;
            if (Screen.AllScreens.Length < screen)
                return;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new System.Drawing.Point(Screen.AllScreens[screen].Bounds.X, Screen.AllScreens[screen].Bounds.Y);
            form.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="args"></param>
        public void UnLock(string args)
        { }
    }
}
