using System;

namespace ShellDll
{
  partial class ShellItem
  {
    private static short MakeSortFlag(ShellItem item)
    {
        if (item.IsFolder)
        {
            if (item.IsDisk)
                return 1;
            if (item.Text == item.Browser.MyDocumentsName &&
                item.Type == item.Browser.SystemFolderName)
                return 2;
            else if (item.Text == item.Browser.MyComputerName)
                return 3;
            else if (item.Type == item.Browser.SystemFolderName)
            {
                if (!item.IsBrowsable)
                    return 4;
                else
                    return 5;
            }
            else if (item.IsFolder && !item.IsBrowsable)
                return 6;
            else
                return 7;
        }
        else
            return 8;
    }
  }
}