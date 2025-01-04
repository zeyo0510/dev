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
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Little_Disk_Cleaner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool bMutexCreated = false;
            Mutex mutexMain = new Mutex(true, "Little Disk Cleaner", out bMutexCreated);

            // If mutex isnt available, show message and exit...
            if (!bMutexCreated)
            {
                MessageBox.Show("Another program seems to be already running...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            // Add event handler for thread exceptions
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            // Check if admin, otherwise exit
            if (!Permissions.IsUserAdministrator)
            {
                MessageBox.Show("You must be an administrator to use this program", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            // Enable needed privileges
            Permissions.SetPrivileges(true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());

            // Disable needed privileges
            Permissions.SetPrivileges(false);

            // Release Mutex
            mutexMain.ReleaseMutex();

            // Save Settings
            Properties.Settings.Default.Save();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CrashReporter ErrorDlg = new CrashReporter((Exception)e.ExceptionObject);
            ErrorDlg.ShowDialog();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            CrashReporter ErrorDlg = new CrashReporter(e.Exception);
            ErrorDlg.ShowDialog();
        }
    }
}
