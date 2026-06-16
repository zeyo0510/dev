using System;

namespace ShellDll
{
  partial class ShellItem
  {
    internal void Clear(bool clearFiles, bool clearFolders)
    {
        if (((clearFiles && filesExpanded) || !clearFiles) &&
            ((clearFolders && foldersExpanded) || !clearFolders) &&
            (clearFiles || clearFolders) && ShellFolder != null && !disposed)
        {
            lock (browser)
            {
                try
                {
                    if (clearFiles)
                    {
                        foreach (IDisposable item in subFiles)
                            item.Dispose();

                        subFiles.Clear();
                        filesExpanded = false;
                    }

                    if (clearFolders)
                    {
                        foreach (IDisposable item in subFolders)
                            item.Dispose();

                        subFolders.Clear();
                        foldersExpanded = false;
                    }
                }
                catch (Exception) { }
            }
        }
    }
  }
}