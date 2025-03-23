using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Category("Options"),
     Description("Shows or hides the folder TreeView"),
     DefaultValue(true),
     Browsable(true)]
    public bool ShowFolders {
      get { return navFoldersButton.Checked; }
      set { navFoldersButton.Checked = value; }
    }
  }
}