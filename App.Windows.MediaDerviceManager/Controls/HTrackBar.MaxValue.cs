using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HTrackBar
  {
    private int _MaxValue = 100;
    /************************************************/
    public int MaxValue
    {
      get
      {
        int retValue = this._MaxValue;
        /************************************************/
        return retValue;
      }
      set
      {
        value = Math.Max(value, this.MinValue);
        /************************************************/
        if (value == this._MaxValue) return;
        /************************************************/
        this._MaxValue = value;
        /************************************************/
        if (this.Value > this._MaxValue) this.Value = this._MaxValue;
        /************************************************/
        base.Invalidate();
      }
    }
  }
}