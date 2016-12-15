using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClientView
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmLogin frmLogin = new FrmLogin();
        ReLogin:
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                bool rec=true;
                System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName + frmLogin._qqNumber, out rec);
                if (rec)
                {
                    Form frm = AppContext.CreateOrActivate(typeof(FrmLoginValidate),
                        true, frmLogin._qqNumber, frmLogin._qqPwd);
                    try
                    {
                        frm.Close();

                        if (frm.DialogResult == DialogResult.OK)
                        {
                            try
                            {
                                AppContext.db.ExeSQL("delete from LOGININFO WHERE l_USERNAME='" + frmLogin._qqNumber.ToString().Trim() + "'");
                                if (frmLogin.RemenbPassword)
                                    AppContext.db.ExeSQL("insert into logininfo(l_username,l_password,l_autologin) values('" + frmLogin._qqNumber.ToString().Trim() + "','" + frmLogin._qqPwd + "'," + frmLogin.Autologin + ")");
                                else
                                    AppContext.db.ExeSQL("insert into logininfo(l_username,l_autologin) values('" + frmLogin._qqNumber.ToString().Trim() + "'," + frmLogin.Autologin + ")");
                            }
                            catch { }
                            Application.Run(new FrmMain());
                        }
                        else
                        {
                            mutex.Close();
                            mutex.Dispose();
                            goto ReLogin;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("登陆失败，可能服务器没启动！"); mutex.Close();
                        mutex.Dispose();
                        goto ReLogin;
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("账号：{0}已经在本机登录，请勿重复登录！", frmLogin._qqNumber));
                    goto ReLogin;
                }
            }
        }
    }
}
