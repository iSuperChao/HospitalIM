using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Entity
{
    /// <summary>
    /// 查找好友包
    /// </summary>
    [Serializable]
    public class PackageSearchUser : PackageBase
    {
        #region 枚举
        
        /// <summary>
        /// 查找类型
        /// </summary>
        public enum ESearchType
        { 
            /// <summary>
            /// 好友
            /// </summary>
            Friend,
            /// <summary>
            /// 群组
            /// </summary>
            Group
        }

        #endregion

        #region 构造函数

        public PackageSearchUser()
        {
            base.HeadCode = EPackageHead.Friend_Sreach;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 查找类型
        /// </summary>
        public ESearchType SearchType { get; set; }

        /// <summary>
        /// 查找的SQL语句
        /// </summary>
        public string SearchSQLString { get; set; }

        /// <summary>
        /// 查询的用户列表
        /// </summary>
        public List<Users> UsersList { get; set; }

        /// <summary>
        /// 是否是精确查找
        /// 如果为true：精确查找，如果为false：按条件查找
        /// </summary>
        public bool IsAccurateSearch { get; set; }

        /// <summary>
        /// 需要查找的QQ号码
        /// </summary>
        public int QQNumber { get; set; }

        /// <summary>
        /// 需要查找的用户昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 需要查找的用户年龄，在控件中的索引
        /// </summary>
        public int AgeIndex { get; set; }

        /// <summary>
        /// 需要查找的用户性别，在控件中的索引
        /// </summary>
        public int GenderIndex { get; set; }

        #endregion

    }
}
