using System;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserTVContextMenuWrapper
  {
    protected override void WndProc(ref Message m)
    {
        if (iContextMenu != null &&
            m.Msg == (int)WinAPI.WM.MENUSELECT &&
            ((int)ShellHelper.HiWord(m.WParam) & (int)WinAPI.MFT.SEPARATOR) == 0 &&
            ((int)ShellHelper.HiWord(m.WParam) & (int)WinAPI.MFT.POPUP) == 0)
        {
            string info = string.Empty;

            if (ShellHelper.LoWord(m.WParam) == (int)CMD_CUSTOM.ExpandCollapse)
                info = "Expands or collapses the current selected item";
            else
            {
                info = ContextMenuHelper.GetCommandString(
                    iContextMenu,
                    ShellHelper.LoWord(m.WParam) - WinAPI.CMD_FIRST, 
                    false);
            }

            br.OnContextMenuMouseHover(new ContextMenuMouseHoverEventArgs(info.ToString()));
        }

        if (iContextMenu2 != null &&
            (m.Msg == (int)WinAPI.WM.INITMENUPOPUP ||
             m.Msg == (int)WinAPI.WM.MEASUREITEM ||
             m.Msg == (int)WinAPI.WM.DRAWITEM))
        {
            if (iContextMenu2.HandleMenuMsg(
                (uint)m.Msg, m.WParam, m.LParam) == WinAPI.S_OK)
                return;
        }

        if (iContextMenu3 != null &&
            m.Msg == (int)WinAPI.WM.MENUCHAR)
        {
            if (iContextMenu3.HandleMenuMsg2(
                (uint)m.Msg, m.WParam, m.LParam, IntPtr.Zero) == WinAPI.S_OK)
                return;
        }

        base.WndProc(ref m);
    }

  }
}