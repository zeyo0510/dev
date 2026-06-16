using System;

namespace ShellDll
{
  partial class ShellItem
  {
    internal void AddItem(ShellItem item)
    {
        Browser.UpdateCondition.ContinueUpdate = false;
        lock (Browser)
        {
            try
            {
                if (item.IsFolder)
                    SubFolders.Add(item);
                else
                    SubFiles.Add(item);

                Browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(null, item, ShellItemUpdateType.Created));
            }
            catch (Exception) { }
        }
    }

  }
}