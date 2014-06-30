using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Hundsun.Framework.AddIn.WinForms;
using Hundsun.Framework.Client;
using Hundsun.Framework.Communication;
using Hundsun.Framework.Entity;
using Hundsun.Framework.IoC;
using System.Data;

namespace Hundsun.Framework.Client
{
    public class LoginController : AddInController
    {
        public const int USER_LOGIN = 69209;
        public const int USER_ROLE_QUERY = 69221;
        public const int ROLE_MENU_QUERY = 69236;
        public const int USER_MENU_QUERY = 69207;

        private IT2DataHandler t2;
        private IH5Adapter h5;
        private ICommunication communication;
        [Dependency]
        public ICommunication Communication
        {
            set
            {
                this.communication = value;
                t2 = this.communication.DBFactory.GetT2Data();
                h5 = this.communication.DBFactory.GetH5Data();
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public HsIdentity Login(string userID, string pwd)
        {
            IT2ESBMessage esbmsg = null;
            IT2Packer packer = null;
            IT2UnPacker unpacker = null;
            HsIdentity identity = null;
            try
            {
                esbmsg = t2.GetT2Esbmsg();
                packer = t2.GetT2Packer(T2PackVersion.PACK_VERSION2);
                
                packer.BeginPack();
                packer.AddField("user_login_id", T2FieldType.TYPE_STRING, userID);
                packer.AddField("user_pwd", T2FieldType.TYPE_STRING, pwd);
                packer.AddField("ws_mac", T2FieldType.TYPE_STRING, GetMacAddress());
                packer.EndPack();

                esbmsg.Prepare(T2TagDef.REQUEST_PACKET, USER_LOGIN);
                esbmsg.MsgBody = packer.GetPackBuf();
                t2.SynSendEsbMessage(ref esbmsg, 5000);

                if (esbmsg.ReturnCode != 0)
                {
                    if (esbmsg.ReturnCode == 1 || esbmsg.ReturnCode == -1)
                    {
                        throw new T2Exception(esbmsg.ErrorNo, esbmsg.ErrorInfo);
                    }
                }

                unpacker = t2.GetT2UnPacker(esbmsg.MsgBody);

                if (esbmsg.ReturnCode > 1) //说明有错
                {
                    int errorNo = unpacker.GetInt("error_no");
                    string errorInfo = unpacker.GetStr("error_info");
                    throw new T2Exception(errorNo, errorInfo);
                }
                else
                {
                    identity = new HsIdentity();
                    identity.OperId = unpacker.GetInt("user_id");
                    identity.OperCode = unpacker.GetStr("user_login_id");
                    identity.CmpId = unpacker.GetInt("cmp_id");
                    identity.SubSystemID = 1;//目前默认为1
                }
            }
            finally
            {
                //释放T2打包器接口
                if (null != packer)
                {
                    t2.ReleaseT2Packer(packer);
                }
                if (null != esbmsg)
                {
                    t2.ReleaseT2Esbmsg(esbmsg);
                }
                if (null != unpacker)
                {
                    // 释放T2解包器接口
                    t2.ReleaseT2UnPacker(unpacker);
                }
            }
            return identity;
        }
        /// <summary>
        /// 获取Mac地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddress()
        {
            ManagementObjectSearcher query = new ManagementObjectSearcher("select * from win32_NetworkAdapterConfiguration");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (var mb in queryCollection)
            {
                if (mb["IPEnabled"].ToString() == "True")
                {
                    return mb["MacAddress"].ToString().Replace(':','-');
                }
            }
            return "00000";
        }
        /// <summary>
        /// 获取用户所有菜单
        /// </summary>
        /// <param name="o4Identity"></param>
        /// <returns></returns>
        public List<MenuInfo> GetUserAllMenus(HsIdentity o4Identity)
        {
            List<MenuInfo> userMenus = GetUserMenus(o4Identity);
            List<RoleInfo> roles = GetUserRoles((int) o4Identity.OperId);
            List<MenuInfo> roleMenus = GetRoleMenus(roles, o4Identity);
            List<MenuInfo> allMenus = UnionMenus(userMenus, roleMenus);
            return allMenus;
        } 
        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="o4Identity"></param>
        /// <returns></returns>
        private List<MenuInfo> GetUserMenus(HsIdentity o4Identity)
        {
            List<MenuInfo> menus = new List<MenuInfo>();
            IT2ESBMessage esbmsg = null;
            IT2Packer packer = null;
            IT2UnPacker unpacker = null;
            IT2UnPacker funcUnpacker = null;
            try
            {
                esbmsg = t2.GetT2Esbmsg();
                packer = t2.GetT2Packer(T2PackVersion.PACK_VERSION2);

                packer.BeginPack();
                packer.AddField("user_id", T2FieldType.TYPE_INT, (int)o4Identity.OperId);
                packer.AddField("subsys_id", T2FieldType.TYPE_INT, 1);
                packer.EndPack();

                esbmsg.Prepare(T2TagDef.REQUEST_PACKET, USER_MENU_QUERY);
                esbmsg.MsgBody = packer.GetPackBuf();
                t2.SynSendEsbMessage(ref esbmsg, 5000);

                if (esbmsg.ReturnCode != 0)
                {
                    if (esbmsg.ReturnCode == 1 || esbmsg.ReturnCode == -1)
                    {
                        throw new T2Exception(esbmsg.ErrorNo, esbmsg.ErrorInfo);
                    }
                }
                unpacker = t2.GetT2UnPacker(esbmsg.MsgBody);
                if (esbmsg.ReturnCode > 1) //说明有错
                {
                    int errorNo = unpacker.GetInt("error_no");
                    string errorInfo = unpacker.GetStr("error_info");
                    throw new T2Exception(errorNo, errorInfo);
                }
                else
                {
                    while (!unpacker.EOF)
                    {
                        MenuInfo menu = new MenuInfo();
                        menu.menu_id = unpacker.GetInt("menu_id");
                        menu.menu_parent_id = unpacker.GetInt("menu_parent_id");
                        menu.menu_order = unpacker.GetStr("menu_order");
                        menu.menu_name = unpacker.GetStr("menu_name");
                        menu.menu_plugin_type = Convert.ToChar(unpacker.GetChar("menu_plugin_type"));
                        menu.menu_plugin_name = unpacker.GetStr("menu_plugin_name");
                        menu.menu_plugin_dll = unpacker.GetStr("menu_plugin_dll");
                        menu.menu_is_leaf = Convert.ToChar(unpacker.GetChar("menu_is_leaf"));
                        menu.menu_icon_name = unpacker.GetStr("menu_icon_name");
                        menu.menu_shortcut = unpacker.GetStr("menu_shortcut");
                        menu.menu_multi_open = Convert.ToChar(unpacker.GetChar("menu_multi_open"));
                        menu.menu_auto_open = Convert.ToChar(unpacker.GetChar("menu_auto_open"));
                        menu.menu_allow_close = Convert.ToChar(unpacker.GetChar("menu_allow_close"));

                        List<FuncInfo> funcs = new List<FuncInfo>();
                        byte[] funcbuf = unpacker.GetRaw("i_func_list");
                        try
                        {
                            funcUnpacker = t2.GetT2UnPacker(funcbuf);
                            while (!funcUnpacker.EOF)
                            {
                                FuncInfo func = new FuncInfo();
                                int funcId = funcUnpacker.GetInt("func_id");
                                if (funcId > 0)
                                {
                                    func.func_id = funcId;
                                    func.func_name = funcUnpacker.GetStr("func_name");
                                    funcs.Add(func);
                                }
                                funcUnpacker.Next();

                            }
                        }finally
                        {
                            if (null != funcUnpacker)
                            {
                                t2.ReleaseT2UnPacker(funcUnpacker);
                            }
                        }
                        menu.i_func_list = funcs;
                        menus.Add(menu);
                        unpacker.Next();
                    }
                }
            }
            finally
            {
                //释放T2打包器接口
                if (null != packer)
                {
                    t2.ReleaseT2Packer(packer);
                }
                if (null != esbmsg)
                {
                    t2.ReleaseT2Esbmsg(esbmsg);
                }
                if (null != unpacker)
                {
                    // 释放T2解包器接口
                    t2.ReleaseT2UnPacker(unpacker);
                }
                
            }
            return menus;
        }
        /// <summary>
        /// 根据用户id获取角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<RoleInfo> GetUserRoles(int userId)
        {
            List<RoleInfo> roles = new List<RoleInfo>();
            IT2ESBMessage esbmsg = null;
            IT2Packer packer = null;
            IT2UnPacker unpacker = null;
            try
            {
                esbmsg = t2.GetT2Esbmsg();
                packer = t2.GetT2Packer(T2PackVersion.PACK_VERSION2);

                packer.BeginPack();
                packer.AddField("user_id", T2FieldType.TYPE_INT, userId);
                packer.EndPack();

                esbmsg.Prepare(T2TagDef.REQUEST_PACKET, USER_ROLE_QUERY);
                esbmsg.MsgBody = packer.GetPackBuf();
                t2.SynSendEsbMessage(ref esbmsg, 5000);

                if (esbmsg.ReturnCode != 0)
                {
                    if (esbmsg.ReturnCode == 1 || esbmsg.ReturnCode == -1)
                    {
                        throw new T2Exception(esbmsg.ErrorNo, esbmsg.ErrorInfo);
                    }
                }
                unpacker = t2.GetT2UnPacker(esbmsg.MsgBody);
                if (esbmsg.ReturnCode > 1) //说明有错
                {
                    int errorNo = unpacker.GetInt("error_no");
                    string errorInfo = unpacker.GetStr("error_info");
                    throw new T2Exception(errorNo, errorInfo);
                }
                else
                {
                    while (!unpacker.EOF)
                    {
                        RoleInfo r = new RoleInfo();
                        r.role_id = unpacker.GetInt("role_id");
                        r.role_name = unpacker.GetStr("role_name");
                        r.cmp_id = unpacker.GetInt("cmp_id");
                        r.remark = unpacker.GetStr("remark");
                        r.auth_flag = unpacker.GetInt("auth_flag");
                        r.auth_begin_date = unpacker.GetInt("auth_begin_date");
                        r.auth_begin_time = unpacker.GetInt("auth_begin_time");
                        r.auth_end_date = unpacker.GetInt("auth_end_date");
                        r.auth_end_time = unpacker.GetInt("auth_end_time");
                        roles.Add(r);
                        unpacker.Next();
                    }
                }
            }
            finally
            {
                //释放T2打包器接口
                if (null != packer)
                {
                    t2.ReleaseT2Packer(packer);
                }
                if (null != esbmsg)
                {
                    t2.ReleaseT2Esbmsg(esbmsg);
                }
                if (null != unpacker)
                {
                    // 释放T2解包器接口
                    t2.ReleaseT2UnPacker(unpacker);
                }
            }
            return roles;
        }
        /// <summary>
        /// 获取角色菜单权限
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="o4Identity"></param>
        /// <returns></returns>
        private List<MenuInfo> GetRoleMenus(List<RoleInfo> roles, HsIdentity o4Identity)
        {            
            List<MenuInfo> menus = new List<MenuInfo>();
            if (roles == null || roles.Count == 0)
            {
                return menus;
            }
            IT2ESBMessage esbmsg = null;
            IT2Packer packer = null;
            IT2UnPacker unpacker = null;
            IT2UnPacker funcUnpacker = null;
            try
            {
                esbmsg = t2.GetT2Esbmsg();
                packer = t2.GetT2Packer(T2PackVersion.PACK_VERSION2);

                packer.BeginPack();
                foreach (var roleInfo in roles)
                {
                    packer.AddField("role_id", T2FieldType.TYPE_INT, roleInfo.role_id);
                    packer.AddField("subsys_id", T2FieldType.TYPE_INT, o4Identity.SubSystemID);
                }
                packer.EndPack();

                esbmsg.Prepare(T2TagDef.REQUEST_PACKET, ROLE_MENU_QUERY);
                esbmsg.MsgBody = packer.GetPackBuf();
                t2.SynSendEsbMessage(ref esbmsg, 5000);

                if (esbmsg.ReturnCode != 0)
                {
                    if (esbmsg.ReturnCode == 1 || esbmsg.ReturnCode == -1)
                    {
                        throw new T2Exception(esbmsg.ErrorNo, esbmsg.ErrorInfo);
                    }
                }
                unpacker = t2.GetT2UnPacker(esbmsg.MsgBody);
                if (esbmsg.ReturnCode > 1) //说明有错
                {
                    int errorNo = unpacker.GetInt("error_no");
                    string errorInfo = unpacker.GetStr("error_info");
                    throw new T2Exception(errorNo, errorInfo);
                }
                else
                {
                    while (!unpacker.EOF)
                    {
                        MenuInfo menu = new MenuInfo();
                        menu.menu_id = unpacker.GetInt("menu_id");
                        menu.menu_parent_id = unpacker.GetInt("menu_parent_id");
                        menu.menu_order = unpacker.GetStr("menu_order");
                        menu.menu_name = unpacker.GetStr("menu_name");
                        menu.menu_plugin_type = Convert.ToChar(unpacker.GetChar("menu_plugin_type"));
                        menu.menu_plugin_name = unpacker.GetStr("menu_plugin_name");
                        menu.menu_plugin_dll = unpacker.GetStr("menu_plugin_dll");
                        menu.menu_is_leaf = Convert.ToChar(unpacker.GetChar("menu_is_leaf"));
                        menu.menu_icon_name = unpacker.GetStr("menu_icon_name");
                        menu.menu_shortcut = unpacker.GetStr("menu_shortcut");
                        menu.menu_multi_open = Convert.ToChar(unpacker.GetChar("menu_multi_open"));
                        menu.menu_auto_open = Convert.ToChar(unpacker.GetChar("menu_auto_open"));
                        menu.menu_allow_close = Convert.ToChar(unpacker.GetChar("menu_allow_close"));

                        List<FuncInfo> funcs = new List<FuncInfo>();
                        byte[] funcbuf = unpacker.GetRaw("i_func_list");
                        funcUnpacker = t2.GetT2UnPacker(funcbuf);
                        while (!funcUnpacker.EOF)
                        {
                            FuncInfo func = new FuncInfo();
                            int funcId = funcUnpacker.GetInt("func_id");
                            if (funcId > 0)
                            {
                                func.func_id = funcId;
                                func.func_name = funcUnpacker.GetStr("func_name");
                                funcs.Add(func);
                            }
                            funcUnpacker.Next();
                        }
                        menu.i_func_list = funcs;
                        menus.Add(menu);
                        unpacker.Next();
                    }
                }
            }
            finally
            {
                //释放T2打包器接口
                if (null != packer)
                {
                    t2.ReleaseT2Packer(packer);
                }
                if (null != esbmsg)
                {
                    t2.ReleaseT2Esbmsg(esbmsg);
                }
                if (null != unpacker)
                {
                    // 释放T2解包器接口
                    t2.ReleaseT2UnPacker(unpacker);
                }
                if(null != funcUnpacker)
                {
                    t2.ReleaseT2UnPacker(funcUnpacker);
                }
            }
            return menus;
        }
        /// <summary>
        /// 保存菜单到文件
        /// </summary>
        /// <param name="menuInfos"></param>
        public void SaveMenuToFile(List<MenuInfo> menuInfos)
        {
            Navigation menuZh_CN = new Navigation();
            string culture = "zh-CN";    
            menuZh_CN.Item = SetNavigation(menuInfos, 0, culture);
            menuZh_CN.ForeColor = "";
            menuZh_CN.BackgroundImage = "menu";
            this.SerilizerToXMlFile<Navigation>(menuZh_CN, MenuConfigXmlName.MainMenuStrip, culture);
        }
        /// <summary>
        /// 保存到菜单文件
        /// </summary>
        /// <param name="menuInfos"></param>
        /// <param name="parentId"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        private NavigationItem[] SetNavigation(List<MenuInfo> menuInfos, long parentId, string culture)
        {
            List<NavigationItem> itemsList = new List<NavigationItem>();
            List<MenuInfo> LevelMenuInfos = new List<MenuInfo>();
            foreach (MenuInfo menuInfo in menuInfos)
            {
                if (menuInfo.menu_parent_id == parentId)
                {
                    LevelMenuInfos.Add(menuInfo);
                }
            }
            LevelMenuInfos.Sort((x, y) => x.menu_id - y.menu_id);

            foreach (MenuInfo levelMenuInfo in LevelMenuInfos)
            {
                NavigationItem nvItem = new NavigationItem();
                if (levelMenuInfo.menu_is_leaf == '1')
                {
                    int cmbType = 2;
                    int.TryParse(levelMenuInfo.menu_plugin_type + "", out cmbType);
                    nvItem.CommandType = cmbType;
                    nvItem.CommandTypeSpecified = true;

                    StringBuilder sb = new StringBuilder();
                    List<FuncInfo> funcs = levelMenuInfo.i_func_list;
                    foreach (var funcInfo in funcs)
                    {
                        sb.Append(funcInfo.func_id).Append(",");
                    }
                    if(funcs.Count>0)
                    {
                        sb.Length = sb.Length - 1;
                    }
                    nvItem.FunctionID = sb.ToString();
                }
                nvItem.CommandID = levelMenuInfo.menu_id.ToString();
                nvItem.CommandIDSpecified = true;
                    nvItem.Label = levelMenuInfo.menu_name;
                nvItem.CommandName = levelMenuInfo.menu_plugin_name;
                nvItem.ShortcutKeys = levelMenuInfo.menu_shortcut;
                itemsList.Add(nvItem);
                nvItem.Item = SetNavigation(menuInfos, levelMenuInfo.menu_id, culture);
            }
            return itemsList.ToArray();
        }
        /// <summary>
        /// 序列化XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        /// <param name="culture"></param>
        private void SerilizerToXMlFile<T>(T obj, string fileName, string culture)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\System\" + culture + @"\" + fileName;
            using (TextWriter textWriter = new StreamWriter(path))
            {
                if (File.Exists(path))
                {
                    serializer.Serialize(textWriter, obj);
                }
            }
        }
        /// <summary>
        /// 关联菜单
        /// </summary>
        /// <param name="userMenus"></param>
        /// <param name="roleMenus"></param>
        /// <returns></returns>
        private List<MenuInfo> UnionMenus(List<MenuInfo> userMenus, List<MenuInfo> roleMenus)
        {
            if (userMenus == null && roleMenus == null)
            {
                return new List<MenuInfo>();
            }
            if (userMenus == null)
            {
                return roleMenus;
            }
            if (roleMenus == null)
            {
                return userMenus;
            }
            foreach (var roleMenu in roleMenus)
            {
                MenuInfo userMenu = userMenus.Find(uMenu => uMenu.menu_id == roleMenu.menu_id);
                if (userMenu == null)
                {
                    userMenus.Add(roleMenu);
                    continue;
                }
                foreach (var roleFunc in roleMenu.i_func_list)
                {
                    FuncInfo userFunc = userMenu.i_func_list.Find(uFunc => uFunc.func_id == roleFunc.func_id);
                    if (userFunc == null)
                    {
                        userMenu.i_func_list.Add(roleFunc);
                    }
                }
            }
            return userMenus;
        }
    }

    public class RoleInfo
    {
        public int role_id { get; set; }
        public string role_name { get; set; }
        public int cmp_id { get; set; }
        public string remark { get; set; }
        public int auth_flag { get; set; }
        public int auth_begin_date { get; set; }
        public int auth_begin_time { get; set; }
        public int auth_end_date { get; set; }
        public int auth_end_time { get; set; }
    }

    public class MenuInfo
    {
        public int menu_id { get; set; }
        public int menu_parent_id { get; set; }
        public string menu_order { get; set; }
        public string menu_name { get; set; }
        public char menu_plugin_type { get; set; }
        public string menu_plugin_name { get; set; }
        public string menu_plugin_dll { get; set; }
        public char menu_is_leaf { get; set; }
        public string menu_icon_name { get; set; }
        public string menu_shortcut { get; set; }
        public char menu_multi_open { get; set; }
        public char menu_auto_open { get; set; }
        public char menu_allow_close { get; set; }
        public List<FuncInfo> i_func_list { get; set; }
    }

    public class FuncInfo
    {
        public int func_id { get; set; }
        public string func_name { get; set; }
    }
}
