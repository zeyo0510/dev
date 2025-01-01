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
    public partial class CursorForm : Form
    {
        private static readonly string FILEDIALOG_NAME_FILTER = "CUR files (*.cur)|*.cur|All files (*.*)|*.*";
        private PeCursorResource cursorRes;

        internal CursorForm(PeCursorResource curRes)
        {
            InitializeComponent();
            this.cursorRes = curRes;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (cursorRes.getCursor() != null)
            {
                Cursor cur = cursorRes.getCursor();
                cur.Draw(e.Graphics, new Rectangle(20, 50, cursorRes.CursorWidth, cursorRes.CursorHeight));

                //Bitmap bitmap = new Bitmap(cur.Size.Width, cur.Size.Height);
                //using (Graphics gBmp = Graphics.FromImage(bitmap))
                //{
                //    cur.Draw(gBmp, new Rectangle(0, 0, cur.Size.Width, cur.Size.Height));
                //    e.Graphics.DrawImage(bitmap, 40, 100);
                //}
            }
        }

        private void saveCursorButton_Click(object sender, EventArgs e)
        {
            if (cursorRes.getCursor() != null)
            {
                SaveFileDialog sfn = new SaveFileDialog();
                sfn.Filter = FILEDIALOG_NAME_FILTER;
                if (sfn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(sfn.FileName, System.IO.FileMode.Create,
                                  System.IO.FileAccess.Write);
                    byte[] iconData = cursorRes.getCursorFileBuffer();
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
