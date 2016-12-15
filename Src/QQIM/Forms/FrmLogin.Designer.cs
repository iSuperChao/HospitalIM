namespace ClientView
{
    partial class FrmLogin
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.chkAutoLogin = new System.Windows.Forms.CheckBox();
            this.chkRememberPwd = new System.Windows.Forms.CheckBox();
            this.lklBackPwd = new System.Windows.Forms.LinkLabel();
            this.lklRegister = new System.Windows.Forms.LinkLabel();
            this.txtLoginPwd = new System.Windows.Forms.TextBox();
            this.cboQQNumber = new System.Windows.Forms.ComboBox();
            this.picUserHead = new System.Windows.Forms.PictureBox();
            this.mbtnSet = new QQControls.MyButton();
            this.mbtnLogin = new QQControls.MyButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLogin
            // 
            this.pnlLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLogin.BackColor = System.Drawing.Color.White;
            this.pnlLogin.Controls.Add(this.chkAutoLogin);
            this.pnlLogin.Controls.Add(this.chkRememberPwd);
            this.pnlLogin.Controls.Add(this.lklBackPwd);
            this.pnlLogin.Controls.Add(this.lklRegister);
            this.pnlLogin.Controls.Add(this.txtLoginPwd);
            this.pnlLogin.Controls.Add(this.cboQQNumber);
            this.pnlLogin.Controls.Add(this.picUserHead);
            this.pnlLogin.Location = new System.Drawing.Point(1, 160);
            this.pnlLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(393, 147);
            this.pnlLogin.TabIndex = 5;
            // 
            // chkAutoLogin
            // 
            this.chkAutoLogin.AutoSize = true;
            this.chkAutoLogin.Location = new System.Drawing.Point(263, 103);
            this.chkAutoLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAutoLogin.Name = "chkAutoLogin";
            this.chkAutoLogin.Size = new System.Drawing.Size(94, 20);
            this.chkAutoLogin.TabIndex = 4;
            this.chkAutoLogin.Text = "自动登录";
            this.chkAutoLogin.UseVisualStyleBackColor = true;
            // 
            // chkRememberPwd
            // 
            this.chkRememberPwd.AutoSize = true;
            this.chkRememberPwd.Location = new System.Drawing.Point(131, 103);
            this.chkRememberPwd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkRememberPwd.Name = "chkRememberPwd";
            this.chkRememberPwd.Size = new System.Drawing.Size(94, 20);
            this.chkRememberPwd.TabIndex = 4;
            this.chkRememberPwd.Text = "记住密码";
            this.chkRememberPwd.UseVisualStyleBackColor = true;
            // 
            // lklBackPwd
            // 
            this.lklBackPwd.AutoSize = true;
            this.lklBackPwd.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lklBackPwd.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.lklBackPwd.Location = new System.Drawing.Point(313, 67);
            this.lklBackPwd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklBackPwd.Name = "lklBackPwd";
            this.lklBackPwd.Size = new System.Drawing.Size(72, 16);
            this.lklBackPwd.TabIndex = 3;
            this.lklBackPwd.TabStop = true;
            this.lklBackPwd.Text = "找回密码";
            // 
            // lklRegister
            // 
            this.lklRegister.AutoSize = true;
            this.lklRegister.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lklRegister.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.lklRegister.Location = new System.Drawing.Point(313, 28);
            this.lklRegister.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklRegister.Name = "lklRegister";
            this.lklRegister.Size = new System.Drawing.Size(72, 16);
            this.lklRegister.TabIndex = 3;
            this.lklRegister.TabStop = true;
            this.lklRegister.Text = "注册工号";
            this.lklRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklRegister_LinkClicked);
            this.lklRegister.Click += new System.EventHandler(this.lklRegister_Click);
            // 
            // txtLoginPwd
            // 
            this.txtLoginPwd.Location = new System.Drawing.Point(131, 61);
            this.txtLoginPwd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLoginPwd.Name = "txtLoginPwd";
            this.txtLoginPwd.PasswordChar = '*';
            this.txtLoginPwd.Size = new System.Drawing.Size(172, 26);
            this.txtLoginPwd.TabIndex = 2;
            // 
            // cboQQNumber
            // 
            this.cboQQNumber.DropDownHeight = 130;
            this.cboQQNumber.FormattingEnabled = true;
            this.cboQQNumber.IntegralHeight = false;
            this.cboQQNumber.Location = new System.Drawing.Point(131, 23);
            this.cboQQNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboQQNumber.Name = "cboQQNumber";
            this.cboQQNumber.Size = new System.Drawing.Size(172, 24);
            this.cboQQNumber.TabIndex = 1;
            this.cboQQNumber.SelectedIndexChanged += new System.EventHandler(this.cboQQNumber_SelectedIndexChanged);
            // 
            // picUserHead
            // 
            this.picUserHead.Image = ((System.Drawing.Image)(resources.GetObject("picUserHead.Image")));
            this.picUserHead.Location = new System.Drawing.Point(0, 11);
            this.picUserHead.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picUserHead.Name = "picUserHead";
            this.picUserHead.Size = new System.Drawing.Size(117, 127);
            this.picUserHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUserHead.TabIndex = 0;
            this.picUserHead.TabStop = false;
            // 
            // mbtnSet
            // 
            this.mbtnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mbtnSet.BackColor = System.Drawing.Color.Transparent;
            this.mbtnSet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnSet.BackgroundImage")));
            this.mbtnSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSet.BtnType = QQControls.MyButton.ButtonType.Normal;
            this.mbtnSet.Content = "设置";
            this.mbtnSet.Down = ((System.Drawing.Image)(resources.GetObject("mbtnSet.Down")));
            this.mbtnSet.Location = new System.Drawing.Point(16, 316);
            this.mbtnSet.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mbtnSet.MoveBg = ((System.Drawing.Image)(resources.GetObject("mbtnSet.MoveBg")));
            this.mbtnSet.Name = "mbtnSet";
            this.mbtnSet.Normal = ((System.Drawing.Image)(resources.GetObject("mbtnSet.Normal")));
            this.mbtnSet.Size = new System.Drawing.Size(93, 28);
            this.mbtnSet.TabIndex = 6;
            // 
            // mbtnLogin
            // 
            this.mbtnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mbtnLogin.BackColor = System.Drawing.Color.Transparent;
            this.mbtnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnLogin.BackgroundImage")));
            this.mbtnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnLogin.BtnType = QQControls.MyButton.ButtonType.Normal;
            this.mbtnLogin.Content = "登录";
            this.mbtnLogin.Down = ((System.Drawing.Image)(resources.GetObject("mbtnLogin.Down")));
            this.mbtnLogin.Location = new System.Drawing.Point(280, 316);
            this.mbtnLogin.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mbtnLogin.MoveBg = ((System.Drawing.Image)(resources.GetObject("mbtnLogin.MoveBg")));
            this.mbtnLogin.Name = "mbtnLogin";
            this.mbtnLogin.Normal = ((System.Drawing.Image)(resources.GetObject("mbtnLogin.Normal")));
            this.mbtnLogin.Size = new System.Drawing.Size(93, 28);
            this.mbtnLogin.TabIndex = 6;
            this.mbtnLogin.Load += new System.EventHandler(this.mbtnLogin_Load);
            this.mbtnLogin.Click += new System.EventHandler(this.mbtnLogin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(393, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 360);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.mbtnLogin);
            this.Controls.Add(this.mbtnSet);
            this.Controls.Add(this.pnlLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "QQ2011";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmLogin_KeyPress);
            this.Controls.SetChildIndex(this.pnlLogin, 0);
            this.Controls.SetChildIndex(this.mbtnSet, 0);
            this.Controls.SetChildIndex(this.mbtnLogin, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLogin;
        private QQControls.MyButton mbtnSet;
        private System.Windows.Forms.PictureBox picUserHead;
        private System.Windows.Forms.LinkLabel lklRegister;
        private System.Windows.Forms.TextBox txtLoginPwd;
        private System.Windows.Forms.ComboBox cboQQNumber;
        private System.Windows.Forms.CheckBox chkAutoLogin;
        private System.Windows.Forms.CheckBox chkRememberPwd;
        private System.Windows.Forms.LinkLabel lklBackPwd;
        private QQControls.MyButton mbtnLogin;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

