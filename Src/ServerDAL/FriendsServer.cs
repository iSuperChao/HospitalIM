using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace ServerDAL
{
    public class FriendsServer
    {
        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="sendId">发送人ID</param>
        /// <param name="recieveId">需要删除好友的ID</param>
        /// <returns>受影响的行数</returns>
        public static int Delete(int sendId, int recieveId)
        {
            string sql = string.Format("delete from [Friends] where HostId = {0} and FriendId = {1}", sendId, recieveId);
            return SqlHelper.ExecuteNonQuery(sql);
        }
    }
}
