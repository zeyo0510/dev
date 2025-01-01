using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SunPEView.PEModel.PEFormat.Resources.StandardId;
using System.IO;

namespace SunPEView.ContentVisualizer.Windows
{
    /// <summary>
    /// Form to visualize an html page and save it to file.
    /// </summary>
    public partial class HtmlPageWindow : Form
    {
        /// <summary>
        /// OpenFileDiaglog file filter
        /// </summary>
        private static readonly string FILEDIALOG_NAME_FILTER = "HTML files (*.html)|*.html|All files (*.*)|*.*";

        private PeHtmlPageResource htmlPageRes;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="htmlPageRes"></param>
        internal HtmlPageWindow(PeHtmlPageResource htmlPageRes)
        {
            InitializeComponent();

            this.htmlPageRes = htmlPageRes;
            if (htmlPageRes.HtmlPageStr != null)
            {
                webBrowser1.DocumentText = htmlPageRes.HtmlPageStr;
            }
        }

        /// <summary>
        /// Called when save icon button was clicked. Save html page to file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void savePageButton_Click(object sender, EventArgs e)
        {
            if (htmlPageRes.HtmlPageStr != null)
            {
                SaveFileDialog sfn = new SaveFileDialog();
                sfn.Filter = FILEDIALOG_NAME_FILTER;
                if (sfn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfn.FileName, webBrowser1.DocumentText);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
