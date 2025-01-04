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
