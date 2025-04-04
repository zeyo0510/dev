using System.Windows.Forms;

namespace CheVolume.Controls
{
  public class CheToolStripProfessionalRenderer : ToolStripProfessionalRenderer
  {
    public ToolStrip toolTip1;

    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs P_0)
    {
      int num = 1;
      if (P_0.ToolStrip.Items.IndexOf(P_0.Item) != num || P_0.ToolStrip != toolTip1)
      {
        base.OnRenderMenuItemBackground(P_0);
      }
    }
  }
}