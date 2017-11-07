using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;


namespace LCApp {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(this.panel1.Width,this.panel1.Height);//主界面显示内容的宽高

            Interface();    //界面初始化显示方法
            BtnAry();       // 四大市场子按钮组——隐藏

            string str2 = System.AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine("地址："+str2);
            ReadXmlData(str2+ @"\Data.xml");//加载xml文档
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

        /// <summary>
        /// 定义存储文件夹地址的泛型数组,P~视频；Str_TMP_1~市场一；Str_TMP_2~市场二；Str_TMP_3~市场三；Str_TMP_4~市场四
        /// </summary>
        public List<string> P_List = new List<string>();//视频

        private List<string> Str_TMP_1 = new List<string>();//市场一
        private List<string> Str_TMP_2 = new List<string>();//市场二
        private List<string> Str_TMP_3 = new List<string>();//市场三
        private List<string> Str_TMP_4 = new List<string>();//市场四


        public List<string> A_List1 = new List<string>();//市场一,90
        public List<string> A_List2 = new List<string>();//市场一,80
        public List<string> A_List3 = new List<string>();//市场一,70

        public List<string> B_List1 = new List<string>();//市场二,90
        public List<string> B_List2 = new List<string>();//市场二,80
        public List<string> B_List3 = new List<string>();//市场二,70

        public List<string> C_List1 = new List<string>();//市场三,90
        public List<string> C_List2 = new List<string>();//市场三,80
        public List<string> C_List3 = new List<string>();//市场三,70

        public List<string> D_List1 = new List<string>();//市场四,90
        public List<string> D_List2 = new List<string>();//市场四,80
        public List<string> D_List3 = new List<string>();//市场四,70

        
        List<Person> PIC11 = new List<Person>();//市场一,90 -->暂存读取的文件数据
        List<Person> PIC12 = new List<Person>();//市场一,80 -->暂存读取的文件数据
        List<Person> PIC13 = new List<Person>();//市场一,70 -->暂存读取的文件数据

        List<Person> PIC21 = new List<Person>();//市场二,90 -->暂存读取的文件数据
        List<Person> PIC22 = new List<Person>();//市场二,80 -->暂存读取的文件数据
        List<Person> PIC23 = new List<Person>();//市场二,70 -->暂存读取的文件数据

        List<Person> PIC31 = new List<Person>();//市场三,90 -->暂存读取的文件数据
        List<Person> PIC32 = new List<Person>();//市场三,80 -->暂存读取的文件数据
        List<Person> PIC33 = new List<Person>();//市场三,70 -->暂存读取的文件数据

        List<Person> PIC41 = new List<Person>();//市场四,90 -->暂存读取的文件数据
        List<Person> PIC42 = new List<Person>();//市场四,80 -->暂存读取的文件数据
        List<Person> PIC43 = new List<Person>();//市场四,70 -->暂存读取的文件数据

        private void ReadXmlData(string str) {
            XmlTextReader reader = new XmlTextReader(str);
            while (reader.Read())
            {
                if (reader.NodeType==XmlNodeType.Element)
                {
                    if (reader.Name=="name")
                    {
                        P_List.Add(reader.GetAttribute(0));
                    }
                    if (reader.Name=="name1")
                    {
                        Str_TMP_1.Add(reader.GetAttribute(0));
                    }
                    if (reader.Name=="name2")
                    {
                        Str_TMP_2.Add(reader.GetAttribute(0));
                    }
                    if (reader.Name=="name3")
                    {
                        Str_TMP_3.Add(reader.GetAttribute(0));
                    }
                    if (reader.Name=="name4")
                    {
                        Str_TMP_4.Add(reader.GetAttribute(0));
                    }
                }
                if (reader.NodeType==XmlNodeType.EndElement)
                {
                }
            }

            for (int i = 0; i < 9; i++)
            {
                if (i ==0 || i==3 || i==6)
                {
                    A_List1.Add(Str_TMP_1[i]);//市场一,90
                    B_List1.Add(Str_TMP_2[i]);//市场二,90
                    C_List1.Add(Str_TMP_3[i]);//市场三,90
                    D_List1.Add(Str_TMP_4[i]);//市场四,90
                }
                else if (i == 1 || i == 4 || i == 7)
                {
                    A_List2.Add(Str_TMP_1[i]);//市场一,80
                    B_List2.Add(Str_TMP_2[i]);//市场二,80
                    C_List2.Add(Str_TMP_3[i]);//市场三,80
                    D_List2.Add(Str_TMP_4[i]);//市场四,80
                }
                else if (i == 2 || i == 5 || i == 8)
                {
                    A_List3.Add(Str_TMP_1[i]);//市场一,70
                    B_List3.Add(Str_TMP_2[i]);//市场二,70
                    C_List3.Add(Str_TMP_3[i]);//市场三,70
                    D_List3.Add(Str_TMP_4[i]);//市场四,70
                }
            }
            
        }
        
