using System;

namespace ShellDll
{
  partial class ShellItem
  {
    internal void Clear(bool clearFiles, bool clearFolders)
    {
      if (((clearFiles && FilesExpanded) || !clearFiles) &&
          ((clearFolders && FoldersExpanded) || !clearFolders) &&
          (clearFiles || clearFolders) && ShellFolder != null && !disposed)
      {
        lock (Browser)
        {
          try
          {
            if (clearFiles)
            {
              foreach (IDisposable item in SubFiles)
              {
                item.Dispose();
              }

              SubFiles.Clear();
              FilesExpanded = false;
            }

            if (clearFolders)
            {
              foreach (IDisposable item in SubFolders)
              {
                item.Dispose();
              }

              SubFolders.Clear();
              FoldersExpanded = false;
            }
          }
          catch (Exception)
          {
            // do nothing...
          }
        }
      }
    }
  }
}