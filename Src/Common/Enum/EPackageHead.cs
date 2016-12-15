using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// 基包
    /// </summary>
    [Serializable]
    public enum EPackageHead
    {
        /// <summary>
        /// 服务器列表
        /// </summary>
        Server_InfoList,
        /// <summary>
        /// 用户注册
        /// </summary>
        User_Register,
        /// <summary>
        /// 用户注销
        /// </summary>
        User_Logout,
        /// <summary>
        /// 用户登录
        /// </summary>
        User_Login,
        /// <summary>
        /// 用户离线
        /// </summary>
        User_OffLine,
        /// <summary>
        /// 登录成功
        /// </summary>
        User_Login_Success,
        /// <summary>
        /// 登录失败
        /// </summary>
        User_Login_Failure,
        /// <summary>
        /// 已经登录
        /// </summary>
        User_Login_Exist,
        /// <summary>
        /// 用户信息
        /// </summary>
        User_Info,
        /// <summary>
        /// 用户状态
        /// </summary>
        User_Status,
        /// <summary>
        /// 好友状态
        /// </summary>
        Friend_Status,
        /// <summary>
        /// 信息
        /// </summary>
        Message,
        /// <summary>
        /// 离线消息
        /// </summary>
        Message_OffLine,
        /// <summary>
        /// 消息状态
        /// </summary>
        Message_Update_State,
        /// <summary>
        /// 传送文件，发送端
        /// </summary>
        FilesTrans,
        /// <summary>
        /// 传送文件成功完成，发送端
        /// </summary>
        FilesTrans_finished,
        /// <summary>
        /// 远程协助
        /// </summary>
        Monitor,
        /// <summary>
        /// 好友列表
        /// </summary>
        User_FriendList,
        /// <summary>
        /// 查询好友
        /// </summary>
        Friend_Sreach,
        /// <summary>
        /// 好友信息
        /// </summary>
        Friend_Info,
        /// <summary>
        /// 加为好友
        /// </summary>
        Friend_Add,
        /// <summary>
        /// 添加好友成功
        /// </summary>
        Friend_Add_Success,
        /// <summary>
        /// 添加好友失败
        /// </summary>
        Friend_Add_Failure,
        /// <summary>
        /// 删除好友
        /// </summary>
        Friend_Delete,
        /// <summary>
        /// 删除好友成功
        /// </summary>
        Friend_Delete_Success,
        /// <summary>
        /// 删除好友失败
        /// </summary>
        Friend_Delete_Failure,
        /// <summary>
        /// URL组名管理
        /// </summary>
        UrlGroup_Manager,
        /// <summary>
        /// URL地址管理
        /// </summary>
        UrlAddress_Manager,
        /// <summary>
        /// 获取组名和地址并放在表里
        /// </summary>
        GetUrlAndAddressTable,
        /// <summary>
        /// 默认
        /// </summary>
        Normal
    }
}
