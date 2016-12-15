using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQControls;
using ALQQControl;
using Common;
using System.Threading;
using Entity;
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ClientView
{
    /// <summary>
    /// QQ主界面
    /// </summary>
    public partial class FrmMain : MainFrame
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

        private bool ismenuload=false;
        #region 构造函数
        public FrmMain()
        {
            InitializeComponent();
            //改变窗体的起始位置
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - 30, 10);
            MsgContext.MessageMainEvent += new Action<PackageMessage>(MsgContext_MessageMainEvent);
            MsgContext.MonitorMainEvent += new Action<PackageMonitor>(MsgContext_MonitorMainEvent);
            MsgContext.FilesTransMainEvent += new Action<PackageFilesTrans>(MsgContext_FilesTransMainEvent);
            ClientManager.Instance.DeleteFriendFailureEvent += new Action<PackageDeleteFriend>(Instance_DeleteFriendFailureEvent);
            ClientManager.Instance.DeleteFriendSucceeEvent += new Action<PackageDeleteFriend>(Instance_DeleteFriendSucceeEvent);
            ClientManager.Instance.FriendStatusEvent += new Action<PackageFriendStatus>(Instance_FriendStatusEvent);
            ClientManager.Instance.FriendListEvent += new Action<PackageFriendList>(Instance_FriendListEvent);
            ClientManager.Instance.GetUrlAddressEvent += new Action<PackageGetUrlTable>(Instance_InitAppMainMenuEvent);
            AppContext.FrmMain = this;
            this.InitPersonInfo();
            //显示完个人信息和好友后，向服务端发送一个离线消息包，获取该用户的离线消息
            PackageOffLineMessage packageOffLineMessage = new PackageOffLineMessage();
            packageOffLineMessage.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageOffLineMessage);

        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载个人信息
        /// </summary>
        private void InitPersonInfo()
        {
            ImageClass imgclss = new ImageClass(Functions.byteArrayToImage(AppContext.LoginUser.FaceImage));
            Image img = imgclss.GetReducedImage(60, 60);
            this.ShowHead.Image = img;
            this.lblNickName.Text = AppContext.LoginUser.Name + "(" + AppContext.LoginUser.Id+ ")";
            this.lblAutograph.Text = AppContext.LoginUser.City + AppContext.LoginUser.Country + AppContext.LoginUser.Unit;
            this.notifyIcon.ShowBalloonTip(200, "登录提示", "欢迎" + AppContext.LoginUser.Name +
                "(" + AppContext.LoginUser.Id + ") 在 " + DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "登录", ToolTipIcon.Info);
            this.ShowNotifyIcon();
            this.InitFriendList();
            this.InitAppMainMenu();
        }

        /// <summary>
        /// 根据性别显示任务栏上的图标
        /// </summary>
        private void ShowNotifyIcon()
        {
            //if (AppContext.LoginUser.Gender == "男")
            //{
            this.notifyIcon.Icon = AppContext.StatusResource.GetObject("Title") as Icon;
            //}
            //else
            //{
            //    this.notifyIcon.Icon = AppContext.StatusResource.GetObject("imonline1") as Icon;
            //}
        }

        /// <summary>
        /// 显示好友信息
        /// </summary>
        public void InitFriendList()
        {
            this.listQQFriendList.Items.Clear();
            this.listQQFriendList.BeginUpdate();
            foreach (Users user in AppContext.FriendList)
            {
                Image img=Functions.byteArrayToImage(user.FaceImage);
                string st="";
                if (user.Status == 1) { img = Functions.byteArrayToImage(user.FaceImage);}
                else { img = Functions.WhiteAndBlack((Bitmap)(Functions.byteArrayToImage(user.FaceImage))); st = ")(离线状态"; }
                ImageClass imgclss = new ImageClass(img);
                img=imgclss.GetReducedImage(45,45);
                QQListBoxItem item = new QQListBoxItem()
                {
                    QQ = user.Id,
                    Title = user.Id.ToString()+st,
                    Remarks = user.Name,     
                    Image = img,
                    Text = user.City+user.Country+user.Unit,
                };
                TimerEx timerEx = new TimerEx(280);
                timerEx.Elapsed += new System.Timers.ElapsedEventHandler(timerEx_Elapsed);
                timerEx.Tag = item;
                item.Tag = timerEx;
                
                if (user.Status == 1)
                {
                    this.listQQFriendList.Items.Insert(0, item);
                }
                else
                    this.listQQFriendList.Items.Add(item);
            }
            this.listQQFriendList.EndUpdate();
        }

       

        #endregion

        #region 事件

        /// <summary>
        /// 查找好友
        /// </summary>
        private void btnSreach_Click(object sender, EventArgs e)
        {
            AppContext.CreateOrActivate(typeof(FrmSearch));
        }

        /// <summary>
        /// 《任务栏图标》右键菜单-退出程序
        /// </summary>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            PackageUserStatus packageUserStatus = new PackageUserStatus();
            packageUserStatus.StatusID = 5;
            packageUserStatus.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageUserStatus);
            ClientManager.Instance.ClientCloseLink();
            this.notifyIcon.Icon = null;
            Environment.Exit(0);    
        }

        /// <summary>
        /// 双击任务栏图标时，让窗体显示在最前端
        /// </summary>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Activate();
        }

        /// <summary>
        /// 《好友列表》右键菜单-删除好友
        /// </summary>
        private void tsmiDeleteFriend_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否确认删除该好友？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (this.listQQFriendList.SelectedItems[0] is QQListBoxItem)
                {
                    QQListBoxItem item = this.listQQFriendList.SelectedItems[0] as QQListBoxItem;
                    PackageDeleteFriend packageDeleteFriend = new PackageDeleteFriend();
                    packageDeleteFriend.RecieveId = item.QQ;
                    packageDeleteFriend.SendId = AppContext.LoginUser.Id;
                    ClientManager.Instance.ClientSendData(packageDeleteFriend);
                }
            }
        }

        /// <summary>
        /// 打开相应的聊天窗口
        /// </summary>
        private void listQQFriendList_DoubleClick(object sender, EventArgs e)
        {
            if (this.listQQFriendList.SelectedItems.Count < 1)
                return;
            if (this.listQQFriendList.SelectedItems[0] is QQListBoxItem)
            {
                QQListBoxItem item = this.listQQFriendList.SelectedItems[0] as QQListBoxItem;
                Users user = AppContext.FriendList.Find(new Predicate<Users>(selectUser =>
                    {
                        return item.QQ == selectUser.Id;
                    }));
                //如果用户头像在闪动，则停止闪动
                if (item.Tag is TimerEx)
                {
                    TimerEx timerEx = item.Tag as TimerEx;
                    if (timerEx.Enabled)
                    {
                        timerEx.Stop();
                       // if (item.Image == null)//还原好友头像
                        {
                            ImageClass imgclss = new ImageClass(Functions.byteArrayToImage(user.FaceImage));
                            Image img= imgclss.GetReducedImage(45, 45);
                            if (user.Status == 1) { item.Image = img; }
                            else if (user.Status == 5) { item.Image = Functions.WhiteAndBlack((Bitmap)(img)); }                
                        }
                    }
                }
                AppContext.CreateOrActivateChatForm(typeof(FrmChat), user);
            }
        }

        /// <summary>
        /// 计时器闪动好友头像
        /// </summary>
        void timerEx_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (sender is TimerEx)
            {
                TimerEx timerEx = sender as TimerEx;
                if (timerEx.Tag is QQListBoxItem)
                {
                    QQListBoxItem item = timerEx.Tag as QQListBoxItem;
                    if (item.Image == null)
                    {
                        //获取当前好友的用户对象
                        Users user = AppContext.FriendList.Find(new Predicate<Users>(friend =>
                            {
                                return friend.Id == item.QQ;
                            }));
                        ImageClass imgclss = new ImageClass(Functions.byteArrayToImage(user.FaceImage));
                        Image img = imgclss.GetReducedImage(45, 45);
                        item.Image = img;
                       // if (user.Status == 1) { item.Image = Functions.byteArrayToImage(user.FaceImage); }
                       //else { item.Image = Functions.WhiteAndBlack((Bitmap)(Functions.byteArrayToImage(user.FaceImage))); } 
                    }
                    else
                    {
                        item.Image = null;
                    }
                    this.listQQFriendList.Invalidate();
                }
            }
        }



        /// <summary>
        /// 系统图标的闪动
        /// </summary>
        private void tmrFlashSysMessage_Tick(object sender, EventArgs e)
        {
            if (this.btnSysMessage.BackgroundImage != null)
            {
                this.btnSysMessage.BackgroundImage = null;
            }
            else
            {
                this.btnSysMessage.BackgroundImage = AppContext.StatusResource.GetObject("message") as Image;
            }
            this.btnSysMessage.Invalidate();
        }

        /// <summary>
        /// 打开系统消息
        /// </summary>
        private void btnSysMessage_Click(object sender, EventArgs e)
        {
            //获取所有的系统消息
            List<Messages> msgList = MsgContext.MsgList.FindAll(new Predicate<Messages>(message =>
            {
                return message.MessageTypeId == 2;
            }));
            //获取所有的系统消息
            List<Messages> befriendmsgList = MsgContext.MsgList.FindAll(new Predicate<Messages>(message =>
            {
                return message.MessageTypeId == 3;
            }));
            //取出系统消息
            if (msgList != null && msgList.Count > 0)
            {
                MsgContext.MsgList.Remove(msgList[0]);
                AppContext.CreateOrActivate(typeof(FrmSysMessage), false, msgList[0]);
                msgList.Remove(msgList[0]);
                if (msgList.Count < 1)
                {
                    this.tmrFlashSysMessage.Enabled = false;
                }
                this.btnSysMessage.BackgroundImage = AppContext.StatusResource.GetObject("message") as Image;
            }
            else if (befriendmsgList != null && befriendmsgList.Count > 0)
            {
                MsgContext.MsgList.Remove(befriendmsgList[0]);
                AppContext.CreateOrActivate(typeof(FrmSysMessage), false, befriendmsgList[0]);
                befriendmsgList.Remove(befriendmsgList[0]);
                if (befriendmsgList.Count < 1)
                {
                    this.tmrFlashSysMessage.Enabled = false;
                }
                this.btnSysMessage.BackgroundImage = AppContext.StatusResource.GetObject("message") as Image;
            
            }
            else
            {
                MessageBox.Show("暂时未有系统消息！");
            }
        }

        #endregion

        #region 自定义事件的处理

        /// <summary>
        /// 删除好友成功
        /// </summary>
        /// <param name="obj">删除好友包</param>
        void Instance_DeleteFriendSucceeEvent(PackageDeleteFriend obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    PackageFriendList packageFriendList = new PackageFriendList();
                    packageFriendList.SendId = AppContext.LoginUser.Id;
                    ClientManager.Instance.ClientSendData(packageFriendList);
                }));
            }
        }

        /// <summary>
        /// 更新好友状态
        /// </summary>
        /// <param name="obj"></param>
        void Instance_FriendStatusEvent(PackageFriendStatus obj)
        {
            //int ISchanged = -1;
            int index = -1;
            QQListBoxItem citem = new QQListBoxItem();
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    foreach (QQListBoxItem item in listQQFriendList.Items)
                    {
                        index++;
                        if (item.QQ == obj.QQNum)
                        {
                            Users user = AppContext.FriendList.Find(new Predicate<Users>(friend =>
                            { return friend.Id == item.QQ; }));
                            user.Status = obj.StatusID;
                            if (obj.StatusID == 1)//上线
                            {
                                ImageClass imgclss = new ImageClass(Functions.byteArrayToImage(user.FaceImage));
                                Image img = imgclss.GetReducedImage(45, 45);
                                item.Image = img;
                                item.Title = user.Id.ToString();
                                this.notifyIcon.ShowBalloonTip(200, "同事上线", "工号：" + item.QQ +
    "(" + item.Remarks + ")上线了！ ", ToolTipIcon.Info);
                                this.ShowNotifyIcon();
                              //  ISchanged = listQQFriendList.Items.IndexOf(item);
                                citem = item;
                            }
                            else if (obj.StatusID == 5)//下线
                            {
                                item.Image = Functions.WhiteAndBlack((Bitmap)(item.Image));
                                item.Title = item.QQ.ToString() + ")(离线状态";
                                this.notifyIcon.ShowBalloonTip(200, "同事下线", "工号：" + item.QQ +
   "(" + item.Remarks + ")下线了！ ", ToolTipIcon.Info);
                                this.ShowNotifyIcon();
                              //  ISchanged = listQQFriendList.Items.IndexOf(item); 
                                citem = item;
                            }
                            break;
                        }
                    }
                   // MessageBox.Show(ISchanged.ToString());
                    if (!(index == -1))
                    {
                        try
                        {
                            listQQFriendList.Items.RemoveAt(index);
                        }
                        catch { }
                        if (obj.StatusID == 1)//上线
                        {                          
                            listQQFriendList.Items.Insert(0, citem);
                            listQQFriendList.Refresh();
                        }
                        else if (obj.StatusID == 5)
                        {
                            listQQFriendList.Items.Add(citem);
                            listQQFriendList.Refresh();
                        }
                    }

                    //MessageBox.Show("qq:"+obj.QQNum+";status:"+obj.StatusID);

                }));
            }
        }

        /// <summary>
        /// 删除好友失败
        /// </summary>
        /// <param name="obj">删除好友包</param>
        void Instance_DeleteFriendFailureEvent(PackageDeleteFriend obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { MessageBox.Show("删除失败！"); }));
            }
        }

        /// <summary>
        /// 更新好友列表
        /// </summary>
        /// <param name="obj"></param>
        void Instance_FriendListEvent(PackageFriendList obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    AppContext.FriendList = obj.FriendList;
                    this.InitFriendList();
                }));
            }
        }


        void Instance_InitAppMainMenuEvent(PackageGetUrlTable obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.InitAppMainMenu();
                }));
            }
        }

        void InitAppMainMenu()
        {
            contextMenuStrip1.Items.Clear();
            foreach (DataRow dr in AppContext.UrlGroup.Rows)
            {
                //添加菜单一 
                ToolStripMenuItem subItem; 
                subItem = AddContextMenu(dr["GroupName"].ToString(), contextMenuStrip1.Items, null); 
                subItem.Tag= dr["id"].ToString();
                subItem.Name="Menu"+dr["id"].ToString();
                //ismenuload = true;
                //timer1.Enabled = false;

                // tsmi.Click += new System.EventHandler(bBBToolStripMenuItem_Click(null,null));
            }
            foreach(DataRow dr in AppContext.UrlAddress.Rows)
            {
                try
                {
                    if (dr["UrlType"].ToString() == "app")
                    {
                        ToolButton bt = new ToolButton();
                        bt = (this.Controls["tbtn" + dr["AppOrder"].ToString()] as ToolButton);
                        toolTip1.SetToolTip(bt, dr["UrlName"].ToString());
                        bt.BackgroundImage = Functions.byteArrayToImage((byte[])dr["UrlImage"]);
                        bt.Tag = dr["UrlAddress"].ToString();
                    }
                    else
                    {
                        ToolStripMenuItem subItem, preItem;
                        preItem = (ToolStripMenuItem)contextMenuStrip1.Items["Menu" + dr["GroupId"].ToString()];
                        //添加子菜单 
                        subItem = AddContextMenu(dr["UrlName"].ToString(), preItem.DropDownItems, new EventHandler(MenuClicked));
                        subItem.Tag = dr["UrlAddress"].ToString();
                    }
                }
                catch { }
            }

        }
        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
        /// <param name="cms">要添加到的子菜单集合</param>
        /// <param name="callback">点击时触发的事件</param>
        /// <returns>生成的子菜单，如果为分隔条则返回null</returns>

        ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);

                return null;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);

                return tsmi;
            }

            return null;
        }

        void MenuClicked(object sender, EventArgs e)
        {
            ShellExecute(this.Handle, "open", (sender as ToolStripMenuItem).Tag.ToString(), null, null, (int)ShowWindowCommands.SW_SHOW);
        } 



        /// <summary>
        /// 主窗口收到消息后，闪动头像
        /// </summary>
        /// <param name="obj"></param>
        void MsgContext_MessageMainEvent(PackageMessage obj)
        {
            Boolean isbefriendedmsg = true;
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                    {
                        if (obj.Message.MessageTypeId == 1)//普通消息
                        {
                            foreach (QQListBoxItem item in this.listQQFriendList.Items)
                            {
                                if (obj.Message.SendId == item.QQ)//找到发送消息的好友
                                {
                                    if (item.Tag is TimerEx)
                                    {
                                        isbefriendedmsg = false;
                                        TimerEx timerEx = item.Tag as TimerEx;
                                        if (!timerEx.Enabled)
                                        {
                                            timerEx.Start();//启动计时器
                                            break;
                                        }
                                    }
                                }
                            }
                            if (isbefriendedmsg)
                            {
                                obj.Message.MessageTypeId = 3;//非好友，但属对方好友所发的信息。
                                this.tmrFlashSysMessage.Enabled = true;
                            }
                        }
                        else if (obj.Message.MessageTypeId == 2)//系统消息
                        {
                            this.tmrFlashSysMessage.Enabled = true;
                        }
                    }));
            }
        }

        /// <summary>
        /// 主窗口收到远程消息后，闪动头像
        /// </summary>
        /// <param name="obj"></param>
        void MsgContext_MonitorMainEvent(PackageMonitor obj)
        {
            Boolean isbefriendedmsg = true;
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (obj.Typemsg == "ask")//普通消息
                    {
                        foreach (QQListBoxItem item in this.listQQFriendList.Items)
                        {
                            if (obj.SendId == item.QQ)//找到发送消息的好友
                            {
                                if (item.Tag is TimerEx)
                                {
                                    isbefriendedmsg = false;
                                    TimerEx timerEx = item.Tag as TimerEx;
                                    if (!timerEx.Enabled)
                                    {
                                        timerEx.Start();//启动计时器
                                        break;
                                    }
                                }
                            }
                        }
                        if (isbefriendedmsg)
                        {
                            PackageMonitor packagemonitor = new  PackageMonitor();
                            packagemonitor.Typemsg = "nofriend";
                            packagemonitor.SendId = AppContext.LoginUser.Id;
                            packagemonitor.RecieveId = obj.SendId;
                            CtoCManager c = new CtoCManager(IPAddress.Parse(obj.SenderIP), obj.SenderPort);
                            c.ClientSendData(packagemonitor);                            
                        }
                    }
                }));
            }
        }

        /// <summary>
        /// 主窗口收到传送文件消息后，闪动头像
        /// </summary>
        /// <param name="obj"></param>
        void MsgContext_FilesTransMainEvent(PackageFilesTrans obj)
        {
            Boolean isbefriendedmsg = true;
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (obj.SanderOrReveived== "sender")//推送方发来的
                    {
                        foreach (QQListBoxItem item in this.listQQFriendList.Items)
                        {
                            if (obj.SendId == item.QQ)//找到发送消息的好友
                            {
                                if (item.Tag is TimerEx)
                                {
                                    isbefriendedmsg = false;
                                    TimerEx timerEx = item.Tag as TimerEx;
                                    if (!timerEx.Enabled)
                                    {
                                        timerEx.Start();//启动计时器
                                        break;
                                    }
                                }
                            }
                        }
                        if (isbefriendedmsg)
                        {
                            PackageFilesTrans packagefilestrans = new PackageFilesTrans();
                            packagefilestrans.SanderOrReveived = "reveived";
                            packagefilestrans.SendId = AppContext.LoginUser.Id;
                            packagefilestrans.RecieveId = obj.SendId;
                            packagefilestrans.Error = "notFriend";
                            CtoCManager c = new CtoCManager(IPAddress.Parse(obj.SenderIP), obj.SenderPort);
                            c.ClientSendData(packagefilestrans);
                        }
                    }
                }));
            }
        }

        #endregion

        /// <summary>
        /// 窗体关闭时，向服务端发送一个离线包
        /// </summary>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            PackageUserStatus packageUserStatus = new PackageUserStatus();
            packageUserStatus.StatusID = 5;
            packageUserStatus.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageUserStatus);
            this.notifyIcon.Icon = null;
            ClientManager.Instance.ClientCloseLink();
        }

        private void tbtnZone_Load(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void ShowHead_Click(object sender, EventArgs e)
        {
            AppContext.CreateOrActivate(typeof(FrmModifUserInfo));
        }

        private void ShowHead_MouseClick(object sender, MouseEventArgs e)
        {
            AppContext.CreateOrActivate(typeof(FrmModifUserInfo));
        }

        private void lblNickName_Click(object sender, EventArgs e)
        {
            AppContext.CreateOrActivate(typeof(FrmModifUserInfo));
        }

        private void picSreach_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text.Length<=0)
                return;
            int i=0,p=-1;
            try
            {
                if ((int.TryParse(txtSearch.Text, out i)))
                {
                    foreach (QQListBoxItem item in this.listQQFriendList.Items)
                    {
                        p++;
                        if (i == item.QQ)//找到好友
                        {
                            listQQFriendList.SelectedIndex = p;
                        }
                    }

                }
                else
                {
                    foreach (QQListBoxItem item in this.listQQFriendList.Items)
                    {
                        p++;
                        if (item.Remarks.Contains(txtSearch.Text))//找到的好友
                        {
                            listQQFriendList.SelectedIndex = p;
                        }
                    }
                }
            }
            catch { }

        }

        private void tbtnZone_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("");
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.picSreach_Click(sender, e);
            }
        }

        private void 查看资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listQQFriendList.SelectedItems[0] is QQListBoxItem)
                {
                    QQListBoxItem item = this.listQQFriendList.SelectedItems[0] as QQListBoxItem;
                    FrmViewUserInfo frm = (FrmViewUserInfo)AppContext.CreateOrActivate(typeof(FrmViewUserInfo));
                    frm.user = AppContext.FriendList.Find(new Predicate<Users>(friend =>
                    {
                        return friend.Id == item.QQ;
                    }));
                    frm.loaddata();
                }
            }
            catch { }
        }

        private void 平台信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listQQFriendList_DoubleClick(sender, e);
        }

        private void tbtnQZone_Click(object sender, EventArgs e)
        {

        }

        private void toolButton2_Click(object sender, EventArgs e)
        {     

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
           // listQQFriendList_DoubleClick(sender, e);
            //try
            //{
            //   if (this.listQQFriendList.SelectedItems[0] is QQListBoxItem)
            //    {
            //        QQListBoxItem item = this.listQQFriendList.SelectedItems[0] as QQListBoxItem;
            //        FrmChat f = new FrmChat(item.Tag as Users);
            //        f=(FrmChat)AppContext.CreateOrActivateChatForm(typeof(FrmChat), item.Tag as Users);
            //        f.toolButton4_Click(sender, e);
            //    }
            //}
        }
         //   catch { }


        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("该功能还没开发好。");
        }

        private void 发送文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listQQFriendList_DoubleClick(sender, e);
            //if (this.listQQFriendList.SelectedItems[0] is QQListBoxItem)
            //{
            //    QQListBoxItem item = this.listQQFriendList.SelectedItems[0] as QQListBoxItem;
            //    FrmChat f = (FrmChat)AppContext.CreateOrActivateChatForm(typeof(FrmChat), item.Tag as Users);
            //    f.toolButton2_Click(sender, e);
            //}
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            //listQQFriendList_DoubleClick(sender, e);
            //if (this.listQQFriendList.SelectedItems[0] is QQListBoxItem)
            //{
            //    QQListBoxItem item = this.listQQFriendList.SelectedItems[0] as QQListBoxItem;
            //    FrmChat f = (FrmChat)AppContext.CreateOrActivateChatForm(typeof(FrmChat), item.Tag as Users);
            //    f.toolButton3_Click(sender, e);
            //}
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "确认退出吗？退出将关闭所有的窗口和远程协助及文件传输！", "确认退出", MessageBoxButtons.YesNo) ==DialogResult.Yes)
            {
                this.Close();
                System.Environment.Exit(0);
            }
        }

        private void pnlMenu_Click(object sender, EventArgs e)
        {
            Point p = new Point(Cursor.Position.X, Cursor.Position.Y - contextMenuStrip1.Height);
            contextMenuStrip1.Show(p);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnSafe_Click(object sender, EventArgs e)
        {
            AboutBox A = new AboutBox();
            A.ShowDialog();
        }

        private void tbtnManager_Click(object sender, EventArgs e)
        {
            FrmUrlManager fum = new FrmUrlManager();
            fum.ShowDialog();
            InitAppMainMenu();
        }


        private void tbtn1_Click(object sender, EventArgs e)
        {
            try
            {
                ShellExecute(this.Handle, "open", tbtn1.Tag.ToString(), null, null, (int)ShowWindowCommands.SW_SHOW);
            }
            catch
            {
                MessageBox.Show("你还没给这个铵钮绑定程序，请先绑定！");
                tbtnManager_Click(sender, e);
            }
        }

        private void tbtn2_Click(object sender, EventArgs e)
        {
            try
            {
                ShellExecute(this.Handle, "open", tbtn2.Tag.ToString(), null, null, (int)ShowWindowCommands.SW_SHOW);
            }
            catch
            {
                MessageBox.Show("你还没给这个铵钮绑定程序，请先绑定！");
                tbtnManager_Click(sender, e);
            }
        }
        private void tbtn3_Click(object sender, EventArgs e)
        {
            try
            {
                ShellExecute(this.Handle, "open", tbtn3.Tag.ToString(), null, null, (int)ShowWindowCommands.SW_SHOW);
            }
            catch
            {
                MessageBox.Show("你还没给这个铵钮绑定程序，请先绑定！");
                tbtnManager_Click(sender, e);
            }
        }

        private void tbtn4_Click(object sender, EventArgs e)
        {
            try
            {
                ShellExecute(this.Handle, "open", tbtn4.Tag.ToString(), null, null, (int)ShowWindowCommands.SW_SHOW);
            }
            catch
            {
                MessageBox.Show("你还没给这个铵钮绑定程序，请先绑定！");
                tbtnManager_Click(sender, e);
            }
        }

        private void tbtn5_Click(object sender, EventArgs e)
        {
            try
            {
                ShellExecute(this.Handle, "open", tbtn5.Tag.ToString(), null, null, (int)ShowWindowCommands.SW_SHOW);
            }
            catch
            {
                MessageBox.Show("你还没给这个铵钮绑定程序，请先绑定！");
                tbtnManager_Click(sender, e);
            }
        }

        private void tbtn6_Click(object sender, EventArgs e)
        {
            try
            {
                ShellExecute(this.Handle, "open", tbtn6.Tag.ToString(), null, null, (int)ShowWindowCommands.SW_SHOW);
            }
            catch
            {
                MessageBox.Show("你还没给这个铵钮绑定程序，请先绑定！");
                tbtnManager_Click(sender, e);
            }
        }

        private void tbtn7_Click(object sender, EventArgs e)
        {
            try
            {
                ShellExecute(this.Handle, "open", tbtn7.Tag.ToString(), null, null, (int)ShowWindowCommands.SW_SHOW);
            }
            catch
            {
                MessageBox.Show("你还没给这个铵钮绑定程序，请先绑定！");
                tbtnManager_Click(sender, e);
            }
        }

        private void tbtn8_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void tbtn8_Click(object sender, EventArgs e)
        {
            try
            {
                ShellExecute(this.Handle, "open", tbtn8.Tag.ToString(), null, null, (int)ShowWindowCommands.SW_SHOW);
            }
            catch
            {
                MessageBox.Show("你还没给这个铵钮绑定程序，请先绑定！");
                tbtnManager_Click(sender, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (ismenuload)
            //{
            //    return;
            //}
            
            //    InitAppMainMenu();

 
            
        }
    }
}
