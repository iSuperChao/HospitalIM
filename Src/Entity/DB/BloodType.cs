using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 血型
    /// </summary>
    [Serializable]
    public class BloodType
    {
        #region 属性
        
        public int Id { get; set; }
        public String BloodTypeName { get; set; }

        #endregion
    }
}
