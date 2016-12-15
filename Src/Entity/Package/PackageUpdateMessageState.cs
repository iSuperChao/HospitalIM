using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 修改消息状态的包
    /// </summary>
    [Serializable]
    public class PackageUpdateMessageState : PackageBase
    {

        #region 构造函数
        
        public PackageUpdateMessageState()
        {
            base.HeadCode = EPackageHead.Message_Update_State;
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 消息ID
        /// </summary>
        public int MessageId { get; set; }

        #endregion

    }
}
