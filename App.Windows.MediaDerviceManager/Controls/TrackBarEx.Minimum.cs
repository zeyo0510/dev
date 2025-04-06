using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class TrackBarEx
  {
    private int _min = 0;
    public int Minimum
    {
      get { return _min; }
      set
      {
        _min = value;
        if (_value < _min) _value = _min;
        this.Invalidate();
      }
    }
  }
}