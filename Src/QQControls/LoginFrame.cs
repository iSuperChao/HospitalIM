using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace QQControls
{
    public partial class LoginFrame : Form
    {
        #region 字段
        
        //获取资源对象
        System.Resources.ResourceManager _resourceManager = new System.Resources.ResourceManager("QQControls.Images", Assembly.GetExecutingAssembly());
        #endregion

        #region 构造函数
        
        public LoginFrame()
        {
            //双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            //获取背景图片
            this.BackgroundImage = _resourceManager.GetObject("LoginPanel_window_windowBkg") as Image;
            //获取标题图片
          //  this.picTitle.Image = _resourceManager.GetObject("Main_Title") as Image;
        }

        #endregion

        #region 事件
        
        /// <summary>
        /// 重写窗体的改变大小事件
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //调用API，将窗体剪成圆角
            int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 7, 7);
            Win32.SetWindowRgn(this.Handle, Rgn, true);
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 最小化当前窗体
        /// </summary>
        private void mbtnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 按住鼠标左键移动窗体
        /// </summary>
        private void LoginFrame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Win32.ReleaseCapture();
                Win32.SendMessage(Handle, 274, 61440 + 9, 0);
            }
        }

        #endregion
    }
}
