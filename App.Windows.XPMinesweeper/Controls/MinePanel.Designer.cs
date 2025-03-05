using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePanel
  {
    private IContainer components = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.components != null)
        {
          this.components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.panel1 = new Control();
      this.panel2 = new Control();
      /************************************************/
      // panel1
      {
        this.panel1.Name     = "panel1";
        this.panel1.Anchor   = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        this.panel1.Location = new Point(11, 11);
        this.panel1.Size     = new Size(146, 033);
      }
      // panel2
      {
        this.panel2.Name     = "panel2";
        this.panel2.Anchor    = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
        this.panel2.Location = new Point(11, 54);
        this.panel2.Size     = new Size(146, 146);
      }
      // MinePanel
      {
        base.Name = "MinePanel";
        base.ClientSize  = new Size(167, 210);
        base.MinimumSize = new Size(167, 210);
        base.Controls.Add(this.panel1);
        base.Controls.Add(this.panel2);
      }
    }
    /************************************************/
    private Control panel1 = null;
    private Control panel2 = null;
  }
}