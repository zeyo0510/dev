using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class TrackBarEx
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
        if (value < this.Minimum) value = this.Minimum;
        if (value > this.Maximum) value = this.Maximum;
        /************************************************/
        if (this._Value == value) return;
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