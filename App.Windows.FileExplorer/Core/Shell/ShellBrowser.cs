using System;
using System.Text;
using System.Runtime.InteropServices;
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
      browsers = new ArrayList();
      updateCondition = new ShellItemUpdateCondition();
      updater = new ShellBrowserUpdater(this);
    }
  }
}