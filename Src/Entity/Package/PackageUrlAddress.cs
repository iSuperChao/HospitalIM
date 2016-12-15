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
    public class PackageUrlAddress : PackageBase
    {
        #region 构造函数

        public PackageUrlAddress()
        {
            base.HeadCode = EPackageHead.UrlAddress_Manager;
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 消息对象
        /// </summary>
        public UrlAddress urladdress { get; set; }
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
