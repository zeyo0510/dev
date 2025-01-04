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
using System.Threading;

namespace Little_Disk_Cleaner
{
    public partial class Analyze : Form
    {
        public Thread threadMain
        {
            get;
            set;
        }

        public static List<DriveInfo> scanDrives
        {
            get;
            set;
        }

        public static List<FileInfo> fileList
        {
            get;
            set;
        }

        public static string CurrentFile
        {
            get;
            set;
        }

        public Analyze(List<DriveInfo> selectedDrives)
        {
            InitializeComponent();

            scanDrives = selectedDrives;
            if (Analyze.fileList == null)
                Analyze.fileList = new List<FileInfo>();
            else
                Analyze.fileList.Clear();

            this.timerUpdateFile.Start();

            this.threadMain = new Thread(new ThreadStart(AnalyzeDisk));
            this.threadMain.Start();
        }

        private void AnalyzeDisk()
        {
            try
            {
                foreach (DriveInfo driveInfo in scanDrives)
                {
                    ScanFiles(driveInfo.RootDirectory);
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (ThreadAbortException )
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
        }

        private void ScanFiles(DirectoryInfo parentInfo)
        {
            try
            {
                foreach (FileInfo fileInfo in parentInfo.GetFiles())
                {
                    Analyze.CurrentFile = fileInfo.FullName;

                    // Check if file is exclude
                    if (FileTypeIsExcluded(fileInfo.Name))
                        continue;

                    // Check for zero-byte files
                    if (Properties.Settings.Default.searchZeroByte)
                    {
                        if (fileInfo.Length == 0)
                        {
                            fileList.Add(fileInfo);
                            continue;
                        }
                    }

                    // Check if file matches types
                    if (!Utils.CompareWildcards(fileInfo.Name, Properties.Settings.Default.searchFilters))
                        continue;

                    // Check if file is in use or write protected
                    if (Properties.Settings.Default.ignoreWriteProtected && fileInfo.IsReadOnly)
                        continue;

                    // Check file attributes
                    if (!FileCheckAttributes(fileInfo))
                        continue;

                    // Check file dates
                    if (Properties.Settings.Default.findFilesAfter || Properties.Settings.Default.findFilesBefore)
                        if (!FileCheckDate(fileInfo))
                            continue;

                    // Check file size
                    if (Properties.Settings.Default.checkFileSize)
                        if (!FileCheckSize(fileInfo))
                            continue;

                    Analyze.fileList.Add(fileInfo);
                }

                foreach (DirectoryInfo childInfo in parentInfo.GetDirectories())
                {
                    if (FolderIsIncluded(childInfo.FullName) && !FolderIsExcluded(childInfo.FullName))
                    {
                        Analyze.fileList.AddRange(childInfo.EnumerateFiles());
                        continue;
                    }

                    if (!FolderIsExcluded(childInfo.FullName))
                        ScanFiles(childInfo);
                }
            }
            catch (Exception ex)
            {
                //TODO: Add better exception handling
            }
        }

        private bool FileCheckSize(FileInfo fileInfo)
        {
            long fileSize = fileInfo.Length / 1024;

            if (Properties.Settings.Default.checkFileSizeLeast > 0)
                if (fileSize <= Properties.Settings.Default.checkFileSizeLeast)
                    return false;

            if (Properties.Settings.Default.checkFileSizeMost > 0)
                if (fileSize >= Properties.Settings.Default.checkFileSizeMost)
                    return false;

            return true;
        }

        private bool FileCheckDate(FileInfo fileInfo)
        {
            DateTime dateTimeFile = DateTime.MinValue;
            bool bRet = false;

            if (Properties.Settings.Default.findFilesMode == 0)
                dateTimeFile = fileInfo.CreationTime;
            else if (Properties.Settings.Default.findFilesMode == 1)
                dateTimeFile = fileInfo.LastWriteTime;
            else if (Properties.Settings.Default.findFilesMode == 2)
                dateTimeFile = fileInfo.LastAccessTime;

            if (Properties.Settings.Default.findFilesAfter)
            {
                if (DateTime.Compare(dateTimeFile, Properties.Settings.Default.dateTimeAfter) >= 0)
                    bRet = true;
            }

            if (Properties.Settings.Default.findFilesBefore)
            {
                if (DateTime.Compare(dateTimeFile, Properties.Settings.Default.dateTimeBefore) <= 0)
                    bRet = true;
            }

            return bRet;
        }

        /// <summary>
        /// Checks file attributes to match what user specified to search for
        /// </summary>
        /// <param name="fileInfo">File Information</param>
        /// <returns>True if file matches attributes</returns>
        private bool FileCheckAttributes(FileInfo fileInfo)
        {
            if ((!Properties.Settings.Default.searchHidden) && ((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                return false;

            if ((!Properties.Settings.Default.searchArchives) && ((fileInfo.Attributes & FileAttributes.Archive) == FileAttributes.Archive))
                return false;

            if ((!Properties.Settings.Default.searchReadOnly) && ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly))
                return false;

            if ((!Properties.Settings.Default.searchSystem) && ((fileInfo.Attributes & FileAttributes.System) == FileAttributes.System))
                return false;

            return true;
        }

        private bool FolderIsIncluded(string dirPath)
        {
            foreach (string includeDir in Properties.Settings.Default.includedFolders)
            {
                if (string.Compare(includeDir, dirPath) == 0 || Utils.CompareWildcard(dirPath, includeDir))
                    return true;
            }

            return false;
        }

        private bool FolderIsExcluded(string dirPath)
        {
            foreach (string excludeDir in Properties.Settings.Default.excludedDirs)
            {
                if (Utils.CompareWildcard(dirPath, excludeDir))
                    return true;
            }

            return false;
        }

        private bool FileTypeIsExcluded(string fileName)
        {
            foreach (string excludeFileType in Properties.Settings.Default.excludedFileTypes)
            {
                if (Utils.CompareWildcard(fileName, excludeFileType))
                    return true;
            }

            return false;
        }

        private void timerUpdateFile_Tick(object sender, EventArgs e)
        {
            this.textBoxCurrentFile.Text = string.Copy(Analyze.CurrentFile);
            this.labelProblems.Text = string.Format("Files Found: {0}", Analyze.fileList.Count);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }

        private void Analyze_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (MessageBox.Show(this, "Are you sure?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        e.Cancel = true;
                    else
                        this.threadMain.Abort();
                }
            }
        }
    }
}
