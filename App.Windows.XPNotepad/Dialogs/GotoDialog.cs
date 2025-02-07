using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using App.Windows.XPNotepad.Main;
/************************************************/
namespace App.Windows.XPNotepad.Dialogs
{
  public partial class GotoDialog : Form
  {
    MainForm mainform;
    int fl;

    public GotoDialog()
    {
      this.InitializeComponent();
    }
    /************************************************/
    private void GotoDialog_Load(object sender, EventArgs e)
    {
      mainform = (MainForm)this.Owner;
      int a;
      a = mainform.start;
      fl = SendMessage(mainform.notepadTextBox.Handle, EM_GETLINECOUNT, 0, 0);
      linenumberTextBox.Text = a.ToString();
    }
    /************************************************/
    private void guiTimer_Tick(object sender, System.EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void okButton_Click(object sender, EventArgs e)
    {
      int a;
      a = Convert.ToInt32(this.linenumberTextBox.Text);
      if (a > fl || a <= 0)
      {
        MessageBox.Show("行数超过范围", "记事本 - 跳行", MessageBoxButtons.OK);
        linenumberTextBox.Text = fl.ToString();
      }
      else
      {
        mainform.notepadTextBox.SelectionStart = SendMessage(mainform.notepadTextBox.Handle, EM_LINEINDEX, a - 1, 0);
        mainform.notepadTextBox.ScrollToCaret();
        Close();
      }
    }
    /************************************************/
    private void cancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }
    
    
    [DllImport("User32.DLL")]
    public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int iParam);

    private const int EM_LINEFROMCHAR = 0xC9;
    private const int EM_LINEINDEX = 0xBB;
    private const int EM_GETLINECOUNT = 0xBA;
    private const int EM_GETLINE = 0xC4;
  }
}