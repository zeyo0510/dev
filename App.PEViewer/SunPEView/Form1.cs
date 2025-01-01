using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;
using SunPEView.ContentVisualizer;
using SunPEView.ContentVisualizer.Interfaces;
using System.IO;

/*
 * SunPEView
 * 0.7.1.0
 * 
 * www.sunshine2k.de || www.bastian-molkenthin.de
 */

/*
 * History:
 * 
 * 0.7.1.0
 * - Improved bit flag characteristics window
 * - Close windows with escape key
 * - Log window (TODO: more useful output)
 * 
 * 0.7.0.0:
 * - FLC: show section name
 * - Error Messages
 * - fixed a scrolling bug in the hex view
 * - support of Base Relocations, Exception, TLS DataDirectory
 * - Standard Resource Support: 
 *   - Bitmaps 
 *   - Cursors & Cursors Groups
 *   - Config Files
 *   - Version Info
 *   - Icons & Icon Groups
 *   - String Tables
 *   - HTML files
 * - Standard Resource Support Basic (only hex window):
 *   - Dialogs  
 *   - Menus
 *   - Accelerators
 *   - RCData
 *   - Font, Fonts Directory
 *   - Message Tables
 * - DEP support of PE image
 * - Reload button
 * 
 * 0.6.0.0:
 * - first released version
 * 
 */

namespace SunPEView
{
    public partial class Form1 : Form
    {
        private static readonly string FILEDIALOG_NAME_FILTER = "Exe/Dll files (*.exe, *.dll)|*.exe;*.dll|All files (*.*)|*.*";

        private PEFile peFile;

        public Form1()
        {
            InitializeComponent();
            // assign application icon to main form
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.Text = "SunPEView " + Application.ProductVersion;

            dataGridView1.Tag = this;
            InitTreeHandler();
            InitFileLocationConversion();
            InitHdbCalculatorToolBar();

            dataGridView1.SelectedRowChanged += new SelectedRowChangedDelegate(dataGridView1_SelectedRowChanged);

            hexEditBox1.SelectedOffsetChanged += new HexEditBox.HexEditBox.SelectedOffsetChangedDelegate(hexEditBox1_SelectedOffsetChanged);

            GuiLogger guiLogger = new GuiLogger(logBox);
            PELogger.Instance.logEvent += new PELogger.LogEventDelegate(guiLogger.logger_logEvent);
            logContextMenuClearLog.Click += new EventHandler(guiLogger.logContextMenuClearLog_Click);
        }

        public PEFileTreeView.PEFileTreeViewControl getPeTreeView() { return peFileTreeView1; }

        void hexEditBox1_SelectedOffsetChanged(object sender, HexEditBox.HexEditBox.OffsetSelectedChangeEvents e)
        {
            toolStripSelOffsetLabel.Text = 
                StringUtil.GetFormattedHexString(e.Offset) +
                " (Hex: " + StringUtil.GetFormattedByteString(e.ByteInfo.hexbyte) + 
                " , Ascii: " + StringUtil.GetFormattedCharString(e.ByteInfo.asciibyte) + ")";
        }

        void dataGridView1_SelectedRowChanged(object sender, SectionRowChangeEvents e)
        {
            Console.Out.WriteLine(e.PeElem);
            hexEditBox1.RemoveAllHighlightRegions();
            hexEditBox1.AddHighlightRegion((int)e.PeElem.Offset, e.PeElem.SizeInBytes(), Color.Yellow);
            if (!hexEditBox1.IsAddressVisible((int)e.PeElem.Offset))
            {
                hexEditBox1.GoToAddress((int)e.PeElem.Offset);
            }
            toolStripSelOffsetLabel.Text = StringUtil.GetFormattedHexString((int)e.PeElem.Offset);
        }

        IContentVisualizer getContentVisualizer()
        {
            return dataGridView1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadOpenFile();
        }

        /// <summary>
        /// Load a new file and initialize all components.
        /// </summary>
        private void LoadOpenFile()
        {
            // load the binary file
            //peFile = new PEFile(@"E:\Working\PEView\TestFiles\LogOps.exe");
                    
            peFile.LoadFile();
            // init hex box
            hexEditBox1.ByteProvider = peFile;
            // add to treeview
            AddFile();
            AddFileSections();
        }

        #region Main Menu Handling

        private void hexBytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contentHexeditSplitContainer.Panel2Collapsed = hexBytesToolStripMenuItem.Checked;
            hexBytesToolStripMenuItem.Checked = !hexBytesToolStripMenuItem.Checked;
        }

        private void fileLocationCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addressConverterToolStrip.Visible = !fileLocationCalculatorToolStripMenuItem.Checked;
            fileLocationCalculatorToolStripMenuItem.Checked = !fileLocationCalculatorToolStripMenuItem.Checked;
        }

        private void hexDecBinCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hdbCalcToolStrip.Visible = !hexDecBinCalculatorToolStripMenuItem.Checked;
            hexDecBinCalculatorToolStripMenuItem.Checked = !hexDecBinCalculatorToolStripMenuItem.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.StartPosition = FormStartPosition.CenterScreen;
            aboutForm.ShowDialog();
        }

        private void logWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainLogSplitContainer.Panel2Collapsed = logWindowToolStripMenuItem.Checked;
            logWindowToolStripMenuItem.Checked = !logWindowToolStripMenuItem.Checked;
        }

        #endregion

        /// <summary>
        /// Open and load a new PE file if possible
        /// </summary>
        private void OpenFile()
        {
            if (CanOpenFile())
            {
                OpenFileDialog ofn = new OpenFileDialog();
                ofn.Filter = FILEDIALOG_NAME_FILTER;
                if (ofn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OpenFile(ofn.FileName);
                }
            }
        }

        /// <summary>
        /// Checks if a new PE file can be opened. Ask user to close current one if a file is already open.
        /// </summary>
        /// <returns>true if a new file can be opened. False if there is a file already open
        /// and was not yet closed.</returns>
        private bool CanOpenFile()
        {
            if (peFile != null)
            {
                DialogResult dlgRes = MessageBox.Show(
                    "Currently only one file can be opened at the same time.\n" +
                    "Do you want to close the current file?",
                    "Close confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgRes == System.Windows.Forms.DialogResult.Yes)
                {
                    CloseFile();
                    System.GC.Collect();
                    return true;
                }
                else if (dlgRes == System.Windows.Forms.DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Open new PE file and load it into GUI.
        /// </summary>
        /// <param name="filename"></param>
        private void OpenFile(string filename)
        {
            peFile = new PEFile(filename);
            peFile.LoadFile();

            // init hex box
            hexEditBox1.ByteProvider = peFile;

            // add to treeview
            AddFile();
            AddFileSections();
        }

        /// <summary>
        /// Close the pe file and reset GUI elements.
        /// </summary>
        private void CloseFile()
        {
            peFileTreeView1.Clear();
            hexEditBox1.ByteProvider = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            peFile = null;
        }

        #region ToolStripMenu
        /// <summary>
        /// Open file in tool strip menu clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        /// <summary>
        /// CLose file in tool strip menu clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        /// <summary>
        /// Reload file on tool strip menu clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reloadToolStripButton_Click(object sender, EventArgs e)
        {
            if (peFile != null)
            {
                string filepathTmp = peFile.FilePath;
                CloseFile();
                OpenFile(filepathTmp);
            }
        }

        #endregion

        #region DragnDrop

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                if (CanOpenFile())
                {
                    OpenFile(files[0]);
                }
            }
        }

        #endregion



    }
}
