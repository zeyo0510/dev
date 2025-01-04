using System;
using System.Drawing;
using System.Windows.Forms;
using FileBrowser;
using ShellDll;

namespace TestApp.Forms {
  public partial class MainForm : Form {
    public MainForm() {
      this.InitializeComponent();
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }
    
    private void fileBrowser_ContextMenuMouseHover(object sender, ContextMenuMouseHoverEventArgs e) {
      this.messageToolStripStatusLabel.Text = e.ContextMenuItemInfo;
    }
    
    private void fileBrowser_SelectedFolderChanged(object sender, SelectedFolderChangedEventArgs e) {
      Icon icon = ShellImageList.GetIcon(e.Node.ImageIndex, true);
      if (icon != null) {
        this.currentToolStripStatusLabel.Image = icon.ToBitmap();
        this.Icon = icon;
      } else {
        this.currentToolStripStatusLabel.Image = null;
        this.Icon = null;
      }
      this.currentToolStripStatusLabel.Text = e.Node.Text;
      this.Text = e.Node.Text;
    }
  }
}