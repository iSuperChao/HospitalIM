using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQControls;
using Common;
using Entity;

namespace ClientView
{
    /// <summary>
    /// 系统消息窗体
    /// </summary>
    public partial class FrmSysMessage : MainFrame
    {
        #region 构造函数
        private Users us;

        public FrmSysMessage(Messages msg)
        {
            InitializeComponent();
            //改变窗体置于屏幕中间
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            ClientManager.Instance.FriendInfoEvent += new Action<PackageFriendInfo>(Instance_FriendInfoEvent);
            ClientManager.Instance.AddFriendFailureEvent += new Action<PackageAddFriend>(Instance_AddFriendFailureEvent);
            ClientManager.Instance.AddFriendSucceeEvent += new Action<PackageAddFriend>(Instance_AddFriendSucceeEvent);
            ClientManager.Instance.FriendListEvent += new Action<PackageFriendList>(Instance_FriendListEvent);
            this.Text = msg.SendId.ToString();
            if (msg != null)
            {

                //向服务端发送请求，获取当前发送消息人的个人信息
                PackageFriendInfo packageFriendInfo = new PackageFriendInfo();
                packageFriendInfo.UserId = msg.SendId;
                ClientManager.Instance.ClientSendData(packageFriendInfo);                
                if (msg.MessageTypeId == 2)
                {
                    //判断该消息是否是离线消息
                    if (msg.Id != 0)
                    {
                        PackageUpdateMessageState packageUpdateMessageState = new PackageUpdateMessageState();
                        packageUpdateMessageState.MessageId = msg.Id;
                        ClientManager.Instance.ClientSendData(packageUpdateMessageState);
                    }
                    Users user = AppContext.FriendList.Find(new Predicate<Users>(friend =>
                        {
                            return msg.SendId == friend.Id;
                        }));
                    if (user == null)
                    {
                        this.lblMessage.Text = "请求加您为好友！";
                        this.btnAddFriend.Visible = true;
                    }
                    else
                    {
                        this.lblMessage.Text = "添加您为好友！";
                        this.btnAddFriend.Visible = false;
                    }
                    this.richTextBox1.Text = "附加消息：无";
                }
                else if (msg.MessageTypeId == 3)
                {
                    if (msg.Id != 0)
                    {
                        PackageUpdateMessageState packageUpdateMessageState = new PackageUpdateMessageState();
                        packageUpdateMessageState.MessageId = msg.Id;
                        ClientManager.Instance.ClientSendData(packageUpdateMessageState);
                    }
                        this.lblMessage.Text = "给你发信息了，目前他还不是你的联系人！";
                        this.richTextBox1.Text = string.Format(
                            "{0} {1}{2}",
                            msg.MessageTime.ToString("yyyy-MM-dd HH:MM:SS"), "\r\n", msg.Message);
                        this.btnAddFriend.Visible = true;
                }
            }
        }

        #endregion

        #region 自定义事件的处理

        /// <summary>
        /// 更新好友
        /// </summary>
        /// <param name="obj">好友列表包</param>
        void Instance_FriendListEvent(PackageFriendList obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    AppContext.FriendList = obj.FriendList;
                    AppContext.FrmMain.InitFriendList();
                }));
            }
        }

        /// <summary>
        /// 显示用户的信息
        /// </summary>
        /// <param name="obj">好友信息包</param>
        void Instance_FriendInfoEvent(PackageFriendInfo obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.us= obj.User;
                    this.lklFromUserId.Text = string.Format("{0}({1})", obj.User.Name, obj.User.Id);
                    ImageClass imgclss = new ImageClass(Functions.byteArrayToImage(obj.User.FaceImage));
                    Image img = imgclss.GetReducedImage(80, 80);
                    this.showHead.HeadImage = img;
                }));
            }
        }

        /// <summary>
        /// 添加好友成功
        /// </summary>
        /// <param name="obj">添加好友包</param>
        void Instance_AddFriendSucceeEvent(PackageAddFriend obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    PackageFriendList packageFriendList = new PackageFriendList();
                    packageFriendList.SendId = obj.SendId;
                    ClientManager.Instance.ClientSendData(packageFriendList);
                    this.Close();
                }));
            }
        }

        /// <summary>
        /// 添加好友失败
        /// </summary>
        /// <param name="obj">添加好友包</param>
        void Instance_AddFriendFailureEvent(PackageAddFriend obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { MessageBox.Show("添加好友失败！"); }));
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 加为好友
        /// </summary>
        private void btnAddFriend_Click(object sender, EventArgs e)
        {
            //判断该用戾是否已经在自己的好友列表里
            Users user = new Users() { Id = Convert.ToInt32(this.Text) };
            if (AppContext.FriendList.Contains(user))
            {
                this.Close();
                return;
            }
            else
            {
                PackageAddFriend packageAddFriend = new PackageAddFriend();
                packageAddFriend.SendId = AppContext.LoginUser.Id;
                packageAddFriend.RecieveId = Convert.ToInt32(this.Text);
                ClientManager.Instance.ClientSendData(packageAddFriend);
            }
        }

        #endregion

        private void lklFromUserId_Click(object sender, EventArgs e)
        {
          //  if (this.listQQFriendList.SelectedItems[0] is QQListBoxItem)
            {
              //  QQListBoxItem item = this.listQQFriendList.SelectedItems[0] as QQListBoxItem;
                FrmViewUserInfo frm = (FrmViewUserInfo)AppContext.CreateOrActivate(typeof(FrmViewUserInfo));
                frm.user = us;
                frm.loaddata();
            }
        }
    }
}
