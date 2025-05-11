using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class AnalyzeDialog
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
      this.groupBox1 = new GroupBox();
      this.countlabel = new Label();
      this.scanningProgressBar = new ProgressBar();
      this.stopButton = new Button();
      this.currentfileTextBox = new TextBox();
      /************************************************/
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      /************************************************/
      // guiTimer
      {
        this.guiTimer.Enabled = true;
        this.guiTimer.Interval = 100;
        /************************************************/
        this.guiTimer.Tick += this.guiTimer_Tick;
      }
      // groupBox1
      {
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Location = new Point(6, 3);
        this.groupBox1.Size = new Size(379, 111);
        /************************************************/
        this.groupBox1.Controls.Add(this.countlabel);
        this.groupBox1.Controls.Add(this.scanningProgressBar);
        this.groupBox1.Controls.Add(this.stopButton);
        this.groupBox1.Controls.Add(this.currentfileTextBox);
      }
      // countlabel
      {
        this.countlabel.Name = "countlabel";
        this.countlabel.AutoSize = true;
        this.countlabel.ImeMode = ImeMode.NoControl;
        this.countlabel.Location = new Point(6, 16);
        this.countlabel.Size = new Size(67, 13);
        this.countlabel.Text = "Files Found: ";
      }
      // scanningProgressBar
      {
        this.scanningProgressBar.Name = "scanningProgressBar";
        this.scanningProgressBar.Location = new Point(9, 32);
        this.scanningProgressBar.Size = new Size(301, 24);
        this.scanningProgressBar.Style = ProgressBarStyle.Marquee;
      }
      // stopButton
      {
        this.stopButton.Name = "stopButton";
        this.stopButton.Location = new Point(316, 33);
        this.stopButton.Size = new Size(57, 23);
        this.stopButton.Text = "Stop";
        /************************************************/
        this.stopButton.Click += this.stopButton_Click;
      }
      // currentfileTextBox
      {
        this.currentfileTextBox.Name = "currentfileTextBox";
        this.currentfileTextBox.Location = new Point(6, 62);
        this.currentfileTextBox.Multiline = true;
        this.currentfileTextBox.ReadOnly = true;
        this.currentfileTextBox.RightToLeft = RightToLeft.No;
        this.currentfileTextBox.Size = new Size(367, 40);
      }
      // AnalyzeDialog
      {
        this.Name = "AnalyzeDialog";
        this.AutoScaleDimensions = new SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(393, 119);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Analyzing...";
        /************************************************/
        this.Controls.Add(this.groupBox1);
      }
      /************************************************/
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
    }
    /************************************************/
    private GroupBox groupBox1 = null;
    private Label countlabel = null;
    private ProgressBar scanningProgressBar = null;
    private Button stopButton = null;
    private TextBox currentfileTextBox = null;
  }
}