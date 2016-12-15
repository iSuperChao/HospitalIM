using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using Common;
using System.Net;
using System.Windows.Forms;
using Entity;
using System.Data;
using System.Configuration;
using System.Diagnostics;

namespace ClientView
{

     
    /// <summary>
    /// 客户端管理
    /// </summary>
    class ClientManager
    {

        [DllImport("shell32.dll")]
        public extern static IntPtr ShellExecute(IntPtr hwnd,
                                                 string lpOperation,
                                                 string lpFile,
                                                 string lpParameters,
                                                 string lpDirectory,
                                                 int nShowCmd
                                                );
        public enum ShowWindowCommands : int
        {

            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 10
        }

        #region 变量

        /// <summary>
        /// 本机IP地址
        /// </summary>
        private IPAddress _localIP = IPAddress.Parse(ConfigurationManager.AppSettings["localIP"].ToString());

        /// <summary>
        /// 服务端IP地址
        /// </summary>
        public IPAddress _serverIP =IPAddress.Parse("10.118.2.90");

        /// <summary>
        /// 服务端端口号
        /// </summary>
        public int _serverPort =Convert.ToInt32("9876");

        /// <summary>
        /// 当前服务器版本,以此是否需要升级本地客户端
        /// </summary>
        public int _version = 0;

        /// <summary>
        /// 连接对象
        /// </summary>
        private SockTCP _sockTcp = new SockTCP();

        #endregion

        #region 构造函数

        public ClientManager()
        {
            DataTable dt = AppContext.db.ReturnDataSet("select top 1 * from ServerInfo ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                _serverIP = IPAddress.Parse(dt.Rows[0]["_serverIP"].ToString());
                _serverPort = Convert.ToInt32(dt.Rows[0]["_serverPort"].ToString());
                _version = Convert.ToInt32(dt.Rows[0]["_version"].ToString());
            }
            this._sockTcp.CreateClientSocket(IPAddress.Any, 0);//创建客户端连接
            this._sockTcp.ConnectRemotePoint(_serverIP, _serverPort);//连接服务端
            this._sockTcp.TCPDataArrival += new SockTCP.TCPDataArrivalEventHandler(_sockTcp_TCPDataArrival);
        }

        #endregion

        #region 属性

