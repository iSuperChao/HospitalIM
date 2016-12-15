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
    public class PackageMonitor : PackageBase
    {
        #region 构造函数

        public PackageMonitor()
        {
            base.HeadCode = EPackageHead.Monitor;
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 消息对象
        /// </summary>
       // public Messages Message { get; set; }
        /// <summary>
        /// ctom=受控端向控制端发消息
        /// mtoc=控制端向受控端发消息
        /// </summary>
        public string MorC { get; set; }
        /// <summary>
        /// ask＝受控端请求控制端
        /// ok=接受请求，开始控制
        /// no=拒绝请求
        /// end=结束控制
        /// nofriend=不是好友不能远程
        /// </summary>
        public string Typemsg { get; set; }
        /// <summary>
        /// 被控端端口
        /// </summary>
        public int CPort { get; set; }

        public string SenderIP { get; set; }
        public int SenderPort { get; set; }

        #endregion
    }
}
