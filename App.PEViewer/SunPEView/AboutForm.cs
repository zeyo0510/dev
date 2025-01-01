using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SunPEView.Properties;
using System.Reflection;
using System.IO;

namespace SunPEView
{
    /// <summary>
    /// About Box
    /// </summary>
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            // set logo
            pictureBox1.Image = Resources.MainIconLarge;
            
            // set version
            //Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            string version = Application.ProductVersion;// ver.Major.ToString("X") + "." + ver.Minor.ToString("X2") + "." + ver.Build.ToString("X2");
            string buildDate = String.Format("{0:yyyy-MM-dd}", RetrieveLinkerTimestamp());
            label3.Text = version + "  /  " + buildDate;
            
            Height = 333;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sunshine2k.de");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:webmaster@sunshine2k.de");
        }

        /// <summary>
        /// Get the date of own executable from PE header.
        /// http://stackoverflow.com/questions/1600962/displaying-the-build-date
        /// </summary>
        /// <returns></returns>
        private DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }

    }
}
