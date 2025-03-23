using System;
using System.Runtime.InteropServices;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private bool PathExists(string path) {
      string realPath = ConvertPath(path);

      if (string.IsNullOrEmpty(realPath))
        return false;
      else if (string.Compare(path, "desktop", true) == 0)
        return true;

      string[] pathParts = realPath.Split('\\');

      for (int i = 0; i < pathParts.Length; i++) {
        bool found = false;
        if (ShellBrowser.DesktopItem.SubFolders.Contains(pathParts[i])) {
          pathParts[i] = ShellItem.GetRealPath(ShellBrowser.DesktopItem.SubFolders[pathParts[i]]);

          found = true;
        } else {
          ShellItem myComp = ShellBrowser.DesktopItem.SubFolders[ShellBrowser.MyComputerName];

          if (myComp.SubFolders.Contains(pathParts[i])) {
            pathParts[i] = ShellItem.GetRealPath(myComp.SubFolders[pathParts[i]]);

            found = true;
          }
        }

        if (!found)
          break;
      }

      realPath = string.Join("\\", pathParts);

      if (realPath.EndsWith(":"))
        realPath += "\\";

      WinAPI.SHFILEINFO info = new WinAPI.SHFILEINFO();
      IntPtr ptr = WinAPI.SHGetFileInfo(realPath, 0, ref info, WinAPI.cbFileInfo, WinAPI.SHGFI.DISPLAYNAME);
      bool exists = (ptr != IntPtr.Zero);

      Marshal.FreeCoTaskMem(ptr);
      return exists;
    }
  }
}