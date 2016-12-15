using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace TConst
{
    public class TCommand
    {
        private CommandStyleEnum _commandStyle;
        //命令类型
        public CommandStyleEnum commandStyle
        {
            get
            {
                return _commandStyle;
            }
        }

        private ArrayList _argList;
        public ArrayList argList
        {
            get
            {
                return _argList;
            }
        }

        public void AppendArg(string arg)
        {
            _argList.Add(arg);
        }

        public TCommand(CommandStyleEnum style)
        {
            _commandStyle = style;
            _argList = new ArrayList();
        }

        public TCommand(byte[] bytesCommand, int len)
        {
            _argList = new ArrayList();

            string strCommand = Encoding.Unicode.GetString(bytesCommand, 0, len);
            string[] list = strCommand.Split('|');

            if (list == null)
            {
                _commandStyle = CommandStyleEnum.cNone;
                return;
            }

            if (list.Length == 0)
            {
                _commandStyle = CommandStyleEnum.cNone;
                return;
            }

            for (int i = 0; i < list.Length; i++)
            {
                if (i == 0)
                {
                    try
                    {
                        int intCommandStyle = Convert.ToInt32(list[i]);
                        _commandStyle = (CommandStyleEnum)intCommandStyle;
                    }
                    catch (Exception)
                    {
                        _commandStyle = CommandStyleEnum.cNone;
                        return;
                    }
                }
                else
                {
                    _argList.Add((string)list[i]);
                }
            }
        }

        public byte[] ToBytes()
        {
            StringBuilder sb = new StringBuilder();
            int intCommand = Convert.ToInt32(commandStyle);
            sb.Append(intCommand.ToString("D3"));

            for (int i = 0; i < _argList.Count; i++)
            {
                sb.Append("|");
                sb.Append((string)_argList[i]);
            }

            return Encoding.Unicode.GetBytes(sb.ToString());
        }

    }
}
