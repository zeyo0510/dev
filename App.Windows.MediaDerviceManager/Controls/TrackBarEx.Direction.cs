using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class TrackBarEx
  {
    private LayoutDirection _direction = LayoutDirection.LeftToRight;
    public LayoutDirection Direction
    {
      get { return _direction; }
      set
      {
        if (_direction != value)
        {
          _direction = value;
          this.Invalidate();
        }
      }
    }
  }
}