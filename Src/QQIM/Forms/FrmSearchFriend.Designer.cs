namespace ClientView
{
    partial class FrmSearchFriend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchFriend));
            this.lblTitleHead = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblShuoMing = new System.Windows.Forms.Label();
            this.listQQFriend = new ALQQControl.QQListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mbtnAddFriend = new QQControls.MyButton();
            this.mbtnCancel = new QQControls.MyButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitleHead
            // 
            this.lblTitleHead.AutoSize = true;
            this.lblTitleHead.BackColor = System.Drawing.Color.Transparent;
            this.lblTitleHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleHead.Location = new System.Drawing.Point(39, 9);
            this.lblTitleHead.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleHead.Name = "lblTitleHead";
            this.lblTitleHead.Size = new System.Drawing.Size(136, 24);
            this.lblTitleHead.TabIndex = 9;
            this.lblTitleHead.Text = "查找联系人/群";
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.Location = new System.Drawing.Point(13, 9);
            this.picIcon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(21, 21);
            this.picIcon.TabIndex = 8;
            this.picIcon.TabStop = false;
            // 
            // lblShuoMing
            // 
            this.lblShuoMing.AutoSize = true;
            this.lblShuoMing.BackColor = System.Drawing.Color.Transparent;
            this.lblShuoMing.Location = new System.Drawing.Point(72, 51);
            this.lblShuoMing.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShuoMing.Name = "lblShuoMing";
            this.lblShuoMing.Size = new System.Drawing.Size(152, 16);
            this.lblShuoMing.TabIndex = 11;
            this.lblShuoMing.Text = "以下是为您查找到的";
            // 
            // listQQFriend
            // 
            this.listQQFriend.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listQQFriend.FormattingEnabled = true;
            this.listQQFriend.Location = new System.Drawing.Point(7, 83);
            this.listQQFriend.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listQQFriend.Name = "listQQFriend";
            this.listQQFriend.Size = new System.Drawing.Size(652, 387);
            this.listQQFriend.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(29, 43);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(35, 32);
            this.panel1.TabIndex = 13;
            // 
            // mbtnAddFriend
            // 
            this.mbtnAddFriend.BackColor = System.Drawing.Color.Transparent;
            this.mbtnAddFriend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnAddFriend.BackgroundImage")));
            this.mbtnAddFriend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbtnAddFriend.BtnType = QQControls.MyButton.ButtonType.Normal;
            this.mbtnAddFriend.Content = "加为联系人";
            this.mbtnAddFriend.Down = ((System.Drawing.Image)(resources.GetObject("mbtnAddFriend.Down")));
            this.mbtnAddFriend.Location = new System.Drawing.Point(421, 479);
            this.mbtnAddFriend.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mbtnAddFriend.MoveBg = ((System.Drawing.Image)(resources.GetObject("mbtnAddFriend.MoveBg")));
            this.mbtnAddFriend.Name = "mbtnAddFriend";
            this.mbtnAddFriend.Normal = ((System.Drawing.Image)(resources.GetObject("mbtnAddFriend.Normal")));
            this.mbtnAddFriend.Size = new System.Drawing.Size(107, 33);
            this.mbtnAddFriend.TabIndex = 14;
            this.mbtnAddFriend.Click += new System.EventHandler(this.mbtnAddFriend_Click);
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.BackColor = System.Drawing.Color.Transparent;
            this.mbtnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnCancel.BackgroundImage")));
            this.mbtnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbtnCancel.BtnType = QQControls.MyButton.ButtonType.Normal;
            this.mbtnCancel.Content = "取消";
            this.mbtnCancel.Down = ((System.Drawing.Image)(resources.GetObject("mbtnCancel.Down")));
            this.mbtnCancel.Location = new System.Drawing.Point(545, 479);
            this.mbtnCancel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mbtnCancel.MoveBg = ((System.Drawing.Image)(resources.GetObject("mbtnCancel.MoveBg")));
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Normal = ((System.Drawing.Image)(resources.GetObject("mbtnCancel.Normal")));
            this.mbtnCancel.Size = new System.Drawing.Size(107, 33);
            this.mbtnCancel.TabIndex = 14;
            this.mbtnCancel.Load += new System.EventHandler(this.mbtnCancel_Load);
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(14, 491);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "超过30条的，只显示前30条记录";
            // 
            // FrmSearchFriend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 533);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnAddFriend);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listQQFriend);
            this.Controls.Add(this.lblShuoMing);
            this.Controls.Add(this.lblTitleHead);
            this.Controls.Add(this.picIcon);
            this.FormHeight = 533;
            this.FormWidth = 667;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmSearchFriend";
            this.SystemButton = QQControls.MainFrame.sysButton.close;
            this.Text = "查找联系人/群";
            this.TitleVisible = true;
            this.Controls.SetChildIndex(this.picIcon, 0);
            this.Controls.SetChildIndex(this.lblTitleHead, 0);
            this.Controls.SetChildIndex(this.lblShuoMing, 0);
            this.Controls.SetChildIndex(this.listQQFriend, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.mbtnAddFriend, 0);
            this.Controls.SetChildIndex(this.mbtnCancel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitleHead;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblShuoMing;
        private ALQQControl.QQListBox listQQFriend;
        private System.Windows.Forms.Panel panel1;
        private QQControls.MyButton mbtnAddFriend;
        private QQControls.MyButton mbtnCancel;
        private System.Windows.Forms.Label label1;
    }
}