using System;

namespace ShellDll
{
  internal delegate void ShellItemUpdateEventHandler(object sender, ShellItemUpdateEventArgs e);
  
  partial class ShellBrowser
  {
    internal event ShellItemUpdateEventHandler ShellItemUpdate;
    
    internal void OnShellItemUpdate(object sender, ShellItemUpdateEventArgs e)
    {
        if (ShellItemUpdate != null)
        {
            ShellItemUpdate(sender, e);
        }
    }
  }
}