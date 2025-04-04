using System;
using System.Windows.Forms;

namespace CheVolume.Controls
{
  public class CheCheckBox : CheckBox
  {
    public CheCheckBox()
    {
      SetStyle(ControlStyles.Selectable, false);
    }

    private void InitializeComponent()
    {
      base.SuspendLayout();
      base.MouseEnter += new System.EventHandler(_OnMouseEnter);
      base.MouseLeave += new System.EventHandler(_OnMouseLeave);
      base.ResumeLayout(false);
    }

    private void _OnMouseLeave(object P_0, EventArgs P_1)
    {
    }

    private void _OnMouseEnter(object P_0, EventArgs P_1)
    {
    }
  }
}