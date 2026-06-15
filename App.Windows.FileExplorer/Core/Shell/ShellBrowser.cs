using System;
using System.Collections;
using System.ComponentModel;

namespace ShellDll
{
  public partial class ShellBrowser : Component
  {
    private ShellBrowserUpdater updater;

    public ShellBrowser()
    {
      InitVars();
      this.Browsers = new ArrayList();
      UpdateCondition = new ShellItemUpdateCondition();
      updater = new ShellBrowserUpdater(this);
    }
  }
}