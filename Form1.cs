using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

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

        private bool BtnClick = false;//传输视频时，按钮失效
        private Thread thdCopyFileThread;//创建一个线程
        private string str = "";//用来记录源文件的名字
        FileStream FormerOpenFileStream;//实例化源文件FileStream类
        FileStream ToFileOpenFileStream;//实例化目标文件FileStream类
        private string Sourcefile;//源文件
        private string Targetfile;//目标文件

        private int progressvalue = 0;
        private int progressmax = 0;

        #region //复制文件函数

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="FormerFile">源文件路径</param>
        /// <param name="ToFile">目的文件路径</param>
        /// <param name="TranSize">传输大小</param>
        /// <param name="progressBar">ProgressBar控件</param>
        public void CopyFile(string FormerFile, string ToFile, int TranSize, ProgressBar progressBar) {
            BtnClick = true;//传输视频时，按钮失效
            //progressBar.Value = 0;//设置进度条的当前位置为0
            //progressBar.Minimum = 0;//设置进度条的最小值为0
            //progressBar.Visible = true;
            progressvalue = 0;
            progressmax = 0;
            try
            {
                FormerOpenFileStream = new FileStream(FormerFile, FileMode.Open, FileAccess.Read);//以只读方式打开源文件
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                FileStream fileToCreate = new FileStream(ToFile, FileMode.Create);//创建目的文件，如果已存在将被覆盖
                fileToCreate.Close();//关闭所有fileToCreate的资源
                fileToCreate.Dispose();//释放所有fileToCreate的资源
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }

            ToFileOpenFileStream = new FileStream(ToFile, FileMode.Append, FileAccess.Write);//以写方式打开目的文件

            int max = Convert.ToInt32(Math.Ceiling((double)FormerOpenFileStream.Length / (double)TranSize));//根据一次传输的大小，计算最大传输个数. Math.Ceiling 方法 (Double),返回大于或等于指定的双精度浮点数的最小整数值

            //progressBar.Maximum = max;//设置进度条的最大值
            progressvalue = 0;
            progressmax = max;

            int FileSize;//每次要拷贝的文件的大小

            if (TranSize < FormerOpenFileStream.Length)//如果分段拷贝，即每次拷贝内容小于文件总长度
            {
                byte[] buffer = new byte[TranSize];//根据传输的大小，定义一个字节数组，用来存储传输的字节
                int copied = 0;//记录传输的大小
                int tem_n = 1;//设置进度栏中进度的增加个数

                while (copied <= ((int)FormerOpenFileStream.Length - TranSize))
                {
                    FileSize = FormerOpenFileStream.Read(buffer, 0, TranSize); //从0开始读到buffer字节数组中，每次最大读TranSize
                    ToFileOpenFileStream.Write(buffer, 0, TranSize); //向目的文件写入字节
                    ToFileOpenFileStream.Flush(); //清空缓存,写入到文件中
                    ToFileOpenFileStream.Position = FormerOpenFileStream.Position; //是源文件的目的文件流的位置相同
                    copied += FileSize; //记录已经拷贝的大小
                    //progressBar.Value = progressBar.Value + tem_n; //增加进度栏的进度块
                    progressvalue = progressvalue + tem_n;
                }

                int leftSize = (int)FormerOpenFileStream.Length - copied;//获取剩余文件的大小
                FileSize = FormerOpenFileStream.Read(buffer, 0, leftSize);//读取剩余的字节
                FormerOpenFileStream.Flush();//清空缓存,写入到文件中
                ToFileOpenFileStream.Write(buffer, 0, leftSize);//写入剩余的部分
                ToFileOpenFileStream.Flush();//清空缓存,写入到文件中
            }
            else//如果整体拷贝，即每次拷贝内容大于文件总长度
            {
                byte[] buffer = new byte[FormerOpenFileStream.Length];
                FormerOpenFileStream.Read(buffer, 0, (int)FormerOpenFileStream.Length);
                FormerOpenFileStream.Flush();
                ToFileOpenFileStream.Write(buffer, 0, (int)FormerOpenFileStream.Length);
                ToFileOpenFileStream.Flush();
            }

            FormerOpenFileStream.Close();
            ToFileOpenFileStream.Close();

            //progressBar.Value = 0;
            progressvalue = 0;
            progressmax = 0;
            str = "";
            BtnClick = false;//传输视频时，按钮失效
        }
        #endregion

        public delegate void CopyFile_Delegate();//定义委托/托管线程
        /// <summary>
        /// 在线程上执行委托（设置托管线程函数）
        /// </summary>
        public void SetCopyFile() {
            //this.Invoke(new CopyFile_Delegate(RunCopyFile));//对指定的线程进行托管
            //下面两行代码等同上面一行代码
            CopyFile_Delegate copyFile_Delegate = new CopyFile_Delegate(RunCopyFile);//创建delegate对象
            this.Invoke(copyFile_Delegate); //调用delegate

        }

        public void Run() {
            thdCopyFileThread = new Thread(start: new ThreadStart(RunCopyFile));
            thdCopyFileThread.Start();
        }

        /// <summary>
        /// 设置线程，运行copy文件，它与代理CopyFile_Delegate应具有相同的参数和返回类型
        /// </summary>
        public void RunCopyFile() {
            CopyFile(Sourcefile, Targetfile, 102400, progressBar1);//复制文件
            Thread.Sleep(0);//避免假死 
            thdCopyFileThread.Abort();//关闭线程
            progressBar1.Visible = false;
        }

        public int Num;//点击人员表格时，所在的序号
        public bool IsEmpty;//判断当前表格是否为空，为空时禁用编辑按钮
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
        public List<string> ListPhoto = new List<string>();//暂存上传文件时，添加的 生活照文件 的路径

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
                Console.WriteLine(@"读取文件夹之前*****************************************************：" + list[j]);
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
            if (p != null)
            {
                Num = 0;//人员所在的序号
                IsEmpty = false;//判断当前表格是否为空，为空时禁用编辑按钮
                dataGridView1.DataSource = p;
                //dataGridView1.Columns["ID"].Visible = false;
                //dataGridView1.Columns["Name"].Visible = false;
                //dataGridView1.Columns["Level"].Visible = false;
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
                Console.WriteLine(p[0].ID);
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

                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
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

                PersonSrcPhotoPathList.Clear(); //移除原有元素
                PersonSrcPhotoPathList = PhotoLists[0]; //添加现有元素
                PersonId = (string)dataGridView1.Rows[0].Cells[0].Value; //输入的 id
                PersonName = (string)dataGridView1.Rows[0].Cells[1].Value; //输入的 姓名
                PersonLevel = (string)dataGridView1.Rows[0].Cells[2].Value; //传入的 级别
                PersonSrcImg = (string)dataGridView1.Rows[0].Cells[3].Value; //传入的 缩略图存放 路径
                PersonSrcInfo = (string)dataGridView1.Rows[0].Cells[4].Value; //传入的 简介图存放 路径
                PersonSrcPhoto = (string)dataGridView1.Rows[0].Cells[5].Value; //传入的 生活照存放 路径
                PersonSrcImgPath = (string)dataGridView1.Rows[0].Cells[6].Value; //传入的 缩略图文件绝对路径
                PersonSrcInfoPath = (string)dataGridView1.Rows[0].Cells[7].Value; //传入的 简介图文件绝对路径

                Image image1 = Image.FromFile(PersonSrcImgPath);
                pictureBox1.Image = new Bitmap(image1);
                image1.Dispose();

                Console.WriteLine(@"缩略图路径：" + PersonSrcImgPath);
                Console.WriteLine(@"简介图路径：" + PersonSrcInfoPath);
            }
            else
            {
                Num = -1;//人员所在的序号
                IsEmpty = true;//判断当前表格是否为空，为空时禁用编辑按钮
                dataGridView1.DataSource = null;
                pictureBox1.Image = null;
            }
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
            button23.Enabled = false;//刷新按钮禁用
        }

        /// <summary>
        /// 四大市场按钮——启用
        /// </summary>
        void BtnArySc() {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
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
            button23.Visible = _bool;//刷新按钮
            button24.Visible = !_bool;//视频设置按钮
        }

        /// <summary>
        /// 主界面缩略图
        /// </summary>
        /// <param name="_bool">缩略图是否隐藏(true/false)</param>
        void Picture(Boolean _bool) {
            pictureBox1.Visible = _bool;
        }

        /// <summary>
        /// 生活照相关 元件，显示与隐藏
        /// </summary>
        /// <param name="_bool">生活照相关 元件，显示与隐藏</param>
        void Add_Exit_Btn(Boolean _bool) {
            label6.Visible = _bool;
            listBox1.Visible = _bool;
            button19.Visible = _bool;
            button20.Visible = _bool;
        }

        //市场一
        private void button1_Click(object sender, EventArgs e) {
            if(BtnClick) return; //传输视频时，按钮失效
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
            if (BtnClick) return; //传输视频时，按钮失效
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
            if (BtnClick) return; //传输视频时，按钮失效
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
            if (BtnClick) return; //传输视频时，按钮失效
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
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();  // 四大市场子按钮组——启用选项
            GridView(true);// 表格 显示
            button5.Enabled = false;

            //市场一,90
            LoadFun(ref _pic11, ref AList1);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;// 人员 按钮,显示
        }

        private void button6_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button6.Enabled = false;

            //市场一,80
            LoadFun(ref _pic12, ref AList2);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;

        }

        private void button7_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button7.Enabled = false;

            //市场一,70
            LoadFun(ref _pic13, ref AList3);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button8.Enabled = false;

            //市场二,90
            LoadFun(ref _pic21, ref BList1);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button9.Enabled = false;

            //市场二,80
            LoadFun(ref _pic22, ref BList2);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button10.Enabled = false;

            //市场二,70
            LoadFun(ref _pic23, ref BList3);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button11.Enabled = false;

            //市场三,90
            LoadFun(ref _pic31, ref CList1);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button12.Enabled = false;

            //市场三,80
            LoadFun(ref _pic32, ref CList2);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button13.Enabled = false;

            //市场三,70
            LoadFun(ref _pic33, ref CList3);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button14_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button14.Enabled = false;

            //市场四,90
            LoadFun(ref _pic41, ref DList1);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button15.Enabled = false;

            //市场四,80
            LoadFun(ref _pic42, ref DList2);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button16_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            ChildBtnAry();
            GridView(true);
            button16.Enabled = false;

            //市场四,70
            LoadFun(ref _pic43, ref DList3);//加载人员资料输出到表格

            Picture(true);
            button17.Visible = true;
        }

        private void button18_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            Application.Exit();
        }

        /// <summary>
        /// 添加人员，选择缩略图
        /// </summary>
        private void textBox1_Click(object sender, EventArgs e) {
            //if (!Upload) return;//不是新增模式不能点击
            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = ".";
            openFileDialog1.FileName = "缩略图文件";
            openFileDialog1.Title = @"请选择 缩略图 图片文件  所需图片分辨率：210px(宽) - 250px(高)";
            openFileDialog1.Filter = @"缩略图文件(*.jpg,*.png)|*.jpg;*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = openFileDialog1.FileName;
                    textBox1.Text = file;
                    Image image2 = Image.FromFile(file);
                    //pictureBox2.Image = new Bitmap(image2);
                    //image2.Dispose();
                    if (image2.Width == 210 && image2.Height == 250)
                    {
                        pictureBox2.Image = new Bitmap(image2);
                        image2.Dispose();
                    }
                    else
                    {
                        textBox1.Text = @"选择缩略图";
                        MessageBox.Show(@"所需图片分辨率：210px * 250px ；图片尺寸不匹配，请重新选择", @"缩略图文件", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        image2.Dispose();
                    }
                }
                catch
                {
                    textBox1.Text = @"选择缩略图";
                    MessageBox.Show(@"错误","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 添加人员，选择简介图
        /// </summary>
        private void textBox2_Click(object sender, EventArgs e) {
            //if (!Upload) return;//不是新增模式不能点击
            openFileDialog2.Multiselect = false;
            openFileDialog2.InitialDirectory = ".";
            openFileDialog2.FileName = "简介图文件";
            openFileDialog2.Title = @"请选择 简介 图片文件  所需图片分辨率：1920px(宽) - 1080px(高)";
            openFileDialog2.Filter = @"简介图文件(*.jpg,*.png)|*.jpg;*.png";

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = openFileDialog2.FileName;
                    textBox2.Text = file;
                    Image image3 = Image.FromFile(file);
                    //pictureBox3.Image = new Bitmap(image3);
                    //image3.Dispose();
                    if (image3.Width == 1920 && image3.Height == 1080)
                    {
                        pictureBox3.Image = new Bitmap(image3);
                        image3.Dispose();
                    }
                    else
                    {
                        textBox2.Text = @"选择简介大图";
                        MessageBox.Show(@"所需图片分辨率：1920px * 1080px ；图片尺寸不匹配，请重新选择", @"简介图文件", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        image3.Dispose();
                    }
                }
                catch
                {
                    textBox2.Text = @"选择简介大图";
                    MessageBox.Show(@"错误", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //主界面，添加人员按钮
        private void button17_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
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
                listBox1.Items.Clear();//生活照表格清空
                ListPhoto.Clear(); //暂存上传文件时，添加的 生活照文件 的路径

                if (PersonSrcImg.IndexOf("Array_1") != -1)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr1]; //市场分类：一
                }
                else if (PersonSrcImg.IndexOf("Array_2") != -1)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr2]; //市场分类：二
                }
                else if (PersonSrcImg.IndexOf("Array_3") != -1)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr3]; //市场分类：三
                }
                else if (PersonSrcImg.IndexOf("Array_4") != -1)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr4]; //市场分类：四
                }

                if (PersonSrcImg.IndexOf("IMG_1") != -1)
                {
                    textBox5.Text = LevelName[(int)Levels.Img90]; //级别：90
                    PersonLevel = "90";
                }
                else if (PersonSrcImg.IndexOf("IMG_2") != -1)
                {
                    textBox5.Text = LevelName[(int)Levels.Img80]; //级别：80
                    PersonLevel = "80";
                }
                else if (PersonSrcImg.IndexOf("IMG_3") != -1)
                {
                    textBox5.Text = LevelName[(int)Levels.Img70]; //级别：70
                    PersonLevel = "70";
                }

                if (PersonSrcImg.IndexOf("IMG_1") == -1)
                {
                    //label6.Visible = false;
                    //listBox1.Visible = false;
                    //button19.Visible = false;
                    //button20.Visible = false;
                    Add_Exit_Btn(false);//生活照相关元件 隐藏
                }
                else
                {
                    //label6.Visible = true;
                    //listBox1.Visible = true;
                    //button19.Visible = true;
                    //button20.Visible = true;
                    Add_Exit_Btn(true);//生活照相关元件 显示
                }
            }
            else if (result == DialogResult.Retry)
            {//编辑
                if (IsEmpty) return;//判断当前表格是否为空，为空时禁用编辑按钮
                Upload = false;//更新
                panel1.Visible = false;
                button21.Text = @"更新";
                ListPhoto.Clear(); //暂存上传文件时，添加的 生活照文件 的路径

                Image image4 = Image.FromFile(PersonSrcImgPath);
                Image image5 = Image.FromFile(PersonSrcInfoPath);

                pictureBox2.Image = new Bitmap(image4);
                pictureBox3.Image = new Bitmap(image5);
                image4.Dispose();
                image5.Dispose();

                textBox1.Text = PersonSrcImgPath;
                textBox2.Text = PersonSrcInfoPath;
                textBox6.Text = PersonId;//id
                textBox3.Text = PersonName;//姓名
                textBox4.Text = ""; //市场分类
                textBox5.Text = ""; //级别
                if (PersonSrcImg.IndexOf("Array_1") != -1)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr1]; //市场分类：一
                }
                else if (PersonSrcImg.IndexOf("Array_2") != -1)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr2]; //市场分类：二
                }
                else if (PersonSrcImg.IndexOf("Array_3") != -1)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr3]; //市场分类：三
                }
                else if (PersonSrcImg.IndexOf("Array_4") != -1)
                {
                    textBox4.Text = CategoryName[(int)Category.Arr4]; //市场分类：四
                }

                if (PersonSrcImg.IndexOf("IMG_1") != -1)
                {
                    textBox5.Text = LevelName[(int)Levels.Img90]; //级别：90
                    PersonLevel = "90";
                }
                else if (PersonSrcImg.IndexOf("IMG_2") != -1)
                {
                    textBox5.Text = LevelName[(int)Levels.Img80]; //级别：80
                    PersonLevel = "80";
                }
                else if (PersonSrcImg.IndexOf("IMG_3") != -1)
                {
                    textBox5.Text = LevelName[(int)Levels.Img70]; //级别：70
                    PersonLevel = "70";
                }

                if (PersonSrcImg.IndexOf("IMG_1") == -1)
                {
                    Add_Exit_Btn(false);//生活照相关元件 隐藏
                }
                else
                {
                    //label6.Visible = true;
                    //listBox1.Visible = true;
                    //button19.Visible = false;//生活照 添加按钮
                    //button20.Visible = false;//生活照 删除按钮
                    Add_Exit_Btn(true);//生活照相关元件 隐藏
                }

                listBox1.Items.Clear();
                for (int i = 0; i < PersonSrcPhotoPathList.Count; i++)
                {
                    listBox1.Items.Add(PersonSrcPhotoPathList[i]);
                }
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    ListPhoto.Add(listBox1.Items[i].ToString());
                }
            }
            else if (result == DialogResult.No)
            {//删除
                DialogResult delResult = System.Windows.Forms.MessageBox.Show(@"确定删除吗？", @"提示：", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (delResult == DialogResult.Yes)
                {
                    pictureBox1.Image.Dispose();
                    File.Delete(PersonSrcImgPath); //传入的 缩略图文件绝对路径

                    File.Delete(PersonSrcInfoPath);//传入的 简介图文件绝对路径

                    for (int i = 0; i < PersonSrcPhotoPathList.Count; i++)
                    {
                        Console.WriteLine(PersonSrcPhotoPathList[i]);
                        File.Delete(PersonSrcPhotoPathList[i]);//传入的 生活照文件数组绝对路径
                    }
                    PersonSrcPhotoPathList.Clear();//清空路径元素

                    Console.WriteLine(@"已删除" + PersonSrcImgPath);
                    System.Windows.Forms.MessageBox.Show(@"已删除人员");

                    dataGridView1.DataSource = null;
                    pictureBox1.Image = null;
                    button23.Enabled = true;//刷新按钮禁用
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(@"删除已取消");
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
        private void dataGridView1_Click(object sender, EventArgs e) {

            int i = int.Parse(dataGridView1.CurrentCellAddress.Y.ToString());
            Num = i;//人员所在的序号
            Console.WriteLine(@"点击的编号是：" + i);
            if (i < 0)
            {
                pictureBox1.Image = null;
                return;
            }
            string id = dataGridView1.Rows[i].Cells[0].Value.ToString();//人员 ID号
            string name = dataGridView1.Rows[i].Cells[1].Value.ToString();//人员 姓名
            string level = dataGridView1.Rows[i].Cells[2].Value.ToString();//人员 级别
            string srcImg = dataGridView1.Rows[i].Cells[3].Value.ToString();//缩略图存放 路径
            string srcInfo = dataGridView1.Rows[i].Cells[4].Value.ToString();//简介图存放 路径
            string srcPhoto = dataGridView1.Rows[i].Cells[5].Value.ToString();//生活照存放 路径
            string srcImgPath = dataGridView1.Rows[i].Cells[6].Value.ToString();//缩略图文件路径
            string srcInfoPath = dataGridView1.Rows[i].Cells[7].Value.ToString();//简介图文件路径

            Image image6 = Image.FromFile(srcImgPath);
            pictureBox1.Image = new Bitmap(image6);
            image6.Dispose();

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

            PersonId = id;                                         //输入的 id
            PersonName = name;                             //输入的 姓名
            PersonLevel = level;                                //传入的 级别
            PersonSrcImg = srcImg;                      //传入的 缩略图存放 路径
            PersonSrcInfo = srcInfo;                     //传入的 简介图存放 路径
            PersonSrcPhoto = srcPhoto;               //传入的 生活照存放 路径
            PersonSrcImgPath = srcImgPath;           //传入的 缩略图文件绝对路径
            PersonSrcInfoPath = srcInfoPath;          //传入的 简介图文件绝对路径
        }

        /// <summary>
        /// 上传/更新 按钮
        /// </summary>
        private void button21_Click(object sender, EventArgs e) {
            //数字
            Regex regNumber = new Regex(@"^[0-9]*$");
            //中文、英文 但不包括下划线等符号
            Regex regXXX = new Regex(@"^[\u4E00-\u9FA5A-Za-z]+$");
            if (textBox6.Text.Trim() == String.Empty)
            {
                MessageBox.Show(@"序号：不能为空", @"警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (textBox3.Text.Trim() == String.Empty)
            {
                MessageBox.Show(@"姓名：不能为空", @"警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (regNumber.IsMatch(textBox6.Text.Trim())==false)
            {
                MessageBox.Show(@"序号：非数字", @"警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (regXXX.IsMatch(textBox3.Text.Trim()) == false)
            {
                MessageBox.Show(@"姓名：非 中文或英文", @"警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string imgName;//缩略图 新名字
            string infoName;//简介图 新名字
            List<string> photoName = new List<string>();//生活照 新名字
            DialogResult UpResult;
            if (Upload)//上传
            {
                UpResult = System.Windows.Forms.MessageBox.Show(@"确定上传", @"提示：", MessageBoxButtons.YesNo);
                if (UpResult == DialogResult.Yes)
                {
                    //textBox6.Text//id
                    //textBox3.Text = PersonName;//姓名
                    //textBox5.Text = LevelName[(int)Levels.Img90]; //级别：90
                    //textBox1.Text;//缩略图文件夹
                    //textBox2.Text;//简介图文件夹

                    imgName = textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + (textBox1.Text.LastIndexOf(".jpg") != -1 ? ".jpg" : ".png");
                    infoName = textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + (textBox2.Text.LastIndexOf(".jpg") != -1 ? ".jpg" : ".png");

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        photoName.Add(textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + @"," + (i + 1) + (listBox1.Items[i].ToString().LastIndexOf(".jpg") != -1 ? ".jpg" : ".png"));
                    }

                    File.Copy(textBox1.Text, PersonSrcImg + imgName);
                    File.Copy(textBox2.Text, PersonSrcInfo + infoName);

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        File.Copy(listBox1.Items[i].ToString(), PersonSrcPhoto + photoName[i]);//生活照 文件绝对路径(旧)，文件绝对路径(新)
                    }

                    Console.WriteLine(PersonSrcImg + imgName);
                    Console.WriteLine(PersonSrcInfo + infoName);
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        Console.WriteLine(PersonSrcPhoto + photoName[i]);
                    }
                    System.Windows.Forms.MessageBox.Show(@"上传完成！");

                    dataGridView1.DataSource = null;
                    pictureBox1.Image = null;
                    panel1.Visible = true;
                    button23.Enabled = true;//刷新按钮禁用
                }
            }
            else//更新
            {
                UpResult = System.Windows.Forms.MessageBox.Show(@"确定更新", @"提示：", MessageBoxButtons.YesNo);
                if (UpResult == DialogResult.Yes)
                {
                    imgName = textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + (textBox1.Text.LastIndexOf(".jpg") != -1 ? ".jpg" : ".png");
                    infoName = textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + (textBox2.Text.LastIndexOf(".jpg") != -1 ? ".jpg" : ".png");

                    //for (int i = 0; i < PersonSrcPhotoPathList.Count; i++)
                    //{
                    //    photoName.Add(textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + @"," + (i + 1) + (textBox1.Text.LastIndexOf(".jpg") != -1 ? ".jpg" : ".png"));
                    //}


                    //缩略图 编辑
                    if (PersonSrcImgPath == textBox1.Text)
                    {
                        if (PersonSrcImgPath == PersonSrcImg + imgName)
                        {

                        }
                        else
                        {
                            File.Move(PersonSrcImgPath, PersonSrcImg + imgName);
                        }
                    }
                    else
                    {
                        if (PersonSrcImgPath == PersonSrcImg + imgName)
                        {
                            File.Copy(textBox1.Text, PersonSrcImgPath, true);
                        }
                        else
                        {
                            File.Copy(textBox1.Text, PersonSrcImgPath, true);
                            File.Move(PersonSrcImgPath, PersonSrcImg + imgName);
                        }
                    }

                    //简介图 编辑
                    if (PersonSrcInfoPath == textBox2.Text)
                    {
                        if (PersonSrcInfoPath == PersonSrcInfo + infoName)
                        {

                        }
                        else
                        {
                            File.Move(PersonSrcInfoPath, PersonSrcInfo + infoName);
                        }
                    }
                    else
                    {
                        if (PersonSrcInfoPath == PersonSrcInfo + infoName)
                        {
                            File.Copy(textBox2.Text, PersonSrcInfoPath, true);
                        }
                        else
                        {
                            File.Copy(textBox2.Text, PersonSrcInfoPath, true);
                            File.Move(PersonSrcInfoPath, PersonSrcInfo + infoName);
                        }
                    }

                    //************************************************************************
                    Console.WriteLine(@"************************************************************************");
                    Console.WriteLine(listBox1.Items.Count);
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        photoName.Add(textBox6.Text + @"_" + textBox3.Text + @"," + PersonLevel + @"," + (i + 1) + (listBox1.Items[i].ToString().LastIndexOf(".jpg") != -1 ? ".jpg" : ".png"));
                        Console.WriteLine(listBox1.Items[i]);
                        Console.WriteLine(photoName[i]);
                    }

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (i < PersonSrcPhotoPathList.Count)
                        {
                            if (PersonSrcPhotoPathList[i] == listBox1.Items[i].ToString())
                            {
                                File.Move(listBox1.Items[i].ToString(), PersonSrcPhoto + photoName[i]);//生活照 文件绝对路径(旧)，文件绝对路径(新)
                                Console.WriteLine(@"==");
                                Console.WriteLine(@"旧：" + listBox1.Items[i]);
                                Console.WriteLine(@"新：" + PersonSrcPhoto + photoName[i]);
                            }
                        }
                        else
                        {
                            File.Copy(listBox1.Items[i].ToString(), PersonSrcPhoto + photoName[i], true);//生活照 文件绝对路径(旧)，文件绝对路径(新)
                            Console.WriteLine(@"i >");
                            Console.WriteLine(@"旧：" + listBox1.Items[i]);
                            Console.WriteLine(@"新：" + PersonSrcPhoto + photoName[i]);
                        }

                    }


                    Console.WriteLine(@"@@@@@@@" + photoName.Count);


                    Console.WriteLine(PersonSrcImg + imgName);
                    Console.WriteLine(PersonSrcInfo + infoName);
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        Console.WriteLine(PersonSrcPhoto + photoName[i]);
                    }
                    System.Windows.Forms.MessageBox.Show(@"更新完成！");

                    dataGridView1.DataSource = null;
                    pictureBox1.Image = null;
                    panel1.Visible = true;
                    button23.Enabled = true;//刷新按钮禁用
                }
            }
        }

        /// <summary>
        /// 加载到表格输出
        /// </summary>
        /// <param name="_pic">暂存读取的文件数据</param>
        /// <param name="List">存储文件夹地址的泛型数组</param>
        public void LoadFun(ref List<Person> _pic, ref List<string> List) {
            _pic.Clear();
            _pic = GetFileName(List);
            Console.WriteLine(@"_pic 人数：" + _pic.Count);
            if (_pic.Count > 0)
            {
                OutPutGridView(_pic);
            }
            else
            {
                Console.WriteLine(@"_pic 数量：" + _pic.Count);

                PersonSrcImg = List[0];                       //传入的 缩略图存放 路径
                PersonSrcInfo = List[1];                      //传入的 简介图存放 路径
                PersonSrcPhoto = List[2];                //传入的 生活照存放 路径
                Console.WriteLine(@"缩略图存放 路径：" + PersonSrcImg);
                Console.WriteLine(@"简介图存放 路径：" + PersonSrcInfo);
                Console.WriteLine(@"生活照存放 路径：" + PersonSrcPhoto);
                OutPutGridView(null);
            }
        }

        private void button23_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            button23.Enabled = false;//刷新按钮禁用
            if (PersonSrcImg.IndexOf("Array_1") != -1 && PersonSrcImg.IndexOf("IMG_1") != -1)
            {
                //市场一,90
                LoadFun(ref _pic11, ref AList1);//加载人员资料输出到表格
            }
            else if (PersonSrcImg.IndexOf("Array_1") != -1 && PersonSrcImg.IndexOf("IMG_2") != -1)
            {
                //市场一,80
                LoadFun(ref _pic12, ref AList2);//加载人员资料输出到表格
            }
            else if (PersonSrcImg.IndexOf("Array_1") != -1 && PersonSrcImg.IndexOf("IMG_3") != -1)
            {
                //市场一,70
                LoadFun(ref _pic13, ref AList3);//加载人员资料输出到表格
            }

            else if (PersonSrcImg.IndexOf("Array_2") != -1 && PersonSrcImg.IndexOf("IMG_1") != -1)
            {
                //市场二,90
                LoadFun(ref _pic21, ref BList1);//加载人员资料输出到表格
            }
            else if (PersonSrcImg.IndexOf("Array_2") != -1 && PersonSrcImg.IndexOf("IMG_2") != -1)
            {
                //市场二,80
                LoadFun(ref _pic22, ref BList2);//加载人员资料输出到表格
            }
            else if (PersonSrcImg.IndexOf("Array_2") != -1 && PersonSrcImg.IndexOf("IMG_3") != -1)
            {
                //市场二,70
                LoadFun(ref _pic23, ref BList3);//加载人员资料输出到表格
            }

            else if (PersonSrcImg.IndexOf("Array_3") != -1 && PersonSrcImg.IndexOf("IMG_1") != -1)
            {
                //市场三,90
                LoadFun(ref _pic31, ref CList1);//加载人员资料输出到表格
            }
            else if (PersonSrcImg.IndexOf("Array_3") != -1 && PersonSrcImg.IndexOf("IMG_2") != -1)
            {
                //市场三,80
                LoadFun(ref _pic32, ref CList2);//加载人员资料输出到表格
            }
            else if (PersonSrcImg.IndexOf("Array_3") != -1 && PersonSrcImg.IndexOf("IMG_3") != -1)
            {
                //市场三,70
                LoadFun(ref _pic33, ref CList3);//加载人员资料输出到表格
            }

            else if (PersonSrcImg.IndexOf("Array_4") != -1 && PersonSrcImg.IndexOf("IMG_1") != -1)
            {
                //市场四,90
                LoadFun(ref _pic41, ref DList1);//加载人员资料输出到表格
            }
            else if (PersonSrcImg.IndexOf("Array_4") != -1 && PersonSrcImg.IndexOf("IMG_2") != -1)
            {
                //市场四,80
                LoadFun(ref _pic42, ref DList2);//加载人员资料输出到表格
            }
            else if (PersonSrcImg.IndexOf("Array_4") != -1 && PersonSrcImg.IndexOf("IMG_3") != -1)
            {
                //市场四,70
                LoadFun(ref _pic43, ref DList3);//加载人员资料输出到表格
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e) {

            if (Upload) //上传
            {

            }
            else
            {
                Console.WriteLine(listBox1.SelectedIndex);
                Console.WriteLine(@"表格人员总数量：" + PhotoLists.Count);
                Console.WriteLine(@"该人生活照数量：" + PersonSrcPhotoPathList.Count);
                if (Num != -1) //人员所在的序号
                {
                    for (int j = 0; j < PersonSrcPhotoPathList.Count; j++)
                    {
                        Console.WriteLine(PersonSrcPhotoPathList[j]);
                        Console.WriteLine(listBox1.Items.Count);
                    }
                }

            }
        }

        /// <summary>
        /// 生活照 添加按钮
        /// </summary>
        private void button19_Click(object sender, EventArgs e) {
            //ListPhoto
            openFileDialog3.Multiselect = false;
            openFileDialog3.InitialDirectory = ".";
            openFileDialog3.FileName = "生活照图片文件";
            openFileDialog3.Title = @"请选择 生活照 图片文件  所需图片分辨率：1920px(宽) - 1080px(高)";
            openFileDialog3.Filter = @"生活照文件(*.jpg,*.png)|*.jpg;*.png";

            if (Upload) //上传
            {
                if (openFileDialog3.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string file = openFileDialog3.FileName;
                        Image image7 = Image.FromFile(file);
                        
                        if (image7.Width == 1920 && image7.Height == 1080)
                        {
                            ListPhoto.Add(file);
                            image7.Dispose();
                        }
                        else
                        {
                            MessageBox.Show(@"所需图片分辨率：1920px * 1080px ；图片尺寸不匹配，请重新选择", @"生活照图片文件", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            image7.Dispose();
                        }
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show(@"错误");
                    }
                }
                listBox1.Items.Clear();
                for (int i = 0; i < ListPhoto.Count; i++)
                {
                    listBox1.Items.Add(ListPhoto[i]);
                }
            }
            else
            {
                if (openFileDialog3.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string file = openFileDialog3.FileName;
                        ListPhoto.Add(file);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show(@"错误");
                    }
                }
                listBox1.Items.Clear();
                for (int i = 0; i < ListPhoto.Count; i++)
                {
                    listBox1.Items.Add(ListPhoto[i]);
                }
            }
        }
        /// <summary>
        /// 生活照 删除按钮
        /// </summary>
        private void button20_Click(object sender, EventArgs e) {
            if (Upload) //上传
            {
                ListPhoto.Clear();
                listBox1.Items.Clear();
                for (int i = 0; i < ListPhoto.Count; i++)
                {
                    listBox1.Items.Add(ListPhoto[i]);
                }
            }
            else
            {
                DialogResult delListPhoto = MessageBox.Show(@"确定删除已有的生活照？", @"警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (delListPhoto == DialogResult.Yes)
                {
                    for (int i = 0; i < PersonSrcPhotoPathList.Count; i++)
                    {
                        if (PersonSrcPhotoPathList[i] == listBox1.Items[i].ToString())
                        {
                            File.Delete(PersonSrcPhotoPathList[i]);
                        }
                    }
                    PersonSrcPhotoPathList.Clear();
                    ListPhoto.Clear();
                    listBox1.Items.Clear();
                    for (int i = 0; i < ListPhoto.Count; i++)
                    {
                        listBox1.Items.Add(ListPhoto[i]);
                    }
                }

            }
        }

        private void button24_Click(object sender, EventArgs e) {
            if (BtnClick) return; //传输视频时，按钮失效
            for (int i = 0; i < PList.Count; i++)
            {
                Console.WriteLine(PList[i]);
            }

            Video videoSet = new Video();
            DialogResult videoResult = videoSet.ShowDialog();

            openFileDialog4.Multiselect = false;
            openFileDialog4.InitialDirectory = ".";
            openFileDialog4.Filter = @"视频文件(*.mp4)|*.mp4";

            if (videoResult == DialogResult.OK)
            {
                openFileDialog4.FileName = "视频文件";
                openFileDialog4.Title = @"请选择 左屏待机 视频文件";

                if (openFileDialog4.ShowDialog() == DialogResult.OK)
                {
                    string tmp = openFileDialog4.FileName;
                    Console.WriteLine(tmp);
                    //File.Copy(tmp, PList[0],true);
                    Sourcefile = "";//源文件
                    Targetfile = "";//目标文件
                    Sourcefile = tmp;
                    Targetfile = PList[0];
                    Run();
                    System.Windows.Forms.MessageBox.Show(@"正在传输……");
                }
            }
            else if (videoResult == DialogResult.Yes)
            {
                openFileDialog4.FileName = "视频文件";
                openFileDialog4.Title = @"请选择 右屏待机 视频文件";

                if (openFileDialog4.ShowDialog() == DialogResult.OK)
                {
                    string tmp = openFileDialog4.FileName;
                    Console.WriteLine(tmp);
                    //File.Copy(tmp,PList[1], true);
                    Sourcefile = "";//源文件
                    Targetfile = "";//目标文件
                    Sourcefile = tmp;
                    Targetfile = PList[1];
                    Run();
                    System.Windows.Forms.MessageBox.Show(@"正在传输……");
                }
            }
            else if (videoResult == DialogResult.Cancel)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (progressmax != 0)
            {
                progressBar1.Maximum = progressmax;
                progressBar1.Value = progressvalue;
                progressBar1.Visible = true;
            }
            else
            {
                progressBar1.Visible = false;
            }
        }
        
    }
}
