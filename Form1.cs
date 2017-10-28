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
            ReadXmlData(@"C:\Users\Max-PC\Source\Repos\LCApp\Data.xml");
        }

        private void ReadXmlData(string str) {
            XmlTextReader reader = new XmlTextReader(str);
            List<XmlModel> xmlModelList = new List<XmlModel>();
            XmlModel xmlModel = new XmlModel();
            while (reader.Read())
            {
                if (reader.NodeType==XmlNodeType.Element)
                {
                    if (reader.Name=="name1")
                    {
                        this.listBox2.Items.Add(reader.GetAttribute(0));
                        this.listBox2.Items.Add(reader.GetAttribute(1));
                        this.listBox2.Items.Add(reader.GetAttribute(2));
                        this.listBox2.Items.Add(reader.GetAttribute(3));
                        this.listBox2.Items.Add(reader.GetAttribute(4));
                        this.listBox2.Items.Add(reader.GetAttribute(5));
                        this.listBox2.Items.Add(reader.GetAttribute(6));
                        this.listBox2.Items.Add(reader.GetAttribute(7));
                        this.listBox2.Items.Add(reader.GetAttribute(8));
                        /*
                        xmlModel.ImgSrc90 = reader.GetAttribute(0);
                        xmlModel.ImgSrc80 = reader.GetAttribute(1);
                        xmlModel.ImgSrc70 = reader.GetAttribute(2);
                        xmlModel.ImgInfoSrc90 = reader.GetAttribute(3);
                        xmlModel.ImgInfoSrc80 = reader.GetAttribute(4);
                        xmlModel.ImgInfoSrc70 = reader.GetAttribute(5);
                        xmlModel.ImgPhotoSrc90 = reader.GetAttribute(6);
                        xmlModel.ImgPhotoSrc80 = reader.GetAttribute(7);
                        xmlModel.ImgPhotoSrc70 = reader.GetAttribute(8);
                        
                        this.listBox2.Items.Add(xmlModel.ImgSrc90);
                        this.listBox2.Items.Add(xmlModel.ImgSrc80);
                        this.listBox2.Items.Add(xmlModel.ImgSrc70);
                        this.listBox2.Items.Add(xmlModel.ImgInfoSrc90);
                        this.listBox2.Items.Add(xmlModel.ImgInfoSrc80);
                        this.listBox2.Items.Add(xmlModel.ImgInfoSrc70);
                        this.listBox2.Items.Add(xmlModel.ImgPhotoSrc90);
                        this.listBox2.Items.Add(xmlModel.ImgPhotoSrc80);
                        this.listBox2.Items.Add(xmlModel.ImgPhotoSrc70);*/
                    }
                }

                if (reader.NodeType==XmlNodeType.EndElement)
                {
                    xmlModelList.Add(xmlModel);
                }
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
            picture(true);
            Add_Exit_Btn(true);
            button5.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            button6.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            button7.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            this.button8.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            this.button9.Enabled = false;
        }

        private void button10_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            this.button10.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            this.button11.Enabled = false;
        }

        private void button12_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            this.button12.Enabled = false;
        }

        private void button13_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            this.button13.Enabled = false;
        }

        private void button14_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            this.button14.Enabled = false;
        }

        private void button15_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            this.button15.Enabled = false;
        }

        private void button16_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            Add_Exit_Btn(true);
            this.button16.Enabled = false;
        }

        private void button18_Click(object sender, EventArgs e) {
            System.Environment.Exit(0);
        }

        private void textBox1_Click(object sender, EventArgs e) {
            this.openFileDialog1.Multiselect = false;
            this.openFileDialog1.InitialDirectory = ".";
            this.openFileDialog1.Title = "请选择缩略图文件";
            this.openFileDialog1.Filter = "缩略图文件(*.jpg,*.png)|*.jpg;*.png";
            this.openFileDialog1.ShowDialog();
            if(openFileDialog1.FileName != string.Empty)
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
    }
}
