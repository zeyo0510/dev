namespace B
{
  partial class FlowLayoutPanel1
  {
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      /************************************************/
      base.Invalidate();
    }
  }
}