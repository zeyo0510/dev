using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariableEditor.Dialogs
{
  partial class AdjuestDailog
  {
    public void AdjuestClientSize()
    {
      float w = Screen.PrimaryScreen.WorkingArea.Width  * 0.75f;
      float h = Screen.PrimaryScreen.WorkingArea.Height * 0.75f;
      /************************************************/
      base.ClientSize = Size.Round(new SizeF(w, h));
      /************************************************/
      base.CenterToParent();
    }
  }
}