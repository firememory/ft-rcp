/**********************************************************************************
* 公司：恒生电子股份有限公司
* 作者：谢秀利 研发中心
* 创建历史：2013-05-23
* 修改历史：
* 
* 描述： 演示-客户服务类
**********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hundsun.Framework.IoC;
using Hundsun.Framework.Communication;
using System.Reflection;
using System.IO; 

namespace Hundsun.Framework.AddIns
{
    /// <summary>
    /// 演示-客户服务类
    /// </summary>
    public class CustomerService:ICustomerService
    {
        public CustomerService()
        {
            t2DataHelper = new T2DataHelper();
            mockDataHelper = new MockDataHelper();
        }

        private bool isMockTest = true;
        /// <summary>
        /// 是否模拟数据测试，目前使用模拟测试
        /// </summary>
        public bool IsMockTest
        {
            get
            {
                return isMockTest;
            }
            set
            {
                isMockTest = value;
            }
        }

        /// <summary>
        /// T2数据帮助器
        /// </summary> 
        private T2DataHelper t2DataHelper; 

        /// <summary>
        /// 模拟的数据帮助器
        /// </summary> 
        private MockDataHelper mockDataHelper;

        /// <summary>
        /// 得到客户信息
        /// </summary>
        /// <param name="basicRow"></param>
        /// <returns></returns>
        DataTable ICustomerService.GetCustomers(string customerID, string Name, string type, string phone)
        {
            // 目前采用模拟的内存数据
            if (this.IsMockTest)
            {
                //直接返回模拟的DataTable
                return this.mockDataHelper.GetMockCustomers(customerID, Name, type, phone);
            }
            else
            {
                //返回DataTable
                return this.t2DataHelper.GetCustomers(customerID, Name, type, phone);
            } 
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="cusomterID"></param>
        void ICustomerService.Delete(int cusomterID)
        {
            // 目前采用模拟的内存数据
            if (this.IsMockTest)
            {
                //删除客户
                this.mockDataHelper.MockDeleteCustomer(cusomterID);
            }
            else
            {
                //删除客户
                this.t2DataHelper.DeleteCustomer(cusomterID); 
            } 
        }

        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="basicRow"></param>
        void ICustomerService.Update(CustomerDataSet.BasicInfoRow basicRow)
        { 
            // 目前采用模拟的内存数据
            if (this.IsMockTest)
            {
                //更新客户
                this.mockDataHelper.MockUpdateCustomer(basicRow); 
            }
            else
            {
                //更新客户
                this.t2DataHelper.UpdateCustomer(basicRow); 
            } 
        }

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="basicRow"></param>
        void ICustomerService.Add(CustomerDataSet.BasicInfoRow basicRow)
        { 
            // 目前采用模拟的内存数据
            if (this.IsMockTest)
            {
                // 新增客户
                this.mockDataHelper.MockAddCustomer(basicRow);
            }
            else
            {
                // 新增客户
                this.t2DataHelper.AddCustomer(basicRow); 
            } 
        }
    }
}
