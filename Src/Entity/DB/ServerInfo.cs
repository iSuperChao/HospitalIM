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
    public class ServerInfo
    {
        #region 属性

        public int id { get; set; }
        public string _serverIP { get; set; }
        public string _serverPort { get; set; }
        public string _notice { get; set; }
        public Boolean _avlid { get; set; }
        public int _version { get; set; }

        #endregion

    }
}
