using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 用户信息包
    /// </summary>
    [Serializable]
    public class PackageUserInfo : PackageBase
    {
        #region 构造函数
        
        public PackageUserInfo()
        {
            base.HeadCode = EPackageHead.User_Info;
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 用户对象
        /// </summary>
        public Users User { get; set; }

        #endregion
    }
}
