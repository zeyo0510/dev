using System;
using System.IO;
using Little_Disk_Cleaner.Properties;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
  {
    public bool CheckFileDate(FileInfo fileInfo)
    {
      DateTime dateTimeFile = DateTime.MinValue;
      bool bRet = false;

      if (Settings.Default.findFilesMode == 0)
        dateTimeFile = fileInfo.CreationTime;
      else if (Settings.Default.findFilesMode == 1)
        dateTimeFile = fileInfo.LastWriteTime;
      else if (Settings.Default.findFilesMode == 2)
        dateTimeFile = fileInfo.LastAccessTime;

      if (Settings.Default.findFilesAfter)
      {
        if (DateTime.Compare(dateTimeFile, Settings.Default.dateTimeAfter) >= 0)
          bRet = true;
      }

      if (Settings.Default.findFilesBefore)
      {
        if (DateTime.Compare(dateTimeFile, Settings.Default.dateTimeBefore) <= 0)
          bRet = true;
      }

      return bRet;
    }
  }
}