using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Browsable(false)]
    internal BrowserListView FileView { get { return fileView; } }
  }
}