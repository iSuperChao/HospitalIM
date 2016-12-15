using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ServerView
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool rec;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out rec);
            if (rec)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
            else
            {
                MessageBox.Show("服务端程序已开启！");
            }
        }
    }
}
