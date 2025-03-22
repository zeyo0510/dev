using System;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void UpdateUI()
    {
      // TODO: do anything...
      this.   cutMenuItem.Enabled = this.notepadTextBox.SelectedText != "";
      this.  copyMenuItem.Enabled = this.notepadTextBox.SelectedText != "";
      this.deleteMenuItem.Enabled = this.notepadTextBox.SelectedText != "";
      this.  gotoMenuItem.Enabled = !this.notepadTextBox.WordWrap;
      
      this.wordwrapMenuItem.Checked = this.notepadTextBox.WordWrap;
      
      this.statusbarMenuItem.Checked = this.bottomStatusBar.Visible;
      this.statusbarMenuItem.Enabled = !this.notepadTextBox.WordWrap;
    }
  }
}