namespace TestApp.Forms {
  partial class MainForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.fileBrowser1 = new FileBrowser.Browser();
      this.pluginWrapper = new FileBrowser.BrowserPluginWrapper();
      this.shellBrowser = new ShellDll.ShellBrowser();
      this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
      this.messageToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.currentToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.mainStatusStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // fileBrowser1
      // 
      this.fileBrowser1.AllowDrop = true;
      this.fileBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.fileBrowser1.MinimumSize = new System.Drawing.Size(300, 200);
      this.fileBrowser1.Name = "fileBrowser1";
      this.fileBrowser1.PluginWrapper = this.pluginWrapper;
      this.fileBrowser1.ShellBrowser = this.shellBrowser;
      this.fileBrowser1.SplitterDistance = 162;
      this.fileBrowser1.TabIndex = 0;
      this.fileBrowser1.ContextMenuMouseHover += new FileBrowser.ContextMenuMouseHoverEventHandler(this.fileBrowser_ContextMenuMouseHover);
      this.fileBrowser1.SelectedFolderChanged += new FileBrowser.SelectedFolderChangedEventHandler(this.fileBrowser_SelectedFolderChanged);
      // 
      // mainStatusStrip
      // 
      this.mainStatusStrip.Name = "mainStatusStrip";
      this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
      this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
      this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
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
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(800, 600);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
        this.fileBrowser1,
        this.mainStatusStrip
      });
      this.Text = "FileBrowser Test";
      this.mainStatusStrip.ResumeLayout(false);
      this.mainStatusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion
    
    private FileBrowser.Browser fileBrowser1;
    private ShellDll.ShellBrowser shellBrowser;
    private FileBrowser.BrowserPluginWrapper pluginWrapper;
    private System.Windows.Forms.StatusStrip mainStatusStrip;
    private System.Windows.Forms.ToolStripStatusLabel messageToolStripStatusLabel;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.ToolStripStatusLabel currentToolStripStatusLabel;
  }
}