        public List<Person> GetFileName(List<string> _List) {
            var lists = new List<List<string>>();
            for (int i = 0; i < _List.Count; i++)
            {
                var list = new List<string>();
                lists.Add(list);
            }

            for (int j = 0; j < _List.Count; j++)
            {
                DirectoryInfo dir = new DirectoryInfo(_List[j]);
                foreach (var item in dir.GetFiles("*"))
                {
                    if (item.Extension==".jpg"|| item.Extension==".png")
                    {
                        lists[j].Add(item.Name);
                       // Console.WriteLine(item.Name);
                    }
                }
            }
            

            List<Person> PersonAry = new List<Person>();
            for (int m = 0; m < lists[0].Count; m++)
            {
                Person P = new Person();
                string[] Str = new string[2];
                string str = lists[0][m];
                Str = str.Split('_');
                P.ID = Str[0]; Console.Write("ID:" + P.ID);
                str = Str[1];
                Str = str.Split(',');
                P.Name = Str[0]; Console.Write("  名字:" + P.Name);
                str = Str[1];
                Str = str.Split('.');
                P.Level = Str[0]; Console.WriteLine("  级别:" + P.Level);
                P.SrcImg = _List[0];
                P.SrcInfo = _List[1];
                P.SrcPhoto = _List[2];
                PersonAry.Add(P); Console.WriteLine("***********");
            }

            for (int k = 0; k < lists.Count; k++)
            {
                for (int l = 0; l < lists[k].Count; l++)
                {
                    if (k==0)
                    {
                        Console.WriteLine("+++++缩略图+++++++");
                        PersonAry[l].FullImgName = lists[k][l];
                        Console.WriteLine("缩略图：" + PersonAry[l].SrcImg + lists[k][l]);
                    }
                    else if (k==1)
                    {
                        Console.WriteLine("+++++简介图+++++++");
                        PersonAry[l].FullInfoName = lists[k][l];
                        Console.WriteLine("简介图：" + PersonAry[l].SrcInfo + lists[k][l]);
                    }
                    else if (k==2)
                    {
                        Console.WriteLine("start……");
                        for (int q = 0; q < PersonAry.Count; q++)
                        {
                            
                            if (lists[k][l].IndexOf(PersonAry[q].Name) >=0 )
                            {
                                PersonAry[q].FullPhotosName.Add(lists[k][l]);
                                Console.WriteLine("生活照图：" + PersonAry[q].SrcPhoto + lists[k][l]);
                            }
                        }
                        Console.WriteLine("End……");
                    }
                }
            }

            for (int o = 0; o < PersonAry.Count; o++)
            {
                Console.WriteLine("****************");
                Console.WriteLine(PersonAry[o].ID +"  "+PersonAry[o].Name+"  "+ PersonAry[o].Level);
                Console.WriteLine("------------------");
                
                for (int r = 0; r < PersonAry[o].FullPhotosName.Count; r++)
                {
                    Console.WriteLine(PersonAry[o].FullPhotosName[r]);
                }
            }
            return PersonAry;
        }

        /// <summary>
        /// 输出显示到表格
        /// </summary>
        /// <param name="P">Person Array</param>
        public void OutPutGridView(List<Person> P) {
            this.dataGridView1.DataSource = P;
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView1.Columns["Name"].Visible = false;
            this.dataGridView1.Columns["Level"].Visible = false;
            this.dataGridView1.Columns["SrcImg"].Visible = false;
            this.dataGridView1.Columns["SrcInfo"].Visible = false;
            this.dataGridView1.Columns["SrcPhoto"].Visible = false;
            for (int i = 0; i < P.Count; i++)
            {
                this.dataGridView1.Rows[i].Cells[0].Value = P[i].ID;
                this.dataGridView1.Rows[i].Cells[1].Value = P[i].Name;
                this.dataGridView1.Rows[i].Cells[2].Value = P[i].Level;
                Console.WriteLine(this.dataGridView1.Rows[i].Cells[6].Value);
            }
        }

