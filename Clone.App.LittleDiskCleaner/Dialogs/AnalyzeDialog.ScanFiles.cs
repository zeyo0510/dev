using System;
using System.IO;
using Little_Disk_Cleaner.Properties;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
  {
    private void ScanFiles(DirectoryInfo parentInfo)
    {
      try
      {
        foreach (FileInfo fileInfo in parentInfo.GetFiles())
        {
          this.CurrentFile = fileInfo.FullName;

          // Check if file is exclude
          if (IsFileTypeExcluded(fileInfo.Name))
              continue;

          // Check for zero-byte files
          if (Settings.Default.searchZeroByte)
          {
            if (fileInfo.Length == 0)
            {
              this.FileList.Add(fileInfo);
              continue;
            }
          }

          // Check if file matches types
          if (!Utils.CompareWildcards(fileInfo.Name, Settings.Default.searchFilters))
            continue;

          // Check if file is in use or write protected
          if (Properties.Settings.Default.ignoreWriteProtected && fileInfo.IsReadOnly)
            continue;

          // Check file attributes
          if (!CheckFileAttributes(fileInfo))
            continue;

          // Check file dates
          if (Properties.Settings.Default.findFilesAfter || Settings.Default.findFilesBefore)
            if (!CheckFileDate(fileInfo))
              continue;

          // Check file size
          if (Properties.Settings.Default.checkFileSize)
            if (!CheckFileSize(fileInfo))
              continue;

          this.FileList.Add(fileInfo);
        }

        foreach (DirectoryInfo childInfo in parentInfo.GetDirectories())
        {
          if (IsFolderIncluded(childInfo.FullName) && !IsFolderExcluded(childInfo.FullName))
          {
            this.FileList.AddRange(childInfo.EnumerateFiles());
            continue;
          }

          if (!IsFolderExcluded(childInfo.FullName))
            ScanFiles(childInfo);
        }
      }
      catch (Exception ex)
      {
        //TODO: Add better exception handling
      }
    }
  }
}