using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Category("Options"),
     Description("Sets the Initial Directory of the Tree"),
     DefaultValue(SpecialFolders.MyComputer),
     Browsable(true)]
    public SpecialFolders StartUpDirectory {
        get { return startupDir; }
        set  {
          if (startupDir != value) {
            startupDir = value;
          }
        }
    }
  }
}