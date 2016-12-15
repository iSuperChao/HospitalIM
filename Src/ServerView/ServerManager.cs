using System;
using System.Collections.Generic;
using System.Text;
using Common;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Entity;
using ServerBLL;
using ServerDAL;
using System.Configuration;

namespace ServerView
{
    /// <summary>
    /// 服务端控制
    /// </summary>
    class ServerManager
    {
        #region 变量

        /// <summary>
        /// 通信类
        /// </summary>
        private SockTCP _server = new SockTCP();
        /// <summary>
        /// 服务端IP地址
        /// </summary>
        private IPAddress _serverIP = IPAddress.Parse(ConfigurationManager.AppSettings["serverIP"].ToString());
        /// <summary>
        /// 服务端端口号
        /// </summary>
        private int _masterPort = Convert.ToInt32(ConfigurationManager.AppSettings["serverPort"].ToString());
        /// <summary>
        /// 用户通信字典集合
        /// </summary>
        public Dictionary<int, Socket> _userSocket = new Dictionary<int, Socket>();

        #endregion

        #region 事件 && 委托
        /// <summary>
        /// 用户上线
        /// </summary>
        public event Action UserOnLineEvent;
        /// <summary>
        /// 用户下线
        /// </summary>
        public event Action UserOffLineEvent;
        /// <summary>
        /// 更新日志
        /// </summary>
        public event Action<string> UpdateLogEvent;
        #endregion

        #region 构造函数

        public ServerManager()
        {
            this._server.TCPDataArrival += new SockTCP.TCPDataArrivalEventHandler(_server_TCPDataArrival);
            //开始监听
            this._server.Listen(this._serverIP, _masterPort);
        }
        #endregion

        #region 方法

