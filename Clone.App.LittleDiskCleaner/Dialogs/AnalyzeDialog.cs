using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  public partial class AnalyzeDialog : Form
  {
    private Thread threadMain = null;

    public AnalyzeDialog(List<DriveInfo> selectedDrives)
    {
      this.InitializeComponent();
      /************************************************/
      this.ScanDrives = selectedDrives;
      /************************************************/
      this.FileList = new List<FileInfo>();
    }
    /************************************************/
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      /************************************************/
      this.threadMain = new Thread(new ThreadStart(AnalyzeDisk));
      this.threadMain.Start();
    }
    /************************************************/
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      base.OnFormClosing(e);
      /************************************************/
      if (this.DialogResult != DialogResult.OK)
      {
        if (e.CloseReason == CloseReason.UserClosing)
        {
          if (MessageBox.Show(this, "Are you sure?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            e.Cancel = true;
          else
            this.threadMain.Abort();
        }
      }
    }
    /************************************************/
    private void guiTimer_Tick(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void stopButton_Click(object sender, EventArgs e)
    {
      base.DialogResult = DialogResult.Abort;
      /************************************************/
      base.Close();
    }
  }
}