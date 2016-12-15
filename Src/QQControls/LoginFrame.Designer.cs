namespace QQControls
{
    partial class LoginFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginFrame));
            this.picTitle = new System.Windows.Forms.PictureBox();
            this.mbtnClose = new QQControls.MyButton();
            this.mbtnMin = new QQControls.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // picTitle
            // 
            this.picTitle.BackColor = System.Drawing.Color.Transparent;
            this.picTitle.Image = ((System.Drawing.Image)(resources.GetObject("picTitle.Image")));
            this.picTitle.Location = new System.Drawing.Point(11, 7);
            this.picTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picTitle.Name = "picTitle";
            this.picTitle.Size = new System.Drawing.Size(80, 20);
            this.picTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTitle.TabIndex = 1;
            this.picTitle.TabStop = false;
            // 
            // mbtnClose
            // 
            this.mbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mbtnClose.BackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnClose.BackgroundImage")));
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.BtnType = QQControls.MyButton.ButtonType.Close;
            this.mbtnClose.Content = "";
            this.mbtnClose.Down = ((System.Drawing.Image)(resources.GetObject("mbtnClose.Down")));
            this.mbtnClose.Location = new System.Drawing.Point(348, -1);
            this.mbtnClose.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mbtnClose.MoveBg = ((System.Drawing.Image)(resources.GetObject("mbtnClose.MoveBg")));
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Normal = ((System.Drawing.Image)(resources.GetObject("mbtnClose.Normal")));
            this.mbtnClose.Size = new System.Drawing.Size(52, 27);
            this.mbtnClose.TabIndex = 3;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            // 
            // mbtnMin
            // 
            this.mbtnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mbtnMin.BackColor = System.Drawing.Color.Transparent;
            this.mbtnMin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnMin.BackgroundImage")));
            this.mbtnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnMin.BtnType = QQControls.MyButton.ButtonType.Minimum;
            this.mbtnMin.Content = "";
            this.mbtnMin.Down = ((System.Drawing.Image)(resources.GetObject("mbtnMin.Down")));
            this.mbtnMin.Location = new System.Drawing.Point(315, -1);
            this.mbtnMin.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mbtnMin.MoveBg = ((System.Drawing.Image)(resources.GetObject("mbtnMin.MoveBg")));
            this.mbtnMin.Name = "mbtnMin";
            this.mbtnMin.Normal = ((System.Drawing.Image)(resources.GetObject("mbtnMin.Normal")));
            this.mbtnMin.Size = new System.Drawing.Size(37, 27);
            this.mbtnMin.TabIndex = 2;
            this.mbtnMin.Click += new System.EventHandler(this.mbtnMin_Click);
            // 
            // LoginFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(400, 347);
            this.Controls.Add(this.mbtnClose);
            this.Controls.Add(this.mbtnMin);
            this.Controls.Add(this.picTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LoginFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginFrame";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFrame_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picTitle;
        private MyButton mbtnMin;
        private MyButton mbtnClose;

    }
}