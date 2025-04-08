using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HTrackBar
  {
    private HDirection _Direction = HDirection.LeftRight;
    /************************************************/
    public HDirection Direction
    {
      get
      {
        HDirection retValue = this._Direction;
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