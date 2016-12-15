using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 消息包
    /// </summary>
    [Serializable]
    public class PackageMessage : PackageBase
    {
        #region 构造函数
        
        public PackageMessage()
        {
            base.HeadCode = EPackageHead.Message;
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 消息对象
        /// </summary>
        public Messages Message { get; set; }

        /// <summary>
        /// 发送的端口号
        /// </summary>
        public int FileSenderPort { get; set; }

        #endregion
    }
}
