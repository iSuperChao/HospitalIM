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
    public partial class FrmViewUserInfo : RegisterFrame
    {
        #region 构造函数
        public Users user;

        public FrmViewUserInfo()
        {
           
            InitializeComponent();
            //改变窗体置于屏幕中间
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            //InitializeComponent();
            ClientManager.Instance.RegisterEvent += new Action<PackageRegister>(Instance_RegisterEvent);
        }

        public void loaddata()
        {
            try
            {
                this.LoadStarAndBloodInfo();
            }
            catch { }
        }

        #endregion

        #region 方法
        
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <returns>用户信息是否满足要求；满足：true；不满足：false</returns>
        private bool ValidateUserInfo()
        {
            ////验证
            //if (!(this.oldpasswordtextBox1.Text.Trim() == user.LoginPwd))
            //{
            //    MessageBox.Show("原密码输入错误！请重新输入！");
            //    return false;
            //}
            //if (this.txtName.Text.Trim().Length < 2 || this.txtPwd.Text.Trim().Length > 28)
            //{
            //    MessageBox.Show("请填写真实的姓名，这个很重要，不然，无法添加联系人。");
            //    return false;
            //}
            //if (this.txtPwd.Text.Trim().Length < 6 || this.txtPwd.Text.Trim().Length > 18)
            //{
            //    MessageBox.Show("密码不符合要求，必须在6-18个字符之间。");
            //    return false;
            //}
            //if (this.txtPwd.Text.Trim() != this.txtConfirmPwd.Text.Trim())
            //{
            //    MessageBox.Show("两次密码输入不一致！请核对后再进行提交。");
            //    return false;
            //}
            //if(!System.Text.RegularExpressions.Regex.IsMatch(this.EmailtextBox1.Text,@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")){
            //    MessageBox.Show("你输入的公安网邮箱不正确，请重新输入。");
            //    return false;
            //}
            //if (!System.Text.RegularExpressions.Regex.IsMatch(this.MobilePhonetextBox2.Text, @"^[1]+[3,5]+\d{9}$"))
            //{
            //    MessageBox.Show("你输入的手机号码不正确，请重新输入。");
            //    return false;
            //}
            //if (this.UNITtextBox1.Text=="")
            //{
            //    MessageBox.Show("省、市、区县名和具体单位必须填写。请先后选择。");
            //    return false;
            //}
            //if (FaceImagepictureBox2.Image==null)
            //{
            //    MessageBox.Show("没有头像,请加载头像！");
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// 加载信息
        /// </summary>
        private void LoadStarAndBloodInfo()
        {
            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //ds = AppContext.db.ReturnDataSet("select a_Code,a_Name from address where a_Grade=1 ");
            //dt = ds.Tables[0];
            //ProvincecomboBox1.DataSource = dt;
            //ProvincecomboBox1.DisplayMember = "a_Name";
            //ProvincecomboBox1.ValueMember = "a_Code";
            //ProvincecomboBox1.SelectedIndex = -1;
            //CitycomboBox2.DataSource = null;
            //CountrycomboBox3.DataSource = null;

            if (user.Gender == "男") { this.rdoMale.Checked = true; } else { this.rdoMale.Checked = false; };
            //this.txtPwd.Text = user.LoginPwd;
            //this.txtConfirmPwd.Text = user.LoginPwd;
            this.txtName.Text = user.Name;
            // StarId = Convert.ToInt32(this.cboStar.SelectedValue),
            // BloodTypeId = Convert.ToInt32(this.cboBlood.SelectedValue)

            this.ProvincecomboBox1.Text = user.Province;
            this.CitycomboBox2.Text = user.City;
            this.CountrycomboBox3.Text = user.Country;
            UNITtextBox1.Text = user.Unit;
            EmailtextBox1.Text = user.Email;
            this.MobilePhonetextBox2.Text = user.MobilePhone;
            ShortPhonetextBox3.Text = user.ShortPhone;
         //   this.EmailusernametextBox4.Text = user.Emailusername;
        //    this.EmailpasswordtextBox5.Text = user.Emailpassword;
           // MessageBox.Show((user.FaceImage.Length.ToString()));
            try
            {
               // this.FaceImagepictureBox2.Image = Functions.byteArrayToImage(user.FaceImage);
                FaceImagepictureBox2.Image = new Bitmap(Functions.byteArrayToImage(user.FaceImage));
               // pbitmap.MakeTransparent(Color.Black);
               // FaceImagepictureBox2.Image = pbitmap;
                //this.FaceImagepictureBox2.Image = Functions.MakeTransparentGif(Functions.byteArrayToImage(user.FaceImage), Color.Black);
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

        }

        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //this.gupDetailInfo.Visible = this.checkBox1.Checked;
        }

        private void btn_Register_Load(object sender, EventArgs e)
        {

        }

        private void ProvincecomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CitycomboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
