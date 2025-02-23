using System;
using System.Drawing;
/************************************************/
namespace App.EnvironmentVariableEditor.Dialogs
{
  partial class AdjuestDialog
  {
    public void AdjuestClientSize()
    {
      float w = base.Owner.ClientSize.Width  * 0.75f;
      float h = base.Owner.ClientSize.Height * 0.75f;
      /************************************************/
      base.ClientSize = Size.Round(new SizeF(w, h));
      /************************************************/
      base.CenterToParent();
    }
  }
}