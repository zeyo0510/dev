namespace CheVolume.Controls;
/************************************************/
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

  private void _OnMouseLeave(object sender, EventArgs e)
  {
  }

  private void _OnMouseEnter(object sender, EventArgs e)
  {
  }
}