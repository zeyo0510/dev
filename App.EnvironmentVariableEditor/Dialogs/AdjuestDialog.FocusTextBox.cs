using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Dialogs
{
  partial class AdjuestDialog
  {
    public void FocusTextBox(string s)
    {
      if (s == "key"  ) this.  keyTextBox.Focus();
      if (s == "value") this.valueTextBox.Focus();
    }
  }
}