using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Serializable]
    public class Users
    {
        #region 属性
        
        public int Id { get; set; }
        public String LoginPwd { get; set; }
        public int FriendShipPolicyId { get; set; }
        public String NickName { get; set; }
        public String FaceKey { get; set; }
        public String Gender { get; set; }
        public int Age { get; set; }
        public String Name { get; set; }
        public int StarId { get; set; }
        public int BloodTypeId { get; set; }
        public int Vip { get; set; }
        public int Status { get; set; }
        public String Autograph { get; set; }
        public String Province { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String Unit { get; set; }
        public String Email { get; set; }
        public String MobilePhone { get; set; }
        public String ShortPhone { get; set; }
        public String Emailusername { get; set; }
        public String Emailpassword { get; set; }
        public byte[] FaceImage { get; set; }
        public DateTime LastLoginTime { get; set; }
        public String LastLoginIp { get; set; }
        public int LastLoginPort { get; set; }        
            
         
           
        #endregion

        #region 重写方法

        /// <summary>
        /// 重写父类的Equals方法，判断两个值的ID
        /// </summary>
        /// <param name="obj">User对象</param>
        /// <returns>两个User对象的ID是否相等，相等返回true,不相等返回false</returns>
        public override bool Equals(object obj)
        {
            if (obj is Users)
            {
                Users user = obj as Users;
                if (user.Id == this.Id)
                    return true;
            }
            return false;
        }
        
        #endregion
    }
}
