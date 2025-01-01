using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using SunPEView.PEModel.Exceptions;
using System.Windows.Forms;
using System.Drawing;
using SunPEView.ContentVisualizer;


namespace SunPEView
{
    partial class Form1
    {
        /// <summary>
        /// flag for internally disabling the text changed events of the address textboxes
        /// </summary>
        private bool isFlcTextChangeEventDisabled;

        /// <summary>
        /// Init toolbar.
        /// </summary>
        public void InitFileLocationConversion()
        {
            toolStripRVATextBox.TextChanged += new EventHandler(toolStripRVATextBox_TextChanged);
            toolStripVATextBox.TextChanged += new EventHandler(toolStripVATextBox_TextChanged);
            toolStripFileOffsetTextBox.TextChanged += new EventHandler(toolStripFileOffsetTextBox_TextChanged);
            toolStripGoToAddressButton.Click += new EventHandler(toolStripGoToAddressButton_Click);

            dataGridView1.ContextMenuSendToFlcItemClicked += new ContentVisualizer.ContentDataGridView.ContextMenuFlcItemClickedDelegate(dataGridView1_ContextMenuFlcItemClicked);
            dataGridView1.ContextMenuGotoHexBoxFlcClicked += new ContentDataGridView.ContextMenuFlcItemClickedDelegate(dataGridView1_ContextMenuGotoHexBoxFlcClicked);
        }

        /// <summary>
        /// Called when Rva textbox changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolStripRVATextBox_TextChanged(object sender, EventArgs e)
        {
            if (peFile == null || isFlcTextChangeEventDisabled) return;
            string rvaStr = StringUtil.GetHexStringWithoutPrefix(toolStripRVATextBox.Text);
            
            long rva;
            if (long.TryParse(rvaStr, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out rva))
            {
                SetFlcTextBoxStandardColors();
                try
                {
                    // get converted addresses
                    long fileOffset, va;
                    peFile.FileLocationCalculator.GetAddressesFromRVA(rva, out fileOffset, out va);
                    // show them in GUI
                    isFlcTextChangeEventDisabled = true;
                    toolStripVATextBox.Text = StringUtil.GetFormattedHexString(va);
                    toolStripFileOffsetTextBox.Text = StringUtil.GetFormattedHexString(fileOffset);
                    toolStripSectionTextBox.Text = peFile.FileLocationCalculator.GetSectionName(fileOffset);
                    isFlcTextChangeEventDisabled = false;
                }
                catch (AddressOutOfRange)
                {
                    isFlcTextChangeEventDisabled = true;
                    toolStripVATextBox.Text = String.Empty;
                    toolStripFileOffsetTextBox.Text = String.Empty;
                    toolStripRVATextBox.BackColor = Color.Tomato;
                    isFlcTextChangeEventDisabled = false;
                }
            }
            else if (rvaStr.Equals(String.Empty))
            {
                /* empty box */
                SetFlcTextBoxStandardColors();
            }
            else
            {
                // invalid value entered
                toolStripRVATextBox.BackColor = Color.Tomato;
            }
        }

        /// <summary>
        /// Called when Va textbox changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolStripVATextBox_TextChanged(object sender, EventArgs e)
        {
            if (peFile == null || isFlcTextChangeEventDisabled) return;
            string vaStr = StringUtil.GetHexStringWithoutPrefix(toolStripVATextBox.Text);

            long va;
            if (long.TryParse(vaStr, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out va))
            {
                SetFlcTextBoxStandardColors();
                try
                {
                    // get converted addresses
                    long fileOffset, rva;
                    peFile.FileLocationCalculator.GetAddressesFromVA(va, out fileOffset, out rva);
                    // show them in GUI
                    isFlcTextChangeEventDisabled = true;
                    toolStripRVATextBox.Text = StringUtil.GetFormattedHexString(rva);
                    toolStripFileOffsetTextBox.Text = StringUtil.GetFormattedHexString(fileOffset);
                    toolStripSectionTextBox.Text = peFile.FileLocationCalculator.GetSectionName(fileOffset);
                    isFlcTextChangeEventDisabled = false;
                }
                catch (AddressOutOfRange)
                {
                    isFlcTextChangeEventDisabled = true;
                    toolStripRVATextBox.Text = String.Empty;
                    toolStripFileOffsetTextBox.Text = String.Empty;
                    toolStripVATextBox.BackColor = Color.Tomato;
                    isFlcTextChangeEventDisabled = false;
                }
            }
            else if (vaStr.Equals(String.Empty))
            {
                /* empty box */
                SetFlcTextBoxStandardColors();
            }
            else
            {
                // invalid value entered
                toolStripVATextBox.BackColor = Color.Tomato;
            }
        }

