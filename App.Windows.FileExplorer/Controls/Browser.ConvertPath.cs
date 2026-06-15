using System;
using System.Globalization;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private string ConvertPath(string path)
    {
      if (string.IsNullOrEmpty(path))
        return path;

      string newPath = path.Trim();

      if (newPath.StartsWith(string.Format(@"{0}\", ShellBrowser.MyComputerName), false, CultureInfo.InstalledUICulture) && newPath.Length > 12)
        newPath = newPath.Substring(path.IndexOf('\\') + 1);

      if (!newPath.EndsWith(@":\") && newPath.EndsWith(@"\"))
        newPath = newPath.Substring(0, newPath.Length - 1);

      if (newPath.EndsWith(@"\"))
        newPath = newPath.Substring(0, newPath.Length - 1);

      return newPath;
    }
  }
}