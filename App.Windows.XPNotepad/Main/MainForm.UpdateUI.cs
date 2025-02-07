using System;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void UpdateUI()
    {
      // TODO: do anything...
      if (notepadTextBox.SelectedText != "")
      {
        cutMenuItem.Enabled = true;
        deleteMenuItem.Enabled = true;
        copyMenuItem.Enabled = true;
      }
      else
      {
        cutMenuItem.Enabled = false;
        deleteMenuItem.Enabled = false;
        copyMenuItem.Enabled = false;
      }
    }
  }
}