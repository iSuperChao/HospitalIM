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
    public class Star
    {
        #region 属性
        
        public int Id { get; set; }
        public String StarName { get; set; }

        #endregion

    }
}
