using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQControls;
using Common;
using Entity;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TConst;
using System.Data.Sql;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace ClientView
{
    /// <summary>
    /// 聊天窗体
    /// </summary>
    public partial class FrmChat : Form
    {
        CtoCManager c=null;
        Boolean canctc = false;
        Users user;


        /// <summary>
        /// ======================================定义：文件传送部分＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        /// </summary>
        private int _filetransport=9999;
        private string _filetransRemotSenderIP = "";
        private int _filetransRemotSenderport = 9999;
        
       // private string filesorpath;
        private long _filestotlesize;
        private long _filesfinishedsize;
       // private long _filessize;
        private NumericUpDown _filestotlenumber=new NumericUpDown();
        private NumericUpDown _filesfinishednumber=new NumericUpDown();
        private List<FilesTransList> filestranslist  = new  List<FilesTransList>();
        private PackageFilesTrans getapackage;
        /// <summary>
        /// ==========================================================================================
        /// </summary>
        /// <param name="u"></param>
        /// 

        /// <summary>
        /// ======================================定义：远程桌面部分＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        /// </summary>
        private int _Monitorlocalport = 9999;
        private int _MonitorRemoteport = 9999;
        TcpServerChannel channel=null;
        MonitorClient monitfrm=null;

        private Thread t1=null;///本地监听线程 


        /// <summary>
        /// ==========================================================================================
        /// </summary>
        /// <param name="u"></param>

        #region 构造函数

        public FrmChat(Users u)
        {
            user = u;
            InitializeComponent();
            //改变窗体置于屏幕中间
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            MsgContext.MessageChatEvent += new Action<PackageMessage>(MsgContext_MessageChatEvent);
            MsgContext.FilesTransChatEvent += new Action<PackageFilesTrans>(MsgContext_FilesTransChatEvent);
            MsgContext.MonitorChatEvent += new Action<PackageMonitor>(MsgContext_MonitorChatEvent);
            ClientManager.Instance.FriendStatusEvent += new Action<PackageFriendStatus>(Instance_FriendStatusEvent);
            ClientManager.Instance.FilesTransFinishedEvent += new Action<PackageFilesTransFinished>(Instance_FilesTransFinishedEventEvent);
            // TserverConnection.
            string st="";

            ImageClass imgclss = new ImageClass(Functions.byteArrayToImage(user.FaceImage));
            Image img = imgclss.GetReducedImage(80, 80);
            if (user.Status == 1) { this.showFriendHead.HeadImage = img; }
            else { this.showFriendHead.HeadImage = Functions.WhiteAndBlack((Bitmap)(img)); st = "(离线状态）"; }
             //this.showFriendHead.HeadImage = Functions.byteArrayToImage(user.FaceImage);
            this.lblAutograph.Text = user.City+user.Country+user.Unit;
            this.lklNickName.Text = string.Format("{0} ({1})", user.Name, user.Id)+st;
            this.Tag = user;
            this.Text ="正在和"+ user.Name+"的对话";
            this.Namelabel.Text = user.Name;
            this.sexlabel.Text = user.Gender;
            this.unittextBox.Text = user.Province+user.City + user.Country + user.Unit;
            this.mobilephonetext.Text = user.MobilePhone;
            this.emailtextBox.Text = user.Email;
            this.QQShow(user);//显示相应的QQ秀
            tabPage2.Parent = null;
            tabPage3.Parent = null;
            tabPage4.Parent = null;
            tabPage5.Parent = null;
            _filesfinishednumber.Parent = this;
            _filesfinishednumber.Visible = false;
            //开启文件传输的监听端口
            Thread t = new Thread(new ThreadStart(WaitForConnect));
            t.IsBackground = true;
            t.Start();
            Thread tcctc = new Thread(new ThreadStart(CheckCTC));
            tcctc.IsBackground = true;
            tcctc.Start();
           

            ///＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝消息＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            //获取该用户在消息池中的消息
            List<Messages> msgList = MsgContext.MsgList.FindAll(new Predicate<Messages>(message =>
                {
                    return message.SendId == user.Id && message.MessageTypeId == 1;
                }));
            if (msgList != null && msgList.Count > 0)
            {
                //判断消息是否为离线消息，如果是离线消息，向服务端发送修改离线消息状态的包
                foreach (Messages message in msgList)
                {
                    if (message.Id != 0)
                    {
                        PackageUpdateMessageState packageUpdateMessageState = new PackageUpdateMessageState();
                        packageUpdateMessageState.MessageId = message.Id;
                        ClientManager.Instance.ClientSendData(packageUpdateMessageState);
                    }
                    //显示消息
                    this.rtbShowMessage.AppendText(string.Format(
                            "{0} ({1}) {2} {3}{4}{3}{3}", user.Name, user.Id,
                            message.MessageTime.ToString("HH:mm:ss"), "\r\n", message.Message));
                    MsgContext.MsgList.Remove(message);//从消息池中把消息移除
                }
                msgList.Clear();
            }

            ///＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝远程协助=====================================================
            List<PackageMonitor> monitorList = MsgContext.MonitorMsgList.FindAll(new Predicate<PackageMonitor>(monitor =>
            {
                return monitor.SendId == user.Id;
            }));
            if (monitorList != null && monitorList.Count > 0)
            {
                //判断消息是否为离线消息，如果是离线消息，向服务端发送修改离线消息状态的包
                PackageMonitor obj = monitorList[monitorList.Count - 1];
                if (obj.Typemsg == "ask")
                {
                    tabPage5.Parent = tabControl1;
                    tabControl1.SelectedTab = tabPage5;
                    monitoraskpanel10.Visible = true;
                    _MonitorRemoteport = obj.CPort;
                }
                else if (obj.Typemsg == "ok")
                {
                    tabPage5.Parent = tabControl1;
                    tabControl1.SelectedTab = tabPage5;
                    monitorcontrolingpanel10.Visible = true;
                    monitoraskingpanel10.Visible = false;
                }
                else if (obj.Typemsg == "no")
                {
                    if (!monitorcontrolingpanel10.Visible && monitoraskingpanel10.Visible)
                        tabPage5.Parent = null;
                    monitoraskpanel10.Visible = false;
                    rtbShowMessage.AppendText("系统消息：对方拒绝远程协助!\r\n");
                }
                else if (obj.Typemsg == "end")
                {
                    if (obj.MorC == "ctom")
                    {
                        if (!(monitfrm == null))
                        {
                            if (!monitoraskingpanel10.Visible && !monitoraskpanel10.Visible && monitorcontrolingpanel10.Visible)
                                tabPage5.Parent = null;
                            monitfrm.Close();
                            rtbShowMessage.AppendText("系统消息：对方结束了远程协助!\r\n");
                        }
                    }
                    else if (obj.MorC == "mtoc")
                    {
                        if (!monitoraskingpanel10.Visible && !monitoraskpanel10.Visible)
                            tabPage5.Parent = null;
                        monitorcontrolingpanel10.Visible = false;
                        rtbShowMessage.AppendText("系统消息：对方结束了远程协助!\r\n");
                    }
                }
                else if (obj.Typemsg == "nofriend")
                {
                    if (!monitorcontrolingpanel10.Visible && monitoraskingpanel10.Visible)
                        tabPage5.Parent = null;
                    monitoraskpanel10.Visible = false;
                    rtbShowMessage.AppendText("系统消息：对不起，你还不是对方的联系人，无法进行远程协助！\r\n");
                }
                foreach (PackageMonitor m in monitorList)
                {
                    MsgContext.MonitorMsgList.Remove(m);//从消息池中把消息移除
                }
                monitorList.Clear();
            }

            if (!AppContext.ChatStateDict.ContainsKey(user.Id))
            {
                ChatState cs = new ChatState();
                cs.ChatCount = 0;
                cs.ChatCountFromLastForbiden = 0;
                cs.isForbiden = false;
                if (cs.FirstChatTime == null)
                    cs.FirstChatTime = DateTime.Now;
                if (cs.LastChatTime == null)
                    cs.LastChatTime = DateTime.Now;
                AppContext.ChatStateDict.Add(user.Id, cs);
            } 
            CheckForbiden(0);
            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        #endregion

        #region 方法

        private bool CheckForbiden(int c)
        {
            if (AppContext.ChatStateDict[user.Id].ChatCount > 100 && DateTime.Now < AppContext.ChatStateDict[user.Id].FirstChatTime.AddDays(1))
            {
                rtbShowMessage.AppendText("防聊天提示：对不起，你在1天之内连续和对方说话已经100次以上!过会再试!\r\n\r\n");
                isFobidenlabel.Text = "是";
                isFobidenlabel.ForeColor = Color.Red;
                lastchattimelabel11.Text = "在" + AppContext.ChatStateDict[user.Id].FirstChatTime.AddDays(1).ToLongDateString() + AppContext.ChatStateDict[user.Id].FirstChatTime.AddDays(1).ToLongTimeString() + "之前";
                chatfobidencountlabel12.Text = "还有" + (101 - AppContext.ChatStateDict[user.Id].ChatCountFromLastForbiden) + "条可发";
                chatcountlabel13.Text = "已发总数:" + AppContext.ChatStateDict[user.Id].ChatCount;
                return false;
            }
           if (AppContext.ChatStateDict[user.Id].isForbiden)
            {
                rtbShowMessage.AppendText("防聊天提示：对不起，你在1个小时之内连续说话30次以上!过会再试!\r\n\r\n");
                isFobidenlabel.Text = "是";
                isFobidenlabel.ForeColor = Color.Red;
               return false;
            }
            if (DateTime.Now < AppContext.ChatStateDict[user.Id].FirstChatTime.AddHours(1))
            {
                if (AppContext.ChatStateDict[user.Id].ChatCountFromLastForbiden > 30)
                {
                    rtbShowMessage.AppendText("防聊天提示：对不起，你在1个小时之内连续说话30次以上!过会再试!\r\n\r\n");
                    isFobidenlabel.Text = "是";
                    isFobidenlabel.ForeColor = Color.Red;
                    lastchattimelabel11.Text = "在" + AppContext.ChatStateDict[user.Id].FirstChatTime.AddHours(1).ToLongDateString() + "  " + AppContext.ChatStateDict[user.Id].FirstChatTime.AddHours(1).ToLongTimeString() + "之前";
                    chatfobidencountlabel12.Text = "还有" + (31 - AppContext.ChatStateDict[user.Id].ChatCountFromLastForbiden) + "条可发";
                    chatcountlabel13.Text = "已发总数:" + AppContext.ChatStateDict[user.Id].ChatCount;
                    return false;
                }
                AppContext.ChatStateDict[user.Id].ChatCountFromLastForbiden += c;
                AppContext.ChatStateDict[user.Id].ChatCount+=c;

            }
            else
            {
                AppContext.ChatStateDict[user.Id].FirstChatTime = DateTime.Now;
                AppContext.ChatStateDict[user.Id].ChatCountFromLastForbiden = 1;
            }
            isFobidenlabel.Text = "否";
            isFobidenlabel.ForeColor = Color.Green;
            lastchattimelabel11.Text = "在" + AppContext.ChatStateDict[user.Id].FirstChatTime.AddHours(1).ToLongDateString() + AppContext.ChatStateDict[user.Id].FirstChatTime.AddHours(1).ToLongTimeString() + "之前";
            chatfobidencountlabel12.Text = "还有" + (31 - AppContext.ChatStateDict[user.Id].ChatCountFromLastForbiden) + "条可发";
            chatcountlabel13.Text = "已发总数:" + AppContext.ChatStateDict[user.Id].ChatCount;
            return true;
        }

        private delegate void dd();
        private void CheckCTC()
        {
            if (user.Status == 1)
                try
                {
                    c = new CtoCManager(IPAddress.Parse(user.LastLoginIp), user.LastLoginPort);
                    canctc = true;
                }
                catch
                {
                    try
                    {
                        dd a = delegate
                        {
                            rtbShowMessage.AppendText("系统消息：对方不在线或者有防火墙，不能直接连接，消息将进行服务器中转!\r\n\r\n");
                            canctc = false;
                        };
                        rtbShowMessage.Invoke(a);
                    }
                    catch { }
                }
        }

        /// <summary>
        /// 显示相应的QQ秀
        /// </summary>
        /// <param name="user">用户</param>
        private void QQShow(Users user)
        {
            if (user.Gender == "男")
            {
                //this.picFriendShow.Image = AppContext.StatusResource.GetObject("qqshow2_boy") as Image;
            }
            else
            {
               // this.picFriendShow.Image = AppContext.StatusResource.GetObject("qqshow2_girl") as Image;
            }
            if(AppContext.LoginUser.Gender == "男")
            {
               // this.picMyShow.Image = AppContext.StatusResource.GetObject("qqshow2_boy") as Image;
            }
            else
            {
               // this.picMyShow.Image = AppContext.StatusResource.GetObject("qqshow2_girl") as Image;
            }
        }

        /// <summary>
        /// 更新好友状态
        /// </summary>
        /// <param name="obj"></param>
        void Instance_FriendStatusEvent(PackageFriendStatus obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if ((this.Tag as Users).Id == obj.QQNum)
                    {
                        string st = "";
                        ImageClass imgclss = new ImageClass(Functions.byteArrayToImage((this.Tag as Users).FaceImage));
                        Image img = imgclss.GetReducedImage(80, 80);
                        if (obj.StatusID == 1)//上线
                        {
                            try
                            {
                                c = new CtoCManager(IPAddress.Parse(user.LastLoginIp), user.LastLoginPort);
                                canctc = true;
                            }
                            catch
                            {
                                rtbShowMessage.AppendText("（系统消息：对方上线了，但对方有防火墙，不能直接连接，消息将进行服务器中转!）\r\n");
                                canctc = false;
                            }
                            user.LastLoginIp = obj.LastLoginIp;
                            user.LastLoginPort = obj.LastLoginPort;
                            this.showFriendHead.HeadImage = img;                             
                            this.lklNickName.Text = string.Format("{0} ({1})", (this.Tag as Users).Name, (this.Tag as Users).Id) + st;
                        }
                        else if (obj.StatusID == 5)//下线
                        {
                            this.showFriendHead.HeadImage = Functions.WhiteAndBlack((Bitmap)(img));
                            st = "(离线状态）"; 
                            this.lklNickName.Text = string.Format("{0} ({1})", (this.Tag as Users).Name, (this.Tag as Users).Id) + st;
                        }
                    }
                }));
            }
          }

        #endregion

        #region 事件

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        private void btn_SendMsg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rtbSend.Text.Trim()))
                return;
            if (CheckForbiden(1))
            {
                PackageMessage packageMessage = new PackageMessage()
                {
                    Message = new Messages()
                    {
                        SendId = AppContext.LoginUser.Id,
                        RecieveId = (this.Tag as Users).Id,
                        Message = this.rtbSend.Text,
                        MessageState = 0,
                        MessageTime = DateTime.Now,
                        MessageTypeId = 1
                    },
                };
                if ((this.Tag as Users).Status == 1 && canctc)
                {
                    c.ClientSendData(packageMessage);
                }
                else
                {
                    ClientManager.Instance.ClientSendData(packageMessage);
                }
                this.rtbShowMessage.AppendText(string.Format(
                            "{0} ({1}) {2} {3}{4}{3}{3}", AppContext.LoginUser.Name, AppContext.LoginUser.Id,
                            DateTime.Now.ToString("HH:mm:ss"), "\r\n", this.rtbSend.Text));
                rtbShowMessage.DetectUrls = true;
                this.rtbSend.ResetText();
                try
                {
                    AppContext.db.ExeSQL("insert into Messages(MyId,FriendId,SendId,RecieveId,Message,MessageTypeId,MessageTime) values(" +
                       AppContext.LoginUser.Id + "," +
                        user.Id + "," +
                        packageMessage.Message.SendId + "," +
                        packageMessage.Message.RecieveId + ",'" +
                        packageMessage.Message.Message + "'," +
                        packageMessage.Message.MessageTypeId + ",'" +
                        packageMessage.Message.MessageTime + "')");
                }
                catch { }
            }
        }

        /// <summary>
        /// 使导航条始终都处于最底端
        /// </summary>
        private void rtbShowMessage_TextChanged(object sender, EventArgs e)
        {
            //设置文本框选定的起始点
            rtbShowMessage.Focus();
            rtbShowMessage.Select(rtbShowMessage.Text.Length, 0);
           // this.rtbShowMessage.SelectionStart = this.rtbShowMessage.GetFirstCharIndexFromLine(this.rtbShowMessage.Lines.Length - 1);
            //滚动到起始点位置
            this.rtbShowMessage.ScrollToCaret();
            this.rtbSend.Select();
        }

        /// <summary>
        /// 按下Ctrl键+回车键时发送消息
        /// </summary>
        private void FrmChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                this.btn_SendMsg_Click(sender, e);
            }
        }

        #endregion

        #region 自定义事件的处理
        

        /// <summary>
        /// 收到消息后，直接将消息显示在窗体上
        /// </summary>
        /// <param name="obj">消息包</param>
        void MsgContext_MessageChatEvent(PackageMessage obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                    {
                        if (this.Tag is Users)
                        {
                            Users user = this.Tag as Users;
                            this.rtbShowMessage.AppendText(string.Format(
                                "{0} ({1}) {2} {3}{4}{3}{3}", user.Name, user.Id,
                                obj.Message.MessageTime.ToString("HH:mm:ss"), "\r\n", obj.Message.Message));
                        }
                    }));
            }
        }

         /// <summary>
        /// 收到消息后，直接将消息显示在窗体上
        /// </summary>
        /// <param name="obj">消息包</param>
        void MsgContext_FilesTransChatEvent(PackageFilesTrans packageFilesTrans)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                    {
                        if (packageFilesTrans.Error == "notFriend")
                        {
                            rtbShowMessage.AppendText("系统消息：对不起，你不是对方的联系人，不能发送文件给对方。\r\n");
                            tabPage2.Parent = null;
                            return;
                        }
                       // timer2.Enabled=true;
                    }));
            }
        }

        /// <summary>
        /// 文件传送完成
        /// </summary>
        /// <param name="obj">消息包</param>
        void Instance_FilesTransFinishedEventEvent(PackageFilesTransFinished packageFilesTransfinished)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                    {
                        if (packageFilesTransfinished.SendId==user.Id)
                        {

                            if (listBox1.Items.Contains(packageFilesTransfinished.FileName))
                                listBox1.Items.Remove(packageFilesTransfinished.FileName);
                            if (packageFilesTransfinished.Success)
                                listBox2.Items.Add(packageFilesTransfinished.FileName);
                            else
                                listBox3.Items.Add(packageFilesTransfinished.FileName);
                            if (listBox1.Items.Count <= 0)
                            {
                                if(!(tabPage3.Parent== null))
                                {
                                    rtbShowMessage.AppendText("系统消息：所有文件都已经传送完成！共" + listBox3.Items.Count + "个文件传送失败！\r\n");
                                if(listBox3.Items.Count<=0)
                                  tabPage3.Parent = null;
                                }
                            }
                            return;
                        }
                       // timer2.Enabled=true;
                    }));
            }
        }

        /// <summary>
        /// 收到消息后，直接将消息显示在窗体上
        /// </summary>
        /// <param name="obj">消息包</param>
        void MsgContext_MonitorChatEvent(PackageMonitor obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (obj.Typemsg == "ask")
                    {
                        tabPage5.Parent = tabControl1;
                        tabControl1.SelectedTab = tabPage5;
                        monitoraskpanel10.Visible = true;
                        _MonitorRemoteport = obj.CPort;
                    }
                    else if (obj.Typemsg == "ok")
                    {
                        tabPage5.Parent = tabControl1;
                        tabControl1.SelectedTab = tabPage5;
                        monitorcontrolingpanel10.Visible = true;
                        monitoraskingpanel10.Visible = false;
                    }
                    else if (obj.Typemsg == "no")
                    {
                        if (!monitorcontrolingpanel10.Visible && monitoraskingpanel10.Visible)
                            tabPage5.Parent = null;
                        monitoraskpanel10.Visible = false;
                        rtbShowMessage.AppendText("系统消息：对方拒绝远程协助!\r\n");
                    }
                    else if (obj.Typemsg == "end")
                    {
                        if (obj.MorC == "ctom")
                        {
                            if (!(monitfrm == null))
                            {
                                if (!monitoraskingpanel10.Visible && !monitoraskpanel10.Visible && monitorcontrolingpanel10.Visible)
                                    tabPage5.Parent = null;
                                monitfrm.stop = true;
                                monitfrm.Close();
                                rtbShowMessage.AppendText("系统消息：对方(被控端)结束了远程协助!\r\n");
                                this.Refresh();
                                Application.DoEvents(); 
                            }
                        }
                        else if (obj.MorC == "mtoc")
                        {
                            if (!monitoraskingpanel10.Visible && !monitoraskpanel10.Visible)
                                tabPage5.Parent = null;
                            monitorcontrolingpanel10.Visible = false;
                            rtbShowMessage.AppendText("系统消息：远程协助已经结束!\r\n");
                        }
                    }
                    else if (obj.Typemsg == "nofriend")
                    {
                        if (!monitorcontrolingpanel10.Visible && monitoraskingpanel10.Visible)
                            tabPage5.Parent = null;
                        monitoraskpanel10.Visible = false;
                        rtbShowMessage.AppendText("系统消息：对不起，你还不是对方的联系人，无法进行远程协助！\r\n");
                    }

                }));
            }
        }

        

        #endregion

        private void btn_SendMsg_Load(object sender, EventArgs e)
        {
        }

        private void FrmChat_Load(object sender, EventArgs e)
        {
        }
        ///＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝发送文件模块＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void toolButton2_Click(object sender, EventArgs e)
        {
            if (!(user.Status == 1))
            {
                rtbShowMessage.AppendText("系统消息：对方不在线，不能发送文件！\r\n");
                return;
            }
            openFileDialog1.Filter = "所有文件|*.*";
            openFileDialog1.InitialDirectory = "c:\\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fname = openFileDialog1.FileName;
                PackageFilesTrans packagefilestrans = new PackageFilesTrans();
                packagefilestrans.FileName = fname;
                packagefilestrans.FileOrPath = "file";
                packagefilestrans.FileSize = Functions.FileSize(fname);
                packagefilestrans.SanderOrReveived = "sender";
                packagefilestrans.SendId = AppContext.LoginUser.Id;
                packagefilestrans.RecieveId = user.Id;
                packagefilestrans.SenderIP = AppContext.LoginUser.LastLoginIp;
                packagefilestrans.SenderPort = AppContext.LoginUser.LastLoginPort;
                packagefilestrans.FilesTransPort = _filetransport;
             
                if (canctc)
                {
                    c.ClientSendData(packagefilestrans);
                    listBox1.Items.Add(fname);
                    tabPage3.Parent = tabControl1;
                    tabControl1.SelectedTab = tabPage3;
                }
                else
                {
                    rtbShowMessage.AppendText("系统消息：对方有防火墙，无法传文件！\r\n");
                }
              }
        }

        public void toolButton3_Click(object sender, EventArgs e)
        {
            if (!(user.Status == 1))
            {
                rtbShowMessage.AppendText("系统消息：对方不在线，不能发送文件！\r\n");
                return;
            }
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "请选择要传送的文件夹！";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string pathname = folderBrowserDialog1.SelectedPath;
                PackageFilesTrans packagefilestrans = new PackageFilesTrans();
                packagefilestrans.PathName = pathname;
                packagefilestrans.FileOrPath = "path";
                int fnunb = 0;
                packagefilestrans.FileSize = Functions.GetDirectoryLength(pathname, ref fnunb);
                if (fnunb > 50)
                {
                    rtbShowMessage.AppendText("系统消息：本系统采用多线程设计，文件夹传输速度快，但该文件夹中文件数量多于50个，建议压缩后以单文件传送。\r\n");
                    return;
                }
                packagefilestrans.SanderOrReveived = "sender";
                packagefilestrans.SendId = AppContext.LoginUser.Id;
                packagefilestrans.RecieveId = user.Id;
                packagefilestrans.SenderIP = AppContext.LoginUser.LastLoginIp;
                packagefilestrans.SenderPort = AppContext.LoginUser.LastLoginPort;
                packagefilestrans.FilesTransPort = _filetransport;
                //  rtbShowMessage.AppendText("系统消息：发给接收者本地用于文件的_filetransport" + _filetransport);
                if (canctc)
                {
                    c.ClientSendData(packagefilestrans);
                    TConst.FilesList fl = new TConst.FilesList();
                    foreach (string fn in fl.GetAllFileName(pathname))
                    {
                        listBox1.Items.Add(fn);
                    }
                    tabPage3.Parent = tabControl1;
                    tabControl1.SelectedTab = tabPage3;
                }
                else { rtbShowMessage.AppendText("系统消息：对方有防火墙，无法传文件！\r\n"); }
            }
            
        }


        private void GetfileEvent(string fname, long flength, long currentpos)
        {
            //string name=Path.GetFileName(fname)+flength.ToString();
            //Boolean exist = false;
            //tabPage3.Parent = tabControl1;
            //foreach (ProgressBar pb in panel1.Controls)
            //{
            //    if (pb.Tag == fname)
            //    {
            //        exist = true;
            //        int pos = Convert.ToInt32(((double)currentpos / (double)flength) * 100);
            //        pb.Value = pos;
            //        System.Drawing.Font font = new System.Drawing.Font("微软雅黑", (float)8, System.Drawing.FontStyle.Regular);
            //        System.Drawing.PointF pointF = new System.Drawing.PointF(1, pb.Height / 2 - 10);
            //        pb.CreateGraphics().DrawString(Path.GetFileName(fname), font, System.Drawing.Brushes.Blue, pointF);
            //        if (flength == currentpos)
            //        {
            //            rtbShowMessage.AppendText(string.Format(
            //                    "{1}{0}:文件传送提示：{1}“{2}”已经传送完毕.",
            //                    DateTime.Now.ToShortTimeString(),
            //                    "\r\n",
            //                   Path.GetFileName(fname)
            //                    ));
            //            pb.Visible = false;
            //            pb.Dispose();
            //        }
            //    }
            //}
            //if (!exist)
            //{
            //    ProgressBar pb = new ProgressBar();
            //    pb.Tag = fname;
            //    pb.Value = 0;
            //    pb.Maximum = 100;
            //    pb.Parent = panel1;
            //    pb.Dock = DockStyle.Top;

            //}
 
        }


        private void WaitForConnect()
        {
            _filetransport+=1;//获取本机端点
            while (true)
            {//通过获取异常然后改变端口使本机可以运行多个本程序实例
                try
                {
                    TListener lis = new TListener(_filetransport);
                    lis.StartListening();
                    lis.GetFile += new Action<string, long, long>(GetfileEvent); 
                    break;
                }
                catch(Exception e)
                {
                    _filetransport += 1;
                    if (_filetransport > 11555)
                    {
                        rtbShowMessage.AppendText("系统消息：文件发送失败，原因是：" + e.ToString() + "\r\n");
                        break;
                    }
                }
            }
        }

        private void toolButton2_Load(object sender, EventArgs e)
        {

        }
        ///＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝接收文件模块======================================
        /// <summary>
        /// 文件另存为 实现将目前的filesmsg包转成filestranslist元素并加到其中。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button4_Click(object sender, EventArgs e)
        {                     
            if(getapackage==null)
                return;
            FilesTransList ftl=new FilesTransList();
            ftl.FileOrPath=getapackage.FileOrPath;
            ftl.FilesSize=getapackage.FileSize;
            ftl.Remotepath = getapackage.PathName;
            ftl.FileNamesList=new List<string>();
           if (getapackage.FileOrPath == "path")
              {
                   folderBrowserDialog1.ShowNewFolderButton = true;
                   folderBrowserDialog1.Description = "请选择文件保存位置";
                   folderBrowserDialog1.ShowDialog();
                   ftl.Savepath = folderBrowserDialog1.SelectedPath;
                   if(ftl.Savepath=="")
                   return;
                        try
                        {                          
                            TcpClient tcp = new TcpClient();
                            tcp.Connect(_filetransRemotSenderIP, _filetransRemotSenderport);
                            TclientConnection con = new TclientConnection(tcp, getapackage.PathName,ftl,filestranslist, _filestotlenumber);
                            Thread td = new Thread(new ThreadStart(con.GetFileList));
                            td.IsBackground = true;
                            td.Start();                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

           }
           else if (getapackage.FileOrPath == "file")
           {
               saveFileDialog1.FileName = Path.GetFileName(getapackage.FileName);
               if (saveFileDialog1.ShowDialog() == DialogResult.OK)
               {
                   ftl.Savepath = saveFileDialog1.FileName;
                   ftl.FileNamesList.Add(getapackage.FileName);
                   ftl.FilesGetFinished = true;
                   filestranslist.Add(ftl);
                   _filestotlesize += getapackage.FileSize;
                   _filestotlenumber.Value += 1;

               }
           }
            getapackage=null;
            newfilepanel1.Visible = false;
            timer2.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {          

            if (filestranslist.Count <= 0)
                return;
            timer1.Enabled = false;
            this.tabPage2.Visible = true;
            tabPage2.Parent = tabControl1;
            FilesTransList fl = new FilesTransList();
            fl = filestranslist[0];
            //判断是否最后下载过的文件  如果是，则判断未下载完成的文件是否存在，存在则继续下载
          //  string fileName = fl.f;
            //if (fileName == "d:\a.txt")
            //{
            //    if (File.Exists("d:\a.txt"))
            //    {
            //        DownFile(fileName,
            //            "path",
            //            Convert.ToInt64(566545));//_sysConfig.GetIniString("LastDownFileLength", "0")
            //        return;
            //    }
            //}
            //如果不是最后下载的文件，则开始新的下载，首先得到文件长度
            try
            {
                    foreach (string fileName in fl.FileNamesList)
                    {
                        long fileLength = GetFileLength(fileName);
                        if (fileLength == 0)
                            continue;
                        string destFileName = "";
                        if (fl.FileOrPath == "file")
                            destFileName = CreateNewFile(fl.Savepath);
                        string s="";
                        if (fl.FileOrPath == "path")
                        {
                            s=(fl.Savepath + "\\" + fileName.Substring(fl.Remotepath.Length + 1));
                            destFileName = CreateNewFile(s);
                        } 
                        if (destFileName == "")
                            continue;
                        //transingnamelabel2.Text = "正在传送：" + Path.GetFileName(fileName);
                        // leftfielsnumlabel1.Text = "待传文件总数：" + filestranslist.Count.ToString();
                        //  leftfilessizelable.Text = "待传总大小：" + (Math.Round((decimal)(_filestotlesize - _filesfinishedsize) / 1048576, 2)).ToString() + "M";
                        if (fl.FileOrPath == "file")
                            DownFile(fileName, fl.Savepath, fileLength);
                        if (fl.FileOrPath == "path")
                            DownFile(fileName,s, fileLength);
                        _filesfinishedsize += fileLength;
                        //   finishednumlabel7.Text = "完成文件总数：" + _filesfinishednumber.ToString();
                        //   label5.Text = "已完成文件总大小：" + (Math.Round((decimal)( _filesfinishedsize) / 1048576, 2)).ToString() + "M";
                    }
                    filestranslist.Remove(fl);          
            }
            catch (Exception ex) { rtbShowMessage.AppendText("系统消息：文件传送出错，信息：" + ex.ToString() + "\r\n"); }
            timer1.Enabled = true;
        }

        private long GetFileLength(string fileName)
        {
            TcpClient tcp = null;
            NetworkStream stream = null;
            try
            {
                #region 建立连接，与发送请求文件长度的命令
                tcp = new TcpClient();
                tcp.Connect(_filetransRemotSenderIP, _filetransRemotSenderport);
                stream = tcp.GetStream();
                TCommand command = new TCommand(CommandStyleEnum.cGetFileLength);
                command.AppendArg(fileName);
                byte[] data = command.ToBytes();
                stream.Write(data, 0, data.Length);
                #endregion

                #region 接收数据
                byte[] recData = new byte[1024];
                int recLen = stream.Read(recData, 0, recData.Length);
                command = new TCommand(recData, recLen);
                #endregion

                #region 转换文件长度
                if (command.commandStyle != CommandStyleEnum.cGetFileLengthReturn)
                    return 0;

                if (command.argList.Count == 0)
                    return 0;

                long fileLength = 0;
                try
                {
                    fileLength = Convert.ToInt64((string)command.argList[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
                #endregion

                return fileLength;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                #region 释放
                if (stream != null)
                    stream.Close();
                if (tcp != null)
                    tcp.Close();
                #endregion
            }
        }

        private string CreateNewFile(string sourceFileName)
        {
            saveFileDialog1.FileName = sourceFileName;
            if (!Directory.Exists(Path.GetDirectoryName(sourceFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(sourceFileName));
            }
            FileStream s = null;
            try
            {
                s = new FileStream(sourceFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                s.SetLength(0);
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
            return sourceFileName;
        }


        private void DownFile(string fileName, string path, long fileLength)
        {
            #region 保存最后下载的信息
            #endregion

            #region 初始化进度条
            FileProgress ap = new FileProgress();
            ap.Parent = transingpanel2;
            ap.Dock = DockStyle.Top;

            #endregion

            #region 启动下载线程
            try
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect(_filetransRemotSenderIP, _filetransRemotSenderport);
                CtoCManager c = new CtoCManager(IPAddress.Parse(user.LastLoginIp), user.LastLoginPort);
                TclientConnection con = new TclientConnection(tcp, fileName, path, fileLength, ap, rtbShowMessage,c, _filestotlenumber, _filesfinishednumber,tabPage2);
                Thread t = new Thread(new ThreadStart(con.GetFile));
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            ///获取文件传输池中的消息
            List<PackageFilesTrans> filesmsgList = MsgContext.FilesMsgList.FindAll(new Predicate<PackageFilesTrans>(packageFilesTrans =>
            {
                return packageFilesTrans.SendId == user.Id;
            }));
            if (filesmsgList != null && filesmsgList.Count > 0)
            {
                timer2.Enabled = false;
                getapackage = filesmsgList[0];
                if (getapackage.Error == "notFriend")
                {
                    rtbShowMessage.AppendText("系统消息：对不起，你不是对方的联系人，不能发送文件给对方。\r\n");
                    tabPage3.Parent = null;
                    MsgContext.FilesMsgList.Remove(getapackage);  
                    timer2.Enabled = true;
                    return;
                }
                
                

                _filetransRemotSenderIP = getapackage.SenderIP;
                _filetransRemotSenderport = getapackage.FilesTransPort;
                this.namelabel8.Text = "名称：";
                sizelabelrereseived.Text = "文件总大小：" + (Math.Round((decimal)getapackage.FileSize / 1048576, 2)).ToString() + "M";
                if (getapackage.FileOrPath == "path")
                {
                    fileorpathlabel9.Text = "类型：文件夹";
                    this.namelabel8.Text = Path.GetDirectoryName(getapackage.PathName);
                }
                else if (getapackage.FileOrPath == "file")
                {
                    fileorpathlabel9.Text = "类型：单文件";
                    this.namelabel8.Text += Path.GetFileName(getapackage.FileName);
                }
                MsgContext.FilesMsgList.Remove(getapackage);                
                tabPage2.Parent = tabControl1;
                tabControl1.SelectedTab = tabPage2;
                newfilepanel1.Visible = true;
            }
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {
                    
        }

        private void button6_Click(object sender, EventArgs e)
        {
            getapackage = null;
            newfilepanel1.Visible = false;
            timer2.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

        }

        private void FrmChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                if(MessageBox.Show("系统消息：传给对方的文件还没传完，确定要退出吗？","",MessageBoxButtons.YesNo)==DialogResult.No)
                   e.Cancel = true;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
        }

        private void 重新发送ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(user.Status == 1))
            {
                rtbShowMessage.AppendText("系统消息：对方不在线，不能发送文件！\r\n");
                return;
            }
           
            ///string filename = openFileDialog1.FileName;
            if (listBox3.SelectedIndex>-1)
            {
                string fname = listBox3.Items[listBox3.SelectedIndex].ToString();
                PackageFilesTrans packagefilestrans = new PackageFilesTrans();
                packagefilestrans.FileName = fname;
                packagefilestrans.FileOrPath = "file";
                packagefilestrans.FileSize = Functions.FileSize(fname);
                packagefilestrans.SanderOrReveived = "sender";
                packagefilestrans.SendId = AppContext.LoginUser.Id;
                packagefilestrans.RecieveId = user.Id;
                packagefilestrans.SenderIP = AppContext.LoginUser.LastLoginIp;
                packagefilestrans.SenderPort = AppContext.LoginUser.LastLoginPort;
                packagefilestrans.FilesTransPort = _filetransport;
                //  rtbShowMessage.AppendText("系统消息：发给接收者本地用于文件的_filetransport" + _filetransport);
                if (canctc)
                {
                    c.ClientSendData(packagefilestrans);
                    listBox3.Items.Remove(fname);
                    listBox1.Items.Add(fname);
                }
                else { rtbShowMessage.AppendText("系统消息：对方有防火墙，不能传文件。\r\n"); }
               }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabPage4.Parent == null)
            {
                tabPage4.Parent = tabControl1;
                tabControl1.SelectedTab = tabPage4;
                this.Width += 200;
                richTextBox1.Clear();
                DataSet DS = new DataSet();
                DS=AppContext.db.ReturnDataSet("SELECT * FROM Messages WHERE Myid="+AppContext.LoginUser.Id+" and FriendId="+user.Id+" order by id asc ");
                richTextBox1.DetectUrls = true;
                foreach (DataRow dr in DS.Tables[0].Rows)
                {
                    if ((int.Parse(dr["SendId"].ToString())) ==AppContext.LoginUser.Id)
                    {
                        this.richTextBox1.AppendText(string.Format(
                                "{0} ({1}) {2} {3}{4}{3}{3}", AppContext.LoginUser.Name, AppContext.LoginUser.Id,
                               DateTime.Parse(dr["MessageTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"), "\r\n", dr["Message"].ToString()));
                    }
                    if ((int.Parse(dr["SendId"].ToString())) == user.Id)
                    {
                        this.richTextBox1.AppendText(string.Format(
                                "{0} ({1}) {2} {3}{4}{3}{3}", user.Name, user.Id,
                               DateTime.Parse(dr["MessageTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"), "\r\n", dr["Message"].ToString()));
                    }

                }
            }
            else {
                tabPage4.Parent = null;
               // tabControl1.SelectedTab = tabPage1;
                this.Width -= 200;
            }
        }

        private void myButton1_Load(object sender, EventArgs e)
        {

        }

        private void myButton2_Load(object sender, EventArgs e)
        {

        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "和" + user.Name + "的对话记录.doc";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
            }
        }

        private void myButton2_Click(object sender, EventArgs e)
        {
            AppContext.db.ExeSQL("delete from messages where  Myid=" + AppContext.LoginUser.Id + " and FriendId=" + user.Id);
            richTextBox1.Clear();
            rtbShowMessage.AppendText("系统消息：对话记录清除成功！\r\n"); 
        }

        private void waitformonitorconnect()
        {
            _Monitorlocalport = 12000;//获取本机端点
            while (true)
            {//通过获取异常然后改变端口使本机可以运行多个本程序实例
                try
                {
                    if (channel == null)
                    {
                        channel = new TcpServerChannel(_Monitorlocalport);
                        try
                        {
                            ChannelServices.RegisterChannel(channel, false);
                        }
                        catch {
                            TcpServerChannel channel1 = (TcpServerChannel)ChannelServices.GetChannel("tcp");
                            ChannelServices.UnregisterChannel(channel1);
                            ChannelServices.RegisterChannel(channel, false);
                        }
                        RemotingConfiguration.RegisterWellKnownServiceType(typeof(QQControls.Monitor), "MonitorServerUrl", WellKnownObjectMode.SingleCall);
                        PackageMonitor packagemonitor = new PackageMonitor();
                        packagemonitor.CPort = _Monitorlocalport;
                        packagemonitor.MorC = "ctom";
                        packagemonitor.Typemsg = "ask";
                        packagemonitor.SendId = AppContext.LoginUser.Id;
                        packagemonitor.RecieveId = user.Id;
                        packagemonitor.SenderIP = AppContext.LoginUser.LastLoginIp;
                        packagemonitor.SenderPort = AppContext.LoginUser.LastLoginPort;

                            c.ClientSendData(packagemonitor);
                        }
                    break;
                }
                catch (Exception er)
                {
                    _Monitorlocalport += 1;
                    if (_Monitorlocalport > 15000)
                    {
                        rtbShowMessage.AppendText("系统消息：无法开启远程，原因是：" + er.ToString() + "\r\n");
                        break;
                    }
                }
            }


        }

        public void toolButton4_Click(object sender, EventArgs e)
        {
            if ((channel == null))
            {
                if (canctc)
                {
                   t1 = new Thread(new ThreadStart(waitformonitorconnect));
                   t1.IsBackground = true;
                    t1.Start();
                    tabPage5.Parent = tabControl1;
                    tabControl1.SelectedTab = tabPage5;
                    monitoraskingpanel10.Visible = true;
                }
                else { rtbShowMessage.AppendText("系统消息：对方有防火墙，不能进行远程协助\r\n"); }

            }
            else
            {
                PackageMonitor packagemonitor = new PackageMonitor();
                packagemonitor.CPort = _Monitorlocalport;
                packagemonitor.MorC = "ctom";
                packagemonitor.Typemsg = "ask";
                packagemonitor.SendId = AppContext.LoginUser.Id;
                packagemonitor.RecieveId = user.Id;
                packagemonitor.SenderIP = AppContext.LoginUser.LastLoginIp;
                packagemonitor.SenderPort = AppContext.LoginUser.LastLoginPort;
                //  rtbShowMessage.AppendText("系统消息：发给接收者本地用于文件的_filetransport" + _filetransport);                   
                
                if (canctc)
                {
                    c.ClientSendData(packagemonitor);
                    tabPage5.Parent = tabControl1;
                    tabControl1.SelectedTab = tabPage5;
                    monitoraskingpanel10.Visible = true;
                }
                else { rtbShowMessage.AppendText("系统消息：对方有防火墙，不能进行远程协助\r\n"); }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
             MonitorClient monitfrm1 = new MonitorClient(user.LastLoginIp, _MonitorRemoteport);
            monitfrm = monitfrm1;
            monitfrm.remoteip = user.LastLoginIp;
            monitfrm.remoteport = _MonitorRemoteport;
            monitfrm.remotemsgport = user.LastLoginPort;
            monitfrm.remotefriend = user;
            monitfrm.Show(null);
            monitfrm.Text = "您正在远程控制"+user.Name+"("+user.Id+")"+"的电脑。";
            PackageMonitor packagemonitor = new PackageMonitor();
            //  packagemonitor.CPort = monitorport;
            packagemonitor.MorC = "mtoc";
            packagemonitor.Typemsg = "ok";
            packagemonitor.SendId = AppContext.LoginUser.Id;
            packagemonitor.RecieveId = user.Id;
            packagemonitor.SenderIP = AppContext.LoginUser.LastLoginIp;
            packagemonitor.SenderPort = AppContext.LoginUser.LastLoginPort;
            if (canctc)
            {
                c.ClientSendData(packagemonitor);
            }
            else {

                rtbShowMessage.AppendText("系统消息：对方有防火墙，不能远程!\r\n");
                ClientManager.Instance.ClientSendData(packagemonitor);
            }
            monitoraskpanel10.Visible = false;
            if (!monitoraskingpanel10.Visible && !monitorcontrolingpanel10.Visible)
                tabPage5.Parent = null;
            }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (!monitoraskingpanel10.Visible && !monitorcontrolingpanel10.Visible)
              tabPage5.Parent = null;
            monitoraskpanel10.Visible = false;
            PackageMonitor packagemonitor = new PackageMonitor();
            //  packagemonitor.CPort = monitorport;
            packagemonitor.MorC = "mtoc";
            packagemonitor.Typemsg = "no";
            packagemonitor.SendId = AppContext.LoginUser.Id;
            packagemonitor.RecieveId = user.Id;
            packagemonitor.SenderIP = AppContext.LoginUser.LastLoginIp;
            packagemonitor.SenderPort = AppContext.LoginUser.LastLoginPort;
           if(canctc){c.ClientSendData(packagemonitor);
            }else{
                rtbShowMessage.AppendText("系统消息：对方有防火墙，不能远程!\r\n");
                ClientManager.Instance.ClientSendData(packagemonitor);
            }
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!monitoraskpanel10.Visible && !monitoraskingpanel10.Visible)
                tabPage5.Parent = null;
            monitorcontrolingpanel10.Visible = false;
            PackageMonitor packagemonitor = new PackageMonitor();
            //  packagemonitor.CPort = monitorport;
            packagemonitor.MorC = "ctom";
            packagemonitor.Typemsg = "end";
            packagemonitor.SendId = AppContext.LoginUser.Id;
            packagemonitor.RecieveId = user.Id;
            packagemonitor.SenderIP = AppContext.LoginUser.LastLoginIp;
            packagemonitor.SenderPort = AppContext.LoginUser.LastLoginPort;
            if (canctc)
            {
                c.ClientSendData(packagemonitor);
            }
            else
            {
                rtbShowMessage.AppendText("系统消息：对方有防火墙，不能远程!\r\n");
                ClientManager.Instance.ClientSendData(packagemonitor);
            }
        }

        public void monitor()
        {
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("对不起，该功能未开发完成。");
        }

        private void tbtnVideo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("对不起，该功能未开发完成。");
        }

        private void toolButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("对不起，该功能未开发完成。");
        }

        private void toolButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("对不起，该功能未开发完成。");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("对不起，该功能未开发完成。");
        }

        private void timerEx1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
          //  this.rtbSend.Text = "X:"+Control.MousePosition.X.ToString() + ", Y:" + Control.MousePosition.Y.ToString();
        }
    }
}
