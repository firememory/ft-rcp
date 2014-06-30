/**********************************************************************************
* 公司：恒生电子股份有限公司
* 作者：谢秀利 研发中心
* 创建历史：2013-05-23
* 修改历史：
* 
* 描述： 演示-客户服务的接口
**********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hundsun.Framework.Communication;

namespace Hundsun.Framework.AddIns
{
    /// <summary>
    /// 演示-客户服务的接口
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// 根据设置的查询字段列表信息，得到未分页的数据。该方法得到的列表数据主要用于列表页面的网格控件
        /// </summary> 
        /// <returns>数据列表</returns>
        DataTable GetCustomers(string customerID, string Name, string type, string phone); 

        /// <summary>
        /// 根据客户主键信息，删除客户信息
        /// </summary>
        /// <param name="cusomterID">客户主键</param> 
        void Delete(int cusomterID);

        /// <summary>
        /// 根据客户信息数据集，更新目标信息
        /// </summary> 
        /// <param name="cusomter">客户信息</param> 
        void Update(CustomerDataSet.BasicInfoRow cusomter);

        /// <summary>
        /// 根据客户信息数据集，新增目标信息
        /// </summary> 
        /// <param name="cusomter">客户信息</param> 
        void Add(CustomerDataSet.BasicInfoRow cusomter);
    } 
}
