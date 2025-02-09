using System;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void InsertCurrentTimeDate()
    {
      string time = DateTime.Now.ToShortTimeString();
      string date = DateTime.Now.ToShortDateString();
      /************************************************/
      this.notepadTextBox.Paste(time + " " + date);
    }
  }
}