using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VLedBar
  {
    private VDirection  _Direction = VDirection.BottomTop;
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
        if (value != this._Direction)
        {
          this._Direction = value;
          /************************************************/
          base.Invalidate();
        }
      }
    }
  }
}