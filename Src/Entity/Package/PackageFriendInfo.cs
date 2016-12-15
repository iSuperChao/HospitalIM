using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 好友信息
    /// </summary>
    [Serializable]
    public class PackageFriendInfo : PackageBase
    {
        #region 构造函数
        
        public PackageFriendInfo()
        {
            base.HeadCode = EPackageHead.Friend_Info;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 用户对象
        /// </summary>
        public Users User { get; set; }

        /// <summary>
        /// 要查询信息好友的ID
        /// </summary>
        public int UserId { get; set; }

        #endregion
    }
}
