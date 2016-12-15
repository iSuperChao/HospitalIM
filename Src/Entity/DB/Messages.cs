using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 消息
    /// </summary>
    [Serializable]
    public class Messages
    {
        #region 属性

        public int Id { get; set; }
        public int SendId { get; set; }
        public int RecieveId { get; set; }
        public string Message { get; set; }
        public int MessageTypeId { get; set; }
        public int MessageState { get; set; }
        public DateTime MessageTime { get; set; }



        #endregion
    }
}
