using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 登录包
    /// </summary>
    [Serializable]
    public class PackageLogin : PackageBase
    {
        #region 构造函数
        
        public PackageLogin()
        {
            base.HeadCode = EPackageHead.User_Login;
        }

        #endregion

        #region 属性

        /// <summary>
        /// QQ号码
        /// </summary>
        public int QQNumber { get; set; }
        /// <summary>
        /// QQ密码
        /// </summary>
        public string QQPwd { get; set; }

        #endregion
    }
}
