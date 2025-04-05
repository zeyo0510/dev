using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class LedBar
  {
    private LayoutDirection  _LayoutDirection = LayoutDirection.BottomToTop;
    /************************************************/
    public LayoutDirection LayoutDirection
    {
      get
      {
        LayoutDirection retValue = this._LayoutDirection;
        /************************************************/
        return retValue;
      }
      set
      {
        if (value != this._LayoutDirection)
        {
          this._LayoutDirection = value;
          /************************************************/
          this.UpdateUI();
        }
      }
    }
  }
}