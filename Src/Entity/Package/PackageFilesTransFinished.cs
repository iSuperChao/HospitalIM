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
    public class PackageFilesTransFinished : PackageBase
    {
        #region 构造函数

        public PackageFilesTransFinished()
        {
            base.HeadCode = EPackageHead.FilesTrans_finished;
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 消息对象
        /// </summary>
       // public Messages Message { get; set; }

        public int SendId { get; set; }

        public string FileName { get; set; }
        public Boolean Success { get; set; }

        #endregion
    }
}
