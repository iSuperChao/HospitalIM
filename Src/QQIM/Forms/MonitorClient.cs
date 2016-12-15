
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;
using Entity;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TConst;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;


namespace ClientView
{
    public partial class MonitorClient : Form
    {
        #region 字段
        private bool Initialized = false;
        public System.Threading.Thread MonitorThread = null;
        public string remoteip;
        public int remoteport;
        public Users remotefriend;

        public int remotemsgport;
        public Boolean stop=false;
        #endregion


        #region 构造函数
        /// <summary>
        /// ip=远程的IP;port=远程的端口
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public MonitorClient(string ip,int port)
        {
            remoteip = ip;
            remoteport = port;
            InitializeComponent();
            Application.Idle += new EventHandler(Application_Idle);
           // this.monitorUserControl1.SetControl(true); 
            checkBox1.Checked = true;
        }
        #endregion

        #region 应用程序空闲状态的处理过程
        void Application_Idle(object sender, EventArgs e)
        {
            this.checkBox1.Enabled = this.remoteip.Trim().Length > 0;
            this.checkBox2.Enabled = this.checkBox1.Enabled;
        }
        #endregion

        #region 监控开始
        private void Monitor()
        {
            while (!stop)
            {
                try
                {
                    this.monitorUserControl1.UpdateDisplay();
                    System.Threading.Thread.Sleep(50);
                }
                catch
                {
                    break;
                }
            }
        }
        #endregion

        #region 监视
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                if (!Initialized)
                {
                    Cursor = Cursors.WaitCursor;

                    this.monitorUserControl1.Initialize(remoteip.Trim(),remoteport);
                    Initialized = true;
                    Cursor = Cursors.Arrow;
                }
                MonitorThread = new System.Threading.Thread(new System.Threading.ThreadStart(Monitor));
                MonitorThread.Start();
            }
            else
            {
                this.MonitorThread.Abort();
            }
        }
        #endregion

        #region 控制
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.monitorUserControl1.SetControl(((CheckBox)sender).Checked);
        }
        #endregion

        private void MonitorClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop = true;
            PackageMonitor packagemonitor = new PackageMonitor();
            //  packagemonitor.CPort = monitorport;
            packagemonitor.MorC = "mtoc";
            packagemonitor.Typemsg = "end";
            packagemonitor.SendId = AppContext.LoginUser.Id;
            packagemonitor.RecieveId = remotefriend.Id;
            packagemonitor.SenderIP = AppContext.LoginUser.LastLoginIp;
            packagemonitor.SenderPort = AppContext.LoginUser.LastLoginPort;
            CtoCManager c = new CtoCManager(IPAddress.Parse(remoteip), remotemsgport);
            c.ClientSendData(packagemonitor);
            this.Refresh();
            Application.DoEvents(); 
        }

        private void MonitorClient_FormClosed(object sender, FormClosedEventArgs e)
        {


           // this.MonitorThread.Abort();
            //this.MonitorThread = null;
        }
    }
}