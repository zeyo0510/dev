using System;
using Little_Disk_Cleaner.Properties;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
  {
    private bool IsFolderExcluded(string dirPath)
    {
      foreach (string excludeDir in Settings.Default.excludedDirs)
      {
        if (Utils.CompareWildcard(dirPath, excludeDir))
        {
          return true;
        }
      }
      /************************************************/
      return false;
    }
  }
}