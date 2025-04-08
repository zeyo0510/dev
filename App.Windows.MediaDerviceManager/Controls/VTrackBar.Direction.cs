using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VTrackBar
  {
    private VDirection _Direction = VDirection.TopBottom;
    /************************************************/
    public VDirection Direction
    {
      get
      {
        VDirection retValue = this._Direction;
        /************************************************/
        return retValue;
      }
      set
      {
        if (value == this._Direction) return;
        /************************************************/
        this._Direction = value;
        /************************************************/
        base.Invalidate();
      }
    }
  }
}