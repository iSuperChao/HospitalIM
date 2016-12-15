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
using ALQQControl;
using Entity;

namespace ClientView
{
    /// <summary>
    /// 显示查找用户、群界面
    /// </summary>
    public partial class FrmSearchFriend : MainFrame
    {
        #region 构造函数

        public FrmSearchFriend(PackageSearchUser obj)
        {
            InitializeComponent();
            //改变窗体置于屏幕中间
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.lblShuoMing.Text += obj.SearchType == PackageSearchUser.ESearchType.Friend ? "用户" : "群组";
            InitSearchUser(obj.UsersList);
            ClientManager.Instance.AddFriendSucceeEvent += new Action<PackageAddFriend>(Instance_AddFriendSucceeEvent);
            ClientManager.Instance.AddFriendFailureEvent += new Action<PackageAddFriend>(Instance_AddFriendFailureEvent);
            ClientManager.Instance.FriendListEvent += new Action<PackageFriendList>(Instance_FriendListEvent);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 将查询到的用户显示在控件上
        /// </summary>
        /// <param name="userList">查询到的用户集合</param>
        private void InitSearchUser(List<Users> userList)
        {
            this.listQQFriend.Items.Clear();
            this.listQQFriend.BeginUpdate();
            foreach (Users user in userList)
            {
                if (user.Id == AppContext.LoginUser.Id)
                    continue;
                ImageClass imgclss = new ImageClass(Functions.byteArrayToImage(user.FaceImage));
                Image img = imgclss.GetReducedImage(40, 40);
                QQListBoxItem item = new QQListBoxItem()
                {
                    QQ = user.Id,
                    Title = user.Id.ToString(),
                    Remarks = user.Name,                    
                    Image =img,
                    Text = user.Province + user.City + user.Country + user.Unit
                };
               if (!(user.Status == 1))
               {
                   item.Image = Functions.WhiteAndBlack((Bitmap)(img));
               } //(item.Image = Functions.byteArrayToImage(user.FaceImage)) : (item.Image = Functions.WhiteAndBlack((Bitmap)(Functions.byteArrayToImage(user.FaceImage))));
                this.listQQFriend.Items.Add(item);
            }
            this.listQQFriend.EndUpdate();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 加为好友
        /// </summary>
        private void mbtnAddFriend_Click(object sender, EventArgs e)
        {
            
            if (this.listQQFriend.SelectedItems[0] is QQListBoxItem)
            {
                QQListBoxItem item = (QQListBoxItem)this.listQQFriend.SelectedItems[0];
                if (this.listQQFriend.SelectedItems.Count < 1)
                {
                    MessageBox.Show("请选择一位好友！");
                    return;
                }
                if (item.QQ == AppContext.LoginUser.Id)
                {
                    MessageBox.Show("不能添加自己为好友！");
                    return;
                }
                //判断对方是否已经是自己的好友
                if (AppContext.FriendList.Contains(new Users() { Id = Convert.ToInt32(item.QQ) }))
                {
                    MessageBox.Show("对方已经是你的好友，不能重复添加！");
                    return;
                }
                //封装一个添加好友的包，发送给服务端
                PackageAddFriend packageAddFriend = new PackageAddFriend() 
                {
                    SendId = AppContext.LoginUser.Id,
                    RecieveId = item.QQ
                };
                ClientManager.Instance.ClientSendData(packageAddFriend);
            }
        }

        #endregion

        #region 自定义事件的处理

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
                    
                    MessageBox.Show("添加好友成功！");
                }));
            }
        }

        /// <summary>
        /// 添加好友后更新主窗体上的好友列表
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

        #endregion

        private void mbtnCancel_Load(object sender, EventArgs e)
        {

        }
    }
}
