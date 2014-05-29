using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Reflection;
using Hundsun.Framework.AddIns;
using Hundsun.Framework.MVP.SmartParts;
using Hundsun.Framework.Communication;
using Hundsun.Framework.IoC;
using System.Threading;
using Hundsun.Framework.AddIn;
using System.Runtime.Caching;
using System.Globalization;
using Hundsun.Framework.Entity;
using System.Diagnostics;
using System.Xml;
using Hundsun.Framework.HSControls;

namespace Hundsun.Framework.Client
{
    [SmartPart]
    public partial class FormLogin : Form
    {
        #region 构造函数
        public FormLogin()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(form_MouseDown);
            this.BackgroundImage = VersionUtility.GetObject<Image>("login");
            this.Icon = VersionUtility.GetObject<Icon>("logo");
            this.lblLogin.Image = Properties.Resources.leave;
            this.lblCancel.Image = Properties.Resources.leave;
            //初始化密码键盘窗体
            this.keyBoardForm = new KeyBoardPopupForm(this.txtPwd);
            lblKeyBoard.Visible = true;
        }
        #endregion
        
        #region 变量
        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(HandleRef hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private ICommunication communication;
        [Dependency]
        public ICommunication Communication
        {
            set { this.communication = value; }
        }

        private IAddInContainer addInContainer;
        [Dependency]
        public IAddInContainer AddInContainer
        {
            set { this.addInContainer = value; }
        }

        private LoginController loginController;
        [CreateNew]
        public LoginController LoginController
        {
            set { this.loginController = value; }
        }
        #endregion

        #region 绘制窗体
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x0112, 0xF010 + 0x0002, 0x000B);
            }
        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush lgb;
            lgb = new LinearGradientBrush(rect, Color.LightGray, Color.Silver, 90);
            g.FillRectangle(lgb, new Rectangle(0, 0, this.Width, this.Height));
            g.DrawRectangle(new Pen(Brushes.White), new Rectangle(11, 79, 435, 172));
            lgb.Dispose();
        }
        /// <summary>
        /// 画圆角
        /// </summary>
        private void SetWindowRegion()
        {
            GraphicsPath formPath;
            formPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            formPath = GetRoundedRectPath(rect, 17);
            this.Region = new Region(formPath);
        }
        /// <summary>
        /// 画圆角
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            path.AddArc(arcRect, 180, 90);          // 左上角 
            arcRect.X = rect.Right - diameter;      // 右上角   
            path.AddArc(arcRect, 270, 90);
            arcRect.Y = rect.Bottom - diameter;     // 右下角 
            path.AddArc(arcRect, 0, 90);
            arcRect.X = rect.Left;                  // 左下角 
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }
        /// <summary>
        /// 画圆角
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(System.EventArgs e)
        {
            this.Region = null;
        }
        /// <summary>
        /// 获取圆角点.
        /// 返回一个scale大小的PointF数组,数组的点从startPoint向endPoint靠近.
        /// </summary>
        /// <param name="startPoint">开始点(每个角顺时针的第一个点)</param>
        /// <param name="endPoint">结束点(每个角顺时针的最后一个点)</param>
        /// <param name="scale">取值范围:[2,90];说明:圆角精度调整,值越大,精度越好</param>
        /// <param name="style">指示获取哪个方向的圆角点.0:左上角,1:右上角,2:右下角,3:左下角</param>
        /// <returns>返回一个scale大小的PointF数组,数组的点从startPoint向endPoint靠近.</returns>
        private PointF[] GetBorderCornerPointf(PointF startPoint, PointF endPoint, int scale, int style)
        {
            float cornerSize = startPoint.X - endPoint.X;
            if (Math.Abs(cornerSize) != Math.Abs(startPoint.Y - endPoint.Y)
                || cornerSize == 0)
            {
                throw new Exception("开始点与结束点必须构成一个正方形对角点");
            }
            if (scale < 2 || scale > 90)
            {
                throw new Exception("参数scale值范围为[2,90]");
            }
            PointF zPoint;
            double pi = 3.1415926F;
            double temp = (double)90.0f / (double)scale; ;
            PointF[] resultPoint = new PointF[scale];
            switch (style)
            {
                case 0:
                    //左上角
                    if (cornerSize > 0)
                    {
                        throw new Exception("对于左上角,开始点X值必须小于结束点X值");
                    }
                    cornerSize = -cornerSize;
                    zPoint = new PointF(startPoint.X + cornerSize, startPoint.Y);

                    for (int i = 0; i < scale; i++)
                    {
                        float x = (float)(cornerSize * Math.Cos(i * temp * pi / 180));
                        float y = (float)(cornerSize * Math.Sin(i * temp * pi / 180));
                        resultPoint[i] = new PointF(zPoint.X - x, zPoint.Y - y);
                    }
                    return resultPoint;
                case 1:
                    //右上角
                    if (cornerSize > 0)
                    {
                        throw new Exception("对于右上角,开始点X值必须小于结束点X值");
                    }
                    cornerSize = -cornerSize;
                    zPoint = new PointF(startPoint.X, startPoint.Y + cornerSize);

                    for (int i = 0; i < scale; i++)
                    {
                        float x = (float)(cornerSize * Math.Cos(i * temp * pi / 180));
                        float y = (float)(cornerSize * Math.Sin(i * temp * pi / 180));
                        resultPoint[scale - i - 1] = new PointF(zPoint.X + x, zPoint.Y - y);
                    }
                    return resultPoint;
                case 2:
                    //右下角
                    if (cornerSize < 0)
                    {
                        throw new Exception("对于右下角,开始点X值必须大于结束点X值");
                    }
                    zPoint = new PointF(startPoint.X - cornerSize, startPoint.Y);
                    for (int i = 0; i < scale; i++)
                    {
                        float x = (float)(cornerSize * Math.Cos(i * temp * pi / 180));
                        float y = (float)(cornerSize * Math.Sin(i * temp * pi / 180));
                        resultPoint[i] = new PointF(zPoint.X + x, zPoint.Y + y);
                    }
                    return resultPoint;
                case 3:
                    //左下角
                    if (cornerSize < 0)
                    {
                        throw new Exception("对于左下角,开始点X值必须大于结束点X值");
                    }
                    zPoint = new PointF(startPoint.X, startPoint.Y - cornerSize);
                    for (int i = 0; i < scale; i++)
                    {
                        float x = (float)(cornerSize * Math.Cos(i * temp * pi / 180));
                        float y = (float)(cornerSize * Math.Sin(i * temp * pi / 180));
                        resultPoint[scale - i - 1] = new PointF(zPoint.X - x, zPoint.Y + y);
                    }
                    return resultPoint;
                default:
                    {
                        throw new Exception("style值必须为0,1,2,3(分别对应左上角,右上角,右下角,左下角)之中的一个");
                    }
            }
        }
        /// <summary>
        /// 设置窗体路径
        /// </summary>
        private void SetFormRegion(int scale)
        {
            int leftTop = 8;
            int rightTop = 10;
            int rightButtom = 11;
            int leftButtom = 10;

            //对于矩形的窗体，要在一个角上画个弧度至少需要2个点，所以4个角需要至少8个点
            PointF p1 = new PointF(leftTop, 0);
            PointF p2 = new PointF(this.Width - rightTop, 0);
            PointF p3 = new PointF(this.Width, rightTop);
            PointF p4 = new PointF(this.Width, this.Height - rightButtom);
            PointF p5 = new PointF(this.Width - rightButtom, this.Height);
            PointF p6 = new PointF(leftButtom, this.Height);
            PointF p7 = new PointF(0, this.Height - leftButtom);
            Point p8 = new Point(0, leftTop);

            System.Drawing.Drawing2D.GraphicsPath shape = new System.Drawing.Drawing2D.GraphicsPath();

            PointF[] temp0 = GetBorderCornerPointf(p8, p1, scale, 0);
            PointF[] temp1 = GetBorderCornerPointf(p2, p3, scale, 1);
            PointF[] temp2 = GetBorderCornerPointf(p4, p5, scale, 2);
            PointF[] temp3 = GetBorderCornerPointf(p6, p7, scale, 3);

            shape.AddLines(new PointF[2] { p7, p8 });
            shape.AddLines(temp0);

            shape.AddLines(new PointF[2] { p1, p2 });
            shape.AddLines(temp1);

            shape.AddLines(new PointF[2] { p3, p4 });
            shape.AddLines(temp2);

            shape.AddLines(new PointF[2] { p5, p6 });
            shape.AddLines(temp3);

            Graphics g = this.CreateGraphics();
            g.DrawPath(new Pen(Color.Black, 2), shape);
            //将窗体的显示区域设为GraphicsPath的实例
            this.Region = new System.Drawing.Region(shape);
        }
        #endregion
     
        #region 操作
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                errorProvider.SetError(txtUserID, Properties.Resources.UserID);
                txtUserID.Focus();
                txtUserID.SelectAll();
                return;
            }
            if (string.IsNullOrEmpty(txtPwd.Text))
            {
                errorProvider.SetError(txtPwd, Properties.Resources.UserPwd);
                txtPwd.Focus();
                txtPwd.SelectAll();
                return;
            }
            HsIdentity hsIdentity = null;
            //这里是演示
            hsIdentity = new HsIdentity();
            hsIdentity.OperCode = "admin";
            hsIdentity.OperPwd = txtPwd.Text;
            hsIdentity.OperName = "Demo 用户";
            hsIdentity.OperId = 1;
            hsIdentity.CmpId = 1;
            hsIdentity.SubSystemID = 1;
            hsIdentity.DeptID = 1;
            //List<MenuInfo> menus = null;
            //try
            //{
            //    hsIdentity = loginController.Login(txtUserID.Text, txtPwd.Text);
            //    menus = loginController.GetUserAllMenus(o4Identity);
            //    loginController.SaveMenuToFile(menus);
            //}
            //catch (T2Exception ex)
            //{
            //    MessageBox.Show("登录失败," + ex.ToShortString());
            //}
            //if (o4Identity == null)
            //{
            //    return;
            //}
            //if (menus == null)
            //{
            //    return;
            //}
            this.communication.DBFactory.Identity = hsIdentity;
            this.DialogResult = DialogResult.OK;
            this.InitH5();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.StartThread();

            // 是否开放登录按钮
            //this.LoginEnabled(false);
        }

        public delegate void AddMessageCallBack(string message);
        private void AddMessage(string message)
        {
            this.lblMessage.Text = message;
        }

        public delegate void LoginEnabledCallBack(bool enable);
        private void LoginEnabled(bool enable)
        {
            if (this.lblLogin.InvokeRequired)
                this.lblLogin.Invoke(new LoginEnabledCallBack(LoginEnabled), enable);
            else
                this.lblLogin.Enabled = enable;
        }

        private void LoginEnabled()
        {
            this.lblLogin.Enabled = true;
        }

        public delegate void ProgressBarCallBack();
        private void ShowProgressBar()
        {
            //progressBar.Value += progressBar.Step;
            int value = progressBar.Value + progressBar.Step;
            //progressBar.Value = value <= 100 ? value : progressBar.Step;
        }

        private void PerformStep()
        {
            if (this.progressBar.InvokeRequired)
                this.progressBar.Invoke(new ProgressBarCallBack(ShowProgressBar));
            else
            {
                //progressBar.Value += progressBar.Step;
                ShowProgressBar();
            }
        }

        private void FinishProgressBar()
        {
            //progressBar.Value = progressBar.Maximum;
        }

        private void FinishStep()
        {
            if (this.progressBar.InvokeRequired)
                this.progressBar.Invoke(new ProgressBarCallBack(FinishProgressBar));
            else
            {
                FinishProgressBar();
            }
        }

        private void ShowMsg(string msg)
        {
            if (this.lblMessage.InvokeRequired)
                this.lblMessage.Invoke(new AddMessageCallBack(AddMessage), msg);
            else
                lblMessage.Text = msg;
        }
  
        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void txt2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }

        private void txt1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{Tab}");
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            bool ret = this.communication.DBFactory.GetT2Data().SetT2Connection();
            int status = this.communication.DBFactory.GetT2Data().UpdateStatus;
            //if (ret)
            //{
            //    this.progressBar.Value = 0;
            //    this.progressBar.Step = 50;
            //    //this.ThreadT2Connect(true);
            //    this.T2ReConnect();
            //}
            switch (status)
            {
                case 0:
                    break;
                case 1: //T2修改
                    this.progressBar.Value = 0;
                    this.progressBar.Step = 50;
                    this.T2ReConnect();
                    break;
                case 2: //H5修改
                    this.progressBar.Value = 0;
                    this.progressBar.Step = 50;
                    ThreadH5Connect(true);
                    break;
                case 3: //T2、H5同时修改
                    this.progressBar.Value = 0;
                    this.progressBar.Step = 25;
                    ThreadH5Connect(false);
                    break;
            }
        }

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                Application.Exit();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
        #endregion

        #region 效果
        private void lblCancel_MouseHover(object sender, EventArgs e)
        {
            lblCancel.Image = Properties.Resources.enter;
            lblCancel.ForeColor = ColorTranslator.FromHtml("#a76001");
        }
        private void lblCancel_MouseLeave(object sender, EventArgs e)
        {
            lblCancel.Image = Properties.Resources.leave;
            lblCancel.ForeColor = ColorTranslator.FromHtml("#6e4001");
        }
        private void lblLogin_MouseHover(object sender, EventArgs e)
        {
            lblLogin.Image = Properties.Resources.enter;
            lblLogin.ForeColor = ColorTranslator.FromHtml("#a76001");
        }
        private void lblLogin_MouseLeave(object sender, EventArgs e)
        {
            lblLogin.Image = Properties.Resources.leave;
            lblLogin.ForeColor = ColorTranslator.FromHtml("#6e4001");
        }
        #endregion

        #region 键盘标签的鼠标操作
        /// <summary>
        /// 光标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblKeyBoard_MouseLeave(object sender, EventArgs e)
        {
            this.lblKeyBoard.BorderStyle = BorderStyle.None;
        }
        /// <summary>
        /// 光标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblKeyBoard_MouseEnter(object sender, EventArgs e)
        {
            this.lblKeyBoard.BorderStyle = BorderStyle.FixedSingle;
        }
        /// <summary>
        /// 弹出服务
        /// </summary>
        IPopupService popupService = new WinPopupService();
        /// <summary>
        /// 键盘弹出窗体
        /// </summary>
        KeyBoardPopupForm keyBoardForm;
        /// <summary>
        /// 打开键盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblKeyBoard_Click(object sender, EventArgs e)
        {
            this.txtPwd.Focus();
            if (keyBoardForm.Visible)
            {
                //隐藏
                keyBoardForm.Visible = false; 
            }
            else
            {
                //显示密码键盘窗体
                Point p =this.Location;
                p.Offset(this.pnlMain.Location);
                p.Offset(this.txtPwd.Location);
                p.Offset(0,this.txtPwd.Height+1); 
                keyBoardForm.Location = p;
                popupService.SetVisibleCore(keyBoardForm, true);
            }
        }
        #endregion  
    }
}