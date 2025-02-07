using System;
using System.Runtime.InteropServices;
using System.Text;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    private const int EM_LINEFROMCHAR = 0xC9;
    private const int EM_LINEINDEX = 0xBB;
    
    [DllImport("shell32.dll")]
    public static extern int ExtractIcon(int handle, string path, int index);

    [DllImport("kernel32.dll")]
    public static extern int GetWindowsDirectory(StringBuilder lpBuffer, int uSize);
    
    [DllImport("shell32.dll", EntryPoint = "ShellAbout")]
    private static extern int ShellAbout(int hWndn, string szApp, string szOtherStuff, int hIcon);

    [DllImport("User32.DLL")]
    public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int iParam);
  }
}