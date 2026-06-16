using System;
/************************************************/
namespace ShellDll
{
  partial class ShellItem
  {
    internal void RemoveItem(ShellItem item)
    {
      Browser.UpdateCondition.ContinueUpdate = false;
      /************************************************/
      lock (Browser)
      {
        try
        {
          if (item.IsFolder)
            SubFolders.Remove(item);
          else
              SubFiles.Remove(item);
          /************************************************/
          Browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(item, null, ShellItemUpdateType.Deleted));
          /************************************************/
          ((IDisposable)item).Dispose();
        }
        catch (Exception)
        {
          // do nothing...
        }
      }
    }
  }
}