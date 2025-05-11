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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Little_Disk_Cleaner.Misc
{
    public partial class FileInfoCtrl : UserControl
    {
        public FileInfoCtrl()
        {
            InitializeComponent();

            ResetInfo();
        }

        public void UpdateInfo(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                ResetInfo();
                return;
            }

            // Get icon
            Icon fileIcon = Icon.ExtractAssociatedIcon(fileInfo.FullName);
            if (fileIcon != null)
                this.pictureBox1.Image = fileIcon.ToBitmap();
            else
                this.pictureBox1.Image = SystemIcons.Application.ToBitmap();

            this.labelFileName.Text = "File Name: " + fileInfo.Name;
            this.labelFileSize.Text = "File Size: " + Utils.ConvertSizeToString(fileInfo.Length);
            this.labelLocation.Text = "Location: " + fileInfo.DirectoryName;
            this.labelAccessed.Text = "Last Accessed: " + fileInfo.LastAccessTime.ToLongDateString();
        }

        public void ResetInfo()
        {
            this.pictureBox1.Image = SystemIcons.Application.ToBitmap();

            this.labelFileName.Text = "File Name: ";
            this.labelFileSize.Text = "File Size: ";
            this.labelLocation.Text = "Location: ";
            this.labelAccessed.Text = "Last Accessed: ";
        }
    }
}
