using System;
using System.IO;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void InitBrowser() {
      updateInvoker = new UpdateInvoker(ShellItemUpdateInvoke);
      provider = new StreamStorageProvider(FileAccess.Read);

      HandleCreated += new EventHandler(Browser_HandleCreated);
      HandleDestroyed += new EventHandler(Browser_HandleDestroyed);

      navAddressBox.SelectedIndexChanged += new EventHandler(navAddressBox_SelectedIndexChanged);
      navAddressBox.KeyDown += new KeyEventHandler(navAddressBox_KeyDown);
    }
  }
}