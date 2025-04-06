using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class TrackBarEx
  {
    private int _max = 100;
    public int Maximum
    {
      get { return _max; }
      set
      {
        _max = value;
        if (_value > _max) _value = _max;
        this.Invalidate();
      }
    }
  }
}