        /// <summary>
        /// 处理客户端发送过来的请求
        /// </summary>
        /// <param name="args"></param>
        void _server_TCPDataArrival(TCPDataEventArgs args)
        {
            switch (args.Code.HeadCode)
            {
                case EPackageHead.User_Register://--------------------------用户注册--------------------------
                    if (args.Code is PackageRegister)
                    {
                        try
                        {
                            PackageRegister packageRegister = args.Code as PackageRegister;
                            if (packageRegister.OperateTypeName == "Regiester")
                                packageRegister.QQNumber = UsersManager.Insert(packageRegister.User);
                            if (packageRegister.OperateTypeName == "Modif")
                                packageRegister.ReturnLows = UsersManager.Update(packageRegister.User);
                            this._server.ServerSendData(args.Socket, packageRegister);
                        }
                        catch (Exception e) { UpdateLogEvent(e.ToString()); }
                    }
                    break;
                case EPackageHead.User_Login://--------------------------用户登录--------------------------
                    if (args.Code is PackageLogin)
                    {
                        PackageLogin packageLogin = args.Code as PackageLogin;
                        Users user = UsersManager.Select(packageLogin.QQNumber, packageLogin.QQPwd);
                        PackageUserInfo packageUserInfo = new PackageUserInfo();
                        if (user != null)//登录成功
                        {

                            packageUserInfo.HeadCode = EPackageHead.User_Login_Success;
                            packageUserInfo.User = user;
                            //判断该用户当前是否已经在线
                            if (!this._userSocket.ContainsKey(packageLogin.QQNumber))
                            {
                                //将该用户的QQ号和Socket放到集合中
                                this._userSocket.Add(packageLogin.QQNumber, args.Socket);
                            }
                            else//当前用户在线，给该用户发送离线通知，并将该用户从集合中移除再添加
                            {
                                PackageUserOffLine packageUserOffLine = new PackageUserOffLine();
                                packageUserOffLine.HeadCode = EPackageHead.User_OffLine;
                                this._server.ServerSendData(_userSocket[packageLogin.QQNumber], packageUserOffLine);
                                _userSocket.Remove(packageLogin.QQNumber);
                                this._userSocket.Add(packageLogin.QQNumber, args.Socket);
                            }
                            this.UserOnLineEvent();
                            ///更新数据库的用户状态及客户端IP，端口
                            user.Status = 1;
                            IPAddress ip= (args.Socket.RemoteEndPoint as IPEndPoint).Address;
                            int port = (args.Socket.RemoteEndPoint as IPEndPoint).Port;
                            user.LastLoginTime = DateTime.Now;
                            user.LastLoginIp = ip.ToString();
                            user.LastLoginPort = port;
                            UsersManager.Update(user);
                            //通知加该人为好友的人上线
                            List<Friends> us = new List<Friends>();
                            us = UsersManager.SelectBeFriends(packageLogin.QQNumber);
                            foreach (Friends u in us)
                            {
                                if (this._userSocket.ContainsKey(u.HostId))
                                {
                                    PackageFriendStatus packagefriendstatus = new PackageFriendStatus();
                                    packagefriendstatus.HeadCode = EPackageHead.Friend_Status;
                                    packagefriendstatus.QQNum = u.FriendId;
                                    packagefriendstatus.StatusID = 1;
                                    packagefriendstatus.LastLoginIp = ip.ToString();
                                    packagefriendstatus.LastLoginPort = port;
                                    this._server.ServerSendData(this._userSocket[u.HostId], packagefriendstatus);
                                }
                            }
                            this.UpdateLogEvent(string.Format("用户上线通知：[ {0}:{1},{2}({3}) ] --上线了--", ip, port, user.Name, user.Id));
                        }
                        else//登录失败
                        {
                            packageUserInfo.HeadCode = EPackageHead.User_Login_Failure;
                        }
                        this._server.ServerSendData(args.Socket, packageUserInfo);
                        ///更新客户端服务器列表信息
                        PackageServerList packageserverlist = new PackageServerList();
                        packageserverlist.HeadCode = EPackageHead.Server_InfoList;
                        packageserverlist.ServerList = ServerInfoManager.Select();
                        if (packageserverlist.ServerList.Count > 0)
                            this._server.ServerSendData(args.Socket, packageserverlist);

                    }
                    break;
                case EPackageHead.User_FriendList://--------------------------获取好友列表--------------------------
                    if (args.Code is PackageFriendList)
                    {
                        PackageFriendList packageFriendList = args.Code as PackageFriendList;
                        packageFriendList.FriendList = UsersManager.Select(packageFriendList.SendId);
                        this._server.ServerSendData(args.Socket, packageFriendList);

                        PackageGetUrlTable packagegeturltable = new PackageGetUrlTable();
                        packagegeturltable.HeadCode = EPackageHead.GetUrlAndAddressTable;
                        packagegeturltable.UrlGroup = UrlGroupServer.SelectDataTable(packageFriendList.SendId);
                        packagegeturltable.UrlAddress = UrlAddressServer.SelectDataTable(packageFriendList.SendId);
                        this._server.ServerSendData(args.Socket, packagegeturltable);
                    }
                    break;
                case EPackageHead.Message_OffLine://--------------------------获取离线消息--------------------------
                    if (args.Code is PackageOffLineMessage)
                    {
                        PackageOffLineMessage packageOffLineMessage = args.Code as PackageOffLineMessage;
                        //判断指定用户是否有离线消息
                        List<Messages> msgList = MessagesManager.Select(packageOffLineMessage.SendId);
                        if (msgList != null && msgList.Count > 0)
                        {
                            PackageMessage packageMessage = new PackageMessage();
                            foreach (Messages message in msgList)
                            {
                                packageMessage.Message = message;
                                this._server.ServerSendData(args.Socket, packageMessage);
                            }
                        }
                    }
                    break;
                case EPackageHead.Friend_Sreach://--------------------------查找好友--------------------------
                    if (args.Code is PackageSearchUser)
                    {
                        PackageSearchUser packageSearchUser = args.Code as PackageSearchUser;
                        packageSearchUser.UsersList = UsersManager.Select(packageSearchUser.SearchSQLString);
                        this._server.ServerSendData(args.Socket, packageSearchUser);
                    }
                    break;
                case EPackageHead.Friend_Add://--------------------------添加好友--------------------------
                    if (args.Code is PackageAddFriend)
                    {
                        try
                        {
                            PackageAddFriend packageAddFriend = args.Code as PackageAddFriend;
                            int result = UsersManager.Insert(packageAddFriend.SendId, packageAddFriend.RecieveId, packageAddFriend.groupid);
                            if (result < 1)//添加好友失败
                            {
                                packageAddFriend.HeadCode = EPackageHead.Friend_Add_Failure;
                            }
                            else//添加好友成功
                            {
                                PackageMessage packageMessage = new PackageMessage();
                                packageMessage.Message = new Messages()
                                {
                                    SendId = packageAddFriend.SendId,
                                    RecieveId = packageAddFriend.RecieveId,
                                    Message = "",
                                    MessageState = 0,
                                    MessageTime = DateTime.Now,
                                    MessageTypeId = 2
                                };
                                //向指定用户发送一个添加好友的系统消息
                                if (this._userSocket.ContainsKey(packageAddFriend.RecieveId))
                                {
                                    Socket socket = this._userSocket[packageAddFriend.RecieveId];
                                    this._server.ServerSendData(socket, packageMessage);
                                }
                                else
                                {
                                    MessagesManager.Insert(packageMessage.Message);
                                }
                                packageAddFriend.HeadCode = EPackageHead.Friend_Add_Success;
                            }
                            this._server.ServerSendData(args.Socket, packageAddFriend);
                        }
                        catch (Exception e) { UpdateLogEvent(e.ToString()); }
                    }
                    break;
                case EPackageHead.Friend_Delete://--------------------------删除好友--------------------------
                    if (args.Code is PackageDeleteFriend)
                    {
                        PackageDeleteFriend packageDeleteFriend = args.Code as PackageDeleteFriend;
                        int count = FriendsManager.Delete(packageDeleteFriend.SendId, packageDeleteFriend.RecieveId);
                        if (count < 1)//删除失败
                        {
                            packageDeleteFriend.HeadCode = EPackageHead.Friend_Delete_Failure;
                        }
                        else//删除成功
                        {
                            packageDeleteFriend.HeadCode = EPackageHead.Friend_Delete_Success;
                        }
                        this._server.ServerSendData(args.Socket, packageDeleteFriend);
                    }
                    break;
                case EPackageHead.Message://--------------------------发送消息--------------------------
                    if (args.Code is PackageMessage)
                    {
                        PackageMessage packageMessage = args.Code as PackageMessage;
                        //判断指定用户是否在线 
                        if (this._userSocket.ContainsKey(packageMessage.Message.RecieveId))//在线
                        {
                            //获取该用户的Socket，向该用户发送消息
                            Socket socket = this._userSocket[packageMessage.Message.RecieveId];
                            this._server.ServerSendData(socket, packageMessage);                            
                        }
                        else//如果指定用户不在线，则把消息插入到数据库
                        {
                            MessagesManager.Insert(packageMessage.Message);
                        }
                    }
                    break;
                case EPackageHead.Friend_Info://--------------------------查询好友信息--------------------------
                    if (args.Code is PackageFriendInfo)
                    {
                        PackageFriendInfo packageFriendInfo = args.Code as PackageFriendInfo;
                        packageFriendInfo.User = UsersManager.Select(packageFriendInfo.UserId, 0);
                        this._server.ServerSendData(args.Socket, packageFriendInfo);
                    }
                    break;
                case EPackageHead.Message_Update_State://--------------------------修改消息状态--------------------------  
                    if (args.Code is PackageUpdateMessageState)
                    {
                        PackageUpdateMessageState packageUpdateMessageState = args.Code as PackageUpdateMessageState;
                        MessagesManager.Update(packageUpdateMessageState.MessageId);
                    }
                    break;
                case EPackageHead.UrlAddress_Manager://--------------------------链接地址管理--------------------------  
                    if (args.Code is PackageUrlAddress)
                    {
                        PackageUrlAddress packageurladdress = args.Code as PackageUrlAddress;
                        Socket socket = this._userSocket[packageurladdress.SendId];
                        if (packageurladdress.operate == "update")
                        {
                            ///成功
                            if (UrlAddressServer.Update(packageurladdress.urladdress) > 0)
                            {
                                packageurladdress.isSuccess = true;
                                this._server.ServerSendData(socket, packageurladdress);
                              }
                            else
                                this._server.ServerSendData(socket, packageurladdress);
                        }
                        else if (packageurladdress.operate == "insert")
                        {
                            ///成功
                            int id = UrlAddressServer.Insert(packageurladdress.urladdress);
                            if (id > 0)
                            {
                                packageurladdress.isSuccess = true;
                                packageurladdress.urladdress.id = id;
                                this._server.ServerSendData(socket, packageurladdress);
                            }
                            else
                                this._server.ServerSendData(socket, packageurladdress);
                        }
                        if (packageurladdress.operate == "delete")
                        {
                            ///成功
                            if (UrlAddressServer.delete(packageurladdress.urladdress) > 0)
                            {
                                packageurladdress.isSuccess = true;
                                this._server.ServerSendData(socket, packageurladdress);
                            }
                            else
                                this._server.ServerSendData(socket, packageurladdress);
                        }
                        if (packageurladdress.isSuccess)
                        {
                            PackageGetUrlTable packagegeturltable = new PackageGetUrlTable();
                            packagegeturltable.HeadCode = EPackageHead.GetUrlAndAddressTable;
                            packagegeturltable.UrlAddress = UrlAddressServer.SelectDataTable(packageurladdress.SendId);
                            this._server.ServerSendData(args.Socket, packagegeturltable);
                        }
                    }
                    break;
                case EPackageHead.UrlGroup_Manager://--------------------------链接地址管理--------------------------  
                    if (args.Code is PackageUrlGroup)
                    {
                        PackageUrlGroup packageurlgroup = args.Code as PackageUrlGroup;
                        Socket socket = this._userSocket[packageurlgroup.SendId];
                        if (packageurlgroup.operate == "update")
                        {
                            ///成功

                            if (UrlGroupServer.Update(packageurlgroup.urlgroup) > 0)
                            {
                                packageurlgroup.isSuccess = true;
                                this._server.ServerSendData(socket, packageurlgroup);
                            }
                            else
                                this._server.ServerSendData(socket, packageurlgroup);
                        }
                        else if (packageurlgroup.operate == "insert")
                        {
                            ///成功
                            int id = UrlGroupServer.Insert(packageurlgroup.urlgroup);
                            if (id > 0)
                            {
                                packageurlgroup.isSuccess = true;
                                this._server.ServerSendData(socket, packageurlgroup);
                            }
                            else
                                this._server.ServerSendData(socket, packageurlgroup);
                        }
                        if (packageurlgroup.operate == "delete")
                        {
                            ///成功
                            if (UrlGroupServer.Delete(packageurlgroup.urlgroup) > 0)
                            {
                                UrlAddressServer.deletebygroupid(packageurlgroup.urlgroup.ID);
                                packageurlgroup.isSuccess = true;
                                this._server.ServerSendData(socket, packageurlgroup);
                            }
                            else
                                this._server.ServerSendData(socket, packageurlgroup);
                        }
                        if (packageurlgroup.isSuccess)
                        {
                            PackageGetUrlTable packagegeturltable = new PackageGetUrlTable();
                            packagegeturltable.HeadCode = EPackageHead.GetUrlAndAddressTable;
                            packagegeturltable.UrlGroup = UrlGroupServer.SelectDataTable(packageurlgroup.SendId);
                            this._server.ServerSendData(args.Socket, packagegeturltable);
                        }
                    }
                    break;
                case EPackageHead.User_OffLine://--------------------------用户下线--------------------------
                    if (args.Code is PackageUserStatus)
                    {
                        PackageUserStatus packageUserStatus = args.Code as PackageUserStatus;
                        switch (packageUserStatus.StatusID)
                        { 
                            case 5://离线
                                this._userSocket.Remove(packageUserStatus.SendId);
                                this.UserOffLineEvent();
                                ///更新数据库的用户状态
                                Users user = UsersManager.Select(packageUserStatus.SendId,0);
                                user.Status = 5;
                                UsersManager.Update(user);
                                ///通知下线
                                 List<Friends> us = new List<Friends>();
                                 us = UsersManager.SelectBeFriends(packageUserStatus.SendId);
                                foreach (Friends u in us)
                                {
                                    if(this._userSocket.ContainsKey(u.HostId))
                                    {
                                        PackageFriendStatus packagefriendstatus = new PackageFriendStatus();
                                        packagefriendstatus.HeadCode = EPackageHead.Friend_Status;
                                        packagefriendstatus.QQNum = u.FriendId;
                                        packagefriendstatus.StatusID = 5;
                                        this._server.ServerSendData(this._userSocket[u.HostId], packagefriendstatus);
                                    }
                                }
                                this.UpdateLogEvent(string.Format("用户下线通知：[ {0} ] 下线了！", packageUserStatus.SendId));
                                break;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseSock()
        {
            this._server.CloseListen();
        }

        #endregion

    }
}
