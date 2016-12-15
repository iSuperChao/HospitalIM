using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 星座
    /// </summary>
    [Serializable]
    public class Friends
    {
        #region 属性

        public int id { get; set; }
        public int HostId { get; set; }
        public int FriendId { get; set; }

        #endregion

    }
}
