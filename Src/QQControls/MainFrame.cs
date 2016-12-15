using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace QQControls
{
    /// <summary>
    /// 主窗体框架
    /// </summary>
    public partial class MainFrame : Form
    {
        #region 字段
        
        ResourceManager _resourceManager = new ResourceManager("QQControls.Images", Assembly.GetExecutingAssembly());
        Bitmap _frameImage = null;
        private bool _titleVisible;
        //记录窗体的大小,用于用户最大化或最小化后返回正常
        private int _form_width;
        private int _form_height;
        //记录窗体的坐标(位置)
        private int _form_PosX;
        private int _form_PosY;
        
        //窗体的状态
        enum start
        {
            normal, max, min
        }
        start st = start.normal;
        //系统按钮
        public enum sysButton
        { 
            normal, minClose, maxClose, close
        }
        sysButton _sysButton = sysButton.normal;
        #endregion

        #region 构造函数

        public MainFrame()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            InitializeComponent();
            _frameImage = _resourceManager.GetObject("LoginPanel_window_windowBkg") as Bitmap;
            
            //给各个面板赋上背景图象
            this.pnlLeftBottom.BackgroundImage = DrawFrameBorder(0, 255, 5, 5);
            this.pnlBottom.BackgroundImage = DrawFrameBorder(5, 255, 5, 5);
            this.pnlRightBottom.BackgroundImage = DrawFrameBorder(295, 255, 5, 5);
            this.pnlLeftTop.BackgroundImage = DrawFrameBorder(0, 0, 5, 5);
            this.pnlRightTop.BackgroundImage = DrawFrameBorder(295, 0, 5, 5);
            this.pnlTop.BackgroundImage = DrawFrameBorder(5, 0, 290, 5);
            this.pnlLeft.BackgroundImage = DrawFrameBorder(0, 5, 5, 250);
            this.pnlRight.BackgroundImage = DrawFrameBorder(295, 5, 5, 250);
            this.BackgroundImage = DrawFrameBorder(5, 5, 290, 250);
        }

        #endregion

        #region 方法

        private Image DrawFrameBorder(int x, int y, int width, int height)
        {
            Rectangle rec = new Rectangle(x, y, width, height);
            System.Drawing.Imaging.PixelFormat pixel = _frameImage.PixelFormat;
            Bitmap bitmap = _frameImage.Clone(rec, pixel);
            //让指定颜色透明
            bitmap.MakeTransparent(Color.FromArgb(255, 0, 255));
            return bitmap;
        }

        /// <summary>
        /// 设置最大化
        /// </summary>
        private void SetStartMax()
        {
            //记录窗体的宽高
            this._form_height = this.FormHeight;
            this._form_width = this.FormWidth;
            //记录窗体的坐标
            this._form_PosX = this.Location.X;
            this._form_PosY = this.Location.Y;
            //改变窗体的宽高
            this.FormWidth = Screen.PrimaryScreen.WorkingArea.Width + 3;
            this.FormHeight = Screen.PrimaryScreen.WorkingArea.Height + 3;
            //改变窗体的坐标
            this.Location = new Point(-1, -1);
            //改变枚举值
            st = start.max;
            this.mbtnMax.BtnType = MyButton.ButtonType.Restore;
            this.mbtnMax.Click -= new EventHandler(mbtnMax_Click);
            this.mbtnMax.Click += new EventHandler(mbtnRestore_Click);
            //this.mbtnRestore.Location = this.mbtnMax.Location;
           // this.mbtnRestore.Visible = true;
        }

        /// <summary>
        /// 恢复默认
        /// </summary>
        private void SetStartNormal()
        {
            //恢复默认大小
            this.FormWidth = this._form_width;
            this.FormHeight = this._form_height;
            //恢复坐标
            this.Location = new Point(this._form_PosX, this._form_PosY);
            //this.mbtnRestore.Visible = false;
            this.mbtnMax.Visible = true;
            //改变枚举值
            this.st = start.normal;
            this.mbtnMax.BtnType = MyButton.ButtonType.Maximum;
            this.mbtnMax.Click -= new EventHandler(mbtnRestore_Click);
            this.mbtnMax.Click += new EventHandler(mbtnMax_Click);
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 在属性面板中隐藏Size属性，由自定义的属性代替
        /// </summary>
        [Browsable(false)]
        public new SizeF Size
        {
            get { return base.Size; }
        }
        public int FormWidth
        {
            get { return this.Width; }
            set
            {
                if (base.Width < 180)
                {
                    return;
                }
                base.Width = value;
            }
        }
        public int FormHeight
        {
            get { return base.Height; }
            set
            {
                if (base.Height < 180)
                {
                    return;
                }
                base.Height = value;
            }
        }
        public bool TitleVisible
        {
            get { return this._titleVisible; }
            set 
            { 
                if (value)
                {
                    this.picTitle.Visible = false;
                }
                else
                {
                    this.picTitle.Visible = true;
                }
                this._titleVisible = value;
            }
        }
        public sysButton SystemButton
        {
            get { return this._sysButton; }
            set 
            {
                this._sysButton = value;
                if (this._sysButton == sysButton.normal)
                {
                    this.mbtnMin.Visible = true;
                    this.mbtnMax.Visible = true;
                    //this.mbtnMin.Location = new Point(this.mbtnMax.Location.X - this.mbtnMin.Width, this.mbtnMax.Location.Y);
                }
                else if (this._sysButton == sysButton.close)
                {
                    this.mbtnMax.Visible = false;
                    this.mbtnMin.Visible = false;
                }
                else if (this._sysButton == sysButton.minClose)
                {
                    this.mbtnMax.Visible = false;
                    //this.mbtnMin.Location = new Point(0,0);
                }
                else if (this._sysButton == sysButton.maxClose)
                {
                    this.mbtnMax.Visible = true;
                    this.mbtnMin.Visible = false;
                }
            }
        }
        #endregion

        #region 事件

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.pnlTop.Width = this.Width - 10;
            this.pnlRightTop.Location = new Point(this.pnlTop.Width + 5, 0);
            this.pnlLeft.Height = this.Height - 10;
            this.pnlRight.Location = new Point(this.Width - 5, 5);
            this.pnlRight.Height = pnlLeft.Height;
            this.pnlLeftBottom.Location = new Point(0, this.pnlLeft.Height + 5);
            this.pnlBottom.Location = new Point(5, this.pnlLeft.Height + 5);
            this.pnlBottom.Width = this.pnlTop.Width;
            this.pnlRightBottom.Location = new Point(this.pnlBottom.Width + 5, this.pnlLeft.Height + 5);
            this.mbtnClose.Left = this.Width - this.mbtnClose.Width - 1;
            this.mbtnMax.Left = this.Width - this.mbtnClose.Width - this.mbtnMax.Width - 1;
            if (this._sysButton == sysButton.minClose)
            {
                this.mbtnMin.Left = this.Width - this.mbtnMin.Width - this.mbtnClose.Width - 1;
            }
            else
            {
                this.mbtnMin.Left = this.Width - (this.mbtnMin.Width * 2) - this.mbtnClose.Width - 1;
            }
        }

        /// <summary>
        /// 重写窗体的改变大小事件
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //调用API，将窗体剪成圆角
            int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 4, 4);
            Win32.SetWindowRgn(this.Handle, Rgn, true);
        }

        /// <summary>
        /// 鼠标按下时移动窗体
        /// </summary>
        private void MainFrame_MouseDown(object sender, MouseEventArgs e)
        {
            if (st != start.max)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Win32.ReleaseCapture();
                    Win32.SendMessage(Handle, 274, 61440 + 9, 0);
                }
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        private void mbtnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            //改变枚举值
            this.st = start.min;
        }

        /// <summary>
        /// 最大化
        /// </summary>
        private void mbtnMax_Click(object sender, EventArgs e)
        {
            SetStartMax();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 恢复默认
        /// </summary>
        private void mbtnRestore_Click(object sender, EventArgs e)
        {
            SetStartNormal();
        }

        #endregion
    }
}
