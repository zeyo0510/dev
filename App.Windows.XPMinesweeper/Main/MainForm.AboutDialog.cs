using System;
using System.Runtime.InteropServices;
/************************************************/
namespace App.Windows.XPMinesweeper.Main
{
  partial class MainForm
  {
    [DllImport("shell32.dll", EntryPoint="ShellAbout")]
    private static extern int ShellAbout(IntPtr hwnd, string szApp, string szOtherStuff, IntPtr hIcon);
    /************************************************/
    public void AboutDialog()
    {
      MainForm.ShellAbout(base.Handle, base.Text, "by JC", base.Icon.Handle);
    }
  }
}