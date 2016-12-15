using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 用户状态包
    /// </summary>
    [Serializable]
    public class PackageUserStatus : PackageBase
    {
        #region 构造函数
        
        public PackageUserStatus()
        {
            base.HeadCode = EPackageHead.User_OffLine;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 状态编号
        /// </summary>
        public int StatusID { get; set; }

        #endregion
    }
}
