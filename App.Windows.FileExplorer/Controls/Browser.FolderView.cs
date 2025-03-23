using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Browsable(false)]
    internal BrowserTreeView FolderView { get { return folderView; } }
  }
}