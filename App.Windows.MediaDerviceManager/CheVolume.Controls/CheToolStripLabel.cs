using System.Drawing;
using System.Windows.Forms;

namespace CheVolume.Controls
{
  public class CheToolStripLabel : ToolStripLabel
  {
    public override Size GetPreferredSize(Size P_0)
    {
      if (base.IsOnOverflow || base.Owner.Orientation == Orientation.Vertical)
      {
        return DefaultSize;
      }
      int num = base.Owner.DisplayRectangle.Width;
      if (base.Owner.OverflowButton.Visible)
      {
        num = num - base.Owner.OverflowButton.Width - base.Owner.OverflowButton.Margin.Horizontal;
      }
      if (num < DefaultSize.Width)
      {
        num = DefaultSize.Width;
      }
      Size preferredSize = base.GetPreferredSize(P_0);
      preferredSize.Width = num;
      return preferredSize;
    }
  }
}