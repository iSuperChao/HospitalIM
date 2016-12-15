using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Net;
using System.Net.Sockets;
using System.Data;

namespace ClientView
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class AppContext
    {
        #region 变量

        public static ConnDbForAcccess db = new ConnDbForAcccess();

        /// <summary>
        /// 当前登录的用户
        /// </summary>
        public static Users LoginUser = null;

        /// <summary>
        /// 当前登录用户的好友列表
        /// </summary>
        public static List<Users> FriendList = new List<Users>();

        /// <summary>
        /// 好友通信字典集合
        /// </summary>
        public Dictionary<int, Socket> FriendsSocket = new Dictionary<int, Socket>();

        /// <summary>
        /// 好友聊天记录＝＝限制、防止聊天专用字典
        /// </summary>
        public static Dictionary<int, ChatState> ChatStateDict = new Dictionary<int, ChatState>();
        /// <summary>
        /// 主窗体
        /// </summary>
        public static FrmMain FrmMain;

        /// <summary>
        /// URL分组表
        /// </summary>
        public static DataTable UrlGroup=new DataTable();
        /// <summary>
        /// URL地址表
        /// </summary>
        public static DataTable UrlAddress = new DataTable();

        /// <summary>
        /// 存储已打开的窗体
        /// </summary>
        private static List<Form> OpenedForms = new List<Form>();

        /// <summary>
        /// 存储已打开的聊天窗体
        /// </summary>
        private static List<Form> OpenedChatForms = new List<Form>();

        

        /// <summary>
        /// 头像资源文件对象
        /// </summary>
        public static ResourceManager FaceResource = new ResourceManager(
            "ClientView.UserFace", Assembly.GetExecutingAssembly());

        /// <summary>
        /// 状态等资源文件对象
        /// </summary>
        public static ResourceManager StatusResource = new ResourceManager(
            "ClientView.StatusImages", Assembly.GetExecutingAssembly());

        #endregion

        #region 方法

        /// <summary>
        /// 判断指定窗体是否已经打开，如果打开：则激活窗体；如果未打开：则通过反射创建该窗体
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="isDialog">是否是模式化窗体，true：模式化，false：非模式化</param>
        /// <param name="args">构造函数的参数</param>
        /// <returns>窗体对象</returns>
        public static Form CreateOrActivate(Type type, bool isDialog = false, params object[] args)
        {
                
            Form frm = null;
            try
            {
                //判断该窗体是否已经打开
                foreach (Form form in OpenedForms)
                {
                    if (form.GetType() == type)
                    {
                        frm = form;
                        if (frm.WindowState == FormWindowState.Minimized)
                            frm.WindowState = FormWindowState.Normal;
                        frm.Activate();
                        break;
                    }
                }
                if (frm == null)//表示该窗体未打开
                {
                    frm = Activator.CreateInstance(type, args) as Form;
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                    OpenedForms.Add(frm);//将该窗体添加到集合中
                    if (isDialog)
                        frm.ShowDialog();
                    else
                        frm.Show();
                }
            }
            catch (Exception er){ }
            return frm;
        }

        /// <summary>
        /// 判断指定的聊天窗口是否已经如果，如果打开：则激活该窗体；如果未打开，通过反射创建并显示该窗体
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="args">构造函数的参数</param>
        /// <returns>窗体对象</returns>
        public static Form CreateOrActivateChatForm(Type type, params object[] args)
        {
            Form chatForm = null;
            if (args.Length > 0)
            {
                foreach (Form form in OpenedChatForms)
                {
                    if (form.Tag is Users && args[0] is Users)
                    {
                        if ((form.Tag as Users).Id == (args[0] as Users).Id)
                        {
                            chatForm = form;
                            if (chatForm.WindowState == FormWindowState.Minimized)
                                chatForm.WindowState = FormWindowState.Normal;
                            chatForm.Activate();
                        }
                    }
                }
                if (chatForm == null)
                {
                    chatForm = Activator.CreateInstance(type, args) as Form;
                    chatForm.FormClosed += new FormClosedEventHandler(chatForm_FormClosed);
                    OpenedChatForms.Add(chatForm);
                    chatForm.Show();
                }
            }
            return chatForm;
        }

        /// <summary>
        /// 判断指定好友的聊天窗口是否已经打开
        /// </summary>
        /// <param name="user">好友ID</param>
        /// <returns>如果打开：true；未打开：false。</returns>
        public static bool IsOpenChatForm(int userId)
        {
            foreach (Form form in OpenedChatForms)
            {
                if (form.Tag is Users)
                {
                    if ((form.Tag as Users).Id == userId)
                        return true;
                }
            }
            return false;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 窗体关闭时，从集合中移除该窗体
        /// </summary>
        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is Form)
            {
                OpenedForms.Remove(sender as Form);
            }
        }

        /// <summary>
        /// 聊天窗体关闭时，从集合中移除
        /// </summary>
        static void chatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is Form)
            {
                OpenedChatForms.Remove(sender as Form);
            }
        }

        #endregion
    }
}
