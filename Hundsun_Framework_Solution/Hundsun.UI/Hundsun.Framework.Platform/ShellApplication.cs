using System;
using System.Collections.Generic;
using System.Text;
using Hundsun.Framework.MVP;
using Hundsun.Framework.MVP.Commands;
using Hundsun.Framework.MVP.WinForms;
using Hundsun.Framework.MVP.SmartParts;
using Hundsun.Framework.MVP.EventBroker;
using Hundsun.Framework.AddIn;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Security.Permissions;
using System.Diagnostics;
using Hundsun.Framework.Entity.Utilities;
using Hundsun.Framework.HSControls;

namespace Hundsun.Framework.Platform
{
    public class ShellApplication : FormShellApplication<AppWorkItem, FormMain>
    {
        #region 应用程序入口
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        //[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public static void Main(string[] args)
        {
            argString = args;
            //foreach (string s in args)
            //{
            //    //if (s == "log")
            //    //    bLog = true;
            //    //if (s == "dos")
            //    //    bDos = true;
            //}
            //if (bDos)
            //this.SetDosMode();
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //Console.WriteLine("您好，欢迎使用恒生统一客户端开发平台");
            //Console.WriteLine("若要打开多个实例请按'M'键，单个实例请按任意键...");
            //ConsoleKeyInfo key = Console.ReadKey(false);
            //if (key.Key == ConsoleKey.M)
            //{
            //    new ShellApplication().Run();
            //}
            //else
            {
                if (SingleInstance.CreateMutex())
                {
                    new ShellApplication().Run();
                    SingleInstance.ReleaseMutex();
                }
                else
                {
                    MessageBox.Show("你已经打开了此系统, 请先关闭！", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                }
            }
        }
        /// <summary>
        /// 创建完Shell后处理
        /// </summary>
        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();
            this.RootWorkItem.Services.Add<IAddInContainer>(this.Shell);
            if (argString != null && argString.Length > 0)
            {
                this.Shell.Args = argString[0];
                this.RootWorkItem.Services.Add<string>(argString[0]);
            }
            else
            {
                this.Shell.Args = "";
            }
        }
        #endregion

        #region 系统异常处理
        /// <summary>
        /// 进程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            WriteErrorLog(e.Exception);
            ShowErrorMsg(e.Exception);    
        }
        /// <summary>
        /// 保存异常截图
        /// </summary>
        private static void Save()
        {
            ScreenUtility.Save();
        }
        /// <summary>
        /// 异常对话框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:/n/n";
            errorMsg = errorMsg + e.Message + "/n/nStack Trace:/n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        } 
        /// <summary>
        /// 当前域异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            WriteErrorLog((Exception)e.ExceptionObject);
            ShowErrorMsg((Exception)e.ExceptionObject);
        }
        static bool isPromptMsg = true;
        /// <summary>
        /// 显示异常信息
        /// </summary>
        /// <param name="ex"></param>
        private static void ShowErrorMsg(Exception ex)
        {
            if (!isPromptMsg || ex==null)
            {
                return;
            }
            string errorMsg = "";
            // 如果捕获到的异常是通信异常，弹出提示框的时候需要显示对应的功能号信息
            var t2Exception =ex as Hundsun.Framework.Communication.T2CommunicationException;
            if (t2Exception!=null)
            {
                errorMsg = string.Format("功能号:{0}, 错误号:{1}, 错误信息:{2}", t2Exception.FunctionNo, t2Exception.ErrorNo, t2Exception.ErrorInfo);
            }
            else
            {
                errorMsg = ex.Message; 
            }

            Hundsun.Framework.UIFrame.MsgBoxUtility.ShowError(errorMsg, Save); 
        }
        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="ex"></param>
        private static void WriteErrorLog(Exception ex)
        {
            if (!bLog)
                return;

            if (ex == null)
            {
                return;
            }

            string errorMsg = "";
            // 如果捕获到的异常是通信异常，弹出提示框的时候需要显示对应的功能号信息
            var t2Exception =ex as Hundsun.Framework.Communication.T2CommunicationException;
            if (t2Exception!=null)
            {
                errorMsg = string.Format("FunctionNo:{0}, ErrorNo:{1}, ErrorInfo:{2}", t2Exception.FunctionNo, t2Exception.ErrorNo, t2Exception.ErrorInfo);
            }
            else
            {
                errorMsg = Convert.ToString(ex.Message).Trim(); 
            } 

            FileStream fs = null;
            StreamWriter sw = null;
            try
            { 
                string path = Path.Combine(Application.StartupPath, @"Log");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "err.log");
                fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.UTF8);

                sw.WriteLine("Source        : " + Convert.ToString(ex.Source).Trim());
                sw.WriteLine("Method        : " + ex.TargetSite == null ? "" : ex.TargetSite.Name);
                sw.WriteLine("Date		    : " + DateTime.Now.ToShortDateString());
                sw.WriteLine("Time		    : " + DateTime.Now.ToLongTimeString());
                sw.WriteLine("Computer	    : " + Dns.GetHostName());
                sw.WriteLine("Error		    : " + errorMsg);
                sw.WriteLine("Stack Trace	: " + Convert.ToString(ex.StackTrace).Trim());
                sw.WriteLine("--------------------------------------------------------------------------------");
                sw.WriteLine();
            }
            catch (Exception x)
            {
                if (sw != null)
                {
                    sw.WriteLine(DateTime.Now.ToShortTimeString() + " " + x.Message);
                }
            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                }

                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        #endregion

        #region 控制台处理
        private static string[] argString;
        private static bool bLog = true; // 是否写日志
        //private static bool bDos = false; // 是否为主调进程分配一个新的控制台
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        private static bool SetDosMode()
        {
           return AllocConsole();
        }
        #endregion
    }
}
