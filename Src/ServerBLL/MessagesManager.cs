using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerDAL;
using Common;
using Entity;

namespace ServerBLL
{
    public class MessagesManager
    {
        #region 添加
        
        /// <summary>
        /// 将消息插入到数据库
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <returns>受影响的行数</returns>
        public static int Insert(Messages message)
        {
            return MessagesServer.Insert(message);
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
            return MessagesServer.Select(qqNumber);
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
            return MessagesServer.Update(messageId);
        }

        #endregion
    }
}