        /// <summary>
        /// 界面初始化显示方法
        /// </summary>
        private void Interface() {
            this.groupBox2.Location = this.groupBox3.Location = this.groupBox4.Location = this.groupBox1.Location;
            this.pictureBox1.Location = new System.Drawing.Point(this.button4.Location.X + this.button4.Width - this.pictureBox1.Width, this.groupBox1.Location.Y);

            GridView(false);
            picture(false);
            Add_Exit_Btn(false);

            this.groupBox1.Text = this.button1.Text;
            this.groupBox2.Text = this.button2.Text;
            this.groupBox3.Text = this.button3.Text;
            this.groupBox4.Text = this.button4.Text;

            this.button5.Text = this.button8.Text = this.button11.Text = this.button14.Text = "首席业务总监";
            this.button6.Text = this.button9.Text = this.button12.Text = this.button15.Text = "高级业务总监";
            this.button7.Text = this.button10.Text = this.button13.Text = this.button16.Text = "业务总监";
        }

        /// <summary>
        /// 四大市场按钮——启用
        /// </summary>
        void BtnArySC() {
            this.button1.Enabled = true;
            this.button2.Enabled = true;
            this.button3.Enabled = true;
            this.button4.Enabled = true;
        }

        /// <summary>
        /// 四大市场子按钮组——隐藏
        /// </summary>
        void BtnAry() {
            this.groupBox1.Visible = false;
            this.groupBox2.Visible = false;
            this.groupBox3.Visible = false;
            this.groupBox4.Visible = false;
        }

        /// <summary>
        /// 四大市场子按钮组——启用选项
        /// </summary>
        void ChildBtnAry() {
            this.button5.Enabled = true;
            this.button6.Enabled = true;
            this.button7.Enabled = true;
            this.button8.Enabled = true;
            this.button9.Enabled = true;
            this.button10.Enabled = true;
            this.button11.Enabled = true;
            this.button12.Enabled = true;
            this.button13.Enabled = true;
            this.button14.Enabled = true;
            this.button15.Enabled = true;
            this.button16.Enabled = true;

            this.button5.ForeColor = Color.Green;
            this.button6.ForeColor = Color.Green;
            this.button7.ForeColor = Color.Green;
            this.button8.ForeColor = Color.Green;
            this.button9.ForeColor = Color.Green;
            this.button10.ForeColor = Color.Green;
            this.button11.ForeColor = Color.Green;
            this.button12.ForeColor = Color.Green;
            this.button13.ForeColor = Color.Green;
            this.button14.ForeColor = Color.Green;
            this.button15.ForeColor = Color.Green;
            this.button16.ForeColor = Color.Green;
        }
        
        /// <summary>
        /// 表格 显示或隐藏
        /// </summary>
        /// <param name="B"></param>
        void GridView(Boolean B) {
            this.dataGridView1.Visible = B;
        }

        /// <summary>
        /// 主界面缩略图
        /// </summary>
        /// <param name="B"></param>
        void picture(Boolean B) {
            this.pictureBox1.Visible = B;
        }

        /// <summary>
        /// 添加人员与退出程序 按钮，显示与隐藏
        /// </summary>
        /// <param name="B"></param>
        void Add_Exit_Btn(Boolean B) {
            this.button17.Visible = B;
            this.button18.Visible = B;
            this.button23.Visible = B;
            this.button23.Enabled = false;
        }

        //市场一
        private void button1_Click(object sender, EventArgs e) {
            BtnAry();           //四大市场子按钮组——隐藏
            BtnArySC();         //四大市场按钮——启用
            ChildBtnAry();      // 四大市场子按钮组——启用选项
            GridView(false);    // 表格 隐藏
            picture(false);     //主界面缩略图 隐藏
            Add_Exit_Btn(false);//添加人员与退出程序 按钮,隐藏
            this.groupBox1.Visible = true;
            this.button1.Enabled = false;
        }

        //市场二
        private void button2_Click(object sender, EventArgs e) {
            BtnAry();
            BtnArySC();
            ChildBtnAry();
            GridView(false);
            picture(false);
            Add_Exit_Btn(false);
            this.groupBox2.Visible = true;
            this.button2.Enabled = false;           
        }

        //市场三
        private void button3_Click(object sender, EventArgs e) {
            BtnAry();
            BtnArySC();
            ChildBtnAry();
            GridView(false);
            picture(false);
            Add_Exit_Btn(false);
            this.groupBox3.Visible = true;
            this.button3.Enabled = false; 
        }