        private static ClientManager _instance;
        public static ClientManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClientManager();
                }
                return _instance;
            }
        }

        #endregion

        #region 委托 && 事件

        #region 注册
        public event Action<PackageRegister> RegisterEvent;
        #endregion

        #region 登录
        /// <summary>
        /// 登录成功
        /// </summary>
        public event Action<PackageUserInfo> LoginSucceeEvent;
        /// <summary>
        /// 好友状态改变
        /// </summary>
        public event Action<PackageFriendStatus> FriendStatusEvent;
        /// <summary>
        /// 登录失败
        /// </summary>
        public event Action<PackageUserInfo> LoginFailureEvent;
        #endregion

        #region 获取好友
        public event Action<PackageFriendList> FriendListEvent;
        #endregion

        #region 查询好友
        public event Action<PackageSearchUser> SearchUserEvent;
        #endregion

        #region 添加好友
        /// <summary>
        /// 添加好友成功
        /// </summary>
        public event Action<PackageAddFriend> AddFriendSucceeEvent;
        /// <summary>
        /// 添加好友失败
        /// </summary>
        public event Action<PackageAddFriend> AddFriendFailureEvent;
        #endregion

        #region 删除好友
        /// <summary>
        /// 删除好友成功
        /// </summary>
        public event Action<PackageDeleteFriend> DeleteFriendSucceeEvent;
        /// <summary>
        /// 删除好友失败
        /// </summary>
        public event Action<PackageDeleteFriend> DeleteFriendFailureEvent;
        #endregion

        #region 消息
        public event Action<PackageMessage> MessageEvent;
        #endregion

        #region 查询好友信息
        public event Action<PackageFriendInfo> FriendInfoEvent;
        #endregion

        #region 文件传送
        public event Action<PackageFilesTrans> FilesTransEvent;
        #endregion

        #region 文件传送完成
        public event Action<PackageFilesTransFinished> FilesTransFinishedEvent;
        #endregion

        #region 远程协助
        public event Action<PackageMonitor> MonitorEvent;
        #endregion

        #region 地址管理
        public event Action<PackageUrlAddress> UrlAddressEvent;
        #endregion

        #region 组名管理
        public event Action<PackageUrlGroup> UrlGroupEvent;
        #endregion

        #region 地址及组的获取 
        public event Action<PackageGetUrlTable> GetUrlAddressEvent;
        #endregion
        #endregion

        #region 方法

        /// <summary>
        /// 接收服务端的消息的处理
        /// </summary>
        /// <param name="args"></param>
        void _sockTcp_TCPDataArrival(TCPDataEventArgs args)
        {
            switch (args.Code.HeadCode)
            {
                case EPackageHead.Server_InfoList://更新服务器列表
                    if (args.Code is PackageServerList)
                    {            
                        try
                        {

                            PackageServerList ps = (PackageServerList)(args.Code);
                            foreach (ServerInfo si in ps.ServerList)
                            {
                                if (si._version > _version)
                                {
                                    MessageBox.Show("对不起，客户端需要升级了！");
                                    try
                                    {
                                        try
                                        {
                                            PackageUserStatus packageUserStatus = new PackageUserStatus();
                                            packageUserStatus.StatusID = 5;
                                            packageUserStatus.SendId = AppContext.LoginUser.Id;
                                            ClientSendData(packageUserStatus);
                                        }
                                        catch { }
                                        IntPtr ip = new IntPtr();
                                        Process proc = new Process();
                                        proc.StartInfo.CreateNoWindow = true;
                                        proc.StartInfo.UseShellExecute = false; //此属性必须设置成false 
                                        proc.StartInfo.RedirectStandardOutput = true;// 此属性必须设置成true 
                                        proc.StartInfo.FileName = @"" + Application.StartupPath + "\\AutoUpdate.exe";
                                        proc.StartInfo.Arguments = si._version.ToString();// ping.exe 192.168.10.* 
                                        proc.Start(); 

                                        //ShellExecute(ip, "open", Application.StartupPath + "\\AutoUpdate.exe", null, null, (int)ShowWindowCommands.SW_SHOW);
                                        System.Environment.Exit(0);
                                    }
                                    catch
                                    {
                                        MessageBox.Show("升级程序出错，请重新下载。");
                                        System.Environment.Exit(0);
                                    }
                                }
                             }
                            AppContext.db.ExeSQL("delete from ServerInfo ");
                            foreach (ServerInfo si in ps.ServerList)
                            {
                                AppContext.db.ExeSQL("insert into ServerInfo(_serverIP,_serverPort,_notice,_version) values('" + si._serverIP + "','" + si._serverPort + "','" + si._notice + "'," + _version + ")"); 
                            }
                        }
                        catch{ }
                    }
                    break;
                case EPackageHead.User_Register://用户注册
                    if (args.Code is PackageRegister)
                    {
                        this.RegisterEvent(args.Code as PackageRegister);
                    }
                    break;
                case EPackageHead.User_Login_Success:// 登录成功
                    if (args.Code is PackageUserInfo)
                    {
                        PackageUserInfo u = args.Code as PackageUserInfo;
                        if (u.User.Status == 1)
                        {                        
                            _sockTcp.Listen(IPAddress.Parse(u.User.LastLoginIp), u.User.LastLoginPort);
                        }
                            ///MessageBox.Show(string.Format("{0}:{1}", IPAddress.Parse(u.User.LastLoginIp), u.User.LastLoginPort));
                        this.LoginSucceeEvent(u);
                        
                    }
                    break;
                case EPackageHead.User_OffLine:// 其它地方登陆,强制下线通知
                    if (args.Code is PackageUserOffLine)
                    {
                        MessageBox.Show("您的工号在其它电脑上登陆,本地电脑将强制退出!");
                        //PackageUserStatus packageUserStatus = new PackageUserStatus();
                        //packageUserStatus.StatusID = 5;
                        //packageUserStatus.SendId = AppContext.LoginUser.Id;
                        //ClientSendData(packageUserStatus);
                        System.Environment.Exit(0);
                    }
                    break;
                case EPackageHead.Friend_Status:// 好友状态改变
                    if (args.Code is PackageFriendStatus)
                    {
                        PackageFriendStatus p = args.Code as PackageFriendStatus;
                        Users user = AppContext.FriendList.Find(new Predicate<Users>(selectUser =>
                        {
                            return p.QQNum == selectUser.Id;
                        }));
                       // user.Emailpassword = "00000000";
                        user.Status = p.StatusID;
                        if (p.StatusID == 1)
                        {
                            user.LastLoginIp = p.LastLoginIp;
                            user.LastLoginPort = p.LastLoginPort;
                            user.LastLoginTime = p.LastLoginTime;
                        }
                        this.FriendStatusEvent(args.Code as PackageFriendStatus);
                    }
                    break;
                case EPackageHead.User_Login_Failure://登录失败
                    if (args.Code is PackageUserInfo)
                    {
                        this.LoginFailureEvent(args.Code as PackageUserInfo);
                    }
                    break;
                case EPackageHead.User_FriendList://获取好友列表
                    if (args.Code is PackageFriendList)
                    {
                        this.FriendListEvent(args.Code as PackageFriendList);
                    }
                    break;
                case EPackageHead.Friend_Sreach://查询好友
                    if (args.Code is PackageSearchUser)
                    {
                        this.SearchUserEvent(args.Code as PackageSearchUser);
                    }
                    break;
                case EPackageHead.Friend_Add_Success://添加好友成功
                    if (args.Code is PackageAddFriend)
                    {
                        this.AddFriendSucceeEvent(args.Code as PackageAddFriend);
                    }
                    break;
                case EPackageHead.Friend_Add_Failure://添加好友失败
                    if (args.Code is PackageAddFriend)
                    {
                        this.AddFriendFailureEvent(args.Code as PackageAddFriend);
                    }
                    break;
                case EPackageHead.Friend_Delete_Success://删除好友成功
                    if (args.Code is PackageDeleteFriend)
                    {
                        this.OnDeleteFriendSucceeEvent(args.Code as PackageDeleteFriend);
                    }
                    break;
                case EPackageHead.Friend_Delete_Failure://删除好友失败
                    if (args.Code is PackageDeleteFriend)
                    {
                        this.OnDeleteFriendSucceeEvent(args.Code as PackageDeleteFriend);
                    }
                    break;
                case EPackageHead.Message://消息
                    if (args.Code is PackageMessage)
                    {
                        this.OnMessageEvent(args.Code as PackageMessage);
                    }
                    break;
                case EPackageHead.Friend_Info://好友个人信息
                    if (args.Code is PackageFriendInfo)
                    {
                        this.OnFriendInfoEvent(args.Code as PackageFriendInfo);
                    }
                    break;
                case EPackageHead.FilesTrans://文件传输的消息
                    if (args.Code is PackageFilesTrans)
                    {
                        this.OnFilesTransEvent(args.Code as PackageFilesTrans);
                    }
                    break;
                case EPackageHead.FilesTrans_finished://文件传输完成的消息
                    if (args.Code is PackageFilesTransFinished)
                    {
                        this.OnFilesTransFinishedEvent(args.Code as PackageFilesTransFinished);
                    }
                    break;
                case EPackageHead.Monitor://文件传输完成的消息
                    if (args.Code is PackageMonitor)
                    {
                        this.OnMonitorEvent(args.Code as PackageMonitor);
                    }
                    break;
                case EPackageHead.UrlAddress_Manager://管理地址链接
                    if (args.Code is PackageUrlAddress)
                    {
                        this.OnUrlAddressEvent(args.Code as PackageUrlAddress);
                    }
                    break;
                case EPackageHead.UrlGroup_Manager://管理组名
                    if (args.Code is PackageUrlGroup)
                    {
                        this.OnUrlGroupEvent(args.Code as PackageUrlGroup);
                    }
                    break;
                case EPackageHead.GetUrlAndAddressTable://获取地址链接及组表
                    if (args.Code is PackageGetUrlTable)
                    {
                        if (!((args.Code as PackageGetUrlTable).UrlGroup==null))
                        {
                            AppContext.UrlGroup = (args.Code as PackageGetUrlTable).UrlGroup;
                        }
                        if (!((args.Code as PackageGetUrlTable).UrlAddress==null))
                        {
                            AppContext.UrlAddress = (args.Code as PackageGetUrlTable).UrlAddress;
                        }
                        this.OnGetUrlAddressEvent(args.Code as PackageGetUrlTable);
                    }
                    break;
            }
        }

        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="obj">消息包</param>
        public void ClientSendData(object obj)
        {
            try
            {
                this._sockTcp.SendRemotePointData(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ClientManager.ClientSendData ::" + ex.Message);
            }
        }

        /// <summary>
        /// 客户端关闭连接
        /// </summary>
        public void ClientCloseLink()
        {
            this._sockTcp.CloseConnect();
        }

        #endregion

        #region 激发自定义事件的方法

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="packageRegister">注册包</param>
        private void OnRegisterEvent(PackageRegister packageRegister)
        {
            if (this.RegisterEvent != null)
            {
                this.RegisterEvent(packageRegister);
            }
        }
        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="packageUserInfo">用户包</param>
        private void OnLoginSucceeEvent(PackageUserInfo packageUserInfo)
        {
            if (this.LoginSucceeEvent != null)
            {
                this.LoginSucceeEvent(packageUserInfo);
            }
        }
        /// <summary>
        /// 好友上线通知
        /// </summary>
        /// <param name="packageFriendStatus">状态包</param>
        private void OnFriendStatusEvent(PackageFriendStatus packageFriendStatus)
        {
            if (this.FriendStatusEvent != null)
            {
                this.FriendStatusEvent(packageFriendStatus);
            }
        }
        /// <summary>
        /// 登录失败
        /// </summary>
        /// <param name="packageUserInfo">用户包</param>
        private void OnLoginFailureEvent(PackageUserInfo packageUserInfo)
        {
            if (this.LoginFailureEvent != null)
            {
                this.LoginFailureEvent(packageUserInfo);
            }
        }
        /// <summary>
        /// 获取好友
        /// </summary>
        /// <param name="packageFriendList">好友列表包</param>
        private void OnFriendListEvent(PackageFriendList packageFriendList)
        {
            if (this.FriendListEvent != null)
            {
                this.FriendListEvent(packageFriendList);
            }
        }

        /// <summary>
        /// 查询好友
        /// </summary>
        /// <param name="packageSearchUser">查询好友包</param>
        private void OnSearchUserEvent(PackageSearchUser packageSearchUser)
        {
            if (this.SearchUserEvent != null)
            {
                this.SearchUserEvent(packageSearchUser);
            }
        }
        /// <summary>
        /// 添加好友成功
        /// </summary>
        /// <param name="packageAddFriend">添加好友包</param>
        private void OnAddFriendSucceeEvent(PackageAddFriend packageAddFriend)
        {
            if (this.AddFriendSucceeEvent != null)
            {
                this.AddFriendSucceeEvent(packageAddFriend);
            }
        }
        /// <summary>
        /// 添加好友失败
        /// </summary>
        /// <param name="packageAddFriend">添加好友包</param>
        private void OnAddFriendFailureEvent(PackageAddFriend packageAddFriend)
        {
            if (this.AddFriendFailureEvent != null)
            {
                this.AddFriendFailureEvent(packageAddFriend);
            }
        }
        /// <summary>
        /// 删除好友成功
        /// </summary>
        /// <param name="packageDeleteFriend">删除好友包</param>
        private void OnDeleteFriendSucceeEvent(PackageDeleteFriend packageDeleteFriend)
        {
            if (this.DeleteFriendSucceeEvent != null)
            {
                this.DeleteFriendSucceeEvent(packageDeleteFriend);
            }
        }
        /// <summary>
        /// 删除好友失败
        /// </summary>
        /// <param name="packageDeleteFriend">删除好友包</param>
        private void OnDeleteFriendFailureEvent(PackageDeleteFriend packageDeleteFriend)
        {
            if (this.DeleteFriendFailureEvent != null)
            {
                this.DeleteFriendFailureEvent(packageDeleteFriend);
            }
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="packageMessage">消息包</param>
        private void OnMessageEvent(PackageMessage packageMessage)
        { 
            if(this.MessageEvent != null)
            {
                this.MessageEvent(packageMessage);
            }
        }
        /// <summary>
        /// 获取好在信息
        /// </summary>
        /// <param name="packageFriendInfo">好友信息包</param>
        private void OnFriendInfoEvent(PackageFriendInfo packageFriendInfo)
        {
            if (this.FriendInfoEvent != null)
            {
                this.FriendInfoEvent(packageFriendInfo);
            }
        }

        /// <summary>
        /// 获取文件传送消息
        /// </summary>
        /// <param name="packageFriendInfo">好友信息包</param>
        private void OnFilesTransEvent(PackageFilesTrans packagefilestrans)
        {
            if (this.FilesTransEvent != null)
            {
                this.FilesTransEvent(packagefilestrans);
            }
        }

        /// <summary>
        /// 文件传送完成消息
        /// </summary>
        /// <param name="packageFriendInfo">好友信息包</param>
        private void OnFilesTransFinishedEvent(PackageFilesTransFinished packagefilestransfinished)
        {
            if (this.FilesTransFinishedEvent != null)
            {
                this.FilesTransFinishedEvent(packagefilestransfinished);
            }
        }

        /// <summary>
        /// 文件传送完成消息
        /// </summary>
        /// <param name="packageFriendInfo">好友信息包</param>
        private void OnMonitorEvent(PackageMonitor packageMonitor)
        {
            if (this.MonitorEvent != null)
            {
                this.MonitorEvent(packageMonitor);
            }
        }

        /// <summary>
        /// 链接地址管理事件
        /// </summary>
        /// <param name="packageFriendInfo">好友信息包</param>
        private void OnUrlAddressEvent(PackageUrlAddress packageurladdress)
        {
            if (this.UrlAddressEvent != null)
            {
                this.UrlAddressEvent(packageurladdress);
            }
        }

        /// <summary>
        /// 链接组名管理事件
        /// </summary>
        /// <param name="packageFriendInfo">好友信息包</param>
        private void OnUrlGroupEvent(PackageUrlGroup packageurlgroup)
        {
            if (this.UrlGroupEvent != null)
            {
                this.UrlGroupEvent(packageurlgroup);
            }
        }

        /// <summary>
        /// 获取链接地址及组管理事件
        /// </summary>
        /// <param name="packageFriendInfo">好友信息包</param>
        private void OnGetUrlAddressEvent(PackageGetUrlTable packagegeturltable)
        {
            //while (this.GetUrlAddressEvent == null)
            {
                if (this.GetUrlAddressEvent != null)
                {
                    this.GetUrlAddressEvent(packagegeturltable);
                   // break;
                }
            }
        }

        #endregion
    }
}
