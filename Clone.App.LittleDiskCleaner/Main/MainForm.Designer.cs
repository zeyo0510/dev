using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner
{
  partial class MainForm
  {
    private IContainer components = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.components = new Container();
      /************************************************/
      this.mainToolStripContainer = new ToolStripContainer();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.listview1 = new ListView();
      this.nameColumnHeader = new ColumnHeader();
      this.locationColumnHeader = new ColumnHeader();
      this.sizeColumnHeader = new ColumnHeader();
      this.fileInfoCtrl1 = new Little_Disk_Cleaner.Misc.FileInfoCtrl();
      this.topMenuStrip = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.scanToolStripMenuItem = new ToolStripMenuItem();
      this.cleanToolStripMenuItem = new ToolStripMenuItem();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.editToolStripMenuItem = new ToolStripMenuItem();
      this.selectAllToolStripMenuItem = new ToolStripMenuItem();
      this.selectNoneToolStripMenuItem = new ToolStripMenuItem();
      this.invertSelectionToolStripMenuItem = new ToolStripMenuItem();
      this.viewToolStripMenuItem = new ToolStripMenuItem();
      this.openfileToolStripMenuItem = new ToolStripMenuItem();
      this.propertiesToolStripMenuItem = new ToolStripMenuItem();
      this.toolsToolStripMenuItem = new ToolStripMenuItem();
      this.restoreSystemToolStripMenuItem = new ToolStripMenuItem();
      this.optionsToolStripMenuItem = new ToolStripMenuItem();
      
      this.notifyIcon1 = new NotifyIcon(this.components);
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.toolStripMenuItemHideShow = new ToolStripMenuItem();
      this.toolStripMenuItemExit = new ToolStripMenuItem();
      /************************************************/
      this.mainToolStripContainer.ContentPanel.SuspendLayout();
      this.mainToolStripContainer.TopToolStripPanel.SuspendLayout();
      this.mainToolStripContainer.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.topMenuStrip.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      /************************************************/
      // mainToolStripContainer
      {
        this.mainToolStripContainer.Name = "mainToolStripContainer";
        this.mainToolStripContainer.Dock = DockStyle.Fill;
        /************************************************/
        this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.topMenuStrip);
        /************************************************/
        this.mainToolStripContainer.ContentPanel.Controls.Add(this.tableLayoutPanel1);
      }
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
      this.tableLayoutPanel1.Controls.Add(this.listview1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.fileInfoCtrl1, 0, 1);
      // listview1
      {
        this.listview1.Name = "listview1";
        this.listview1.CheckBoxes = true;
        this.listview1.Dock = DockStyle.Fill;
        this.listview1.FullRowSelect = true;
        this.listview1.Location = new System.Drawing.Point(3, 3);
        this.listview1.Size = new System.Drawing.Size(708, 261);
        this.listview1.UseCompatibleStateImageBehavior = false;
        this.listview1.View = View.Details;
        /************************************************/
        this.listview1.Columns.Add(this.nameColumnHeader);
        this.listview1.Columns.Add(this.locationColumnHeader);
        this.listview1.Columns.Add(this.sizeColumnHeader);
        /************************************************/
        this.listview1.SelectedIndexChanged += this.listViewProblems_SelectedIndexChanged;
      }
      /************************************************/
      // nameColumnHeader
      {
        this.nameColumnHeader.Text = "Name";
      }
      // locationColumnHeader
      {
        this.locationColumnHeader.Text = "Location";
      }
      // sizeColumnHeader
      {
        this.sizeColumnHeader.Text = "Size";
      }
      // fileInfoCtrl1
      {
        this.fileInfoCtrl1.Name = "fileInfoCtrl1";
        this.fileInfoCtrl1.Dock = DockStyle.Fill;
      }
      
      
      
      // topMenuStrip
      {
        this.topMenuStrip.Name = "topMenuStrip";
        this.topMenuStrip.Dock = DockStyle.Top;
        /************************************************/
        this.topMenuStrip.Items.Add(this.fileToolStripMenuItem);
        this.topMenuStrip.Items.Add(this.editToolStripMenuItem);
        this.topMenuStrip.Items.Add(this.viewToolStripMenuItem);
        this.topMenuStrip.Items.Add(this.toolsToolStripMenuItem);
      }
      // fileToolStripMenuItem
      {
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.Text = "File";
        /************************************************/
        this.fileToolStripMenuItem.DropDownItems.Add(this.scanToolStripMenuItem);
        this.fileToolStripMenuItem.DropDownItems.Add(this.cleanToolStripMenuItem);
        this.fileToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.fileToolStripMenuItem.DropDownItems.Add(this.exitToolStripMenuItem);
      }
      // scanToolStripMenuItem
      {
        this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
        this.scanToolStripMenuItem.Text = "Scan Disk(s)";
        /************************************************/
        this.scanToolStripMenuItem.Click += this.ScanDisk;
      }
      // cleanToolStripMenuItem
      {
        this.cleanToolStripMenuItem.Name = "cleanToolStripMenuItem";
        this.cleanToolStripMenuItem.Enabled = false;
        this.cleanToolStripMenuItem.Text = "Clean Files";
        /************************************************/
        this.cleanToolStripMenuItem.Click += this.CleanDisk;
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
        this.editToolStripMenuItem.DropDownItems.Add(this.selectAllToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(this.selectNoneToolStripMenuItem);
        this.editToolStripMenuItem.DropDownItems.Add(this.invertSelectionToolStripMenuItem);
      }
      // selectAllToolStripMenuItem
      {
        this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
        this.selectAllToolStripMenuItem.Text = "Select All";
        /************************************************/
        this.selectAllToolStripMenuItem.Click += this.selectAllToolStripMenuItem_Click;
      }
      // selectNoneToolStripMenuItem
      {
        this.selectNoneToolStripMenuItem.Name = "selectNoneToolStripMenuItem";
        this.selectNoneToolStripMenuItem.Text = "Select None";
        /************************************************/
        this.selectNoneToolStripMenuItem.Click += this.selectNoneToolStripMenuItem_Click;
      }
      // invertSelectionToolStripMenuItem
      {
        this.invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
        this.invertSelectionToolStripMenuItem.Text = "Invert Selection";
        /************************************************/
        this.invertSelectionToolStripMenuItem.Click += this.invertSelectionToolStripMenuItem_Click;
      }
      // viewToolStripMenuItem
      {
        this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
        this.viewToolStripMenuItem.Text = "View";
        /************************************************/
        this.viewToolStripMenuItem.DropDownItems.Add(this.openfileToolStripMenuItem);
        this.viewToolStripMenuItem.DropDownItems.Add(this.propertiesToolStripMenuItem);
      }
      // openfileToolStripMenuItem
      {
        this.openfileToolStripMenuItem.Name = "openfileToolStripMenuItem";
        this.openfileToolStripMenuItem.Text = "Open File";
        /************************************************/
        this.openfileToolStripMenuItem.Click += this.openfileToolStripMenuItem_Click;
      }
      // propertiesToolStripMenuItem
      {
        this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
        this.propertiesToolStripMenuItem.Text = "Properties";
        /************************************************/
        this.propertiesToolStripMenuItem.Click += this.propertiesToolStripMenuItem_Click;
      }
      // toolsToolStripMenuItem
      {
        this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
        this.toolsToolStripMenuItem.Text = "Tools";
        /************************************************/
        this.toolsToolStripMenuItem.DropDownItems.Add(this.restoreSystemToolStripMenuItem);
        this.toolsToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        this.toolsToolStripMenuItem.DropDownItems.Add(this.optionsToolStripMenuItem);
      }
      // restoreSystemToolStripMenuItem
      {
        this.restoreSystemToolStripMenuItem.Name = "restoreSystemToolStripMenuItem";
        this.restoreSystemToolStripMenuItem.Text = "Restore System";
        /************************************************/
        this.restoreSystemToolStripMenuItem.Click += this.restoreSystemToolStripMenuItem_Click;
      }
      // optionsToolStripMenuItem
      {
        this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
        this.optionsToolStripMenuItem.Text = "Options";
        /************************************************/
        this.optionsToolStripMenuItem.Click += this.Options;
      }
      
      // notifyIcon1
      {
        this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
        this.notifyIcon1.Icon = base.Icon;
        this.notifyIcon1.Text = "Little Disk Cleaner";
        this.notifyIcon1.Visible = true;
        /************************************************/
        this.notifyIcon1.MouseDoubleClick += this.notifyIcon1_MouseDoubleClick;
      }
      // contextMenuStrip1
      {
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        /************************************************/
        this.contextMenuStrip1.Items.Add(this.toolStripMenuItemHideShow);
        this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
        this.contextMenuStrip1.Items.Add(this.toolStripMenuItemExit);
      }
      // toolStripMenuItemHideShow
      {
        this.toolStripMenuItemHideShow.Name = "toolStripMenuItemHideShow";
        this.toolStripMenuItemHideShow.Text = "Hide/Show";
        /************************************************/
        this.toolStripMenuItemHideShow.Click += this.HideShow;
      }
      // toolStripMenuItemExit
      {
        this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
        this.toolStripMenuItemExit.Text = "Exit";
        /************************************************/
        this.toolStripMenuItemExit.Click += this.exitToolStripMenuItem_Click;
      }
      // MainForm
      {
        this.Name = "MainForm";
        this.AutoScaleDimensions = new SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(714, 422);
        this.MainMenuStrip = this.topMenuStrip;
        this.Text = "Little Disk Cleaner";
        this.WindowState = FormWindowState.Maximized;
        this.Controls.Add(this.mainToolStripContainer);
        /************************************************/
        this.FormClosing += this.Main_FormClosing;
      }
      /************************************************/
      this.mainToolStripContainer.ContentPanel.ResumeLayout(false);
      this.mainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.mainToolStripContainer.TopToolStripPanel.PerformLayout();
      this.mainToolStripContainer.ResumeLayout(false);
      this.mainToolStripContainer.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.topMenuStrip.ResumeLayout(false);
      this.topMenuStrip.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
    /************************************************/
    private ToolStripContainer mainToolStripContainer = null;
    private TableLayoutPanel tableLayoutPanel1;
    private ListView     listview1            = null;
    private ColumnHeader nameColumnHeader     = null;
    private ColumnHeader locationColumnHeader = null;
    private ColumnHeader sizeColumnHeader     = null;
    private Misc.FileInfoCtrl fileInfoCtrl1   = null;
    private MenuStrip         topMenuStrip                     = null;
    private ToolStripMenuItem fileToolStripMenuItem            = null;
    private ToolStripMenuItem scanToolStripMenuItem            = null;
    private ToolStripMenuItem cleanToolStripMenuItem           = null;
    private ToolStripMenuItem exitToolStripMenuItem            = null;
    private ToolStripMenuItem editToolStripMenuItem            = null;
    private ToolStripMenuItem selectAllToolStripMenuItem       = null;
    private ToolStripMenuItem selectNoneToolStripMenuItem      = null;
    private ToolStripMenuItem invertSelectionToolStripMenuItem = null;
    private ToolStripMenuItem viewToolStripMenuItem            = null;
    private ToolStripMenuItem openfileToolStripMenuItem        = null;
    private ToolStripMenuItem propertiesToolStripMenuItem      = null;
    private ToolStripMenuItem toolsToolStripMenuItem           = null;
    private ToolStripMenuItem restoreSystemToolStripMenuItem   = null;
    private ToolStripMenuItem optionsToolStripMenuItem         = null;
    
    private NotifyIcon notifyIcon1;
    private ContextMenuStrip contextMenuStrip1 = null;
    private ToolStripMenuItem toolStripMenuItemHideShow;
    private ToolStripMenuItem toolStripMenuItemExit;
  }
}