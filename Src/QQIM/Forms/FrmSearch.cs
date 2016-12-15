using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQControls;
using Common;
using Entity;

namespace ClientView
{
    /// <summary>
    /// 查找好友、群组界面
    /// </summary>
    public partial class FrmSearch : MainFrame
    {
        #region 构造函数

        public FrmSearch()
        {
            InitializeComponent();
            //改变窗体置于屏幕中间
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
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
            ClientManager.Instance.SearchUserEvent += new Action<PackageSearchUser>(Instance_SearchUserEvent);
        }

        #endregion

        #region 事件
        
        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查找指定的好友
        /// </summary>
        private void btn_Sreach_Click(object sender, EventArgs e)
        {           
            string sql = "select top 30 * from [Users] where 1=1 ";
            if(IdcheckBox1.Checked&&(!string.IsNullOrEmpty(IdtxtQQNumber.Text.Trim())))
                sql+=" and Id="+IdtxtQQNumber.Text.Trim();
            if(this.NamecheckBox2.Checked&&(!string.IsNullOrEmpty(this.Nametextbox.Text.Trim())))
                sql+=" and Name like '%"+Nametextbox.Text.Trim()+"%'";
            if(this.EmailcheckBox4.Checked&&(!string.IsNullOrEmpty(this.EmailtextBox1.Text.Trim())))
                sql+=" and email='"+EmailtextBox1.Text.Trim()+"'";
            if(this.MobilePhonecheckBox5.Checked&&(!string.IsNullOrEmpty(this.MobilePhonetextBox2.Text.Trim())))
                sql+=" and MobilePhone='"+MobilePhonetextBox2.Text.Trim()+"'";
                //根据性别列表框中的索引值增加条件
            if(this.GendercheckBox3.Checked)
            {
                if(rdoFemale.Checked)
                        sql += "and Gender = '女'";
                else
                        sql += "and Gender = '男'";
            }
            if(this.ProvincecheckBox6.Checked&&(!string.IsNullOrEmpty(this.ProvincecomboBox1.Text.Trim())))
                sql += " and Province like '%" + ProvincecomboBox1.Text.Trim() + "%'";
            if (this.CitycheckBox7.Checked && (!string.IsNullOrEmpty(this.CitycomboBox2.Text.Trim())))
                sql += " and city like '%" + CitycomboBox2.Text.Trim() + "%'";
            if (this.CountrycheckBox8.Checked && (!string.IsNullOrEmpty(this.CountrycomboBox3.Text.Trim())))
                sql += " and Country like '%" + CountrycomboBox3.Text.Trim() + "%'";
            if (this.UNIcheckBox9.Checked && (!string.IsNullOrEmpty(this.UnitcomboBox4.Text.Trim())))
                sql += " and Unit like '%" +UNITtextBox1.Text+UnitcomboBox4.Text.Trim() + "%'";
            //MessageBox.Show(sql);

            PackageSearchUser packageSearchUser = new PackageSearchUser();
            packageSearchUser.SearchSQLString = sql;
            ClientManager.Instance.ClientSendData(packageSearchUser);
            
        }

        /// <summary>
        /// 每当单选按钮的Check属性发生变化时，隐藏或显示相应的面板
        /// </summary>
        private void rdoSearch_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.rdoAccurateSearch.Checked)
            //{
            //    this.pnlAccurate.Visible = true;
            //    this.pnlCondition.Visible = false;
            //}
            //else
            //{
            //    this.pnlAccurate.Visible = false;
            //    this.pnlCondition.Visible = true;
            //}
        }

        #endregion

        #region 自定义事件的处理
        
        /// <summary>
        /// 显示查询到的用户
        /// </summary>
        /// <param name="obj">查找好友包</param>
        void Instance_SearchUserEvent(PackageSearchUser obj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                    {
                        //打开显示查询到用户的窗体
                        AppContext.CreateOrActivate(typeof(FrmSearchFriend), true, obj);
                    }));
            }
        }

        #endregion

        private void btn_Sreach_Load(object sender, EventArgs e)
        {

        }

        private void ProvincecomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CitycomboBox2.DataSource = AppContext.db.ReturnDataSet("select a_Code,a_Name from address where a_Grade=2 and a_ParentCode='" + this.ProvincecomboBox1.SelectedValue + "'").Tables[0];
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

        private void btn_Sreach_Load_1(object sender, EventArgs e)
        {

        }
    }
}
