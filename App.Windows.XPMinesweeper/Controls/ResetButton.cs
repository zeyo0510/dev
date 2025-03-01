using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public partial class ResetButton : Button
  {
    private Color gray = Color.Silver;
    private Color darkGray = Color.Gray;
    private Pen lightPen,  darkGrayPen, grayPen;
    private Brush grayBrush;
    /************************************************/
    public ResetButton()
    {
      InitializeComponent();

      SetStyle(ControlStyles.Selectable, false);
      BackColor = gray;
      Width = 26;
      Height = 26;
    }

    protected override CreateParams CreateParams
    {
      get
      {
        //const int WS_EX_CLIENTEDGE = 0x200;
        CreateParams cp = base.CreateParams;
        //cp.ExStyle = cp.ExStyle | WS_EX_CLIENTEDGE;
        return cp;
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Rectangle rect = ClientRectangle;
      Graphics g = e.Graphics;

      g.FillRectangle(grayBrush, rect);
      drawFrame(g, new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1));
      if (Image != null)
      {
        int offset;
        if (pressed)
          offset = 1;
        else
          offset = 0;
        g.DrawImage(Image, rect.Left + 4 + offset, rect.Top + 4 + offset);
      }
    }

    private void drawFrame(Graphics g, Rectangle rect)
    {
      if (pressed)
      {
        #region Top Border
        g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
        g.DrawLine(darkGrayPen, rect.Left + 1, rect.Top + 1, rect.Right, rect.Top + 1);
        #endregion

        #region Bottom Border
        g.DrawLine(darkGrayPen, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom);
        #endregion

        #region Left Border
        g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
        g.DrawLine(darkGrayPen, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom);
        #endregion

        #region Right Border
        g.DrawLine(darkGrayPen, rect.Right, rect.Top + 1, rect.Right, rect.Bottom);
        #endregion
      }
      else
      {
        #region Top Border
        g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
        g.DrawLine(lightPen, rect.Left + 1, rect.Top + 1, rect.Right - 2, rect.Top + 1);
        #endregion

        #region Bottom Border
        g.DrawLine(darkGrayPen, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom);
        g.DrawLine(darkGrayPen, rect.Left + 2, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
        g.DrawLine(darkGrayPen, rect.Left + 3, rect.Bottom - 2, rect.Right, rect.Bottom - 2);
        #endregion

        #region Left Border
        g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
        g.DrawLine(lightPen, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 2);
        g.DrawLine(lightPen, rect.Left + 2, rect.Top + 1, rect.Left + 2, rect.Bottom - 3);
        #endregion

        #region Right Border
        g.DrawLine(darkGrayPen, rect.Right, rect.Top + 1, rect.Right, rect.Bottom);
        g.DrawLine(darkGrayPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom);
        g.DrawLine(darkGrayPen, rect.Right - 2, rect.Top + 3, rect.Right - 2, rect.Bottom);
        #endregion
      }
    }

    private bool pressed;

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);

      if (e.Button == MouseButtons.Left)
      {
        pressed = true;
        Invalidate();
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);

      if (e.Button == MouseButtons.Left)
      {
        pressed = false;
        Invalidate();
      }
    }
  }
}