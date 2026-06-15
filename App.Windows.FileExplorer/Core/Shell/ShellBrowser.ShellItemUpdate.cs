using System;
/************************************************/
namespace ShellDll
{
  partial class ShellBrowser
  {
    internal event ShellItemUpdateEventHandler ShellItemUpdate = null;
    /************************************************/
    internal void OnShellItemUpdate(object sender, ShellItemUpdateEventArgs e)
    {
      if (this.ShellItemUpdate != null) this.ShellItemUpdate(sender, e);
    }
  }
}