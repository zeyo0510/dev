namespace CheVolume.Controls;
/************************************************/
partial class AudioFlowLayoutPanel
{
  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    /************************************************/
    base.Invalidate();
  }
}