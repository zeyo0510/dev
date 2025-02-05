using System;
using System.Windows.Forms;
using App.Windows.XPNotepad.Main;

namespace notepad
{
    public partial class Form3 : Form
    {
        MainForm mainform;
        string mr, tt;
        static string fl = "";
        static string fr = "";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            mainform = (MainForm)this.Owner;
            if (mainform.cu != "")
                textBox1.Text = mainform.cu;
            if (mainform.cu == "")
                textBox2.Text = "";
            else textBox2.Text = fr;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mainform.ffind = true;
                mainform.tBox2 = textBox1.Text;
                if (mainform.mtBox1.SelectedText == mainform.mtBox1.Text)
                    mainform.mtBox1.Select(0, 0);
                fl = textBox1.Text;
                fr = textBox2.Text;
                if (checkBox1.Checked)
                {
                    mr = mainform.mtBox1.Text;
                    tt = textBox1.Text;
                    mainform.f2checkbox = true;
                }
                else
                {
                    mr = mainform.mtBox1.Text.ToLower();
                    tt = textBox1.Text.ToLower();
                    mainform.f2checkbox = false;
                }
                mainform.ffl = 1;
                if (mr.IndexOf(tt, mainform.mtBox1.SelectionStart + mainform.mtBox1.SelectedText.Length) >= 0)
                {
                    mainform.mtBox1.Select(mr.IndexOf(tt, mainform.mtBox1.SelectionStart + mainform.mtBox1.SelectedText.Length), textBox1.Text.Length);
                    mainform.mtBox1.ScrollToCaret();//当屏幕显示不了时，实现滚动
                }
                else MessageBox.Show(("找不到\"" + textBox1.Text + "\""), "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                mainform.ffind = true;
                mainform.tBox2 = textBox1.Text;
                if (mainform.mtBox1.SelectedText == mainform.mtBox1.Text)
                    mainform.mtBox1.Select(0, 0);
                fl = textBox1.Text;
                fr = textBox2.Text;
                if (checkBox1.Checked)
                {
                    mr = mainform.mtBox1.Text;
                    tt = textBox1.Text;
                    mainform.f2checkbox = true;
                }
                else
                {
                    mr = mainform.mtBox1.Text.ToLower();
                    tt = textBox1.Text.ToLower();
                    mainform.f2checkbox = false;
                }
                if (mainform.mtBox1.SelectedText == tt)
                    mainform.mtBox1.SelectedText = textBox2.Text;
                mainform.ffl = 1;
                if (mr.IndexOf(tt, mainform.mtBox1.SelectionStart + mainform.mtBox1.SelectedText.Length) >= 0)
                {
                    mainform.mtBox1.Select(mr.IndexOf(tt, mainform.mtBox1.SelectionStart + mainform.mtBox1.SelectedText.Length), textBox1.Text.Length);
                    mainform.mtBox1.ScrollToCaret();//当屏幕显示不了时，实现滚动
                }
                else MessageBox.Show(("找不到\"" + textBox1.Text + "\""), "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                mainform.ffind = true;
                mainform.tBox2 = textBox1.Text;
                mainform.mtBox1.SelectionStart = 0;
                mainform.mtBox1.Select(0, 0);
                fl = textBox1.Text;
                fr = textBox2.Text;
                if (checkBox1.Checked)
                {
                    mr = mainform.mtBox1.Text;
                    tt = textBox1.Text;
                    mainform.f2checkbox = true;
                }
                else
                {
                    mr = mainform.mtBox1.Text.ToLower();
                    tt = textBox1.Text.ToLower();
                    mainform.f2checkbox = false;
                }
                mainform.mtBox1.SelectionStart = 0;
                mainform.ffl = 1;
                while (mr.IndexOf(tt, mainform.mtBox1.SelectionStart + mainform.mtBox1.SelectedText.Length) >= 0)
                {
                    mainform.mtBox1.Select(mr.IndexOf(tt, mainform.mtBox1.SelectionStart + mainform.mtBox1.SelectedText.Length), textBox1.Text.Length);
                    mainform.mtBox1.ScrollToCaret();//当屏幕显示不了时，实现滚动
                    mainform.mtBox1.SelectedText = textBox2.Text;
                    if (checkBox1.Checked)
                    {
                        mr = mainform.mtBox1.Text;
                        tt = textBox1.Text;
                    }
                    else
                    {
                        mr = mainform.mtBox1.Text.ToLower();
                        tt = textBox1.Text.ToLower();
                    }
                    mainform.mtBox1.SelectionStart = 0;
                }
                mainform.mtBox1.SelectionStart = 0;
            }
            catch { }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainform.replace = 0;
            mainform.cu = textBox1.Text;
        }
    }
}
