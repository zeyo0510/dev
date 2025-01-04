/*
    Little Disk Cleaner
    Copyright (C) 2008-2010 Little Apps  (http://www.littleapps.co.cc/)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Little_Disk_Cleaner
{
    public partial class Options : Form
    {

        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            // Drives
            foreach (ListViewItem listViewItem in Properties.Settings.Default.diskDrives)
                this.listViewDrives.Items.Add(listViewItem.Clone() as ListViewItem);

            // Advanced
            this.checkBoxArchive.Checked = Properties.Settings.Default.searchArchives;
            this.checkBoxHidden.Checked = Properties.Settings.Default.searchHidden;
            this.checkBoxReadOnly.Checked = Properties.Settings.Default.searchReadOnly;
            this.checkBoxSystem.Checked = Properties.Settings.Default.searchSystem;

            if (Properties.Settings.Default.findFilesMode == 0)
                this.radioButtonFindCreated.Checked = true;
            else if (Properties.Settings.Default.findFilesMode == 1)
                this.radioButtonFindModified.Checked = true;
            else if (Properties.Settings.Default.findFilesMode == 2)
                this.radioButtonFindAccessed.Checked = true;

            this.checkBoxFindAfter.Checked = Properties.Settings.Default.findFilesAfter;
            this.checkBoxFindBefore.Checked = Properties.Settings.Default.findFilesBefore;

            this.dateTimePickerAfter.Value = Properties.Settings.Default.dateTimeAfter;
            this.dateTimePickerBefore.Value = Properties.Settings.Default.dateTimeBefore;

            this.checkBoxSize.Checked = Properties.Settings.Default.checkFileSize;
            this.numericUpDownSizeAtLeast.Value = Convert.ToDecimal(Properties.Settings.Default.checkFileSizeLeast);
            this.numericUpDownSizeAtMost.Value = Convert.ToDecimal(Properties.Settings.Default.checkFileSizeMost);

            // Search Options
            this.checkBoxWriteProtected.Checked = Properties.Settings.Default.ignoreWriteProtected;
            this.checkBoxZeroLength.Checked = Properties.Settings.Default.searchZeroByte;

            if (Properties.Settings.Default.filterMode == 0)
                this.radioButtonFilterSafe.Checked = true;
            else if (Properties.Settings.Default.filterMode == 1)
                this.radioButtonFilterMed.Checked = true;
            else if (Properties.Settings.Default.filterMode == 2)
                this.radioButtonFilterAgg.Checked = true;

            this.textBoxSearchFilters.Text = Properties.Settings.Default.searchFilters;

            this.checkBoxAutoClean.Checked = Properties.Settings.Default.autoClean;

            // Removal
            if (Properties.Settings.Default.removeMode == 0)
                this.radioButtonRemove.Checked = true;
            else if (Properties.Settings.Default.removeMode == 1)
                this.radioButtonRecycle.Checked = true;
            else if (Properties.Settings.Default.removeMode == 2)
                this.radioButtonMove.Checked = true;
            this.textBoxMoveFolder.Text = Properties.Settings.Default.moveFolder;
            this.checkBoxAutoSysRestore.Checked = Properties.Settings.Default.autoRestorePoints;

            // Excluded Dirs
            foreach (string excludeDir in Properties.Settings.Default.excludedDirs)
                this.listViewExcludeFolders.Items.Add(excludeDir);
            this.listViewExcludeFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Excluded Files
            foreach (string excludeFile in Properties.Settings.Default.excludedFileTypes)
                this.listViewFiles.Items.Add(excludeFile);
            this.listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Included Folders
            foreach (string includedFolder in Properties.Settings.Default.includedFolders)
                this.listViewIncFolders.Items.Add(includedFolder);
            this.listViewIncFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void FilterRadioChanged(object sender, EventArgs e)
        {
            if (this.radioButtonFilterSafe.Checked == true)
                this.textBoxSearchFilters.Text = "*.tmp; *.temp; *.gid; *.chk; *.~*;";
            else if (this.radioButtonFilterMed.Checked == true)
                this.textBoxSearchFilters.Text = "*.tmp; *.temp; *.gid; *.chk; *.~*;*.old; *.fts; *.ftg; *.$$$; *.---; ~*.*; *.??$; *.___; *._mp; *.dmp; *.prv; CHKLIST.MS; *.$db; *.??~; *.db$; chklist.*; mscreate.dir;";
            else if (this.radioButtonFilterAgg.Checked == true)
                this.textBoxSearchFilters.Text = "*.tmp; *.temp; *.gid; *.chk; *.~*;*.old; *.fts; *.ftg; *.$$$; *.---; ~*.*; *.??$; *.___; *._mp; *.dmp; *.prv; CHKLIST.MS; *.$db; *.??~; *.db$; chklist.*; mscreate.dir;*.wbk; *log.txt; *.err; *.log; *.sik; *.bak; *.ilk; *.aps; *.ncb; *.pch; *.?$?; *.?~?; *.^; *._dd; *._detmp; 0*.nch; *.*_previous; *_previous;";

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // Drives
            Properties.Settings.Default.diskDrives = new System.Collections.ArrayList(this.listViewDrives.Items);

            // Searching
            if (this.radioButtonFilterSafe.Checked)
                Properties.Settings.Default.filterMode = 0;
            else if (this.radioButtonFilterMed.Checked)
                Properties.Settings.Default.filterMode = 1;
            else if (this.radioButtonFilterAgg.Checked)
                Properties.Settings.Default.filterMode = 2;

            Properties.Settings.Default.searchFilters = this.textBoxSearchFilters.Text;

            Properties.Settings.Default.ignoreWriteProtected = this.checkBoxWriteProtected.Checked;
            Properties.Settings.Default.searchZeroByte = this.checkBoxZeroLength.Checked;

            Properties.Settings.Default.autoClean = this.checkBoxAutoClean.Checked;

            // Advanced
            Properties.Settings.Default.searchHidden = this.checkBoxHidden.Checked;
            Properties.Settings.Default.searchArchives = this.checkBoxArchive.Checked;
            Properties.Settings.Default.searchReadOnly = this.checkBoxReadOnly.Checked;
            Properties.Settings.Default.searchSystem = this.checkBoxSystem.Checked;

            if (this.radioButtonFindCreated.Checked)
                Properties.Settings.Default.findFilesMode = 0;
            else if (this.radioButtonFindModified.Checked)
                Properties.Settings.Default.findFilesMode = 1;
            else if (this.radioButtonFindAccessed.Checked)
                Properties.Settings.Default.findFilesMode = 2;

            Properties.Settings.Default.findFilesAfter = this.checkBoxFindAfter.Checked;
            Properties.Settings.Default.findFilesBefore = this.checkBoxFindBefore.Checked;

            Properties.Settings.Default.dateTimeAfter = this.dateTimePickerAfter.Value;
            Properties.Settings.Default.dateTimeBefore = this.dateTimePickerBefore.Value;

            Properties.Settings.Default.checkFileSize = this.checkBoxSize.Checked;
            Properties.Settings.Default.checkFileSizeLeast = Convert.ToInt32(this.numericUpDownSizeAtLeast.Value);
            Properties.Settings.Default.checkFileSizeMost = Convert.ToInt32(this.numericUpDownSizeAtMost.Value);

            // Removal
            if (this.radioButtonRemove.Checked)
                Properties.Settings.Default.removeMode = 0;
            else if (this.radioButtonRecycle.Checked)
                Properties.Settings.Default.removeMode = 1;
            else if (this.radioButtonMove.Checked)
                Properties.Settings.Default.removeMode = 2;

            Properties.Settings.Default.moveFolder = this.textBoxMoveFolder.Text;

            Properties.Settings.Default.autoRestorePoints = this.checkBoxAutoSysRestore.Checked;

            // Included Folders
            Properties.Settings.Default.includedFolders.Clear();
            foreach (ListViewItem listViewItem in this.listViewIncFolders.Items)
                Properties.Settings.Default.includedFolders.Add(listViewItem.Text);

            // Excluded Folders
            Properties.Settings.Default.excludedDirs.Clear();
            foreach (ListViewItem lvi in this.listViewExcludeFolders.Items)
                Properties.Settings.Default.excludedDirs.Add(lvi.Text);

            // Excluded Files
            Properties.Settings.Default.excludedFileTypes.Clear();
            foreach (ListViewItem lvi in this.listViewFiles.Items)
                Properties.Settings.Default.excludedFileTypes.Add(lvi.Text);

            this.Close();
        }

        private void buttonFoldersRemove_Click(object sender, EventArgs e)
        {
            if (this.listViewExcludeFolders.SelectedItems[0] != null)
            {
                if (MessageBox.Show(this, "Are you sure?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.listViewExcludeFolders.SelectedItems[0].Remove();
                    this.listViewExcludeFolders.Refresh();
                }
            }
        }

        private void buttonFilesRemove_Click(object sender, EventArgs e)
        {
            if (this.listViewFiles.SelectedItems[0] != null)
            {
                if (MessageBox.Show(this, "Are you sure?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.listViewFiles.SelectedItems[0].Remove();
                    this.listViewFiles.Refresh();
                }
            }
        }

        private void buttonFilesAdd_Click(object sender, EventArgs e)
        {
            AddExcludeFileType addFileType = new AddExcludeFileType();
            addFileType.AddFileType += new AddFileTypeEventHandler(addFileType_AddFileType);
            addFileType.ShowDialog(this);
        }

        void addFileType_AddFileType(object sender, AddFileTypeEventArgs e)
        {
            this.listViewFiles.Items.Add(e.fileType);
            this.listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonFoldersAdd_Click(object sender, EventArgs e)
        {
            AddExcludeFolder addFolder = new AddExcludeFolder();
            addFolder.AddExcludeFolderDelegate += new AddExcludeFolderEventHandler(addFolder_AddExcludeFolder);
            addFolder.ShowDialog(this);
        }

        void addFolder_AddExcludeFolder(object sender, AddExcludeFolderEventArgs e)
        {
            this.listViewExcludeFolders.Items.Add(e.folderPath);
            this.listViewExcludeFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonIncFoldersAdd_Click(object sender, EventArgs e)
        {
            AddIncludeFolder addIncFolder = new AddIncludeFolder();
            addIncFolder.AddIncFolder += new AddIncFolderEventHandler(addIncFolder_AddIncFolder);
            addIncFolder.ShowDialog(this);
        }

        void addIncFolder_AddIncFolder(object sender, AddIncFolderEventArgs e)
        {
            this.listViewIncFolders.Items.Add(e.folderPath);
            this.listViewIncFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonIncFoldersRemove_Click(object sender, EventArgs e)
        {
            if (this.listViewIncFolders.SelectedItems[0] != null)
            {
                if (MessageBox.Show(this, "Are you sure?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.listViewIncFolders.SelectedItems[0].Remove();
                    this.listViewIncFolders.Refresh();
                }
            }
        }
    }
}
