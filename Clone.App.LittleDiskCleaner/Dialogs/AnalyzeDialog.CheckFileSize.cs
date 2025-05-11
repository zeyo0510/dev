using System;
using System.IO;
using Little_Disk_Cleaner.Properties;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
  {
    private bool CheckFileSize(FileInfo fileInfo)
    {
      long fileSize = fileInfo.Length / 1024;
      /************************************************/
      if (Settings.Default.checkFileSizeLeast > 0)
        if (fileSize <= Settings.Default.checkFileSizeLeast)
          return false;
      /************************************************/
      if (Settings.Default.checkFileSizeMost > 0)
        if (fileSize >= Settings.Default.checkFileSizeMost)
          return false;
      /************************************************/
      return true;
    }
  }
}