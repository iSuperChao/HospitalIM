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
    public class UrlAddressServer
    {
        #region 添加
        
        /// <summary>
        /// 将URL地址插入到数据库
        /// </summary>
        /// <param name="message">地址对象</param>
        /// <returns>受影响的行数</returns>
        public static int Insert(UrlAddress ruladdress)
        {
            string sql = string.Format(
                "insert into [UrlAddress](Owner,UrlType,AppOrder,UrlAddress,GroupId,UrlName) values({0}, '{1}', {2}, '{3}', {4},'{5}');select @@identity;",
                ruladdress.Owner,
                ruladdress.UrlType,
                ruladdress.AppOrder,
                ruladdress.Urladdress,
                ruladdress.GroupId,
                ruladdress.UrlName
                );
            object obj = SqlHelper.ExecuteScalar(sql);
            int id = -1;
            if (obj is decimal)
            {
                id = Convert.ToInt32(obj);
                if (!(ruladdress.UrlImage==null))
                   SqlHelper.ExecuteNonQuery("update [UrlAddress] set UrlImage=@FaceImg where id=" + id, ruladdress.UrlImage);
            }
            return id;
            }

        #endregion

        #region 查询

        /// <summary>
        /// 民警所有的地址列表
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <returns>消息集合</returns>
        public static List<UrlAddress> Select(int qqNumber)
        {
            string sql = "select * from [UrlAddress] where Owner=" + qqNumber;
            return SqlHelper.ExecuteList<UrlAddress>(sql);
        }

        public static DataTable SelectDataTable(int qqNumber)
        {
            string sql = "select * from [UrlAddress] where Owner=" + qqNumber;
            return SqlHelper.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 判断某行记录是否存在
        /// </summary>
        /// <param name="qqNumber"></param>
        /// <returns></returns>
        public static UrlAddress Select(UrlAddress ruladdress)
        {
            string sql = "select * from [UrlAddress] where Owner=" + ruladdress.Owner + " and AppOrder=" + ruladdress.AppOrder + " and UrlType='" + ruladdress.Urladdress + "'";
            return SqlHelper.ExecuteEntity<UrlAddress>(sql);
        }

        #endregion

        #region 修改
        
        /// <summary>
        /// 修改地址名
        /// </summary>
        /// <param name="messageId">地址ID</param>
        /// <returns>受引影的行数</returns>
        public static int Update(UrlAddress ruladdress)
        {
            // string sql = "select * from [UrlAddress] where Owner=" + ruladdress.Owner + " and AppOrder=" + ruladdress.AppOrder + " and UrlType='" + ruladdress.Urladdress + "'";
            //List<UrlAddress> l=new List<UrlAddress>();
            //l=SqlHelper.ExecuteList<UrlAddress>(sql);
            //if (ruladdress.id>0)
            //{
            if (ruladdress.UrlImage == null)
            {
                string sql = string.Format("update [UrlAddress] set UrlType ='{0}',AppOrder={1},UrlAddress='{2}',GroupId={3},UrlName='{4}' where id=" + ruladdress.id,
                ruladdress.UrlType,
                ruladdress.AppOrder,
                ruladdress.Urladdress,
                ruladdress.GroupId,
                ruladdress.UrlName
                );
                return SqlHelper.ExecuteNonQuery(sql);
            }
            else
            {
                string sql = string.Format("update [UrlAddress] set UrlType ='{0}',AppOrder={1},UrlAddress='{2}',GroupId={3},UrlName='{4}',UrlImage=@FaceImg where id=" + ruladdress.id,
                ruladdress.UrlType,
                ruladdress.AppOrder,
                ruladdress.Urladdress,
                ruladdress.GroupId,
                ruladdress.UrlName
                );
                return SqlHelper.ExecuteNonQuery(sql, ruladdress.UrlImage);
            }
            //}
            //else
            //{
            //    return Insert(ruladdress);
            //}
        }

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="messageId">地址ID</param>
        /// <returns>受引影的行数</returns>
        public static int delete(UrlAddress ruladdress)
        {
            string sql = "delete from [UrlAddress] where Id = " + ruladdress.id;
            return SqlHelper.ExecuteNonQuery(sql);
        }

        public static int deletebygroupid(int groupid)
        {
            string sql = "delete from [UrlAddress] where GroupId = " + groupid;
            return SqlHelper.ExecuteNonQuery(sql);
        }

        #endregion
    }
}
