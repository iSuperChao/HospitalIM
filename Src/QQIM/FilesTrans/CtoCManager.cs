using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using Common;
using System.Net;
using System.Windows.Forms;
using Entity;
using System.Data;
using System.Configuration;

namespace ClientView
{
    /// <summary>
    /// 客户端管理
    /// </summary>
    class CtoCManager
    {

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
        /// 连接对象
        /// </summary>
        private SockTCP _sockTcp = new SockTCP();


        public CtoCManager(IPAddress ip,int port)
        {
            this._sockTcp.CreateClientSocket(IPAddress.Any, 0);//创建客户端连接
            this._sockTcp.ConnectRemotePoint(ip, port);//连接服务端
        }


        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="obj">消息包</param>
        public void ClientSendData(object obj)
        {

                this._sockTcp.SendRemotePointData(obj);

        }
        /// <summary>
        /// 客户端关闭连接
        /// </summary>
        public void ClientCloseLink()
        {
            this._sockTcp.CloseConnect();
        }

    }
}
