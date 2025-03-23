using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Category("Options"),
     Description("Sets the Initial Directory of the Tree when StartUpDirectory is set to \"Other\""),
     DefaultValue(""),
     Browsable(true)]
    public string StartUpDirectoryOther {
      get { return otherStartupDir; }
      set  {
        if (otherStartupDir != value) {
          otherStartupDir = value;
        }
      }
    }
  }
}