using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VTrackBar
  {
    private int _Min = 0;
    /************************************************/
    public int Minimum
    {
      get
      {
        int retValue = this._Min;
        /************************************************/
        return retValue;
      }
      set
      {
        value = Math.Min(value, this.Maximum);
        /************************************************/
        if (value == this._Min) return;
        /************************************************/
        this._Min = value;
        /************************************************/
        if (this.Value < this._Min)
        {
          this.Value = this._Min;
        }
        /************************************************/
        base.Invalidate();
      }
    }
  }
}