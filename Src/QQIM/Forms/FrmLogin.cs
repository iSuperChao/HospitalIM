using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQControls;
using System.Net.Sockets;
using System.Net;

namespace ClientView
{
    /// <summary>
    /// 登录窗体
    /// </summary>
    public partial class FrmLogin : LoginFrame
    {
        #region 变量
        public int _qqNumber = 0;
        public string _qqPwd = string.Empty;
        public Boolean RemenbPassword;
        public Boolean Autologin;
        private DataSet ds;
        #endregion

        #region 构造函数

        public FrmLogin()
        {            
            InitializeComponent();
            //改变窗体置于屏幕中间
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
#if DEBUG
            //string[] qqs = new string[] { "10000", "10002", "10043", "10045" };
            try
            {
                ds = AppContext.db.ReturnDataSet("select * from logininfo ");
                this.cboQQNumber.DataSource = ds.Tables[0];
            }
            catch { MessageBox.Show("数据库出错，可能是程序安装目录没有权限，请取消安装文件夹只读属性，或到属性面版的“安全”里配置USERS为完全控制。"); }
            this.cboQQNumber.DisplayMember = "l_username";
            this.cboQQNumber.ValueMember = "l_password";
            this.cboQQNumber.DataSource = ds.Tables[0];
            cboQQNumber.SelectedIndex = -1;
            txtLoginPwd.Text = "";
            //this.txtLoginPwd.Text = "000000";
#endif
            
        }

        #endregion

        #region 事件

        /// <summary>
        /// 注册帐号
        /// </summary>
        private void lklRegister_Click(object sender, EventArgs e)
        {
            AppContext.CreateOrActivate(typeof(FrmRegister));
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        private void mbtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cboQQNumber.Text.Trim()))
            {
                MessageBox.Show("请输入帐号！", "提示");
                return;
            }
            if (string.IsNullOrEmpty(this.txtLoginPwd.Text.Trim()))
            {
                MessageBox.Show("密码不能为空！", "提示");
                return;
            }
            try
            {
                this._qqNumber = Convert.ToInt32(this.cboQQNumber.Text.Trim());
                this._qqPwd = this.txtLoginPwd.Text.Trim();
                this.RemenbPassword = this.chkRememberPwd.Checked;
                this.Autologin = this.chkAutoLogin.Checked;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch { MessageBox.Show("工号号码输入不正确。"); }
        }

        /// <summary>
        /// 按下回车键，登录
        /// </summary>
        private void FrmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.mbtnLogin_Click(sender, e);
            }
        }

        #endregion

        /// <summary>
        /// 测式帐号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboQQNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQQNumber.SelectedIndex > -1)
            {
                this.txtLoginPwd.Text = this.cboQQNumber.SelectedValue.ToString();
                DataTable dt=(DataTable)cboQQNumber.DataSource;
                this.chkRememberPwd.Checked = (ds.Tables[0].Rows[cboQQNumber.SelectedIndex]["l_password"].ToString()=="")?false:true;
            }
        }

        private void mbtnLogin_Load(object sender, EventArgs e)
        {

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {


        }

        private void lklRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        
    }
}
