using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using TConst;
using System.IO;
using QQControls;
using Entity;

namespace ClientView
{
    class TclientConnection
    {
        private TcpClient _tcp;
        private  FilesTransList _fileslist;
        private ProgressBar _p;
        private FileProgress fp;
        private string _fileName;
        private string _path;
        private string _remotepath;
        private long _fileLength;
        private RichTextBox _rtb;
        private List<FilesTransList> _filestranslist;
        private CtoCManager c;
        private NumericUpDown _ftotolnumb;
        private NumericUpDown _finishednumb;
        private TabPage _tp;

        public TclientConnection(TcpClient tcp, string rmpath, FilesTransList fileslist, List<FilesTransList> ftl, NumericUpDown filetotlenumb)
        {
            _tcp = tcp;
            _fileslist = fileslist;
            _filestranslist = new List<FilesTransList>();
            _filestranslist = ftl;
            _remotepath = rmpath;
            _ftotolnumb = filetotlenumb;

        }

        public TclientConnection(TcpClient tcp, string fileName, string path, long fileLength, FileProgress ap, RichTextBox richtb, CtoCManager ctcm, NumericUpDown _filestotlenumber, NumericUpDown _filesfinishednumber,TabPage tbpg)
        {
            _tcp = tcp;
            _fileName = fileName;
            _path = path;
            _fileLength = fileLength;
            _rtb = richtb;
            _ftotolnumb = _filestotlenumber;
            _finishednumb = _filesfinishednumber;
            _tp = tbpg;

            fp = ap;
            _p= fp.Controls["p"] as ProgressBar;
            Label l = fp.Controls["l"] as Label;
            l.Text = Path.GetFileName(fileName);
            c = ctcm;
            
            ////dd ap = delegate()
            ////   {
            //       // PointF pointF = new PointF(this.progressBar1.Width / 2 - 10, this.progressBar1.Height / 2 - 10);
            //       System.Drawing.Font font = new System.Drawing.Font("微软雅黑", (float)8, System.Drawing.FontStyle.Regular);
            //       System.Drawing.PointF pointF = new System.Drawing.PointF(1, _p.Height / 2 - 10);
            //       _p.CreateGraphics().DrawString(Path.GetFileName(fileName), font, System.Drawing.Brushes.Black, pointF);
            ////   };
            ////_p.Invoke(ap);
        }

        private delegate void dd();
        public void GetFileList()
        {
            if (_tcp == null)
                return;
            if (_fileslist == null)
                return;

            NetworkStream stream = _tcp.GetStream();
            try
            {
                TCommand command = new TCommand(CommandStyleEnum.cList);
                command.AppendArg(_remotepath);
                byte[] data = command.ToBytes();
                stream.Write(data, 0, data.Length);

                byte[] recData = new byte[9999];
                int recLen = stream.Read(recData, 0, recData.Length);

                if (recLen == 0)
                    return;
                command = new TCommand(recData, recLen);
                if (command.commandStyle != CommandStyleEnum.cListReturn)
                    return;
                    _fileslist.FileNamesList.Clear();
                    for (int i = 0; i < command.argList.Count; i++)
                    {
                        _fileslist.FileNamesList.Add((string)command.argList[i]);
                    }
                    _filestranslist.Add(_fileslist);
                    _ftotolnumb.Value+=_fileslist.FileNamesList.Count;

            }
            finally
            {
                stream.Close();
                _tcp.Close();
            }
        }


        public void GetFile()
        {
            #region 定义变量
            FileStream s;
            long currentSize;
            int p = 0;
            #endregion

            #region 获取当前下载进度
            try
            {
                s = new FileStream(_path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                currentSize = s.Length;
                s.Position = currentSize;
                p = Convert.ToInt32(((double)currentSize / (double)_fileLength) * 100);
                dd a = delegate()
                {
                    _p.Value = p;
                };
                _p.Invoke(a);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            #endregion

            #region 判断文件是否已经下载完毕
            if (currentSize == _fileLength)
            {
                MessageBox.Show("文件已经传送完毕!");
                s.Close();
                return;
            }
            #endregion

            #region 开始下载
            NetworkStream stream = _tcp.GetStream();
            try
            {
                TCommand command = new TCommand(CommandStyleEnum.cGetFile);
                command.AppendArg(_fileName);
                command.AppendArg(currentSize.ToString());
                byte[] data = command.ToBytes();
                stream.Write(data, 0, data.Length);

                while (true)
                {
                    byte[] recData = new byte[1024];
                    int recLen = stream.Read(recData, 0, 1024);
                    if (recLen == 0)//断开
                        break;

                    s.Write(recData, 0, recLen);
                    currentSize += recLen;
                    p = Convert.ToInt32(((double)currentSize / (double)_fileLength) * 100);
                    if (p != _p.Value)
                    {
                        dd a = delegate()
                        {
                            _p.Value = p;
                            fp.Visible = true;
                            Application.DoEvents();
                        };
                        _p.Invoke(a);
                    }

                    if (currentSize == _fileLength)
                    {
                        dd aa = delegate()
                    {
                        _rtb.AppendText(string.Format(
                                "{1}{0}:文件传送提示：{1}“{2}”已经传送完毕，保存在:“{3}”{1}{1}",
                                DateTime.Now.ToShortTimeString(),
                                "\r\n",
                               Path.GetFileName(_fileName),
                                _path
                                ));
                        fp.Visible = false;
                        fp.Dispose();

                    };
                        _p.Invoke(aa);
                        PackageFilesTransFinished packagefilestransfinished = new PackageFilesTransFinished();
                        packagefilestransfinished.FileName = _fileName;
                        packagefilestransfinished.Success = true;
                        packagefilestransfinished.SendId = AppContext.LoginUser.Id;
                        //packagefilestransfinished.RecieveId = user.Id;
                        //  MessageBox.Show("发给接收者本地用于文件的_filetransport" + _filetransport);
                        c.ClientSendData(packagefilestransfinished);
                        //_finishednumb.Value += 1;
                        //if (_ftotolnumb.Value == _finishednumb.Value)
                        //{
                        //    _tp.Parent = null;
                        //    MessageBox.Show("所有文件都接收完毕！");
                        //}
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                PackageFilesTransFinished packagefilestransfinished = new PackageFilesTransFinished();
                packagefilestransfinished.FileName = _fileName;
                packagefilestransfinished.Success = false;
                packagefilestransfinished.SendId = AppContext.LoginUser.Id;
                //packagefilestransfinished.RecieveId = user.Id;
                //  MessageBox.Show("发给接收者本地用于文件的_filetransport" + _filetransport);
                c.ClientSendData(packagefilestransfinished);
                //_ftotolnumb.Value -= 1;
            }
            finally
            {
                dd aap = delegate()
                    {
                        _finishednumb.Value += 1;
                    };
                _finishednumb.Invoke(aap);
                if (_ftotolnumb.Value == _finishednumb.Value)
                {

                    dd aa = delegate()
                   {
                       _tp.Parent = null;
                   };
                    _tp.Invoke(aa);
                    MessageBox.Show(_ftotolnumb.Parent, "所有文件都接收完毕！");
                }
                #region 释放
                fp.Dispose();
                stream.Close();
                _tcp.Close();
                s.Close();
                #endregion
            }
            #endregion
        }
    }
}
