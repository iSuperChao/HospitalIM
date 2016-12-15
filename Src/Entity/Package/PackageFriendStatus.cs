using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 好友状态包
    /// </summary>
    [Serializable]
    public class PackageFriendStatus : PackageBase
    {
        #region 构造函数

        public PackageFriendStatus()
        {
            base.HeadCode = EPackageHead.Friend_Status;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 状态编号 1表示上线 0表示下线
        /// </summary>
        public int StatusID { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>
        public int QQNum { get; set; }

        /// <summary>
        /// 联系人IP地址
        /// </summary>
        public String LastLoginIp { get; set; }
        /// <summary>
        /// 联系人端口
        /// </summary>
        public int LastLoginPort { get; set; }
        /// <summary>
        /// 联系人最后登陆时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        #endregion
    }
}
