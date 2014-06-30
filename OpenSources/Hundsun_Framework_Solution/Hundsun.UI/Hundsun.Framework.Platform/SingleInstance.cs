using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hundsun.Framework.Platform
{
    public class SingleInstance
    {
        #region 进程同步操作
        /// <summary>
        /// 声明进程同步基元
        /// </summary>
        public static Mutex NewMutex = null;
        /// <summary>
        /// 初始化同步基元
        /// </summary>
        /// <returns></returns>
        public static bool CreateMutex()
        {
            return CreateMutex(System.Reflection.Assembly.GetEntryAssembly().FullName + "118");
        }
        /// <summary>
        /// 重载函数实现自定义初始化
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CreateMutex(string name)
        {
            bool result = false;
            NewMutex = new Mutex(true, name, out result);
            return result;
        }
        /// <summary>
        /// 释放同步基元
        /// </summary>
        public static void ReleaseMutex()
        {
            if (NewMutex != null)
                NewMutex.Close();
        }
        #endregion
    }
}
