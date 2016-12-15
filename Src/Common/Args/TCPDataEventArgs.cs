using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Common
{
    public class TCPDataEventArgs : EventArgs
    {
        public Socket Socket;
        public PackageBase Code;
        public IPAddress IpAddress;
        public int Port;

        public TCPDataEventArgs(PackageBase headCode, IPAddress ipAddress, int port)
        {
            this.Code = headCode;
            this.IpAddress = ipAddress;
            this.Port = port;
        }
        public TCPDataEventArgs(Socket socket, PackageBase headCode, IPAddress ipAddress, int port)
            : this(headCode, ipAddress, port)
        {
            this.Socket = socket;
        }
    }
}
