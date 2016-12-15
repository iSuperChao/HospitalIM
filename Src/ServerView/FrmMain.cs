using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common;
using System.Runtime.Serialization.Formatters.Binary;
using ServerBLL;
using System.IO;
using Entity;

namespace ServerView
{
    /// <summary>
    /// 服务端
    /// </summary>
    public partial class FrmMain : Form
    {
        #region 委托
        delegate void ChangeOnLineCountHandler(int num);
        #endregion

        #region 字段

        ServerManager _serverManager = null;
        /// <summary>
        /// 用户socket，链接状态字典集合
        /// </summary>
        public Dictionary<int, int> _userSocketstate = new Dictionary<int, int>();

        #endregion

        #region 构造函数

        public FrmMain()
        {
            InitializeComponent();
            tsmiStartServer_Click(null, null);

        }

        #endregion

        #region 自定义事件

        /// <summary>
        /// 更新日志
        /// </summary>
        /// <param name="obj">日志信息</param>
        void _serverManager_UpdateLogEvent(string obj)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>((arg) =>
                    {
                        this.rtbLog.AppendText(arg + "\r\n");
                    }), obj);
            }
        }

        /// <summary>
        /// 用户上线
        /// </summary>
        void _serverManager_UserOnLineEvent()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ChangeOnLineCountHandler(this.ChangeOnLineCount), 1);
            }
            else
            {
                this.ChangeOnLineCount(1);
            }
        }

        /// <summary>
        /// 用户下线
        /// </summary>
        void _serverManager_UserOffLineEvent()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ChangeOnLineCountHandler(this.ChangeOnLineCount), -1);
            }
            else
            {
                this.ChangeOnLineCount(1);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 改成控件中“当前在线人数”的值
        /// </summary>
        /// <param name="num"></param>
        private void ChangeOnLineCount(int num)
        {
           // this.tslCount.Text = (Convert.ToInt32(tslCount.Text) + num) + "";
            this.tslCount.Text = _serverManager._userSocket.Count.ToString();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 《退出程序》
        /// </summary>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);//强制退出应用程序
        }

        /// <summary>
        /// 窗体关闭时，释放资源
        /// </summary>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tsmiStopServer_Click(null, null);
        }

        /// <summary>
        /// 《启动服务》
        /// </summary>
        private void tsmiStartServer_Click(object sender, EventArgs e)
        {
            if (this._serverManager == null)
            {
                this._serverManager = new ServerManager();
                this._serverManager.UserOnLineEvent += new Action(_serverManager_UserOnLineEvent);
                this._serverManager.UserOffLineEvent += new Action(_serverManager_UserOffLineEvent);
                this._serverManager.UpdateLogEvent += new Action<string>(_serverManager_UpdateLogEvent);
            }
            if (this._serverManager != null)
            {
                this.tsmiStartServer.Checked = true;
                this.tsmiStopServer.Checked = false;
            }
        }

        /// <summary>
        /// 《停止服务》
        /// </summary>
        private void tsmiStopServer_Click(object sender, EventArgs e)
        {
            if (this._serverManager != null)
            {
                this._serverManager.CloseSock();
                this._serverManager = null;
            }
            if (this._serverManager == null)
            {
                this.tsmiStartServer.Checked = false;
                this.tsmiStopServer.Checked = true;
            }
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try
            {
                foreach (int u in _serverManager._userSocket.Keys)
                {
                    if (!_userSocketstate.ContainsKey(u))
                    {
                        _userSocketstate.Add(u, 0);
                    }
                    try
                    {
                        _serverManager._userSocket[u].Send(new byte[] { });
                        if (_userSocketstate[u] > 0)
                            _userSocketstate[u] -= 1;
                    }
                    catch (Exception er) { _userSocketstate[u] += 1; rtbLog.AppendText(u + ":已经第" + _userSocketstate[u] + "次链接失败." + "\r\n"); }
                }


                int[] keys = new int[_userSocketstate.Count];
                _userSocketstate.Keys.CopyTo(keys, 0);
                foreach (int key in keys)
                {
                    if (!_serverManager._userSocket.ContainsKey(key))
                    {
                        _userSocketstate.Remove(key);
                        continue;
                    }
                    if (_userSocketstate[key] > 3)
                    {
                        _serverManager._userSocket.Remove(key);
                        _userSocketstate.Remove(key);
                        ///更新数据库的用户状态
                        Users user = UsersManager.Select(key, 0);
                        user.Status = 5;
                        UsersManager.Update(user);
                        ChangeOnLineCount(0);
                        rtbLog.AppendText(string.Format("用户下线通知：[ {0} ] 超过3次检测连接失败，确定断线了！" + "\r\n", key));
                    }

                }
            }
            catch (Exception er) { rtbLog.AppendText("轮询检测用户连接出错：" + er.ToString() + "\r\n"); };

            timer1.Enabled = true ;

        }

    }
}
