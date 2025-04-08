using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VTrackBar
  {
    private int _MinValue = 0;
    /************************************************/
    public int MinValue
    {
      get
      {
        int retValue = this._MinValue;
        /************************************************/
        return retValue;
      }
      set
      {
        value = Math.Min(value, this.MaxValue);
        /************************************************/
        if (value == this._MinValue) return;
        /************************************************/
        this._MinValue = value;
        /************************************************/
        if (this.Value < this._MinValue) this.Value = this._MinValue;
        /************************************************/
        base.Invalidate();
      }
    }
  }
}