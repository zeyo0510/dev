using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HLedBar
  {
    private HDirection  _Direction = HDirection.LeftRight;
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