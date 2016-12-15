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
    public partial class ToolButton : UserControl
    {

        #region 构造函数

        public ToolButton()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        #endregion

        private void ToolButton_MouseEnter(object sender, EventArgs e)
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void ToolButton_MouseLeave(object sender, EventArgs e)
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void ToolButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        private void ToolButton_MouseUp(object sender, MouseEventArgs e)
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

    }
}
