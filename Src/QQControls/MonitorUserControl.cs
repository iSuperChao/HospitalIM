using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace QQControls
{
    public partial class MonitorUserControl : UserControl
    {
        #region Win32API������װ
        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey
        (
            uint uCode,
            uint uMapType
        );
        #endregion

        #region �ֶ�
        private Monitor robj;
        private Bitmap m_Bitmap = null;
        private bool Control = false;
        private Size desktopWindowSize;
        #endregion

        #region ���캯��
        public MonitorUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region ��ʼ��
        public void Initialize(string remoteMachine,int remoteport)
        {
            try
            {
                try
                {
                    ChannelServices.RegisterChannel(new TcpChannel(), false);
                }
                catch { }
                robj = (Monitor)Activator.GetObject(typeof(Monitor), "tcp://" + remoteMachine + ":" + remoteport+ "/MonitorServerUrl");
                desktopWindowSize = robj.GetDesktopBitmapSize();
                m_Bitmap = new Bitmap(desktopWindowSize.Width, desktopWindowSize.Height);
                this.AutoScrollMinSize = desktopWindowSize;
                UpdateDisplay();
            }
            catch
            {
                
            }
        }
        #endregion

        #region ����
        private void MonitorUserControl_Paint(object sender, PaintEventArgs e)
        {
            System.Threading.Monitor.Enter(this);
            if (m_Bitmap != null)
            {
                Point P = new Point(AutoScrollPosition.X, AutoScrollPosition.Y);
                //e.Graphics.DrawImage(m_Bitmap, P);
                this.pictureBox1.Image = m_Bitmap;
            }
            System.Threading.Monitor.Exit(this);
        }
        #endregion

        #region ����ƶ�
        private void MonitorUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (Control)
            {
                float x = (float)(desktopWindowSize.Width / this.Width);
                float y = (float)(desktopWindowSize.Height / this.Height);
                robj.MoveMouse(e.X,e.Y);
                //labelremote.Text = "�Է���Ļ��С:XY:"+desktopWindowSize.Width+","+desktopWindowSize.Height;
                //labelthis.Text = "���ؼ���С:XY:" + this.Width + "," + this.Height;
                //labelexy.Text = "���ؼ������λ�ã�" + e.X + "," + e.Y;
                //labelremotexy.Text="�����Զ�����λ��:"+(int)((e.X) * x)+","+ (int)((e.Y) * y);
            }
        }
        #endregion

        #region ��갴���ͷ�
        private void MonitorUserControl_MouseUp(object sender, MouseEventArgs e)
        {
            float x = (float)(desktopWindowSize.Width / this.Width);
            float y = (float)(desktopWindowSize.Height / this.Height);
            robj.PressOrReleaseMouseButton(false, e.Button == MouseButtons.Left, e.X, e.Y);
            //labelremote.Text = "�Է���Ļ��С:XY:" + desktopWindowSize.Width + "," + desktopWindowSize.Height;
            //labelthis.Text = "���ؼ���С:XY:" + this.Width + "," + this.Height;
            //labelexy.Text = "���ؼ������λ�ã�" + e.X + "," + e.Y;
            //labelremotexy.Text = "�����Զ�����λ��:" + (int)((e.X) * x) + "," + (int)((e.Y) * y);
        }
        #endregion

        #region ��갴������
        private void MonitorUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            float x = (float)(desktopWindowSize.Width / this.Width);
            float y = (float)(desktopWindowSize.Height / this.Height);
           // robj.PressOrReleaseMouseButton(true, e.Button == MouseButtons.Left, (int)((e.X) * x), (int)((e.Y) * y));
            robj.PressOrReleaseMouseButton(true, e.Button == MouseButtons.Left, e.X, e.Y);
            //labelremote.Text = "�Է���Ļ��С:XY:" + desktopWindowSize.Width + "," + desktopWindowSize.Height;
            //labelthis.Text = "���ؼ���С:XY:" + this.Width + "," + this.Height;
            //labelexy.Text = "���ؼ������λ�ã�" + e.X + "," + e.Y;
            //labelremotexy.Text = "�����Զ�����λ��:" + (int)((e.X) * x) + "," + (int)((e.Y) * y);
        }
        #endregion

        #region ��������
        private void MonitorUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            robj.SendKeystroke((byte)e.KeyCode, (byte)MapVirtualKey((uint)e.KeyCode, 0), true, false);
        }
        #endregion

        #region �����ͷ�
        private void MonitorUserControl_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            robj.SendKeystroke((byte)e.KeyCode, (byte)MapVirtualKey((uint)e.KeyCode, 0), false, false);
        }
        #endregion

        #region ����
        public void SetControl(bool Control)
        {
            this.Control = Control;
        }

        public void UpdateDisplay()
        {
            System.Threading.Monitor.Enter(this);

            try
            {
                byte[] BitmapBytes = robj.GetDesktopBitmapBytes();

                if (BitmapBytes.Length > 0)
                {
                    MemoryStream MS = new MemoryStream(BitmapBytes, false);
                    m_Bitmap = (Bitmap)Image.FromStream(MS);
                  //  m_Bitmap = new Size(this.Width,this.Height);

                    Point P = new Point(AutoScrollPosition.X, AutoScrollPosition.Y);
                   // CreateGraphics().DrawImage(m_Bitmap, P);
                    this.pictureBox1.Image = m_Bitmap;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch
            {

            }
            System.Threading.Monitor.Exit(this);
            System.Threading.Thread.Sleep(100);
        }
        #endregion
    }
}
