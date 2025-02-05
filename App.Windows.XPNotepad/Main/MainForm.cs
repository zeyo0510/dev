using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using App.Windows.XPNotepad.Dialogs;
using Microsoft.Win32;
using notepad;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  public partial class MainForm : Form
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

    public MainForm()
    {
      this.InitializeComponent();
    }
    
    public MainForm(string[] test) : this()
    {
      if (test.Length > 0)
      {
        fl = true;
        ft = test[0];
      }
    }  

    public MainForm(int a)
    {
      this.InitializeComponent();
    }

    private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      notepadTextBox.Undo();
    }

    private void 剪切ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      notepadTextBox.Cut();
    }

    private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      notepadTextBox.Copy();
    }

    private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      notepadTextBox.Paste();
      notepadTextBox.Font = new Font("宋体", notepadTextBox.Font.Size);
    }

    private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      if (notepadTextBox.SelectedText != "")
        notepadTextBox.SelectedText = "";
    }

    private void 全选ToolStripMenuItem2_Click(object sender, EventArgs e)
    {
      notepadTextBox.SelectAll();
    }

    private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (notepadTextBox.Text == "")
      {
          notepadTextBox.Text = "";
          n = 0;
          this.Text = "无标题 - 记事本";
          ffind = false;
          cu = "";
      }
      else
      {
          try
          {
              if (ts != notepadTextBox.Text)
              {
                  Form5 f = new Form5();
                  f.Owner = this;
                  f.ShowDialog();
                  if (f.b == 2 || (f.b == 1 && this.Text != "无标题 - 记事本"))
                  {
                      notepadTextBox.Text = "";
                      n = 0;
                      ts = "";
                      this.Text = "无标题 - 记事本";
                      ffind = false;
                      cu = "";
                  }
              }
              else
              {
                  notepadTextBox.Text = "";
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
          if (ts != notepadTextBox.Text)
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
              sw.Write(this.notepadTextBox.Text);
              sw.Flush();
              sw.Close();
              n = 1;
              ts = notepadTextBox.Text;
              if (exe == 0)
                  this.Text = Path.GetFileName(saveFileDialog1.FileName) + " - 记事本";
              else
                  this.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + " - 记事本";
          }
          else
          {
              StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.Default);
              sw.Write(this.notepadTextBox.Text);
              sw.Flush();
              sw.Close();
              ts = notepadTextBox.Text;
          }
      }
      catch { notepadTextBox.SelectionStart = notepadTextBox.TextLength; }
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
              sw.Write(this.notepadTextBox.Text);
              sw.Flush();
              sw.Close();
              ts = notepadTextBox.Text;
              if (exe == 0)
                  this.Text = Path.GetFileName(saveFileDialog1.FileName) + " - 记事本";
              else
                  this.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + " - 记事本";
          }
      }
      catch { notepadTextBox.SelectionStart = notepadTextBox.TextLength; }
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
      GotoDialog f4 = new GotoDialog();
      f4.Owner = this;
      f4.Location = new Point(this.Location.X + 15, this.Location.Y + 80);
      f4.ShowDialog();
    }

    private void 时间日期ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      notepadTextBox.Paste(DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString());
    }

    private void 状态栏SToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (statusbarMenuItem.Checked == false)
      {
        statusbarMenuItem.Checked = true;
        bottomStatusBar.Visible = true;
      }
      else
      {
        statusbarMenuItem.Checked = false;
        bottomStatusBar.Visible = false;
      }
    }

    private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (wordwrapMenuItem.Checked == false)
      {
          wordwrapMenuItem.Checked = true;
          notepadTextBox.WordWrap = true;
          gotoMenuItem1.Enabled = false;
      }
      else
      {
          wordwrapMenuItem.Checked = false;
          notepadTextBox.WordWrap = false;
          gotoMenuItem1.Enabled = true;
      }
      if (wordwrapMenuItem.Checked)
      {
          statusbarMenuItem.Enabled = false;
          if (statusbarMenuItem.Checked)
              stc = 1;
          else stc = 0;
          if (bottomStatusBar.Visible)
              ss = 1;
          else ss = 0;
          bottomStatusBar.Visible = false;
          statusbarMenuItem.Checked = false;
      }
      else
      {
          statusbarMenuItem.Enabled = true;
          if (stc == 1) statusbarMenuItem.Checked = true;
          if (ss == 1) bottomStatusBar.Visible = true;
      }
    }

    private void 关于记事本ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShellAbout(this.Handle.ToInt32(), "记事本", "朱靥超 Visual Studio 2008 RTM 修正版", this.Icon.Handle.ToInt32());
    }

    private void mtBox1_TextChanged(object sender, EventArgs e)
    {
      GetPoint();
      undoMenuItem.Enabled = true;
      if (notepadTextBox.Text != "")
      {
        findMenuItem.Enabled = true;
        findnextMenuItem.Enabled = true;
      }
      else
      {
        findMenuItem.Enabled = false;
        findnextMenuItem.Enabled = false;
      }
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (notepadTextBox.Text == "") Dispose();
      try
      {
        if (ts != notepadTextBox.Text)
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
      lines = notepadTextBox.Lines.Length;
      if (bottomStatusBar.Visible == true)
        statusbarMenuItem.Checked = true;
      if (notepadTextBox.Text == "")
      {
        undoMenuItem.Enabled = false;
        cutMenuItem.Enabled = false;
        deleteMenuItem.Enabled = false;
        copyMenuItem.Enabled = false;

        findMenuItem.Enabled = false;
        findnextMenuItem.Enabled = false;
      }
    }

    private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int cur = notepadTextBox.SelectionStart;
      if (fontDialog1.ShowDialog() != DialogResult.Cancel)
        notepadTextBox.Font = fontDialog1.Font;
      notepadTextBox.Select(cur, 0);
      notepadTextBox.SelectionStart = cur;
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
      Point cursorPos = GetCursorPos(this.notepadTextBox);
      this.lncolStatusBarPanel.Text = "    第 " + cursorPos.Y.ToString() + " 行 , " + "第 " + cursorPos.X.ToString() + " 列";
      start = cursorPos.Y;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (notepadTextBox.SelectedText != "")
      {
        cutMenuItem.Enabled = true;
        deleteMenuItem.Enabled = true;
        copyMenuItem.Enabled = true;
      }
      else
      {
        cutMenuItem.Enabled = false;
        deleteMenuItem.Enabled = false;
        copyMenuItem.Enabled = false;
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
                    if (notepadTextBox.SelectedText == notepadTextBox.Text)
                        notepadTextBox.Select(0, 0);
                    string tmtbox = notepadTextBox.Text;
                    string ttbox = tBox2;
                    if (f2checkbox == false)
                    {
                        tmtbox = notepadTextBox.Text.ToLower();
                        ttbox = ttbox.ToLower();
                    }
                    if (ffl == 0)
                    {
                        if (notepadTextBox.SelectionStart >= 0)
                        {
                            notepadTextBox.Select(notepadTextBox.Text.LastIndexOf(ttbox, notepadTextBox.SelectionStart - 1), ttbox.Length);
                            notepadTextBox.ScrollToCaret();
                        }
                        else MessageBox.Show(("找不到\"" + tBox2 + "\""), "记事本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (ffl == 1)
                    {
                        if (notepadTextBox.Text.IndexOf(ttbox, notepadTextBox.SelectionStart + notepadTextBox.SelectedText.Length) >= 0)
                        {
                            notepadTextBox.Select(notepadTextBox.Text.IndexOf(ttbox, notepadTextBox.SelectionStart + notepadTextBox.SelectedText.Length), ttbox.Length);
                            notepadTextBox.ScrollToCaret();
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
        if (ts != notepadTextBox.Text)
        {
          Form5 f = new Form5();
          f.Owner = this;
          f.ShowDialog();
          if (f.b == 1 || f.b == 2)
          {
            notepadTextBox.Text = "";
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
      notepadTextBox.Text = open.ReadToEnd();
      open.Close();
      if (exe == 0)
        this.Text = Path.GetFileName(openFileDialog1.FileName) + " - 记事本";
      else
        this.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + " - 记事本";
      ts = notepadTextBox.Text;
      n = 1;
      notepadTextBox.Font = new Font("宋体", notepadTextBox.Font.Size);
      notepadTextBox.SelectionStart = 0;
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
      notepadTextBox.Text = open.ReadToEnd();
      open.Close();
      strreader();
    }

    public void drag()
    {
      StreamReader open = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default, true);
      notepadTextBox.Text = open.ReadToEnd();
      open.Close();
      strreader();
      ft = "";
      notepadTextBox.SelectionStart = 0;
    }

    private void statusBar1_Resize(object sender, EventArgs e)
    {
      try
      {
        statusBarPanel1.Width = (int)Math.Round(bottomStatusBar.Size.Width * 0.73);
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
      e.Graphics.DrawString(notepadTextBox.Text, notepadTextBox.Font, Brushes.Black, e.PageBounds);
    }
  }
}