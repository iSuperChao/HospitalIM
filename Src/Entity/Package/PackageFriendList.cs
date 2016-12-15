using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 好友列表包
    /// </summary>
    [Serializable]
    public class PackageFriendList : PackageBase
    {
        #region 构造函数
        
        public PackageFriendList()
        {
            base.HeadCode = EPackageHead.User_FriendList;
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 好友列表
        /// </summary>
        public List<Users> FriendList { get; set; }

        #endregion

    }
}
