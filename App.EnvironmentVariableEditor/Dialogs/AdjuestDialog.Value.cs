using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Dialogs
{
  partial class AdjuestDialog
  {
    private string value = "";
    /************************************************/
    public string Value
    {
      get
      {
        string retValue = this.value;
        /************************************************/
        return retValue;
      }
      set
      {
        if (this.value != value)
        {
          this.value = value;
          /************************************************/
          this.valueTextBox.Text = this.value;
        }
      }
    }
  }
}