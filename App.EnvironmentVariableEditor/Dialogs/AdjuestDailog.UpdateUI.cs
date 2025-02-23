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
      this.  keyTextBox.Enabled = (this.ManipulationMode == ManipulationMode.Insert) ? true  : this.keyTextBox.Enabled;
      this.  keyTextBox.Enabled = (this.ManipulationMode == ManipulationMode.Update) ? false : this.keyTextBox.Enabled;
      this.  keyTextBox.Enabled = (this.ManipulationMode == ManipulationMode.Delete) ? false : this.keyTextBox.Enabled;
      this.valueLabel  .Enabled = true;
      this.valueTextBox.Enabled = (this.ManipulationMode == ManipulationMode.Insert) ? true  : this.valueTextBox.Enabled;
      this.valueTextBox.Enabled = (this.ManipulationMode == ManipulationMode.Update) ? true  : this.valueTextBox.Enabled;
      this.valueTextBox.Enabled = (this.ManipulationMode == ManipulationMode.Delete) ? false : this.valueTextBox.Enabled;
      /************************************************/
      this.    okButton.Enabled = this.Key != "" && this.Value != "";
      this.cancelButton.Enabled = true;
    }
  }
}