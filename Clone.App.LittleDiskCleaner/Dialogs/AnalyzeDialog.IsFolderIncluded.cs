using System;
using Little_Disk_Cleaner.Properties;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
  {
    private bool IsFolderIncluded(string dirPath)
    {
      foreach (string includeDir in Settings.Default.includedFolders)
      {
        if (string.Compare(includeDir, dirPath) == 0 || Utils.CompareWildcard(dirPath, includeDir))
        {
          return true;
        }
      }
      /************************************************/
      return false;
    }
  }
}