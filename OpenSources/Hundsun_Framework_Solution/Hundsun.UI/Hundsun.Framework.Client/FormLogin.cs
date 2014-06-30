using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using Hundsun.Framework.Communication;
using System.Globalization;
using System.IO;
using System.Data.SQLite;
using Hundsun.Framework.AddIn.WinForms;
using System.Diagnostics;
using System.Reflection;

namespace Hundsun.Framework.Client
{
    public partial class FormLogin
    {
        #region 通信初始化
        /// <summary>
        /// 退出操作  
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (this.DialogResult == DialogResult.OK)
                return;
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        string t2configFile = Application.StartupPath + "\\t2sdk.ini";
        /// <summary>
        /// 初始化H5数据
        /// </summary>
        private void InitH5()
        {
            if (!AddInUtility.H5)
                return;
            new Thread(delegate()
            {
                try
                {
                    List<int> marketTypes = new List<int>();
                    XmlDocument xmlDocument = new XmlDocument();
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreComments = true;
                    XmlReader xmlReader = XmlReader.Create("h5sdk.xml", settings);
                    xmlDocument.Load(xmlReader);
                    foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
                    {
                        foreach (XmlAttribute xmlAttribute in xmlNode.Attributes)
                        {
                            if (xmlAttribute.Name == "mkttype")
                                marketTypes.Add(Int32.Parse(xmlAttribute.Value.Substring(2, xmlAttribute.Value.Length - 2), NumberStyles.HexNumber));
                        }
                    }
                    IH5Adapter h5Adapter = this.communication.DBFactory.GetH5Data();
                    if (null == h5Adapter)
                        return;
                    else
                    {
                        ISQLiteDataHandler sqlDataHandler = new SQLiteDataHandler(Path.Combine(Application.StartupPath, "Quotation.db3"), "12345678");
                        string cmdText = "Delete From SecurityCollection";
                        sqlDataHandler.ExecuteNonQuery(cmdText);
                        SQLiteParameter[] parameters = new SQLiteParameter[0];

                        List<string> batchSqls = new List<string>();
                        foreach (int marketType in marketTypes)
                        {
                            int errno = 0;
                            IMarketOverview mktOverview = h5Adapter.GetSupportMarketInfo(marketType, 2000, ref errno, false);
                            int totalCount = mktOverview.TotalCount;
                            for (int stockIndex = 0; stockIndex < totalCount; stockIndex++)
                            {
                                IStockInitInfo stockInitInfo = mktOverview.GetStockInitInfo(stockIndex);
                                string insertSql = "Insert Into SecurityCollection Values( '" + stockInitInfo.StockCode + "', '" +
                                    stockInitInfo.StockName + "', " +
                                    Convert.ToString(stockInitInfo.MarketType) + ", " +
                                    Convert.ToString(stockInitInfo.CodeType) + ", " +
                                    Convert.ToString(stockInitInfo.StockStatus) + " )";
                                batchSqls.Add(insertSql);
                            }
                        }

                        sqlDataHandler.ExecuteBatchNonQuery(batchSqls);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }).Start();
        }
        /// <summary>
        /// 启动连接T2线程
        /// </summary>
        private void StartThread()
        {
            new Thread(
                delegate()
                {
                    AddIn.Utilities.IO.LoadAddInObjects();
                    PerformStep();
                }).Start();

            LoginEnabled(true);
            ThreadStart tsH5 = new ThreadStart(ThreadH5Connect);
            Thread tH5 = new Thread(tsH5);
            tH5.Start();
        }
        /// <summary>
        /// H5线程
        /// </summary>
        private void ThreadH5Connect()
        {
            this.ThreadH5Connect(false);
        }
        /// <summary>
        /// H5线程
        /// </summary>
        /// <param name="reconnect"></param>
        private void ThreadH5Connect(bool reconnect)
        {
            if (AddInUtility.H5)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.addInContainer.Language.ToString());
                ShowMsg(Properties.Resources.InitH5Message);
                
                try
                {
                    IH5Adapter h5 = this.communication.DBFactory.GetH5Data();
                    PerformStep();
                }
                catch (H5Exception exp)
                {
                    string noteInfo = Properties.Resources.InitH5Fail + "\r\n(" + exp.ToString() + ")";
                    ShowMsg(noteInfo);
                    return;
                }

                ShowMsg(Properties.Resources.InitH5OK);

                Thread.Sleep(1000);
                PerformStep();
            }

            if (!reconnect) //第一次
            {
                ThreadStart ts = new ThreadStart(ThreadT2Connect);
                Thread t = new Thread(ts);
                t.Start();
            }
            else
            {
                FinishStep();
            }
        }
        /// <summary>
        /// T2线程
        /// </summary>
        private void ThreadT2Connect()
        {
            this.ThreadT2Connect(false);
        }
        /// <summary>
        /// 启动T2重连线程
        /// </summary>
        private void T2ReConnect()
        {
            ThreadStart ts = new ThreadStart(StartT2ReConnect);
            Thread t = new Thread(ts);
            t.Start();
        }
        /// <summary>
        /// T2重连
        /// </summary>
        private void StartT2ReConnect()
        {
            this.ThreadT2Connect(true);
        }
        /// <summary>
        /// T2线程
        /// </summary>
        private void ThreadT2Connect(bool reconnect)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.addInContainer.Language.ToString());
            ShowMsg(Properties.Resources.InitMessage);
            try
            {
                PerformStep();
                IT2DataHandler t2 = this.communication.DBFactory.GetT2Data();
                if (!reconnect)
                {
                    // 未连接则建立连接
                    if (!t2.Connected)
                    {
                        t2.InitT2Connect(t2configFile, 5000);
                    }
                }
                else
                {
                    t2.ReConnect(4000);
                }
                Thread.Sleep(1000);
            }
            catch (T2CommunicationException exp)
            {
                string noteInfo = Properties.Resources.InitFail + "\r\n(" + exp.ToShortString() + ")";
                ShowMsg(noteInfo);
                return;
            }
            ShowMsg(Properties.Resources.InitOK);
            Thread.Sleep(1000);
            ShowMsg(Properties.Resources.InitEnd);

