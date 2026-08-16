namespace B
{
  partial class AudioFlowLayoutPanel
  {
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      /************************************************/
      base.Invalidate();
    }
  }
}