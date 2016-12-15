namespace ClientView
{
    partial class FrmSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearch));
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblTitleHead = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tabGroup = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gendergroupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoFemale = new System.Windows.Forms.RadioButton();
            this.rdoMale = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.UNIcheckBox9 = new System.Windows.Forms.CheckBox();
            this.CountrycheckBox8 = new System.Windows.Forms.CheckBox();
            this.CitycheckBox7 = new System.Windows.Forms.CheckBox();
            this.ProvincecheckBox6 = new System.Windows.Forms.CheckBox();
            this.UNITtextBox1 = new System.Windows.Forms.TextBox();
            this.UnitcomboBox4 = new System.Windows.Forms.ComboBox();
            this.CountrycomboBox3 = new System.Windows.Forms.ComboBox();
            this.CitycomboBox2 = new System.Windows.Forms.ComboBox();
            this.ProvincecomboBox1 = new System.Windows.Forms.ComboBox();
            this.MobilePhonecheckBox5 = new System.Windows.Forms.CheckBox();
            this.MobilePhonetextBox2 = new System.Windows.Forms.TextBox();
            this.EmailcheckBox4 = new System.Windows.Forms.CheckBox();
            this.EmailtextBox1 = new System.Windows.Forms.TextBox();
            this.GendercheckBox3 = new System.Windows.Forms.CheckBox();
            this.NamecheckBox2 = new System.Windows.Forms.CheckBox();
            this.Nametextbox = new System.Windows.Forms.TextBox();
            this.IdcheckBox1 = new System.Windows.Forms.CheckBox();
            this.IdtxtQQNumber = new System.Windows.Forms.TextBox();
            this.picSep_Line = new System.Windows.Forms.PictureBox();
            this.lblSreachMethod = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.btn_Cancel = new QQControls.MyButton();
            this.btn_Sreach = new QQControls.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.tabGroup.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gendergroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSep_Line)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.Location = new System.Drawing.Point(12, 7);
            this.picIcon.Margin = new System.Windows.Forms.Padding(4);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(21, 21);
            this.picIcon.TabIndex = 6;
            this.picIcon.TabStop = false;
            // 
            // lblTitleHead
            // 
            this.lblTitleHead.AutoSize = true;
            this.lblTitleHead.BackColor = System.Drawing.Color.Transparent;
            this.lblTitleHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleHead.Location = new System.Drawing.Point(37, 7);
            this.lblTitleHead.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleHead.Name = "lblTitleHead";
            this.lblTitleHead.Size = new System.Drawing.Size(136, 24);
            this.lblTitleHead.TabIndex = 7;
            this.lblTitleHead.Text = "查找联系人/群";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Controls.Add(this.tabGroup);
            this.pnlContent.Location = new System.Drawing.Point(1, 36);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(704, 373);
            this.pnlContent.TabIndex = 8;
            // 
            // tabGroup
            // 
            this.tabGroup.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabGroup.Controls.Add(this.tabPage1);
            this.tabGroup.Controls.Add(this.tabPage2);
            this.tabGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabGroup.Location = new System.Drawing.Point(0, 0);
            this.tabGroup.Margin = new System.Windows.Forms.Padding(4);
            this.tabGroup.Name = "tabGroup";
            this.tabGroup.Padding = new System.Drawing.Point(0, 0);
            this.tabGroup.SelectedIndex = 0;
            this.tabGroup.Size = new System.Drawing.Size(704, 373);
            this.tabGroup.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.PowderBlue;
            this.tabPage1.Controls.Add(this.gendergroupBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.UNIcheckBox9);
            this.tabPage1.Controls.Add(this.CountrycheckBox8);
            this.tabPage1.Controls.Add(this.CitycheckBox7);
            this.tabPage1.Controls.Add(this.ProvincecheckBox6);
            this.tabPage1.Controls.Add(this.UNITtextBox1);
            this.tabPage1.Controls.Add(this.UnitcomboBox4);
            this.tabPage1.Controls.Add(this.CountrycomboBox3);
            this.tabPage1.Controls.Add(this.CitycomboBox2);
            this.tabPage1.Controls.Add(this.ProvincecomboBox1);
            this.tabPage1.Controls.Add(this.MobilePhonecheckBox5);
            this.tabPage1.Controls.Add(this.MobilePhonetextBox2);
            this.tabPage1.Controls.Add(this.EmailcheckBox4);
            this.tabPage1.Controls.Add(this.EmailtextBox1);
            this.tabPage1.Controls.Add(this.GendercheckBox3);
            this.tabPage1.Controls.Add(this.NamecheckBox2);
            this.tabPage1.Controls.Add(this.Nametextbox);
            this.tabPage1.Controls.Add(this.IdcheckBox1);
            this.tabPage1.Controls.Add(this.IdtxtQQNumber);
            this.tabPage1.Controls.Add(this.picSep_Line);
            this.tabPage1.Controls.Add(this.lblSreachMethod);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(696, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "查找联系人";
            // 
            // gendergroupBox1
            // 
            this.gendergroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.gendergroupBox1.Controls.Add(this.rdoFemale);
            this.gendergroupBox1.Controls.Add(this.rdoMale);
            this.gendergroupBox1.Location = new System.Drawing.Point(134, 210);
            this.gendergroupBox1.Name = "gendergroupBox1";
            this.gendergroupBox1.Size = new System.Drawing.Size(164, 44);
            this.gendergroupBox1.TabIndex = 44;
            this.gendergroupBox1.TabStop = false;
            // 
            // rdoFemale
            // 
            this.rdoFemale.AutoSize = true;
            this.rdoFemale.BackColor = System.Drawing.Color.Transparent;
            this.rdoFemale.Location = new System.Drawing.Point(86, 16);
            this.rdoFemale.Margin = new System.Windows.Forms.Padding(4);
            this.rdoFemale.Name = "rdoFemale";
            this.rdoFemale.Size = new System.Drawing.Size(45, 20);
            this.rdoFemale.TabIndex = 26;
            this.rdoFemale.TabStop = true;
            this.rdoFemale.Text = "女";
            this.rdoFemale.UseVisualStyleBackColor = false;
            // 
            // rdoMale
            // 
            this.rdoMale.AutoSize = true;
            this.rdoMale.BackColor = System.Drawing.Color.Transparent;
            this.rdoMale.Checked = true;
            this.rdoMale.Location = new System.Drawing.Point(25, 17);
            this.rdoMale.Margin = new System.Windows.Forms.Padding(4);
            this.rdoMale.Name = "rdoMale";
            this.rdoMale.Size = new System.Drawing.Size(45, 20);
            this.rdoMale.TabIndex = 25;
            this.rdoMale.TabStop = true;
            this.rdoMale.Text = "男";
            this.rdoMale.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(34, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(568, 16);
            this.label1.TabIndex = 43;
            this.label1.Text = "小提示:在对应的条件前打勾才查找该项目,不打勾的条件项目不列入查找范围。";
            // 
            // UNIcheckBox9
            // 
            this.UNIcheckBox9.AutoSize = true;
            this.UNIcheckBox9.BackColor = System.Drawing.Color.Transparent;
            this.UNIcheckBox9.Location = new System.Drawing.Point(368, 184);
            this.UNIcheckBox9.Name = "UNIcheckBox9";
            this.UNIcheckBox9.Size = new System.Drawing.Size(94, 20);
            this.UNIcheckBox9.TabIndex = 42;
            this.UNIcheckBox9.Text = "具体部门";
            this.UNIcheckBox9.UseVisualStyleBackColor = false;
            // 
            // CountrycheckBox8
            // 
            this.CountrycheckBox8.AutoSize = true;
            this.CountrycheckBox8.BackColor = System.Drawing.Color.Transparent;
            this.CountrycheckBox8.Location = new System.Drawing.Point(368, 144);
            this.CountrycheckBox8.Name = "CountrycheckBox8";
            this.CountrycheckBox8.Size = new System.Drawing.Size(94, 20);
            this.CountrycheckBox8.TabIndex = 41;
            this.CountrycheckBox8.Text = "县（区）";
            this.CountrycheckBox8.UseVisualStyleBackColor = false;
            // 
            // CitycheckBox7
            // 
            this.CitycheckBox7.AutoSize = true;
            this.CitycheckBox7.BackColor = System.Drawing.Color.Transparent;
            this.CitycheckBox7.Location = new System.Drawing.Point(368, 106);
            this.CitycheckBox7.Name = "CitycheckBox7";
            this.CitycheckBox7.Size = new System.Drawing.Size(46, 20);
            this.CitycheckBox7.TabIndex = 40;
            this.CitycheckBox7.Text = "市";
            this.CitycheckBox7.UseVisualStyleBackColor = false;
            // 
            // ProvincecheckBox6
            // 
            this.ProvincecheckBox6.AutoSize = true;
            this.ProvincecheckBox6.BackColor = System.Drawing.Color.Transparent;
            this.ProvincecheckBox6.Location = new System.Drawing.Point(368, 68);
            this.ProvincecheckBox6.Name = "ProvincecheckBox6";
            this.ProvincecheckBox6.Size = new System.Drawing.Size(46, 20);
            this.ProvincecheckBox6.TabIndex = 39;
            this.ProvincecheckBox6.Text = "省";
            this.ProvincecheckBox6.UseVisualStyleBackColor = false;
            // 
            // UNITtextBox1
            // 
            this.UNITtextBox1.Location = new System.Drawing.Point(461, 178);
            this.UNITtextBox1.Name = "UNITtextBox1";
            this.UNITtextBox1.Size = new System.Drawing.Size(85, 26);
            this.UNITtextBox1.TabIndex = 38;
            this.UNITtextBox1.Visible = false;
            // 
            // UnitcomboBox4
            // 
            this.UnitcomboBox4.FormattingEnabled = true;
            this.UnitcomboBox4.Items.AddRange(new object[] {
            "法制大队",
            "派出所",
            "刑侦大队",
            "治安大队",
            "经侦大队",
            "国保大队",
            "交警大队",
            "特警大队",
            "禁毒大队",
            "外事科",
            "户政科",
            "后勤科",
            "政治处",
            "看守所",
            "网警（科技科）",
            "机关党委",
            "办公室",
            "局领导",
            "高速交警",
            "纪委督察"});
            this.UnitcomboBox4.Location = new System.Drawing.Point(547, 179);
            this.UnitcomboBox4.Name = "UnitcomboBox4";
            this.UnitcomboBox4.Size = new System.Drawing.Size(92, 24);
            this.UnitcomboBox4.TabIndex = 36;
            // 
            // CountrycomboBox3
            // 
            this.CountrycomboBox3.FormattingEnabled = true;
            this.CountrycomboBox3.Location = new System.Drawing.Point(461, 140);
            this.CountrycomboBox3.Name = "CountrycomboBox3";
            this.CountrycomboBox3.Size = new System.Drawing.Size(176, 24);
            this.CountrycomboBox3.TabIndex = 34;
            // 
            // CitycomboBox2
            // 
            this.CitycomboBox2.FormattingEnabled = true;
            this.CitycomboBox2.Location = new System.Drawing.Point(461, 102);
            this.CitycomboBox2.Name = "CitycomboBox2";
            this.CitycomboBox2.Size = new System.Drawing.Size(176, 24);
            this.CitycomboBox2.TabIndex = 32;
            this.CitycomboBox2.SelectedIndexChanged += new System.EventHandler(this.CitycomboBox2_SelectedIndexChanged);
            // 
            // ProvincecomboBox1
            // 
            this.ProvincecomboBox1.FormattingEnabled = true;
            this.ProvincecomboBox1.Location = new System.Drawing.Point(461, 66);
            this.ProvincecomboBox1.Name = "ProvincecomboBox1";
            this.ProvincecomboBox1.Size = new System.Drawing.Size(176, 24);
            this.ProvincecomboBox1.TabIndex = 30;
            this.ProvincecomboBox1.SelectedIndexChanged += new System.EventHandler(this.ProvincecomboBox1_SelectedIndexChanged);
            // 
            // MobilePhonecheckBox5
            // 
            this.MobilePhonecheckBox5.AutoSize = true;
            this.MobilePhonecheckBox5.BackColor = System.Drawing.Color.Transparent;
            this.MobilePhonecheckBox5.Location = new System.Drawing.Point(39, 184);
            this.MobilePhonecheckBox5.Name = "MobilePhonecheckBox5";
            this.MobilePhonecheckBox5.Size = new System.Drawing.Size(94, 20);
            this.MobilePhonecheckBox5.TabIndex = 29;
            this.MobilePhonecheckBox5.Text = "手机长号";
            this.MobilePhonecheckBox5.UseVisualStyleBackColor = false;
            // 
            // MobilePhonetextBox2
            // 
            this.MobilePhonetextBox2.Location = new System.Drawing.Point(134, 180);
            this.MobilePhonetextBox2.Name = "MobilePhonetextBox2";
            this.MobilePhonetextBox2.Size = new System.Drawing.Size(166, 26);
            this.MobilePhonetextBox2.TabIndex = 28;
            // 
            // EmailcheckBox4
            // 
            this.EmailcheckBox4.AutoSize = true;
            this.EmailcheckBox4.BackColor = System.Drawing.Color.Transparent;
            this.EmailcheckBox4.Location = new System.Drawing.Point(39, 146);
            this.EmailcheckBox4.Name = "EmailcheckBox4";
            this.EmailcheckBox4.Size = new System.Drawing.Size(94, 20);
            this.EmailcheckBox4.TabIndex = 27;
            this.EmailcheckBox4.Text = "内网邮箱";
            this.EmailcheckBox4.UseVisualStyleBackColor = false;
            // 
            // EmailtextBox1
            // 
            this.EmailtextBox1.Location = new System.Drawing.Point(134, 144);
            this.EmailtextBox1.Name = "EmailtextBox1";
            this.EmailtextBox1.Size = new System.Drawing.Size(167, 26);
            this.EmailtextBox1.TabIndex = 26;
            // 
            // GendercheckBox3
            // 
            this.GendercheckBox3.AutoSize = true;
            this.GendercheckBox3.BackColor = System.Drawing.Color.Transparent;
            this.GendercheckBox3.Location = new System.Drawing.Point(40, 227);
            this.GendercheckBox3.Name = "GendercheckBox3";
            this.GendercheckBox3.Size = new System.Drawing.Size(62, 20);
            this.GendercheckBox3.TabIndex = 25;
            this.GendercheckBox3.Text = "性别";
            this.GendercheckBox3.UseVisualStyleBackColor = false;
            // 
            // NamecheckBox2
            // 
            this.NamecheckBox2.AutoSize = true;
            this.NamecheckBox2.BackColor = System.Drawing.Color.Transparent;
            this.NamecheckBox2.Location = new System.Drawing.Point(39, 105);
            this.NamecheckBox2.Name = "NamecheckBox2";
            this.NamecheckBox2.Size = new System.Drawing.Size(94, 20);
            this.NamecheckBox2.TabIndex = 12;
            this.NamecheckBox2.Text = "真实姓名";
            this.NamecheckBox2.UseVisualStyleBackColor = false;
            // 
            // Nametextbox
            // 
            this.Nametextbox.Location = new System.Drawing.Point(134, 102);
            this.Nametextbox.Margin = new System.Windows.Forms.Padding(4);
            this.Nametextbox.MaxLength = 20;
            this.Nametextbox.Name = "Nametextbox";
            this.Nametextbox.Size = new System.Drawing.Size(164, 26);
            this.Nametextbox.TabIndex = 11;
            // 
            // IdcheckBox1
            // 
            this.IdcheckBox1.AutoSize = true;
            this.IdcheckBox1.BackColor = System.Drawing.Color.Transparent;
            this.IdcheckBox1.Checked = true;
            this.IdcheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IdcheckBox1.Location = new System.Drawing.Point(40, 66);
            this.IdcheckBox1.Name = "IdcheckBox1";
            this.IdcheckBox1.Size = new System.Drawing.Size(62, 20);
            this.IdcheckBox1.TabIndex = 4;
            this.IdcheckBox1.Text = "工号";
            this.IdcheckBox1.UseVisualStyleBackColor = false;
            // 
            // IdtxtQQNumber
            // 
            this.IdtxtQQNumber.ForeColor = System.Drawing.Color.Black;
            this.IdtxtQQNumber.Location = new System.Drawing.Point(134, 64);
            this.IdtxtQQNumber.Margin = new System.Windows.Forms.Padding(4);
            this.IdtxtQQNumber.Name = "IdtxtQQNumber";
            this.IdtxtQQNumber.Size = new System.Drawing.Size(164, 26);
            this.IdtxtQQNumber.TabIndex = 8;
            // 
            // picSep_Line
            // 
            this.picSep_Line.Image = ((System.Drawing.Image)(resources.GetObject("picSep_Line.Image")));
            this.picSep_Line.Location = new System.Drawing.Point(97, 13);
            this.picSep_Line.Margin = new System.Windows.Forms.Padding(4);
            this.picSep_Line.Name = "picSep_Line";
            this.picSep_Line.Size = new System.Drawing.Size(255, 1);
            this.picSep_Line.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSep_Line.TabIndex = 1;
            this.picSep_Line.TabStop = false;
            // 
            // lblSreachMethod
            // 
            this.lblSreachMethod.AutoSize = true;
            this.lblSreachMethod.Location = new System.Drawing.Point(14, 7);
            this.lblSreachMethod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSreachMethod.Name = "lblSreachMethod";
            this.lblSreachMethod.Size = new System.Drawing.Size(72, 16);
            this.lblSreachMethod.TabIndex = 0;
            this.lblSreachMethod.Text = "查找条件";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.PowderBlue;
            this.tabPage2.Controls.Add(this.txtGroupName);
            this.tabPage2.Controls.Add(this.lblGroupName);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(696, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "查找群";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(61, 84);
            this.txtGroupName.Margin = new System.Windows.Forms.Padding(4);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(371, 26);
            this.txtGroupName.TabIndex = 1;
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(61, 59);
            this.lblGroupName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(88, 16);
            this.lblGroupName.TabIndex = 0;
            this.lblGroupName.Text = "群组名称：";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.BackgroundImage")));
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Cancel.BtnType = QQControls.MyButton.ButtonType.Normal;
            this.btn_Cancel.Content = "取消";
            this.btn_Cancel.Down = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Down")));
            this.btn_Cancel.Location = new System.Drawing.Point(607, 421);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Cancel.MoveBg = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.MoveBg")));
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Normal = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Normal")));
            this.btn_Cancel.Size = new System.Drawing.Size(93, 29);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Sreach
            // 
            this.btn_Sreach.BackColor = System.Drawing.Color.Transparent;
            this.btn_Sreach.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Sreach.BackgroundImage")));
            this.btn_Sreach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Sreach.BtnType = QQControls.MyButton.ButtonType.Normal;
            this.btn_Sreach.Content = "查找";
            this.btn_Sreach.Down = ((System.Drawing.Image)(resources.GetObject("btn_Sreach.Down")));
            this.btn_Sreach.Location = new System.Drawing.Point(505, 423);
            this.btn_Sreach.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Sreach.MoveBg = ((System.Drawing.Image)(resources.GetObject("btn_Sreach.MoveBg")));
            this.btn_Sreach.Name = "btn_Sreach";
            this.btn_Sreach.Normal = ((System.Drawing.Image)(resources.GetObject("btn_Sreach.Normal")));
            this.btn_Sreach.Size = new System.Drawing.Size(93, 29);
            this.btn_Sreach.TabIndex = 9;
            this.btn_Sreach.Load += new System.EventHandler(this.btn_Sreach_Load_1);
            this.btn_Sreach.Click += new System.EventHandler(this.btn_Sreach_Click);
            // 
            // FrmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 467);
            this.Controls.Add(this.btn_Sreach);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.lblTitleHead);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.pnlContent);
            this.FormHeight = 467;
            this.FormWidth = 707;
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmSearch";
            this.SystemButton = QQControls.MainFrame.sysButton.minClose;
            this.Text = "查找联系人/群";
            this.TitleVisible = true;
            this.Controls.SetChildIndex(this.pnlContent, 0);
            this.Controls.SetChildIndex(this.picIcon, 0);
            this.Controls.SetChildIndex(this.lblTitleHead, 0);
            this.Controls.SetChildIndex(this.btn_Cancel, 0);
            this.Controls.SetChildIndex(this.btn_Sreach, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.tabGroup.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gendergroupBox1.ResumeLayout(false);
            this.gendergroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSep_Line)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblTitleHead;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TabControl tabGroup;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private QQControls.MyButton btn_Cancel;
        private QQControls.MyButton btn_Sreach;
        private System.Windows.Forms.PictureBox picSep_Line;
        private System.Windows.Forms.Label lblSreachMethod;
        private System.Windows.Forms.TextBox IdtxtQQNumber;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.CheckBox IdcheckBox1;
        private System.Windows.Forms.CheckBox NamecheckBox2;
        private System.Windows.Forms.TextBox Nametextbox;
        private System.Windows.Forms.CheckBox MobilePhonecheckBox5;
        private System.Windows.Forms.TextBox MobilePhonetextBox2;
        private System.Windows.Forms.CheckBox EmailcheckBox4;
        private System.Windows.Forms.TextBox EmailtextBox1;
        private System.Windows.Forms.CheckBox GendercheckBox3;
        private System.Windows.Forms.CheckBox UNIcheckBox9;
        private System.Windows.Forms.CheckBox CountrycheckBox8;
        private System.Windows.Forms.CheckBox CitycheckBox7;
        private System.Windows.Forms.CheckBox ProvincecheckBox6;
        private System.Windows.Forms.TextBox UNITtextBox1;
        private System.Windows.Forms.ComboBox UnitcomboBox4;
        private System.Windows.Forms.ComboBox CountrycomboBox3;
        private System.Windows.Forms.ComboBox CitycomboBox2;
        private System.Windows.Forms.ComboBox ProvincecomboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gendergroupBox1;
        private System.Windows.Forms.RadioButton rdoFemale;
        private System.Windows.Forms.RadioButton rdoMale;
    }
}