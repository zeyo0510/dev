using System;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void CopySelectedWordToClipboard()
    {
      this.notepadTextBox.Copy();
    }
  }
}