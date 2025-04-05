using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class LedBar
  {
    private float _Value = 0;
    /************************************************/
    public float Value
    {
      get
      {
        float retValue = this._Value;
        /************************************************/
        return retValue;
      }
      set
      {
        int num = (int)Math.Ceiling(value * 15f);
        /************************************************/
        if (this._Value != num)
        {
          this._Value = num;
          /************************************************/
          this.UpdateUI();
        }
      }
    }
  }
}