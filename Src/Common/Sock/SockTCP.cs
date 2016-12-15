using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Common
{
    public class SockTCP
    {
        #region 变量
        private Thread _tcpServerThread;
        private Thread _getConnectDataThread;
        private NetworkStream _clientStream;
        private BinaryFormatter _formatter = new BinaryFormatter();
        private Socket _tcpSock;
        private Socket _clientSock;
        private Thread _connectThread;
        public IPAddress clientIP;
        public int clientPort;
        #endregion

        #region 委托 && 事件
        public delegate void TCPDataArrivalEventHandler(TCPDataEventArgs args);
        public event TCPDataArrivalEventHandler TCPDataArrival = null;
        #endregion

        #region 监听端
        /// <summary>
        /// 监听
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="port">端口号</param>
        public void Listen(IPAddress ipAddress, int port)
        {
            this._tcpSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._tcpSock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this._tcpSock.Bind(new IPEndPoint(ipAddress, port));
            this._tcpSock.Listen(10);
            this._tcpServerThread = new Thread(new ThreadStart(this.GetConnectSocket));
            this._tcpServerThread.IsBackground = true;
            this._tcpServerThread.Start();
        }
        /// <summary>
        /// 关闭监听
        /// </summary>
        public void CloseListen()
        {
            try
            {
                if (this._tcpSock != null)
                {
                    if (this._tcpSock.Connected)
                    {
                        this._tcpSock.Shutdown(SocketShutdown.Both);
                    }
                    this._tcpSock.Close();
                    this._tcpSock = null;
                }
                if (this._connectThread != null)
                {
                    if (this._connectThread.ThreadState == System.Threading.ThreadState.Running)
                    {
                        this._connectThread.Abort();
                        this._connectThread = null;
                    }
                }
                if (this._tcpServerThread != null)
                {
                    if (this._tcpServerThread.ThreadState == System.Threading.ThreadState.Running)
                    {
                        this._tcpServerThread.Abort();
                        this._tcpServerThread = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SockTCP::CloseListen()");
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 获取连接套接字
        /// </summary>
        private void GetConnectSocket()
        {
            while (true)
            {
                try
                {
                    if (this._tcpSock != null)
                    {
                        Socket client = this._tcpSock.Accept();
                        if (client.Connected)
                        {
                            this._connectThread = new Thread(new ParameterizedThreadStart(this.GetConnectData));
                            this._connectThread.IsBackground = true;
                            this._connectThread.Start(client);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SockTCP::GetConnectSocket()");
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 获取连接数据
        /// </summary>
        /// <param name="obj"></param>
        private void GetConnectData(object obj)
        {
            if (obj is Socket)
            {
                Socket client = obj as Socket;
                if (client != null)
                {
                    while (client.Connected)
                    {
                        try
                        {
                            PackageBase code = null;
                            IPEndPoint remoteEndPoint = (IPEndPoint)client.RemoteEndPoint;
                            using (NetworkStream stream = new NetworkStream(client))
                            {
                                if (stream.DataAvailable)
                                {
                                    code = this._formatter.Deserialize(stream) as PackageBase;//挂起的操作
                                }
                            }
                            if (code == null)
                                continue;
                            if (this.TCPDataArrival != null)
                            {
                                this.TCPDataArrival(new TCPDataEventArgs(client, code, remoteEndPoint.Address, remoteEndPoint.Port));
                            }
                        }
                        catch (Exception ex)
                        {   //表示客户端已经与服务器断开连接
                            Debug.WriteLine("SockTCP::GetConnectData(object)表示客户端已经与服务器断开连接");
                            Debug.WriteLine(ex.ToString());
                            break;
                        }
                    }
                    try
                    {
                        //服务器端释放已经断开的客户端连接Socket对象资源
                        if (client.Connected)
                        {
                            client.Shutdown(SocketShutdown.Both);
                        }
                        client.Close();
                        client = null;
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("SockTCP::GetConnectData(object obj)服务器端释放已经断开的客户端连接Socket对象资源失败");
                    }
                }
            }
            else
            {
                Debug.WriteLine("SockTCP::GetConnectData(object obj)：obj对象必须是Socket类型");
            }
        }
        /// <summary>
        /// 服务端发送消息
        /// </summary>
        /// <param name="socket">目标Socket</param>
        /// <param name="obj">需要发送的包</param>
        public void ServerSendData(Socket socket, object obj)
        {
            try
            {
                this._formatter.Serialize(new NetworkStream(socket), obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SockTCP::ServerSendData()");
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region 连接端
        /// <summary>
        /// 创建客户端连接
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口</param>
        public void CreateClientSocket(IPAddress ip, int port)
        {
            try
            {
                this._clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this._clientSock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                this._clientSock.Bind(new IPEndPoint(ip, port));
            }
            catch (Exception ex)
            {
                Console.WriteLine("SockTCP::CreateClientSocket();创建连接失败");
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 连接远程端点
        /// </summary>
        /// <param name="remoteIp">IP地址</param>
        /// <param name="port">端口</param>
        public void ConnectRemotePoint(IPAddress remoteIp, int port)
        {
                //建立远程主机的连接
                IPEndPoint ipep = new IPEndPoint(remoteIp, port);  
                this._clientSock.Connect(ipep);
                this._clientStream = new NetworkStream(this._clientSock);
                this.GetConnectData();
                this.clientIP = ipep.Address;
                this.clientPort = ipep.Port;          
        }
        /// <summary>
        /// 获取连接
        /// </summary>
        private void GetConnectData()
        {
            this._getConnectDataThread = new Thread(new ThreadStart(this.GetRecieveData));
            this._getConnectDataThread.IsBackground = true;
            this._getConnectDataThread.Start();
        }
        /// <summary>
        /// 获取接收数据
        /// </summary>
        private void GetRecieveData()
        {
            IPEndPoint remoteEndPoint = (IPEndPoint)this._clientSock.RemoteEndPoint;
            while (true)
            {
                try
                {
                    PackageBase code = this._formatter.Deserialize(this._clientStream) as PackageBase;
                    if (code == null)
                        continue;
                    if (TCPDataArrival != null)
                    {
                        this.TCPDataArrival(new TCPDataEventArgs(code, remoteEndPoint.Address, remoteEndPoint.Port));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SockTCP::GetRecieveData();获取连接数据失败");
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 向远程主机发送消息
        /// </summary>
        /// <param name="obj">包</param>
        public void SendRemotePointData(object obj)
        {
            try
            {
                this._formatter.Serialize(this._clientStream, obj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SockTCP::SendRemotePointData();");
                Debug.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseConnect()
        {
            Thread.Sleep(200);
            try
            {
                if (this._getConnectDataThread != null)
                {
                    this._getConnectDataThread.Abort();
                    this._getConnectDataThread = null;
                }
                if (this._clientSock != null)
                {
                    if (this._clientSock.Connected)
                    {
                        this._clientSock.Shutdown(SocketShutdown.Both);
                    }
                    this._clientSock.Close();
                    this._clientSock = null;
                }
                if (this._clientStream != null)
                {
                    this._clientStream.Dispose();
                    this._clientStream = null;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("SockTCP::CloseConnect()关闭连接失败");
            }
        }
        #endregion
    }
}