        //市场四
        private void button4_Click(object sender, EventArgs e) {
            BtnAry();
            BtnArySC();
            ChildBtnAry();
            GridView(false);
            picture(false);
            Add_Exit_Btn(false);
            this.groupBox4.Visible = true;
            this.button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e) {
            ChildBtnAry();  // 四大市场子按钮组——启用选项
            GridView(true);// 表格 隐藏
            button5.Enabled = false;

            if (PIC11.Count == 0)
                PIC11 = GetFileName(A_List1);//市场一,90
            OutPutGridView(PIC11);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button6_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button6.Enabled = false;

            if (PIC12.Count == 0)
                PIC12 = GetFileName(A_List2);//市场一,80
            OutPutGridView(PIC12);

            picture(true);
            Add_Exit_Btn(true);
            
        }

        private void button7_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            button7.Enabled = false;

            if (PIC13.Count == 0)
                PIC13 = GetFileName(A_List3);//市场一,70
            OutPutGridView(PIC13);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button8_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            this.button8.Enabled = false;

            if (PIC21.Count == 0)
                PIC21 = GetFileName(B_List1);//市场二,90
            OutPutGridView(PIC21);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button9_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            this.button9.Enabled = false;

            if (PIC22.Count == 0)
                PIC22 = GetFileName(B_List2);//市场二,80
            OutPutGridView(PIC22);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button10_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            this.button10.Enabled = false;

            if (PIC23.Count == 0)
                PIC23 = GetFileName(B_List3);//市场二,70
            OutPutGridView(PIC23);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button11_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            this.button11.Enabled = false;

            if (PIC31.Count == 0)
                PIC31 = GetFileName(C_List1);//市场三,90
            OutPutGridView(PIC31);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button12_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            this.button12.Enabled = false;

            if (PIC32.Count == 0)
                PIC32 = GetFileName(C_List2);//市场三,80
            OutPutGridView(PIC32);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button13_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            this.button13.Enabled = false;

            if (PIC33.Count == 0)
                PIC33 = GetFileName(C_List3);//市场三,70
            OutPutGridView(PIC33);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button14_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            this.button14.Enabled = false;

            if (PIC41.Count == 0)
                PIC41 = GetFileName(D_List1);//市场四,90
            OutPutGridView(PIC41);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button15_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            this.button15.Enabled = false;

            if (PIC42.Count == 0)
                PIC42 = GetFileName(D_List2);//市场四,80
            OutPutGridView(PIC42);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button16_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            this.button16.Enabled = false;

            if (PIC43.Count == 0)
                PIC43 = GetFileName(D_List3);//市场四,70
            OutPutGridView(PIC43);

            picture(true);
            Add_Exit_Btn(true);
        }

        private void button18_Click(object sender, EventArgs e) {
            System.Environment.Exit(0);
        }

        /// <summary>
        /// 添加人员，选择缩略图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_Click(object sender, EventArgs e) {
            this.openFileDialog1.Multiselect = false;
            this.openFileDialog1.InitialDirectory = ".";
            this.openFileDialog1.Title = "请选择缩略图文件";
            this.openFileDialog1.Filter = "缩略图文件(*.jpg,*.png)|*.jpg;*.png";
            //this.openFileDialog1.ShowDialog();
            //if (openFileDialog1.FileName != string.Empty)
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = this.openFileDialog1.FileName;
                    this.textBox1.Text = file;
                    this.pictureBox2.Image = Image.FromFile(file);
                }
                catch
                {
                    this.textBox1.Text = "选择缩略图";
                    MessageBox.Show("错误");
                }
            }
        }

        /// <summary>
        /// 添加人员，选择简介图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_Click(object sender, EventArgs e) {
            this.openFileDialog2.Multiselect = false;
            this.openFileDialog2.InitialDirectory = ".";
            this.openFileDialog2.Title = "请选择简介图文件";
            this.openFileDialog2.Filter = "简介图文件(*.jpg,*.png)|*.jpg;*.png";
            //this.openFileDialog2.ShowDialog();
            //if (openFileDialog2.FileName != string.Empty)
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = this.openFileDialog2.FileName;
                    this.textBox2.Text = file;
                }
                catch
                {
                    this.textBox2.Text = "选择简介大图";
                    MessageBox.Show("错误");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void label4_Click(object sender, EventArgs e) {

        }

        //主界面，添加人员按钮
        private void button17_Click(object sender, EventArgs e) {
            this.panel1.Visible = false;
        }

        //添加人员面板，取消按钮
        private void button22_Click(object sender, EventArgs e) {
            this.panel1.Visible = true;
        }

        private void dataGridView1_Click(object sender, EventArgs e) {
           // Console.WriteLine(this.dataGridView1.Columns["SrcImg"].);
        }
    }
}
