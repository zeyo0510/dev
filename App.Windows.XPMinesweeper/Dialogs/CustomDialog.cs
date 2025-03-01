using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Dialogs
{
  public partial class frmCustomGame : Form
  {
    public frmCustomGame()
    {
      this.InitializeComponent();
    }

    private static int strToInt(string text)
    {
      if (Regex.IsMatch(text, @"^-?\d+$"))
        return Int32.Parse(text);
      else
        return 0;
    }

    public static bool ShowSelf(IWin32Window parent, Point location, ref int width, ref int height, ref int mineCount)
    {
      bool result;
      frmCustomGame cg = new frmCustomGame();
      cg.tbHeight.Text = height.ToString();
      cg.tbWidth.Text = width.ToString();
      cg.tbMineCount.Text = mineCount.ToString();
      cg.Location = location;
      if (cg.ShowDialog(parent) == DialogResult.OK)
      {
        width = strToInt(cg.tbWidth.Text);
        height = strToInt(cg.tbHeight.Text);
        mineCount = strToInt(cg.tbMineCount.Text);
        result = true;
      }
      else
        result = false;
      cg.Dispose();
      cg = null;
      return result;
    }



    private void btnOK_Click(object sender, System.EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }
  }
}