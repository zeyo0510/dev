using a;
using CheVolume.Controls;
using System.ComponentModel;
/************************************************/
namespace CheVolume.Forms;
/************************************************/
partial class MainForm : Form
{
  private IContainer components = null;
  /************************************************/
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.components?.Dispose();
    }
    /************************************************/
    base.Dispose(disposing);
  }
  /************************************************/
  private void InitializeComponent()
  {
    this.components = new Container();
    /************************************************/
    this.topMenuStrip              = new();
    this.fileToolStripMenuItem     = new();
    this.advancedToolStripMenuItem = new();
    this.settingsToolStripMenuItem = new();
    this.exitToolStripMenuItem     = new();
    this.flowLayoutPanel1          = new();
    /************************************************/
    // topMenuStrip
    {
      this.topMenuStrip.Name = "topMenuStrip";
      /************************************************/
      this.topMenuStrip.Items.Add(this.fileToolStripMenuItem);
    }
    // fileToolStripMenuItem
    {
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Text = "File";
      /************************************************/
      this.fileToolStripMenuItem.DropDownItems.Add(this.advancedToolStripMenuItem);
      this.fileToolStripMenuItem.DropDownItems.Add(this.settingsToolStripMenuItem);
      this.fileToolStripMenuItem.DropDownItems.Add(this.exitToolStripMenuItem);
    }
    // advancedToolStripMenuItem
    {
      this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
      this.advancedToolStripMenuItem.Text = "Advanced";
      /************************************************/
      this.advancedToolStripMenuItem.Click += this.advancedToolStripMenuItem_Click;
    }
    // settingsToolStripMenuItem
    {
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Text = "Settings";
      /************************************************/
      this.settingsToolStripMenuItem.Click += this.settingsToolStripMenuItem_Click;
    }
    // exitToolStripMenuItem
    {
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Text = "Exit";
      /************************************************/
      this.exitToolStripMenuItem.Click += this.exitToolStripMenuItem_Click;
    }
    // flowLayoutPanel1
    {
      this.flowLayoutPanel1.Name         = "flowLayoutPanel1";
      this.flowLayoutPanel1.Dock         = DockStyle.Fill;
      this.flowLayoutPanel1.AutoScroll   = true;
      this.flowLayoutPanel1.AutoSize     = true;
      this.flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.flowLayoutPanel1.BackColor    = Color.Transparent;
      this.flowLayoutPanel1.Margin       = new(50);
      this.flowLayoutPanel1.Padding      = new(2);
      this.flowLayoutPanel1.WrapContents = false;
    }
    // MainForm
    {
      base.Name                = "MainForm";
      base.AutoScaleDimensions = new(8f, 16f);
      base.AutoScaleMode       = AutoScaleMode.Font;
      base.SizeGripStyle       = SizeGripStyle.Show;
      this.DoubleBuffered      = true;
      this.Text                = "CheVolume";
      /************************************************/
      base.Controls.Add(this.flowLayoutPanel1);
      base.Controls.Add(this.topMenuStrip);
    }
  }
  /************************************************/
  private MenuStrip topMenuStrip = null;
  private ToolStripMenuItem fileToolStripMenuItem = null;
  private ToolStripMenuItem advancedToolStripMenuItem = null;
  private ToolStripMenuItem settingsToolStripMenuItem = null;
  private ToolStripMenuItem exitToolStripMenuItem = null;
  private FlowLayoutPanel flowLayoutPanel1 = null;
}