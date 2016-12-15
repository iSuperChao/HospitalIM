using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;
using Entity;
using System.Configuration;

namespace ServerDAL
{
    public class UsersServer
    {
        #region 添加

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">用户提交的注册信息</param>
        /// <returns>注册的QQ号码</returns>
        public static int Insert(Users user)
        {
           // if (user.LastLoginTime == null || user.LastLoginTime.ToString() == "0001/1/1 0:00:00" || user.LastLoginTime.ToString() == "")
                user.LastLoginTime = DateTime.Now;
            int qqNumber = -1;
            string sql = string.Format(
                "insert into [Users](LoginPwd, NickName, Gender, Age, Name, StarId, BloodTypeId,Province,City,Country,Unit,Email,MobilePhone,ShortPhone,Emailusername,Emailpassword,LastLoginTime,LastLoginIp,LastLoginPort)" +
                " values('{0}', '{1}', '{2}', 2, '{4}', 2, 2,'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18});select @@identity;", user.LoginPwd,
                user.NickName, user.Gender, user.Age, user.Name, user.StarId, user.BloodTypeId, user.Province, user.City, user.Country, user.Unit, user.Email, user.MobilePhone, user.ShortPhone, 
                user.Emailusername,user.Emailpassword, user.LastLoginTime,user.LastLoginIp,user.LastLoginPort);            
            object obj = SqlHelper.ExecuteScalar(sql);
            if (obj is decimal)
            {
                qqNumber = Convert.ToInt32(obj);
                sql = "update [Users] set FaceImage=@FaceImg where id=" + qqNumber;
                SqlHelper.ExecuteNonQuery(sql,user.FaceImage);
            }
            return qqNumber;
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="sendId">发送请求人的ID</param>
        /// <param name="recieveId">接收请求人的ID</param>
        /// <returns>添加好友是否成功</returns>
        public static int Insert(int sendId, int recieveId,int groupid)
        {
            string sql = string.Format("insert into [Friends](HostId,FriendId,GroupId) values({0}, {1},{2})", sendId, recieveId,groupid);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="u"></param>
        public static int Update(Users u)
        {

            u.LastLoginTime=DateTime.Now;
            string sql = "update [Users] set LoginPwd='" + u.LoginPwd + "', FriendShipPolicyId=" + u.FriendShipPolicyId + ", NickName='" + u.NickName + "', FaceKey='" + u.FaceKey + "', Gender='" + u.Gender + "', Age=" + u.Age + ", Name='" + u.Name + "', StarId=" + u.StarId + ", BloodTypeId=" + u.BloodTypeId + ", Vip=" + u.Vip + ", Status=" + u.Status + ", Autograph='" + u.Autograph + "', Province='" + u.Province + "', City='" + u.City + "', Country='" + u.Country + "', Unit='" + u.Unit + "', Email='" + u.Email + "', MobilePhone='" + u.MobilePhone + "', ShortPhone='" + u.ShortPhone + "', Emailusername='" + u.Emailusername + "', LastLoginTime='" + u.LastLoginTime + "', LastLoginIp='" + u.LastLoginIp + "', LastLoginPort=" + u.LastLoginPort + ", Emailpassword='" + u.Emailpassword + "',FaceImage=@FaceImg where Id = " + u.Id;
            return SqlHelper.ExecuteNonQuery(sql,u.FaceImage);  
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
            string sql = string.Format(
                "select * from [Users] where ID = {0} and LoginPwd = '{1}'", qqNumber, qqPwd);
            return SqlHelper.ExecuteEntity<Users>(sql);
        }

        /// <summary>
        /// 查询指定用户的好友列表
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <returns>好友信息集合</returns>
        public static List<Users> Select(int qqNumber)
        {
            string sql = string.Format(
                "select * from [Users] where Id in (select distinct FriendId from [Friends] where HostId = {0})",
                qqNumber);
            return SqlHelper.ExecuteList<Users>(sql);
        }

        /// <summary>
        /// 查询指定用户被加为好友的列表
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <returns>加他好友信息集合</returns>
        public static List<Friends> SelectBeFriends(int qqNumber)
        {
            string sql = string.Format(
                "select  * from [Friends] where FriendId = {0}",
                qqNumber);
            return SqlHelper.ExecuteList<Friends>(sql);
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
        public static List<Users> Select(string sqlstr)
        {

            return SqlHelper.ExecuteList<Users>(sqlstr);
        }

        /// <summary>
        /// 查询指定用户的个人信息
        /// </summary>
        /// <param name="qqNumber">要查询的QQ号</param>
        /// <param name="zero">0</param>
        /// <returns>用户对象</returns>
        public static Users Select(int qqNumber, int zero)
        {
            string sql = "select * from [Users] where Id = " + qqNumber;
            return SqlHelper.ExecuteEntity<Users>(sql);
        }

        #endregion
    }
}
