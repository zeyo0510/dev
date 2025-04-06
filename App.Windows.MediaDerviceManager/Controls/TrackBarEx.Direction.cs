using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class TrackBarEx
  {
    private LayoutDirection _Direction = LayoutDirection.LeftToRight;
    /************************************************/
    public LayoutDirection Direction
    {
      get
      {
        LayoutDirection retValue = this._Direction;
        /************************************************/
        return retValue;
      }
      set
      {
        if (this._Direction == value) return;
        /************************************************/
        this._Direction = value;
        /************************************************/
        base.Invalidate();
      }
    }
  }
}