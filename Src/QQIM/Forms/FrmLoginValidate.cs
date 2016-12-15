using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQControls;
using System.Resources;
using System.Reflection;
using Common;
using Entity;

namespace ClientView
{
    /// <summary>
    /// 正在登录,登录验证窗体
    /// </summary>
    public partial class FrmLoginValidate : MainFrame
    {
        #region 变量
        private int _index = 1;
        #endregion

        #region 构造函数

        public FrmLoginValidate(int qqNumber, string qqPwd)
        {
            InitializeComponent();
            ClientManager.Instance.LoginSucceeEvent += new Action<PackageUserInfo>(Instance_LoginSucceedEvent);
            ClientManager.Instance.LoginFailureEvent += new Action<PackageUserInfo>(Instance_LoginFailureEvent);
            ClientManager.Instance.FriendListEvent += new Action<PackageFriendList>(Instance_FriendListEvent);
            this.notifyIcon.Text = qqNumber + "正在登录...";
            //向服务端发送用户登录包
            PackageLogin packageLogin = new PackageLogin();
            packageLogin.QQNumber = qqNumber;
            packageLogin.QQPwd = qqPwd;
            ClientManager.Instance.ClientSendData(packageLogin);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 任务栏上的图标动画
        /// </summary>
        private void tmrNotifyIcon_Tick(object sender, EventArgs e)
        {
            try
            {
                this.notifyIcon.Icon = AppContext.StatusResource.GetObject("Loading_" + this._index) as Icon;
                picLogining.Image = AppContext.StatusResource.GetObject("Loading_" + this._index+"1") as Image;
                if (this._index < 7)
                {
                    this._index++;
                }
                else
                {
                    this._index = 1;
                }
            }
            catch (Exception er) { }
        }

        #endregion

        #region 自定义事件处理

        /// <summary>
        /// 获取用户好友列表
        /// </summary>
        /// <param name="obj">好友列表包</param>
        void Instance_FriendListEvent(PackageFriendList obj)
        {
            AppContext.FriendList = obj.FriendList;
        }

        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="obj">登录包</param>
        void Instance_LoginSucceedEvent(PackageUserInfo obj)
        {
            AppContext.LoginUser = obj.User;
            //向服务端发送获取好友列表的包
            PackageFriendList packageFriendList = new PackageFriendList();
            packageFriendList.SendId = obj.User.Id;
            ClientManager.Instance.ClientSendData(packageFriendList);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 登录失败
        /// </summary>
        /// <param name="obj">登录包</param>
        void Instance_LoginFailureEvent(PackageUserInfo obj)
        {
            MessageBox.Show("帐号或密码错误，登录失败！");
           // Application.ProductName:=
            this.notifyIcon.Icon = null;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion

        private void btn_Close_Load(object sender, EventArgs e)
        {

        }
    }
}
