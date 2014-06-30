using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.HSControls;
using Hundsun.Framework.AddIn;
using Hundsun.Framework.HSControls.Scroll;
using Hundsun.Framework.AddIn.WinForms;
using Hundsun.Framework.IoC;

namespace Hundsun.Framework.Client
{
    /// <summary>
    /// 消息滚动展示窗体【为Demo作用，请用户自行扩展相关业务逻辑】
    /// </summary>
    public partial class FormMsgDisplayer : FormFloating, IMessageDisplayer
    {
        #region 初始化
        private void HsFormFloating_Load(object sender, EventArgs e)
        { 
            //设置窗体弹出后的位置
            int y = 50;
            int x = 0;
            if (this.addInContiner != null && this.addInContiner is Form)
            {
                x = Screen.GetWorkingArea(this.addInContiner as Form).Width - this.Width - 70; 
            }
            else
            {
                x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 70; 
            }
            this.Location = new Point(x, y); 
        } 
        #endregion 
        
        #region 上下文菜单弹出时
        /// <summary>
        /// 菜单弹出的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            itemViewAllMsg.Enabled = this.scrollingText.ItemCount > 0;
            itemMarkAllMsgRead.Enabled = this.scrollingText.ItemCount > 0;
            itemViewMsg.Enabled = !string.IsNullOrWhiteSpace(this.scrollingText.ID);
            itemMarkMsgRead.Enabled = !string.IsNullOrWhiteSpace(this.scrollingText.ID);

            //标记速度菜单项选中状态
            itemSpeedSlow.Checked = this.scrollingText.TextScrollSpeed == ScrollSpeed.Low;
            itemSpeedNormal.Checked = this.scrollingText.TextScrollSpeed == ScrollSpeed.Normal;
            itemSpeedFast.Checked = this.scrollingText.TextScrollSpeed == ScrollSpeed.High;
        }
        #endregion

        #region 消息处理事件(扩展点)
        /// <summary>
        /// 查看选中消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemViewMsg_Click(object sender, EventArgs e)
        {
            this.scrollingText.RemoveScrollElement(this.scrollingText.ID);
        }
        /// <summary>
        /// 查看所有消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemViewAllMsg_Click(object sender, EventArgs e)
        {
            this.scrollingText.RemoveAllScrollElement();
        }
        /// <summary>
        /// 标记选中消息为已读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemMarkMsgRead_Click(object sender, EventArgs e)
        { 
            this.scrollingText.RemoveScrollElement(this.scrollingText.ID);
        }
        /// <summary>
        /// 标记所有消息为已读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemMarkAllMsgRead_Click(object sender, EventArgs e)
        {
            this.scrollingText.RemoveAllScrollElement();
        }
        /// <summary>
        /// 某一条消息被双击时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void msgItemElement_DoubleClick(object sender, HSControls.Scroll.ScrollEventArgs e)
        {
            MessageBox.Show(string.Format("这里只是演示，消息ID:{0},消息标题:{1}", e.ID, e.Element.Title));
        } 
        #endregion 

        #region IMessageDisplayer 接口 
        /// <summary>
        /// 消息展示窗体中的每一条消息
        /// </summary>  
        void IMessageDisplayer.Display(string id, string title)
        {
            ((IMessageDisplayer)(this)).Display(id, title, null, null);
        }
        /// <summary>
        /// 消息展示窗体中的每一条消息
        /// </summary>  
        void IMessageDisplayer.Display(string id, string title, string content)
        {
            ((IMessageDisplayer)(this)).Display(id, title, content, null);
        }
        /// <summary>
        /// 消息展示窗体中的每一条消息
        /// </summary>  
        void IMessageDisplayer.Display(string id, string title, string content, DateTime? time)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("title");
            if (!time.HasValue)
                time = DateTime.Now;

            ScrollElement msgItemElement = new ScrollElement(id, title, content, time.Value, MsgStatus.Unread.ToString(), this.scrollingText);
            msgItemElement.ImageClick += new EventHandler<HSControls.Scroll.ScrollEventArgs>(msgItemElement_DoubleClick);
            msgItemElement.TextClick += new EventHandler<HSControls.Scroll.ScrollEventArgs>(msgItemElement_DoubleClick); 
            scrollingText.AddScrollElement(msgItemElement);

            //如果当前滚动窗体为显示，但是有消息就显示该消息
            if (scrollingText.Message.Count > 0)
            {
                if (!this.Visible)
                {
                    this.Show();
                }
            }
        } 
        #endregion
    } 
}
