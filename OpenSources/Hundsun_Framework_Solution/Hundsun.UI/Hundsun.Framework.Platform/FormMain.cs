using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.AddIn;
using Hundsun.Framework.AddIn.WinForms;
using Hundsun.Framework.IoC;
using Hundsun.Framework.HSControls;
using Hundsun.Framework.Entity;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using System.Reflection;
using Hundsun.Framework.MVP.EventBroker;
using System.Runtime.InteropServices;
using System.IO;
using Hundsun.Framework.HSControls.Skin;
using Hundsun.Framework.HSControls.Strip;

namespace Hundsun.Framework.Platform
{
    public partial class FormMain : SkinForm, IAddInContainer, IFloating
    {
        #region 构造函数
        public FormMain()
        {
            InitializeComponent();
            this.Size = new Size(800, 550);
            this.MinimumSize = this.Size;
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.language = AddInUtility.Language;
            //this.language = "en";
            if (language.Contains("en"))
                language = "en";
            if (language.Contains("zh"))
                language = "zh-CN";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            _InitializeComponent();

            this.notifyIcon.Visible = false;
            this.notifyIcon.Text = this.Text;
            this.notifyIcon.Icon = this.Icon;
            this.notifyIcon.BalloonTipTitle = Properties.Resources.TipMessage;
            this.notifyIcon.BalloonTipText = this.Text;
            this.notifyIcon.MouseClick += new MouseEventHandler(notifyIcon_MouseClick);
            this.notifyIcon.MouseDoubleClick += new MouseEventHandler(notifyIcon_MouseDoubleClick);

            #region 活动工作区
            object pk = null;
            this.addInWorkspace.SetIdle += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    pk = this.addInController.GetPKey(this.addInWorkspace);
                    this.addInController.SetUsable(false, this.addInWorkspace);
                }
                );
            this.addInWorkspace.SetUsable += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    if (this.DocumentChanged != null)
                    {
                        this.DocumentChanged(this, new Hundsun.Framework.AddIn.DataEventArgs<string>(this.ActiveDocumentName));
                    }
                    if (pk != null)
                    {
                        this.SetValue(pk);
                    }
                    this.addInController.SetUsable(true, this.addInWorkspace);
                    this.addInController.Focus(this.addInWorkspace);
                }
                );
            this.addInWorkspace.MouseDoubleClick += new MouseEventHandler(
                delegate(object sender, MouseEventArgs e)
                {
                    if (this.Status == AddInStatus.Edit)
                    {
                        this.addInController.Remove(this.addInWorkspace);
                    }
                }
                );
            #endregion

            #region 推迟显示主界面
            this.WindowState = FormWindowState.Minimized;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            BackgroundWorker background = new BackgroundWorker();
            readyTimer.Interval = 50;
            bool isStart = false;
            int itemNum = 0;
            readyTimer.Tick += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    if (!isStart)
                        background.RunWorkerAsync();
                    if (this.toolStripList.Count > 0)
                    {
                        if (itemNum != 0)
                        {
                            this.readyTimer.Stop();
                            this.ShowIcon = true;
                            this.ShowInTaskbar = true;
                            this.notifyIcon.Visible = true;
                            this.HandleMenu();
                            this.LoadDocumentPage();
                            this.LoadAddInPart();
                            this.WindowState = FormWindowState.Maximized;
                            if (AddIn.WinForms.AddInUtility.IsAddInScale)
                            {
                                AddInPanel panel = this.addInWorkspace.ActiveSmartPart as AddInPanel;
                                this.ReInitFactor(panel);
                                float fl = AddInUtility.ScaleSize;
                                if (fl > 1.0f)
                                {
                                    this.CaptionHeight = (int)(formHeight * fl);
                                    this.CaptionFont = new Font(formFont.FontFamily, (float)formFont.Size * fl, formFont.Style);
                                    this.ScaleControl(this, new SizeF(fl, fl));
                                }
                            }
                            //this.notifyIcon.ShowBalloonTip(3000);
                        }
                    }
                    isStart = true;
                }
                );
            background.DoWork += new DoWorkEventHandler(this.LoadFream);
            background.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    itemNum = this.GetMaxNum();
                }
                );
            background.WorkerReportsProgress = true;
            #endregion

            #region 定期锁屏
            lockScreen.Enabled = true;
            lockScreen.Interval = 1000;
            lockScreen.Tick += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    if (!UseLockScreen)
                        return;
                    if (GetLastInputTime() / 1000 == lockTime)
                    {
                        if (this.MainMenuStrip.Items.Count > 0)
                        {
                            // 锁屏
                            this.Lock();
                            this.addInController.WorkItem.Items.Get<IExecuter>("Client").UnLock(this.Args.ToString());
                        }
                    }
                }
                );
            #endregion
        }
        #endregion

        #region 变量
        private System.Windows.Forms.Timer readyTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer lockScreen = new System.Windows.Forms.Timer();
        private int lockTime = 10;
        private string language = "";
        private object args = null;
        private ControlTheme skin = new ControlTheme();
        List<ToolStrip> toolStripList = new List<ToolStrip>();
        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }
        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        /// <summary>
        /// 打开最后操作界面
        /// </summary>
        private delegate void LoadDocumentPageDelegate();
        /// <summary>
        /// 控制中心
        /// </summary>
        private AddInController addInController;
        [CreateNew]
        public AddInController AddInController
        {
            set
            {
                this.addInController = value;
                if (AddInController.AddInWorkItem == null)
                    AddInController.AddInWorkItem = this.addInController.WorkItem;
                if (AddInController.AddInState == null)
                    AddInController.AddInState = this.addInController.State;
                AddInController.AddInWorkItem.Services.Add<AddInController>(value);
                this.addInController.PKeyType = typeof(StockCode);
                // 初始化皮肤
                this.InitStyle();
            }
        }
        #endregion

        #region 加载菜单栏等
        /// <summary>
        /// 获取最大数
        /// </summary>
        /// <returns></returns>
        private int GetMaxNum()
        {
            List<int> num = new List<int>();
            foreach (ToolStrip toolstrip in this.toolStripList)
            {
                num.Add(toolstrip.Items.Count);
            }
            num.Sort();
            if (num.Count < 1)
                return 0;
            return num[num.Count - 1];
        }
        /// <summary>
        /// 添加菜单栏等
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFream(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            #region 加载工具条
            this.addInController.CreateToolStrip<hsToolStrip>(
                delegate()
                {
                    return VersionUtility.Deserialize<ToolStripBox>("Tools");
                }, delegate(hsToolStrip ts)
                {
                    try
                    {
                        this.toolStripContainer.TopToolStripPanel.SuspendLayout();
                        ts.GripStyle = ToolStripGripStyle.Hidden;
                        ts.RenderMode = ToolStripRenderMode.System;
                        ts.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(skin.Name + "toolbar");
                        ts.Stretch = true;
                        ts.Paint += new PaintEventHandler(ts_Paint);
                        this.toolStripContainer.TopToolStripPanel.Controls.Add(ts);
                        this.toolStripList.Add(ts);
                    }
                    finally
                    {
                        this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
                    }
                }, delegate(hsToolStrip ts)
                {
                    try
                    {
                        ts.SuspendLayout();
                        this.addInController.LoadToolStrip(
                            delegate()
                            {
                                return VersionUtility.Deserialize<ToolStripObject>((ts.Tag as ToolStripBoxItem).Key);
                            }, ts, null);
                    }
                    finally
                    {
                        ts.ResumeLayout(false);
                    }
                }, this);
            #endregion

            #region 扩展工具条
            this.addInController.CreateToolStrip<hsToolStrip>(
                delegate()
                {
                    return VersionUtility.Deserialize<ToolStripBox>("ExTools");
                }, delegate(hsToolStrip ts)
                {
                    try
                    {
                        this.toolStripContainer.ContentPanel.SuspendLayout();
                        ts.GripStyle = ToolStripGripStyle.Hidden;
                        ts.RenderMode = ToolStripRenderMode.System;
                        ts.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(skin.Name + "toolbar");
                        ts.Stretch = true;
                        ts.Paint += new PaintEventHandler(ts_Paint);
                        this.toolStripContainer.ContentPanel.Controls.Add(ts);
                        this.toolStripList.Add(ts);
                    }
                    finally
                    {
                        this.toolStripContainer.ContentPanel.ResumeLayout(true);
                    }
                }, delegate(hsToolStrip ts)
                {
                    try
                    {
                        ts.SuspendLayout();
                        this.addInController.LoadToolStrip(
                            delegate()
                            {
                                return VersionUtility.Deserialize<ToolStripObject>((ts.Tag as ToolStripBoxItem).Key);
                            }, ts, null);
                    }
                    finally
                    {
                        ts.ResumeLayout(true);
                    }
                }, this);
            #endregion

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
                        menu.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(skin.Name + "menu");
                        this.addInController.LoadToolStrip(
                           delegate()
                           {
                               return VersionUtility.Deserialize<ToolStripObject>("MenuStripObject");
                           }, menu, null);
                    }
                    finally
                    {
                        menu.ResumeLayout(false);
                    }
                }, this.MainMenuStrip, this.tsi_Click,
                  delegate(string persistString)
                  {
                      return this.addInController.LoadToolStripItem(persistString, false, false);
                  }, null);
            #endregion

            #region 加载状态栏
            this.addInController.CreateToolStrip<hsStatusStrip>(
                delegate()
                {
                    return VersionUtility.Deserialize<ToolStripBox>("Status");
                },
                delegate(hsStatusStrip ss)
                {
                    try
                    {
                        this.SuspendLayout();
                        ss.AutoSize = false;
                        ss.Height = 25;
                        ss.SizingGrip = false;
                        ss.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(skin.Name + "status");
                        ss.BackgroundImageLayout = ImageLayout.Stretch;
                        this.Controls.Add(ss);
                        this.toolStripList.Add(ss);
                    }
                    finally
                    {
                        this.ResumeLayout();
                    }
                },
                delegate(hsStatusStrip ss)
                {
                    try
                    {
                        ss.SuspendLayout();
                        this.addInController.LoadToolStrip(
                                delegate()
                                {
                                    return VersionUtility.Deserialize<ToolStripObject>((ss.Tag as ToolStripBoxItem).Key);
                                }, ss, null);
                        this.ChangeBackImage(skin.Name);
                    }
                    finally
                    {
                        ss.ResumeLayout();
                    }
                },
                this);
            #endregion
        }
        /// <summary>
        /// 去除工具栏下面白线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ts_Paint(object sender, PaintEventArgs e)
        {
            ToolStrip toolStrip = sender as ToolStrip;

            if (toolStrip.RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, toolStrip.Width, toolStrip.Height - 3);
                e.Graphics.SetClip(rect);
            }
        }
        /// <summary>
        /// 加载功能方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsi_Click(object sender, EventArgs e)
        {
            AddInArgsInfo args = (sender as ToolStripItem).Tag as AddInArgsInfo;
            if (args == null)
                return;
            if (!string.IsNullOrEmpty(args.AddInPartName) && string.IsNullOrEmpty(args.CommandID) && string.IsNullOrEmpty(args.TabIndex))
                this.AddNewPart(args.AddInPartName);
            else
                this.AddNewPart(args);
            this.Text = string.Format(Properties.Resources.CurrentPage, Properties.Resources.FormCaption, args.AddInPartName);
        }
        #endregion

        #region 主界面操作
        /// <summary>
        /// 初始化主题
        /// </summary>
        protected override void InitStyle()
        {
            skin.Initialize();
            skin = BaseTheme.Theme as ControlTheme;
            this.SetSkin(skin);
            skin.ThemeChanged += new BaseTheme.CurrentThemeChangedDele(skin_CurrentThemeChanged);
            base.InitStyle();
        }
        /// <summary>
        /// 样式变化
        /// </summary>
        /// <param name="theme"></param>
        void skin_CurrentThemeChanged(BaseTheme theme)
        {
            skin = theme as ControlTheme;
            base.InitStyle();
            this.ChangeBackImage(theme.Name);
            this.SetSkin(skin);
            if (this.addInWorkspace.SmartParts != null && this.addInWorkspace.SmartParts.Count > 0)
            {
                foreach (AddInPanel pnl in this.addInWorkspace.SmartParts)
                {
                    this.SetAddInPanelSkin(pnl);
                    if (pnl.Contents.Count > 0)
                    {
                        foreach (AddInContent content in pnl.Contents)
                        {
                            content.BackColor = theme.AddPart_BackColor;
                            ((Control)content.AddPart).Refresh();
                            if ((Control)content.AddPart is IAddInPart)
                                ((IAddInPart)(Control)content.AddPart).SetTheme(theme);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 切换菜单栏、工具栏、状态栏等背景图
        /// </summary>
        /// <param name="themeName"></param>
        void ChangeBackImage(string themeName)
        {
            this.topMenuStrip.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(themeName + "menu");
            this.topMenuStrip.ForeColor = skin.MenuStrip_Text_DefaultColor;
            this.topMenuStrip.Renderer = new MenuStripRender(skin);
            this.topMenuStrip.Font = new Font(topMenuStrip.Font.FontFamily, skin.MenuStrip_Text_FontSize, GraphicsUnit.Point);

            foreach (ToolStripItem t in this.topMenuStrip.Items)
            {
                t.ForeColor = skin.MenuStrip_Text_DefaultColor;
            }
            foreach (Control c in this.Controls)
            {
                if (c is StatusStrip)
                {
                    ((StatusStrip)c).BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(themeName + "status");
                    ((StatusStrip)c).ForeColor = skin.MenuStrip_Text_DefaultColor;
                    foreach (ToolStripItem item in ((StatusStrip)c).Items)
                    {
                        item.ForeColor = skin.MenuStrip_Text_DefaultColor;
                        if (item is ToolStripControlHost)
                        {
                            Control ctl = (item as ToolStripControlHost).Control;
                            foreach (Control ct in ctl.Controls)
                            {
                                ct.ForeColor = skin.MenuStrip_Text_DefaultColor;
                                if (ct is ToolStrip)
                                {
                                    foreach (ToolStripItem tsi in ((ToolStrip)ct).Items)
                                    {
                                        tsi.ForeColor = skin.MenuStrip_Text_DefaultColor;
                                    }
                                }
                            }
                        }
                    }
                    continue;
                }
            }
            foreach (Control c in this.toolStripContainer.TopToolStripPanel.Controls)
            {
                if (c is ToolStrip)
                {
                    ((ToolStrip)c).BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(themeName + "toolbar");
                    ((ToolStrip)c).ForeColor = skin.MenuStrip_Text_DefaultColor;
                    ToolStripRender tsRender = new ToolStripRender(skin);
                    skin.SetStripItemColors((ToolStrip)c);
                    foreach (ToolStripItem item in ((ToolStrip)c).Items)
                    {
                        item.ForeColor = skin.MenuStrip_Text_DefaultColor;
                        if (item is ToolStripControlHost)
                        {
                            Control ctl = (item as ToolStripControlHost).Control;
                            System.Reflection.PropertyInfo propertyInfo = ctl.GetType().GetProperty("Theme");
                            propertyInfo.SetValue(ctl, this.skin, null);
                            foreach (Control ct in ctl.Controls)
                            {
                                ct.ForeColor = skin.MenuStrip_Text_DefaultColor;
                                if (ct is ToolStrip)
                                {
                                    foreach (ToolStripItem tsi in ((ToolStrip)ct).Items)
                                    {
                                        tsi.ForeColor = skin.MenuStrip_Text_DefaultColor;
                                    }
                                }
                                if (ct is MenuStrip)
                                {
                                    foreach (ToolStripItem t in ((MenuStrip)ct).Items)
                                    {
                                        t.ForeColor = skin.MenuStrip_Text_DefaultColor;
                                    }
                                    // 绘制菜单
                                    ToolStripColorTable tsct = new ToolStripColorTable();
                                    tsct.Base = Color.Transparent;
                                    tsct.Fore = skin.MenuStrip_Text_DefaultColor;
                                    ((MenuStrip)ct).Renderer = new ProfessionalToolStripRendererEx(tsct);
                                }
                            }
                        }
                    }
                    continue;
                }
            }
            foreach (Control c in this.toolStripContainer.ContentPanel.Controls)
            {
                if (c is ToolStrip)
                {
                    ((ToolStrip)c).BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(themeName + "toolbar");
                    ((ToolStrip)c).ForeColor = skin.MenuStrip_Text_DefaultColor;
                    ToolStripRender tsRender = new ToolStripRender(skin);
                    skin.SetStripItemColors((ToolStrip)c);
                    foreach (ToolStripItem item in ((ToolStrip)c).Items)
                    {
                        item.ForeColor = skin.MenuStrip_Text_DefaultColor;
                        if (item is ToolStripControlHost)
                        {
                            Control ctl = (item as ToolStripControlHost).Control;
                            foreach (Control ct in ctl.Controls)
                            {
                                ct.ForeColor = skin.MenuStrip_Text_DefaultColor;
                                if (ct is ToolStrip)
                                {
                                    foreach (ToolStripItem tsi in ((ToolStrip)ct).Items)
                                    {
                                        tsi.ForeColor = skin.MenuStrip_Text_DefaultColor;
                                    }
                                }
                                if (ct is MenuStrip)
                                {
                                    foreach (ToolStripItem t in ((MenuStrip)ct).Items)
                                    {
                                        t.ForeColor = skin.MenuStrip_Text_DefaultColor;
                                    }
                                    // 绘制菜单
                                    ToolStripColorTable tsct = new ToolStripColorTable();
                                    tsct.Base = Color.Transparent;
                                    tsct.Fore = skin.MenuStrip_Text_DefaultColor;
                                    ((MenuStrip)ct).Renderer = new ProfessionalToolStripRendererEx(tsct);
                                }
                            }
                        }
                    }
                    continue;
                }
            }
        }
        /// <summary>
        /// 重新获取初始化控件坐标因子列表
        /// </summary>
        /// <param name="panel"></param>
        void ReInitFactor(AddInPanel panel)
        {
            if (panel != null)
            {
                foreach (AddInContent content in panel.Contents)
                {
                    ((UIFrame.AddInPartBase)content.AddPart).ReInitFactor((Control)content.AddPart);
                }
            }
        }
        /// <summary>
        /// 获取闲置时间
        /// </summary>
        /// <returns></returns>
        static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            if (!GetLastInputInfo(ref vLastInputInfo)) return 0;
            return Environment.TickCount - (long)vLastInputInfo.dwTime;
        }
        /// <summary>
        /// 窗体恢复显示
        /// 实现IFloating接口
        /// </summary>
        public void RestoreWindow()
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Maximized;
            if (!this.Visible)
                this.Show();
            this.Activate();
        }
        /// <summary>
        /// 关闭窗口保持工作区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Application.StartupPath, @"FunctionSchemes\" + Properties.Resources.LastDocument + ".config");
            if (this.addInWorkspace.ActiveSmartPart != null)
                ((AddInPanel)this.addInWorkspace.ActiveSmartPart).SaveAs(configFile, true);
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.addInController.Load(
                delegate()
                {
                    return VersionUtility.Deserialize<Executer>("Executer");
                }, this);
            this.readyTimer.Start();
        }
        /// <summary>
        /// 系统退出窗口关闭后
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (AddIn.WinForms.AddInUtility.IsAddInScale)
                AddIn.WinForms.AddInUtility.ScaleSize = 1.0f;
        }
        /// <summary>
        /// 系统退出提示
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            //是否隐藏到托盘还是直接退出系统。
            //默认直接退出系统
            bool isHide = false;

            //用户关闭单击关闭，或Alt+F4来关闭主窗体，则执行以下逻辑。
            if (e.CloseReason == CloseReason.UserClosing)
            {
                bool? isHideSystemInNotify = AddInUtility.IsHideSystemInNotify;
                //未配置过IsHideSystemInNotify节点
                if (!isHideSystemInNotify.HasValue)
                {
                    FormExit frmExit = new FormExit();
                    frmExit.TopMost = true;
                    //选择隐藏到托盘
                    DialogResult result = frmExit.ShowDialog();
                    frmExit = null;
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        isHide = true;
                    }
                    else if (result == System.Windows.Forms.DialogResult.No)
                    {
                        isHide = false;
                    }
                    else
                    {
                        //这种情况下，用户应该是单击了提示窗体的关闭按钮或者按了键盘的ESC键
                        //所以应该指示不关闭系统也不隐藏系统
                        e.Cancel = true;
                        return;
                    }
                }
                else//已经配置过IsHideSystemInNotify节点
                {
                    //读取节点值
                    isHide = AddInUtility.IsHideSystemInNotify.Value;
                }
            }
            else
            {
                isHide = false;//非用户关闭单击关闭，或Alt+F4来关闭主窗体
            }

            if (isHide)
            {
                this.Hide();
                this.notifyIcon.Visible = true;
                this.notifyIcon.ShowBalloonTip(2000);
                e.Cancel = true;
            }
            else//不是选择隐藏到托盘，那就直接退出
            {
                try
                {
                    this.notifyIcon.Visible = false;
                    this.notifyIcon.Dispose();
                    if (this.AppExit != null)
                        this.AppExit(this, EventArgs.Empty);
                    this.addInController.WorkItem.Terminate();
                }
                catch (Exception ex)
                {
                    //记录日志
                    throw ex;
                }
                finally
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
        }
        /// <summary>
        /// 系统退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsmiExit_Click(object sender, EventArgs e)
        {
            this.CloseApplication();
        }
        /// <summary>
        /// 打开主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Maximized;
            if (!this.Visible)
            {
                this.Show();
            }
            this.Activate();
        }
        /// <summary>
        /// 双击图标打开主界面最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Maximized;
                if (!this.Visible)
                {
                    this.Show();
                }

                this.Activate();
            }
        }
        /// <summary>
        /// 打击打开主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!this.Visible)
                {
                    this.Show();
                }
                this.Activate();
            }
        }
        #endregion

        #region IAddInContainer接口实现
        public event EventHandler AppExit;
        public event EventHandler<Hundsun.Framework.AddIn.DataEventArgs<string>> DocumentChanged;
        /// <summary>
        /// 退出应用程序
        /// </summary>
        public void CloseApplication()
        {
            //强制关闭应用程序
            OnFormClosing(new FormClosingEventArgs(CloseReason.ApplicationExitCall, false));
        }
        /// <summary>
        /// 切换皮肤
        /// </summary>
        private void SetTheme()
        {
            if (this.addInWorkspace.ActiveSmartPart != null)
            {
                foreach (AddInPanel pnl in this.addInWorkspace.SmartParts)
                {
                    if (pnl.Contents.Count > 0)
                    {
                        foreach (AddInContent content in pnl.Contents)
                        {
                            ((Control)content.AddPart).Refresh();
                            if ((Control)content.AddPart is IAddInPart)
                                ((IAddInPart)(Control)content.AddPart).SetTheme(skin);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 当前活动文档区
        /// </summary>
        public string ActiveDocumentName
        {
            get
            {
                return this.addInController.GetActiveDocumentName(this.addInWorkspace);
            }
        }
        /// <summary>
        /// 当前插件名
        /// </summary>
        public string ActiveAddPartName
        {
            get
            {
                return this.addInController.GetActiveAddPartName(this.addInWorkspace);
            }
        }
        /// <summary>
        /// 上一页
        /// </summary>
        public void GoBack()
        {
            this.addInController.GoBack(this.addInWorkspace);
        }
        /// <summary>
        /// 下一页
        /// </summary>
        public void NextContent()
        {
            this.addInController.NextContent(this.addInWorkspace);
        }
        /// <summary>
        /// 新添文档区
        /// </summary>
        public void NewDocument()
        {
            this.addInController.NewDocument(this.addInWorkspace);
            this.SetTheme();
        }
        /// <summary>
        /// 新添空白文档区
        /// </summary>
        public string NewBlackDocument(string name)
        {
            string ret = this.addInController.NewBlackDocument(this.addInWorkspace, name);
            this.SetTheme();
            return ret;
        }
        /// <summary>
        /// 新添文档区
        /// </summary>
        public void NewDocument(string group)
        {
            this.addInController.NewDocument(this.addInWorkspace, group);
            this.SetTheme();
        }
        /// <summary>
        /// 保存方案文档
        /// </summary>
        public void SaveDocument()
        {
            this.addInController.SaveDocument(this.addInWorkspace);
        }
        /// <summary>
        /// 保存方案文档
        /// </summary>
        public void SaveDocument(string folder)
        {
            this.addInController.SaveDocument(this.addInWorkspace, folder);
        }
        /// <summary>
        /// 导入文档区
        /// </summary>
        /// <returns></returns>
        public string LoadDocument()
        {
            try
            {
                string doc = this.addInController.LoadDocument(this.addInWorkspace);
                this.SetTheme();
                return doc;
            }
            finally
            {
                this.Focus();
            }
        }
        /// <summary>
        /// 导入文档区
        /// </summary>
        /// <returns></returns>
        public string LoadDocumentByFolder(string folder)
        {
            try
            {
                string doc = this.addInController.LoadDocumentByFolder(this.addInWorkspace, folder);
                this.SetTheme();
                return doc;
            }
            finally
            {
                this.Focus();
            }
        }
        /// <summary>
        /// 导入文档区
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public string LoadDocument(string functionId)
        {
            try
            {
                string doc = this.addInController.LoadDocument(this.addInWorkspace, functionId);
                this.SetTheme();
                return doc;
            }
            finally
            {
                this.Focus();
            }
        }
        /// <summary>
        /// 导入文档区
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public string LoadDocument(List<object> args)
        {
            try
            {
                string doc = this.addInController.LoadDocument(this.addInWorkspace, args);
                this.SetTheme();
                return doc;
            }
            finally
            {
                this.Focus();
            }
        }
        /// <summary>
        /// 导入文档区
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public string LoadDocument(AddInArgsInfo args)
        {
            try
            {
                string doc = this.addInController.LoadDocument(this.addInWorkspace, args);
                this.SetTheme();
                return doc;
            }
            finally
            {
                this.Focus();
            }
        }
        /// <summary>
        /// 关闭文档区
        /// </summary>
        public void CloseDocument()
        {
            this.addInController.RemoveAll(this.addInWorkspace);
        }
        /// <summary>
        /// 关闭当前文档区
        /// </summary>
        public void CloseActiveDocument()
        {
            this.addInController.Remove(this.addInWorkspace);
        }
        /// <summary>
        /// 关闭当前插件
        /// </summary>
        public void CloseActiveAddInPart()
        {
            this.addInController.CloseActiveAddInPart(this.addInWorkspace);
        }
        /// <summary>
        /// 新添插件
        /// </summary>
        public void AddNewPart()
        {
            this.addInController.AddNewPart(this.addInWorkspace);
            this.SetTheme();
        }
        /// <summary>
        /// 新添插件
        /// </summary>
        public void AddNewPartByGroup(string group)
        {
            this.addInController.AddNewPartByGroup(this.addInWorkspace, group);
            this.SetTheme();
        }
        /// <summary>
        /// 新添插件
        /// </summary>
        public void AddNewPart(string functionId)
        {
            this.addInController.AddNewPart(this.addInWorkspace, functionId);
            this.SetTheme();
        }
        /// <summary>
        /// 新添插件
        /// </summary>
        public void AddNewPart(List<object> args)
        {
            this.addInController.AddNewPart(this.addInWorkspace, args);
            this.SetTheme();
        }
        /// <summary>
        /// 新添插件
        /// </summary>
        public void AddNewPart(List<object> args, Hundsun.Framework.AddIn.WinForms.Docking.DockState state)
        {
            this.addInController.AddNewPart(this.addInWorkspace, args, state);
            this.SetTheme();
        }
        /// <summary>
        /// 新添插件
        /// </summary>
        public void AddNewPart(AddInArgsInfo args)
        {
            this.addInController.AddNewPart(this.addInWorkspace, args);
            this.SetTheme();
        }
        /// <summary>
        /// 新添插件
        /// </summary>
        public void AddNewPart(AddInArgsInfo args, Hundsun.Framework.AddIn.WinForms.Docking.DockState state, bool isAuto)
        {
            this.addInController.AddNewPart(this.addInWorkspace, args, state, isAuto);
            this.SetTheme();
        }
        /// <summary>
        /// 新添插件
        /// </summary>
        public void AddNewPart(AddInArgsInfo args, Hundsun.Framework.AddIn.WinForms.Docking.DockState state, bool isAuto, bool isOpenSame)
        {
            this.addInController.AddNewPart(this.addInWorkspace, args, state, isAuto, isOpenSame);
            this.SetTheme();
        }
        /// <summary>
        /// 建立联动
        /// </summary>
        public void BuildLinkageHandler(bool isAuto)
        {
            this.addInController.BuildLinkageHandler(this.addInWorkspace, isAuto);
        }
        /// <summary>
        /// 注销原有联动关系
        /// </summary>
        public void DestroyAllLinkageHandler()
        {
            this.addInController.DestroyAllLinkageHandler(this.addInWorkspace);
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(object value)
        {
            this.addInController.SetValue(value, this.addInWorkspace);
        }
        /// <summary>
        /// 设置皮肤颜色
        /// </summary>
        /// <param name="value"></param>
        public void SetSkin(object value)
        {
            this.addInController.SetSkin(value);
        }
        /// <summary>
        /// 设置AddInPanel皮肤
        /// </summary>
        /// <param name="panel">AddInPanel</param>
        public void SetAddInPanelSkin(object panel)
        {
            this.addInController.SetAddInPanelSkin(panel);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public new void Refresh()
        {
            this.addInController.ForEach(this.addInWorkspace,
                delegate(object obj)
                {
                    if (obj != null)
                    {
                        AddInUtility.SetLanguageResources((Control)obj);
                        ((Control)obj).Refresh();
                        ((Control)obj).Parent.Refresh();
                    }
                }
            );

            this.toolStripList.Clear();

            int len = this.MainMenuStrip.Items.Count;
            for (int i = 0; i < len; i++)
            {
                //if (this.MainMenuStrip.Items[i] is ToolStripLabel)
                //    continue;
                this.MainMenuStrip.Items.RemoveAt(i);
                len -= 1;
                i--;
            }

            this.toolStripContainer.TopToolStripPanel.Controls.Clear();
            foreach (Control c in this.toolStripContainer.ContentPanel.Controls)
            {
                if (c is ToolStrip)
                {
                    this.toolStripContainer.ContentPanel.Controls.Remove(c);
                    continue;
                }
            }

            foreach (Control c in this.Controls)
            {
                if (c is StatusStrip)
                {
                    this.Controls.Remove(c);
                    continue;
                }
            }

            new Thread(
                delegate()
                {
                    this.LoadFream(null, null);
                }).Start();
            if ((AddInPanel)this.addInWorkspace.ActiveSmartPart == null)
            {
                this.Text = Properties.Resources.FormCaption;
            }
            else
            {
                if (((AddInPanel)this.addInWorkspace.ActiveSmartPart).ActiveContent != null)
                    this.Text = string.Format(Properties.Resources.CurrentPage, Properties.Resources.FormCaption,
                        ((AddInPanel)this.addInWorkspace.ActiveSmartPart).ActiveContent.DockHandler.TabText);
                else
                    this.Text = Properties.Resources.FormCaption;
            }
            this.Activate();
        }
        /// <summary>
        /// 关键key
        /// </summary>
        public object PK
        {
            get
            {
                return this.addInController.GetPKey(this.addInWorkspace);
            }
        }
        /// <summary>
        /// 语言名称
        /// </summary>
        public object Language
        {
            get
            {
                return this.language;
            }
            set
            {
                this.language = (string)value;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.language);
                ProductInfo.PRODUCT_NAME = Properties.Resources.FormCaption;
                if (this.language.Contains("en"))
                {
                    AddInUtility.Language = "en";
                }
                else if (this.language.Contains("zh"))
                {
                    AddInUtility.Language = "zh-CN";
                }
                this.Refresh();
            }
        }
        /// <summary>
        /// 参数
        /// </summary>
        public object Args
        {
            get
            {
                return this.args;
            }
            set
            {
                this.args = value;
            }
        }
        /// <summary>
        /// 设置快捷键
        /// </summary>
        /// <param name="item"></param>
        /// <param name="clear"></param>
        protected void SetShortcutKeys(ToolStripItem item, bool clear = true)
        {
            if (!(item is ToolStripMenuItem))
            {
                return;
            }
            if (clear)
            {
                (item as ToolStripMenuItem).ShortcutKeys = Keys.None;
            }
            else
            {
                ToolStripMenuItem tsi = (item as ToolStripMenuItem);
                if (tsi.Tag == null || tsi.ShortcutKeyDisplayString == null || tsi.ShortcutKeyDisplayString == "")
                    return;
                Keys mKey = Keys.None;
                string[] keys = tsi.ShortcutKeyDisplayString.Split('+');
                for (int i = 0; i < keys.Length; i++)
                {
                    object obj = Enum.Parse(typeof(Keys), keys[i], true);
                    if (obj != null)
                    {
                        if (i == 0)
                        {
                            mKey = (Keys)obj;
                        }
                        else
                        {
                            mKey = (Keys)(mKey | (Keys)obj);
                        }
                    }
                }
                tsi.ShortcutKeys = mKey;
            }
        }
        /// <summary>
        /// 锁屏
        /// </summary>
        public void Lock()
        {
            foreach (Control c in this.Controls)
            {
                if (c is StatusStrip)
                {
                    ((StatusStrip)c).Enabled = false;
                    continue;
                }
                if (c is MenuStrip)
                {
                    MenuStrip ms = (MenuStrip)c;
                    //ms.Enabled = false;
                    for (int i = 0; i < ms.Items.Count; i++)
                    {
                        if (ms.Items[i] is ToolStripLabel)
                            continue;
                        ToolStripDropDownItem item = (ToolStripDropDownItem)ms.Items[i];
                        item.Enabled = false;
                        SetShortcutKeys(item);
                        if (item.Text == Properties.Resources.Settings)
                        {
                            item.Enabled = true;
                            foreach (ToolStripItem tsi in item.DropDownItems)
                            {
                                tsi.Enabled = false;
                                if (tsi.Text == Properties.Resources.UnLock)
                                {
                                    tsi.Enabled = true;
                                }
                            }
                        }
                        foreach (ToolStripItem tsi in item.DropDownItems)
                        {
                            SetShortcutKeys(tsi);
                        }
                    }
                    continue;
                }
            }
            //this.toolStripContainer.Enabled = false;
            foreach (Control c in this.toolStripContainer.TopToolStripPanel.Controls)
            {
                if (c is ToolStrip)
                {
                    foreach (ToolStripItem item in ((ToolStrip)c).Items)
                    {
                        if (item is ToolStripControlHost)
                        {
                            Control ctl = (item as ToolStripControlHost).Control;
                            foreach (Control ct in ctl.Controls)
                            {
                                if (ct is Label)
                                {
                                    ct.Enabled = true;
                                    continue;
                                }
                                if (ct is MenuStrip)
                                {
                                    MenuStrip ms = (MenuStrip)ct;
                                    for (int i = 0; i < ms.Items.Count; i++)
                                    {
                                        ToolStripDropDownItem tsd = (ToolStripDropDownItem)ms.Items[i];
                                        tsd.Enabled = false;
                                        SetShortcutKeys(tsd);
                                        if (tsd.Text == Properties.Resources.Settings)
                                        {
                                            tsd.Enabled = true;
                                            foreach (ToolStripItem tsi in tsd.DropDownItems)
                                            {
                                                tsi.Enabled = false;
                                                if (tsi.Text == Properties.Resources.UnLock)
                                                {
                                                    tsi.Enabled = true;
                                                }
                                            }
                                        }
                                        foreach (ToolStripItem tsi in tsd.DropDownItems)
                                        {
                                            SetShortcutKeys(tsi);
                                        }
                                    }
                                    continue;
                                }
                                if (ct is ContextMenuStrip)
                                {
                                    ContextMenuStrip ms = (ContextMenuStrip)ct;
                                    for (int i = 0; i < ms.Items.Count; i++)
                                    {
                                        ToolStripDropDownItem tsd = (ToolStripDropDownItem)ms.Items[i];
                                        tsd.Enabled = false;
                                        SetShortcutKeys(tsd);
                                        if (tsd.Text == Properties.Resources.Settings)
                                        {
                                            tsd.Enabled = true;
                                            foreach (ToolStripItem tsi in tsd.DropDownItems)
                                            {
                                                tsi.Enabled = false;
                                                if (tsi.Text == Properties.Resources.UnLock)
                                                {
                                                    tsi.Enabled = true;
                                                }
                                            }
                                        }
                                        foreach (ToolStripItem tsi in tsd.DropDownItems)
                                        {
                                            SetShortcutKeys(tsi);
                                        }
                                    }
                                    continue;
                                }
                                ct.Enabled = false;
                            }
                        }
                        else
                        {
                            item.Enabled = false;
                        }
                    }
                }
            }
            this.addInWorkspace.Enabled = false;
        }
        /// <summary>
        /// 解锁
        /// </summary>
        public void UnLock()
        {
            foreach (Control c in this.Controls)
            {
                if (c is StatusStrip)
                {
                    ((StatusStrip)c).Enabled = true;
                    continue;
                }
                if (c is MenuStrip)
                {
                    MenuStrip ms = (MenuStrip)c;
                    for (int i = 0; i < ms.Items.Count; i++)
                    {
                        if (ms.Items[i] is ToolStripLabel)
                            continue;
                        ToolStripDropDownItem item = (ToolStripDropDownItem)ms.Items[i];
                        item.Enabled = true;
                        SetShortcutKeys(item, false);
                        foreach (ToolStripItem tsi in item.DropDownItems)
                        {
                            tsi.Enabled = true;
                            if (tsi.Text == Properties.Resources.UnLock)
                            {
                                tsi.Enabled = false;
                            }
                        }
                        foreach (ToolStripItem tsi in item.DropDownItems)
                        {
                            SetShortcutKeys(tsi, false);
                        }
                    }
                    continue;
                }
            }
            //this.toolStripContainer.Enabled = true;
            foreach (Control c in this.toolStripContainer.TopToolStripPanel.Controls)
            {
                if (c is ToolStrip)
                {
                    foreach (ToolStripItem item in ((ToolStrip)c).Items)
                    {
                        if (item is ToolStripControlHost)
                        {
                            Control ctl = (item as ToolStripControlHost).Control;
                            foreach (Control ct in ctl.Controls)
                            {
                                if (ct is MenuStrip)
                                {
                                    MenuStrip ms = (MenuStrip)ct;
                                    for (int i = 0; i < ms.Items.Count; i++)
                                    {
                                        ToolStripDropDownItem tsd = (ToolStripDropDownItem)ms.Items[i];
                                        tsd.Enabled = true;
                                        SetShortcutKeys(tsd, false);
                                        if (tsd.Text == Properties.Resources.Settings)
                                        {
                                            foreach (ToolStripItem tsi in tsd.DropDownItems)
                                            {
                                                tsi.Enabled = true;
                                                if (tsi.Text == Properties.Resources.UnLock)
                                                {
                                                    tsi.Enabled = false;
                                                }
                                            }
                                        }
                                        foreach (ToolStripItem tsi in tsd.DropDownItems)
                                        {
                                            SetShortcutKeys(tsi, false);
                                        }
                                    }
                                    continue;
                                }
                                if (ct is ContextMenuStrip)
                                {
                                    ContextMenuStrip ms = (ContextMenuStrip)ct;
                                    for (int i = 0; i < ms.Items.Count; i++)
                                    {
                                        ToolStripDropDownItem tsd = (ToolStripDropDownItem)ms.Items[i];
                                        tsd.Enabled = true;
                                        SetShortcutKeys(tsd, false);
                                        if (tsd.Text == Properties.Resources.Settings)
                                        {
                                            foreach (ToolStripItem tsi in tsd.DropDownItems)
                                            {
                                                tsi.Enabled = true;
                                                if (tsi.Text == Properties.Resources.UnLock)
                                                {
                                                    tsi.Enabled = false;
                                                }
                                            }
                                        }
                                        foreach (ToolStripItem tsi in tsd.DropDownItems)
                                        {
                                            SetShortcutKeys(tsi, false);
                                        }
                                    }
                                    continue;
                                }
                                ct.Enabled = true;
                            }
                        }
                        else
                        {
                            item.Enabled = true;
                        }
                    }
                }
            }
            this.addInWorkspace.Enabled = true;
        }
        /// <summary>
        /// 关键字
        /// </summary>
        public object UsableKey
        {
            get
            {
                return this.addInController.GetUsableKey(this.addInWorkspace);
            }
        }
        /// <summary>
        /// 编辑状态
        /// </summary>
        public AddInStatus Status
        {
            get
            {
                return AddInUtility.Status;
            }
            set
            {
                AddInUtility.Status = value;
            }
        }
        /// <summary>
        /// 应答
        /// </summary>
        /// <param name="keyValue"></param>
        public void AnswerKey(int keyValue)
        {
            this.MainMenuStrip.Focus();
            if (keyValue >= 96 && keyValue <= 105)
            {
                keyValue -= 48;
            }
            if ((keyValue >= 48 && keyValue <= 57) ||
                (keyValue >= 65 && keyValue <= 90) ||
                keyValue == 27)
            {
                this.OnKeyPress(new KeyPressEventArgs((char)keyValue));
            }
            else
            {
                this.OnKeyDown(new KeyEventArgs((Keys)keyValue));
            }
        }
        #endregion
    }
}
