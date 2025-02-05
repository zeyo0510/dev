using System;
using System.Windows.Forms;
using App.Windows.XPNotepad.Main;

namespace notepad
{
    public partial class Form2 : Form
    {
        MainForm mainform;
        string mr, tt;
        static string fl = "";

        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainform.find = 0;
            mainform.cu = textBox1.Text;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            mainform = (MainForm)this.Owner;
            if (mainform.cu != "")
                textBox1.Text = mainform.cu;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainform.mtBox1.SelectedText == mainform.mtBox1.Text)
                    mainform.mtBox1.Select(0, 0);
                fl = textBox1.Text;
                mainform.ffind = true;
                mainform.tBox2 = textBox1.Text;
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
                if (radioButton1.Checked == true)
                {
                    mainform.ffl = 0;
                    if (mainform.mtBox1.SelectionStart >= 0)
                    {
                        mainform.mtBox1.Select(mr.LastIndexOf(tt, mainform.mtBox1.SelectionStart - 1), textBox1.Text.Length);
                        mainform.mtBox1.ScrollToCaret();
                    }
                    else MessageBox.Show(("找不到\"" + textBox1.Text + "\""), "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (radioButton2.Checked == true)
                {
                    mainform.ffl = 1;
                    if (mr.IndexOf(tt, mainform.mtBox1.SelectionStart + mainform.mtBox1.SelectedText.Length) >= 0)
                    {
                        mainform.mtBox1.Select(mr.IndexOf(tt, mainform.mtBox1.SelectionStart + mainform.mtBox1.SelectedText.Length), textBox1.Text.Length);
                        mainform.mtBox1.ScrollToCaret();//当屏幕显示不了时，实现滚动
                    }
                    else MessageBox.Show(("找不到\"" + textBox1.Text + "\""), "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { MessageBox.Show(("找不到\"" + textBox1.Text + "\""),"记事本",MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "") button1.Enabled = true;
            else button1.Enabled = false;
        }
    }
}
