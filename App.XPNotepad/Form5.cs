using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace notepad
{
    public partial class Form5 : Form
    {
        Form1 mainform;
        public int b = 0;
        
        public Form5()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            b = 3;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainform.Text == "无标题 - 记事本")
                {
                    mainform.saveFileDialog1.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
                    mainform.saveFileDialog1.FileName = "*.txt";
                    if (mainform.saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        b = 3;
                    else
                    {
                        b = 1;
                        StreamWriter save = new StreamWriter(mainform.saveFileDialog1.FileName, false, System.Text.Encoding.Default);
                        save.Write(mainform.mtBox1.Text);
                        save.Flush();
                        save.Close();
                        mainform.ts = mainform.mtBox1.Text;
                        if (mainform.exe == 0)
                            mainform.Text = Path.GetFileName(mainform.saveFileDialog1.FileName) + " - 记事本";
                        else
                            mainform.Text = Path.GetFileNameWithoutExtension(mainform.saveFileDialog1.FileName) + " - 记事本";
                        mainform.n = 1;
                    }
                }
                else
                {
                    b = 1;
                    StreamWriter save = new StreamWriter(mainform.saveFileDialog1.FileName, false, System.Text.Encoding.Default);
                    save.Write(mainform.mtBox1.Text);
                    save.Flush();
                    save.Close();
                    mainform.ts = mainform.mtBox1.Text;
                }
            }
            catch { }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b = 2;
            Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            mainform = (Form1)this.Owner;
            if (mainform.Text == "无标题 - 记事本")
            {
                label2.Text = "无标题?";
            }
            else
            {
                this.Size = new Size(354, 150);
                panel1.Size = new Size(348,78);
                button1.Location = new Point(button1.Location.X, button1.Location.Y + 20);
                button2.Location = new Point(button2.Location.X, button2.Location.Y + 20);
                button3.Location = new Point(button3.Location.X, button3.Location.Y + 20);
                label2.Location = new Point(label1.Location.X, label1.Location.Y + 20);
                if (mainform.saveFileDialog1.FileName != "")
                    label2.Text = mainform.saveFileDialog1.FileName + "?";
                else label2.Text = mainform.openFileDialog1.FileName + "?";
            }
        }
    }
}
