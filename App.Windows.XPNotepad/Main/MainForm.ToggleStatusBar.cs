using System;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void ToggleStatusBar()
    {
      if (statusbarMenuItem.Checked == false)
      {
        statusbarMenuItem.Checked = true;
        bottomStatusBar.Visible = true;
      }
      else
      {
        statusbarMenuItem.Checked = false;
        bottomStatusBar.Visible = false;
      }
    }
  }
}