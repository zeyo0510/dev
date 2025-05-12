using System;
using System.Linq;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner
{
  public partial class AddExcludeFileType : Form
  {
    public event AddFileTypeEventHandler AddFileType;

    public AddExcludeFileType()
    {
      InitializeComponent();
    }

    private void buttonOk_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(this.textBox1.Text))
      {
        MessageBox.Show(this, "Please enter a file type", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      if (AddFileType != null)
      {
        AddFileTypeEventArgs eventArgs = new AddFileTypeEventArgs();
        eventArgs.fileType = this.textBox1.Text;
        AddFileType(this, eventArgs);
      }

      this.Close();
    }
  }

  public class AddFileTypeEventArgs : EventArgs
  {
    public string fileType
    {
      get;
      set;
    }
  }
  
  public delegate void AddFileTypeEventHandler(object sender, AddFileTypeEventArgs e);
}