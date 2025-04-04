using System.ComponentModel;
using System.Windows.Forms;

namespace CheVolume.Controls
{
  public class CheRadioButton : RadioButton
  {
    private IContainer components;

    public CheRadioButton()
    {
      InitializeComponent();
    }

    private void _OnMouseMove(object P_0, MouseEventArgs P_1)
    {
    }

    protected override void Dispose(bool P_0)
    {
      if (P_0 && components != null)
      {
        components.Dispose();
      }
      base.Dispose(P_0);
    }

    private void InitializeComponent()
    {
      base.SuspendLayout();
      this.Cursor = Cursors.Hand;
      base.MouseMove += new MouseEventHandler(_OnMouseMove);
      base.ResumeLayout(false);
    }
  }
}