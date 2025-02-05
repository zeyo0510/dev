using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
//Download by http://www.codesc.net
namespace notepad
{
    public partial class Form1 : Form
    {
        public int n = 0;
        int ss = 0, stc = 0;
        public int find = 0, replace = 0;
        public string ts = "";
        public int start;
        int[] emer = new int[10000];
        public bool ffind = false;
        public int ffl = 0;
        public string tBox2 = "";
        bool fl = false;
        string ft = "";
        int lines;
        public string cu = "";
        public int exe = 0;
        string sys = "";
        public bool f2checkbox = false;
        public int totalcols = 0;

        Form2 f2;
        Form3 f3;

        [DllImport("shell32.dll", EntryPoint = "ShellAbout")]
        private static extern int ShellAbout(int hWndn, string szApp, string szOtherStuff, int hIcon);

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int iParam);

        private const int EM_LINEFROMCHAR = 0xC9;
        private const int EM_LINEINDEX = 0xBB;

        private Point GetCursorPos(TextBox textBox)
        {
            Point cursorPos = new Point(0, 0);
            int x, y;
            y = SendMessage(textBox.Handle, EM_LINEFROMCHAR, textBox.SelectionStart, 0);
            x = textBox.SelectionStart - SendMessage(textBox.Handle, EM_LINEINDEX, y, 0);
            cursorPos.Y = ++y;
            cursorPos.X = ++x;
            return cursorPos;
        }

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string[] test)
            : this()
        {
            if (test.Length > 0)
            {
                fl = true;
                ft = test[0];
            }
        }  

        public Form1(int a)
        {
            InitializeComponent();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mtBox1.Undo();
        }

        private void 剪切ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mtBox1.Cut();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mtBox1.Copy();
        }

        private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mtBox1.Paste();
            mtBox1.Font = new Font("宋体", mtBox1.Font.Size);
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (mtBox1.SelectedText != "")
                mtBox1.SelectedText = "";
        }

        private void 全选ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            mtBox1.SelectAll();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mtBox1.Text == "")
            {
                mtBox1.Text = "";
                n = 0;
                this.Text = "无标题 - 记事本";
                ffind = false;
                cu = "";
            }
            else
            {
                try
                {
                    if (ts != mtBox1.Text)
                    {
                        Form5 f = new Form5();
                        f.Owner = this;
                        f.ShowDialog();
                        if (f.b == 2 || (f.b == 1 && this.Text != "无标题 - 记事本"))
                        {
                            mtBox1.Text = "";
                            n = 0;
                            ts = "";
                            this.Text = "无标题 - 记事本";
                            ffind = false;
                            cu = "";
                        }
                    }
                    else
                    {
                        mtBox1.Text = "";
                        n = 0;
                        ts = "";
                        this.Text = "无标题 - 记事本";
                        ffind = false;
                        cu = "";
                    }
                }
                catch { }
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileExt();
                if (ts != mtBox1.Text)
                {
                    Form5 f = new Form5();
                    f.Owner = this;
                    f.ShowDialog();
                    if (f.b == 2 || f.b == 1)
                        open();
                }
                else open();
            }
            catch { }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileExt();
                if (n == 0)
                {
                    saveFileDialog1.Title = "保存";
                    saveFileDialog1.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
                    saveFileDialog1.FileName = "*.txt";
                    saveFileDialog1.ShowDialog();
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.Default);
                    sw.Write(this.mtBox1.Text);
                    sw.Flush();
                    sw.Close();
                    n = 1;
                    ts = mtBox1.Text;
                    if (exe == 0)
                        this.Text = Path.GetFileName(saveFileDialog1.FileName) + " - 记事本";
                    else
                        this.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + " - 记事本";
                }
                else
                {
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.Default);
                    sw.Write(this.mtBox1.Text);
                    sw.Flush();
                    sw.Close();
                    ts = mtBox1.Text;
                }
            }
            catch { mtBox1.SelectionStart = mtBox1.TextLength; }
        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileExt();
                saveFileDialog1.Title = "另存为";
                saveFileDialog1.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
                saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName + ".txt");
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.Default);
                    sw.Write(this.mtBox1.Text);
                    sw.Flush();
                    sw.Close();
                    ts = mtBox1.Text;
                    if (exe == 0)
                        this.Text = Path.GetFileName(saveFileDialog1.FileName) + " - 记事本";
                    else
                        this.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + " - 记事本";
                }
            }
            catch { mtBox1.SelectionStart = mtBox1.TextLength; }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (find == 0)
                {
                    f2 = new Form2();
                    f2.Owner = this;
                    f2.Location = new Point(this.Location.X + 50, this.Location.Y + 160);
                    f2.Show();
                    find = 1;
                }
                else f2.BringToFront();
            }
            catch { }
        }

        private void 定位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (replace == 0)
                {
                    f3 = new Form3();
                    f3.Owner = this;
                    f3.Location = new Point(this.Location.X + 50, this.Location.Y + 120);
                    f3.Show();
                    replace = 1;
                }
                else f3.BringToFront();
            }
            catch { }
        }

        private void 转到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Owner = this;
            f4.Location = new Point(this.Location.X + 15, this.Location.Y + 80);
            f4.ShowDialog();
        }

        private void 时间日期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mtBox1.Paste(DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString());
        }

        private void 状态栏SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (状态栏SToolStripMenuItem.Checked == false)
            {
                状态栏SToolStripMenuItem.Checked = true;
                statusBar1.Visible = true;
            }
            else
            {
                状态栏SToolStripMenuItem.Checked = false;
                statusBar1.Visible = false;
            }
        }

        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (自动换行ToolStripMenuItem.Checked == false)
            {
                自动换行ToolStripMenuItem.Checked = true;
                mtBox1.WordWrap = true;
                menuItem50.Enabled = false;
            }
            else
            {
                自动换行ToolStripMenuItem.Checked = false;
                mtBox1.WordWrap = false;
                menuItem50.Enabled = true;
            }
            if (自动换行ToolStripMenuItem.Checked)
            {
                状态栏SToolStripMenuItem.Enabled = false;
                if (状态栏SToolStripMenuItem.Checked)
                    stc = 1;
                else stc = 0;
                if (statusBar1.Visible)
                    ss = 1;
                else ss = 0;
                statusBar1.Visible = false;
                状态栏SToolStripMenuItem.Checked = false;
            }
            else
            {
                状态栏SToolStripMenuItem.Enabled = true;
                if (stc == 1) 状态栏SToolStripMenuItem.Checked = true;
                if (ss == 1) statusBar1.Visible = true;
            }
        }

        private void 关于记事本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellAbout(this.Handle.ToInt32(), "记事本", "朱靥超 Visual Studio 2008 RTM 修正版", this.Icon.Handle.ToInt32());
        }

        private void mtBox1_TextChanged(object sender, EventArgs e)
        {
            GetPoint();
            撤销UToolStripMenuItem.Enabled = true;
            if (mtBox1.Text != "")
            {
                查找ToolStripMenuItem.Enabled = true;
                查找下一个ToolStripMenuItem.Enabled = true;
            }
            else
            {
                查找ToolStripMenuItem.Enabled = false;
                查找下一个ToolStripMenuItem.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mtBox1.Text == "") Dispose();
            try
            {
                if (ts != mtBox1.Text)
                {
                    Form5 f = new Form5();
                    f.Owner = this;
                    f.ShowDialog();
                    if (f.b == 2) Dispose();
                    else if (f.b == 3) e.Cancel = true;
                }
            }
            catch { }
        }
        [DllImport("shell32.dll")]
        public static extern int ExtractIcon(int handle, string path, int index);

        [DllImport("kernel32.dll")]
        public static extern int GetWindowsDirectory(StringBuilder lpBuffer, int uSize);

        private void Form1_Shown(object sender, EventArgs e)
        {
//            StringBuilder sb = new StringBuilder(128);
//            GetWindowsDirectory(sb, sb.Capacity);
//            string startname = sb.ToString() + "\\notepad.exe";
//            IntPtr icon = new IntPtr(ExtractIcon(this.Handle.ToInt32(), startname, 0));
//            this.Icon = Icon.FromHandle(icon);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetPoint();


            if (fl) mtBox1_DragDrop(null, null);
            lines = mtBox1.Lines.Length;
            if (statusBar1.Visible == true)
                状态栏SToolStripMenuItem.Checked = true;
            if (mtBox1.Text == "")
            {
                撤销UToolStripMenuItem.Enabled = false;
                剪切ToolStripMenuItem.Enabled = false;
                删除ToolStripMenuItem.Enabled = false;
                复制zToolStripMenuItem.Enabled = false;

                查找ToolStripMenuItem.Enabled = false;
                查找下一个ToolStripMenuItem.Enabled = false;
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cur = mtBox1.SelectionStart;
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
                mtBox1.Font = fontDialog1.Font;
            mtBox1.Select(cur, 0);
            mtBox1.SelectionStart = cur;
        }

        private void 页面设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.pageSetupDialog1.Document = this.printDocument1;
                if (this.pageSetupDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.printDocument1.Print();
                }
            }
            catch { MessageBox.Show("您尚未安装打印机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.printDialog1.Document = this.printDocument1;
                    this.printDocument1.Print();
                }
            }
            catch { MessageBox.Show("您尚未安装打印机", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void menuItem57_Click(object sender, EventArgs e)
        {
            if (sys == "Windows Vista")
                Help.ShowHelp(this, "C:\\Windows\\winhlp32.exe");
            else
                Help.ShowHelp(this, "C:\\WINDOWS\\Help\\notepad.chm");
        }

        private void FileExt()
        {
            String regPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
            RegistryKey regKey = Registry.CurrentUser;
            try
            {
                exe = (int)regKey.OpenSubKey(regPath, true).GetValue("HideFileExt");
            }
            catch { }
        }

        private void GetPoint()
        {
            Point cursorPos = GetCursorPos(this.mtBox1);
            this.statusBarPanel2.Text = "    第 " + cursorPos.Y.ToString() + " 行 , " + "第 " + cursorPos.X.ToString() + " 列";
            start = cursorPos.Y;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mtBox1.SelectedText != "")
            {
                剪切ToolStripMenuItem.Enabled = true;
                删除ToolStripMenuItem.Enabled = true;
                复制zToolStripMenuItem.Enabled = true;
            }
            else
            {
                剪切ToolStripMenuItem.Enabled = false;
                删除ToolStripMenuItem.Enabled = false;
                复制zToolStripMenuItem.Enabled = false;
            }
        }

        private void 查找下一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ffind == false)
                    if (find == 0)
                    {
                        f2 = new Form2();
                        f2.Owner = this;
                        f2.Location = new Point(this.Location.X + 50, this.Location.Y + 160);
                        f2.Show();
                        find = 1;
                    }
                    else f2.BringToFront();
                else
                {
                    try
                    {
                        if (mtBox1.SelectedText == mtBox1.Text)
                            mtBox1.Select(0, 0);
                        string tmtbox = mtBox1.Text;
                        string ttbox = tBox2;
                        if (f2checkbox == false)
                        {
                            tmtbox = mtBox1.Text.ToLower();
                            ttbox = ttbox.ToLower();
                        }
                        if (ffl == 0)
                        {
                            if (mtBox1.SelectionStart >= 0)
                            {
                                mtBox1.Select(mtBox1.Text.LastIndexOf(ttbox, mtBox1.SelectionStart - 1), ttbox.Length);
                                mtBox1.ScrollToCaret();
                            }
                            else MessageBox.Show(("找不到\"" + tBox2 + "\""), "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (ffl == 1)
                        {
                            if (mtBox1.Text.IndexOf(ttbox, mtBox1.SelectionStart + mtBox1.SelectedText.Length) >= 0)
                            {
                                mtBox1.Select(mtBox1.Text.IndexOf(ttbox, mtBox1.SelectionStart + mtBox1.SelectedText.Length), ttbox.Length);
                                mtBox1.ScrollToCaret();
                            }
                            else MessageBox.Show(("找不到\"" + tBox2 + "\""), "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch { MessageBox.Show(("找不到\"" + tBox2 + "\""), "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch { }
        }

        private void mtBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void mtBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (ft == "")
                    openFileDialog1.FileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                else openFileDialog1.FileName = ft;
                if (ts != mtBox1.Text)
                {
                    Form5 f = new Form5();
                    f.Owner = this;
                    f.ShowDialog();
                    if (f.b == 1 || f.b == 2)
                    {
                        mtBox1.Text = "";
                        n = 0;
                        drag();
                    }
                }
                else drag();
            }
            catch { MessageBox.Show("拒绝访问。", "记事本", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        public void strreader()
        {
            StreamReader open = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default, true);
            open.Close();
            open = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default, true);
            mtBox1.Text = open.ReadToEnd();
            open.Close();
            if (exe == 0)
                this.Text = Path.GetFileName(openFileDialog1.FileName) + " - 记事本";
            else
                this.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + " - 记事本";
            ts = mtBox1.Text;
            n = 1;
            mtBox1.Font = new Font("宋体", mtBox1.Font.Size);
            mtBox1.SelectionStart = 0;
            saveFileDialog1.FileName = openFileDialog1.FileName;
        }

        public void open()
        {
            openFileDialog1.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
            openFileDialog1.FileName = "";
            if (sys != "Windows Vista")
                openFileDialog1.FileName = "*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.Yes) n = 1;
            StreamReader open = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default, true);
            mtBox1.Text = open.ReadToEnd();
            open.Close();
            strreader();
        }

        public void drag()
        {
            StreamReader open = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default, true);
            mtBox1.Text = open.ReadToEnd();
            open.Close();
            strreader();
            ft = "";
            mtBox1.SelectionStart = 0;
        }

        private void statusBar1_Resize(object sender, EventArgs e)
        {
            try
            {
                statusBarPanel1.Width = (int)Math.Round(statusBar1.Size.Width * 0.73);
            }
            catch { }
        }

        private void mtBox1_Click(object sender, EventArgs e)
        {
            GetPoint();
        }

        private void mtBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            GetPoint();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(mtBox1.Text, mtBox1.Font, Brushes.Black, e.PageBounds);
        }
    }
}
