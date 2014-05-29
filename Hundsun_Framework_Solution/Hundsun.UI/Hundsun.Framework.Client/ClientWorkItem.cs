using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Diagnostics;
using System.Xml;
using System.Reflection;
using Hundsun.Framework.AddIn;
using Hundsun.Framework.MVP;
using Hundsun.Framework.MVP.SmartParts;
using Hundsun.Framework.IoC;
using Hundsun.Framework.Entity.Interface;
using Hundsun.Framework.Communication;
using Hundsun.Framework.Entity;
using Hundsun.Framework.MVP.EventBroker;
using Hundsun.Framework.AddIn.WinForms;
using Hundsun.Framework.HSControls;
using Hundsun.Framework.Entity.Utilities;

namespace Hundsun.Framework.Client
{
    public class ClientWorkItem : WorkItem, IExecuter, IBusiness
    {
        #region IExecuter接口
        private ISynchronizeInvoke synInvoker = null;
        private FormLogin fLogin = null;
        /// <summary>
        /// 启动WorkItem
        /// </summary>
        /// <param name="invoker"></param>
        public void Start(ISynchronizeInvoke invoker)
        {
            if (invoker != null)
                this.synInvoker = invoker;
            this.Parent.Services.Add(typeof(IBusiness), this);
            this.Parent.Services.Add(typeof(IExecuter), this);
            this.Parent.Services.Add<ICommunication>(new Communication());
            this.Parent.Terminated += new EventHandler(Parent_Terminated);
            //if (this.Parent.Services.Get<string>() != null)
            //    this.Login(this.Parent.Services.Get<string>());
            //else
            this.Login("");
        }
        /// <summary>
        /// WorkItem终结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Parent_Terminated(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="args"></param>
        public void UnLock(string args)
        {
            if (args == "")
            {
                if (fLogin != null)
                {
                    fLogin.ShowIcon = false;
                    fLogin.ShowInTaskbar = false;
                    if (fLogin.ShowDialog().Equals(DialogResult.OK))
                    {
                        //解除锁屏
                        this.RootWorkItem.Services.Get<IAddInContainer>().UnLock();
                        this.Parent.Items.Remove(fLogin);
                        fLogin = null;
                        return;
                    }
                }
                string id = "flogin";
                fLogin = this.Parent.Items.AddNew<FormLogin>(id);
                fLogin.ShowIcon = false;
                fLogin.ShowInTaskbar = false;
                if (fLogin.ShowDialog().Equals(DialogResult.OK))
                {
                    //解除锁屏
                    this.RootWorkItem.Services.Get<IAddInContainer>().UnLock();
                    this.Parent.Items.Remove(fLogin);
                    fLogin = null;
                }
            }
        }
        #endregion

