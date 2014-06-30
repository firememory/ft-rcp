/**********************************************************************************
* 公司：恒生电子股份有限公司
* 作者：谢秀利 研发中心
* 创建历史：2013-05-23
* 修改历史：
* 
* 描述： 演示-模拟客户数据帮助器&T2客户数据帮助器。目前使用模拟数据
**********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hundsun.Framework.Communication;
using Hundsun.Framework.IoC;
using System.Data;

namespace Hundsun.Framework.AddIns
{
    #region 模拟客户数据帮助器
    /// <summary>
    /// 模拟客户数据帮助器
    /// </summary>
    class MockDataHelper
    {
        //模拟客户列表
        CustomerDataSet.BasicInfoDataTable mockCustomers;

        public MockDataHelper()
        {
            //生成模拟数据
            mockCustomers = new CustomerDataSet.BasicInfoDataTable();
            CustomerDataSet.BasicInfoRow dr;
            string[] addrs = new string[] { "浙江", "浙江杭州", "浙江宁波", "浙江台州", "浙江温州", "浙江嘉兴", "浙江湖州" };
            string[] emails = new string[] { "someone@hundsun.com", "ab@hundsun.com", "cd@163.com", "ef@yahoo.com", "hi@hundsun.com", "gk@hundsun.com", "someone@gmail.com" };
            Random rand = new Random();
            for (int i = 1; i <=1000; i++)
            {
                dr = mockCustomers.NewBasicInfoRow();
                dr.Addr = addrs[rand.Next(0, addrs.Length - 1)]; 
                dr.CustomerID = i;
                dr.Email = emails[rand.Next(0, emails.Length - 1)];
                dr.Level = (i % 3 + 1).ToString();
                dr.Name = "Name " + dr.CustomerID;
                dr.OfficeTel = "0571-2882" + rand.Next(0, 1000).ToString().PadLeft(4, '0');
                dr.Phone = "1666666" + rand.Next(0, 1000).ToString().PadLeft(3, '0') + '6';
                dr.Type = (i % 3 + 1).ToString();
                dr.TradeDate = DateTime.Now.Subtract(new TimeSpan(i % 100, 0, 0, 0));
                dr.TradeMoney = (i % 100 + 1) * rand.Next(100,1000);
                mockCustomers.Rows.Add(dr);
            }
            
            mockCustomers.AcceptChanges();
        }

        /// <summary>
        /// 得到查询的客户列表
        /// </summary>
        /// <param name="basicRow"></param>
        /// <returns></returns>
        internal DataTable GetMockCustomers(string customerID, string Name, string type, string phone)
        {
            StringBuilder sb = new StringBuilder();
            //拼接过滤串 
            if (!string.IsNullOrWhiteSpace(customerID))
            {
                sb.AppendFormat("CustomerID ={0}", customerID);
                sb.AppendFormat(" And ");
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                sb.AppendFormat("Name like '{0}'", Name);
                sb.AppendFormat(" And ");
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                sb.AppendFormat("Phone like '{0}'", phone);
                sb.AppendFormat(" And ");
            }
            if (!string.IsNullOrWhiteSpace(type))
            {
                sb.AppendFormat("Type = '{0}'", type);
                sb.AppendFormat(" And ");
            }
            //删除 And 
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 5, 5);
            }
            //设置过滤串
            //mockCustomers.DefaultView.RowFilter = sb.ToString();
            //return mockCustomers.DefaultView;
            var drs = mockCustomers.Select(sb.ToString(), "CustomerID desc");
            var dt = new CustomerDataSet.BasicInfoDataTable();
            foreach (var dr in drs)
            {
                dt.ImportRow(dr);
            }
            return dt; 
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="cusomterID"></param>
        /// <returns></returns>
        internal void MockDeleteCustomer(int cusomterID)
        {
            var dr = mockCustomers.Rows.Find(cusomterID);
            mockCustomers.Rows.Remove(dr);
            this.mockCustomers.AcceptChanges();
        }

        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="customer"></param>
        internal void MockUpdateCustomer(CustomerDataSet.BasicInfoRow basicRow)
        {
            //必须包含客户的基本信息，才能更新数据
            if (basicRow == null)
            {
                throw new ArgumentException("basicRow");
            }   

            var dr = mockCustomers.Rows.Find(basicRow.CustomerID) as CustomerDataSet.BasicInfoRow;
            dr.Addr = basicRow.Addr;
            dr.Email = basicRow.Email;
            dr.Level = basicRow.Level;
            dr.Name = basicRow.Name;
            dr.OfficeTel = basicRow.OfficeTel;
            dr.Phone = basicRow.Phone;
            dr.Type = basicRow.Type;
            dr.TradeDate = basicRow.TradeDate;
            dr.TradeMoney = basicRow.TradeMoney;
            this.mockCustomers.AcceptChanges();
        }

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="basicRow"></param>
        internal void MockAddCustomer(CustomerDataSet.BasicInfoRow basicRow)
        {
            //必须包含客户的基本信息，才能更新数据
            if (basicRow == null)
            {
                throw new ArgumentException("basicRow");
            }
            var targetRow = this.mockCustomers.NewBasicInfoRow();
            CopyRow(basicRow, targetRow);
            //设置Id值
            var maxIdRow = this.mockCustomers.Rows[this.mockCustomers.Rows.Count-1] as CustomerDataSet.BasicInfoRow;  
            targetRow.CustomerID = maxIdRow.CustomerID + 1; 
            this.mockCustomers.Rows.Add(targetRow);
            this.mockCustomers.AcceptChanges();
        }

        private void CopyRow(CustomerDataSet.BasicInfoRow srcRow, CustomerDataSet.BasicInfoRow targetRow)
        {
            foreach (DataColumn col in targetRow.Table.Columns)
            {
                targetRow[col.ColumnName] = srcRow[col.ColumnName];
            }
        }
    }
    #endregion

    #region T2客户数据帮助器
    /// <summary>
    /// T2数据帮助器
    /// </summary>
    public class T2DataHelper
    {
        /// <summary>
        /// 框架注入ICommunication
        /// </summary>
        [Dependency]
        public ICommunication Communication
        {
            set
            {
                if (value != null)
                {
                    t2 = value.DBFactory.GetT2Data();
                }
            }
        }

        /// <summary>
        /// T2数据通信处理接口
        /// </summary>
        private IT2DataHandler t2;

        /// <summary>
        /// 根据设置的查询字段列表信息，得到数据表。该方法得到的列表数据主要用于列表页面的网格控件
        /// </summary>
        /// <param name="functionId">功能号</param>
        /// <param name="queryfields">查询字段列表</param>
        /// <returns>数据列表</returns>
        private DataTable GetDataTable(int functionId, Dictionary<T2Field, object> queryfields)
        {
            if (t2 == null)
                throw new ApplicationException("The T2 data handler is null");

            // 在T2ESBMessage池中取T2报文操纵接口
            IT2ESBMessage esbmsg = t2.GetT2Esbmsg();
            // 在T2Packer池中取T2打包器接口
            IT2Packer packer = t2.GetT2Packer(T2PackVersion.PACK_VERSION2);

            // 打包
            packer.BeginPack();
            T2Field field;
            foreach (KeyValuePair<T2Field, object> pair in queryfields)
            {
                field = pair.Key;
                packer.AddField(field.FieldName, field.FieldType, field.FieldWidth, pair.Value);
                packer.EndPack();
            }
            packer.EndPack();

            // 初始化报文
            esbmsg.Prepare(T2TagDef.REQUEST_PACKET, functionId);
            //esbmsg.Prepare(T2TagDef.REQUEST_PACKET, 70);
            // 设置报文包体部分数据
            esbmsg.MsgBody = packer.GetPackBuf();
            // 释放T2打包器接口
            t2.ReleaseT2Packer(packer);

            //同步发送
            t2.SynSendEsbMessage(ref esbmsg, 5000);

            // 在T2UnPacker池中取T2解包器接口
            IT2UnPacker unpacker = t2.GetT2UnPacker(esbmsg.MsgBody);
            t2.ReleaseT2Esbmsg(esbmsg);

            // 数据为空 或 存在error_no字段（表示出错）返回
            if (unpacker.GetColName(0) == "error_no")
            {
                // 如：获取错误号和错误信息
                int errorNo = unpacker.GetInt("error_no");
                string errorInfo = unpacker.GetStr("error_info");
                throw new BizException(errorNo, errorInfo);
            }
            else
            {
                DataTable dt = unpacker.ToDataTable(null);

                // 释放T2解包器接口
                t2.ReleaseT2UnPacker(unpacker);
                return dt;
            }
        } 

        /// <summary>
        /// 无返回值操作
        /// </summary>  
        /// <returns></returns>
        private void GeneralOperate(int functionId, Dictionary<T2Field, object> t2fields)
        {
            if (t2 == null)
                throw new ApplicationException("The T2 data handler is null");
            if (t2fields == null || t2fields.Count==0)
                throw new ArgumentNullException("t2fields");

            IT2ESBMessage esbmsg = t2.GetT2Esbmsg();
            // 在T2Packer池中取T2打包器接口
            IT2Packer packer = t2.GetT2Packer(T2PackVersion.PACK_VERSION2);
            // 打包 
            packer.BeginPack(); 
            T2Field field;
            foreach (KeyValuePair<T2Field, object> pair in t2fields)
            {
                field = pair.Key;
                packer.AddField(field.FieldName, field.FieldType, field.FieldWidth, pair.Value);
                packer.EndPack();
            }
            packer.EndPack();

            // 初始化报文，功能号码
            esbmsg.Prepare(T2TagDef.REQUEST_PACKET, functionId);
            // 设置报文包体部分数据
            esbmsg.MsgBody = packer.GetPackBuf();
            // 释放T2打包器接口
            t2.ReleaseT2Packer(packer);

            //同步发送
            t2.SynSendEsbMessage(ref esbmsg, 5000);

            // 在T2UnPacker池中取T2解包器接口
            IT2UnPacker unpacker = t2.GetT2UnPacker(esbmsg.MsgBody);
            t2.ReleaseT2Esbmsg(esbmsg);
            if ("error_no" == unpacker.GetColName(0))
            {
                // 如：获取错误号和错误信息
                int errorNo = unpacker.GetInt("error_no");
                string errorInfo = unpacker.GetStr("error_info");
                throw new BizException(errorNo, errorInfo);
            }

            // 释放T2解包器接口
            t2.ReleaseT2UnPacker(unpacker);
        }

        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="basicRow">客户基本信息</param>
        internal void UpdateCustomer(CustomerDataSet.BasicInfoRow basicRow)
        {
            //必须包含客户的基本信息，才能更新数据
            if (basicRow == null)
            {
                throw new ArgumentException("basicRow");
            }   

            //****************************************************************
            //如何取得数据，对于T2数据包的封装有各业务系统自己定义。
            //下面的代码只是做个范例之用，不能作为成熟的解决方案 
            //简单的将BasicRow 封装成 T2字段列表提供给服务端解析 
            var t2Fields = new Dictionary<T2Field, object>();
            var field = new T2Field("CustomerID", T2FieldType.TYPE_INT);
            t2Fields.Add(field, basicRow.CustomerID);
            field = new T2Field("Addr", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Addr);
            field = new T2Field("CreatedDate", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.TradeDate.ToShortDateString());
            field = new T2Field("Email", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Email);
            field = new T2Field("Level", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Level);
            field = new T2Field("Name", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Name);
            field = new T2Field("OfficeTel", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.OfficeTel);
            field = new T2Field("Phone", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Phone);
            field = new T2Field("Type", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Type);

            //功能号
            int functionId = 3;
            this.GeneralOperate(functionId, t2Fields);
            //****************************************************************
        }

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="basicRow">客户基本信息</param>
        internal void AddCustomer(CustomerDataSet.BasicInfoRow basicRow)
        {
            //必须包含客户的基本信息，才能更新数据
            if (basicRow == null)
            {
                throw new ArgumentException("basicRow");
            }   

            //****************************************************************
            //如何取得数据，对于T2数据包的封装有各业务系统自己定义。
            //下面的代码只是做个范例之用，不能作为成熟的解决方案 
            //简单的将BasicRow 封装成 T2字段列表提供给服务端解析 
            var t2Fields = new Dictionary<T2Field, object>();
            var field = new T2Field("Addr", T2FieldType.TYPE_INT);
            t2Fields.Add(field, basicRow.Addr);
            field = new T2Field("CreatedDate", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.TradeDate.ToShortDateString());
            field = new T2Field("Email", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Email);
            field = new T2Field("Level", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Level);
            field = new T2Field("Name", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Name);
            field = new T2Field("OfficeTel", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.OfficeTel);
            field = new T2Field("Phone", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Phone);
            field = new T2Field("Type", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, basicRow.Type);

            //功能号
            int functionId = 4;
            this.GeneralOperate(functionId, t2Fields);
            //**************************************************************** 
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="cusomterID">客户编号</param>
        internal void DeleteCustomer(int cusomterID)
        {
            //****************************************************************
            //如何取得数据，对于T2数据包的封装有各业务系统自己定义。
            //下面的代码只是做个范例之用，不能作为成熟的解决方案 
            //简单的将CustomerID 封装成 T2字段列表提供给服务端解析 
            var t2Fields = new Dictionary<T2Field, object>();
            var field = new T2Field("CustomerID", T2FieldType.TYPE_INT);
            t2Fields.Add(field, cusomterID);
            //功能号
            int functionId = 2;
            this.GeneralOperate(functionId, t2Fields);
            //****************************************************************
        }

        /// <summary>
        /// 得到客户信息
        /// </summary>
        /// <param name="customerID">客户编号</param>
        /// <param name="Name">客户名称</param>
        /// <param name="type">客户类型</param>
        /// <param name="phone">客户手机</param>
        /// <returns></returns>
        internal DataTable GetCustomers(string customerID, string Name, string type, string phone)
        {
            //****************************************************************
            //如何取得数据，对于T2数据包的封装有各业务系统自己定义。
            //下面的代码只是做个范例之用，不能作为成熟的解决方案 
            //简单的将封装成 T2字段列表提供给服务端解析 
            var t2Fields = new Dictionary<T2Field, object>();
            var field = new T2Field("Name", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, Name);
            if (!string.IsNullOrWhiteSpace(customerID))
            {
                field = new T2Field("CustomerID", T2FieldType.TYPE_INT);
                t2Fields.Add(field, int.Parse(customerID));
            }
            field = new T2Field("Phone", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, phone);
            field = new T2Field("Type", T2FieldType.TYPE_STRING);
            t2Fields.Add(field, type);
            //功能号
            int functionId = 1;
            return this.GetDataTable(functionId, t2Fields); 
            //****************************************************************
        }
    }

    /// <summary>
    /// 业务异常
    /// </summary>
    public class BizException : Exception
    {
        /// <summary>
        /// 错误编号
        /// </summary>
        public int ErrorNo
        {
            get;
            private set;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorInfo
        {
            get;
            private set;
        }

        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="errorNo">错误编号</param>
        /// <param name="errorInfo">错误信息</param>
        public BizException(int errorNo, string errorInfo)
            : base(errorInfo)
        {
            this.ErrorNo = errorNo;
            this.ErrorInfo = errorInfo;
        }
    }
    #endregion
}
