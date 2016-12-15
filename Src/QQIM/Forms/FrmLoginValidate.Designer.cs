namespace ClientView
{
    partial class FrmLoginValidate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoginValidate));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.picLogining = new System.Windows.Forms.PictureBox();
            this.btn_Close = new QQControls.MyButton();
            this.lblQQNumber = new System.Windows.Forms.Label();
            this.lblLogining = new System.Windows.Forms.Label();
            this.tmrNotifyIcon = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picLogining)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            // 
            // picLogining
            // 
            this.picLogining.BackColor = System.Drawing.Color.Transparent;
            this.picLogining.Image = ((System.Drawing.Image)(resources.GetObject("picLogining.Image")));
            this.picLogining.Location = new System.Drawing.Point(117, 230);
            this.picLogining.Margin = new System.Windows.Forms.Padding(4);
            this.picLogining.Name = "picLogining";
            this.picLogining.Size = new System.Drawing.Size(142, 151);
            this.picLogining.TabIndex = 6;
            this.picLogining.TabStop = false;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Close.BackgroundImage")));
            this.btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Close.BtnType = QQControls.MyButton.ButtonType.Normal;
            this.btn_Close.Content = "取消";
            this.btn_Close.Down = ((System.Drawing.Image)(resources.GetObject("btn_Close.Down")));
            this.btn_Close.Location = new System.Drawing.Point(127, 453);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Close.MoveBg = ((System.Drawing.Image)(resources.GetObject("btn_Close.MoveBg")));
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Normal = ((System.Drawing.Image)(resources.GetObject("btn_Close.Normal")));
            this.btn_Close.Size = new System.Drawing.Size(115, 37);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Load += new System.EventHandler(this.btn_Close_Load);
            // 
            // lblQQNumber
            // 
            this.lblQQNumber.AutoSize = true;
            this.lblQQNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblQQNumber.Location = new System.Drawing.Point(163, 384);
            this.lblQQNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQQNumber.Name = "lblQQNumber";
            this.lblQQNumber.Size = new System.Drawing.Size(40, 16);
            this.lblQQNumber.TabIndex = 8;
            this.lblQQNumber.Text = "工号";
            // 
            // lblLogining
            // 
            this.lblLogining.AutoSize = true;
            this.lblLogining.BackColor = System.Drawing.Color.Transparent;
            this.lblLogining.Location = new System.Drawing.Point(140, 405);
            this.lblLogining.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogining.Name = "lblLogining";
            this.lblLogining.Size = new System.Drawing.Size(96, 16);
            this.lblLogining.TabIndex = 9;
            this.lblLogining.Text = "正在登录...";
            // 
            // tmrNotifyIcon
            // 
            this.tmrNotifyIcon.Enabled = true;
            this.tmrNotifyIcon.Interval = 250;
            this.tmrNotifyIcon.Tick += new System.EventHandler(this.tmrNotifyIcon_Tick);
            // 
            // FrmLoginValidate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 693);
            this.Controls.Add(this.lblLogining);
            this.Controls.Add(this.lblQQNumber);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.picLogining);
            this.FormWidth = 373;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLoginValidate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SystemButton = QQControls.MainFrame.sysButton.minClose;
            this.Text = "QQ2011";
            this.Controls.SetChildIndex(this.picLogining, 0);
            this.Controls.SetChildIndex(this.btn_Close, 0);
            this.Controls.SetChildIndex(this.lblQQNumber, 0);
            this.Controls.SetChildIndex(this.lblLogining, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picLogining)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.PictureBox picLogining;
        private QQControls.MyButton btn_Close;
        private System.Windows.Forms.Label lblQQNumber;
        private System.Windows.Forms.Label lblLogining;
        private System.Windows.Forms.Timer tmrNotifyIcon;
    }
}