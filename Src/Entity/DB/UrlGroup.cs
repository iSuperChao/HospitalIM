using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    /// <summary>
    /// URL链接分组
    /// </summary>
    [Serializable]
    public class UrlGroup
    {
        #region 属性

        public int ID { get; set; }
        public String GroupName { get; set; } //分组名称
        public int Owner { get; set; }        //所有者

        #endregion

    }
}
