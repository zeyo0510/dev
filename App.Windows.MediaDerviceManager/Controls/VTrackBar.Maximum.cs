using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VTrackBar
  {
    private int _Max = 100;
    /************************************************/
    public int Maximum
    {
      get
      {
        int retValue = this._Max;
        /************************************************/
        return retValue;
      }
      set
      {
        value = Math.Max(value, this.Minimum);
        /************************************************/
        if (value == this._Max) return;
        /************************************************/
        this._Max = value;
        /************************************************/
        if (this.Value > this._Max)
        {
          this.Value = this._Max;
        }
        /************************************************/
        base.Invalidate();
      }
    }
  }
}