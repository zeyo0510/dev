using System;
using System.Threading;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void UpdateLoop() {
      while (updating) {
        if (selectedItem != null) {
          ShellBrowser.UpdateCondition.ContinueUpdate = true;
          selectedItem.Update(true, true);
        }

        Thread.Sleep(500);
      }
    }
  }
}