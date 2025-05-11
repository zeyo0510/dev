using System;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
  {
    public void UpdateUI()
    {
      this.countlabel.Text = string.Format("Files Found: {0}", this.FileList.Count);
      this.currentfileTextBox.Text = string.Copy(this.CurrentFile);
    }
  }
}