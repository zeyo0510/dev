using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Browsable(false)]
    internal BrowserComboBox NavAddressBox
    {
      get
      {
        return this.navAddressBox;
      }
    }
  }
}