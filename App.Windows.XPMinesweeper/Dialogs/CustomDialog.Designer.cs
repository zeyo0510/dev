using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Dialogs
{
  partial class CustomDialog
  {
    private Container components = null;
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
      this.heightLabel   = new Label();
      this.heightTextBox = new TextBox();
      this.widthLabel    = new Label();
      this.widthTextBox  = new TextBox();
      this.minesLabel    = new Label();
      this.minesTextBox  = new TextBox();
      this.okButton      = new Button();
      this.cancelButton  = new Button();
      /************************************************/
      this.SuspendLayout();
      /************************************************/
      // heightLabel
      {
        this.heightLabel.Name     = "heightLabel";
        this.heightLabel.AutoSize = true;
        this.heightLabel.Location = new Point(12, 24);
        this.heightLabel.Size     = new Size(54, 17);
        this.heightLabel.Text     = "&Height:";
      }
      // heightTextBox
      {
        this.heightTextBox.Name        = "heightTextBox";
        this.heightTextBox.BorderStyle = BorderStyle.FixedSingle;
        this.heightTextBox.Location    = new Point(67, 20);
        this.heightTextBox.MaxLength   = 5;
        this.heightTextBox.Size        = new Size(40, 21);
        this.heightTextBox.Text        = "";
      }
      // widthLabel
      {
        this.widthLabel.Name     = "widthLabel";
        this.widthLabel.AutoSize = true;
        this.widthLabel.Location = new Point(12, 52);
        this.widthLabel.Size     = new Size(54, 17);
        this.widthLabel.Text     = "&Width:";
      }
      // widthTextBox
      {
        this.widthTextBox.Name        = "widthTextBox";
        this.widthTextBox.BorderStyle = BorderStyle.FixedSingle;
        this.widthTextBox.Location    = new Point(67, 48);
        this.widthTextBox.MaxLength   = 5;
        this.widthTextBox.Size        = new Size(40, 21);
        this.widthTextBox.Text        = "";
      }
      // minesLabel
      {
        this.minesLabel.Name     = "minesLabel";
        this.minesLabel.AutoSize = true;
        this.minesLabel.Location = new Point(12, 80);
        this.minesLabel.Size     = new Size(54, 17);
        this.minesLabel.Text     = "&Mines:";
      }
      // minesTextBox
      {
        this.minesTextBox.Name        = "minesTextBox";
        this.minesTextBox.BorderStyle = BorderStyle.FixedSingle;
        this.minesTextBox.Location    = new Point(67, 76);
        this.minesTextBox.MaxLength   = 5;
        this.minesTextBox.Size        = new Size(40, 21);
        this.minesTextBox.Text        = "";
      }
      // okButton
      {
        this.okButton.Name      = "okButton";
        this.okButton.FlatStyle = FlatStyle.Popup;
        this.okButton.Location  = new Point(124, 32);
        this.okButton.Size      = new Size(48, 23);
        this.okButton.Text      = "OK";
        /************************************************/
        this.okButton.Click += this.okButton_Click;
      }
      // cancelButton
      {
        this.cancelButton.Name         = "cancelButton";
        this.cancelButton.DialogResult = DialogResult.Cancel;
        this.cancelButton.FlatStyle    = FlatStyle.Popup;
        this.cancelButton.Location     = new Point(124, 66);
        this.cancelButton.Size         = new Size(48, 23);
        this.cancelButton.Text         = "Cancel";
      }
      // CustomDialog
      {
        this.Name              = "CustomDialog";
        this.AcceptButton      = this.okButton;
        this.AutoScaleBaseSize = new Size(6, 14);
        this.CancelButton      = this.cancelButton;
        this.ClientSize        = new Size(192, 121);
        this.FormBorderStyle   = FormBorderStyle.FixedDialog;
        this.HelpButton        = true;
        this.MaximizeBox       = false;
        this.MinimizeBox       = false;
        this.ShowInTaskbar     = false;
        this.SizeGripStyle     = SizeGripStyle.Show;
        this.StartPosition     = FormStartPosition.Manual;
        this.Text              = "Custom Field";
        /************************************************/
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.minesTextBox);
        this.Controls.Add(this.widthTextBox);
        this.Controls.Add(this.heightTextBox);
        this.Controls.Add(this.minesLabel);
        this.Controls.Add(this.widthLabel);
        this.Controls.Add(this.heightLabel);
      }
      /************************************************/
      this.ResumeLayout(false);
    }
    /************************************************/
    private  Label   heightLabel   = null;
    internal TextBox heightTextBox = null;
    private  Label   widthLabel    = null;
    internal TextBox widthTextBox  = null;
    private  Label   minesLabel    = null;
    internal TextBox minesTextBox  = null;
    private  Button  okButton      = null;
    private  Button  cancelButton  = null;
  }
}