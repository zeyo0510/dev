using System;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void DeleteSelectedWord()
    {
      this.notepadTextBox.SelectedText = "";
    }
  }
}