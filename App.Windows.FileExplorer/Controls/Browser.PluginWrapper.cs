using System;
using System.ComponentModel;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Category("Options"),
     Description("Sets the BrowserPluginWrapper for the control, if null the control will create it's own."),
     DefaultValue(null),
     Browsable(true)]
    public BrowserPluginWrapper PluginWrapper {
      get { return pluginWrapper; }
      set {
        if (!BrowserPluginWrapper.Equals(PluginWrapper, value)) {
          if (handleCreated) Browser_HandleDestroyed(this, new EventArgs());
          pluginWrapper = value;
          if (handleCreated) Browser_HandleCreated(this, new EventArgs());
        }
      }
    }
  }
}