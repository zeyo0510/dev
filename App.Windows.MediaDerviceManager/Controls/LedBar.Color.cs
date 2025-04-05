using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class LedBar
  {
    private bool _Color = false;
    /************************************************/
    public bool Color
    {
      get
      {
        bool retValue = this._Color;
        /************************************************/
        return retValue;
      }
      set
      {
        if (value != this._Color)
        {
          this._Color = value;
          /************************************************/
          this.UpdateUI();
        }
      }
    }
  }
}