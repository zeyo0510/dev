using System;
using System.Linq;
using System.Windows.Forms;

namespace Little_Disk_Cleaner
{
    public partial class AddExcludeFolder : Form
    {
        public event AddExcludeFolderEventHandler AddExcludeFolderDelegate;

        public AddExcludeFolder()
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

            if (AddExcludeFolderDelegate != null)
            {
                AddExcludeFolderEventArgs eventArgs = new AddExcludeFolderEventArgs();
                eventArgs.folderPath = this.textBox1.Text;
                AddExcludeFolderDelegate(this, eventArgs);
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

    public class AddExcludeFolderEventArgs : EventArgs
    {
        public string folderPath
        {
            get;
            set;
        }
    }
    public delegate void AddExcludeFolderEventHandler(object sender, AddExcludeFolderEventArgs e);
}