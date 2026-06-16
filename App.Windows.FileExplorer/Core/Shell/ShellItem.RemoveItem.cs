using System;

namespace ShellDll
{
  partial class ShellItem
  {
    internal void RemoveItem(ShellItem item)
    {
        browser.UpdateCondition.ContinueUpdate = false;

        lock (browser)
        {
            try
            {
                if (item.IsFolder)
                    SubFolders.Remove(item);
                else
                    SubFiles.Remove(item);

                Browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(item, null, ShellItemUpdateType.Deleted));
                ((IDisposable)item).Dispose();
            }
            catch (Exception) { }
        }
    }
  }
}