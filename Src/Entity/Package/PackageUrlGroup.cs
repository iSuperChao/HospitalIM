using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// URL组管理包
    /// </summary>
    [Serializable]
    public class PackageUrlGroup : PackageBase
    {
        #region 构造函数

        public PackageUrlGroup()
        {
            base.HeadCode = EPackageHead.UrlGroup_Manager;
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 消息对象
        /// </summary>
        public UrlGroup urlgroup { get; set; }
        /// <summary>
        /// insert,update,delete
        /// </summary>
        public string operate { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool isSuccess { get; set; }

        #endregion
    }
}
