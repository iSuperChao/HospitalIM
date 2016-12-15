using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 获取服务器列表包
    /// </summary>
    [Serializable]
    public class PackageServerList : PackageBase
    {
        #region 构造函数

        public PackageServerList()
        {
            base.HeadCode = EPackageHead.Server_InfoList;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public List<ServerInfo> ServerList { get; set; }

        #endregion
    }
}
