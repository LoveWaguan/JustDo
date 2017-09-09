using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NotePad
{
    public partial class Find : Form
    {
        public Find()
        {
            InitializeComponent();
        }
        public delegate void myDel(bool UandL,string direction,string searchText);//传递参数为：是否大小写及查找方向
        public event myDel myEvent;
 
        /// <summary>
        /// 查找按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) { myEvent(checkBox1.Checked, radioButton1.Text,textBox1.Text); }
            else { myEvent(checkBox1.Checked, radioButton2.Text,textBox1.Text); }
         }
        //判断textBox1是否有内容
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            { button1.Enabled = false; }//不可用状态
            else
            { button1.Enabled = true; }
        }
        //加载窗体
        private void Find_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;//不可用状态
            radioButton1.Checked = true;
        }
        //关闭窗体
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
