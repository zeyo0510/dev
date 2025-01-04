using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace notepad
{
    public partial class Form4 : Form
    {
        Form1 mainform;
        int fl;

        public Form4()
        {
            InitializeComponent();
        }

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int iParam);

        private const int EM_LINEFROMCHAR = 0xC9;
        private const int EM_LINEINDEX = 0xBB;
        private const int EM_GETLINECOUNT = 0xBA;
        private const int EM_GETLINE = 0xC4;


        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a;
            a = Convert.ToInt32(textBox1.Text);
            if (a > fl || a <= 0)
            {
                MessageBox.Show("行数超过范围", "记事本 - 跳行", MessageBoxButtons.OK);
                textBox1.Text = fl.ToString();
            }
            else
            {
                mainform.mtBox1.SelectionStart = SendMessage(mainform.mtBox1.Handle, EM_LINEINDEX, a - 1, 0);
                mainform.mtBox1.ScrollToCaret();
                Close();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            mainform = (Form1)this.Owner;
            int a;
            a = mainform.start;
            fl = SendMessage(mainform.mtBox1.Handle, EM_GETLINECOUNT, 0, 0);
            textBox1.Text = a.ToString();
        }
    }
}
