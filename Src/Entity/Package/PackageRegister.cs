using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 注册包
    /// </summary>
    [Serializable]
    public class PackageRegister : PackageBase
    {
        #region 构造函数
        
        public PackageRegister()
        {
            base.HeadCode = EPackageHead.User_Register;
        }
        #endregion

        #region 属性
        
        /// <summary>
        /// 用户信息
        /// </summary>
        public Users User { get; set; }

        /// <summary>
        /// 向用户回应的QQ号码
        /// </summary>
        public int QQNumber { get; set; }

        /// <summary>
        /// 操作类型：Modif ：修改； Regiester：删除。
        /// </summary>
        public string OperateTypeName { get; set; }

        /// <summary>
        /// 向用户回应的修改记录行数，大于0表示修改成功
        /// </summary>
        public int ReturnLows { get; set; }

        #endregion

    }
}
