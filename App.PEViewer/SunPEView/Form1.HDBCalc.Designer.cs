using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Globalization;

namespace SunPEView
{
    /// <summary>
    /// Partical class for handling the Hex-Dec-Bin calculator toolbar.
    /// </summary>
    partial class Form1
    {
        /// <summary>
        /// flag for internally disabling the text changed events of the address textboxes
        /// </summary>
        private bool isHdbTextChangeEventDisabled;

        /// <summary>
        /// Init toolbar.
        /// </summary>
        public void InitHdbCalculatorToolBar()
        {
            toolStripHEXTextBox.TextChanged += new EventHandler(toolStripHEXTextBox_TextChanged);
            toolStripDECTextBox.TextChanged += new EventHandler(toolStripDECTextBox_TextChanged);
            toolStripBINTextBox.TextChanged += new EventHandler(toolStripBINTextBox_TextChanged);
        }

        /// <summary>
        /// Called when text in hex textbox changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolStripHEXTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isHdbTextChangeEventDisabled) return;

            string hexValStr = StringUtil.GetHexStringWithoutPrefix(toolStripHEXTextBox.Text);

            long hexVal;
            if (long.TryParse(hexValStr, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hexVal))
            {
                SetHdbTextBoxStandardColors();
                isHdbTextChangeEventDisabled = true;
                toolStripBINTextBox.Text = Convert.ToString(hexVal, 2);
                toolStripDECTextBox.Text = Convert.ToString(hexVal, 10);
                isHdbTextChangeEventDisabled = false;
            }
            else if (hexValStr.Equals(String.Empty))
            {
                /* empty box */
                SetHdbTextBoxStandardColors();
            }
            else
            {
                /* invalid value entered */
                toolStripHEXTextBox.BackColor = Color.Tomato;
            }
        }

        /// <summary>
        /// Called when text in decimal textbox changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolStripDECTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isHdbTextChangeEventDisabled) return;

            long decVal;
            if (long.TryParse(toolStripDECTextBox.Text, out decVal))
            {
                SetHdbTextBoxStandardColors();
                isHdbTextChangeEventDisabled = true;
                toolStripBINTextBox.Text = Convert.ToString(decVal, 2);
                toolStripHEXTextBox.Text = Convert.ToString(decVal, 16);
                isHdbTextChangeEventDisabled = false;
            }
            else if (toolStripDECTextBox.Text.Equals(String.Empty))
            {
                /* empty box */
                SetHdbTextBoxStandardColors();
            }
            else
            {
                /* invalid value entered */
                toolStripDECTextBox.BackColor = Color.Tomato;
            }
        }

        /// <summary>
        /// Called when text in binary textbox changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolStripBINTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isHdbTextChangeEventDisabled) return;

            if (StringUtil.IsValidBinaryString(toolStripBINTextBox.Text))
            {
                long decVal = Convert.ToInt64(toolStripBINTextBox.Text, 2);
                SetHdbTextBoxStandardColors();
                isHdbTextChangeEventDisabled = true;
                toolStripDECTextBox.Text = Convert.ToString(decVal, 10);
                toolStripHEXTextBox.Text = Convert.ToString(decVal, 16);
                isHdbTextChangeEventDisabled = false;
            }
            else if (toolStripDECTextBox.Text.Equals(String.Empty))
            {
                /* empty box */
                SetHdbTextBoxStandardColors();
            }
            else
            {
                /* invalid value entered */
                toolStripBINTextBox.BackColor = Color.Tomato;
            }
        }

        private void SetHdbTextBoxStandardColors()
        {
            toolStripHEXTextBox.BackColor = SystemColors.Window;
            toolStripDECTextBox.BackColor = SystemColors.Window;
            toolStripBINTextBox.BackColor = SystemColors.Window;
        }

    }
}
