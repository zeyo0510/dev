using System;
/************************************************/
namespace ShellDll
{
  internal class ShellItemUpdateEventArgs : EventArgs
  {
    public ShellItemUpdateEventArgs(ShellItem oldItem, ShellItem newItem, ShellItemUpdateType type)
    {
      this.OldItem    = oldItem;
      this.NewItem    = newItem;
      this.UpdateType = type;
    }
    /************************************************/
    public ShellItem OldItem
    {
      get; private set;
    }
    /************************************************/
    public ShellItem NewItem
    {
      get; private set;
    }
    /************************************************/
    public ShellItemUpdateType UpdateType
    {
      get; private set;
    }
  }
}