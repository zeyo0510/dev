using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Browsable(false)]
    internal bool SelectionChange {
      get { return selectionChange; }
      set { selectionChange = value; }
    }
  }
}