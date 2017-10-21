using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCApp {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(569, 487);

            Interface();    //界面初始化显示方法
            BtnAry();       // 四大市场子按钮组——隐藏

        }

        /// <summary>
        /// 界面初始化显示方法
        /// </summary>
        private void Interface() {
            this.groupBox2.Location = this.groupBox3.Location = this.groupBox4.Location = this.groupBox1.Location;
            this.pictureBox1.Location = new System.Drawing.Point(this.button4.Location.X + this.button4.Width - this.pictureBox1.Width, this.groupBox1.Location.Y);

            GridView(false);
            picture(false);

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

        private void button1_Click(object sender, EventArgs e) {
            BtnAry();       //四大市场子按钮组——隐藏
            BtnArySC();     //四大市场按钮——启用
            ChildBtnAry();  // 四大市场子按钮组——启用选项
            GridView(false);// 表格 隐藏
            picture(false); //主界面缩略图 隐藏
            this.groupBox1.Visible = true;
            this.button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e) {
            BtnAry();
            BtnArySC();
            ChildBtnAry();
            
            this.groupBox2.Visible = true;
            this.button2.Enabled = false;

            GridView(false);
            picture(false);
        }

        private void button3_Click(object sender, EventArgs e) {
            BtnAry();
            BtnArySC();
            ChildBtnAry();

            this.groupBox3.Visible = true;
            this.button3.Enabled = false;

            GridView(false);
            picture(false);
        }

        private void button4_Click(object sender, EventArgs e) {
            BtnAry();
            BtnArySC();
            ChildBtnAry();

            this.groupBox4.Visible = true;
            this.button4.Enabled = false;

            GridView(false);
            picture(false);
        }

        private void button5_Click(object sender, EventArgs e) {
            ChildBtnAry();  // 四大市场子按钮组——启用选项
            GridView(true);// 表格 隐藏
            picture(true);
            button5.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            button6.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            button7.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            this.button8.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            this.button9.Enabled = false;
        }

        private void button10_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            this.button10.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            this.button11.Enabled = false;
        }

        private void button12_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            this.button12.Enabled = false;
        }

        private void button13_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            this.button13.Enabled = false;
        }

        private void button14_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            this.button14.Enabled = false;
        }

        private void button15_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            this.button15.Enabled = false;
        }

        private void button16_Click(object sender, EventArgs e) {
            ChildBtnAry();
            GridView(true);
            picture(true);
            this.button16.Enabled = false;
        }

        private void button18_Click(object sender, EventArgs e) {
            System.Environment.Exit(0);
        }
    }
}
