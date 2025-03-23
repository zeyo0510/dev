using System;
using System.ComponentModel;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Browsable(false)]
    public ShellItem SelectedItem {
      get { return selectedItem; }
      set {
        if (value != null)
          SelectPath(value, false);
      }
    }
  }
}