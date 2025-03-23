using System.ComponentModel;
using System.Drawing;
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
      this.currentToolStripStatusLabel = new ToolStripStatusLabel();
      // fileBrowser1
      {
        this.fileBrowser1.Name = "fileBrowser1";
        this.fileBrowser1.AllowDrop = true;
        this.fileBrowser1.Dock = DockStyle.Fill;
        this.fileBrowser1.PluginWrapper = this.pluginWrapper;
        this.fileBrowser1.ShellBrowser = this.shellBrowser;
        this.fileBrowser1.SplitterDistance = 162;
        /************************************************/
        this.fileBrowser1.ContextMenuMouseHover += new FileBrowser.ContextMenuMouseHoverEventHandler(this.fileBrowser_ContextMenuMouseHover);
        this.fileBrowser1.SelectedFolderChanged += new FileBrowser.SelectedFolderChangedEventHandler(this.fileBrowser_SelectedFolderChanged);
      }
      // bottomStatusStrip
      {
        this.bottomStatusStrip.Name = "bottomStatusStrip";
        /************************************************/
        this.bottomStatusStrip.Items.Add(this.messageToolStripStatusLabel);
        this.bottomStatusStrip.Items.Add(new ToolStripSeparator());
        this.bottomStatusStrip.Items.Add(this.currentToolStripStatusLabel);
      }
      // messageToolStripStatusLabel
      {
        this.messageToolStripStatusLabel.Name      = "messageToolStripStatusLabel";
        this.messageToolStripStatusLabel.Spring    = true;
        this.messageToolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
      }
      // currentToolStripStatusLabel
      {
        this.currentToolStripStatusLabel.Name       = "currentToolStripStatusLabel";
        this.currentToolStripStatusLabel.ImageAlign = ContentAlignment.MiddleLeft;
        this.currentToolStripStatusLabel.TextAlign  = ContentAlignment.MiddleLeft;
      }
      // MainForm
      {
        this.Name = "MainForm";
        this.AutoScaleMode = AutoScaleMode.Dpi;
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "App.Windows.FileExplorer";
        /************************************************/
        this.Controls.Add(this.fileBrowser1);
        this.Controls.Add(this.bottomStatusStrip);
      }
    }
    /************************************************/
    private FileBrowser.Browser fileBrowser1;
    private ShellDll.ShellBrowser shellBrowser;
    private FileBrowser.BrowserPluginWrapper pluginWrapper;
    
    private StatusStrip bottomStatusStrip = null;
    private ToolStripStatusLabel messageToolStripStatusLabel = null;
    private ToolStripStatusLabel currentToolStripStatusLabel = null;
  }
}