using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class BrowserComboBox
  {
    [Browsable(false)]
    public BrowserComboItem CurrentItem
    {
      get; set;
    }
  }
}