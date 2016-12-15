using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using TConst;
using System.IO;
using System.Windows.Forms;

namespace ClientView
{
    class TserverConnection
    {
        private TcpClient _tcp;
        public TserverConnection(TcpClient tcp)
        {
            _tcp = tcp;
        }

        #region 注册
      //  public event Action<string ,long,long > GetFile;
        #endregion

        public void WaitForSendData()
        {
            NetworkStream stream = _tcp.GetStream();

            try
            {
                while (true)
                {
                    try
                    {
                        byte[] data = new byte[1024];
                        int recLen = stream.Read(data, 0, 1024);
                        if (recLen == 0)
                            break;

                        TCommand command = new TCommand(data, recLen);
                        ExtractRecStr(command, stream);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
            finally
            {
                stream.Close();
                _tcp.Close();
            }
        }

        private void ExtractRecStr(TCommand command, NetworkStream stream)
        {
            switch (command.commandStyle)
            {
                case CommandStyleEnum.cList:
                    OnGetFileList(command, stream);
                    break;
                case CommandStyleEnum.cGetFileLength:
                    OnGetFileLength(command, stream);
                    break;
                case CommandStyleEnum.cGetFile:
                    OnGetFile(command, stream);
                    break;
                default:
                    break;
            }
        }

        private void OnGetFileList(TCommand command, NetworkStream stream)
        {
            command = GetData.GetFileListCommand((string)command.argList[0]);
            byte[] data = command.ToBytes();
            stream.Write(data, 0, data.Length);
        }

        private void OnGetFileLength(TCommand command, NetworkStream stream)
        {
            long fileLength = GetData.GetFileLength((string)command.argList[0]);

            TCommand tempCommand = null;
            if (fileLength == 0)
                tempCommand = new TCommand(CommandStyleEnum.cGetFileLengthReturnNone);
            else
                tempCommand = new TCommand(CommandStyleEnum.cGetFileLengthReturn);

            tempCommand.AppendArg(fileLength.ToString());

            byte[] data = tempCommand.ToBytes();
            stream.Write(data, 0, data.Length);
        }

        private void OnGetFile(TCommand command, NetworkStream stream)
        {
            #region 根据客户端已下载的字节数来定位文件流
            long currentSize = Convert.ToInt64((string)command.argList[1]);
            long fileLength;
            long needLength;

            FileStream s = GetData.GetFileStream((string)command.argList[0]);
            if (s == null)
                return;

            fileLength = s.Length;
            if (currentSize >= fileLength)
                return;
            s.Position = currentSize;//如果已经下载了100字节,则position应该设置为100,即从100开始传输
            #endregion

            try
            {
                #region 发送数据
                while (true)
                {
                    needLength = fileLength - s.Position;
                    if (needLength == 0)
                        break;

                    if (needLength > 1024)
                        needLength = 1024;

                    byte[] data = new byte[1024];
                    int len = s.Read(data, 0, 1024);
                    if (len == 0)
                        break;

                    stream.Write(data, 0, len);
                   // GetFile((string)command.argList[0], fileLength, s.Position);
                }
                #endregion
            }
            catch (Exception)
            {
            }
            finally
            {
                s.Close();
            }
        }
    }
}
