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
    public class PackageFilesTrans : PackageBase
    {
        #region 构造函数

        public PackageFilesTrans()
        {
            base.HeadCode = EPackageHead.FilesTrans;
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 消息对象
        /// </summary>
       // public Messages Message { get; set; }
        public string SanderOrReveived { get; set; }
        public string FileOrPath { get; set; }
        public int SendId { get; set; }

        public string FileName { get; set; }
        public string PathName { get; set; }

        public long FileSize { get; set; }

        public string SenderIP { get; set; }
        public int SenderPort { get; set; }//接收消息的端口
        public int FilesTransPort { get; set; }///发送和接收文件的端口

        public string Error { get; set; }///不是好友，不能传输文件
        #endregion
    }
}
