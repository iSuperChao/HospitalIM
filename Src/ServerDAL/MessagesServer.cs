using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;
using Entity;


namespace ServerDAL
{
    public class MessagesServer
    {
        #region 添加
        
        /// <summary>
        /// 将消息插入到数据库
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <returns>受影响的行数</returns>
        public static int Insert(Messages message)
        {
            string sql = string.Format(
                "insert into [Messages] values({0}, {1}, '{2}', {3}, {4}, {5})",
                message.SendId, message.RecieveId, message.Message, message.MessageTypeId,
                message.MessageState, "default");
            return SqlHelper.ExecuteNonQuery(sql);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 判断指定用户是否有离线消息
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <returns>消息集合</returns>
        public static List<Messages> Select(int qqNumber)
        {
            string sql = "select * from [Messages] where RecieveId=" + qqNumber + " and MessageState = 0";
            return SqlHelper.ExecuteList<Messages>(sql);
        }

        #endregion

        #region 修改
        
        /// <summary>
        /// 修改消息在数据库中的状态
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <returns>受引影的行数</returns>
        public static int Update(int messageId)
        {
            string sql = "update [Messages] set MessageState = 1 where Id = " + messageId;
            return SqlHelper.ExecuteNonQuery(sql);
        }

        #endregion
    }
}
