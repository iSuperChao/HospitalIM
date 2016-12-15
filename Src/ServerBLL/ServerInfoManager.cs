using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerDAL;
using Common;
using Entity;

namespace ServerBLL
{
    public class ServerInfoManager
    {
        #region 添加
        
        /// <summary>
        /// 将服务器信息插入到数据库
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <returns>受影响的行数</returns>
        public static int Insert(ServerInfo serverinfo)
        {
            return ServerInfoServer.Insert(serverinfo);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 判断指定用户是否有离线消息
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <returns>消息集合</returns>
        public static List<ServerInfo> Select()
        {
            return ServerInfoServer.Select();
        }

        #endregion
    }
}
