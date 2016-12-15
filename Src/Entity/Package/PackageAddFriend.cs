using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 添加好友包
    /// </summary>
    [Serializable]
    public class PackageAddFriend : PackageBase
    {
        #region 构造函数
        
        public PackageAddFriend()
        {
            base.HeadCode = EPackageHead.Friend_Add;
        }

        public int groupid { get; set; }

        #endregion
    }
}
