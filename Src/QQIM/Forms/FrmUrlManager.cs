using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Common;
using Entity;

namespace ClientView
{
    public partial class FrmUrlManager : Form
    {
        public FrmUrlManager()
        {
            InitializeComponent();
            ClientManager.Instance.UrlAddressEvent += new Action<PackageUrlAddress>(Instance_UrlAddressEvent);
            ClientManager.Instance.UrlGroupEvent += new Action<PackageUrlGroup>(Instance_UrlGroupEvent);
            ClientManager.Instance.GetUrlAddressEvent += new Action<PackageGetUrlTable>(Instance_GetUrlAddressEvent);
            dataGridView_urladdress.AutoGenerateColumns = false;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
        class Win32
        {
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
            public const uint SHGFI_SMALLICON = 0x1; // 'Small icon
            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
            [DllImport("shell32.dll")]
            public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);
        }
        private Control findControl(string controlName)
        {
            foreach (Control c in tabPage1.Controls)
            {
                if (c.Name == controlName)
                {
                    return c;
                }
            }
            return null;
        }

        void Instance_GetUrlAddressEvent(PackageGetUrlTable packagegeturltable)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                  comboBox_groupname.DataSource = AppContext.UrlGroup;
                  button_groupadd.Enabled = true;
                  button_groupadd.Text = "新增";
                  button_groupmodif.Enabled = true;
                  button_groupmodif.Text = "修改";
                  button_groupdelete.Enabled = true;
                  button_groupdelete.Text = "删除";
                  button_menuaddressadd.Enabled = true;
                  button_menuaddressadd.Text = "新增";
                  button_menuaddressmodif.Enabled = true;
                  button_menuaddressmodif.Text = "修改";
                  button_menuaddressdelete.Enabled = true;
                  button_menuaddressdelete.Text = "删除";
                  comboBox_groupname.Refresh();
                  comboBox_groupname_SelectedIndexChanged(null,null);
                  dataGridView_urladdress_Click(null, null);
                }));
            }
        }

        void Instance_UrlGroupEvent(PackageUrlGroup packurlgroup)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (packurlgroup.isSuccess)
                    {
                        MessageBox.Show("操作成功。");
                    }
                    else
                    {
                        MessageBox.Show("对不起，没有操作成功，具体原因不明。");
                    }

                }));
            }
        }

        private delegate void dd();

        void Instance_UrlAddressEvent(PackageUrlAddress packurladdress)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                    {
                        if (packurladdress.isSuccess)
                        {
                            MessageBox.Show("操作成功。");
                            try
                            {
                                    if (packurladdress.urladdress.UrlType == "app")
                                    {
                                        string s = "numericUpDown_appid" + packurladdress.urladdress.AppOrder;
                                        ((NumericUpDown)findControl("numericUpDown_appid" + packurladdress.urladdress.AppOrder)).Value = packurladdress.urladdress.id;
                                        ((Button)findControl("button_appsave" + packurladdress.urladdress.AppOrder)).Text = "保存";
                                        ((Button)findControl("button_appsave" + packurladdress.urladdress.AppOrder)).Enabled = true;
                                    }
                                    else if (packurladdress.urladdress.UrlType == "menu")
                                    {

                                    }
                                }
                            catch (Exception er) { }
                        }
                        else
                        {
                            MessageBox.Show("对不起，没有操作成功，具体原因不明。");
                            int s = packurladdress.urladdress.AppOrder;
                            dd a = delegate
                            {
                                ((Button)findControl("button_appsave" + s)).Text = "保存";
                                ((Button)findControl("button_appsave" + s)).Enabled = true;
                            };
                            ((Button)findControl("button_appsave" + s)).Invoke(a);
                        }

                    }));
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button_apploadaddress1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.*|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                Win32.SHGetFileInfo(openFileDialog1.FileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                Icon myIcon = Icon.FromHandle(shinfo.hIcon);
                pictureBox_app1.Image = myIcon.ToBitmap();
                textBox_address1.Text = openFileDialog1.FileName;
                textBox_appname1.Text = Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void button_apploadpic1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.png|*.png|*.gif|*.gif|*.bmp|*.bmp|*.ico|*.ico|*.jpg|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image im = Image.FromFile(openFileDialog1.FileName);
                if (im.Height > 100 || im.Width > 100)
                {
                    MessageBox.Show("图片太大了，请改小点重新加载。");
                    return;
                }
                pictureBox_app1.Image = im;
            }
        }

        private void button_apploadpic2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.png|*.png|*.gif|*.gif|*.bmp|*.bmp|*.ico|*.ico|*.jpg|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image im= Image.FromFile(openFileDialog1.FileName);
                if (im.Height > 100 || im.Width > 100)
                {
                    MessageBox.Show("图片太大了，请改小点重新加载。");
                    return;
                }
                pictureBox_app2.Image = im;
            }
        }

        private void button_apploadpic3_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.png|*.png|*.gif|*.gif|*.bmp|*.bmp|*.ico|*.ico|*.jpg|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image im = Image.FromFile(openFileDialog1.FileName);
                if (im.Height > 100 || im.Width > 100)
                {
                    MessageBox.Show("图片太大了，请改小点重新加载。");
                    return;
                }
                pictureBox_app3.Image = im;
            }
        }

        private void button_apploadpic4_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.png|*.png|*.gif|*.gif|*.bmp|*.bmp|*.ico|*.ico|*.jpg|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image im = Image.FromFile(openFileDialog1.FileName);
                if (im.Height > 100 || im.Width > 100)
                {
                    MessageBox.Show("图片太大了，请改小点重新加载。");
                    return;
                }
                pictureBox_app4.Image = im;
            }
        }

        private void button_apploadpic5_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.png|*.png|*.gif|*.gif|*.bmp|*.bmp|*.ico|*.ico|*.jpg|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image im = Image.FromFile(openFileDialog1.FileName);
                if (im.Height > 100 || im.Width > 100)
                {
                    MessageBox.Show("图片太大了，请改小点重新加载。");
                    return;
                }
                pictureBox_app5.Image = im;
            }
        }

        private void button_apploadpic6_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.png|*.png|*.gif|*.gif|*.bmp|*.bmp|*.ico|*.ico|*.jpg|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image im = Image.FromFile(openFileDialog1.FileName);
                if (im.Height > 100 || im.Width > 100)
                {
                    MessageBox.Show("图片太大了，请改小点重新加载。");
                    return;
                }
                pictureBox_app6.Image = im;
            }
        }

        private void button_apploadpic7_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.png|*.png|*.gif|*.gif|*.bmp|*.bmp|*.ico|*.ico|*.jpg|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image im = Image.FromFile(openFileDialog1.FileName);
                if (im.Height > 100 || im.Width > 100)
                {
                    MessageBox.Show("图片太大了，请改小点重新加载。");
                    return;
                }
                pictureBox_app7.Image = im;
            }
        }

        private void button_apploadpic8_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.png|*.png|*.gif|*.gif|*.bmp|*.bmp|*.ico|*.ico|*.jpg|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image im = Image.FromFile(openFileDialog1.FileName);
                if (im.Height > 100 || im.Width > 100)
                {
                    MessageBox.Show("图片太大了，请改小点重新加载。");
                    return;
                }
                pictureBox_app8.Image = im;
            }
        }

        private void button_apploadaddress2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.*|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                Win32.SHGetFileInfo(openFileDialog1.FileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                Icon myIcon = Icon.FromHandle(shinfo.hIcon);
                pictureBox_app2.Image = myIcon.ToBitmap();
                textBox_address2.Text = openFileDialog1.FileName;
                textBox_appname2.Text = Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void button_apploadaddress3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.*|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                Win32.SHGetFileInfo(openFileDialog1.FileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                Icon myIcon = Icon.FromHandle(shinfo.hIcon);
                pictureBox_app3.Image = myIcon.ToBitmap();
                textBox_address3.Text = openFileDialog1.FileName;
                textBox_appname3.Text = Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void button_apploadaddress4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.*|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                Win32.SHGetFileInfo(openFileDialog1.FileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                Icon myIcon = Icon.FromHandle(shinfo.hIcon);
                pictureBox_app4.Image = myIcon.ToBitmap();
                textBox_address4.Text = openFileDialog1.FileName;
                textBox_appname4.Text = Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void button_apploadaddress5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.*|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                Win32.SHGetFileInfo(openFileDialog1.FileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                Icon myIcon = Icon.FromHandle(shinfo.hIcon);
                pictureBox_app5.Image = myIcon.ToBitmap();
                textBox_address5.Text = openFileDialog1.FileName;
                textBox_appname5.Text = Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void button_apploadaddress6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.*|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                Win32.SHGetFileInfo(openFileDialog1.FileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                Icon myIcon = Icon.FromHandle(shinfo.hIcon);
                pictureBox_app6.Image = myIcon.ToBitmap();
                textBox_address6.Text = openFileDialog1.FileName;
                textBox_appname6.Text = Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void button_apploadaddress7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.*|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                Win32.SHGetFileInfo(openFileDialog1.FileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                Icon myIcon = Icon.FromHandle(shinfo.hIcon);
                pictureBox_app7.Image = myIcon.ToBitmap();
                textBox_address7.Text = openFileDialog1.FileName;
                textBox_appname7.Text = Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void button_apploadaddress8_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.*|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                Win32.SHGetFileInfo(openFileDialog1.FileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                Icon myIcon = Icon.FromHandle(shinfo.hIcon);
                pictureBox_app8.Image = myIcon.ToBitmap();
                textBox_address8.Text = openFileDialog1.FileName;
                textBox_appname8.Text = Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox_address1.Text == "" || textBox_appname1.Text == "" || pictureBox_app1.Image == null)
            {
                MessageBox.Show("名称、地址、图片必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new  PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "app";
            packageurladdress.urladdress.GroupId = -1;
            packageurladdress.urladdress.AppOrder = 1;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id; 
            packageurladdress.urladdress.id = Convert.ToInt32(numericUpDown_appid1.Value);
            packageurladdress.urladdress.UrlName = textBox_appname1.Text;
            packageurladdress.urladdress.Urladdress = textBox_address1.Text;
            packageurladdress.urladdress.UrlImage = Functions.imageToByteArray(pictureBox_app1.Image);
            if (packageurladdress.urladdress.id == 0)
            {
                packageurladdress.operate = "insert";
            }
            else
            {
                packageurladdress.operate = "update";
            }
            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_appsave1.Enabled = false;
            button_appsave1.Text = "正在提交，请稍后...";

        }

        private void button_appsave2_Click(object sender, EventArgs e)
        {
            if (textBox_address2.Text == "" || textBox_appname2.Text == "" || pictureBox_app2.Image == null)
            {
                MessageBox.Show("名称、地址、图片必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new  PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "app";
            packageurladdress.urladdress.GroupId = -1;
            packageurladdress.urladdress.AppOrder = 2;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id; ;
            packageurladdress.urladdress.id = Convert.ToInt32(numericUpDown_appid2.Value);
            packageurladdress.urladdress.UrlName = textBox_appname2.Text;
            packageurladdress.urladdress.Urladdress = textBox_address2.Text;
            packageurladdress.urladdress.UrlImage = Functions.imageToByteArray(pictureBox_app2.Image);
            if (packageurladdress.urladdress.id == 0)
            {
                packageurladdress.operate = "insert";
            }
            else
            {
                packageurladdress.operate = "update";
            }
            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_appsave2.Enabled = false;
            button_appsave2.Text = "正在提交，请稍后...";
        }

        private void button_appsave3_Click(object sender, EventArgs e)
        {
            if (textBox_address3.Text == "" || textBox_appname3.Text == "" || pictureBox_app3.Image == null)
            {
                MessageBox.Show("名称、地址、图片必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "app";
            packageurladdress.urladdress.GroupId = -1;
            packageurladdress.urladdress.AppOrder = 3;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id; ;
            packageurladdress.urladdress.id = Convert.ToInt32(numericUpDown_appid3.Value);
            packageurladdress.urladdress.UrlName = textBox_appname3.Text;
            packageurladdress.urladdress.Urladdress = textBox_address3.Text;
            packageurladdress.urladdress.UrlImage = Functions.imageToByteArray(pictureBox_app3.Image);
            if (packageurladdress.urladdress.id == 0)
            {
                packageurladdress.operate = "insert";
            }
            else
            {
                packageurladdress.operate = "update";
            }
            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_appsave3.Enabled = false;
            button_appsave3.Text = "正在提交，请稍后...";
        }

        private void button_appsave4_Click(object sender, EventArgs e)
        {
            if (textBox_address4.Text == "" || textBox_appname4.Text == "" || pictureBox_app4.Image == null)
            {
                MessageBox.Show("名称、地址、图片必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "app";
            packageurladdress.urladdress.GroupId = -1;
            packageurladdress.urladdress.AppOrder = 4;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id; ;
            packageurladdress.urladdress.id = Convert.ToInt32(numericUpDown_appid4.Value);
            packageurladdress.urladdress.UrlName = textBox_appname4.Text;
            packageurladdress.urladdress.Urladdress = textBox_address4.Text;
            packageurladdress.urladdress.UrlImage = Functions.imageToByteArray(pictureBox_app4.Image);
            if (packageurladdress.urladdress.id == 0)
            {
                packageurladdress.operate = "insert";
            }
            else
            {
                packageurladdress.operate = "update";
            }
            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_appsave4.Enabled = false;
            button_appsave4.Text = "正在提交，请稍后...";
        }

        private void button_appsave5_Click(object sender, EventArgs e)
        {
            if (textBox_address5.Text == "" || textBox_appname5.Text == "" || pictureBox_app5.Image == null)
            {
                MessageBox.Show("名称、地址、图片必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "app";
            packageurladdress.urladdress.GroupId = -1;
            packageurladdress.urladdress.AppOrder = 5;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id; ;
            packageurladdress.urladdress.id = Convert.ToInt32(numericUpDown_appid5.Value);
            packageurladdress.urladdress.UrlName = textBox_appname5.Text;
            packageurladdress.urladdress.Urladdress = textBox_address5.Text;
            packageurladdress.urladdress.UrlImage = Functions.imageToByteArray(pictureBox_app5.Image);
            if (packageurladdress.urladdress.id == 0)
            {
                packageurladdress.operate = "insert";
            }
            else
            {
                packageurladdress.operate = "update";
            }
            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_appsave5.Enabled = false;
            button_appsave5.Text = "正在提交，请稍后...";
        }

        private void button_appsave6_Click(object sender, EventArgs e)
        {
            if (textBox_address6.Text == "" || textBox_appname6.Text == "" || pictureBox_app6.Image == null)
            {
                MessageBox.Show("名称、地址、图片必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "app";
            packageurladdress.urladdress.GroupId = -1;
            packageurladdress.urladdress.AppOrder = 6;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id; ;
            packageurladdress.urladdress.id = Convert.ToInt32(numericUpDown_appid6.Value);
            packageurladdress.urladdress.UrlName = textBox_appname6.Text;
            packageurladdress.urladdress.Urladdress = textBox_address6.Text;
            packageurladdress.urladdress.UrlImage = Functions.imageToByteArray(pictureBox_app6.Image);
            if (packageurladdress.urladdress.id == 0)
            {
                packageurladdress.operate = "insert";
            }
            else
            {
                packageurladdress.operate = "update";
            }
            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_appsave6.Enabled = false;
            button_appsave6.Text = "正在提交，请稍后...";
        }

        private void button_appsave7_Click(object sender, EventArgs e)
        {
            if (textBox_address7.Text == "" || textBox_appname7.Text == "" || pictureBox_app7.Image == null)
            {
                MessageBox.Show("名称、地址、图片必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "app";
            packageurladdress.urladdress.GroupId = -1;
            packageurladdress.urladdress.AppOrder = 7;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id; ;
            packageurladdress.urladdress.id = Convert.ToInt32(numericUpDown_appid7.Value);
            packageurladdress.urladdress.UrlName = textBox_appname7.Text;
            packageurladdress.urladdress.Urladdress = textBox_address7.Text;
            packageurladdress.urladdress.UrlImage = Functions.imageToByteArray(pictureBox_app7.Image);
            if (packageurladdress.urladdress.id == 0)
            {
                packageurladdress.operate = "insert";
            }
            else
            {
                packageurladdress.operate = "update";
            }
            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_appsave7.Enabled = false;
            button_appsave7.Text = "正在提交，请稍后...";
        }

        private void button_appsave8_Click(object sender, EventArgs e)
        {
            if (textBox_address8.Text == "" || textBox_appname8.Text == "" || pictureBox_app8.Image == null)
            {
                MessageBox.Show("名称、地址、图片必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "app";
            packageurladdress.urladdress.GroupId = -1;
            packageurladdress.urladdress.AppOrder = 8;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id; ;
            packageurladdress.urladdress.id = Convert.ToInt32(numericUpDown_appid8.Value);
            packageurladdress.urladdress.UrlName = textBox_appname8.Text;
            packageurladdress.urladdress.Urladdress = textBox_address8.Text;
            packageurladdress.urladdress.UrlImage = Functions.imageToByteArray(pictureBox_app8.Image);
            if (packageurladdress.urladdress.id == 0)
            {
                packageurladdress.operate = "insert";
            }
            else
            {
                packageurladdress.operate = "update";
            }
            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_appsave8.Enabled = false;
            button_appsave8.Text = "正在提交，请稍后...";
        }

        private void FrmUrlManager_Load(object sender, EventArgs e)
        {
            foreach (DataRow dr in AppContext.UrlAddress.Rows)
            {
                try
                {
                    if (dr["UrlType"].ToString() == "app")
                    {
                        string s = dr["AppOrder"].ToString();
                        ((NumericUpDown)findControl("numericUpDown_appid" + s)).Value = int.Parse(dr["id"].ToString());
                        ((TextBox)findControl("textBox_appname" + s)).Text = dr["UrlName"].ToString();
                        ((TextBox)findControl("textBox_address" + s)).Text = dr["UrlAddress"].ToString();
                        ((PictureBox)findControl("pictureBox_app" + s)).Image = Functions.byteArrayToImage((byte[])dr["UrlImage"]);
                    }
                }
                catch(Exception er)
                {

                }
            }
            comboBox_groupname.DataSource = AppContext.UrlGroup;
        }

        private void comboBox_groupname_SelectedIndexChanged(object sender, EventArgs e)
        {

            //AppContext.UrlAddress.Select(" GroupId=" + comboBox_groupname.SelectedValue);
            //IEnumerable<DataRow> query = from order in AppContext.UrlAddress.AsEnumerable()
            //                             where order.Field<Int32>("GroupId") == Int32.Parse(comboBox_groupname.SelectedValue.ToString())
            //                             select order;
            try
            {
                DataTable boundTable = AppContext.UrlAddress.Select(" GroupId=" + comboBox_groupname.SelectedValue).CopyToDataTable<DataRow>();
                if (boundTable.Rows.Count > 0)
                    dataGridView_urladdress.DataSource = boundTable;
                else
                    dataGridView_urladdress.DataSource = null;
            }
            catch { dataGridView_urladdress.DataSource = null; }
            textBox_groupname.Text = comboBox_groupname.Text;
            dataGridView_urladdress_Click(null, null);
          //  dataGridView_urladdress.BindingContext()
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (comboBox_groupname.FindString(textBox_groupname.Text) > -1)
            {
                MessageBox.Show("对不起，该组名已经存在。");
                return;
            }
            PackageUrlGroup packageurlgroup = new  PackageUrlGroup();
            UrlGroup u = new  UrlGroup();
            packageurlgroup.urlgroup = u;
            packageurlgroup.urlgroup.GroupName = textBox_groupname.Text;
            packageurlgroup.urlgroup.Owner = AppContext.LoginUser.Id; ;
            packageurlgroup.operate = "insert";
            packageurlgroup.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurlgroup);
            button_groupadd.Enabled = false;
            button_groupadd.Text = "请稍后...";
        }

        private void button_groupmodif_Click(object sender, EventArgs e)
        {
            if (comboBox_groupname.SelectedIndex< 0)
            {
                MessageBox.Show("对不起，先选择要修改的组名。");
                return;
            }
            if (comboBox_groupname.FindString(textBox_groupname.Text) > -1)
            {
                MessageBox.Show("对不起，该组名已经存在。");
                return;
            }
            PackageUrlGroup packageurlgroup = new PackageUrlGroup();
            UrlGroup u = new UrlGroup();
            packageurlgroup.urlgroup = u;
            packageurlgroup.urlgroup.GroupName = textBox_groupname.Text;
            packageurlgroup.urlgroup.Owner = AppContext.LoginUser.Id;
            packageurlgroup.urlgroup.ID =Int32.Parse( comboBox_groupname.SelectedValue.ToString()); ;
            packageurlgroup.operate = "update";
            packageurlgroup.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurlgroup);
            button_groupmodif.Enabled = false;
            button_groupmodif.Text = "请稍后...";
        }

        private void button_groupdelete_Click(object sender, EventArgs e)
        {
            if (comboBox_groupname.SelectedIndex < 0)
            {
                MessageBox.Show("对不起，先选择组名。");
                return;
            }

            PackageUrlGroup packageurlgroup = new PackageUrlGroup();
            UrlGroup u = new UrlGroup();
            packageurlgroup.urlgroup = u;
            packageurlgroup.urlgroup.GroupName = textBox_groupname.Text;
            packageurlgroup.urlgroup.Owner = AppContext.LoginUser.Id;
            packageurlgroup.urlgroup.ID = Int32.Parse(comboBox_groupname.SelectedValue.ToString()); 
            packageurlgroup.operate = "delete";
            packageurlgroup.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurlgroup);
            button_groupdelete.Enabled = false;
            button_groupdelete.Text = "请稍后...";
        }

        private void button_menuaddressadd_Click(object sender, EventArgs e)
        {
            if (textBox_urlname.Text == "" || textBox_urladdress.Text == "" )
            {
                MessageBox.Show("名称、地址必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "menu";
            packageurladdress.urladdress.GroupId = Int32.Parse(comboBox_groupname.SelectedValue.ToString());
            packageurladdress.urladdress.AppOrder = 0;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id;
            packageurladdress.urladdress.id = -1;
            packageurladdress.urladdress.UrlName = textBox_urlname.Text;
            packageurladdress.urladdress.Urladdress = textBox_urladdress.Text;

                packageurladdress.operate = "insert";

            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_menuaddressadd.Enabled = false;
            button_menuaddressadd.Text = "正在提交，请稍后...";
        }

        private void button_menuaddressmodif_Click(object sender, EventArgs e)
        {
            if (textBox_urlname.Text == "" || textBox_urladdress.Text == "")
            {
                MessageBox.Show("名称、地址必需都要填写全面。");
                return;
            }
            PackageUrlAddress packageurladdress = new PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "menu";
            packageurladdress.urladdress.GroupId = Int32.Parse(comboBox_groupname.SelectedValue.ToString());
            packageurladdress.urladdress.AppOrder = 0;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id;
            packageurladdress.urladdress.id = int.Parse(dataGridView_urladdress.CurrentRow.Cells[0].Value.ToString());
            packageurladdress.urladdress.UrlName = textBox_urlname.Text;
            packageurladdress.urladdress.Urladdress = textBox_urladdress.Text;

            packageurladdress.operate = "update";

            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_menuaddressmodif.Enabled = false;
            button_menuaddressmodif.Text = "正在提交，请稍后...";
        }

        private void button_menuaddressdelete_Click(object sender, EventArgs e)
        {
            if (dataGridView_urladdress.CurrentRow == null)
            {
                MessageBox.Show("对不起，没有选择数据行。");
                return;
            }
            if (MessageBox.Show("确定要删除吗？","删除",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
            {
                return;
            }
            PackageUrlAddress packageurladdress = new PackageUrlAddress();
            UrlAddress u = new UrlAddress();
            packageurladdress.urladdress = u;
            packageurladdress.urladdress.UrlType = "menu";
            packageurladdress.urladdress.GroupId = Int32.Parse(comboBox_groupname.SelectedValue.ToString());
            packageurladdress.urladdress.AppOrder = 0;
            packageurladdress.urladdress.Owner = AppContext.LoginUser.Id;
            packageurladdress.urladdress.id = int.Parse(dataGridView_urladdress.CurrentRow.Cells[0].Value.ToString());
            //packageurladdress.urladdress.UrlName = textBox_urlname.Text;
            //packageurladdress.urladdress.Urladdress = textBox_urladdress.Text;

            packageurladdress.operate = "delete";

            packageurladdress.SendId = AppContext.LoginUser.Id;
            ClientManager.Instance.ClientSendData(packageurladdress);
            button_menuaddressdelete.Enabled = false;
            button_menuaddressdelete.Text = "正在提交，请稍后...";
        }

        private void dataGridView_urladdress_Click(object sender, EventArgs e)
        {
            try
            {
                textBox_urlname.Text = dataGridView_urladdress.CurrentRow.Cells[1].Value.ToString();
                textBox_urladdress.Text = dataGridView_urladdress.CurrentRow.Cells[2].Value.ToString();
            }
            catch { }

        }

        private void button25_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
