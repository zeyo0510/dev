using System;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void CutSelectedWordToClipboard()
    {
      this.notepadTextBox.Cut();
    }
  }
}