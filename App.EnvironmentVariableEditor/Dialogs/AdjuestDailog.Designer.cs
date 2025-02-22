using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariableEditor.Dialogs
{
  partial class AdjuestDailog
  {
    private IContainer components = null;
    /************************************************/
    private Timer guiTimer = null;
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
      this.components = new Container();
      /************************************************/
      this.guiTimer = new Timer(this.components);
      /************************************************/
      this.keyLabel     = new Label();
      this.keyTextBox   = new TextBox();
      this.valueLabel   = new Label();
      this.valueTextBox = new TextBox();
      this.cancelButton = new Button();
      this.okButton     = new Button();
      /************************************************/
      // guiTimer
      {
        this.guiTimer.Enabled  = true;
        this.guiTimer.Interval = 100;
        /************************************************/
        this.guiTimer.Tick += this.guiTimer_Tick;
      }
      /************************************************/
      // keyLabel
      {
        this.keyLabel.Name        = "keyLabel";
        this.keyLabel.Anchor      = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        this.keyLabel.AutoSize    = false;
        this.keyLabel.BorderStyle = BorderStyle.FixedSingle;
        this.keyLabel.Location    = new Point(005, 005);
        this.keyLabel.Size        = new Size(274, 020);
        this.keyLabel.Text        = "Key:";
        this.keyLabel.TextAlign   = ContentAlignment.MiddleLeft;
        /************************************************/
        this.keyLabel.Click += this.keyLabel_Click;
      }
      // keyTextBox
      {
        this.keyTextBox.Name     = "keyTextBox";
        this.keyTextBox.Anchor   = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        this.keyTextBox.AutoSize = false;
        this.keyTextBox.Location = new Point(this.keyLabel.Left + 000, this.keyLabel.Bottom + 005);
        this.keyTextBox.Size     = this.keyLabel.Size;
        /************************************************/
        this.keyTextBox.DataBindings.Add(new Binding("Text", this, "Key"));
        /************************************************/
        this.keyTextBox.TextChanged += this.keyTextBox_TextChanged;
      }
      // valueLabel
      {
        this.valueLabel.Name        = "valueLabel";
        this.valueLabel.Anchor      = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        this.valueLabel.AutoSize    = false;
        this.valueLabel.BorderStyle = BorderStyle.FixedSingle;
        this.valueLabel.Location    = new Point(this.keyTextBox.Left + 000, this.keyTextBox.Bottom + 005);
        this.valueLabel.Size        = this.keyTextBox.Size;
        this.valueLabel.Text        = "Value:";
        this.valueLabel.TextAlign   = ContentAlignment.MiddleLeft;
        /************************************************/
        this.valueLabel.Click += this.valueLabel_Click;
      }
      // valueTextBox
      {
        this.valueTextBox.Name      = "valueListBox";
        this.valueTextBox.Anchor    = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        this.valueTextBox.AutoSize  = false;
        this.valueTextBox.Location  = new Point(this.valueLabel.Left + 000, this.valueLabel.Bottom + 005);
        this.valueTextBox.Multiline = true;
        this.valueTextBox.Size      = new Size(this.valueLabel.Width, 148);
        /************************************************/
        this.valueTextBox.DataBindings.Add(new Binding("Text", this, "Value"));
        /************************************************/
        this.valueTextBox.TextChanged += this.valueTextBox_TextChanged;
      }
      // cancelButton
      {
        this.cancelButton.Name         = "cancelButton";
        this.cancelButton.Anchor       = AnchorStyles.Right | AnchorStyles.Bottom;
        this.cancelButton.AutoSize     = false;
        this.cancelButton.DialogResult = DialogResult.Cancel;
        this.cancelButton.Location     = new Point(this.ClientRectangle.Right - 005 - 099, this.ClientRectangle.Bottom - 005 - 024);
        this.cancelButton.Size         = new Size(100, 25);
        this.cancelButton.Text         = "Cancel";
        /************************************************/
        this.cancelButton.Click += this.cancelButton_Click;
      }
      // okButton
      {
        this.okButton.Name         = "okButton";
        this.okButton.Anchor       = AnchorStyles.Right | AnchorStyles.Bottom;
        this.okButton.DialogResult = DialogResult.OK;
        this.okButton.AutoSize     = false;
        this.okButton.Location     = new Point(this.cancelButton.Left - 005 - 100, this.cancelButton.Top - 000);
        this.okButton.Size         = this.cancelButton.Size;
        this.okButton.Text         = "OK";
        /************************************************/
        this.okButton.Click += this.okButton_Click;
      }
      // AdjuestDailog
      {
        base.Name            = "AdjuestDailog";
        base.AcceptButton    = this.okButton;
        base.AutoScaleMode   = AutoScaleMode.None;
        base.CancelButton    = this.cancelButton;
        base.Font            = new Font(FontFamily.GenericMonospace, 10f);
        base.Text            = "Adjuest";
        base.FormBorderStyle = FormBorderStyle.FixedDialog;
        /************************************************/
        base.Controls.Add(this.keyLabel);
        base.Controls.Add(this.keyTextBox);
        base.Controls.Add(this.valueLabel);
        base.Controls.Add(this.valueTextBox);
        base.Controls.Add(this.cancelButton);
        base.Controls.Add(this.okButton);
      }
    }
    /************************************************/
    private Label   keyLabel     = null;
    private TextBox keyTextBox   = null;
    private Label   valueLabel   = null;
    private TextBox valueTextBox = null;
    private Button  cancelButton = null;
    private Button  okButton     = null;
  }
}