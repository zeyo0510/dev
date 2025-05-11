using System;
using System.IO;
using Little_Disk_Cleaner.Properties;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
  {
    private bool CheckFileAttributes(FileInfo fileInfo)
    {
      if ((!Settings.Default.searchHidden) && ((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
        return false;
      /************************************************/
      if ((!Settings.Default.searchArchives) && ((fileInfo.Attributes & FileAttributes.Archive) == FileAttributes.Archive))
        return false;
      /************************************************/
      if ((!Settings.Default.searchReadOnly) && ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly))
        return false;
      /************************************************/
      if ((!Settings.Default.searchSystem) && ((fileInfo.Attributes & FileAttributes.System) == FileAttributes.System))
        return false;
      /************************************************/
      return true;
    }
  }
}