        #region IBusiness接口
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="item"></param>
        public void Add(object item)
        {
            this.Parent.Items.Add(item);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="item"></param>
        public void Remove(object item)
        {
            this.Parent.Items.Remove(item);
        }
        /// <summary>
        /// 获取用户文件夹
        /// </summary>
        /// <returns></returns>
        public string GetUserFolder()
        {
            return "";
        }
        /// <summary>
        /// 自动更新
        /// </summary>
        private void AutoUpdate()
        {
        }
        /// <summary>
        /// 通信设置
        /// </summary>
        public void CommunicationSetting()
        {
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="args"></param>
        public void Login(string args)
        {
            if (args == "")
            {
                string id = "login";
                FormLogin login = this.Parent.Items.Get<FormLogin>(id);
                if (login != null)
                    this.Parent.Items.Remove(login);
                login = this.Parent.Items.AddNew<FormLogin>(id);
                if (login.ShowDialog() != DialogResult.OK)
                {
                    //解除锁屏
                    this.RootWorkItem.Services.Get<IAddInContainer>().UnLock();
                    return;
                }
                else
                {
                    //登录成功后，将消息展示器窗体加入服务容器中，供其他地方DependencyAttribute方法引用
                    this.Parent.Services.Add<IMessageDisplayer>(new FormMsgDisplayer()); 
                }
            }
        }
        #endregion

        #region 自动更新
        /// <summary>
        /// 自动更新
        /// </summary>
        private void AutoUpgrade()
        {
            string curDir = Application.StartupPath;
            string autoUpgradeAppName = curDir + @"\Hundsun.Framework.AutoUpgrade.exe";
            ProcessStartInfo autoUpgradeStartInfo = new ProcessStartInfo(autoUpgradeAppName);

            //自动升级需要传入用户信息，根据用户信息下载相应更新文件,传入的用户信息为Xml字符串流    
            string userInfo = string.Empty;
            XmlDocument argsXml = new XmlDocument();
            try
            {
                XmlDeclaration xmlDecl = argsXml.CreateXmlDeclaration("1.0", "utf-8", null);
                argsXml.AppendChild(xmlDecl);
                XmlNode node = argsXml.CreateElement("Upgrade");
                XmlAttribute attr = argsXml.CreateAttribute("info");
                attr.Value = "userinfo";
                //node.InnerText = "userinfo";
                node.Attributes.Append(attr);
                argsXml.AppendChild(node);

                XmlNode subNode = argsXml.CreateElement("userID");
                subNode.InnerText = "007";
                node.AppendChild(subNode);
                subNode = argsXml.CreateElement("password");
                subNode.InnerText = "123456";
                node.AppendChild(subNode);
            }
            catch
            {
                return;
            }

            // 将Xml字符串流中的双引号替换成单引号，
            // *必须，否则自动更新程序将无法解析*
            string innerXml = argsXml.InnerXml;
            userInfo = StringReplace(innerXml, "\"", "\'");

            // 参数串为 "0" 或者 "Upgrade" 均表示系统更新
            autoUpgradeStartInfo.Arguments = "0 " + userInfo;
            autoUpgradeStartInfo.WorkingDirectory = curDir;
            Process proc = null;
            try
            {
                proc = Process.Start(autoUpgradeStartInfo);
            }
            catch (System.ComponentModel.Win32Exception exp)
            {
                string errInfo = exp.ToString();
                return;
            }
            proc.WaitForExit();
            int exitCode = proc.ExitCode;
            /*
             * 返回码为0表示初始化成功，此时需要重启程序
             * 因初始化数据暂时存在目标目录的temp临时文件夹中
             * 重启将拷贝临时文件到目标文件，同时删除临时文件
             * 所以，务必传入：源路径、目标路径，重启程序全路径
             */
            if (exitCode == 0)
            {
                if (MessageBox.Show("更新完成, 系统将重启", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    == System.Windows.Forms.DialogResult.OK)
                {
                    string curApplication = Application.ExecutablePath;
                    string sourcePath = curDir + @"\temp";
                    string destPath = curDir;
                    // 第一参数串为 "1" 或者 "ReStart" 均表示重启程序
                    string args = "ReStart " + sourcePath + " " + destPath + " " + curApplication;
                    autoUpgradeStartInfo.Arguments = args;
                    Process.Start(autoUpgradeStartInfo);
                    Application.ExitThread(); //释放所有线程
                    Environment.Exit(0);
                }
            }
        }
        /// <summary>     
        /// 替换字符串中的某一子串
        /// </summary>     
        /// <param name="str">待处理的字符串</param>     
        /// <param name="toRep">要替换的字符串中的子串</param>     
        /// <param name="strRep">用来替换toRep字符串的字符串</param>     
        /// <returns>返回一个结果字符串</returns>     
        private string StringReplace(string str, string oldValue, string newValue)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            int startIndex = 0, endIndex = 0;

            for (; ; )
            {
                string str_tmp = str.Substring(startIndex);
                endIndex = str_tmp.IndexOf(oldValue);

                if (endIndex == -1)
                {
                    sb.Append(str_tmp);
                    break;
                }
                else
                {
                    sb.Append(str_tmp.Substring(0, endIndex)).Append(newValue);
                    startIndex += endIndex + oldValue.Length;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 系统初始化
        /// </summary>
        private void InitSystem()
        {
            string curDir = Application.StartupPath;
            string autoUpgradeAppName = curDir + @"\Hundsun.Framework.AutoUpgrade.exe";
            ProcessStartInfo autoUpgradeStartInfo = new ProcessStartInfo(autoUpgradeAppName);

            // 参数串为 "2" 或者 "InitSystem" 均表示初始化系统
            autoUpgradeStartInfo.Arguments = "2";
            autoUpgradeStartInfo.WorkingDirectory = curDir;
            Process proc = null;
            try
            {
                proc = Process.Start(autoUpgradeStartInfo);
            }
            catch (System.ComponentModel.Win32Exception exp)
            {
                string errInfo = exp.ToString();
                return;
            }
            proc.WaitForExit();
            int exitCode = proc.ExitCode;
            /*
             * 返回码为0表示初始化成功，此时需要重启程序
             * 因初始化数据暂时存在目标目录的temp临时文件夹中
             * 重启将拷贝临时文件到目标文件，同时删除临时文件
             * 所以，务必传入：源路径、目标路径，重启程序全路径
             */
            if (exitCode == 0)
            {
                if (MessageBox.Show("初始化完成, 系统将重启", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    == System.Windows.Forms.DialogResult.OK)
                {
                    string curApplication = Application.ExecutablePath;
                    string sourcePath = curDir + @"\temp";
                    string destPath = curDir;
                    // 第一参数串为 "1" 或者 "ReStart" 均表示重启程序
                    //string args = "ReStart " + sourcePath + " " + destPath + " " + curApplication;
                    string args = "1 " + sourcePath + " " + destPath + " " + curApplication;
                    autoUpgradeStartInfo.Arguments = args;
                    Process.Start(autoUpgradeStartInfo);
                    Application.ExitThread();   //释放所有线程
                    Environment.Exit(0);
                }
            }
        }
        #endregion
    }
}