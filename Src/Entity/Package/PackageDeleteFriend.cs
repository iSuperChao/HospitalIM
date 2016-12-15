using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 删除好友包
    /// </summary>
    [Serializable]
    public class PackageDeleteFriend : PackageBase
    {
        #region 构造函数
        
        public PackageDeleteFriend()
        {
            base.HeadCode = EPackageHead.Friend_Delete;
        }

        #endregion
    }
}
