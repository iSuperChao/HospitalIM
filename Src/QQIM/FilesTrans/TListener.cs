using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientView
{
    class TListener
    {
        private int _port;
        TserverConnection con;

        #region 注册
        public event Action<string, long, long> GetFile;
        #endregion

        public TListener(int port)
        {
            _port = port;
          //  con.GetFile += new Action<string,long,long>(GetfileEvent);
        }

        private void GetfileEvent(string fname,long flenth,long fposition)
        {
            GetFile(fname,flenth, fposition);
 
        }


        public void StartListening()
        {
            TcpListener tcpl = new TcpListener(IPAddress.Any, _port);//新建一个TcpListener对象
            tcpl.Start();

            while (true)//开始监听
            {
                TcpClient tcp = tcpl.AcceptTcpClient();
                con = new TserverConnection(tcp);
                Thread t = new Thread(new ThreadStart(con.WaitForSendData));
                t.IsBackground = true;
                t.Start();
            }
        }
    }
    
}
