using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQControls;
using System.Runtime.Serialization.Formatters.Binary;
using Common;
using Entity;

namespace ClientView
{
    /// <summary>
    /// 民警修改资料窗体
    /// </summary>
    public partial class FrmModifUserInfo : RegisterFrame
    {
        #region 构造函数

        public FrmModifUserInfo()
        {
            InitializeComponent();
            //改变窗体置于屏幕中间
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.LoadStarAndBloodInfo();
            ClientManager.Instance.RegisterEvent += new Action<PackageRegister>(Instance_RegisterEvent);
        }

        #endregion

        #region 方法
        
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <returns>用户信息是否满足要求；满足：true；不满足：false</returns>
        private bool ValidateUserInfo()
        {
            //验证
            if (!(this.oldpasswordtextBox1.Text.Trim() == AppContext.LoginUser.LoginPwd))
            {
                MessageBox.Show("原密码输入错误！请重新输入！");
                return false;
            }
            if (this.txtName.Text.Trim().Length < 2 || this.txtPwd.Text.Trim().Length > 28)
            {
                MessageBox.Show("请填写真实的姓名，这个很重要，不然，无法添加联系人。");
                return false;
            }
            if (this.txtPwd.Text.Trim().Length < 6 || this.txtPwd.Text.Trim().Length > 18)
            {
                MessageBox.Show("密码不符合要求，必须在6-18个字符之间。");
                return false;
            }
            if (this.txtPwd.Text.Trim() != this.txtConfirmPwd.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致！请核对后再进行提交。");
                return false;
            }
            if(!System.Text.RegularExpressions.Regex.IsMatch(this.EmailtextBox1.Text,@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")){
                MessageBox.Show("你输入的公安网邮箱不正确，请重新输入。");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(this.MobilePhonetextBox2.Text, @"^[1]+[3,5]+\d{9}$"))
            {
                MessageBox.Show("你输入的手机号码不正确，请重新输入。");
                return false;
            }
            if (this.UNITtextBox1.Text=="")
            {
                MessageBox.Show("省、市、区县名和具体单位必须填写。请先后选择。");
                return false;
            }
            if (FaceImagepictureBox2.Image==null)
            {
                MessageBox.Show("没有头像,请加载头像！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 加载信息
        /// </summary>
        private void LoadStarAndBloodInfo()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = AppContext.db.ReturnDataSet("select a_Code,a_Name from address where a_Grade=1 ");
            dt = ds.Tables[0];
            ProvincecomboBox1.DataSource = dt;
            ProvincecomboBox1.DisplayMember = "a_Name";
            ProvincecomboBox1.ValueMember = "a_Code";
            ProvincecomboBox1.SelectedIndex = -1;
            CitycomboBox2.DataSource = null;
            CountrycomboBox3.DataSource = null;

            if (AppContext.LoginUser.Gender == "男") { this.rdoMale.Checked = true; } else { this.rdoMale.Checked = false; };
            this.txtPwd.Text = AppContext.LoginUser.LoginPwd;
            this.txtConfirmPwd.Text = AppContext.LoginUser.LoginPwd;
            this.txtName.Text = AppContext.LoginUser.Name;
            // StarId = Convert.ToInt32(this.cboStar.SelectedValue),
            // BloodTypeId = Convert.ToInt32(this.cboBlood.SelectedValue)

            this.ProvincecomboBox1.Text = AppContext.LoginUser.Province;
            this.CitycomboBox2.Text = AppContext.LoginUser.City;
            this.CountrycomboBox3.Text = AppContext.LoginUser.Country;
            UNITtextBox1.Text = AppContext.LoginUser.Unit;
            EmailtextBox1.Text = AppContext.LoginUser.Email;
            this.MobilePhonetextBox2.Text = AppContext.LoginUser.MobilePhone;
            ShortPhonetextBox3.Text = AppContext.LoginUser.ShortPhone;
            this.EmailusernametextBox4.Text = AppContext.LoginUser.Emailusername;
            this.EmailpasswordtextBox5.Text = AppContext.LoginUser.Emailpassword;
           // MessageBox.Show((AppContext.LoginUser.FaceImage.Length.ToString()));
            try
            {
               // this.FaceImagepictureBox2.Image = Functions.byteArrayToImage(AppContext.LoginUser.FaceImage);
                FaceImagepictureBox2.Image = new Bitmap(Functions.byteArrayToImage(AppContext.LoginUser.FaceImage));
               // pbitmap.MakeTransparent(Color.Black);
               // FaceImagepictureBox2.Image = pbitmap;
                //this.FaceImagepictureBox2.Image = Functions.MakeTransparentGif(Functions.byteArrayToImage(AppContext.LoginUser.FaceImage), Color.Black);
               // FaceImagepictureBox2.Image.
                //FaceImagepictureBox2.Image.
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }

        }

        #endregion

        #region 事件
        
        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 用户信息修改
        /// </summary>
        private void btn_Register_Click(object sender, EventArgs e)
        {
            if (this.ValidateUserInfo())
            {
                //封装一个修改包发送给服务端进行修改
                PackageRegister packageRegister = new PackageRegister();
                packageRegister.User = new Users()
                {                
                    Id=AppContext.LoginUser.Id,
                    Gender = this.rdoMale.Checked ? "男" : "女",
                    LoginPwd = this.txtPwd.Text.Trim(),
                    Name = this.txtName.Text.Trim(),
                    FriendShipPolicyId = 1,
                    NickName="",
                    FaceKey="",
                    Age=20,
                    StarId = 1,
                    BloodTypeId = 1,
                    Vip = 0,
                    Status = 5,
                    Autograph = "",
                    Province=this.ProvincecomboBox1.Text,
                    City=this.CitycomboBox2.Text,
                    Country=this.CountrycomboBox3.Text,
                    Unit=UNITtextBox1.Text,
                    Email=EmailtextBox1.Text,
                    MobilePhone=this.MobilePhonetextBox2.Text,
                    ShortPhone=ShortPhonetextBox3.Text,
                    Emailusername=this.EmailusernametextBox4.Text,
                    Emailpassword = this.EmailpasswordtextBox5.Text,
                };
                packageRegister.User.FaceImage = Functions.imageToByteArray(FaceImagepictureBox2.Image);         
                packageRegister.OperateTypeName = "Modif";
                ClientManager.Instance.ClientSendData(packageRegister);
            }
        }

        /// <summary>
        /// 在年龄文本框中只能输入数字
        /// </summary>
        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != 13 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region 自定义事件处理

        /// <summary>
        /// 注册后的处理
        /// </summary>
        void Instance_RegisterEvent(PackageRegister obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (obj.OperateTypeName == "Modif")
                    {
                        if (obj.ReturnLows < 1)
                        {
                            MessageBox.Show("由于不可知的原因，您的资料修改没有成功！\r\n请稍后再试！", "修改失败");
                        }
                        else
                        {
                            AppContext.LoginUser = obj.User;
                            string msg = "修改成功！请记住您的新密码及其它资料！";
                            MessageBox.Show(msg, "修改成功");
                            btn_Close_Click(null, null);
                        }
                    }
                }));
            }
        }

        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.gupDetailInfo.Visible = this.checkBox1.Checked;
        }

        private void btn_Register_Load(object sender, EventArgs e)
        {

        }

        private void ProvincecomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CitycomboBox2.DataSource = AppContext.db.ReturnDataSet("select a_Code,a_Name from address where a_Grade=2 and a_ParentCode='"+this.ProvincecomboBox1.SelectedValue+"'").Tables[0];
            CitycomboBox2.DisplayMember = "a_Name";
            CitycomboBox2.ValueMember = "a_Code";
            CountrycomboBox3.SelectedIndex = -1;
        }

        private void CitycomboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CountrycomboBox3.DataSource = AppContext.db.ReturnDataSet("select a_Code,a_Name from address where a_Grade=3 and a_ParentCode='" + this.CitycomboBox2.SelectedValue + "'").Tables[0];
            CountrycomboBox3.DisplayMember = "a_Name";
            CountrycomboBox3.ValueMember = "a_Code";
        }

        private void UnitcomboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void myButton1_Load(object sender, EventArgs e)
        {

        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FaceImagepictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
                if (FaceImagepictureBox2.Image.Height > 100 || FaceImagepictureBox2.Image.Width > 100)
                {
                    ImageClass imgclss = new ImageClass(FaceImagepictureBox2.Image);
                    FaceImagepictureBox2.Image = imgclss.GetReducedImage(100, 100);
                }
            }

        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            List<Font> fontArray = new List<Font>();
            fontArray.Add(new Font("Ariel ", 8, FontStyle.Bold));
            fontArray.Add(new Font("Courier ", 8, FontStyle.Italic));
            fontArray.Add(new Font("Veranda ", 8, FontStyle.Bold));
            Graphics g = e.Graphics;
            Rectangle r = e.Bounds;
            Size imageSize = imageList1.ImageSize;
            Font fn = null;
            if (e.Index >= 0)
            {
                fn = (Font)fontArray[0];
                string s = (string)comboBox1.Items[e.Index];
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                if (e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect))
                {
                    //画条目背景 
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), r);
                    //绘制图像 
                    imageList1.Draw(e.Graphics, r.Left, r.Top, e.Index);
                    //显示字符串 
                    e.Graphics.DrawString(s, fn, new SolidBrush(Color.Black), r.Left + imageSize.Width, r.Top);
                    //显示取得焦点时的虚线框 
                    e.DrawFocusRectangle();
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), r);
                    imageList1.Draw(e.Graphics, r.Left, r.Top, e.Index);
                    e.Graphics.DrawString(s, fn, new SolidBrush(Color.Black), r.Left + imageSize.Width, r.Top);
                    e.DrawFocusRectangle();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FaceImagepictureBox2.Image = this.imageList1.Images[comboBox1.SelectedIndex];
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
