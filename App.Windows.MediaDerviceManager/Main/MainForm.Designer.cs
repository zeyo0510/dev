using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using App.Windows.MediaDerviceManager.Controls;
/************************************************/
namespace App.Windows.MediaDerviceManager.Main
{
  partial class MainForm
  {
    private IContainer components;
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
      this.toolToolStripContainer1 = new ToolStripContainer();
      this.audioSessionManagerPanel1 = new AudioSessionManagerPanel();
//      this.HDeviceVolumeControl1 = new HDeviceVolumeControl(mmDeviceCollection1[0]);
      this.topMenuStrip = new MenuStrip();
      this.systemToolStripMenuItem = new ToolStripMenuItem();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.viewToolStripMenuItem = new ToolStripMenuItem();
      this.advancedToolStripMenuItem = new ToolStripMenuItem();
      this.windowToolStripMenuItem = new ToolStripMenuItem();
      this.pinToolStripMenuItem = new ToolStripMenuItem();
      /************************************************/
      base.SuspendLayout();
      /************************************************/
      // toolToolStripContainer1
      {
        this.toolToolStripContainer1.Name = "toolToolStripContainer1";
        this.toolToolStripContainer1.Dock = DockStyle.Fill;
        /************************************************/
        this.toolToolStripContainer1.ContentPanel.Controls.Add(this.audioSessionManagerPanel1);
      }
      // audioSessionManagerPanel1
      {
        this.audioSessionManagerPanel1.Name = "audioSessionManagerPanel1";
        this.audioSessionManagerPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.audioSessionManagerPanel1.AutoScroll = true;
        this.audioSessionManagerPanel1.AutoSize = true;
        this.audioSessionManagerPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.audioSessionManagerPanel1.Location = new Point(5, 5);
        this.audioSessionManagerPanel1.MinimumSize = new System.Drawing.Size(747, 625);
        this.audioSessionManagerPanel1.PenColor = System.Drawing.Color.Empty;
        this.audioSessionManagerPanel1.Thickness = 0;
        this.audioSessionManagerPanel1.WrapContents = false;
      }
      // HDeviceVolumeControl1
      {
//        this.HDeviceVolumeControl1.Name = "HDeviceVolumeControl1";
//        this.HDeviceVolumeControl1.Location = new Point(500, 25);
      }
      // topMenuStrip
      {
        this.topMenuStrip.Name = "topMenuStrip";
        this.topMenuStrip.Dock = DockStyle.Top;
        /************************************************/
        this.topMenuStrip.Items.Add(this.fileToolStripMenuItem);
        this.topMenuStrip.Items.Add(this.viewToolStripMenuItem);
        this.topMenuStrip.Items.Add(this.windowToolStripMenuItem);
      }
      // fileToolStripMenuItem
      {
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.Text = "File";
        /************************************************/
        this.fileToolStripMenuItem.DropDownItems.Add(this.systemToolStripMenuItem);
        this.fileToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.fileToolStripMenuItem.DropDownItems.Add(this.exitToolStripMenuItem);
        /************************************************/
        this.fileToolStripMenuItem.DropDownOpening += this.fileToolStripMenuItem_DropDownOpening;
      }
      // systemToolStripMenuItem
      {
        this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
        this.systemToolStripMenuItem.Text = "System";
        /************************************************/
        this.systemToolStripMenuItem.Click += this.systemToolStripMenuItem_Click;
      }
      // exitToolStripMenuItem
      {
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        this.exitToolStripMenuItem.Text = "Exit";
        /************************************************/
        this.exitToolStripMenuItem.Click += this.exitToolStripMenuItem_Click;
      }
      // viewToolStripMenuItem
      {
        this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
        this.viewToolStripMenuItem.Text = "View";
        /************************************************/
        this.viewToolStripMenuItem.DropDownItems.Add(this.advancedToolStripMenuItem);
        /************************************************/
        this.viewToolStripMenuItem.DropDownOpening += this.viewToolStripMenuItem_DropDownOpening;
      }
      // advancedToolStripMenuItem
      {
        this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
        this.advancedToolStripMenuItem.Text ="Advanced";
        /************************************************/
        this.advancedToolStripMenuItem.Click += this.advancedToolStripMenuItem_Click;
      }
      // windowToolStripMenuItem
      {
        this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
        this.windowToolStripMenuItem.Text = "Window";
        /************************************************/
        this.windowToolStripMenuItem.DropDownItems.Add(this.pinToolStripMenuItem);
        /************************************************/
        this.windowToolStripMenuItem.DropDownOpening += this.windowToolStripMenuItem_DropDownOpening;
      }
      // pinToolStripMenuItem
      {
        this.pinToolStripMenuItem.Name = "pinToolStripMenuItem";
        this.pinToolStripMenuItem.Text = "Pin";
        /************************************************/
        this.pinToolStripMenuItem.Click += this.pinToolStripMenuItem_Click;
      }
      
      
      
      
      
      

      base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
      base.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      base.ClientSize = new System.Drawing.Size(1045, 756);
//      base.Controls.Add(this.HDeviceVolumeControl1);
//base.Controls.Add(new TrackBarEx() { Width = 250, Height = 250, Location = new Point(5, 5), Direction = LayoutDirection.RightToLeft } );
      base.Controls.Add(this.toolToolStripContainer1);
      base.Controls.Add(this.topMenuStrip);
      this.DoubleBuffered = true;
      this.MaximumSize = new System.Drawing.Size(3994, 803);
      this.MinimumSize = new System.Drawing.Size(794, 803);
      base.Name = "Main";
      base.SizeGripStyle = SizeGripStyle.Show;
      this.Text = "Windows.MediaDerviceManager";
      base.FormClosing += new FormClosingEventHandler(_OnFormClosing);
      /************************************************/
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    private ToolStripContainer toolToolStripContainer1 = null;
    private AudioSessionManagerPanel audioSessionManagerPanel1 = null;
//    private HDeviceVolumeControl HDeviceVolumeControl1 = null;
    private MenuStrip topMenuStrip = null;
    private ToolStripMenuItem fileToolStripMenuItem = null;
    private ToolStripMenuItem systemToolStripMenuItem = null;
    private ToolStripMenuItem exitToolStripMenuItem = null;
    private ToolStripMenuItem viewToolStripMenuItem = null;
    private ToolStripMenuItem advancedToolStripMenuItem = null;
    private ToolStripMenuItem windowToolStripMenuItem = null;
    private ToolStripMenuItem pinToolStripMenuItem = null;
  }
}