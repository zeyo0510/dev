using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Dialogs
{
  partial class AdjuestDialog
  {
    private string key = "";
    /************************************************/
    public string Key
    {
      get
      {
        string retValue = this.key;
        /************************************************/
        return retValue;
      }
      set
      {
        if (this.key != value)
        {
          this.key = value;
          /************************************************/
          this.keyTextBox.Text = this.key;
        }
      }
    }
  }
}