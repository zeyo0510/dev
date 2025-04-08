using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HTrackBar
  {
    private int _Value = 50;
    /************************************************/
    public int Value
    {
      get
      {
        int retValue = this._Value;
        /************************************************/
        return retValue;
      }
      set
      {
        if (value < this.MinValue) value = this.MinValue;
        if (value > this.MaxValue) value = this.MaxValue;
        /************************************************/
        if (value == this._Value) return;
        /************************************************/
        this._Value = value;
        /************************************************/
        if (this.ValueChanged != null) this.ValueChanged(this, EventArgs.Empty);
        /************************************************/
        base.Invalidate();
      }
    }
  }
}