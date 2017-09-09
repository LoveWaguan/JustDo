using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NotePad
{
    public partial class MyNotePad : Form
    {
        private string FileTextPath = string.Empty;//定义文本的路径
        private int indexDo = 0;//设置向下初始索引位置为0
        private int indexUp = 0;//设置向上查找的初始位置
        private bool flag = true;//设置标记
        private int index = 0;//设置替换时查找下一个的index值为0

         public MyNotePad()
        {
            InitializeComponent();
         }
         //保存文件的对话框显示方法
        private void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件(*.txt)|*.txt|富文本文件(*.rft)|*.rtf";
            sfd.Title = "保存";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            sfd.FileName = MyNotePad.ActiveForm.Text + ".txt";
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                FileTextPath = sfd.FileName.ToString();//获取文件路径
                if (File.Exists(FileTextPath))//如果文件已存在，则删除
                {
                    File.Delete(FileTextPath);
                }
                 if ((System.IO.Path.GetExtension(sfd.FileName)).ToLower() == ".txt")
                    richTextBox1.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
                else { richTextBox1.SaveFile(sfd.FileName); }

                 this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length;//设置光标位置
                 MyNotePad.ActiveForm.Text = Path.GetFileName(sfd.FileName) + "- 记事本";
             }
            else if (dr == DialogResult.Cancel)
            {
                flag=false ; 
            }
          }
        private void OpenFile() 
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件(*.txt)|*.txt|富文本文件(*.rft)|*.rtf";
            ofd.Title = "打开";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if ((System.IO.Path.GetExtension(ofd.FileName)).ToLower() == ".txt")
                    richTextBox1.LoadFile(ofd.FileName, RichTextBoxStreamType.PlainText);
                else { richTextBox1.LoadFile(ofd.FileName); }

                FileTextPath = ofd.FileName.ToString();//获取当前打开文本的全路径，需要打开成功
                this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length;//设置光标位置
                MyNotePad.ActiveForm.Text = Path.GetFileName(ofd.FileName)+"- 记事本";
            }
        }
        //若有改变重新写入
        private void ReWrite()
        {
            if ((System.IO.Path.GetExtension(FileTextPath)).ToLower() == ".txt")
                richTextBox1.SaveFile(FileTextPath, RichTextBoxStreamType.PlainText);
            else { richTextBox1.SaveFile(FileTextPath); }
          }
         //新建文本
         private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!richTextBox1.Modified && richTextBox1.Text != string.Empty)
            {
                DialogResult dr = MessageBox.Show("是否保存该文本？", "保存", MessageBoxButtons.YesNoCancel);
                if (dr.Equals(DialogResult.Yes))
                {
                    if (FileTextPath == string.Empty)//文件路径空
                        SaveFile(); //弹出文件保存对话框
                    else
                    {
                        ReWrite();//重写
                        richTextBox1.Clear();//清除当前文本
                        MyNotePad.ActiveForm.Text = "无标题 - 记事本";//默认显示的窗体名称
                    }
                }
                else if (dr.Equals(DialogResult.No))
                {
                    richTextBox1.Clear();//清除当前文本
                    MyNotePad.ActiveForm.Text = "无标题 - 记事本";//默认显示的窗体名称
                }
            }
            else
            {
                richTextBox1.Clear();
                MyNotePad.ActiveForm.Text = "无标题 - 记事本";//默认显示的窗体名称
            }
            FileTextPath = string.Empty;//清空路径
           }
        //打开文本文件
         private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if (richTextBox1.Text != string.Empty && !richTextBox1.Modified && FileTextPath!=string.Empty)//如果不空且文本改变
             {
                 DialogResult dr = MessageBox.Show("是否将更改保存到" + FileTextPath, "记事本", MessageBoxButtons.YesNoCancel);
                 if (dr.Equals(DialogResult.Yes))//点击保存
                 {
                     if(FileTextPath==string.Empty)
                        SaveFile();//保存文件
                     OpenFile();//打开
                   }
                 else if (dr.Equals(DialogResult.No))
                 {
                     OpenFile();
                 }
             }
             else //文本未改变
             { OpenFile();  }
            }
        //保存
         private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if (!richTextBox1.Modified && richTextBox1.Text != string.Empty)
             {
                 if (FileTextPath == string.Empty)//文件路径空
                 {
                     SaveFile(); //弹出文件保存对话框
                     if (flag)
                     {
                         if ((System.IO.Path.GetExtension(FileTextPath)).ToLower() == ".txt")
                             richTextBox1.LoadFile(FileTextPath, RichTextBoxStreamType.PlainText);
                         else { richTextBox1.LoadFile(FileTextPath); }
                     }
                 }
                 else
                     ReWrite();//重写
                richTextBox1.Modified = true ;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
             }
          }
        //另存为，需要打开保存对话框SaveFileDialog
         private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             SaveFile();//保存对话框
             //加载文件
             if (flag)
             {
                 if ((System.IO.Path.GetExtension(FileTextPath)).ToLower() == ".txt")
                     richTextBox1.LoadFile(FileTextPath, RichTextBoxStreamType.PlainText);
                 else { richTextBox1.LoadFile(FileTextPath); }
                 richTextBox1.SelectionStart = richTextBox1.Text.Length;//设置光标位置
             }
         }
        //关闭文本文件
         private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if (!richTextBox1.Modified && richTextBox1.Text != string.Empty)
             {
                 DialogResult dr = MessageBox.Show("是否将更改保存到" + FileTextPath, "记事本", MessageBoxButtons.YesNoCancel);
                 if (dr.Equals(DialogResult.Yes))
                 {
                     if (FileTextPath == string.Empty)
                         SaveFile();
                     else
                         ReWrite();
                     Application.ExitThread();
                 }
                 else if (dr.Equals(DialogResult.No))
                 {
                     Application.ExitThread();
                 }
             }
             else
             {
                 Application.ExitThread();
             }
          }
         //关闭窗体时
         private void MyNotePad_FormClosing(object sender, FormClosingEventArgs e)
         {
             if (!richTextBox1.Modified && richTextBox1.Text!=string.Empty)
             {
                 DialogResult dr = MessageBox.Show("是否将更改保存到" + FileTextPath, "记事本", MessageBoxButtons.YesNoCancel);
                 if (dr.Equals(DialogResult.Yes))
                 {
                     if (FileTextPath == string.Empty)
                         SaveFile();
                     else
                         ReWrite();
                     Application.ExitThread();
                 }
                 else if (dr.Equals(DialogResult.Cancel))
                 { e.Cancel = true; }
                 else if (dr.Equals(DialogResult.No))
                 {
                     Application.ExitThread();
                 }
             }
         }
        //全选
         private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             richTextBox1.SelectAll();
         }
        //复制
         private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             richTextBox1.Copy();
         }
        //剪切
         private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             richTextBox1.Cut();
         }
        //粘贴
         private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             richTextBox1.Paste();
         }
        //撤销
         private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             richTextBox1.Undo();
         }
         //重做
         private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             richTextBox1.Redo();
         }
        //删除
         private void 删除ToolStripMenuItem_Click(object sender, EventArgs e) 
         {
             string oldStr = richTextBox1.Text;
             richTextBox1.Text = oldStr.Remove(richTextBox1.SelectionStart, richTextBox1.SelectedText.Length);
         }
         //是否换行
         private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             richTextBox1.WordWrap = 自动换行ToolStripMenuItem.CheckState == CheckState.Checked ? true : false;
         }
         //改变字体大小
         private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             FontDialog fd = new FontDialog();
             fd.ShowApply = true;
             fd.ShowEffects = true;
             fd.ShowHelp = true;
             fd.ShowDialog();
             richTextBox1.Font = fd.Font;
           }
         //右击事件
         private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
         {
             if (e.Button == MouseButtons.Right)
             {
                 contextMenuStrip1.Show(System.Windows.Forms.Form.MousePosition);
             }
         }
        //查找
         private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             indexUp = richTextBox1.Text.Length  - richTextBox1.SelectionStart;
             indexDo = richTextBox1.SelectionStart;
             Find find = new Find();
             find.myEvent += new Find.myDel(FindText);
             find.Show();
           }
        //查找函数，委托使用
         private void FindText(bool UandL, string direction,string searchText)
         {
             //是否区别大小写
              string TranStr = string.Empty;
              if (UandL == true)
                  TranStr = searchText.ToUpper();
              else 
                  TranStr = searchText;
             //提前判断是否包含此文本内容
              if (!richTextBox1.Text.Contains(TranStr))
              { MessageBox.Show("找不到\"" + searchText + "\"", "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information); }
              else
              {
                  if (direction == "向下") {  SearchDown(TranStr, searchText);}
                  else { SearchUp(TranStr, searchText); }
              }
            }
        //文件的路径被改变
         private void FilePathChanged()
         {
             indexDo = 0;//重新设置index的初值
             indexUp = 0;//
             index = 0;
         }
        //向下查找
         private void SearchDown(string TranStr,string searchText)
         {
             indexDo = richTextBox1.Text.IndexOf(TranStr, indexDo);
             if (indexDo == -1)
             {
                 MessageBox.Show("找不到\"" + searchText + "\"", "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  indexDo = richTextBox1.Text.LastIndexOf(TranStr)+1;
             }
             else
             {
                 richTextBox1.Focus();
                 richTextBox1.Select(indexDo, TranStr.Length);
                 indexDo++;
             }
          }
          //向上查找
         private void SearchUp(string TranStr, string searchText)
         {
             string newStr = GetText();
             indexUp = newStr.IndexOf(TranStr, indexUp);
             if (indexUp == -1)
             {
                 MessageBox.Show("找不到\"" + searchText + "\"", "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  indexUp = richTextBox1.Text.LastIndexOf(TranStr)+1;
             }
             else
             {
                 richTextBox1.Focus();
                 richTextBox1.Select(richTextBox1.Text.Length  - indexUp-1, TranStr.Length);
                 indexUp++;
             }
         }
         //获取richtextbox的反向文本
         private string GetText()
         {
             char[] charText = richTextBox1.Text.ToCharArray();
             char[] NewcharText = new char[charText.Length];
             int j = 0;
             for (int i = charText.Length-1; i >-1 ; i--)
             {
                 NewcharText[j] = charText[i];
                 j++;
             }
             string newStr = new string(NewcharText);
             return newStr;
         }
         //获取当前光标位置
          private void richTextBox1_SelectionChanged(object sender, EventArgs e)
         {
             indexDo = richTextBox1.SelectionStart;//获取当前的光标位置，转换成索引下标
             indexUp = richTextBox1.Text.Length  - richTextBox1.SelectionStart-1;
         }
          //替换[Replacec窗体]
          private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
          {
              index = richTextBox1.SelectionStart;
              Replace replace = new Replace();
              replace.myEvent += new Replace.Mydel(replace_myEvent);
              replace.myEventR += new Replace.MydelReplace(replace_myEventR);
              replace.myEventRA += new Replace.MydelReplaceAll(replace_myEventRA);
              replace.myEventC += new Replace.MydelClose(replace_myEventC);
              replace.Show();
          }
        //关闭替换窗体
          private void replace_myEventC(bool flag, string searchText, bool check)
          {
              if (flag)
                  replace_myEvent(searchText, check);
          }
        //替换全部
          void replace_myEventRA(string destText,string oldText)
          {
              string oldStr = richTextBox1.Text;
              richTextBox1.Text = oldStr.Replace(oldText, destText);
          }
        //替换一个
         private void replace_myEventR(string destText)
          {    
               string str= richTextBox1.Text;
               string firstStr=str.Substring(0,richTextBox1.SelectionStart);
               string secondStr = str.Substring(richTextBox1.SelectionStart + richTextBox1.SelectedText.Length, richTextBox1.Text.Length - richTextBox1.SelectionStart - richTextBox1.SelectedText.Length);
               richTextBox1.Text = firstStr + destText + secondStr;
              }
        //查找下一个[替换中的查找]
         private void replace_myEvent(string searchText, bool check)
          {
              //是否区别大小写
              string TranStr = string.Empty;
              if (check == true)
                  TranStr = searchText.ToUpper();
              else
                  TranStr = searchText;

              index = richTextBox1.Text.IndexOf(TranStr, index);
                  if (index == -1)
                {
                    MessageBox.Show("找不到\"" + searchText + "\"", "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    index = richTextBox1.Text.LastIndexOf(TranStr) + 1;
                }
                else
                {
                    richTextBox1.Focus();
                    richTextBox1.Select(index, TranStr.Length);
                    index++;
                }
           }
           //文本改变时，设置文本是否改变的bool值为false
          private void richTextBox1_TextChanged(object sender, EventArgs e)
          {
              richTextBox1.Modified = false;
          }
          //设置颜色
          private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
          {
              ColorDialog cd = new ColorDialog();
              cd.ShowHelp=true;
              cd.SolidColorOnly = true;
              cd.ShowDialog();
              richTextBox1.ForeColor = cd.Color;
          }
         //关于
          private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
          {
              MessageBox.Show("本文本编辑器由 云海逍遥生 先生 编写，仅供使用！严禁盗版！","关于",MessageBoxButtons.OK);
          }
        //鼠标按下时获取光标位置[替换中的查找]
          private void richTextBox1_MouseDown_1(object sender, MouseEventArgs e)
          {
              index = richTextBox1.SelectionStart;
          }
          }
}
