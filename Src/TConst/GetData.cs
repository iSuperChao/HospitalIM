using System;
using System.Collections.Generic;
using System.Text;
//using HTIMM;
using System.Windows.Forms;
using System.IO;

namespace TConst
{
    public class GetData
    {
        private static List<string> GetFileList(string FilePath)
        {
            //string[] files = System.IO.Directory.GetFiles(FilePath);

            //for (int i = 0; i < files.Length; i++)
            //{
            //    files[i] = System.IO.Path.GetFileName(files[i]);
            //}

            //return files;
            FilesList fa = new FilesList();
            return fa.GetAllFileName(FilePath);

        }

        public static TCommand GetFileListCommand(string path)
        {
           // TSysConfig sysConfig = new TSysConfig();
            if (path == "-1")
            {
                MessageBox.Show("没有找到目录");
                return null;
            }

            List<string> files = new List<string>();
              files=  GetFileList(path);
            TCommand command = new TCommand(CommandStyleEnum.cListReturn);

            foreach (string s in files)
                command.AppendArg(s);

            return command;
        }

        public static long GetFileLength(string file)
        {//
          //  TSysConfig sysConfig = new TSysConfig();
            string path="" ;//= sysConfig.GetIniString("path", "-1");
            if (path == "-1")
            {
                MessageBox.Show("没有从系统配置文件中找到目录");
                return 0;
            }

            string fileName = Path.Combine(path, file);
            FileStream s = null;
            try
            {
                s = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return s.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
        }

        public static FileStream GetFileStream(string file)
        {
           // TSysConfig sysConfig = new TSysConfig();
            string path=""; //= sysConfig.GetIniString("path", "-1");
            if (path == "-1")
            {
                MessageBox.Show("没有从系统配置文件中找到目录");
                return null;
            }

            string fileName = Path.Combine(path, file);

            FileStream s = null;
            try
            {
                s = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
