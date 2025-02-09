using System;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void ShowAboutDialog()
    {
      ShellAbout(base.Handle.ToInt32(), "Notepad", "", base.Icon.Handle.ToInt32());
    }
  }
}
