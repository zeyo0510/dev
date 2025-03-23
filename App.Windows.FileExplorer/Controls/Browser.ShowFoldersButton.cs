using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Category("Options"),
     Description("Shows or hides the button to hide and show the Folders TreeView"),
     DefaultValue(true),
     Browsable(true)]
    public bool ShowFoldersButton {
      get { return navFoldersButton.Visible; }
      set { navFoldersButton.Visible = value; }
    }
  }
}