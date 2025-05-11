using System;
using System.Linq;
using System.Threading;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
  {
    private void AnalyzeDisk()
    {
      try
      {
        this.ScanDrives
      . ToList()
      . ForEach((_) => {
          this.ScanFiles(_.RootDirectory);
        });
        /************************************************/
        this.DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      catch (ThreadAbortException )
      {
        this.DialogResult = System.Windows.Forms.DialogResult.Abort;
      }
    }
  }
}