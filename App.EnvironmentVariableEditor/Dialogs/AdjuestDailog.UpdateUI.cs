using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Dialogs
{
  partial class AdjuestDailog
  {
    public void UpdateUI()
    {
      // TODO: do anything...
      this.  keyLabel  .Enabled = true;
      this.  keyTextBox.Enabled = true;
      this.valueLabel  .Enabled = true;
      this.valueTextBox.Enabled = true;
      /************************************************/
      this.    okButton.Enabled = this.Key != "" && this.Value != "";
      this.cancelButton.Enabled = true;
    }
  }
}