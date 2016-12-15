using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;
using Entity;


namespace ServerDAL
{
    public class ServerInfoServer
    {
        #region 添加
        
        /// <summary>
        /// 将服务器信息插入到数据库
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <returns>受影响的行数</returns>
        public static int Insert(ServerInfo message)
        {
          //  string sql = string.Format(
         //       "insert into [ServerInfo] values({0}, {1}, '{2}', {3}, {4}, {5})",
          //      message.SendId, message.RecieveId, message.Message, message.MessageTypeId,
          //      message.MessageState, "default");
            return SqlHelper.ExecuteNonQuery("");
        }

        #endregion

        #region 查询

        /// <summary>
        /// 选出所有有效的服务器信息
        /// </summary>
        /// <param name="qqNumber"></param>
        /// <returns>服务器列表</returns>
        public static List<ServerInfo> Select()
        {
            string sql = "select * from [ServerInfo] where _avlid=1";
            return SqlHelper.ExecuteList<ServerInfo>(sql);
        }

        #endregion
    }
}
