using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerDAL;
using Common;
using Entity;

namespace ServerBLL
{
    public class UsersManager
    {
        #region 添加

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">用户提交的注册信息</param>
        /// <returns>注册的QQ号码</returns>
        public static int Insert(Users user)
        {
            return UsersServer.Insert(user);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">用户提交的注册信息</param>
        /// <returns>注册的QQ号码</returns>
        public static int Insert(int sendId, int recieveId,int groupid)
        {
            return UsersServer.Insert(sendId, recieveId,groupid);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <param name="qqPwd">QQ密码</param>
        /// <returns>登录的用户对象</returns>
        public static Users Select(int qqNumber, string qqPwd)
        {
            return UsersServer.Select(qqNumber, qqPwd);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="qqNumber"></param>
        /// <param name="qqPwd"></param>
        /// <returns></returns>
         public  static int Update(Users user)
        {
            return UsersServer.Update(user);
        }

        /// <summary>
        /// 查询指定用户的好友列表
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <returns>好友信息集合</returns>
        public static List<Users> Select(int qqNumber)
        {
            return UsersServer.Select(qqNumber);
        }

        /// <summary>
        /// 查询指定用户的被加为好友的列表
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <returns>好友信息集合</returns>
        public static List<Friends> SelectBeFriends(int qqNumber)
        {
            return UsersServer.SelectBeFriends(qqNumber);
        }



        /// <summary>
        /// 查找好友
        /// </summary>
        /// <param name="isAccurateSearch">是否为《精确查找》：true-精确查找；false-条件查找</param>
        /// <param name="qqNumber">QQ号码</param>
        /// <param name="nickName">昵称</param>
        /// <param name="ageIndex">选中年龄的索引</param>
        /// <param name="genderIndex">选中性别的索引</param>
        /// <returns>查找到的用户集合</returns>
        public static List<Users> Select( string sqlstr)
        {
            return UsersServer.Select(sqlstr);
        }

        /// <summary>
        /// 查询指定用户的个人信息
        /// </summary>
        /// <param name="qqNumber">要查询的QQ号</param>
        /// <param name="zero">0</param>
        /// <returns>用户对象</returns>
        public static Users Select(int qqNumber, int zero)
        {
            return UsersServer.Select(qqNumber, zero);
        }



        #endregion
    }
}
