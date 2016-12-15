using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data;

namespace Entity
{
    /// <summary>
    /// URL组管理包
    /// </summary>
    [Serializable]
    public class PackageGetUrlTable : PackageBase
    {
        #region 构造函数

        public PackageGetUrlTable()
        {
            base.HeadCode = EPackageHead.GetUrlAndAddressTable;
        }

        #endregion

        #region 属性

        /// <summary>
        /// URL分组表
        /// </summary>
        public  DataTable UrlGroup { get; set; }
        /// <summary>
        /// URL地址表
        /// </summary>
        public DataTable UrlAddress { get; set; }

        #endregion
    }
}
