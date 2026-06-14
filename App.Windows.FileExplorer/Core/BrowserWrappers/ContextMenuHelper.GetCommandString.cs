using System;
using System.Text;
using ShellDll;

namespace FileBrowser
{
  partial class ContextMenuHelper
  {
    public static string GetCommandString(IContextMenu iContextMenu, uint idcmd, bool executeString)
    {
      string command = GetCommandStringW(iContextMenu, idcmd, executeString);

      if (string.IsNullOrEmpty(command))
        command = GetCommandStringA(iContextMenu, idcmd, executeString);

      return command;
    }

    public static string GetCommandStringA(IContextMenu iContextMenu, uint idcmd, bool executeString)
    {
      string info = string.Empty;
      byte[] bytes = new byte[256];
      int index;

      iContextMenu.GetCommandString(idcmd, (executeString ? WinAPI.GCS.VERBA : WinAPI.GCS.HELPTEXTA), 0, bytes, WinAPI.MAX_PATH);

      index = 0;
      while (index < bytes.Length && bytes[index] != 0)
      {
        index++;
      }

      if (index < bytes.Length)
          info = Encoding.Default.GetString(bytes, 0, index);

      return info;
    }
    
    public static string GetCommandStringW(IContextMenu iContextMenu, uint idcmd, bool executeString)
    {
        string info = string.Empty;
        byte[] bytes = new byte[256];
        int index;

        iContextMenu.GetCommandString(
            idcmd,
            (executeString ? WinAPI.GCS.VERBW : WinAPI.GCS.HELPTEXTW),
            0,
            bytes,
            WinAPI.MAX_PATH);

        index = 0;
        while (index < bytes.Length - 1 && (bytes[index] != 0 || bytes[index + 1] != 0))
        { index += 2; }

        if (index < bytes.Length - 1)
            info = Encoding.Unicode.GetString(bytes, 0, index + 1);

        return info;
    }
  }
}