            // 是否启动更新
            if(AddIn.WinForms.AddInUtility.IsStartAutoUpdate)
                this.StartAutoUpdate();
            //PerformStep();
            this.FinishStep();
        }
        #endregion

        #region 自动更新
        /*
         * 注意：
         * 以下代码仅供参考，自动更新开启逻辑（如：是否开启，检测周期等）需要使用者自行决定；
         * 右下角更新提示窗标题使用者可自行给出~
         * 相应的国际化信息也得使用者自行给出。。。
         */
        // 模块信息，记录日志用
        private string module = "Hundsun.Framework.Client";
        /// <summary>
        /// 启动自动更新线程
        /// 在检测到存在更新文件时，弹出更新窗体
        /// </summary>
        private void StartAutoUpdate()
        {
            // 强制更新登陆按钮置灰
            if (AddInUtility.IsForceUpdate)
                this.EnableLogin(false);
            else
                this.EnableLogin(true); 

            string location = "FormLogin.StartAutoUpdate";
            string logInfo = String.Format("初始化自动更新环境...");
            HSLog.LogUpgrade.UpgradeLog(HSLogLevel.INFO, location, module, logInfo);

            IT2DataHandler t2 = this.communication.DBFactory.GetT2Data();
            try
            {
                // 初始化文件更新器
                t2.InitFileUpdater(10000);
                t2.FileUpdater.OnReceiveFileList += 
                    new OnRecvFileListEventHandler(FileUpdater_OnReceiveFileList);
            }
            catch (T2FileUpdateException e)
            {
                // TODO:
                // 登陆按钮激活
                this.EnableLogin(true);

                logInfo = String.Format("初始化自动更新环境失败！\r\n({0})", e.ToShortString());
                HSLog.LogUpgrade.UpgradeLog(HSLogLevel.WARN, location, module, logInfo);
                Hundsun.Framework.UIFrame.MsgBoxUtility.ShowError(logInfo);
            }
        }
        /// <summary>
        /// 收到更新，启动更新线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="filelist"></param>
        private void FileUpdater_OnReceiveFileList(object sender, List<T2UpdateFileInfo> filelist)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, List<T2UpdateFileInfo>>(this.FileUpdater_OnReceiveFileList),
                    new object[] { sender, filelist });
                return;
            }

            string location = "FormLogin.FileUpdater_OnReceiveFileList";
            string logInfo = String.Empty;

            // 无可用更新，直接返回
            if (filelist == null || filelist.Count == 0)
            {
                // TODO:
                // 登陆按钮激活
                if (AddInUtility.IsForceUpdate)
                    this.EnableLogin(true);
                return;
            }

            // 计算文件大小和最后更新时间
            /*
             * 如：8MB, 2014-04-23
             */
            T2FileUpdater updater = sender as T2FileUpdater;
            string size = updater.GetTotalSize(filelist);
            string time = updater.GetFileTime(filelist);

            // 发现更新，启动更新线程
            logInfo = String.Format("启动自动更新线程...");
            HSLog.LogUpgrade.UpgradeLog(HSLogLevel.INFO, location, module, logInfo);

            // 非强制更新则临时线程启动
            //if (!this.forceUpdate)
            //{
            //    Thread t = new Thread(new ThreadStart(delegate()
            //    {
            //        // 启动更新
            //        StartUpdate(size, time);
            //    }));
            //    t.Priority = ThreadPriority.Normal;
            //    t.Start();
            //}
            //else
            //{
            //    // 登陆按钮置灰
            //    this.EnableLogin(false);
            //    // 启动更新
            //    StartUpdate(size, time);
            //}

            // 启动更新
            Thread t = new Thread(new ThreadStart(delegate()
            {
                StartUpdate(size, time);
            }));
            t.Priority = ThreadPriority.Normal;
            t.Start();
        }
        /// <summary>
        /// 当前语言环境
        /// </summary>
        string language = Thread.CurrentThread.CurrentUICulture.Name;
        /// <summary>
        /// 升级弹窗
        /// </summary>
        Form notice = null;
        /// <summary>
        /// 启动更新程序
        /// </summary>
        private void StartUpdate(string size, string time)
        {
            // 设置语言环境
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            string location = "FormLogin.StartUpdate";
            string logInfo = String.Format("弹窗询问是否升级...");
            HSLog.LogUpgrade.UpgradeLog(HSLogLevel.INFO, location, module, logInfo);

            // 弹窗询问是否升级
            //NoticeForm nf = new NoticeForm(Int32.MaxValue);
            //nf.PopShow(23, 0, true);
            // 使用反射启动升级气泡弹窗
            if (notice == null || notice.IsDisposed)
            {
                Assembly assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + @"/Hundsun.Framework.AutoUpgrade.exe");
                notice = (Form)assembly.CreateInstance("Hundsun.Framework.AutoUpgrade" + ".NoticeForm", true, 
                    BindingFlags.Default, null,
                    new object[] { Int32.MaxValue, AddInUtility.IsForceUpdate }, Thread.CurrentThread.CurrentCulture, null);
            }
            // 设置弹窗标题
            /*
             * 可在此处设置个性化的提示窗口标题
             * 提示窗口默认显示为"HSRCF 自动升级提示"
             */
            //notice.Text = "HSRCF 自动升级提示";

            // 设置弹窗文本
            MethodInfo method = notice.GetType().GetMethod("SetMessage", BindingFlags.Public | BindingFlags.Instance, null, 
                new Type[] { typeof(string), typeof(string) }, null);
            method.Invoke(notice, new object[] { size, time });
          
            // 显示弹窗
            method = notice.GetType().GetMethod("PopShow", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(uint), typeof(bool) }, null);
            method.Invoke(notice, new object[] { (uint)23, (uint)0, true });

            // 取消更新
            if (notice.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                logInfo = String.Format("用户取消更新！");
                HSLog.LogUpgrade.UpgradeLog(HSLogLevel.INFO, location, module, logInfo);
                return;
            }

            // 用户选择更新，启动自动更新程序
            logInfo = String.Format("用户选择更新！");
            HSLog.LogUpgrade.UpgradeLog(HSLogLevel.INFO, location, module, logInfo);

            string curDir = Application.StartupPath;
            if (!curDir.EndsWith("\\"))
                curDir += "\\";
            // 自动更新程序名，自动更新程序需和主程序目录一致
            string updateFilename = "Hundsun.Framework.AutoUpgrade.exe";
            string updateFullFilename = curDir + updateFilename;

            // 参数准备
            /* 
             * 参数1：T2配置文件
             * 参数2：待重启应用程序全路径名
             * 参数3：语言环境，en/zh-CN
             */
            string configFile = t2configFile;
            string curApplication = Application.ExecutablePath;
            // 2014-4-24 12:04:23，使用全局语言
            //string language = Thread.CurrentThread.CurrentUICulture.Name;

            // 用问号替换空格
            /* 
             * 注意：
             * 为防止全路径含空格，传参时用了"?"替换，待启动进程获取参数后做逆替换...
             */
            configFile = configFile.Replace(" ", "?");
            curApplication = curApplication.Replace(" ", "?");
            string arguments = String.Format("{0} {1} {2}", configFile, curApplication, language);

            // 启动自动更新程序，更新系统
            ProcessStartInfo procStartInfo = new ProcessStartInfo(updateFullFilename);
            procStartInfo.Arguments = arguments;
            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            try
            {
                // 启动失败
                if (!proc.Start())
                {
                    // todo:
                    logInfo = String.Format("启动自动更新程序{0}失败！", updateFilename);
                    HSLog.LogUpgrade.UpgradeLog(HSLogLevel.ERROR, location, module, logInfo);
                    Hundsun.Framework.UIFrame.MsgBoxUtility.ShowError(logInfo);
                }
            }
            catch (Exception e)
            {
                // todo:
                logInfo = String.Format("启动自动更新程序{0}失败！\r\n(Catched Exception: {1})", 
                    updateFilename, e.ToString());
                HSLog.LogUpgrade.UpgradeLog(HSLogLevel.ERROR, location, module, logInfo);
                Hundsun.Framework.UIFrame.MsgBoxUtility.ShowError(logInfo);
            }
        }
        /// <summary>
        /// 激活登陆按钮
        /// </summary>
        /// <param name="enable">是否激活</param>
        private void EnableLogin(bool enable)
        {
            if (this.lblLogin.InvokeRequired)
                this.lblLogin.Invoke(new Action<bool>(EnableLogin), enable);
            else
                this.lblLogin.Enabled = enable;
        }
        #endregion
    }
}
