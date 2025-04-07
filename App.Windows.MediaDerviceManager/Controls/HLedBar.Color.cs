using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HLedBar
  {
    private bool _Color = true;
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
          base.Invalidate();
        }
      }
    }
  }
}