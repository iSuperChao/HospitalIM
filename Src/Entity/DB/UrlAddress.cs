using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    /// <summary>
    /// RUL链接地址
    /// </summary>
    [Serializable]
    public class UrlAddress
    {
        #region 属性
        public int id { get; set; }
        public int Owner { get; set; }
        public string UrlType { get; set; } //链接类型：app主界面那几个常用的按钮,menu菜单里的连接
        public int AppOrder { get; set; }    
        public string Urladdress { get; set; }
        public string UrlName { get; set; }
        public int GroupId { get; set; }
        public byte[] UrlImage { get; set; }
        #endregion
    }
}
