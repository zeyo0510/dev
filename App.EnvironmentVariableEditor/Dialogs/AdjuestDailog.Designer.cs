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
      this.keyLabel      = new Label();
      this.keyTextBox    = new TextBox();
      this.valueLabel    = new Label();
      this.valueTextBox  = new TextBox();
      this.valueComboBox = new ComboBox();
      this.upButton      = new Button();
      this.downButton    = new Button();
      this.okButton      = new Button();
      this.cancelButton  = new Button();
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
        this.keyLabel.BorderStyle = BorderStyle.FixedSingle;
        this.keyLabel.Location    = new Point(5, 5);
        this.keyLabel.Size        = new Size(100, 26);
        this.keyLabel.Text        = "Key:";
        /************************************************/
        this.keyLabel.Click += this.keyLabel_Click;
      }
      // keyTextBox
      {
        this.keyTextBox.Name     = "keyTextBox";
        this.keyTextBox.Location = new Point(110, 5);
        this.keyTextBox.Size      = new Size(150, 26);
        /************************************************/
        this.keyTextBox.TextChanged += this.keyTextBox_TextChanged;
      }
      // valueLabel
      {
        this.valueLabel.Name        = "valueLabel";
        this.valueLabel.BorderStyle = BorderStyle.FixedSingle;
        this.valueLabel.Location    = new Point(5, 36);
        this.valueLabel.Size        = new Size(100, 26);
        this.valueLabel.Text        = "Value:";
        /************************************************/
        this.valueLabel.Click += this.valueLabel_Click;
      }
      // valueTextBox
      {
        this.valueTextBox.Name     = "valueTextBox";
        this.valueTextBox.Location = new Point(110, 36);
        this.valueTextBox.Size     = new Size(150, 26);
        /************************************************/
        this.valueTextBox.TextChanged += this.valueTextBox_TextChanged;
      }
      // valueComboBox
      {
        this.valueComboBox.Name = "valueComboBox";
        this.valueComboBox.Location = new Point(110, 36);
        this.valueComboBox.DropDownStyle = ComboBoxStyle.Simple;
        this.valueComboBox.Items.Add("A");
        this.valueComboBox.Items.Add("B");
        this.valueComboBox.Items.Add("C");
      }
      // upButton
      {
        this.upButton.Name     = "upButton";
        this.upButton.Location = new Point(265, 36);
        this.upButton.Size     = new Size(26, 26);
        this.upButton.Text     = "↑";
        /************************************************/
        this.upButton.Click += this.upButton_Click;
      }
      // downButton
      {
        this.downButton.Name     = "downButton";
        this.downButton.Location = new Point(265, 67);
        this.downButton.Size     = new Size(26, 26);
        this.downButton.Text     = "↓";
        /************************************************/
        this.downButton.Click += this.downButton_Click;
      }
      // okButton
      {
        this.okButton.Name     = "okButton";
        this.okButton.Location = new Point(5, 67);
        this.okButton.Size     = new Size(100, 26);
        this.okButton.Text     = "OK";
        /************************************************/
        this.okButton.Click += this.okButton_Click;
      }
      // cancelButton
      {
        this.cancelButton.Name     = "cancelButton";
        this.cancelButton.Location = new Point(120, 67);
        this.cancelButton.Size     = new Size(100, 26);
        this.cancelButton.Text     = "Cancel";
        /************************************************/
        this.cancelButton.Click += this.cancelButton_Click;
      }
      // AdjuestDailog
      {
        base.Name          = "AdjuestDailog";
        base.AcceptButton  = this.okButton;
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton  = this.cancelButton;
        base.Font          = new Font(FontFamily.GenericMonospace, 8f);
        base.Text          = "Adjuest";
        /************************************************/
        base.Controls.Add(this.keyLabel);
        base.Controls.Add(this.keyTextBox);
        base.Controls.Add(this.valueLabel);
//        base.Controls.Add(this.valueTextBox);
        base.Controls.Add(this.valueComboBox);
        base.Controls.Add(this.upButton);
        base.Controls.Add(this.downButton);
        base.Controls.Add(this.okButton);
        base.Controls.Add(this.cancelButton);
      }
    }
    /************************************************/
    private Label    keyLabel      = null;
    private TextBox  keyTextBox    = null;
    private Label    valueLabel    = null;
    private TextBox  valueTextBox  = null;
    private ComboBox valueComboBox = null;
    private Button   upButton      = null;
    private Button   downButton    = null;
    private Button   okButton      = null;
    private Button   cancelButton  = null;
  }
}