        /// <summary>
        /// Called when file offset textbox changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolStripFileOffsetTextBox_TextChanged(object sender, EventArgs e)
        {
            if (peFile == null || isFlcTextChangeEventDisabled) return;
            string fileOffStr = StringUtil.GetHexStringWithoutPrefix(toolStripFileOffsetTextBox.Text);

            long fileOffset;
            if (long.TryParse(fileOffStr, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out fileOffset))
            {
                SetFlcTextBoxStandardColors();
                try
                {
                    // get converted addresses
                    long va, rva;
                    peFile.FileLocationCalculator.GetAddressesFromFileOffset(fileOffset, out rva, out va);
                    // show them in GUI
                    isFlcTextChangeEventDisabled = true;
                    toolStripRVATextBox.Text = StringUtil.GetFormattedHexString(rva);
                    toolStripVATextBox.Text = StringUtil.GetFormattedHexString(va);
                    toolStripSectionTextBox.Text = peFile.FileLocationCalculator.GetSectionName(fileOffset);
                    isFlcTextChangeEventDisabled = false;
                }
                catch (AddressOutOfRange)
                {
                    isFlcTextChangeEventDisabled = true;
                    toolStripRVATextBox.Text = String.Empty;
                    toolStripVATextBox.Text = String.Empty;
                    toolStripFileOffsetTextBox.BackColor = Color.Tomato;
                    isFlcTextChangeEventDisabled = false;
                }
            }
            else if (fileOffStr.Equals(String.Empty))
            {
                /* empty box */
                SetFlcTextBoxStandardColors();
            }
            else
            {
                // invalid value entered
                toolStripFileOffsetTextBox.BackColor = Color.Tomato;
            }
        }

        /// <summary>
        /// Called when 'Show Address in HexBox' is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolStripGoToAddressButton_Click(object sender, EventArgs e)
        {
            if (!toolStripFileOffsetTextBox.Text.Equals(String.Empty))
            {
                string fileOffStr = StringUtil.GetHexStringWithoutPrefix(toolStripFileOffsetTextBox.Text);
                long fileOffset;
                if (long.TryParse(fileOffStr, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out fileOffset))
                {
                    if (hexEditBox1.isOffsetValid((int)fileOffset))
                    {
                        hexEditBox1.GoToAddress((int)fileOffset);
                    }
                }
            }
        }

        private void SetFlcTextBoxStandardColors()
        {
            toolStripVATextBox.BackColor = SystemColors.Window;
            toolStripFileOffsetTextBox.BackColor = SystemColors.Window;
            toolStripRVATextBox.BackColor = SystemColors.Window;
        }


        void dataGridView1_ContextMenuFlcItemClicked(object sender, ContentDataGridView.ContextMenuFlcClickedEventArgs e)
        {
            switch (e.Type)
            {
                case ContentDataGridView.ContextMenuFlcClickedEventArgs.FLCVALUETYPE.RVA:
                    toolStripRVATextBox.Text = e.Value;
                    toolStripRVATextBox.PerformClick();
                    break;
                case ContentDataGridView.ContextMenuFlcClickedEventArgs.FLCVALUETYPE.VA:
                    toolStripVATextBox.Text = e.Value;
                    toolStripVATextBox.PerformClick();
                    break;
                case ContentDataGridView.ContextMenuFlcClickedEventArgs.FLCVALUETYPE.FILEOFFSET:
                    toolStripFileOffsetTextBox.Text = e.Value;
                    toolStripFileOffsetTextBox.PerformClick();
                    break;
                default: break;
            }
        }

        void dataGridView1_ContextMenuGotoHexBoxFlcClicked(object sender, ContentDataGridView.ContextMenuFlcClickedEventArgs e)
        {
            long valueAsLong;
            if (long.TryParse(e.Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out valueAsLong))
            {
                long fileOffset = 0;
                switch (e.Type)
                {
                    case ContentDataGridView.ContextMenuFlcClickedEventArgs.FLCVALUETYPE.RVA:
                        fileOffset = peFile.FileLocationCalculator.GetFileOffsetFromRVA(valueAsLong);
                        break;
                    case ContentDataGridView.ContextMenuFlcClickedEventArgs.FLCVALUETYPE.VA:
                        fileOffset = peFile.FileLocationCalculator.GetFileOffsetFromVA(valueAsLong);
                        break;
                    case ContentDataGridView.ContextMenuFlcClickedEventArgs.FLCVALUETYPE.FILEOFFSET:
                        fileOffset = valueAsLong;
                        break;
                    default: break;
                }

                if (hexEditBox1.isOffsetValid((int)fileOffset))
                {
                    hexEditBox1.GoToAddress((int)fileOffset);
                }
            }        
        }
    }
}
