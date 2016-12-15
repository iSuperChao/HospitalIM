namespace QQControls
{
    partial class MyButton
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblString = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblString
            // 
            this.lblString.BackColor = System.Drawing.Color.Transparent;
            this.lblString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblString.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblString.ForeColor = System.Drawing.Color.Black;
            this.lblString.Location = new System.Drawing.Point(0, 0);
            this.lblString.Name = "lblString";
            this.lblString.Size = new System.Drawing.Size(86, 30);
            this.lblString.TabIndex = 0;
            this.lblString.Text = "按钮";
            this.lblString.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblString.Click += new System.EventHandler(this.lblString_Click);
            this.lblString.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblString_MouseDown);
            this.lblString.MouseEnter += new System.EventHandler(this.lblString_MouseEnter);
            this.lblString.MouseLeave += new System.EventHandler(this.lblString_MouseLeave);
            this.lblString.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblString_MouseUp);
            // 
            // MyButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.lblString);
            this.Name = "MyButton";
            this.Size = new System.Drawing.Size(86, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblString;
    }
}
