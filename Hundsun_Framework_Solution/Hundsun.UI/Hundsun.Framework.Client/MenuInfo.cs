using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hundsun.Framework.Client
{
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
}
