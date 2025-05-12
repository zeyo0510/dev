using System;
using System.Linq;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner
{
  public partial class AddIncludeFolder : Form
  {
    public event AddIncFolderEventHandler AddIncFolder;

    public AddIncludeFolder()
    {
      InitializeComponent();
    }

    private void buttonOk_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(this.textBox1.Text))
      {
        MessageBox.Show(this, "Please enter a folder", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      if (AddIncFolder != null)
      {
        AddIncFolderEventArgs eventArgs = new AddIncFolderEventArgs();
        eventArgs.folderPath = this.textBox1.Text;
        AddIncFolder(this, eventArgs);
      }

      this.Close();
    }

    private void buttonBrowse_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog browserDlg = new FolderBrowserDialog();
      browserDlg.ShowDialog(this);
      this.textBox1.Text = browserDlg.SelectedPath;
    }
  }

  public class AddIncFolderEventArgs : EventArgs
  {
    public string folderPath
    {
      get;
      set;
    }
  }
  public delegate void AddIncFolderEventHandler(object sender, AddIncFolderEventArgs e);
}