using System;
using System.Collections.Generic;
using System.Text;

namespace Hundsun.Framework.Client
{
    public interface IBusiness
    {
        void Add(object item);
        void Remove(object item);
        void Login(string args);
        void CommunicationSetting();
        string GetUserFolder();
    }
}
