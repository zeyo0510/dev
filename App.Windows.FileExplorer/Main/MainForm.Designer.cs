using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.FileExplorer.Main
{
  partial class MainForm
  {
    private IContainer components = null;
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
    private void InitializeComponent() {
      this.fileBrowser1 = new FileBrowser.Browser();
      this.pluginWrapper = new FileBrowser.BrowserPluginWrapper();
      this.shellBrowser = new ShellDll.ShellBrowser();
      this.bottomStatusStrip = new StatusStrip();
      this.messageToolStripStatusLabel = new ToolStripStatusLabel();
      this.toolStripStatusLabel1 = new ToolStripStatusLabel();
      this.currentToolStripStatusLabel = new ToolStripStatusLabel();
      // 
      // fileBrowser1
      // 
      this.fileBrowser1.AllowDrop = true;
      this.fileBrowser1.Dock = DockStyle.Fill;
      this.fileBrowser1.MinimumSize = new System.Drawing.Size(300, 200);
      this.fileBrowser1.Name = "fileBrowser1";
      this.fileBrowser1.PluginWrapper = this.pluginWrapper;
      this.fileBrowser1.ShellBrowser = this.shellBrowser;
      this.fileBrowser1.SplitterDistance = 162;
      this.fileBrowser1.TabIndex = 0;
      this.fileBrowser1.ContextMenuMouseHover += new FileBrowser.ContextMenuMouseHoverEventHandler(this.fileBrowser_ContextMenuMouseHover);
      this.fileBrowser1.SelectedFolderChanged += new FileBrowser.SelectedFolderChangedEventHandler(this.fileBrowser_SelectedFolderChanged);
      // 
      // bottomStatusStrip
      // 
      this.bottomStatusStrip.Name = "bottomStatusStrip";
      this.bottomStatusStrip.Items.AddRange(new ToolStripItem[] {
        this.messageToolStripStatusLabel,
        this.toolStripStatusLabel1,
        this.currentToolStripStatusLabel
      });
      // 
      // messageToolStripStatusLabel
      // 
      this.messageToolStripStatusLabel.Name = "messageToolStripStatusLabel";
      this.messageToolStripStatusLabel.Spring = true;
      this.messageToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.BorderSides = ToolStripStatusLabelBorderSides.Left;
      this.toolStripStatusLabel1.BorderStyle = Border3DStyle.Etched;
      // 
      // currentToolStripStatusLabel
      // 
      this.currentToolStripStatusLabel.Name = "currentToolStripStatusLabel";
      this.currentToolStripStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.currentToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // MainForm
      // 
      this.Name = "MainForm";
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(800, 600);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Controls.AddRange(new Control[] {
        this.fileBrowser1,
        this.bottomStatusStrip
      });
      this.Text = "FileBrowser Test";
    }
    /************************************************/
    private FileBrowser.Browser fileBrowser1;
    private ShellDll.ShellBrowser shellBrowser;
    private FileBrowser.BrowserPluginWrapper pluginWrapper;
    
    private StatusStrip bottomStatusStrip = null;
    private ToolStripStatusLabel messageToolStripStatusLabel = null;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ToolStripStatusLabel currentToolStripStatusLabel = null;
  }
}