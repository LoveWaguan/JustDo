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
    public partial class Replace : Form
    {
        public Replace()
        {
            InitializeComponent();
        }
 
        public delegate void Mydel(string searchText,bool check);
        public delegate void MydelReplace(string destText);
        public delegate void MydelReplaceAll(string destText,string oldText);
        public delegate void MydelClose(bool flag, string searchText, bool check);
        public event Mydel myEvent;
        public event MydelReplace myEventR;
        public event MydelReplaceAll myEventRA;
        public event MydelClose myEventC;
        //文本框文本改变时
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
         }
        //加载窗体，设置三个按钮enable为false
        private void Replace_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }
        //退出
        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            if (checkBox1.Checked)
              myEventC(true,textBox1.Text,true);
            else
                myEventC(true, textBox1.Text, false);
        }
        //替换全部
        private void button3_Click(object sender, EventArgs e)
        {
            myEventRA(textBox2.Text,textBox1.Text);
        }
        //替换
        private void button2_Click(object sender, EventArgs e)
        {
            myEventR(textBox2.Text);
        }
        //查找下一个
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                myEvent(textBox1.Text,true);
            }
            else
            {
                myEvent(textBox1.Text,false);
            }
        }
     }
}
