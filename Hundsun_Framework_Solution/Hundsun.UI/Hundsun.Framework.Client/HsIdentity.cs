using System;
using System.Collections.Generic;
using System.Text;

namespace Hundsun.Framework.Client
{
    /// <summary>
    /// 用于表示客户端用户身份标示类。
    /// 该类应该放入业务应用的公共实体信息项目（如Hundsun.Framework.BizEntity）
    /// </summary> 
    public class HsIdentity : GenericIdentity, Hundsun.Framework.Communication.IIdentity
    {
        /// <summary>
        /// 操作员的特定属性1(Demo)
        /// </summary>
        public string SpecialProperty1
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员的特定属性2(Demo)
        /// </summary>
        public string SpecialProperty2
        {
            get;
            set;
        }
        public int CmpId { get; set; }
        public int DeptID { get; set; }
        public int SubSystemID { get; set; }
    }
}
