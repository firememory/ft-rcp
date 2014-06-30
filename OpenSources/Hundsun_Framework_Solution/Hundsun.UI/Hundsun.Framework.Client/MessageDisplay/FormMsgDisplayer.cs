using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.HSControls.Scroll;
using Hundsun.Framework.AddIn;
using Hundsun.Framework.HSControls;
using Hundsun.Framework.AddIn.WinForms;
using Hundsun.Framework.IoC;

namespace Hundsun.Framework.Client
{ 
    public partial class FormMsgDisplayer
    {
        #region 构造函数
        public FormMsgDisplayer()
        {
            InitializeComponent();

            this.SetParent(AddIn.WinForms.AddInController.AddInWorkItem.Services.Get<IAddInContainer>() as IFloating);
            this.scrollingText.TextScrollSpeed = (ScrollSpeed)Enum.Parse(typeof(ScrollSpeed), AddInUtility.ScrollSpeed);//设置速度
            this.scrollingText.Font = AddInUtility.ScrollFont;
            this.scrollingText.ForeColor = AddInUtility.ScrollFontColor; 
        }
        #endregion

        #region 字段和属性
        private IAddInContainer addInContiner;
        /// <summary>
        /// 插件容器接口
        /// </summary>
        [Dependency]
        public IAddInContainer AddInContainer
        {
            set
            {
                addInContiner = value;
            }
        }
        #endregion

        #region 系统菜单事件(打开主窗口和退出系统)
        /// <summary>
        /// 打开主窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemOpenMainForm_Click(object sender, EventArgs e)
        {
            this.SwitchToMain();
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemExitSystem_Click(object sender, EventArgs e)
        {
            this.addInContiner.CloseApplication();
        }
        #endregion

        #region 其他菜单操作(隐藏&字体&设置)
        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        /// <summary>
        /// 字体大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemFontSize_Click(object sender, EventArgs e)
        {
            this.fontDialog1.Font = this.scrollingText.Font;
            if (this.fontDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.scrollingText.Font = this.fontDialog1.Font;
                AddInUtility.ScrollFont = this.scrollingText.Font;
            }
        }
        /// <summary>
        /// 字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemFontColor_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.scrollingText.ForeColor;
            if (this.colorDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.scrollingText.ForeColor = this.colorDialog1.Color;
                //覆盖每个滚动元素的前景色
                foreach (ScrollElement se in this.scrollingText.Message)
                {
                    se.ForeColor = this.scrollingText.ForeColor;
                }
                AddInUtility.ScrollFontColor = this.colorDialog1.Color;
            }
        } 
        /// <summary>
        /// 速度为慢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSpeedSlow_Click(object sender, EventArgs e)
        {
            if (!itemSpeedSlow.Checked)
            {
                itemSpeedSlow.Checked = true;
                this.scrollingText.TextScrollSpeed = ScrollSpeed.Low;
                AddInUtility.ScrollSpeed = this.scrollingText.TextScrollSpeed.ToString();
            }
        }
        /// <summary>
        /// 速度为中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSpeedNormal_Click(object sender, EventArgs e)
        {
            if (!itemSpeedNormal.Checked)
            {
                itemSpeedNormal.Checked = true;
                this.scrollingText.TextScrollSpeed = ScrollSpeed.Normal;
                AddInUtility.ScrollSpeed = this.scrollingText.TextScrollSpeed.ToString();
            }
        }
        /// <summary>
        /// 速度为快
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSpeedFast_Click(object sender, EventArgs e)
        {
            if (!itemSpeedFast.Checked)
            {
                itemSpeedFast.Checked = true;
                this.scrollingText.TextScrollSpeed = ScrollSpeed.High;
                AddInUtility.ScrollSpeed = this.scrollingText.TextScrollSpeed.ToString();
            }
        } 
        #endregion
    }
}
