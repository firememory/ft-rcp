using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Hundsun.Framework.Client.Properties;
using Hundsun.Framework.MVP.SmartParts;
using Hundsun.Framework.Communication;
using Hundsun.Framework.IoC;
using Hundsun.Framework.AddIn;

namespace Hundsun.Framework.Client
{
    [SmartPart]
    public partial class FormPassword : Form
    {
        #region 构造函数
        public FormPassword(ICommunication communication)
        {
            InitializeComponent();
            this.communication = communication;
            this.txtOld.Focus();
        }

        private ICommunication communication;
        private Point mouseOffset;        //记录鼠标指针的坐标
        private bool isMouseDown = false; //记录鼠标按键是否按下
        #endregion

        #region 画圆角
        /// <summary>
        /// 画圆角
        /// </summary>
        public void SetWindowRegion()
        {
            GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);//this.Left-10,this.Top-10,this.Width-10,this.Height-10);                 
            FormPath = GetRoundedRectPath(rect, 15);
            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);      // 左上角 
            arcRect.X = rect.Right - diameter;  // 右上角   
            path.AddArc(arcRect, 270, 90);
            arcRect.Y = rect.Bottom - diameter; // 右下角 
            path.AddArc(arcRect, 0, 90);
            arcRect.X = rect.Left;              // 左下角 
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }
        protected override void OnResize(System.EventArgs e)
        {
            this.Region = null;
            SetWindowRegion();
        }
        #endregion

        #region 操作
        private void txtOld_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                txtNew.Focus();
            }
        }
        private void txtNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                txtRepeat.Focus();
            }
        }
        private void txtRepeat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnOK.PerformClick();
            }
        }
        private void btn_MouseEnter(object sender, EventArgs e)
        {
            if (((Button)sender).Name == "btnCancel")
                ((Button)sender).BackgroundImage = Resources.updateCancel2;
            else
                ((Button)sender).BackgroundImage = Resources.updateOK2;
        }
        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (((Button)sender).Name == "btnCancel")
                ((Button)sender).BackgroundImage = Resources.updateCancel2;
            else
                ((Button)sender).BackgroundImage = Resources.updateOK2;
        }
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            if (((Button)sender).Name == "btnCancel")
                ((Button)sender).BackgroundImage = Resources.updateCancel1;
            else
                ((Button)sender).BackgroundImage = Resources.updateOK1;
        }
        #endregion

        #region 鼠标
        /// <summary>
        /// 确定修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOld.Text))
            {
                errorProvider.SetError(txtOld, Properties.Resources.txtOld);
                txtNew.SelectAll();
                txtNew.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtNew.Text))
            {
                errorProvider.SetError(txtNew, Properties.Resources.txtNew);
                txtNew.SelectAll();
                txtNew.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRepeat.Text))
            {
                errorProvider.SetError(txtRepeat, Properties.Resources.txtRepeat);
                txtRepeat.SelectAll();
                txtRepeat.Focus();
                return;
            }
            if (txtNew.Text != txtRepeat.Text)
            {
                errorProvider.SetError(txtRepeat, Properties.Resources.txtSame);
                txtRepeat.SelectAll();
                txtRepeat.Focus();
                return;
            }
        }
        private void FormPassword_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;
            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight -
                 SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }
        private void FormPassword_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                this.Location = mousePos;
            }
        }
        private void FormPassword_MouseUp(object sender, MouseEventArgs e)
        {
            // 修改鼠标状态isMouseDown的值
            // 确保只有鼠标左键按下并移动时，才移动窗体
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }
        #endregion
    }
}
