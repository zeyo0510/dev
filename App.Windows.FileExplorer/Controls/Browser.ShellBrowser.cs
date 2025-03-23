using System;
using System.ComponentModel;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Category("Options"),
     Description("Sets the ShellBrowser for the control, if null the control will create it's own."),
     DefaultValue(null),
     Browsable(true)]
    public ShellBrowser ShellBrowser {
      get { return shellBrowser; }
      set {
        if (!ShellDll.ShellBrowser.Equals(ShellBrowser, value)) {
          if (ShellBrowser != null)
            ShellBrowser.Browsers.Remove(this);

          if (handleCreated) Browser_HandleDestroyed(this, new EventArgs());
          shellBrowser = value;
          if (handleCreated) Browser_HandleCreated(this, new EventArgs());

          if (!ShellBrowser.Browsers.Contains(this))
            ShellBrowser.Browsers.Add(this);
        }
      }
    }
  }
}