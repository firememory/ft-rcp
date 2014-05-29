/**********************************************************************************
* 公司：恒生电子股份有限公司
* 作者：谢秀利 研发中心
* 创建历史：2013-05-23
* 修改历史：
* 
* 描述： 演示插件-客户基本信息管理插件
**********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.IoC;
using Hundsun.Framework.UIFrame;
using Hundsun.Framework.HSControls;
using Hundsun.Framework.AddIn;
using Hundsun.Framework.AddIn.WinForms;
using System.Threading;

namespace Hundsun.Framework.AddIns
{
    /// <summary>
    /// 演示插件-客户基本信息管理插件
    /// </summary>
    /// <remarks>
    /// AddInPart特性各个参数说明如下：
    /// Name代表插件名；必填，否则框架无法找到插件。
    /// Group代表插件所属分组；选填。
    /// ICON代表插件使用的图标（在工作区中显示）；选填。
    /// Author代表插件作者；选填。
    /// Version代表插件版本；选填。
    /// Description代表插件描述信息；选填。
    /// </remarks>
    [AddInPart("演示:客户信息管理", "", "xiexl", "1", "1", "Customer")]
    public partial class DemoCustomerView : AddInPartBase
    {
        #region 构造函数
        public DemoCustomerView()
        {
            InitializeComponent();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 客户服务接口,框架依赖注入
        /// </summary>  
        [Dependency(CreateType = typeof(CustomerService))]
        public ICustomerService CustomerService
        {
            get;
            set;
        }

        /// <summary>
        /// 消息滚动展示器
        /// </summary>
        [Dependency]
        public Hundsun.Framework.Client.IMessageDisplayer MessageDisplayer
        {
            get;
            set;
        }
        #endregion

        #region 初始化
        /// <summary>
        /// Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerView_Load(object sender, EventArgs e)
        { 
            //设置不自动产生列，该设置应该在你已经配置了列布局信息后，设置
            this.hsDataGridView1.AutoGenerateColumns = true;
            //加载列布局,计算列宽度请使用hsDataGridView1的AutoSizeColumnMode属性进行设置
            ColumnLayoutUtility.LoadColumnLayout(this.toolBtnLayout);
            string menu = this.MenuID;
            List<string> function = this.Functions;
            //this.QueryAndBindGridData();
            ////加载列布局,从配置文件中加载，指定是否创建列
            //ColumnLayoutUtility.LoadColumnLayout(this.toolBtnLayout.GroupName, this.toolBtnLayout.TargetGrid, true);
        }
        #endregion

        #region 主题样式相关

        public override void SetTheme(int theme)
        {
            base.SetTheme(theme);
        }
        #endregion

        #region 插件的打开和关闭
        /// <summary>
        /// 当关闭当前插件时候，该方法将被调用；
        /// 方法返回true，表示继续关闭，否则阻止关闭操作。
        /// </summary>
        /// <returns></returns>
        public override bool CloseAddInPart()
        {
            //这里只是演示，您的插件里可能有复杂的关闭前的逻辑。
            bool b = MsgBoxUtility.ShowConfirmYesNo("您确定需要关闭客户信息管理吗？");
            if (b)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 当启动插件时候，该方法将被调用；
        /// </summary>
        public override void RunAddInPart()
        {
            base.RunAddInPart();
        }
        #endregion

        #region 功能权限
        /// <summary>
        /// 根据菜单号取得功能号，返回的功能列表将被用于顾虑Toolbar上的功能按钮。
        /// 目前默认返回 "1", "2", "3", "4", "5"。
        /// 在该Demo中"1" 是新增；"2" 是修改；"3"是查看；"4"是删除；"5"是查询(和列布局设置)
        /// </summary>
        /// <param name="menuID">菜单号</param>
        /// <returns></returns>
        //public override List<string> GetFunctions(string menuID)
        //{
        //    //业务系统可以编写自己的插件基类实现自己的功能权限逻辑：根据菜单号，获取功能号列表
        //    //这里只是演示
        //    if (menuID == "1" || string.IsNullOrWhiteSpace(menuID))
        //    {
        //        return new List<string>() { "1", "2", "3", "4", "5" };
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        [AddInParameter]
        public override string MenuID
        {
            get
            {
                return base.MenuID;
            }
            set
            {
                base.MenuID = value;
            }
        }

        [AddInParameter]
        public override string FunctionIDs
        {
            get
            {
                return base.FunctionIDs;
            }
            set
            {
                base.FunctionIDs = value;
                base.Functions = new List<string>(value.ToString().Split(',')); 
            }
        }

        #endregion

        #region DataGridView的事件
        #region DataGridView的格式化
        /// <summary>
        /// DataGridView的格式化。
        /// 如果不采用自动生成列的话，就尽量避免使用该事件进行格式化，
        /// 因为你完全可以使用类似DataGridViewComboBoxColumn的列类型来实现自动的格式化。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dgv = sender as hsDataGridView;
            if (dgv.Columns.Contains("Level") && dgv.Columns["Level"].Index == e.ColumnIndex)
            {
                e.Value = GetLevelName(Convert.ToString(e.Value));
                e.FormattingApplied = true;
            }
            else if (dgv.Columns.Contains("Type") && dgv.Columns["Type"].Index == e.ColumnIndex)
            {
                e.Value = GetTypeName(Convert.ToString(e.Value));
                e.FormattingApplied = true;
            }
        }

        /// <summary>
        /// 得到类型名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetTypeName(string value)
        {
            if (value != null)
            {
                var values = value.Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries);
                string result = "";
                foreach (var v in values)
                {
                    //应该采用获取字典来取得，这里只是做演示所以直接判断返回值
                    if (v == "1")
                    {
                        result += "客户类型" + ",";
                    }
                    else if (v == "2")
                    {
                        result += "客户类型2" + ",";
                    }
                    else if (v == "3")
                    {
                        result += "客户类型3" + ",";
                    }
                    else
                        continue;
                }

                if (result.Length > 0)
                {
                    result = result.Remove(result.Length - 1, 1);
                }

                return result;
            }

            return "";
        }

        /// <summary>
        /// 得到级别名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetLevelName(string value)
        {
            //应该采用获取字典来取得，这里只是做演示所以直接判断返回值
            if (value == "1")
            {
                return "Common";
            }
            else if (value == "2")
            {
                return "VIP";
            }
            else if (value == "3")
            {
                return "Super";
            }
            else
                return "";
        }
        #endregion

        /// <summary>
        /// 单元格双击，默认打开查看窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //打开查看窗体
            OpenViewForm();
        }
        #endregion  

        #region 按钮事件

        #region 查询按钮单击
        /// <summary>
        /// 查询按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnQuery_Click(object sender, EventArgs e)
        {
            LoadingWaitingUtility.Show(this.hsPanel1,
                (o) =>
                {
                    System.Threading.Thread.Sleep(2000);
                    o.DisplayMessage = "正在加载客户信息1，请等待...";
                    this.QueryAndBindGridData();
                    System.Threading.Thread.Sleep(2000);
                    o.DisplayMessage = "正在加载客户信息2，请等待...";
                    System.Threading.Thread.Sleep(2000);
                    o.DisplayMessage = "正在加载客户信息3，请等待...";
                    System.Threading.Thread.Sleep(2000);
                }
                , null, "正在加载客户信息，请等待...", true);
            //查询和绑定数据
            //this.QueryAndBindGrid();
        }
        #endregion 

        #region 新增按钮单击
        /// <summary>
        /// 新增按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnAdd_Click(object sender, EventArgs e)
        {
            DemoCustomerDetailForm frm = new DemoCustomerDetailForm(FormState.Add, null, this.CustomerService);
            //绑定窗体新增成功事件
            frm.AddSucceed += (obj, args) =>
            {
                // 记住分页信息的查询客户信息操作然后绑定客户信息
                this.RemeberPagerQueryAndBind();

                //发布滚动消息
                this.MessageDisplayer.Display("customerview" + DateTime.Now.ToString(), string.Format("{0}客户新增成功！",args.Args.Name), "客户新增成功消息内容",DateTime.Now.AddDays(-1));
            };
            frm.ShowDialog();
        }
        #endregion

        #region 查看按钮单击
        /// <summary>
        /// 查看按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnView_Click(object sender, EventArgs e)
        {
            //打开查看窗体
            OpenViewForm();
        }

        /// <summary>
        /// 打开查看窗体
        /// </summary>
        private void OpenViewForm()
        {
            CustomerDataSet.BasicInfoRow basicRow = GetSelectedRow();
            if (basicRow == null)
            {
                MsgBoxUtility.ShowError("请选择一条记录！");
                return;
            }

            DemoCustomerDetailForm frm = new DemoCustomerDetailForm(FormState.View, basicRow, null);
            frm.ShowDialog();
        }
        #endregion

        #region 删除按钮单击
        /// <summary>
        /// 删除按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnDelete_Click(object sender, EventArgs e)
        {
            CustomerDataSet.BasicInfoRow basicRow = GetSelectedRow();
            if (basicRow == null)
            {
                MsgBoxUtility.ShowError("请选择一条记录！");
                return;
            }
            try
            {
                this.CustomerService.Delete(basicRow.CustomerID);

                // 记住分页信息的查询客户信息操作然后绑定客户信息
                this.RemeberPagerQueryAndBind();
            }
            catch (Exception ex)
            {
                MsgBoxUtility.ShowTips(ex.Message);
            }
        }
        #endregion

        #region 编辑按钮单击
        /// <summary>
        /// 编辑按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnEdit_Click(object sender, EventArgs e)
        {
            CustomerDataSet.BasicInfoRow basicRow = GetSelectedRow();
            if (basicRow == null)
            {
                MsgBoxUtility.ShowError("请选择一条记录！");
                return;
            }

            DemoCustomerDetailForm frm = new DemoCustomerDetailForm(FormState.Edit, basicRow, this.CustomerService);
            //绑定窗体更新成功事件
            frm.UpdateSucceed += (obj, args) =>
            {
                // 记住分页信息的查询客户信息操作然后绑定客户信息
                this.RemeberPagerQueryAndBind();
            };
            frm.ShowDialog();
        }
        #endregion

        #region 导出
        private void btnExport_Click(object sender, EventArgs e)
        {
            DataGridViewExportForm frm = new DataGridViewExportForm(this.hsDataGridView1, new String[] { "客户列表" });
            frm.ShowDialog();
        }
        #endregion

        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataGridViewPrintForm frm = new DataGridViewPrintForm(this.hsDataGridView1, new String[] { "客户列表" });
            frm.ShowDialog();
            //Dictionary<string, object> o = this.hsDataGridView1.FooterList;
        }
        #endregion

        #endregion

        #region 私有方法
        /// <summary>
        /// 记住分页信息的查询客户信息操作然后绑定客户信息
        /// </summary>
        private void RemeberPagerQueryAndBind()
        {
            //记录当前的分页号
            int oldPageIndex = this.hsPager1.CurrentPageIndex;
            //是否首页
            bool isFistPage = this.hsPager1.IsFirstPage;
            //查询和绑定Grid
            this.QueryAndBindGrid();
            //hsPager1绑定数据源后，会跳转到首页
            if (!isFistPage)
            {
                //跳转到指定的分页号
                this.hsPager1.GoPage(oldPageIndex);
            }
        }

        /// <summary>
        /// 查询客户信息操作然后绑定客户信息
        /// </summary>
        private void QueryAndBindGrid()
        {
            try
            {
                //先置分页数据源为空
                this.hsPager1.TargetDataSource = null;
                //重新查询
                this.hsPager1.TargetDataSource = this.CustomerService.GetCustomers(this.hsLabelTextBox1.Value, this.hsLabelTextBox2.Value, this.hsLabelComboBox1.Value, this.hsLabelTextBox3.Value);

            }
            catch (Exception ex)
            {
                MsgBoxUtility.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// 查询客户信息操作然后绑定客户信息
        /// </summary>
        private void QueryAndBindGridData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Action(this.QueryAndBindGridData));
            }
            else
            {
                this.QueryAndBindGrid();
            }
        }
        /// <summary>
        /// 得到选中行
        /// </summary>
        /// <returns></returns>
        private CustomerDataSet.BasicInfoRow GetSelectedRow()
        {
            if (this.hsDataGridView1.SelectedRows.Count > 0)
            {
                return (this.hsDataGridView1.SelectedRows[0].DataBoundItem as DataRowView).Row as CustomerDataSet.BasicInfoRow;
            }
            else
            {
                return null;
            }
        }
        #endregion   
    }
}
