using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPNotepad.Dialogs
{
  partial class GotoDialog
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
      this.linenumberLabel   = new Label();
      this.linenumberTextBox = new TextBox();
      this.okButton          = new Button();
      this.cancelButton      = new Button();
      /************************************************/
      // guiTimer
      {
        this.guiTimer.Enabled  = true;
        this.guiTimer.Interval = 100;
        /************************************************/
        this.guiTimer.Tick += guiTimer_Tick;
      }
      // linenumberLabel
      {
        this.linenumberLabel.Name = "linenumberLabel";
        this.linenumberLabel.AutoSize = true;
        this.linenumberLabel.Location = new System.Drawing.Point(9, 13);
        this.linenumberLabel.Size = new System.Drawing.Size(53, 12);
        this.linenumberLabel.TabIndex = 0;
        this.linenumberLabel.Text = "&Line Number:";
      }
      // linenumberTextBox
      {
        this.linenumberTextBox.Name = "linenumberTextBox";
        this.linenumberTextBox.Location = new System.Drawing.Point(80, 13);
        this.linenumberTextBox.Size = new System.Drawing.Size(90, 21);
        this.linenumberTextBox.TabIndex = 1;
        this.linenumberTextBox.Text = "1";
      }
      // okButton
      {
        this.okButton.Name = "okButton";
        this.okButton.Location = new System.Drawing.Point(11, 54);
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 2;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        /************************************************/
        this.okButton.Click += this.okButton_Click;
      }
      // cancelButton
      {
        this.cancelButton.Name = "button2";
        this.cancelButton.DialogResult = DialogResult.Cancel;
        this.cancelButton.Location = new System.Drawing.Point(118, 54);
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 3;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        /************************************************/
        this.cancelButton.Click += this.cancelButton_Click;
      }
      // GotoDialog
      {
        this.Name = "GotoDialog";
        this.AcceptButton = this.okButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = this.cancelButton;
        this.ClientSize = new System.Drawing.Size(284, 87);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.KeyPreview = true;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.Manual;
        this.Text = "Goto line";
        /************************************************/
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.linenumberTextBox);
        this.Controls.Add(this.linenumberLabel);
        /************************************************/
        this.Load += this.GotoDialog_Load;
      }
    }
    /************************************************/
    private Label   linenumberLabel   = null;
    private TextBox linenumberTextBox = null;
    private Button  okButton          = null;
    private Button  cancelButton      = null;
  }
}