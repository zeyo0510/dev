namespace B
{
  partial class FlowLayoutPanel1
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      /************************************************/
      Graphics g = e.Graphics;
      /************************************************/
      Rectangle rect = base.ClientRectangle;
      /************************************************/
      this.DrawBorder(g, rect);
    }
    /************************************************/
    private void DrawBorder(Graphics g, Rectangle rect)
    {
      int l = rect.Left   + 0;
      int t = rect.Top    + 0;
      int r = rect.Right  - 1;
      int b = rect.Bottom - 1;
      /************************************************/
      using (Pen pen = new(Color.FromArgb(50, 50, 50)))
      {
        g.DrawRectangle(pen, l, t, r, b);
      }
    }
  }
}