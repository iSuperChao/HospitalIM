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
    public partial class ShowHead : UserControl
    {
        #region 字段
        
        ResourceManager _resourceManager = new ResourceManager("QQControls.Images", Assembly.GetExecutingAssembly());
        private Image _normal;

        #endregion

        #region 构造函数

        public ShowHead()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            InitializeComponent();
            _normal = _resourceManager.GetObject("ContactHeadBg_Normal") as Image;
            this.BackgroundImage = _normal;
        }

        #endregion

        #region 属性

        public Image HeadImage
        {
            get { return this.picHead.Image; }
            set { this.picHead.Image = value; }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 鼠标划过时
        /// </summary>
        private void ToolButton_MouseEnter(object sender, EventArgs e)
        {
            //this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackgroundImage = _resourceManager.GetObject("ContactHeadBg_Hightlight") as Image;
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// 鼠标离开时
        /// </summary>
        private void ToolButton_MouseLeave(object sender, EventArgs e)
        {
            //this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BackgroundImage = _normal;
            this.Cursor = Cursors.Default;
        }

        #endregion
    }
}
