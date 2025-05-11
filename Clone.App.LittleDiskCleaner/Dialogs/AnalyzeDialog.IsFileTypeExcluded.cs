using System;
using System.Linq;
using Little_Disk_Cleaner.Properties;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
  {
    private bool IsFileTypeExcluded(string fileName)
    {
      foreach (string excludeFileType in Settings.Default.excludedFileTypes)
      {
        if (Utils.CompareWildcard(fileName, excludeFileType))
        {
          return true;
        }
      }
      /************************************************/
      return false;
    }
  }
}