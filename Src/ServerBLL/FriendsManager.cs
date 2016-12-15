using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerDAL;

namespace ServerBLL
{
    public class FriendsManager
    {
        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="sendId">发送人ID</param>
        /// <param name="recieveId">需要删除好友的ID</param>
        /// <returns>受影响的行数</returns>
        public static int Delete(int sendId, int recieveId)
        {
            return FriendsServer.Delete(sendId, recieveId);
        }
    }
}
