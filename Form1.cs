using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace LCApp {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ClientSize = new Size(panel1.Width, panel1.Height);//主界面显示内容的宽高

            Interface();    //界面初始化显示方法
            BtnAry();       // 四大市场子按钮组——隐藏
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(@"地址：" + path);
            ReadXmlData(path + @"\Data.xml");//加载xml文档
            //getFileName(A_List1);//市场一,90
            //getFileName(B_List1);//市场二,90
            //getFileName(C_List1);//市场三,90
            //getFileName(D_List1);//市场四,90

            //getFileName(A_List2);//市场一,80
            //getFileName(B_List2);//市场二,80
            //getFileName(C_List2);//市场三,80
            //getFileName(D_List2);//市场四,80

            //getFileName(A_List3);//市场一,70
            //getFileName(B_List3);//市场二,70
            //getFileName(C_List3);//市场三,70
            //getFileName(D_List3);//市场四,70
        }

        public bool Upload = true;//上传 true，更新false
        public string PersonId;                             //输入的 id
        public string PersonName;                  //输入的 姓名
        public string PersonLevel;                        //传入的 级别
        public string PersonSrcImg;                //传入的 缩略图存放 路径
        public string PersonSrcInfo;                //传入的 简介图存放 路径
        public string PersonSrcPhoto;             //传入的 生活照存放 路径
        public string PersonSrcImgPath;         //传入的 缩略图文件绝对路径
        public string PersonSrcInfoPath;         //传入的 简介图文件绝对路径
        public List<string> PersonSrcPhotoPathList = new List<string>();//传入的生活照文件的路径

        public List<List<string>> PhotoLists = new List<List<string>>();//暂存点击表格后加载的生活照文件路径数据
        public string[] LevelName = new[] { "首席业务总监", "高级业务总监", "业务总监" };
        public string[] CategoryName = new[] { "刘莉 市场", "来傅莲/杨辉 市场", "彭焰 市场", "韩振铎/王春洁 市场" };
        /// <summary>
        /// 级别：img90 = 首席业务总监，img80 = 高级业务总监，img70 = 业务总监
        /// </summary>
        public enum Levels {
            Img90 = 0,
            Img80 = 1,
            Img70 = 2
        }

        /// <summary>
        /// 市场分类：arr1 = 刘莉 市场，arr2 = 来傅莲/杨辉 市场，arr3 = 彭焰 市场，arr4 = 韩振铎/王春洁 市场
        /// </summary>
        public enum Category {
            Arr1 = 0,
            Arr2 = 1,
            Arr3 = 2,
            Arr4 = 3
        }

        /// <summary>
        /// 定义存储文件夹地址的泛型数组,P~视频；Str_TMP_1~市场一；Str_TMP_2~市场二；Str_TMP_3~市场三；Str_TMP_4~市场四
        /// </summary>
        public List<string> PList = new List<string>();//视频

        private List<string> Str_TMP_1 = new List<string>();//市场一
        private List<string> Str_TMP_2 = new List<string>();//市场二
        private List<string> Str_TMP_3 = new List<string>();//市场三
        private List<string> Str_TMP_4 = new List<string>();//市场四


        public List<string> AList1 = new List<string>();//市场一,90
        public List<string> AList2 = new List<string>();//市场一,80
        public List<string> AList3 = new List<string>();//市场一,70

        public List<string> BList1 = new List<string>();//市场二,90
        public List<string> BList2 = new List<string>();//市场二,80
        public List<string> BList3 = new List<string>();//市场二,70

        public List<string> CList1 = new List<string>();//市场三,90
        public List<string> CList2 = new List<string>();//市场三,80
        public List<string> CList3 = new List<string>();//市场三,70

        public List<string> DList1 = new List<string>();//市场四,90
        public List<string> DList2 = new List<string>();//市场四,80
        public List<string> DList3 = new List<string>();//市场四,70


        List<Person> _pic11 = new List<Person>();//市场一,90 -->暂存读取的文件数据
        List<Person> _pic12 = new List<Person>();//市场一,80 -->暂存读取的文件数据
        List<Person> _pic13 = new List<Person>();//市场一,70 -->暂存读取的文件数据

        List<Person> _pic21 = new List<Person>();//市场二,90 -->暂存读取的文件数据
        List<Person> _pic22 = new List<Person>();//市场二,80 -->暂存读取的文件数据
        List<Person> _pic23 = new List<Person>();//市场二,70 -->暂存读取的文件数据

        List<Person> _pic31 = new List<Person>();//市场三,90 -->暂存读取的文件数据
        List<Person> _pic32 = new List<Person>();//市场三,80 -->暂存读取的文件数据
        List<Person> _pic33 = new List<Person>();//市场三,70 -->暂存读取的文件数据

        List<Person> _pic41 = new List<Person>();//市场四,90 -->暂存读取的文件数据
        List<Person> _pic42 = new List<Person>();//市场四,80 -->暂存读取的文件数据
        List<Person> _pic43 = new List<Person>();//市场四,70 -->暂存读取的文件数据

        private void ReadXmlData(string str) {
            XmlTextReader reader = new XmlTextReader(str);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "name")
                    {
                        PList.Add(reader.GetAttribute(0));
                    }
                    if (reader.Name == "name1")
                    {
                        Str_TMP_1.Add(reader.GetAttribute(0));
                    }
                    if (reader.Name == "name2")
                    {
                        Str_TMP_2.Add(reader.GetAttribute(0));
                    }
                    if (reader.Name == "name3")
                    {
                        Str_TMP_3.Add(reader.GetAttribute(0));
                    }
                    if (reader.Name == "name4")
                    {
                        Str_TMP_4.Add(reader.GetAttribute(0));
                    }
                }
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                }
            }

            for (int i = 0; i < 9; i++)
            {
                if (i == 0 || i == 3 || i == 6)
                {
                    AList1.Add(Str_TMP_1[i]);//市场一,90
                    BList1.Add(Str_TMP_2[i]);//市场二,90
                    CList1.Add(Str_TMP_3[i]);//市场三,90
                    DList1.Add(Str_TMP_4[i]);//市场四,90
                }
                else if (i == 1 || i == 4 || i == 7)
                {
                    AList2.Add(Str_TMP_1[i]);//市场一,80
                    BList2.Add(Str_TMP_2[i]);//市场二,80
                    CList2.Add(Str_TMP_3[i]);//市场三,80
                    DList2.Add(Str_TMP_4[i]);//市场四,80
                }
                else if (i == 2 || i == 5 || i == 8)
                {
                    AList3.Add(Str_TMP_1[i]);//市场一,70
                    BList3.Add(Str_TMP_2[i]);//市场二,70
                    CList3.Add(Str_TMP_3[i]);//市场三,70
                    DList3.Add(Str_TMP_4[i]);//市场四,70
                }
            }

        }

        public List<Person> GetFileName(List<string> list) {
            var listAry = new List<List<string>>();
            for (int i = 0; i < list.Count; i++)
            {
                var list0 = new List<string>();
                listAry.Add(list0);
            }

            for (int j = 0; j < list.Count; j++)
            {
                DirectoryInfo dir = new DirectoryInfo(list[j]);
                foreach (var item in dir.GetFiles("*"))
                {
                    if (item.Extension == ".jpg" || item.Extension == ".png")
                    {
                        listAry[j].Add(item.Name);
                        // Console.WriteLine(item.Name);
                    }
                }
            }


            List<Person> personAry = new List<Person>();
            for (int m = 0; m < listAry[0].Count; m++)
            {
                Person p = new Person();
                string[] strAry;
                string str = listAry[0][m];
                strAry = str.Split('_');
                p.ID = strAry[0]; Console.Write(@"ID:" + p.ID);
                str = strAry[1];
                strAry = str.Split(',');
                p.Name = strAry[0]; Console.Write(@"  名字:" + p.Name);
                str = strAry[1];
                strAry = str.Split('.');
                p.Level = strAry[0]; Console.WriteLine(@"  级别:" + p.Level);
                p.SrcImg = list[0]; Console.WriteLine(@"缩略图存放路径：………………………………     : " + list[0]);
                p.SrcInfo = list[1]; Console.WriteLine(@"简介图存放路径：………………………………     : " + list[1]);
                p.SrcPhoto = list[2]; Console.WriteLine(@"生活图存放路径：………………………………     : " + list[2]);
                personAry.Add(p); Console.WriteLine(@"++++++++++++");
            }

            for (int k = 0; k < listAry.Count; k++)
            {
                for (int l = 0; l < listAry[k].Count; l++)
                {
                    if (k == 0)
                    {
                        Console.WriteLine(@"+++++缩略图+++++++");
                        personAry[l].ImgName = listAry[k][l]; Console.WriteLine(@"缩略图文件名：………………………………     : " + listAry[k][l]);
                        personAry[l].SrcImgPath = personAry[l].SrcImg + listAry[k][l]; Console.WriteLine(@"缩略图文件路径: " + personAry[l].SrcImgPath);
                    }
                    else if (k == 1)
                    {
                        Console.WriteLine(@"+++++简介图+++++++");
                        personAry[l].InfoName = listAry[k][l]; Console.WriteLine(@"简介图文件名：………………………………     : " + listAry[k][l]);
                        personAry[l].SrcInfoPath = personAry[l].SrcInfo + listAry[k][l]; Console.WriteLine(@"简介图文件路径: " + personAry[l].SrcInfoPath);
                    }
                    else if (k == 2)
                    {
                        //Console.WriteLine(@"+++++生活照图+++++++");
                        for (int q = 0; q < personAry.Count; q++)
                        {
                            if (listAry[k][l].IndexOf(personAry[q].Name) >= 0)
                            {
                                personAry[q].FullPhotosName.Add(personAry[q].SrcPhoto + listAry[k][l]);
                            }
                        }
                    }
                }
            }

            for (int r = 0; r < personAry.Count; r++)
            {
                if (personAry[r].FullPhotosName.Count > 0) Console.WriteLine(@"start……" + personAry[r].Name);
                for (int w = 0; w < personAry[r].FullPhotosName.Count; w++)
                {
                    Console.WriteLine(@"生活照图：" + personAry[r].FullPhotosName[w]);
                }
                if (personAry[r].FullPhotosName.Count > 0) Console.WriteLine(@"end……");
            }

            for (int o = 0; o < personAry.Count; o++)
            {
                if (personAry[o].FullPhotosName.Count > 0)
                {
                    Console.WriteLine(@"++ ID    姓名    级别 +++++++");
                    Console.WriteLine(personAry[o].ID + @"  " + personAry[o].Name + @"  " + personAry[o].Level);
                    Console.WriteLine(@"+++++生活照列表+++++++");
                }

                for (int r = 0; r < personAry[o].FullPhotosName.Count; r++)
                {
                    Console.WriteLine(personAry[o].FullPhotosName[r]);
                }
            }
            return personAry;
        }

        /// <summary>
        /// 输出显示到表格
        /// </summary>
        /// <param name="p">Person Array</param>
        public void OutPutGridView(List<Person> p) {
            dataGridView1.DataSource = p;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Name"].Visible = false;
            dataGridView1.Columns["Level"].Visible = false;
            dataGridView1.Columns["SrcImg"].Visible = false;
            dataGridView1.Columns["SrcInfo"].Visible = false;
            dataGridView1.Columns["SrcPhoto"].Visible = false;
            dataGridView1.Columns["ImgName"].Visible = false;
            dataGridView1.Columns["InfoName"].Visible = false;
            dataGridView1.Columns["SrcImgPath"].Visible = false;
            dataGridView1.Columns["SrcInfoPath"].Visible = false;
            // dataGridView2.Columns["FullPhotosName"].Visible = false;
            PhotoLists.Clear();
            //Console.WriteLine("加载前有生活照人数：" + PhotoLists.Count);

            for (int i = 0; i < p.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = p[i].ID;
                dataGridView1.Rows[i].Cells[1].Value = p[i].Name;
                dataGridView1.Rows[i].Cells[2].Value = p[i].Level;
                dataGridView1.Rows[i].Cells[3].Value = p[i].SrcImg;
                dataGridView1.Rows[i].Cells[4].Value = p[i].SrcInfo;
                dataGridView1.Rows[i].Cells[5].Value = p[i].SrcPhoto;
                dataGridView1.Rows[i].Cells[6].Value = p[i].SrcImgPath;
                dataGridView1.Rows[i].Cells[7].Value = p[i].SrcInfoPath;
                PhotoLists.Add(p[i].FullPhotosName);
            }

            int n = 0;
            for (int j = 0; j < PhotoLists.Count; j++)
            {
                for (int k = 0; k < PhotoLists[j].Count; k++)
                {
                    Console.WriteLine(@"生活照：" + PhotoLists[j][k]);
                }
                if (PhotoLists[j].Count > 0) n++;
            }
            Console.WriteLine(@"加载后有生活照人数：" + n);

            string str = dataGridView1.Rows[0].Cells[6].Value.ToString();
            Console.WriteLine(@"缩略图路径：" + str);
            pictureBox1.Image = Image.FromFile(str);


            PersonSrcPhotoPathList.Clear();//移除原有元素
            PersonSrcPhotoPathList = PhotoLists[0];//添加现有元素
            PersonId = (string)dataGridView1.Rows[0].Cells[0].Value;//输入的 id
            PersonName = (string)dataGridView1.Rows[0].Cells[1].Value;//输入的 姓名
            PersonLevel = (string)dataGridView1.Rows[0].Cells[2].Value;//传入的 级别
            PersonSrcImg = (string)dataGridView1.Rows[0].Cells[3].Value;//传入的 缩略图存放 路径
            PersonSrcInfo = (string)dataGridView1.Rows[0].Cells[4].Value;//传入的 简介图存放 路径
            PersonSrcPhoto = (string)dataGridView1.Rows[0].Cells[5].Value;//传入的 生活照存放 路径
            PersonSrcImgPath = (string)dataGridView1.Rows[0].Cells[6].Value;//传入的 缩略图文件绝对路径
            PersonSrcInfoPath = (string)dataGridView1.Rows[0].Cells[7].Value;//传入的 简介图文件绝对路径
        }

        /// <summary>
        /// 界面初始化显示方法
        /// </summary>
        private void Interface() {
            groupBox2.Location = groupBox3.Location = groupBox4.Location = groupBox1.Location;
            pictureBox1.Location = new Point(button4.Location.X + button4.Width - pictureBox1.Width, groupBox1.Location.Y);

            GridView(false);
            Picture(false);
            button17.Visible = false;

            groupBox1.Text = button1.Text;
            groupBox2.Text = button2.Text;
            groupBox3.Text = button3.Text;
            groupBox4.Text = button4.Text;

            button5.Text = button8.Text = button11.Text = button14.Text = @"首席业务总监";
            button6.Text = button9.Text = button12.Text = button15.Text = @"高级业务总监";
            button7.Text = button10.Text = button13.Text = button16.Text = @"业务总监";
        }

        /// <summary>
        /// 四大市场按钮——启用
        /// </summary>
        void BtnArySc() {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        /// <summary>
        /// 四大市场子按钮组——隐藏
        /// </summary>
        void BtnAry() {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
        }

        /// <summary>
        /// 四大市场子按钮组——启用选项
        /// </summary>
        void ChildBtnAry() {
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            button13.Enabled = true;
            button14.Enabled = true;
            button15.Enabled = true;
            button16.Enabled = true;

            button5.ForeColor = Color.Green;
            button6.ForeColor = Color.Green;
            button7.ForeColor = Color.Green;
            button8.ForeColor = Color.Green;
            button9.ForeColor = Color.Green;
            button10.ForeColor = Color.Green;
            button11.ForeColor = Color.Green;
            button12.ForeColor = Color.Green;
            button13.ForeColor = Color.Green;
            button14.ForeColor = Color.Green;
            button15.ForeColor = Color.Green;
            button16.ForeColor = Color.Green;
        }

        /// <summary>
        /// 表格 显示或隐藏
        /// </summary>
        /// <param name="_bool"></param>
        void GridView(Boolean _bool) {
            dataGridView1.Visible = _bool;
        }

        /// <summary>
        /// 主界面缩略图
        /// </summary>
        /// <param name="_bool"></param>
        void Picture(Boolean _bool) {
            pictureBox1.Visible = _bool;
        }

        /// <summary>
        /// 生活照相关 元件，显示与隐藏
        /// </summary>
        /// <param name="_bool"></param>
        void Add_Exit_Btn(Boolean _bool) {
            label6.Visible = _bool;
            listBox1.Visible = _bool;
            button19.Visible = _bool;
            button20.Visible = _bool;
        }

        //市场一
        private void button1_Click(object sender, EventArgs e) {
            BtnAry();           //四大市场子按钮组——隐藏
            BtnArySc();         //四大市场按钮——启用
            ChildBtnAry();      // 四大市场子按钮组——启用选项
            GridView(false);    // 表格 隐藏
            Picture(false);     //主界面缩略图 隐藏
            button17.Visible = false;// 人员 按钮,隐藏
            groupBox1.Visible = true;
            button1.Enabled = false;
        }

        //市场二
        private void button2_Click(object sender, EventArgs e) {
            BtnAry();
            BtnArySc();
            ChildBtnAry();
            GridView(false);
            Picture(false);
            button17.Visible = false;
            groupBox2.Visible = true;
            button2.Enabled = false;
        }

        //市场三
        private void button3_Click(object sender, EventArgs e) {
            BtnAry();
            BtnArySc();
            ChildBtnAry();
            GridView(false);
            Picture(false);
            button17.Visible = false;
            groupBox3.Visible = true;
            button3.Enabled = false;
        }

        //市场四
        private void button4_Click(object sender, EventArgs e) {
            BtnAry();
            BtnArySc();
            ChildBtnAry();
            GridView(false);
            Picture(false);
            button17.Visible = false;
            groupBox4.Visible = true;
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e) {
            ChildBtnAry();  // 四大市场子按钮组——启用选项
            GridView(true);// 表格 隐藏
            button5.Enabled = false;

            if (_pic11.Count == 0)
            {
                _pic11 = GetFileName(AList1);//市场一,90
            }
            else if (_pic11.Count > 0)
            {
                _pic11.Clear();
                _pic11 = GetFileName(AList1);//市场一,90
            }
            OutPutGridView(_pic11);

            Picture(true);
            button17.Visible = true;// 人员 按钮,显示
        }

        private void button6_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button6.Enabled = false;

            if (_pic12.Count == 0)
                _pic12 = GetFileName(AList2);//市场一,80
            OutPutGridView(_pic12);

            Picture(true);
            button17.Visible = true;

        }

        private void button7_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button7.Enabled = false;

            if (_pic13.Count == 0)
                _pic13 = GetFileName(AList3);//市场一,70
            OutPutGridView(_pic13);

            Picture(true);
            button17.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button8.Enabled = false;

            if (_pic21.Count == 0)
                _pic21 = GetFileName(BList1);//市场二,90
            OutPutGridView(_pic21);

            Picture(true);
            button17.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button9.Enabled = false;

            if (_pic22.Count == 0)
                _pic22 = GetFileName(BList2);//市场二,80
            OutPutGridView(_pic22);

            Picture(true);
            button17.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button10.Enabled = false;

            if (_pic23.Count == 0)
                _pic23 = GetFileName(BList3);//市场二,70
            OutPutGridView(_pic23);

            Picture(true);
            button17.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button11.Enabled = false;

            if (_pic31.Count == 0)
                _pic31 = GetFileName(CList1);//市场三,90
            OutPutGridView(_pic31);

            Picture(true);
            button17.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button12.Enabled = false;

            if (_pic32.Count == 0)
                _pic32 = GetFileName(CList2);//市场三,80
            OutPutGridView(_pic32);

            Picture(true);
            button17.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button13.Enabled = false;

            if (_pic33.Count == 0)
                _pic33 = GetFileName(CList3);//市场三,70
            OutPutGridView(_pic33);

            Picture(true);
            button17.Visible = true;
        }

        private void button14_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button14.Enabled = false;

            if (_pic41.Count == 0)
                _pic41 = GetFileName(DList1);//市场四,90
            OutPutGridView(_pic41);

            Picture(true);
            button17.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button15.Enabled = false;

            if (_pic42.Count == 0)
                _pic42 = GetFileName(DList2);//市场四,80
            OutPutGridView(_pic42);

            Picture(true);
            button17.Visible = true;
        }

        private void button16_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button16.Enabled = false;

            if (_pic43.Count == 0)
                _pic43 = GetFileName(DList3);//市场四,70
            OutPutGridView(_pic43);

            Picture(true);
            button17.Visible = true;
        }

        private void button18_Click(object sender, EventArgs e) {
            Environment.Exit(0);
        }

        /// <summary>
        /// 添加人员，选择缩略图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_Click(object sender, EventArgs e) {
            if (!Upload) return;//不是新增模式不能点击
            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = ".";
            openFileDialog1.Title = @"请选择缩略图文件";
            openFileDialog1.Filter = @"缩略图文件(*.jpg,*.png)|*.jpg;*.png";
            //openFileDialog1.ShowDialog();
            //if (openFileDialog1.FileName != string.Empty)
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = openFileDialog1.FileName;
                    textBox1.Text = file;
                    pictureBox2.Image = Image.FromFile(file);
                }
                catch
                {
                    textBox1.Text = @"选择缩略图";
                    MessageBox.Show(@"错误");
                }
            }
        }

        /// <summary>
        /// 添加人员，选择简介图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_Click(object sender, EventArgs e) {
            if (!Upload) return;//不是新增模式不能点击
            openFileDialog2.Multiselect = false;
            openFileDialog2.InitialDirectory = ".";
            openFileDialog2.Title = @"请选择简介图文件";
            openFileDialog2.Filter = @"简介图文件(*.jpg,*.png)|*.jpg;*.png";
            //openFileDialog2.ShowDialog();
            //if (openFileDialog2.FileName != string.Empty)
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = openFileDialog2.FileName;
                    textBox2.Text = file;
                    pictureBox3.Image = Image.FromFile(file);
                }
                catch
                {
                    textBox2.Text = @"选择简介大图";
                    MessageBox.Show(@"错误");
                }
            }
        }

        //主界面，添加人员按钮
        private void button17_Click(object sender, EventArgs e) {

            Info formInfo = new Info();
            DialogResult result = formInfo.ShowDialog();
            if (result == DialogResult.Cancel)
            {//取消

            }
            else if (result == DialogResult.OK)
            {//添加
                Upload = true;//上传
                panel1.Visible = false;
                button21.Text = @"上传";
                textBox1.Text = @"选择缩略图";
                textBox2.Text = @"选择简介打图";
                pictureBox2.Image = null;
                pictureBox3.Image = null;
                textBox6.Text = "";//id
                textBox3.Text = "";//姓名
                textBox4.Text = ""; //市场分类
                textBox5.Text = ""; //级别

                if (PersonSrcImg.IndexOf("Array_1") != 0)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr1]; //市场分类：一
                }
                else if (PersonSrcImg.IndexOf("Array_2") != 0)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr2]; //市场分类：二
                }
                else if (PersonSrcImg.IndexOf("Array_3") != 0)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr3]; //市场分类：三
                }
                else if (PersonSrcImg.IndexOf("Array_4") != 0)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr4]; //市场分类：四
                }

                if (PersonSrcImg.IndexOf("IMG_1") != 0)
                {
                    textBox5.Text = LevelName[(int)Levels.Img90]; //级别：90
                }
                else if (PersonSrcImg.IndexOf("IMG_2") != 0)
                {
                    textBox5.Text = LevelName[(int)Levels.Img80]; //级别：80
                }
                else if (PersonSrcImg.IndexOf("IMG_3") != 0)
                {
                    textBox5.Text = LevelName[(int)Levels.Img70]; //级别：70
                }

                if (PersonSrcImg.IndexOf("IMG_1") == -1)
                {
                    label6.Visible = false;
                    listBox1.Visible = false;
                    button19.Visible = false;
                    button20.Visible = false;
                }
                else
                {
                    label6.Visible = true;
                    listBox1.Visible = true;
                    button19.Visible = true;
                    button20.Visible = true;
                }
            }
            else if (result == DialogResult.Retry)
            {//编辑
                Upload = false;//更新
                panel1.Visible = false;
                button21.Text = @"更新";
                pictureBox2.Image = Image.FromFile(PersonSrcImgPath);
                pictureBox3.Image = Image.FromFile(PersonSrcInfoPath);
                textBox1.Text = PersonSrcImgPath;
                textBox2.Text = PersonSrcInfoPath;
                textBox6.Text = PersonId;//id
                textBox3.Text = PersonName;//姓名
                textBox4.Text = ""; //市场分类
                textBox5.Text = ""; //级别
                if (PersonSrcImg.IndexOf("Array_1") != 0)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr1]; //市场分类：一
                }
                else if (PersonSrcImg.IndexOf("Array_2") != 0)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr2]; //市场分类：二
                }
                else if (PersonSrcImg.IndexOf("Array_3") != 0)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr3]; //市场分类：三
                }
                else if (PersonSrcImg.IndexOf("Array_4") != 0)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr4]; //市场分类：四
                }

                if (PersonSrcImg.IndexOf("IMG_1") != 0)
                {
                    textBox5.Text = LevelName[(int)Levels.Img90]; //级别：90
                }
                else if (PersonSrcImg.IndexOf("IMG_2") != 0)
                {
                    textBox5.Text = LevelName[(int)Levels.Img80]; //级别：80
                }
                else if (PersonSrcImg.IndexOf("IMG_3") != 0)
                {
                    textBox5.Text = LevelName[(int)Levels.Img70]; //级别：70
                }

                if (PersonSrcImg.IndexOf("IMG_1") == -1)
                {
                    label6.Visible = false;
                    listBox1.Visible = false;
                    button19.Visible = false;
                    button20.Visible = false;
                }
                else
                {
                    label6.Visible = true;
                    listBox1.Visible = true;
                    button19.Visible = true;
                    button20.Visible = true;
                }

                //PersonSrcPhotoPathList.Clear();//移除原有元素
                //PersonSrcPhotoPathList = PhotoLists[i];//添加现有元素
                listBox1.Items.Clear();
                for (int i = 0; i < PersonSrcPhotoPathList.Count; i++)
                {
                    listBox1.Items.Add(PersonSrcPhotoPathList[i]);
                }

            }
            else if (result == DialogResult.No)
            {//删除
                DialogResult delResult = MessageBox.Show(@"确定删除吗？", @"提示：", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (delResult == DialogResult.Yes)
                {
                    pictureBox1.Image = null;
                    File.Delete(PersonSrcImgPath);//传入的 缩略图文件绝对路径

                    File.Delete(PersonSrcInfoPath);//传入的 简介图文件绝对路径
                    for (int i = 0; i < PersonSrcPhotoPathList.Count; i++)
                    {
                        File.Delete(PersonSrcPhotoPathList[i]);//传入的 生活照文件数组绝对路径
                    }
                    PersonSrcPhotoPathList.Clear();//清空路径元素
                    
                    MessageBox.Show(@"已删除人员");
                }
                else
                {
                    MessageBox.Show(@"删除已取消");
                }
            }
        }

        //添加人员面板，取消按钮
        private void button22_Click(object sender, EventArgs e) {
            panel1.Visible = true;

        }
        /// <summary>
        /// 点击表格，获取人员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_Click(object sender, EventArgs e) {

            int i = int.Parse(dataGridView1.CurrentCellAddress.Y.ToString());

            string id = dataGridView1.Rows[i].Cells[0].Value.ToString();//人员 ID号
            string name = dataGridView1.Rows[i].Cells[1].Value.ToString();//人员 姓名
            string level = dataGridView1.Rows[i].Cells[2].Value.ToString();//人员 级别
            string srcImg = dataGridView1.Rows[i].Cells[3].Value.ToString();//缩略图存放 路径
            string srcInfo = dataGridView1.Rows[i].Cells[4].Value.ToString();//简介图存放 路径
            string srcPhoto = dataGridView1.Rows[i].Cells[5].Value.ToString();//生活照存放 路径
            string srcImgPath = dataGridView1.Rows[i].Cells[6].Value.ToString();//缩略图文件路径
            string srcInfoPath = dataGridView1.Rows[i].Cells[7].Value.ToString();//简介图文件路径

            pictureBox1.Image = Image.FromFile(srcImgPath);

            PersonSrcPhotoPathList.Clear();//移除原有元素
            PersonSrcPhotoPathList = PhotoLists[i];//添加现有元素

            Console.WriteLine(@"人员 ID号：" + id);
            Console.WriteLine(@"人员 姓名：" + name);
            Console.WriteLine(@"人员 级别：" + level);
            Console.WriteLine(@"缩略图存放 路径：" + srcImg);
            Console.WriteLine(@"简介图存放 路径：" + srcInfo);
            Console.WriteLine(@"生活照存放 路径：" + srcPhoto);
            Console.WriteLine(@"缩略图文件 绝对路径：" + srcImgPath);
            Console.WriteLine(@"简介图文件 绝对路径：" + srcInfoPath);

            for (int j = 0; j < PersonSrcPhotoPathList.Count; j++)
            {
                Console.WriteLine(@"生活照文件路径：" + PersonSrcPhotoPathList[j]);
            }
            Console.WriteLine("");

            PersonId = id;            //输入的 id
            PersonName = name;                           //输入的 姓名
            PersonLevel = level;   //传入的 级别
            PersonSrcImg = srcImg;                       //传入的 缩略图存放 路径
            PersonSrcInfo = srcInfo;                      //传入的 简介图存放 路径
            PersonSrcPhoto = srcPhoto;                //传入的 生活照存放 路径
            PersonSrcImgPath = srcImgPath;         //传入的 缩略图文件绝对路径
            PersonSrcInfoPath = srcInfoPath;        //传入的 简介图文件绝对路径
        }

        private void button21_Click(object sender, EventArgs e) {
            string imgName;
            string infoName;
            DialogResult UpResult;
            if (Upload)//上传
            {
                UpResult = MessageBox.Show(@"确定上传", @"提示：", MessageBoxButtons.YesNo);
                if (UpResult == DialogResult.Yes)
                {
                    //textBox6.Text//id
                    //textBox3.Text = PersonName;//姓名
                    //textBox5.Text = LevelName[(int)Levels.Img90]; //级别：90
                    //textBox1.Text;//缩略图文件夹
                    //textBox2.Text;//简介图文件夹

                    imgName = textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + (textBox1.Text.LastIndexOf(".jpg") != -1 ? ".jpg" : ".png");
                    infoName = textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + (textBox2.Text.LastIndexOf(".jpg") != -1 ? ".jpg" : ".png");
                    
                    File.Copy(textBox1.Text, PersonSrcImg + imgName);
                    File.Copy(textBox2.Text, PersonSrcInfo + infoName);

                    Console.WriteLine(PersonSrcImg + imgName);
                    Console.WriteLine(PersonSrcInfo + infoName);
                    MessageBox.Show(@"上传完成！");
                    panel1.Visible = true;
                }
            }
            else//更新
            {
                UpResult = MessageBox.Show(@"确定更新", @"提示：", MessageBoxButtons.YesNo);
                if (UpResult == DialogResult.Yes)
                {
                    imgName = textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + (textBox1.Text.LastIndexOf(".jpg") != -1 ? ".jpg" : ".png");
                    infoName = textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + (textBox2.Text.LastIndexOf(".jpg") != -1 ? ".jpg" : ".png");
                    //dataGridView1.DataSource = null;
                    //pictureBox1.Image = null;
                    //pictureBox2.Image = null;
                    //pictureBox3.Image = null;
                    File.Move(PersonSrcImgPath, PersonSrcImg + imgName);
                    File.Move(PersonSrcInfoPath, PersonSrcInfo + infoName);

                    //File.Move(PersonSrcImgPath, @"F:\MyFile\Data\" + imgName);
                    //File.Move(PersonSrcInfoPath, @"F:\MyFile\Data\" + infoName);

                    Console.WriteLine(PersonSrcImg + imgName);
                    Console.WriteLine(PersonSrcInfo + infoName);
                    MessageBox.Show(@"更新完成！");
                    panel1.Visible = true;
                }
            }
        }
    }
}
