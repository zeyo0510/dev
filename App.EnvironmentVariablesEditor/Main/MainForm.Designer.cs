using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariablesEditor.Main
{
  partial class MainForm
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
      this.topMenuStrip          = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      /************************************************/
      // guiTimer
      {
        this.guiTimer.Enabled  = true;
        this.guiTimer.Interval = 100;
        /************************************************/
        this.guiTimer.Tick += this.guiTimer_Tick;
      }
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
        this.fileToolStripMenuItem.DropDownItems.Add(this.exitToolStripMenuItem);
        /************************************************/
        this.fileToolStripMenuItem.DropDownOpening += this.fileToolStripMenuItem_DropDownOpening;
      }
      // exitToolStripMenuItem
      {
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        this.exitToolStripMenuItem.Text = "Exit";
        /************************************************/
        this.exitToolStripMenuItem.Click += this.exitToolStripMenuItem_Click;
      }
      // MainForm
      {
        base.Name          = "MainForm";
        base.AutoScaleMode = AutoScaleMode.Font;
        base.Text          = "EnvironmentVariablesEditor";
        /************************************************/
        base.Controls.Add(this.topMenuStrip);
      }
    }
    /************************************************/
    private MenuStrip         topMenuStrip          = null;
    private ToolStripMenuItem fileToolStripMenuItem = null;
    private ToolStripMenuItem exitToolStripMenuItem = null;
  }
}