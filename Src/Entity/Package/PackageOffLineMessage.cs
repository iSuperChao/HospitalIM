using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 离线消息包
    /// </summary>
    [Serializable]
    public class PackageOffLineMessage : PackageBase
    {
        public PackageOffLineMessage()
        {
            base.HeadCode = EPackageHead.Message_OffLine;
        }
    }
}
