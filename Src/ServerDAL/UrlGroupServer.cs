using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;
using Entity;
using System.Data;


namespace ServerDAL
{
    public class UrlGroupServer
    {
        #region 添加
        
        /// <summary>
        /// 将组名插入到数据库
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <returns>受影响的行数</returns>
        public static int Insert(UrlGroup urlgroup)
        {
            string sql = string.Format(
                "insert into [UrlGroup] values({0}, '{1}')",
                urlgroup.Owner, urlgroup.GroupName);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 民警所有的组名列表
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <returns>消息集合</returns>
        public static List<UrlGroup> Select(int qqNumber)
        {
            string sql = "select * from [UrlGroup] where Owner=" + qqNumber;
            return SqlHelper.ExecuteList<UrlGroup>(sql);
        }

        public static DataTable SelectDataTable(int qqNumber)
        {
            string sql = "select * from [UrlGroup] where Owner=" + qqNumber;
            return SqlHelper.ExecuteDataTable(sql);
        }

        #endregion

        #region 修改
        
        /// <summary>
        /// 修改组名
        /// </summary>
        /// <param name="messageId">组ID</param>
        /// <returns>受引影的行数</returns>
        public static int Update(UrlGroup urlgroup)
        {
            string sql = "update [UrlGroup] set GroupName ='" + urlgroup.GroupName + "' where Id = " + urlgroup.ID;
            return SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除组名
        /// </summary>
        /// <param name="messageId">组ID</param>
        /// <returns>受引影的行数</returns>
        public static int Delete(UrlGroup urlgroup)
        {
            string sql = "delete from [UrlGroup] where Id = " + urlgroup.ID;
            return SqlHelper.ExecuteNonQuery(sql);
        }

        #endregion
    }
}
