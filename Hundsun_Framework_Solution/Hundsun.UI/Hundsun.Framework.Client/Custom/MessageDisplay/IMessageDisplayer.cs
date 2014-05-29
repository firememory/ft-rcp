using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hundsun.Framework.Client
{ 
    /// <summary>
    /// 消息展示接口
    /// </summary>
    public interface IMessageDisplayer
    {
        ///// <summary>
        ///// 将指定的消息，加入展示队列中进行展示
        ///// </summary>
        ///// <param name="msgItem">它表示消息展示窗体中的每一条消息</param>
        //void Display(MsgDisplayItem msgItem);
        /// <summary>
        /// 将指定的消息，加入展示队列中进行展示
        /// </summary>
        /// <param name="id">消息编号</param>
        /// <param name="title">消息标题</param> 
        void Display(string id, string title); 
        /// <summary>
        /// 将指定的消息，加入展示队列中进行展示
        /// </summary>
        /// <param name="id">消息编号</param>
        /// <param name="title">消息标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="time">日期时间</param>
        void Display(string id, string title, string content);
        /// <summary>
        /// 将指定的消息，加入展示队列中进行展示
        /// </summary>
        /// <param name="id">消息编号</param>
        /// <param name="title">消息标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="time">日期时间</param>
        void Display(string id, string title, string content, DateTime? time);
    }
}
