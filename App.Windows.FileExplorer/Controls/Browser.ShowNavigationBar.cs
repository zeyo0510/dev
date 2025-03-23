using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Category("Options"),
     Description("Shows or hides the Navigation Bar"),
     DefaultValue(true),
     Browsable(true)]
    public bool ShowNavigationBar {
      get { return navigationBar.Visible; }
      set { navigationBar.Visible = value; }
    }
  }
}