using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace QQControls
{
    /// <summary>
    /// 按钮
    /// </summary>
    public partial class MyButton : UserControl
    {
        #region 字段

        ResourceManager _resourceManager = new ResourceManager("QQControls.Images", Assembly.GetExecutingAssembly());

        #endregion

        #region 枚举
        /// <summary>
        /// 按钮类型
        /// </summary>
        public enum ButtonType
        {
            Normal, Maximum, Minimum, Restore, Close, SmallButton
        }

        #endregion

        #region 构造函数

        public MyButton()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            ChangeButtonType();
            this.BackgroundImage = this._normal;
        }

        #endregion

        #region 字段
        
        private Image _normal;
        private Image _down;
        private Image _moveBg;
        private Image _upDown;
        private ButtonType _type = ButtonType.Normal;
        #endregion

        #region 方法

        /// <summary>
        /// 根据用户的选择改变各个状态的背景图片
        /// </summary>
        /// <returns></returns>
        private void ChangeButtonType()
        {
            switch (_type)
            { 
                case ButtonType.Minimum:
                    this._normal = _resourceManager.GetObject("btn_mini_normal") as Image;
                    this._down = _resourceManager.GetObject("btn_mini_down") as Image;
                    this._moveBg = _resourceManager.GetObject("btn_mini_highlight") as Image;
                    this._upDown = _normal;
                    this.Width = 28;
                    this.Height = 20;
                    break;
                case ButtonType.Maximum:
                    this._normal = _resourceManager.GetObject("btn_max_normal") as Image;
                    this._down = _resourceManager.GetObject("btn_max_down") as Image;
                    this._moveBg = _resourceManager.GetObject("btn_max_highlight") as Image;
                    this._upDown = _normal;
                    this.Width = 28;
                    this.Height = 20;
                    break;
                case ButtonType.Close:
                    this._normal = _resourceManager.GetObject("btn_close_normal") as Image;
                    this._down = _resourceManager.GetObject("btn_close_down") as Image;
                    this._moveBg = _resourceManager.GetObject("btn_close_highlight") as Image;
                    this._upDown = _resourceManager.GetObject("btn_close_disable") as Image;
                    this.Width = 39;
                    this.Height = 20;
                    break;
                case ButtonType.Restore:
                    this._normal = _resourceManager.GetObject("btn_restore_normal") as Image;
                    this._down = _resourceManager.GetObject("btn_restore_down") as Image;
                    this._moveBg = _resourceManager.GetObject("btn_restore_highlight") as Image;
                    this._upDown = _normal;
                    this.Width = 28;
                    this.Height = 20;
                    break;
                case ButtonType.SmallButton:
                    this._normal = _resourceManager.GetObject("AddAccountBtn") as Image;
                    this._down = _resourceManager.GetObject("AddAccountBtn_Down") as Image;
                    this._moveBg = _resourceManager.GetObject("AddAccountBtn_mouseover") as Image;
                    this._upDown = _moveBg;
                    this.Width = 120;
                    this.Height = 36;
                    break;
                default:
                    this._normal = _resourceManager.GetObject("login_btn_normal") as Image;
                    this._down = _resourceManager.GetObject("login_btn_down") as Image;
                    this._moveBg = _resourceManager.GetObject("login_btn_highlight") as Image;
                    this._upDown = _resourceManager.GetObject("login_btn_focus") as Image;
                    this.Width = 86;
                    this.Height = 30;
                    break;
            }
            this.BackgroundImage = _normal;
        }

        #endregion

        #region 属性

        [Description("鼠标默认背景")]
        public Image Normal
        {
            get { return _normal; }
            set { this._normal = value; }
        }
        [Description("鼠标按下时背景")]
        public Image Down
        {
            get { return _down; }
            set { this._down = value; }
        }
        [Description("鼠标默认背景")]
        public Image MoveBg
        {
            get { return _moveBg; }
            set { this._moveBg = value; }
        }
        [Description("按钮上的文字信息")]
        public string Content
        {
            get { return this.lblString.Text; }
            set { this.lblString.Text = value; }
        }
        [Description("按钮的类型")]
        public ButtonType BtnType 
        {
            get { return this._type;}
            set 
            { 
                this._type = value;
                ChangeButtonType();
            }
        }
        #endregion  

        #region 事件
        
        private void lblString_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = this._moveBg;
        }

        private void lblString_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = this._down;
        }

        private void lblString_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = this._upDown;
        }

        private void lblString_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = this._normal;
        }

        private void lblString_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        #endregion
    }
}
