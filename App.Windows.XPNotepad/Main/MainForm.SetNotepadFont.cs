using System;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void SetNotepadFont()
    {
      FontDialog dialog = new FontDialog();
      {
        dialog.AllowVerticalFonts = false;
        dialog.Font               = this.notepadTextBox.Font;
        dialog.ShowEffects        =  false;
      }
      /************************************************/
      if (dialog.ShowDialog(this) != DialogResult.OK)
      {
        return;
      }
      /************************************************/
      this.notepadTextBox.Font = dialog.Font;
    }
  }
}