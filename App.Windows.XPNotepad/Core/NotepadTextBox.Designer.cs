using System;
using System.ComponentModel;
/************************************************/
namespace App.Windows.XPNotepad.Core
{
  partial class NotepadTextBox
  {
    private IContainer components = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.components != null)
        {
          this.components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      // NotepadTextBox
      {
        base.Name = "NotepadTextBox";
      }
    }
  }
}