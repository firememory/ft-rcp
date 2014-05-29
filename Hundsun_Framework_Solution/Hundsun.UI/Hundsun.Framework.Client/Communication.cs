using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Threading;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Management;
using Hundsun.Framework.Entity;
using Hundsun.Framework.AddIn.WinForms;
using Hundsun.Framework.Communication;

namespace Hundsun.Framework.Client
{
    /// <summary>
    /// 实现通信总接口
    /// </summary>
    public class Communication : ICommunication
    {
        public Communication()
        {
            // 实例工厂类
            factory = new DBFactory();
        }
        /// <summary>
        /// 接口工厂
        /// </summary>
        private IDBFactory factory = null;
        public IDBFactory DBFactory
        {
            get
            {
                return factory;
            }
        }
    }
}
