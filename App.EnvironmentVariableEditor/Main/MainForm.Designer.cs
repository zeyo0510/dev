using System;
using System.ComponentModel;
using System.Windows.Forms;
using App.EnvironmentVariableEditor.Controls;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
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
      this.EnvVarListView1              = new EnvVarListView();
      this.topMenuStrip                 = new MenuStrip();
      this.fileToolStripMenuItem        = new ToolStripMenuItem();
      this.importToolStripMenuItem      = new ToolStripMenuItem();
      this.exportToolStripMenuItem      = new ToolStripMenuItem();
      this.exitToolStripMenuItem        = new ToolStripMenuItem();
      this.editToolStripMenuItem        = new ToolStripMenuItem();
      this.undoToolStripMenuItem        = new ToolStripMenuItem();
      this.redoToolStripMenuItem        = new ToolStripMenuItem();
      this.cutToolStripMenuItem         = new ToolStripMenuItem();
      this.copyToolStripMenuItem        = new ToolStripMenuItem();
      this.pasteToolStripMenuItem       = new ToolStripMenuItem();
      this.deleteToolStripMenuItem      = new ToolStripMenuItem();
      this.viewToolStripMenuItem        = new ToolStripMenuItem();
      this.refreshToolStripMenuItem     = new ToolStripMenuItem();
      this.statusbarToolStripMenuItem   = new ToolStripMenuItem();
      this.bottomStatusStrip            = new StatusStrip();
      this.messsageToolStripStatusLabel = new ToolStripStatusLabel();
      /************************************************/
      // guiTimer
      {
        this.guiTimer.Enabled  = true;
        this.guiTimer.Interval = 100;
        /************************************************/
        this.guiTimer.Tick += this.guiTimer_Tick;
      }
      // EnvVarListView1
      {
        this.EnvVarListView1.Name = "EnvVarListView1";
        this.EnvVarListView1.Dock = DockStyle.Fill;
      }
      // topMenuStrip
      {
        this.topMenuStrip.Name       = "topMenuStrip";
        this.topMenuStrip.Dock       = DockStyle.Top;
        this.topMenuStrip.RenderMode = ToolStripRenderMode.System;
        /************************************************/
        this.topMenuStrip.Items.Add(this.fileToolStripMenuItem);
        this.topMenuStrip.Items.Add(this.editToolStripMenuItem);
        this.topMenuStrip.Items.Add(this.viewToolStripMenuItem);
      }
      // fileToolStripMenuItem
      {
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.Text = "File";
        /************************************************/
        this.fileToolStripMenuItem.DropDownItems.Add(this.importToolStripMenuItem);
        this.fileToolStripMenuItem.DropDownItems.Add(this.exportToolStripMenuItem);
        this.fileToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.fileToolStripMenuItem.DropDownItems.Add(this.exitToolStripMenuItem);
        /************************************************/
        this.fileToolStripMenuItem.DropDownOpening += this.fileToolStripMenuItem_DropDownOpening;
      }
      // importToolStripMenuItem
      {
        this.importToolStripMenuItem.Name = "importToolStripMenuItem";
        this.importToolStripMenuItem.Text = "Import";
        /************************************************/
        this.importToolStripMenuItem.Click += this.importToolStripMenuItem_Click;
      }
      // exportToolStripMenuItem
      {
        this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
        this.exportToolStripMenuItem.Text = "Export";
        /************************************************/
        this.exportToolStripMenuItem.Click += this.exportToolStripMenuItem_Click;
      }
      // exitToolStripMenuItem
      {
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        this.exitToolStripMenuItem.Text = "Exit";
        /************************************************/
        this.exitToolStripMenuItem.Click += this.exitToolStripMenuItem_Click;
      }
      // editToolStripMenuItem
      {
        this.editToolStripMenuItem.Name = "editToolStripMenuItem";
        this.editToolStripMenuItem.Text = "Edit";
        /************************************************/
        this.editToolStripMenuItem.DropDownItems.Add(this.undoToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(this.redoToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.editToolStripMenuItem.DropDownItems.Add(this.cutToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(this.copyToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(this.pasteToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(this.deleteToolStripMenuItem);
        /************************************************/
        this.editToolStripMenuItem.DropDownOpening += this.editToolStripMenuItem_DropDownOpening;
      }
      // undoToolStripMenuItem
      {
        this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
        this.undoToolStripMenuItem.Text = "Undo";
        /************************************************/
        this.undoToolStripMenuItem.Click += this.undoToolStripMenuItem_Click;
      }
      // redoToolStripMenuItem
      {
        this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
        this.redoToolStripMenuItem.Text = "Redo";
        /************************************************/
        this.redoToolStripMenuItem.Click += this.redoToolStripMenuItem_Click;
      }
      // cutToolStripMenuItem
      {
        this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
        this.cutToolStripMenuItem.Text = "Cut";
        /************************************************/
        this.cutToolStripMenuItem.Click += this.cutToolStripMenuItem_Click;
      }
      // copyToolStripMenuItem
      {
        this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
        this.copyToolStripMenuItem.Text = "Copy";
        /************************************************/
        this.copyToolStripMenuItem.Click += this.copyToolStripMenuItem_Click;
      }
      // pasteToolStripMenuItem
      {
        this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
        this.pasteToolStripMenuItem.Text = "Paste";
        /************************************************/
        this.pasteToolStripMenuItem.Click += this.pasteToolStripMenuItem_Click;
      }
      // deleteToolStripMenuItem
      {
        this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
        this.deleteToolStripMenuItem.Text = "Delete";
        /************************************************/
        this.deleteToolStripMenuItem.Click += this.deleteToolStripMenuItem_Click;
      }
      // viewToolStripMenuItem
      {
        this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
        this.viewToolStripMenuItem.Text = "View";
        /************************************************/
        this.viewToolStripMenuItem.DropDownItems.Add(this.refreshToolStripMenuItem);
        this.viewToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.viewToolStripMenuItem.DropDownItems.Add(this.statusbarToolStripMenuItem);
        /************************************************/
        this.viewToolStripMenuItem.DropDownOpening += this.viewToolStripMenuItem_DropDownOpening;
      }
      // refreshToolStripMenuItem
      {
        this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
        this.refreshToolStripMenuItem.Text = "Refresh";
        /************************************************/
        this.refreshToolStripMenuItem.Click += this.refreshToolStripMenuItem_Click;
      }
      // statusbarToolStripMenuItem
      {
        this.statusbarToolStripMenuItem.Name = "statusbarToolStripMenuItem";
        this.statusbarToolStripMenuItem.Text = "Status Bar";
        /************************************************/
        this.statusbarToolStripMenuItem.Click += this.statusbarToolStripMenuItem_Click;;
      }
      // bottomStatusStrip
      {
        this.bottomStatusStrip.Name       = "bottomStatusStrip";
        this.bottomStatusStrip.Dock       = DockStyle.Bottom;
        this.bottomStatusStrip.RenderMode = ToolStripRenderMode.System;
        /************************************************/
        this.bottomStatusStrip.Items.Add(this.messsageToolStripStatusLabel);
      }
      // messsageToolStripStatusLabel
      {
        this.messsageToolStripStatusLabel.Name = "messsageToolStripStatusLabel";
        this.messsageToolStripStatusLabel.Text = "Ready";
        /************************************************/
        this.messsageToolStripStatusLabel.TextChanged += this.messsageToolStripStatusLabel_TextChanged;
      }
      // MainForm
      {
        base.Name          = "MainForm";
        base.AutoScaleMode = AutoScaleMode.Font;
        base.StartPosition = FormStartPosition.Manual;
        base.Text          = "EnvironmentVariableEditor";
        /************************************************/
        base.Controls.Add(this.EnvVarListView1);
        base.Controls.Add(this.topMenuStrip);
        base.Controls.Add(this.bottomStatusStrip);
      }
    }
    /************************************************/
    private EnvVarListView       EnvVarListView1              = null;
    private MenuStrip            topMenuStrip                 = null;
    private ToolStripMenuItem    fileToolStripMenuItem        = null;
    private ToolStripMenuItem    importToolStripMenuItem      = null;
    private ToolStripMenuItem    exportToolStripMenuItem      = null;
    private ToolStripMenuItem    exitToolStripMenuItem        = null;
    private ToolStripMenuItem    editToolStripMenuItem        = null;
    private ToolStripMenuItem    undoToolStripMenuItem        = null;
    private ToolStripMenuItem    redoToolStripMenuItem        = null;
    private ToolStripMenuItem    cutToolStripMenuItem         = null;
    private ToolStripMenuItem    copyToolStripMenuItem        = null;
    private ToolStripMenuItem    pasteToolStripMenuItem       = null;
    private ToolStripMenuItem    deleteToolStripMenuItem      = null;
    private ToolStripMenuItem    viewToolStripMenuItem        = null;
    private ToolStripMenuItem    refreshToolStripMenuItem     = null;
    private ToolStripMenuItem    statusbarToolStripMenuItem   = null;
    private StatusStrip          bottomStatusStrip            = null;
    private ToolStripStatusLabel messsageToolStripStatusLabel = null;
  }
}