namespace ClientView
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.lblNickName = new System.Windows.Forms.Label();
            this.lblAutograph = new System.Windows.Forms.Label();
            this.tbtnZone = new QQControls.ToolButton();
            this.tbtnEmail = new QQControls.ToolButton();
            this.tbtnSchool = new QQControls.ToolButton();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.业务操作系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.信息查询系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.常用网站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其它ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbtnManager = new QQControls.ToolButton();
            this.btnSysMessage = new QQControls.ToolButton();
            this.btnSafe = new System.Windows.Forms.Button();
            this.btnSreach = new System.Windows.Forms.Button();
            this.tbtn8 = new QQControls.ToolButton();
            this.tbtn1 = new QQControls.ToolButton();
            this.tbtn2 = new QQControls.ToolButton();
            this.tbtn3 = new QQControls.ToolButton();
            this.tbtn4 = new QQControls.ToolButton();
            this.tbtn5 = new QQControls.ToolButton();
            this.tbtn6 = new QQControls.ToolButton();
            this.tbtn7 = new QQControls.ToolButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.picSreach = new System.Windows.Forms.PictureBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctmsNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.listQQFriendList = new ALQQControl.QQListBox();
            this.ctmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDeleteFriend = new System.Windows.Forms.ToolStripMenuItem();
            this.查看资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.电子邮件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平台信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手机短信ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.发送文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.消息记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrFlashSysMessage = new System.Windows.Forms.Timer(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new System.Data.DataSet();
            this.ShowHead = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolButton1 = new QQControls.ToolButton();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.业务系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSreach)).BeginInit();
            this.ctmsNotify.SuspendLayout();
            this.ctmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowHead)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNickName
            // 
            this.lblNickName.AutoSize = true;
            this.lblNickName.BackColor = System.Drawing.Color.Transparent;
            this.lblNickName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNickName.Font = new System.Drawing.Font("Aharoni", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNickName.ForeColor = System.Drawing.Color.Black;
            this.lblNickName.Location = new System.Drawing.Point(105, 32);
            this.lblNickName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNickName.Name = "lblNickName";
            this.lblNickName.Size = new System.Drawing.Size(97, 20);
            this.lblNickName.TabIndex = 8;
            this.lblNickName.Text = "NickName";
            this.toolTip1.SetToolTip(this.lblNickName, "点击查看或修改你的个人资料。");
            this.lblNickName.Click += new System.EventHandler(this.lblNickName_Click);
            // 
            // lblAutograph
            // 
            this.lblAutograph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAutograph.BackColor = System.Drawing.Color.Transparent;
            this.lblAutograph.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAutograph.Location = new System.Drawing.Point(104, 66);
            this.lblAutograph.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutograph.Name = "lblAutograph";
            this.lblAutograph.Size = new System.Drawing.Size(262, 27);
            this.lblAutograph.TabIndex = 9;
            this.lblAutograph.Text = "Autograph";
            this.lblAutograph.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbtnZone
            // 
            this.tbtnZone.BackColor = System.Drawing.Color.Transparent;
            this.tbtnZone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtnZone.BackgroundImage")));
            this.tbtnZone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtnZone.Location = new System.Drawing.Point(176, 102);
            this.tbtnZone.Margin = new System.Windows.Forms.Padding(5);
            this.tbtnZone.Name = "tbtnZone";
            this.tbtnZone.Size = new System.Drawing.Size(21, 21);
            this.tbtnZone.TabIndex = 10;
            this.toolTip1.SetToolTip(this.tbtnZone, "平台信息群发：将信息通过本平台发给你的联系人，你可以选择要发送给哪些哦。");
            this.tbtnZone.Load += new System.EventHandler(this.tbtnZone_Load);
            this.tbtnZone.Click += new System.EventHandler(this.tbtnZone_Click);
            // 
            // tbtnEmail
            // 
            this.tbtnEmail.BackColor = System.Drawing.Color.Transparent;
            this.tbtnEmail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtnEmail.BackgroundImage")));
            this.tbtnEmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtnEmail.Location = new System.Drawing.Point(107, 98);
            this.tbtnEmail.Margin = new System.Windows.Forms.Padding(5);
            this.tbtnEmail.Name = "tbtnEmail";
            this.tbtnEmail.Size = new System.Drawing.Size(24, 24);
            this.tbtnEmail.TabIndex = 11;
            this.toolTip1.SetToolTip(this.tbtnEmail, "打开我的电子邮箱。请确保你的个人资料里所填的邮箱信息是正确的哦。");
            // 
            // tbtnSchool
            // 
            this.tbtnSchool.BackColor = System.Drawing.Color.Transparent;
            this.tbtnSchool.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtnSchool.BackgroundImage")));
            this.tbtnSchool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtnSchool.Location = new System.Drawing.Point(209, 102);
            this.tbtnSchool.Margin = new System.Windows.Forms.Padding(5);
            this.tbtnSchool.Name = "tbtnSchool";
            this.tbtnSchool.Size = new System.Drawing.Size(21, 21);
            this.tbtnSchool.TabIndex = 11;
            this.toolTip1.SetToolTip(this.tbtnSchool, "手机短息群发：通过本平台特别设计的功能发送手机短消息给你的联系人。");
            // 
            // pnlMenu
            // 
            this.pnlMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMenu.BackColor = System.Drawing.Color.Transparent;
            this.pnlMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMenu.BackgroundImage")));
            this.pnlMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMenu.ContextMenuStrip = this.contextMenuStrip1;
            this.pnlMenu.Location = new System.Drawing.Point(13, 624);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(53, 53);
            this.pnlMenu.TabIndex = 12;
            this.pnlMenu.Click += new System.EventHandler(this.pnlMenu_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.业务操作系统ToolStripMenuItem,
            this.信息查询系统ToolStripMenuItem,
            this.常用网站ToolStripMenuItem,
            this.其它ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 108);
            // 
            // 业务操作系统ToolStripMenuItem
            // 
            this.业务操作系统ToolStripMenuItem.Name = "业务操作系统ToolStripMenuItem";
            this.业务操作系统ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.业务操作系统ToolStripMenuItem.Text = "业务操作系统";
            // 
            // 信息查询系统ToolStripMenuItem
            // 
            this.信息查询系统ToolStripMenuItem.Name = "信息查询系统ToolStripMenuItem";
            this.信息查询系统ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.信息查询系统ToolStripMenuItem.Text = "信息查询系统";
            // 
            // 常用网站ToolStripMenuItem
            // 
            this.常用网站ToolStripMenuItem.Name = "常用网站ToolStripMenuItem";
            this.常用网站ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.常用网站ToolStripMenuItem.Text = "常用网站";
            // 
            // 其它ToolStripMenuItem
            // 
            this.其它ToolStripMenuItem.Name = "其它ToolStripMenuItem";
            this.其它ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.其它ToolStripMenuItem.Text = "其它";
            // 
            // tbtnManager
            // 
            this.tbtnManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbtnManager.BackColor = System.Drawing.Color.Transparent;
            this.tbtnManager.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtnManager.BackgroundImage")));
            this.tbtnManager.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtnManager.Location = new System.Drawing.Point(82, 648);
            this.tbtnManager.Margin = new System.Windows.Forms.Padding(5);
            this.tbtnManager.Name = "tbtnManager";
            this.tbtnManager.Size = new System.Drawing.Size(27, 27);
            this.tbtnManager.TabIndex = 13;
            this.toolTip1.SetToolTip(this.tbtnManager, "设置管理");
            this.tbtnManager.Click += new System.EventHandler(this.tbtnManager_Click);
            // 
            // btnSysMessage
            // 
            this.btnSysMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSysMessage.BackColor = System.Drawing.Color.Transparent;
            this.btnSysMessage.BackgroundImage = global::ClientView.StatusImages.message;
            this.btnSysMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSysMessage.Location = new System.Drawing.Point(118, 649);
            this.btnSysMessage.Margin = new System.Windows.Forms.Padding(5);
            this.btnSysMessage.Name = "btnSysMessage";
            this.btnSysMessage.Size = new System.Drawing.Size(27, 27);
            this.btnSysMessage.TabIndex = 13;
            this.btnSysMessage.Click += new System.EventHandler(this.btnSysMessage_Click);
            // 
            // btnSafe
            // 
            this.btnSafe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSafe.BackColor = System.Drawing.Color.Transparent;
            this.btnSafe.FlatAppearance.BorderSize = 0;
            this.btnSafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSafe.Image = ((System.Drawing.Image)(resources.GetObject("btnSafe.Image")));
            this.btnSafe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSafe.Location = new System.Drawing.Point(220, 646);
            this.btnSafe.Margin = new System.Windows.Forms.Padding(4);
            this.btnSafe.Name = "btnSafe";
            this.btnSafe.Size = new System.Drawing.Size(75, 31);
            this.btnSafe.TabIndex = 14;
            this.btnSafe.Text = "关于";
            this.btnSafe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSafe.UseVisualStyleBackColor = false;
            this.btnSafe.Click += new System.EventHandler(this.btnSafe_Click);
            // 
            // btnSreach
            // 
            this.btnSreach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSreach.BackColor = System.Drawing.Color.Transparent;
            this.btnSreach.FlatAppearance.BorderSize = 0;
            this.btnSreach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSreach.Image = ((System.Drawing.Image)(resources.GetObject("btnSreach.Image")));
            this.btnSreach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSreach.Location = new System.Drawing.Point(151, 646);
            this.btnSreach.Margin = new System.Windows.Forms.Padding(4);
            this.btnSreach.Name = "btnSreach";
            this.btnSreach.Size = new System.Drawing.Size(75, 31);
            this.btnSreach.TabIndex = 14;
            this.btnSreach.Text = "查找";
            this.btnSreach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSreach.UseVisualStyleBackColor = false;
            this.btnSreach.Click += new System.EventHandler(this.btnSreach_Click);
            // 
            // tbtn8
            // 
            this.tbtn8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbtn8.BackColor = System.Drawing.Color.Transparent;
            this.tbtn8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtn8.BackgroundImage")));
            this.tbtn8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtn8.Location = new System.Drawing.Point(336, 614);
            this.tbtn8.Margin = new System.Windows.Forms.Padding(5);
            this.tbtn8.Name = "tbtn8";
            this.tbtn8.Size = new System.Drawing.Size(21, 21);
            this.tbtn8.TabIndex = 16;
            this.tbtn8.Click += new System.EventHandler(this.tbtn8_Click);
            this.tbtn8.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.tbtn8_ChangeUICues);
            // 
            // tbtn1
            // 
            this.tbtn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbtn1.BackColor = System.Drawing.Color.Transparent;
            this.tbtn1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtn1.BackgroundImage")));
            this.tbtn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtn1.Location = new System.Drawing.Point(76, 613);
            this.tbtn1.Margin = new System.Windows.Forms.Padding(5);
            this.tbtn1.Name = "tbtn1";
            this.tbtn1.Size = new System.Drawing.Size(27, 27);
            this.tbtn1.TabIndex = 17;
            this.tbtn1.Click += new System.EventHandler(this.tbtn1_Click);
            // 
            // tbtn2
            // 
            this.tbtn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbtn2.BackColor = System.Drawing.Color.Transparent;
            this.tbtn2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtn2.BackgroundImage")));
            this.tbtn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtn2.Location = new System.Drawing.Point(112, 613);
            this.tbtn2.Margin = new System.Windows.Forms.Padding(5);
            this.tbtn2.Name = "tbtn2";
            this.tbtn2.Size = new System.Drawing.Size(27, 27);
            this.tbtn2.TabIndex = 17;
            this.tbtn2.Click += new System.EventHandler(this.tbtn2_Click);
            // 
            // tbtn3
            // 
            this.tbtn3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbtn3.BackColor = System.Drawing.Color.Transparent;
            this.tbtn3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtn3.BackgroundImage")));
            this.tbtn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtn3.Location = new System.Drawing.Point(148, 613);
            this.tbtn3.Margin = new System.Windows.Forms.Padding(5);
            this.tbtn3.Name = "tbtn3";
            this.tbtn3.Size = new System.Drawing.Size(27, 27);
            this.tbtn3.TabIndex = 17;
            this.tbtn3.Click += new System.EventHandler(this.tbtn3_Click);
            // 
            // tbtn4
            // 
            this.tbtn4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbtn4.BackColor = System.Drawing.Color.Transparent;
            this.tbtn4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtn4.BackgroundImage")));
            this.tbtn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtn4.Location = new System.Drawing.Point(184, 613);
            this.tbtn4.Margin = new System.Windows.Forms.Padding(5);
            this.tbtn4.Name = "tbtn4";
            this.tbtn4.Size = new System.Drawing.Size(27, 27);
            this.tbtn4.TabIndex = 17;
            this.tbtn4.Click += new System.EventHandler(this.tbtn4_Click);
            // 
            // tbtn5
            // 
            this.tbtn5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbtn5.BackColor = System.Drawing.Color.Transparent;
            this.tbtn5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtn5.BackgroundImage")));
            this.tbtn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtn5.Location = new System.Drawing.Point(220, 613);
            this.tbtn5.Margin = new System.Windows.Forms.Padding(5);
            this.tbtn5.Name = "tbtn5";
            this.tbtn5.Size = new System.Drawing.Size(27, 27);
            this.tbtn5.TabIndex = 17;
            this.tbtn5.Click += new System.EventHandler(this.tbtn5_Click);
            // 
            // tbtn6
            // 
            this.tbtn6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbtn6.BackColor = System.Drawing.Color.Transparent;
            this.tbtn6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtn6.BackgroundImage")));
            this.tbtn6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtn6.Location = new System.Drawing.Point(256, 613);
            this.tbtn6.Margin = new System.Windows.Forms.Padding(5);
            this.tbtn6.Name = "tbtn6";
            this.tbtn6.Size = new System.Drawing.Size(27, 27);
            this.tbtn6.TabIndex = 17;
            this.tbtn6.Click += new System.EventHandler(this.tbtn6_Click);
            // 
            // tbtn7
            // 
            this.tbtn7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbtn7.BackColor = System.Drawing.Color.Transparent;
            this.tbtn7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtn7.BackgroundImage")));
            this.tbtn7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbtn7.Location = new System.Drawing.Point(292, 613);
            this.tbtn7.Margin = new System.Windows.Forms.Padding(5);
            this.tbtn7.Name = "tbtn7";
            this.tbtn7.Size = new System.Drawing.Size(27, 27);
            this.tbtn7.TabIndex = 17;
            this.tbtn7.Click += new System.EventHandler(this.tbtn7_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(5, 131);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(361, 26);
            this.txtSearch.TabIndex = 18;
            this.toolTip1.SetToolTip(this.txtSearch, "输入联系人工号号码或姓名查找。");
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // picSreach
            // 
            this.picSreach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picSreach.BackColor = System.Drawing.Color.White;
            this.picSreach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSreach.Image = ((System.Drawing.Image)(resources.GetObject("picSreach.Image")));
            this.picSreach.Location = new System.Drawing.Point(343, 135);
            this.picSreach.Margin = new System.Windows.Forms.Padding(4);
            this.picSreach.Name = "picSreach";
            this.picSreach.Size = new System.Drawing.Size(18, 18);
            this.picSreach.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSreach.TabIndex = 19;
            this.picSreach.TabStop = false;
            this.toolTip1.SetToolTip(this.picSreach, "查找联系人。");
            this.picSreach.Click += new System.EventHandler(this.picSreach_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.ctmsNotify;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // ctmsNotify
            // 
            this.ctmsNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExit});
            this.ctmsNotify.Name = "ctmsNotify";
            this.ctmsNotify.Size = new System.Drawing.Size(113, 30);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(112, 26);
            this.tsmiExit.Text = "退出";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // listQQFriendList
            // 
            this.listQQFriendList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listQQFriendList.ContextMenuStrip = this.ctmsMenu;
            this.listQQFriendList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listQQFriendList.FormattingEnabled = true;
            this.listQQFriendList.Items.AddRange(new ALQQControl.QQListBoxItem[] {
            ((ALQQControl.QQListBoxItem)(resources.GetObject("listQQFriendList.Items")))});
            this.listQQFriendList.Location = new System.Drawing.Point(1, 166);
            this.listQQFriendList.Margin = new System.Windows.Forms.Padding(4);
            this.listQQFriendList.Name = "listQQFriendList";
            this.listQQFriendList.Size = new System.Drawing.Size(369, 434);
            this.listQQFriendList.TabIndex = 22;
            this.listQQFriendList.DoubleClick += new System.EventHandler(this.listQQFriendList_DoubleClick);
            // 
            // ctmsMenu
            // 
            this.ctmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteFriend,
            this.查看资料ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.电子邮件ToolStripMenuItem,
            this.平台信息ToolStripMenuItem,
            this.手机短信ToolStripMenuItem,
            this.toolStripMenuItem4,
            this.toolStripMenuItem2,
            this.toolStripMenuItem5,
            this.发送文件ToolStripMenuItem,
            this.toolStripMenuItem6,
            this.toolStripMenuItem3,
            this.消息记录ToolStripMenuItem});
            this.ctmsMenu.Name = "ctmsMenu";
            this.ctmsMenu.Size = new System.Drawing.Size(161, 282);
            // 
            // tsmiDeleteFriend
            // 
            this.tsmiDeleteFriend.Name = "tsmiDeleteFriend";
            this.tsmiDeleteFriend.Size = new System.Drawing.Size(160, 26);
            this.tsmiDeleteFriend.Text = "删除好友";
            this.tsmiDeleteFriend.Click += new System.EventHandler(this.tsmiDeleteFriend_Click);
            // 
            // 查看资料ToolStripMenuItem
            // 
            this.查看资料ToolStripMenuItem.Name = "查看资料ToolStripMenuItem";
            this.查看资料ToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.查看资料ToolStripMenuItem.Text = "查看资料";
            this.查看资料ToolStripMenuItem.Click += new System.EventHandler(this.查看资料ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // 电子邮件ToolStripMenuItem
            // 
            this.电子邮件ToolStripMenuItem.Name = "电子邮件ToolStripMenuItem";
            this.电子邮件ToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.电子邮件ToolStripMenuItem.Text = "电子邮件";
            // 
            // 平台信息ToolStripMenuItem
            // 
            this.平台信息ToolStripMenuItem.Name = "平台信息ToolStripMenuItem";
            this.平台信息ToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.平台信息ToolStripMenuItem.Text = "平台信息";
            this.平台信息ToolStripMenuItem.Click += new System.EventHandler(this.平台信息ToolStripMenuItem_Click);
            // 
            // 手机短信ToolStripMenuItem
            // 
            this.手机短信ToolStripMenuItem.Name = "手机短信ToolStripMenuItem";
            this.手机短信ToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.手机短信ToolStripMenuItem.Text = "手机短信";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(157, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 26);
            this.toolStripMenuItem2.Text = "远程协助";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(160, 26);
            this.toolStripMenuItem5.Text = "视频对话";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // 发送文件ToolStripMenuItem
            // 
            this.发送文件ToolStripMenuItem.Name = "发送文件ToolStripMenuItem";
            this.发送文件ToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.发送文件ToolStripMenuItem.Text = "发送单文件";
            this.发送文件ToolStripMenuItem.Click += new System.EventHandler(this.发送文件ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(160, 26);
            this.toolStripMenuItem6.Text = "发送文件夹";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(157, 6);
            // 
            // 消息记录ToolStripMenuItem
            // 
            this.消息记录ToolStripMenuItem.Name = "消息记录ToolStripMenuItem";
            this.消息记录ToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.消息记录ToolStripMenuItem.Text = "消息记录";
            // 
            // tmrFlashSysMessage
            // 
            this.tmrFlashSysMessage.Interval = 260;
            this.tmrFlashSysMessage.Tick += new System.EventHandler(this.tmrFlashSysMessage_Tick);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // ShowHead
            // 
            this.ShowHead.BackColor = System.Drawing.Color.Transparent;
            this.ShowHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ShowHead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowHead.Location = new System.Drawing.Point(9, 32);
            this.ShowHead.Name = "ShowHead";
            this.ShowHead.Size = new System.Drawing.Size(80, 84);
            this.ShowHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ShowHead.TabIndex = 23;
            this.ShowHead.TabStop = false;
            this.ShowHead.Click += new System.EventHandler(this.lblNickName_Click);
            // 
            // toolButton1
            // 
            this.toolButton1.BackColor = System.Drawing.Color.Transparent;
            this.toolButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolButton1.BackgroundImage")));
            this.toolButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolButton1.Location = new System.Drawing.Point(137, 100);
            this.toolButton1.Margin = new System.Windows.Forms.Padding(5);
            this.toolButton1.Name = "toolButton1";
            this.toolButton1.Size = new System.Drawing.Size(24, 24);
            this.toolButton1.TabIndex = 24;
            this.toolTip1.SetToolTip(this.toolButton1, "打开我的电子邮箱。请确保你的个人资料里所填的邮箱信息是正确的哦。");
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(293, 646);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 26;
            this.button1.Text = "退出";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(176, 25);
            this.toolStripMenuItem7.Text = "toolStripMenuItem7";
            // 
            // 业务系统ToolStripMenuItem
            // 
            this.业务系统ToolStripMenuItem.Name = "业务系统ToolStripMenuItem";
            this.业务系统ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 操作系统ToolStripMenuItem
            // 
            this.操作系统ToolStripMenuItem.Name = "操作系统ToolStripMenuItem";
            this.操作系统ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(292, 646);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 31);
            this.button2.TabIndex = 26;
            this.button2.Text = "退出";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 693);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.toolButton1);
            this.Controls.Add(this.ShowHead);
            this.Controls.Add(this.picSreach);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.tbtn7);
            this.Controls.Add(this.tbtn6);
            this.Controls.Add(this.tbtn5);
            this.Controls.Add(this.tbtn4);
            this.Controls.Add(this.tbtn3);
            this.Controls.Add(this.tbtn2);
            this.Controls.Add(this.tbtn1);
            this.Controls.Add(this.tbtn8);
            this.Controls.Add(this.btnSreach);
            this.Controls.Add(this.btnSafe);
            this.Controls.Add(this.btnSysMessage);
            this.Controls.Add(this.tbtnManager);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.tbtnSchool);
            this.Controls.Add(this.tbtnEmail);
            this.Controls.Add(this.tbtnZone);
            this.Controls.Add(this.lblAutograph);
            this.Controls.Add(this.lblNickName);
            this.Controls.Add(this.listQQFriendList);
            this.FormWidth = 373;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "公安办公通信平台";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Controls.SetChildIndex(this.listQQFriendList, 0);
            this.Controls.SetChildIndex(this.lblNickName, 0);
            this.Controls.SetChildIndex(this.lblAutograph, 0);
            this.Controls.SetChildIndex(this.tbtnZone, 0);
            this.Controls.SetChildIndex(this.tbtnEmail, 0);
            this.Controls.SetChildIndex(this.tbtnSchool, 0);
            this.Controls.SetChildIndex(this.pnlMenu, 0);
            this.Controls.SetChildIndex(this.tbtnManager, 0);
            this.Controls.SetChildIndex(this.btnSysMessage, 0);
            this.Controls.SetChildIndex(this.btnSafe, 0);
            this.Controls.SetChildIndex(this.btnSreach, 0);
            this.Controls.SetChildIndex(this.tbtn8, 0);
            this.Controls.SetChildIndex(this.tbtn1, 0);
            this.Controls.SetChildIndex(this.tbtn2, 0);
            this.Controls.SetChildIndex(this.tbtn3, 0);
            this.Controls.SetChildIndex(this.tbtn4, 0);
            this.Controls.SetChildIndex(this.tbtn5, 0);
            this.Controls.SetChildIndex(this.tbtn6, 0);
            this.Controls.SetChildIndex(this.tbtn7, 0);
            this.Controls.SetChildIndex(this.txtSearch, 0);
            this.Controls.SetChildIndex(this.picSreach, 0);
            this.Controls.SetChildIndex(this.ShowHead, 0);
            this.Controls.SetChildIndex(this.toolButton1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSreach)).EndInit();
            this.ctmsNotify.ResumeLayout(false);
            this.ctmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowHead)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNickName;
        private System.Windows.Forms.Label lblAutograph;
        private QQControls.ToolButton tbtnZone;
        private QQControls.ToolButton tbtnEmail;
        private QQControls.ToolButton tbtnSchool;
        private System.Windows.Forms.Panel pnlMenu;
        private QQControls.ToolButton tbtnManager;
        private QQControls.ToolButton btnSysMessage;
        private System.Windows.Forms.Button btnSafe;
        private System.Windows.Forms.Button btnSreach;
        private QQControls.ToolButton tbtn8;
        private QQControls.ToolButton tbtn1;
        private QQControls.ToolButton tbtn2;
        private QQControls.ToolButton tbtn3;
        private QQControls.ToolButton tbtn4;
        private QQControls.ToolButton tbtn5;
        private QQControls.ToolButton tbtn6;
        private QQControls.ToolButton tbtn7;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.PictureBox picSreach;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        //private ALQQControl.Main_tab.MainTabCotrl.QQTabCotrl qqTabCotrl1;
        private ALQQControl.QQListBox listQQFriendList;
        private System.Windows.Forms.Timer tmrFlashSysMessage;
        private System.Windows.Forms.ContextMenuStrip ctmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteFriend;
        private System.Windows.Forms.ContextMenuStrip ctmsNotify;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.PictureBox ShowHead;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem 查看资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 电子邮件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 平台信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手机短信ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 发送文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 消息记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private QQControls.ToolButton toolButton1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem 业务系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作系统ToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 业务操作系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 信息查询系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 常用网站ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其它ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
    }
}