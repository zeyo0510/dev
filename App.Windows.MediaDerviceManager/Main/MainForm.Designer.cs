using System;
using System.ComponentModel;
using System.Windows.Forms;
using a;
using CheVolume.Controls;
/************************************************/
namespace App.Windows.MediaDerviceManager.Main
{
  partial class MainForm
  {
    private IContainer components;
    /************************************************/
    protected override void Dispose(bool P_0)
    {
      if (P_0 && components != null)
      {
        components.Dispose();
      }
      base.Dispose(P_0);
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.components = new Container();
      /************************************************/
      this.topMenuStrip = new MenuStrip();
      this.systemToolStripMenuItem = new ToolStripMenuItem();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.viewToolStripMenuItem = new ToolStripMenuItem();
      this.advancedToolStripMenuItem = new ToolStripMenuItem();
      this.windowToolStripMenuItem = new ToolStripMenuItem();
      this.pinToolStripMenuItem = new ToolStripMenuItem();
      
      
      this.pnlSessMgr = new a.AudioSessionManagerPanel();
      /************************************************/
      base.SuspendLayout();
      /************************************************/
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
      
      
      
      
      
      
      this.pnlSessMgr.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.pnlSessMgr.AutoScroll = true;
      this.pnlSessMgr.AutoSize = true;
      this.pnlSessMgr.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.pnlSessMgr.Location = new System.Drawing.Point(15, 142);
      this.pnlSessMgr.Margin = new Padding(0, 0, 27, 0);
      this.pnlSessMgr.MinimumSize = new System.Drawing.Size(747, 625);
      this.pnlSessMgr.Name = "pnlSessMgr";
      this.pnlSessMgr.PenColor = System.Drawing.Color.Empty;
      this.pnlSessMgr.Size = new System.Drawing.Size(747, 625);
      this.pnlSessMgr.TabIndex = 2;
      this.pnlSessMgr.Thickness = 0;
      this.pnlSessMgr.WrapContents = false;
      base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
      base.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      base.ClientSize = new System.Drawing.Size(1045, 756);
      base.Controls.Add(this.pnlSessMgr);
      base.Controls.Add(this.topMenuStrip);
      this.DoubleBuffered = true;
      this.MaximumSize = new System.Drawing.Size(3994, 803);
      this.MinimumSize = new System.Drawing.Size(794, 803);
      base.Name = "Main";
      base.SizeGripStyle = SizeGripStyle.Show;
      this.Text = "CheVolume";
      base.FormClosing += new FormClosingEventHandler(_OnFormClosing);
      /************************************************/
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    private AudioSessionManagerPanel pnlSessMgr;
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