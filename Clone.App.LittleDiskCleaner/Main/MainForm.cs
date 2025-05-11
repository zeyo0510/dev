using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Little_Disk_Cleaner.Dialogs;
using Little_Disk_Cleaner.Properties;
/************************************************/
namespace Little_Disk_Cleaner
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      this.InitializeComponent();
      /************************************************/
      PopulateDiskDrives();

      if (Settings.Default.includedFolders == null)
        PopulateIncludeFolders();

      /***********************************************************************************************************/
      /* THE CODE BELOW IS LICENSED UNDER THE CREATIVE COMMONS ATTRIBUTION NON-COMMERCIAL NO DERIVATIVES LICENSE */
      if (Settings.Default.firstRun)
      {
        MessageBox.Show(this, "This program is freeware. If you paid for a copy of this program, get a refund!", "Little Disk Cleaner", MessageBoxButtons.OK, MessageBoxIcon.Information);
        Settings.Default.firstRun = false;
      }
      /* THE CODE ABOVE IS LICENSED UNDER THE CREATIVE COMMONS ATTRIBUTION NON-COMMERCIAL NO DERIVATIVES LICENSE */
      /***********************************************************************************************************/
    }
    /************************************************/
    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason == CloseReason.UserClosing)
      {
        if (MessageBox.Show(this, "Are you sure?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
          e.Cancel = true;
      }
    }
    /************************************************/
    private void listViewProblems_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listview1.SelectedItems.Count > 0)
      {
        fileInfoCtrl1.UpdateInfo(this.listview1.SelectedItems[0].Tag as FileInfo);
      }
      else
      {
        fileInfoCtrl1.ResetInfo();
      }
    }
    /************************************************/
    private void ScanDisk(object sender, EventArgs e)
    {
      this.fileInfoCtrl1.ResetInfo();
      this.listview1.Items.Clear();

      List<DriveInfo> selDrives = new List<DriveInfo>();
      foreach (ListViewItem lvi in Settings.Default.diskDrives)
      {
        if (lvi.Checked)
          selDrives.Add(lvi.Tag as DriveInfo);
      }

      if (selDrives.Count == 0)
      {
        MessageBox.Show(this, "No drives selected", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      AnalyzeDialog analyze = new AnalyzeDialog(selDrives);
      DialogResult dlgResult = analyze.ShowDialog(this);

      if (dlgResult == System.Windows.Forms.DialogResult.OK)
      {
        this.notifyIcon1.ShowBalloonTip(6000, "Little Disk Cleaner", "Finished analyzing hard drive(s)", ToolTipIcon.Info);
      }
      else
      {
        this.notifyIcon1.ShowBalloonTip(6000, "Little Disk Cleaner", "Aborted analyzing hard drive(s)", ToolTipIcon.Info);
      }

      foreach (FileInfo fileInfo in analyze.FileList)
      {
        string fileName = fileInfo.Name;
        string filePath = fileInfo.DirectoryName;
        string fileSize = Utils.ConvertSizeToString(fileInfo.Length);

        ListViewItem listViewItem = new ListViewItem(new string[] { fileName, filePath, fileSize });
        listViewItem.Checked = true;
        listViewItem.Tag = fileInfo;
        this.listview1.Items.Add(listViewItem);
      }

      this.listview1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

      if (Settings.Default.autoClean)
        CleanDisk(this, new EventArgs());
      else
      {
        this.cleanToolStripMenuItem.Enabled = true;
      }
    }
    /************************************************/
    private void CleanDisk(object sender, EventArgs e)
    {
      if (this.listview1.Items.Count > 0)
      {
        if (!Settings.Default.autoClean)
          if (MessageBox.Show(this, "Are you sure you want to remove these files?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
            return;

        long lSeqNum = 0;
        SysRestore.StartRestore("Before Little Disk Cleaner Cleaning", out lSeqNum);

        foreach (ListViewItem lvi in this.listview1.Items)
        {
          if (!lvi.Checked)
            continue;

          try
          {
              FileInfo fileInfo = lvi.Tag as FileInfo;

              // Make sure file exists
              if (!fileInfo.Exists)
                continue;

              if (Settings.Default.removeMode == 0)
              {
                // Remove permanately
                fileInfo.Delete();
              }
              else if (Settings.Default.removeMode == 1)
              {
                // Recycle file
                Utils.SendFileToRecycleBin(fileInfo.FullName);
              }
              else
              {
                // Move file to specified directory
                if (!Directory.Exists(Settings.Default.moveFolder))
                  Directory.CreateDirectory(Settings.Default.moveFolder);

                File.Move(fileInfo.FullName, string.Format(@"{0}\{1}", Settings.Default.moveFolder, fileInfo.Name));
              }
            }
            catch (Exception)
            {

            }
          }

          if (lSeqNum != 0)
          {
              SysRestore.EndRestore(lSeqNum);
          }

          MessageBox.Show(this, "Successfully cleaned files from disk", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

          // Clear problems
          this.listview1.Items.Clear();
          this.fileInfoCtrl1.ResetInfo();

          // Disable clean disk
          this.cleanToolStripMenuItem.Enabled = false;
      }
    }
    /************************************************/
    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ExitApp();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SelectAllItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void selectNoneToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SelectNoneItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.InvertSelectionItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void openfileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.OpenFile();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ViewFileProperties();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void restoreSystemToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.RestoreSystem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void Options(object sender, EventArgs e)
    {
      OptionsDialog dialog = new OptionsDialog();
      /************************************************/
      dialog.ShowDialog(this);
    }
    
    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
      {
        this.Show();
        this.WindowState = FormWindowState.Normal;
      }
      else
      {
        this.Hide();
        this.WindowState = FormWindowState.Minimized;
      }
    }
    
    private void HideShow(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
      {
        this.Show();
        this.WindowState = FormWindowState.Normal;
      }
      else
      {
        this.Hide();
        this.WindowState = FormWindowState.Minimized;
      }
    }
    
    
    
    private void PopulateIncludeFolders()
    {
      Settings.Default.includedFolders = new System.Collections.Specialized.StringCollection();
      Settings.Default.includedFolders.Add(Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User));
      Settings.Default.includedFolders.Add(Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.Machine));
      Settings.Default.includedFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.Recent));
      Settings.Default.includedFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
    }

    private void PopulateDiskDrives()
    {
      if (Settings.Default.diskDrives == null)
        Settings.Default.diskDrives = new System.Collections.ArrayList();
      else
        Settings.Default.diskDrives.Clear();

      string winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
      foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
      {
        if (!driveInfo.IsReady || driveInfo.DriveType != DriveType.Fixed)
          continue;

        string freeSpace = Utils.ConvertSizeToString(driveInfo.TotalFreeSpace);
        string totalSpace = Utils.ConvertSizeToString(driveInfo.TotalSize);

        ListViewItem listViewItem = new ListViewItem(new string[] { driveInfo.Name, driveInfo.DriveFormat, totalSpace, freeSpace });
        if (winDir.Contains(driveInfo.Name))
          listViewItem.Checked = true;
        listViewItem.Tag = driveInfo;

        // Store as listviewitem cause im too lazy 
        Settings.Default.diskDrives.Add(listViewItem);
      }
    }
  }
}