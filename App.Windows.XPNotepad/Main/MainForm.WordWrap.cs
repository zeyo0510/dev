using System;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    private bool tmpStatusBar = false;
    /************************************************/
    public void WordWrap()
    {
      this.notepadTextBox.WordWrap = !this.notepadTextBox.WordWrap;
      /************************************************/
      if (this.notepadTextBox.WordWrap)
      {
        this.tmpStatusBar = this.bottomStatusBar.Visible;
      }
      /************************************************/
      this.bottomStatusBar.Visible = !this.notepadTextBox.WordWrap && this.tmpStatusBar;
    }
  }
}