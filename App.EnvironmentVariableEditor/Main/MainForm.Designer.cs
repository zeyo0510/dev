using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
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
      this.ListView1                    = new ListView();
      this.topMenuStrip                 = new MenuStrip();
      this.fileToolStripMenuItem        = new ToolStripMenuItem();
      this.importToolStripMenuItem      = new ToolStripMenuItem();
      this.exportToolStripMenuItem      = new ToolStripMenuItem();
      this.exitToolStripMenuItem        = new ToolStripMenuItem();
      this.editToolStripMenuItem        = new ToolStripMenuItem();
      this.copyToolStripMenuItem        = new ToolStripMenuItem();
      this.insertToolStripMenuItem      = new ToolStripMenuItem();
      this.updateToolStripMenuItem      = new ToolStripMenuItem();
      this.deleteToolStripMenuItem      = new ToolStripMenuItem();
      this.selectallToolStripMenuItem   = new ToolStripMenuItem();
      this.viewToolStripMenuItem        = new ToolStripMenuItem();
      this.userToolStripMenuItem        = new ToolStripMenuItem();
      this.machineToolStripMenuItem     = new ToolStripMenuItem();
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
        this.ListView1.Name          = "ListView1";
        this.ListView1.Dock          = DockStyle.Fill;
        this.ListView1.FullRowSelect = true;
        this.ListView1.GridLines     = true;
        this.ListView1.View          = View.Details;
        /************************************************/
        this.ListView1.Columns.Add("Key");
        this.ListView1.Columns.Add("Value");
        /************************************************/
        this.ListView1.DoubleClick += this.ListView1_DoubleClick;
      }
      // topMenuStrip
      {
        this.topMenuStrip.Name       = "topMenuStrip";
        this.topMenuStrip.Dock       = DockStyle.Top;
        this.topMenuStrip.Font       = new Font(FontFamily.GenericMonospace, 8f);
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
        this.editToolStripMenuItem.DropDownItems.Add(this.copyToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.editToolStripMenuItem.DropDownItems.Add(this.insertToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(this.updateToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(this.deleteToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.editToolStripMenuItem.DropDownItems.Add(this.selectallToolStripMenuItem);
        /************************************************/
        this.editToolStripMenuItem.DropDownOpening += this.editToolStripMenuItem_DropDownOpening;
      }
      // copyToolStripMenuItem
      {
        this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
        this.copyToolStripMenuItem.Text = "Copy";
        /************************************************/
        this.copyToolStripMenuItem.Click += this.copyToolStripMenuItem_Click;
      }
      // insertToolStripMenuItem
      {
        this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
        this.insertToolStripMenuItem.Text = "Insert";
        /************************************************/
        this.insertToolStripMenuItem.Click += this.insertToolStripMenuItem_Click;
      }
      // updateToolStripMenuItem
      {
        this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
        this.updateToolStripMenuItem.Text = "Update";
        /************************************************/
        this.updateToolStripMenuItem.Click += this.updateToolStripMenuItem_Click;
      }
      // deleteToolStripMenuItem
      {
        this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
        this.deleteToolStripMenuItem.Text = "Delete";
        /************************************************/
        this.deleteToolStripMenuItem.Click += this.deleteToolStripMenuItem_Click;
      }
      // selectallToolStripMenuItem
      {
        this.selectallToolStripMenuItem.Name = "selectallToolStripMenuItem";
        this.selectallToolStripMenuItem.Text = "Select All";
        /************************************************/
        this.selectallToolStripMenuItem.Click += this.selectallToolStripMenuItem_Click;
      }
      // viewToolStripMenuItem
      {
        this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
        this.viewToolStripMenuItem.Text = "View";
        /************************************************/
        this.viewToolStripMenuItem.DropDownItems.Add(this.userToolStripMenuItem);
        this.viewToolStripMenuItem.DropDownItems.Add(this.machineToolStripMenuItem);
        this.viewToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.viewToolStripMenuItem.DropDownItems.Add(this.refreshToolStripMenuItem);
        this.viewToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.viewToolStripMenuItem.DropDownItems.Add(this.statusbarToolStripMenuItem);
        /************************************************/
        this.viewToolStripMenuItem.DropDownOpening += this.viewToolStripMenuItem_DropDownOpening;
      }
      // userToolStripMenuItem
      {
        this.userToolStripMenuItem.Name = "userToolStripMenuItem";
        this.userToolStripMenuItem.Text = "User";
        /************************************************/
        this.userToolStripMenuItem.Click += this.userToolStripMenuItem_Click;
      }
      //
      {
        this.machineToolStripMenuItem.Name = "machineToolStripMenuItem";
        this.machineToolStripMenuItem.Text = "Machine";
        /************************************************/
        this.machineToolStripMenuItem.Click += this.machineToolStripMenuItem_Click;
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
        this.bottomStatusStrip.Font       = new Font(FontFamily.GenericMonospace, 8f);
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
        base.Font          = new Font(FontFamily.GenericMonospace, 8f);
        base.StartPosition = FormStartPosition.Manual;
        base.Text          = "EnvironmentVariableEditor";
        /************************************************/
        base.Controls.Add(this.ListView1);
        base.Controls.Add(this.topMenuStrip);
        base.Controls.Add(this.bottomStatusStrip);
      }
    }
    /************************************************/
    private ListView             ListView1                    = null;
    private MenuStrip            topMenuStrip                 = null;
    private ToolStripMenuItem    fileToolStripMenuItem        = null;
    private ToolStripMenuItem    importToolStripMenuItem      = null;
    private ToolStripMenuItem    exportToolStripMenuItem      = null;
    private ToolStripMenuItem    exitToolStripMenuItem        = null;
    private ToolStripMenuItem    editToolStripMenuItem        = null;
    private ToolStripMenuItem    copyToolStripMenuItem        = null;
    private ToolStripMenuItem    insertToolStripMenuItem      = null;
    private ToolStripMenuItem    updateToolStripMenuItem      = null;
    private ToolStripMenuItem    deleteToolStripMenuItem      = null;
    private ToolStripMenuItem    selectallToolStripMenuItem   = null;
    private ToolStripMenuItem    viewToolStripMenuItem        = null;
    private ToolStripMenuItem    userToolStripMenuItem        = null;
    private ToolStripMenuItem    machineToolStripMenuItem     = null;
    private ToolStripMenuItem    refreshToolStripMenuItem     = null;
    private ToolStripMenuItem    statusbarToolStripMenuItem   = null;
    private StatusStrip          bottomStatusStrip            = null;
    private ToolStripStatusLabel messsageToolStripStatusLabel = null;
  }
}