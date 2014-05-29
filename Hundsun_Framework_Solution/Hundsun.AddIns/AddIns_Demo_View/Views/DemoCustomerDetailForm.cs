/**********************************************************************************
 * 公司：恒生电子股份有限公司
 * 作者：谢秀利 研发中心
 * 创建历史：2013-05-23
 * 修改历史：
 * 
 * 描述： 演示明细窗体-客户基本信息明细窗体
**********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hundsun.Framework.UIFrame;
using Hundsun.Framework.Entity;
using Hundsun.Framework.HSControls;

namespace Hundsun.Framework.AddIns
{
    /// <summary>
    /// 演示明细窗体-客户基本信息明细窗体
    /// </summary>
    internal partial class DemoCustomerDetailForm : AddInFormBase
    {
        #region 构造函数
        /// <summary>
        /// 该构造函数只是提供设计器使用
        /// </summary>
        public DemoCustomerDetailForm() { InitializeComponent(); }

        /// <summary>
        /// 调用方使用该构造函数
        /// </summary>
        /// <param name="formState">当前窗体状态</param>
        /// <param name="basicRow">客户信息行，新增时候为空</param>
        /// <param name="customerService">客户服务接口实例</param>
        public DemoCustomerDetailForm(FormState formState, CustomerDataSet.BasicInfoRow basicRow, ICustomerService customerService)
            : base()
        {
            InitializeComponent(); 
            this.basicRow = basicRow;
            this.formState = formState;
            this.customerService = customerService; 
        }
        #endregion

        #region 字段
        /// <summary>
        /// 窗体状态
        /// </summary>
        FormState formState = FormState.View;
        /// <summary>
        /// 窗体的绑定的数据行
        /// </summary>
        CustomerDataSet.BasicInfoRow basicRow;
        /// <summary>
        /// 客户服务接口
        /// </summary>
        ICustomerService customerService;

        /// <summary>
        /// 更新事件
        /// </summary>
        [Browsable(false)]
        internal event EventHandler<TEventArgs<CustomerDataSet.BasicInfoRow>> UpdateSucceed;
        /// <summary>
        /// 新增事件
        /// </summary>
        [Browsable(false)]
        internal event EventHandler<TEventArgs<CustomerDataSet.BasicInfoRow>> AddSucceed;

        private DataTable typeDt = null;

        private DataTable levelDt = null;
        private DataTable level2Dt = null;
        #endregion

        #region 初始化
       
        /// <summary>
        /// 窗体Load事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDetailForm_Load(object sender, EventArgs e)
        { 
            // 校验窗体输入参数
            if (this.ValidateParams())
            {
                //初始化
                this.InitForm(); 
            }
            else
            {
                this.SetUIStateForView();
            }

            //动态布局
            this.pnlMain.DoLayout();
            //this.pnlMain.LayoutOrientation = LayoutOrientation.Vertical;
            //this.pnlMain.LayoutCol = 2;
            //var minSize = this.pnlMain.DoLayout();
            //this.Size = new Size(minSize.Width, minSize.Height + 35);
        }

        /// <summary>
        /// 校验窗体输入参数
        /// </summary>
        private bool ValidateParams()
        {
            if (basicRow == null && (this.formState == FormState.View || this.formState == FormState.Edit))
            {
                MsgBoxUtility.ShowError("请指定客户信息！");
                return false;
            }

            if (customerService == null && (this.formState == FormState.Add || this.formState == FormState.Edit))
            {
                MsgBoxUtility.ShowError("请指定客户信息服务接口！");
                return false;
            }

            return true;
        } 

        /// <summary>
        /// 初始化界面
        /// </summary>
        void InitForm()
        {
            //如果是新做状态初始化一空行
            if (this.formState == FormState.Add)
            {
                CustomerDataSet.BasicInfoDataTable dt = new CustomerDataSet.BasicInfoDataTable();
                this.basicRow = dt.NewBasicInfoRow();
                this.basicRow.TradeDate = DateTime.Today;
            }

            //初始化数据绑定
            this.InitDataBindings();

            //绑定界面数据&设置标题
            this.SetUIState();
        }

        /// <summary>
        /// 初始化数据绑定
        /// </summary>
        private void InitDataBindings()
        { 
            //初始化下拉控件数据源 
            //客户级别
            this.levelDt = new DataTable();
            levelDt.Columns.Add("levelId");
            levelDt.Columns.Add("levelName");
            levelDt.Rows.Add(new object[] { "1", "Common" });
            levelDt.Rows.Add(new object[] { "2", "VIP" });
            levelDt.Rows.Add(new object[] { "3", "Super" });

            this.level2Dt = this.levelDt.Copy();
            this.level2Dt.Rows.RemoveAt(2);

            //客户类型
            this.typeDt = new DataTable();
            typeDt.Columns.Add("typeId");
            typeDt.Columns.Add("typeName");
            typeDt.Rows.Add(new object[] { "1", "客户类型1" });
            typeDt.Rows.Add(new object[] { "2", "客户类型2" });
            typeDt.Rows.Add(new object[] { "3", "客户类型3" });


            //客户级别（单选）
            this.cmbLevel.KeyName = "levelId";
            this.cmbLevel.DataSource = this.levelDt;

            //客户类型（多选） 
            this.cmbType.KeyName = "typeId";
            this.cmbType.DataSource = this.typeDt; 

            this.txtId.Value = basicRow.CustomerID.ToString();
            this.cmbType.Value = basicRow.Type;
            this.cmbLevel.Value = basicRow.Level;
            this.numTradeMoney.Value = basicRow.TradeMoney;
            this.txtName.Value = basicRow.Name;
            this.txtAddr.Value = basicRow.Addr;
            this.txtEmail.Value = basicRow.Email;
            this.txtPhone.Value = basicRow.Phone;
            this.txtTel.Value = basicRow.OfficeTel;
            this.dtpTradeDate.Value = basicRow.TradeDate; 
        }

        
        private DataTable GetCustomerType()
        {
           
            return typeDt;
        }

        /// <summary>
        /// 设置界面UI的状态：窗体标题，控件的可用性，可见性
        /// </summary>
        private void SetUIState()
        {
            //绑定界面数据&设置标题
            switch (this.formState)
            {
                case FormState.Edit:
                    this.SetTitle("修改客户基本信息");
                    this.SetUIStateForEdit();
                    break;
                case FormState.Add:
                    this.SetTitle("新增客户基本信息");
                    this.SetUIStateForAdd();
                    break;
                case FormState.View:
                    this.SetTitle("查看客户基本信息");
                    this.SetUIStateForView();
                    break;
                default:
                    this.SetTitle("维护客户基本信息");
                    this.SetUIStateForView();
                    break;
            }
        }

        /// <summary>
        /// 设置新增状态下界面UI的状态
        /// </summary>
        private void SetUIStateForAdd()
        {
            this.txtId.Visible = false;
        }

        /// <summary>
        /// 设置查看状态下界面UI的状态
        /// </summary>
        private void SetUIStateForView()
        {
            this.txtId.Enabled = false;
            this.txtName.Enabled = false;
            this.txtAddr.Enabled = false;
            this.txtEmail.Enabled = false;
            this.txtPhone.Enabled = false;
            this.txtTel.Enabled = false;
            this.cmbType.Enabled = false;
            this.cmbLevel.Enabled = false;
            this.dtpTradeDate.Enabled = false;
            this.numTradeMoney.Enabled = false;
            this.btnOK.Visible = false;
            this.btnReset.Visible = false;
        }

        /// <summary>
        /// 设置编辑状态下界面UI的状态
        /// </summary>
        private void SetUIStateForEdit()
        {
            this.txtId.Enabled = false;
        }

        private void SetTitle(string title)
        {
            this.Text = title;
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 按钮启用自动校验(EnableAutoValidate属性为True)前事件处理程序，添加自定义校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_BeforeAutoValidate(object sender, AutoValidateCustomEventArgs e)
        {
            //添加自定义校验
            //添加校验：交易日期不能大于今天
            e.AddCustomValidator(() =>
            {
                if (this.dtpTradeDate.Value.Date > DateTime.Today)
                {
                    return new ValidatingResult()
                    {
                        Success = false,
                        ErrorControls = new Control[] { this.dtpTradeDate },
                        ErrorMessage = "交易日期不能大于今天"
                    };
                }
                else
                {
                    return new ValidatingResult()
                    {
                        Success = true,
                        ErrorControls = null,
                        ErrorMessage = null
                    };
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        public override void SetTheme(int theme)
        {
            base.SetTheme(theme);
        }
        /// <summary>
        /// 按钮启用自动校验(EnableAutoValidate属性为True)后事件处理程序,根据校验结果看是否更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_AfterAutoValidate(object sender, AutoValidateResultEventArgs e)
        {
            //如果校验通过
            if (e.Result)
            {
                //绑定界面数据&设置标题
                switch (this.formState)
                {
                    case FormState.Edit:
                        try
                        {
                            //收集数据
                            this.CollectData();

                            //更新
                            this.customerService.Update(this.basicRow);
                            MsgBoxUtility.ShowTips("修改成功！");
                            //执行更新成功事件
                            if (this.UpdateSucceed != null)
                            { 
                                this.UpdateSucceed(this, new TEventArgs<CustomerDataSet.BasicInfoRow>(this.basicRow));
                            }
                        }
                        catch (Exception ex)
                        {
                            MsgBoxUtility.ShowError("修改失败：" + ex.Message);
                        }
                        break;
                    case FormState.Add:
                        try
                        {
                            //收集数据
                            this.CollectData();
                            //新增
                            this.customerService.Add(this.basicRow);
                            MsgBoxUtility.ShowTips("新增成功！");
                            //执行添加成功事件
                            if (this.AddSucceed != null)
                            {
                                this.AddSucceed(this, new TEventArgs<CustomerDataSet.BasicInfoRow>(this.basicRow));
                            }
                        } 
                        catch (Exception ex)
                        {
                            MsgBoxUtility.ShowError("新增失败："+ex.Message);
                        }
                        break; 
                    default: 
                        break;
                }
            }
        }

        /// <summary>
        /// 重置按钮单击：重置hspanel内的子控件的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            //重置hspanel内的子控件的值
            this.pnlMain.ResetChildren();
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 
        
        #region 私有方法

        /// <summary>
        /// /收集数据
        /// </summary>
        private void CollectData()
        {
            this.basicRow.Type = this.cmbType.Value;
            this.basicRow.Level = this.cmbLevel.Value;
            this.basicRow.TradeMoney = this.numTradeMoney.Value;
            this.basicRow.Name = this.txtName.Value;
            this.basicRow.Addr = this.txtAddr.Value;
            this.basicRow.Email = this.txtEmail.Value;
            this.basicRow.Phone = this.txtPhone.Value;
            this.basicRow.OfficeTel = this.txtTel.Value;
            this.basicRow.TradeDate = this.dtpTradeDate.Value;
        } 

        #endregion

        #region 校验事件&校验成功后事件
        /// <summary>
        /// 客户名称校验事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_HSValidating(object sender, HSValidatingEventArgs e)
        {
            if (!txtName.Value.StartsWith("N"))
            {
                e.Result = false;
                e.ErrorMessage = "客户名称必须是大写N打头的";
            } 
        }

        /// <summary>
        /// 客户名称校验成功后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_HSValidated(object sender, HSValidatedEventArgs e)
        {
            if (this.formState == FormState.Edit)
            {
                var customers = this.customerService.GetCustomers("", txtName.Value, "", "");

                if (customers != null && customers.Rows.Count ==1 )
                {
                    var basicDr = customers.Rows[0] as CustomerDataSet.BasicInfoRow;
                     
                    //找到相似的客户基本信息，带出客户信息 
                    this.txtPhone.Value = basicDr.Phone;
                    this.txtTel.Value = basicDr.OfficeTel;
                    this.txtEmail.Value = basicDr.Email;
                    this.txtAddr.Value = basicDr.Addr;
                    this.numTradeMoney.Value = basicDr.TradeMoney;
                    this.dtpTradeDate.Value = basicDr.TradeDate; 
                }
            }
        }

        /// <summary>
        /// 客户类型校验成功后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbType_HSValidated(object sender, HSValidatedEventArgs e)
        {
            var selectTypes = this.cmbType.GetSelectItems<DataRow>();
            if (selectTypes!=null && selectTypes.Count > 0)
            {
                foreach(DataRow selectType in selectTypes)
                {
                    //如果客户类型中包含客户类型1则联动客户级别,变更数据源为level2Dt
                    if (Convert.ToString(selectType["typeId"]) == "1")
                    {
                        //客户级别（单选）  
                        this.cmbLevel.DataSource = this.level2Dt;
                        return;
                    } 
                }

                this.cmbLevel.DataSource = levelDt;
            }
        }
        #endregion  
    } 

    /// <summary>
    /// 事件参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TEventArgs<T> : EventArgs
    {
        public T Args
        {
            get;
            private set;
        }

        public TEventArgs(T args)
        {
            this.Args = args;
        }
    }
    
    /// <summary>
    /// 窗体状态
    /// </summary>
    internal enum FormState
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add,
        /// <summary>
        /// 修改
        /// </summary>
        Edit,
        /// <summary>
        /// 查看
        /// </summary>
        View
    }
}
