using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Dialogs
{
  public partial class CustomDialog : Form
  {
    public CustomDialog()
    {
      this.InitializeComponent();
    }
    /************************************************/
    private void okButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }
    /************************************************/
    private static int strToInt(string text)
    {
      if (Regex.IsMatch(text, @"^-?\d+$"))
        return Int32.Parse(text);
      else
        return 0;
    }
    /************************************************/
    public static bool ShowSelf(IWin32Window parent, Point location, ref int width, ref int height, ref int mineCount)
    {
      bool result;
      CustomDialog cg = new CustomDialog();
      cg.heightTextBox.Text = height.ToString();
      cg.widthTextBox.Text = width.ToString();
      cg.minesTextBox.Text = mineCount.ToString();
      cg.Location = location;
      if (cg.ShowDialog(parent) == DialogResult.OK)
      {
        width = strToInt(cg.widthTextBox.Text);
        height = strToInt(cg.heightTextBox.Text);
        mineCount = strToInt(cg.minesTextBox.Text);
        result = true;
      }
      else
        result = false;
      cg.Dispose();
      cg = null;
      return result;
    }
  }
}