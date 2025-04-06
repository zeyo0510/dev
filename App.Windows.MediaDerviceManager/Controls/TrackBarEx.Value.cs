using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class TrackBarEx
  {
    private int _value = 50;
    public int Value
    {
      get
      {
        return _value;
      }
      set
      {
        if (value < _min) value = _min;
        if (value > _max) value = _max;
        if (_value != value)
        {
          _value = value;
          if (ValueChanged != null)
            ValueChanged(this, EventArgs.Empty);
          this.Invalidate();
        }
      }
    }
  }
}