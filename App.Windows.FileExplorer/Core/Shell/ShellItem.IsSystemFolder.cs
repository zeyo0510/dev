using System;
/************************************************/
namespace ShellDll
{
  partial class ShellItem
  {
    public bool IsSystemFolder
    {
      get
      {
        return this.Type == Browser.SystemFolderName;
      }
    }
  }
}