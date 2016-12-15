using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;

namespace ClientView
{
    /// <summary>
    /// 消息处理
    /// </summary>
    static class MsgContext
    {
        #region 变量
        /// <summary>
        /// 消息池
        /// </summary>
        public static List<Messages> MsgList = new List<Messages>();
        /// <summary>
        /// 文件发送消息池
        /// </summary>
        public static List<PackageFilesTrans> FilesMsgList = new List<PackageFilesTrans>();
        /// <summary>
        /// 远程协助消息池
        /// </summary>
        public static List<PackageMonitor> MonitorMsgList = new List<PackageMonitor>();
        #endregion

        #region 事件
        /// <summary>
        /// 主窗口的消息事件
        /// </summary>
        public static event Action<PackageMessage> MessageMainEvent;
        /// <summary>
        /// 聊天窗口的消息事件
        /// </summary>
        public static event Action<PackageMessage> MessageChatEvent;

        /// <summary>
        /// 主窗口的消息事件
        /// </summary>
        public static event Action<PackageFilesTrans> FilesTransMainEvent;
        /// <summary>
        /// 聊天窗口的文件传送消息事件
        /// </summary>
        public static event Action<PackageFilesTrans> FilesTransChatEvent;
        /// <summary>
        /// 主窗口的远程协助消息事件
        /// </summary>
        public static event Action<PackageMonitor> MonitorMainEvent;
        /// <summary>
        /// 聊天窗口的远程协助消息事件
        /// </summary>
        public static event Action<PackageMonitor> MonitorChatEvent;
        #endregion

        #region 构造函数

        static MsgContext()
        {
            ClientManager.Instance.MessageEvent += new Action<PackageMessage>(Instance_MessageEvent);
            ClientManager.Instance.FilesTransEvent += new Action<PackageFilesTrans>(Instance_FilesTransEvent);
            ClientManager.Instance.MonitorEvent += new Action<PackageMonitor>(Instance_MonitorEvent);        
        }

        #endregion

        #region 自定义事件的处理

        /// <summary>
        /// 收到服务端发送过来的消息后的处理
        /// </summary>
        /// <param name="obj">消息包</param>
        static void Instance_MessageEvent(PackageMessage obj)
        {
            try
            {
                AppContext.db.ExeSQL("insert into Messages(MyId,FriendId,SendId,RecieveId,Message,MessageTypeId,MessageTime) values("+
                    AppContext.LoginUser.Id+","+
                    obj.Message.SendId + "," +
                    obj.Message.SendId + "," +
                    obj.Message.RecieveId+
                    ",'"+obj.Message.Message+"',"+
                    obj.Message.MessageTypeId+",'"+
                    obj.Message.MessageTime+"')");
            }
            catch { }
            //判断指定好友的聊天窗口是否已经打开
            if (AppContext.IsOpenChatForm(obj.Message.SendId))
            {
                //如果打开-则把消息直接显示在窗天窗体上
                MessageChatEvent(obj);
            }
            else
            { 
                //将消息添加到消息池
                MsgList.Add(obj.Message);
                //闪动指定用户的头像
                OnMessageMainEvent(obj);
            }
        }

        /// <summary>
        /// 收到服务端发送过来的文件传送请求后的处理
        /// </summary>
        /// <param name="obj">消息包</param>
        static void Instance_FilesTransEvent(PackageFilesTrans obj)
        {
            FilesMsgList.Add(obj);
            ////判断指定好友的聊天窗口是否已经打开
            if (!AppContext.IsOpenChatForm(obj.SendId))
            {
                OnFilesTransMainEvent(obj);
            }
        }

        /// <summary>
        /// 收到对方发送过来的远程消息的处理
        /// </summary>
        /// <param name="obj">消息包</param>
        static void Instance_MonitorEvent(PackageMonitor obj)
        {
            //判断指定好友的聊天窗口是否已经打开
            if (AppContext.IsOpenChatForm(obj.SendId))
            {
                //如果打开-则把消息直接显示在窗天窗体上
                MonitorChatEvent(obj);
            }
            else
            {
                //将消息添加到消息池
                MonitorMsgList.Add(obj);
                //闪动指定用户的头像
                OnMonitorMainEvent(obj);
            }
        }

        #endregion

        #region 激发自定义事件的方法

        /// <summary>
        /// 主窗口的消息事件
        /// </summary>
        /// <param name="packageMessage">消息包</param>
        static void OnMessageMainEvent(PackageMessage packageMessage)
        {
            if (MessageMainEvent != null)
            {
                MessageMainEvent(packageMessage);
            }
        }
        ///// <summary>
        ///// 聊天窗口的消息事件
        ///// </summary>
        ///// <param name="packageMessage">消息包</param>
        //static void OnMessageChatEvent(PackageMessage packageMessage)
        //{
        //    if (MessageChatEvent != null)
        //    {
        //        MessageChatEvent(packageMessage);
        //    }
        //}
        /// <summary>
        /// 主窗口的远程消息事件
        /// </summary>
        /// <param name="packageMessage">消息包</param>
        static void OnMonitorMainEvent(PackageMonitor packagemonitor)
        {
            if (MonitorMainEvent != null)
            {
                MonitorMainEvent(packagemonitor);
            }
        }
        ///// <summary>
        ///// 聊天窗口的远程消息事件
        ///// </summary>
        ///// <param name="packageMessage">消息包</param>
        //static void OnMonitorChatEvent(PackageMessage packageMessage)
        //{
        //    if (MessageChatEvent != null)
        //    {
        //        MessageChatEvent(packageMessage);
        //    }
        //}
        /// <summary>
        /// 主窗口的消息事件
        /// </summary>
        /// <param name="packageMessage">消息包</param>
        static void OnFilesTransMainEvent(PackageFilesTrans packageFilesTrans)
        {
            if (FilesTransMainEvent != null)
            {
                FilesTransMainEvent(packageFilesTrans);
            }
        }
        /// <summary>
        /// 聊天窗口的消息事件
        /// </summary>
        /// <param name="packageMessage">消息包</param>
        static void OnFilesTransChatEvent(PackageFilesTrans packageFilesTrans)
        {
            if (FilesTransChatEvent != null)
            {
                FilesTransChatEvent(packageFilesTrans);
            }
        }

        #endregion
    }
}
