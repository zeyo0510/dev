using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SunPEView.PEModel.PEFormat.Resources.StandardId;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat.Resources;
using SunPEView.PEModel.PEFormat;
using System.IO;

namespace SunPEView.ContentVisualizer.Windows
{
    /// <summary>
    /// Form to visualize an icon and save it to file.
    /// </summary>
    public partial class IconForm : Form
    {
        /// <summary>
        /// OpenFileDiaglog file filter
        /// </summary>
        private static readonly string FILEDIALOG_NAME_FILTER = "Icon files (*.ico)|*.ico|All files (*.*)|*.*";
        /// <summary>
        /// PeIconResource from file to show
        /// </summary>
        private PeIconResource iconRes;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iconRes"></param>
        internal IconForm(PeIconResource iconRes)
        {
            InitializeComponent();
            this.iconRes = iconRes;
        }

        /// <summary>
        /// Overriden paint method to draw icon
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (iconRes.getIcon() != null)
            {
                e.Graphics.DrawIcon(iconRes.getIcon(), 20, 50);
            }
        }

        /// <summary>
        /// Called when save icon button was clicked. Save icon to file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveIconButton_Click(object sender, EventArgs e)
        {
            if (iconRes.getIcon() != null)
            {
                SaveFileDialog sfn = new SaveFileDialog();
                sfn.Filter = FILEDIALOG_NAME_FILTER;
                if (sfn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(sfn.FileName, System.IO.FileMode.Create,
                                  System.IO.FileAccess.Write);
                    byte[] iconData = iconRes.getIconFileBuffer();
                    fs.Write(iconData, 0, iconData.Length);
                    fs.Close();
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
