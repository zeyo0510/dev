using a;
using CheVolume.Controls;
using System.ComponentModel;
/************************************************/
namespace CheVolume.Forms;
/************************************************/
partial class MainForm : Form
{
  private IContainer components;
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
    this.components = new System.ComponentModel.Container();
    // System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheVolume.Forms.Main));
    this.topMenuStrip = new();
    this.fileToolStripMenuItem = new();
    this.advancedToolStripMenuItem = new();
    this.settingsToolStripMenuItem = new();
    this.exitToolStripMenuItem = new();
    this.pnlSessMgr = new a.AudioSessionManagerPanel();
    base.SuspendLayout();
    /************************************************/
    // topMenuStrip
    this.topMenuStrip.Name = "topMenuStrip";
    this.topMenuStrip.Items.Add(this.fileToolStripMenuItem);
    // fileToolStripMenuItem
    this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
    this.fileToolStripMenuItem.Text = "File";
    this.fileToolStripMenuItem.DropDownItems.Add(this.advancedToolStripMenuItem);
    this.fileToolStripMenuItem.DropDownItems.Add(this.settingsToolStripMenuItem);
    this.fileToolStripMenuItem.DropDownItems.Add(this.exitToolStripMenuItem);
    // advancedToolStripMenuItem
    this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
    this.advancedToolStripMenuItem.Text = "Advanced";
    this.advancedToolStripMenuItem.Click += this.advancedToolStripMenuItem_Click;
    // settingsToolStripMenuItem
    this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
    this.settingsToolStripMenuItem.Text = "Settings";
    this.settingsToolStripMenuItem.Click += this.settingsToolStripMenuItem_Click;
    // exitToolStripMenuItem
    this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
    this.exitToolStripMenuItem.Text = "Exit";
    this.exitToolStripMenuItem.Click += this.exitToolStripMenuItem_Click;
    // pnlSessMgr
    this.pnlSessMgr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
    this.pnlSessMgr.AutoScroll = true;
    this.pnlSessMgr.AutoSize = true;
    this.pnlSessMgr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
    this.pnlSessMgr.Location = new System.Drawing.Point(15, 142);
    this.pnlSessMgr.Margin = new System.Windows.Forms.Padding(0, 0, 27, 0);
    this.pnlSessMgr.MinimumSize = new System.Drawing.Size(747, 625);
    this.pnlSessMgr.Name = "pnlSessMgr";
    this.pnlSessMgr.PenColor = System.Drawing.Color.Empty;
    this.pnlSessMgr.Size = new System.Drawing.Size(747, 625);
    this.pnlSessMgr.TabIndex = 2;
    this.pnlSessMgr.Thickness = 0;
    this.pnlSessMgr.WrapContents = false;
    // MainForm
    base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
    base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.BackColor = System.Drawing.Color.White;
    base.ClientSize = new System.Drawing.Size(1045, 756);
    base.Controls.Add(this.pnlSessMgr);
    this.DoubleBuffered = true;
    // base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
    base.Margin = new System.Windows.Forms.Padding(4);
    this.MaximumSize = new System.Drawing.Size(3994, 803);
    this.MinimumSize = new System.Drawing.Size(794, 803);
    base.Name = "MainForm";
    base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
    this.Text = "CheVolume";
    base.Controls.Add(this.topMenuStrip);
    base.ResumeLayout(false);
    base.PerformLayout();
  }
  /************************************************/
  private MenuStrip topMenuStrip;
  private ToolStripMenuItem fileToolStripMenuItem;
  private ToolStripMenuItem advancedToolStripMenuItem;
  private ToolStripMenuItem settingsToolStripMenuItem;
  private ToolStripMenuItem exitToolStripMenuItem;
  private AudioSessionManagerPanel pnlSessMgr;
}