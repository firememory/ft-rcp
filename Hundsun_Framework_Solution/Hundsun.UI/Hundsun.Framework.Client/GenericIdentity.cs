using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Hundsun.Framework.Client
{
    /// <summary>
    /// 该类应该放入业务应用的公共实体信息项目（如Hundsun.Framework.BizEntity）
    /// </summary>
    public class GenericIdentity : Hundsun.Framework.Communication.IIdentity
    {
        /// <summary>
        /// 获取或设置操作员Id
        /// </summary>
        public long OperId
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置操作员的编号
        /// </summary>
        public string OperCode
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置操作员姓名
        /// </summary>
        public string OperName
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置当前操作员密码
        /// </summary>
        public string OperPwd
        {
            get;
            set;
        }
        private string language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        /// <summary>
        /// 获取或设置当前的语言环境信息
        /// CultureInfo.Name值，如zh-CN,en-US,zh,en等等
        /// </summary>
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }
    }
}
