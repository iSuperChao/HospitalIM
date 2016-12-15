namespace ClientView
{
    partial class FrmSysMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSysMessage));
            this.btnCancel = new QQControls.MyButton();
            this.btnAddFriend = new QQControls.MyButton();
            this.lklFromUserId = new System.Windows.Forms.LinkLabel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.showHead = new QQControls.ShowHead();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.BtnType = QQControls.MyButton.ButtonType.Normal;
            this.btnCancel.Content = "取消";
            this.btnCancel.Down = ((System.Drawing.Image)(resources.GetObject("btnCancel.Down")));
            this.btnCancel.Location = new System.Drawing.Point(471, 320);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnCancel.MoveBg = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveBg")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Normal = ((System.Drawing.Image)(resources.GetObject("btnCancel.Normal")));
            this.btnCancel.Size = new System.Drawing.Size(107, 33);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddFriend
            // 
            this.btnAddFriend.BackColor = System.Drawing.Color.Transparent;
            this.btnAddFriend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddFriend.BackgroundImage")));
            this.btnAddFriend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddFriend.BtnType = QQControls.MyButton.ButtonType.Normal;
            this.btnAddFriend.Content = "加为好友";
            this.btnAddFriend.Down = ((System.Drawing.Image)(resources.GetObject("btnAddFriend.Down")));
            this.btnAddFriend.Location = new System.Drawing.Point(354, 320);
            this.btnAddFriend.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnAddFriend.MoveBg = ((System.Drawing.Image)(resources.GetObject("btnAddFriend.MoveBg")));
            this.btnAddFriend.Name = "btnAddFriend";
            this.btnAddFriend.Normal = ((System.Drawing.Image)(resources.GetObject("btnAddFriend.Normal")));
            this.btnAddFriend.Size = new System.Drawing.Size(107, 33);
            this.btnAddFriend.TabIndex = 6;
            this.btnAddFriend.Click += new System.EventHandler(this.btnAddFriend_Click);
            // 
            // lklFromUserId
            // 
            this.lklFromUserId.AutoSize = true;
            this.lklFromUserId.BackColor = System.Drawing.Color.Transparent;
            this.lklFromUserId.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lklFromUserId.Location = new System.Drawing.Point(123, 50);
            this.lklFromUserId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklFromUserId.Name = "lklFromUserId";
            this.lklFromUserId.Size = new System.Drawing.Size(88, 16);
            this.lklFromUserId.TabIndex = 7;
            this.lklFromUserId.TabStop = true;
            this.lklFromUserId.Text = "FromUserId";
            this.lklFromUserId.Click += new System.EventHandler(this.lklFromUserId_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Location = new System.Drawing.Point(120, 78);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(136, 16);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.Text = "请求加您为好友！";
            // 
            // showHead
            // 
            this.showHead.BackColor = System.Drawing.Color.Transparent;
            this.showHead.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("showHead.BackgroundImage")));
            this.showHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.showHead.HeadImage = null;
            this.showHead.Location = new System.Drawing.Point(23, 50);
            this.showHead.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.showHead.Name = "showHead";
            this.showHead.Size = new System.Drawing.Size(88, 87);
            this.showHead.TabIndex = 9;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(123, 107);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(455, 172);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(120, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(472, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄";
            // 
            // FrmSysMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 380);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.showHead);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddFriend);
            this.Controls.Add(this.lklFromUserId);
            this.Controls.Add(this.lblMessage);
            this.FormHeight = 380;
            this.FormWidth = 600;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmSysMessage";
            this.SystemButton = QQControls.MainFrame.sysButton.close;
            this.Text = "系统消息";
            this.Controls.SetChildIndex(this.lblMessage, 0);
            this.Controls.SetChildIndex(this.lklFromUserId, 0);
            this.Controls.SetChildIndex(this.btnAddFriend, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.showHead, 0);
            this.Controls.SetChildIndex(this.richTextBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private QQControls.MyButton btnCancel;
        private QQControls.MyButton btnAddFriend;
        private System.Windows.Forms.LinkLabel lklFromUserId;
        private System.Windows.Forms.Label lblMessage;
        private QQControls.ShowHead showHead;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;

    }
}