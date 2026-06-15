using System;
using System.Threading;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void InitUpdate()
    {
      updateThread = new Thread(new ThreadStart(UpdateLoop));
      updateThread.IsBackground = true;
      updating = true;

      ShellBrowser.ShellItemUpdate += new ShellItemUpdateEventHandler(shellBrowser_ShellItemUpdate);
      updateThread.Start();
    }
  }
}