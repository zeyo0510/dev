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
    public partial class BitmapForm : Form
    {
        private static readonly string FILEDIALOG_NAME_FILTER = "BMP files (*.bmp)|*.bmp|All files (*.*)|*.*";

        private PeBitmapResource bitmapRes;

        internal BitmapForm(PeBitmapResource bitmapRes)
        {
            InitializeComponent();
            this.bitmapRes = bitmapRes;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (bitmapRes.GetBitmap() != null)
            {
                e.Graphics.DrawImage(bitmapRes.GetBitmap(), 20, 50);
            }
        }

        private void saveIconButton_Click(object sender, EventArgs e)
        {
            if (bitmapRes.GetBitmap() != null)
            {
                SaveFileDialog sfn = new SaveFileDialog();
                sfn.Filter = FILEDIALOG_NAME_FILTER;
                if (sfn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bitmapRes.GetBitmap().Save(sfn.FileName);
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
