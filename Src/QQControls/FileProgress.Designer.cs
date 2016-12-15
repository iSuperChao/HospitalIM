namespace QQControls
{
    partial class FileProgress
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.p = new System.Windows.Forms.ProgressBar();
            this.l = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // p
            // 
            this.p.Dock = System.Windows.Forms.DockStyle.Top;
            this.p.Location = new System.Drawing.Point(0, 0);
            this.p.Name = "p";
            this.p.Size = new System.Drawing.Size(203, 23);
            this.p.TabIndex = 0;
            // 
            // l
            // 
            this.l.AutoSize = true;
            this.l.Dock = System.Windows.Forms.DockStyle.Top;
            this.l.Location = new System.Drawing.Point(0, 23);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(56, 16);
            this.l.TabIndex = 0;
            this.l.Text = "label1";
            // 
            // FileProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.l);
            this.Controls.Add(this.p);
            this.Name = "FileProgress";
            this.Size = new System.Drawing.Size(203, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar p;
        private System.Windows.Forms.Label l;
    }
}
