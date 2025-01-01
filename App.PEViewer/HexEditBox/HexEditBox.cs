using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

/*
 * Clipboard!
 */

namespace HexEditBox
{
    public class HexEditBox : RichTextBox
    {
        /*****************************************************
         * Constants
         *****************************************************/
        /// <summary>
        /// char offset in line where the actual hex data begins
        /// "1234-6789__" = 10 
        /// </summary>
        internal static readonly int LINE_OFFSET_ADDRESS_WITH_SPACE = 10;

        /// <summary>
        /// this is the position right after the last hex byte in a line
        /// </summary>
        internal static readonly int LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE = 57;

        /// <summary>
        /// number of spaces between hex bytes and chars
        /// </summary>
        internal static readonly int LINE_NUM_SPACES_HEX_BYTES_TO_CHARS = 2;

        /// <summary>
        /// position in line where the ascii values start
        /// </summary>
        internal static readonly int LINE_OFFSET_CHARS = LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE + LINE_NUM_SPACES_HEX_BYTES_TO_CHARS;

        /// <summary>
        /// number of characters in a line
        /// </summary>
        internal static readonly int LINE_TOTAL_NUM_OF_CHARS = LINE_OFFSET_CHARS + 16;

        /// <summary>
        /// color for address font
        /// </summary>
        private readonly Color addressColor = Color.Gray;

        /// <summary>
        /// WM_PAINT message identifier, used to preprocess and suppress this message
        /// </summary>
        const short WM_PAINT = 0x00f;

        /*****************************************************
         * Variables
         *****************************************************/
        /// <summary>
        /// internal flag to temporary suppress of onSelectionChange handling
        /// </summary>
        private bool isOnSelectionChangeHandlingSuppressed;

        /// <summary>
        /// internal flag to temporary suppress of painting the control
        /// </summary>
        private static bool isPaintProcessingSuppressed = true;

        /// <summary>
        /// provider of byte content to display
        /// </summary>
        private IByteProvider byteProvider;

        /// <summary>
        /// flag if content of hexeditbox is readonly or can be modified
        /// </summary>
        private bool isReadOnlyContent;

        /// <summary>
        /// number of visible lines at a time
        /// </summary>
        private int numOfVisibleLines;

        /// <summary>
        /// currently first visible hex line in box
        /// </summary>
        private int startVisibleLine;

        /// <summary>
        /// currently last visible hex line in box
        /// </summary>
        private int endVisibleLine;

        private ContextMenuHandler contextMenuHandler;

        /*****************************************************
         * Constructors
         *****************************************************/
        public HexEditBox() : base()
        {
            this.Font = new Font("Courier New", 9, FontStyle.Regular);
            this.ScrollBars = RichTextBoxScrollBars.Horizontal;
            this.isOnSelectionChangeHandlingSuppressed = false;
            HexEditBox.isPaintProcessingSuppressed = false;
            this.offsetChangeEventSuppressed = false;
            this.isReadOnlyContent = true;
            this.numOfVisibleLines = 0;      // this value is calculated later
            this.CreateVerticalScrollbar();
            //markedRegions = new List<MarkedRegion>();
            markedRegionsHandler = new MarkedRegionsHandler(this);
            this.MouseWheel += new MouseEventHandler(HexEditBox_MouseWheel);
        }

        void HexEditBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                vScrollBar.Value = Math.Min(this.vScrollBar.Maximum, vScrollBar.Value + 1);
            }
            else if (e.Delta > 0)
            {
                vScrollBar.Value = Math.Max(this.vScrollBar.Minimum, vScrollBar.Value - 1);
            }
        }


        /*****************************************************
         * Number of visible lines hack
         *****************************************************/
        #region Number of visible lines hack
        public void GetNumberOfVisibleLines()
        {
            if (vScrollBar == null) return;     // hack to avoid excpetion on resizing

            // make backup of cotent
            string contentBackup = this.Text;
            // prevent updating
            this.SuspendLayout();

            // add lines to listbox and use built-in functions to get number of visible lines
            this.Clear();
            for (int i = 0; i < 100; i++)
            {
                Text += Environment.NewLine;
            }
            Point pos = new Point(0, 0);
            int firstIndex = this.GetCharIndexFromPosition(pos);
            int firstLine = this.GetLineFromCharIndex(firstIndex);
            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = this.GetCharIndexFromPosition(pos);
            int lastLine = this.GetLineFromCharIndex(lastIndex);
            // get finally number of visible lines
            numOfVisibleLines = lastLine - firstLine;
            // restore content
            this.Text = contentBackup;
            
            // active painting again
            this.ResumeLayout();
        }

        /// <summary>
        /// Handle event to recalculate the number of visible lines at one time.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SuspendLayout();
            GetNumberOfVisibleLines();
            if (Text.Length > 0)
            {   // check avoid exception, because the message might be processed during an empty box state.
                ReloadVisibleBoxContent();
            }
            ResumeLayout();
            
        }

        

        /// <summary>
        /// Handle event to recalculate the number of visible lines at one time.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SuspendLayout();
            GetNumberOfVisibleLines();
            if (Text.Length > 0)
            {   // check avoid exception, because the message might be processed during an empty box state.
                ColorVisibleLines();
            }
            ResumeLayout();
        }

        /// <summary>
        /// Handle event to recalculate the number of visible lines at one time.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            GetNumberOfVisibleLines();
            if (Text.Length > 0)
            {   // check avoid exception, because the message might be processed during an empty box state.
                ColorVisibleLines();
            }
        }
        #endregion


        /*****************************************************
         * Overriden methods / events
         *****************************************************/
        #region Overwritten methods / events
        /// <summary>
        /// Handle the KeyPress event to
        /// - overwrite the bytes without inserting new or invalid characters
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (isReadOnlyContent)
            {
                e.Handled = true;
                return;
            }

            offsetChangeEventSuppressed = true;

            if (this.SelectionLength == 0)
            {
                /* get current selected colunm position in current line */
                int column = getColumnInCurrentLine();

                if (isCaretInHexByteRange(column))
                {
                    if (isCaretAtWhiteSpacePosition(column))
                    {
                        if (isCaretAtEndOfByteRange(column))
                        {
                            /* we are at end of hex byte line, so move to beginning of next line */
                            int curline = this.GetLineFromCharIndex(this.SelectionStart);
                            if (curline < this.Lines.Length)
                            {
                                this.SelectionStart = this.GetFirstCharIndexFromLine(curline + 1) + LINE_OFFSET_ADDRESS_WITH_SPACE;
                            }
                        }
                        else
                        {
                            /* proceed one step to beginning of next byte */
                            SelectionStart++;
                        }
                    }
                    else if ((isCaretAtByteStartPosition(column) || isCaretAtMidBytePosition(column)) &&
                             isValidHexCharacter(e.KeyChar))
                    {
                        // important to save the caret position at this point because SelectionStart is modified in subsequent calls
                        int posTmp = this.SelectionStart;
                        ReplaceChar(posTmp, e.KeyChar);
                        UpdateLineAtCharPos(posTmp);
                    }
                }
                else if (isCaretInAsciiRange(column))
                {
                    int posTmp = this.SelectionStart;
                    UpdateAsciiChar(posTmp, e.KeyChar);
                    UpdateLineAtCharPos(posTmp);
                    if (isCaretAtEndOfAsciiRange(column))
                    {
                        /* we are at end of hex byte line, so move to beginning of next line */
                        int curline = this.GetLineFromCharIndex(this.SelectionStart);
                        if (curline < this.Lines.Length)
                        {
                            this.SelectionStart = this.GetFirstCharIndexFromLine(curline) + LINE_OFFSET_CHARS;
                        }
                    }
                }
            }

            offsetChangeEventSuppressed = false;
            processOffsetChangedEvents();

            e.Handled = true;
            base.OnKeyPress(e);
        }



        /// <summary>
        /// Handle the KeyDown event to 
        /// - improve navigation with arrow keys.
        /// - avoid deletion of content
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            /* avoid deleting of content */
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
            }

            offsetChangeEventSuppressed = true;

            if (this.SelectionLength == 0)
            {
                /* get index of first char in current line */
                int firstCharIndex = this.GetFirstCharIndexOfCurrentLine();
                /* get current selected colunm position in current line */
                int column = this.SelectionStart - firstCharIndex;

                if (isCaretInHexByteRange(column))
                {
                    /* special case: caret is behind the last byte and right arrow pressed
                     * -> swap to beginning of next line */
                    if (isCaretAtEndOfByteRange(column) && e.KeyCode == Keys.Right)
                    {
                        /* we are at end of hex byte line, so move to beginning of next line */
                        int curline = this.GetLineFromCharIndex(this.SelectionStart);
                        if (curline < this.Lines.Length)
                        {
                            this.SelectionStart = this.GetFirstCharIndexFromLine(curline + 1) +
                                                       LINE_OFFSET_ADDRESS_WITH_SPACE;
                            e.Handled = true;
                        }
                    }
                    /* special case: caret is in front of first byte in line and left arrow pressed
                     * -> swap to end of previous line */
                    else if (isCaretAtStartOfByteRange(column) && e.KeyCode == Keys.Left)
                    {
                        /* caret is at beginning of hex line, so move to end of previous line */
                        int curline = this.GetLineFromCharIndex(this.SelectionStart);
                        if (curline > 0)
                        {
                            this.SelectionStart = this.GetFirstCharIndexFromLine(curline - 1) +
                                                LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE - 2;
                            e.Handled = true;
                        }
                    }
                    /* caret in front of byte && Key Right -> move right to next byte */
                    else if (isCaretAtByteStartPosition(column) && e.KeyCode == Keys.Right)
                    {
                        SelectionStart += (isCaretAtFrontOfLastByte(column)) ? 2 : 3;
                        e.Handled = true;
                    }
                    /* caret in front of byte && Key Left -> move left to next byte */
                    else if (isCaretAtByteStartPosition(column) && e.KeyCode == Keys.Left)
                    {
                        SelectionStart -= 3;
                        e.Handled = true;
                    }
                }


            } /* end SelectionLength == 0 */

            offsetChangeEventSuppressed = false;
            processOffsetChangedEvents();

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Handle the onSelectionChanged event
        /// - to prevent selecting the address part at the beginning of a line (acting as readonly part then)
        /// - correct handling for the OffsetChanged event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            if (isOnSelectionChangeHandlingSuppressed)
            {
                return;
            }

            if (byteProvider == null)
            {
                return;
            }

            /* get index of first char in current line */
            int firstCharIndex = this.GetFirstCharIndexOfCurrentLine();

            /* get current selected colunm position in current line */
            int column = getColumnInCurrentLine();

            /* prevent selection of the address part */
            if (column < LINE_OFFSET_ADDRESS_WITH_SPACE)
            {
                this.SelectionStart = firstCharIndex + LINE_OFFSET_ADDRESS_WITH_SPACE;
            }

            if (this.SelectionStart < byteProvider.Length)
            {
                processOffsetChangedEvents();
            }
        }

        /// <summary>
        /// Override WndProc to prevent flickering when updating the richtextbox.
        /// To disable painting temporary, set <paramref name="isPaintProcessingSuppressed"/> to true.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // Code courtesy of Mark Mihevc
            // sometimes we want to eat the paint message so we don't have to see all the
            // flicker from when we select the text to change the color.
            if (m.Msg == WM_PAINT)
            {
                if (!isPaintProcessingSuppressed)
                    base.WndProc(ref m); // if we decided to paint this control, just call the RichTextBox WndProc
                else
                    m.Result = IntPtr.Zero; // not painting, must set this to IntPtr.Zero if not painting therwise serious problems.
            }
            else
                base.WndProc(ref m); // message other than WM_PAINT, jsut do what you normally do.
        }
        #endregion

        /*****************************************************
         * Private Methods
         *****************************************************/
        #region Private Methods
        /// <summary>
        /// Get the column of the caret in column line
        /// </summary>
        /// <returns></returns>
        internal int getColumnInCurrentLine()
        {
            /* get index of first char in current line */
            int firstCharIndex = this.GetFirstCharIndexOfCurrentLine();
            /* get current selected colunm position in current line */
            return (this.SelectionStart - firstCharIndex);
        }

        /// <summary>
        /// Get the current selected, absolute line number
        /// </summary>
        /// <returns></returns>
        internal int getAbsoluteCurrentLine()
        {
            return startVisibleLine + GetLineFromCharIndex(this.SelectionStart);
        }

        /// <summary>
        /// Check if column is at front of a hex byte
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal bool isCaretAtByteStartPosition(int column)
        {
            // byte start is at 11, 14, 17..
            return (((column - LINE_OFFSET_ADDRESS_WITH_SPACE) % 3) == 0);
        }

        /// <summary>
        /// Check if column is in the middle of a hex byte
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal bool isCaretAtMidBytePosition(int column)
        {
            // byte start is at 12, 15..
            return (((column - LINE_OFFSET_ADDRESS_WITH_SPACE) % 3) == 1);
        }

        /// <summary>
        /// Checks if column is currently at a whitespace position in the hex byte range
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal bool isCaretAtWhiteSpacePosition(int column)
        {
            // byte start is at 13, 16,
            return (((column - LINE_OFFSET_ADDRESS_WITH_SPACE) % 3) == 2);
        }

        /// <summary>
        /// Check if column is in hex byte range, including first and end position 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal bool isCaretInHexByteRange(int column)
        {
            return (column >= LINE_OFFSET_ADDRESS_WITH_SPACE && column <= LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE);
        }

        /// <summary>
        /// Check if column is directly at start of hex bytes
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal bool isCaretAtStartOfByteRange(int column)
        {
            return (column == LINE_OFFSET_ADDRESS_WITH_SPACE);
        }

        /// <summary>
        /// Check if column is directly at the end of hex bytes
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal bool isCaretAtEndOfByteRange(int column)
        {
            return (column == LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE);
        }

        /// <summary>
        /// Check if column is directly in front of last hex byte
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal bool isCaretAtFrontOfLastByte(int column)
        {
            return (column == (LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE - 2));
        }

        /// <summary>
        /// Check if column is in ASCII range, includes the first position
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal bool isCaretInAsciiRange(int column)
        {
            return (column >= LINE_OFFSET_CHARS);
        }


        /// <summary>
        /// Check if column is in ASCII range, includes the first position, excludes end of line
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal bool isCaretInAsciiRangeWithoutLineEnd(int column)
        {
            return ((column >= LINE_OFFSET_CHARS) && (column < LINE_OFFSET_CHARS + 16));
        }

        /// <summary>
        /// Check if colum is at the end of a line.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool isCaretAtEndOfAsciiRange(int column)
        {
            return (column == (LINE_OFFSET_CHARS + 16));
        }

        /// <summary>
        /// Checks if character is a valid hex character (['0'-'9']|['a'-'f']['A'-'F'])
        /// </summary>
        /// <param name="c">character to check</param>
        /// <returns>true if c is a hex value</returns>
        private bool isValidHexCharacter(char c)
        {
            return (Char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));
        }

        /// <summary>
        /// Replace single character in richtextbox at given position.
        /// </summary>
        /// <param name="pos">start position in richtextbox</param>
        /// <param name="newChar">new character to insert</param>
        private void ReplaceChar(int pos, char newChar)
        {
            /* replace the old char at current position with the new one */
            SelectionStart = pos;
            SelectionLength = 1;
            SelectedText = newChar.ToString();
        }

        /// <summary>
        /// Replace string in richtextbox at given position.
        /// </summary>
        /// <param name="pos">start position in richtextbox</param>
        /// <param name="newStr">new string to insert</param>
        private void ReplaceString(int pos, string newStr)
        {
            SelectionStart = pos;
            SelectionLength = newStr.Length;
            SelectedText = newStr;
        }

        /// <summary>
        /// Replace the existing character at given position with the new one and update the hexeditbox.
        /// Does not update the actual data in byte provider! Thus after scrolling all changes are currently lost!
        /// </summary>
        /// <param name="pos">character position in hexeditbox</param>
        /// <param name="newChar">new charavter to insert</param>
        private void UpdateLineAtCharPos(int pos)
        {
            /* get the actual bytes (including the updated one) and build the new line */
            int curLineNum = this.GetLineFromCharIndex(pos);
            byte[] bytes = GetByteArrayFromLine(curLineNum);
            string newLine = ComposeLine((curLineNum + startVisibleLine)* 16, bytes, 0, 16);

            /* replace old line with new one */
            int firstCharIndex = this.GetFirstCharIndexFromLine(curLineNum);
            isPaintProcessingSuppressed = true;
            isOnSelectionChangeHandlingSuppressed = true;
            ReplaceString(firstCharIndex, newLine);
            isOnSelectionChangeHandlingSuppressed = false;
            ColorLine(curLineNum);
            isPaintProcessingSuppressed = false;

            this.SelectionStart = pos + 1;
        }

        /// <summary>
        /// Called if an ascii byte is changed in the hexbox. 
        /// Update the whole line.
        /// </summary>
        /// <param name="pos">position in line of changed character. Must be in CHARS / ASCII range.</param>
        /// <param name="newChar">new character to overwrite existing one with</param>
        private void UpdateAsciiChar(int pos, char newChar)
        {
            /* get character position of beginning of related hex byte */
            int curLineNum = this.GetLineFromCharIndex(pos);
            int columnInLine = getColumnInCurrentLine();
            int hexBytePos = (columnInLine - LINE_OFFSET_CHARS) * 3 + LINE_OFFSET_ADDRESS_WITH_SPACE;
            hexBytePos += this.GetFirstCharIndexFromLine(curLineNum);
            /* get character code as hex string */
            string byteStr = String.Format("{0:X2}", (int)newChar);
            ReplaceChar(hexBytePos, byteStr[0]);
            ReplaceChar(hexBytePos+1, byteStr[1]);
            this.SelectionStart = pos;
        }

        /// <summary>
        /// Get the byte values of the requested line in hexeditbox.
        /// </summary>
        /// <param name="curLineNum">line number to get bytes from</param>
        /// <returns>the byte values of the given line; has always length 16 but may contain whitespace strings</returns>
        private byte[] GetByteArrayFromLine(int curLineNum)
        {
            string curLineStr = this.Lines[curLineNum];
            string[] bytesStr = curLineStr.Substring(LINE_OFFSET_ADDRESS_WITH_SPACE, 16 * 3 - 1).Split(' ');

            // each line is is stuffed with spaces and thus equal long
            byte[] lineBytes = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                string byteStr = bytesStr[i];
                lineBytes[i] = Byte.Parse(byteStr, System.Globalization.NumberStyles.HexNumber);
            }

            return lineBytes;
        }

        /// <summary>
        /// Reload complete box content from byte provider and color content.
        /// </summary>
        internal void ReloadVisibleBoxContent()
        {
            // clear whole old context
            isPaintProcessingSuppressed = true;
            offsetChangeEventSuppressed = true;
            this.Clear();
            // update status variables
            startVisibleLine = Math.Max(0, vScrollBar.Value);
            endVisibleLine = startVisibleLine + numOfVisibleLines;
            // set scroll bar
            CalculateScrollbarMaximum();
            // recalculate location and size of scrollbar :-)
            vScrollBar.Location = new Point(this.Width - vScrollBar.Width - 4, 0);
            vScrollBar.Size = new Size(vScrollBar.Width, this.Height);
            // add content
            FillVisibleLines();
            // color lines
            ColorVisibleLines();
            // color highlight contents
            markedRegionsHandler.ColorRegions();
            // important to set selection to beginning to be able to scroll to first line
            this.SelectionStart = 0;
            offsetChangeEventSuppressed = false;
            isPaintProcessingSuppressed = false;
        }

        /// <summary>
        /// Fill visible area of richtext box with data.
        /// </summary>
        private void FillVisibleLines()
        {
            byte[] buffer = byteProvider.GetBytes();
            int addr = startVisibleLine * 16;
            
            StringBuilder sb = new StringBuilder(numOfVisibleLines * 70);
            for (int line = startVisibleLine; line <= endVisibleLine; line++)
            {
                sb.Append(ComposeLine(addr, buffer, addr, Math.Min(16, buffer.Length - addr)));
                sb.Append(Environment.NewLine);

                addr += 16;
            }
            this.Text = sb.ToString();

        }

        /// <summary>
        /// Get the string of one hex line including address, hex bytes and ascii bytes.
        /// </summary>
        /// <param name="addr">address / offset of first byte in line</param>
        /// <param name="buffer">bytes values to print. Only the first 16 bytes are considered.</param>
        /// <param name="count">number of actual bytes in buffer.</param>
        /// <returns></returns>
        private string ComposeLine(long addr, byte[] buffer, int bufferOffset, int count)
        {
            // get address
            string str = String.Format("{0:X8}  ", (uint)addr);

            // hex bytes
            for (int i = 0; i < 16; i++)
            {
                // in case last line contains less than 16 bytes, fill with spaces
                str += (i < count) ? String.Format("{0:X2}", buffer[bufferOffset+i]) : "  ";
                str += " ";
            }

            // add space between hex bytes and char; one space was already inserted in above loop
            str += new String(' ', LINE_NUM_SPACES_HEX_BYTES_TO_CHARS - 1);
            
            // ascii bytes
            for (int i = 0; i < 16; i++)
            {
                char ch = (i < count) ? Convert.ToChar(buffer[bufferOffset+i]) : ' ';
                str += Char.IsControl(ch) ? "." : ch.ToString();
            }
            return str;
        }

        /// <summary>
        /// Color all currently visible lines.
        /// </summary>
        private void ColorVisibleLines()
        {
            isOnSelectionChangeHandlingSuppressed = true;
            for (int line = 0; line <= numOfVisibleLines; line++)
            {
                ColorLine(line);
            }
            isOnSelectionChangeHandlingSuppressed = false;
        }

        /// <summary>
        /// Color one line of the richtextbox.
        /// </summary>
        /// <param name="line">line number to color. The line number is based on the visible content, not the whole content</param>
        private void ColorLine(int line)
        {
            int firstCharIndex = this.GetFirstCharIndexFromLine(line);
            this.SelectionStart = firstCharIndex;
            this.SelectionLength = 8;
            this.SelectionColor = addressColor;
            // color even byte pairs
            for (int i = 0; i < 4; i++)
            {
                this.SelectionStart = firstCharIndex + LINE_OFFSET_ADDRESS_WITH_SPACE + (i * 2 * 6);
                this.SelectionLength = 5;
                this.SelectionColor = Color.DarkBlue;
            }
            // color odd byte pairs
            for (int i = 0; i < 4; i++)
            {
                this.SelectionStart = firstCharIndex + LINE_OFFSET_ADDRESS_WITH_SPACE + 6 + (i * 2 * 6);
                this.SelectionLength = 5;
                this.SelectionColor = Color.SteelBlue;
            }
            // color rest black
            this.SelectionStart = firstCharIndex + LINE_OFFSET_CHARS;
            this.SelectionLength = 16;
            this.SelectionColor = Color.Black;
            this.SelectionLength = 0;
        }

        /// <summary>
        /// Get the byte at given line and given column.
        /// The line must be visible.
        /// </summary>
        /// <param name="line">global line number to get the information for</param>
        /// <param name="column">column in given line to get the byte information for</param>
        /// <returns></returns>
        //private byte getByteFromColumn(int line, int column)
        //{
        //    // get the index of the visible line
        //    int internaLine = line - startVisibleLine;
        //    if (internaLine < 0 || internaLine > numOfVisibleLines)
        //    {
        //        throw new ArgumentOutOfRangeException("line parameter is out of range: " + internaLine);
        //    }

        //    byte retByte;
        //    if (isCaretInHexByteRange(column))
        //    {
        //        /* Hex Byte */
        //        // start position in current line string of hex byte
        //        int startPosInStrOfHexByte = column - ((column - LINE_OFFSET_ADDRESS_WITH_SPACE) % 3);
        //        // extract the hex byte as string
        //        string byteStr = this.Lines[internaLine].Substring(startPosInStrOfHexByte, 2);
        //        // convert the hex byte string to a byte
        //        retByte = Byte.Parse(byteStr, System.Globalization.NumberStyles.HexNumber);
        //    }
        //    else
        //    {   // should never happen
        //        throw new InvalidOperationException("Character position is out of range: " + column);
        //    }
        //    return retByte;
        //}

        #endregion

        /*****************************************************
         * Public Methods
         *****************************************************/
        #region Public Methods

        /// <summary>
        /// Scroll the hexeditbox to the given address to make it visible.
        /// </summary>
        /// <param name="address">adress to move caret to</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GoToAddress(int address) 
        {
            if (byteProvider != null)
            {
                if (address > byteProvider.Length)
                {
                    throw new ArgumentOutOfRangeException("Address is too large: " + address);
                }
                this.vScrollBar.Value = Math.Min(address / 16, vScrollBar.Maximum);  // will trigger onValueChanged() event

                // TEST
                offsetChangeEventSuppressed = true;
                /* get absolute line number of our line */
                int absoluteLine = address / 16;
                // get number of line in visible content
                int visLineNumber = absoluteLine - VisibleLineIndexStart;
                /* start position of line of given address */
                int startPosOfCurLine = GetFirstCharIndexFromLine(visLineNumber);
                int columnHexByte = HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE + (address % 16) * 3;
                this.SelectionStart = startPosOfCurLine + columnHexByte;
                //SelectionLength = 2;
                this.Focus();
                offsetChangeEventSuppressed = false;
            }
        }

        /// <summary>
        /// Returns true if given address is currently in visible area.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool IsAddressVisible(int address)
        {
            bool result = false;
            if (byteProvider != null)
            {
                result = ( (address >= (startVisibleLine * 16)) &&
                           (address < ((endVisibleLine + 1) * 16) ) );
            }
            return result;
        }

        /// <summary>
        /// Returns true if given address is an offset inside file.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool isOffsetValid(int address)
        {
            return (address >= 0 && address <= byteProvider.Length);
        }

        #endregion

        #region Marked Region Handling

        MarkedRegionsHandler markedRegionsHandler;

        /// <summary>
        /// Add an address to highlight with given color.
        /// </summary>
        /// <param name="address">address to color</param>
        /// <param name="color">color used for backround highlighting</param>
        public void AddHighlightRegion(int address, int length, Color color)
        {
            if (markedRegionsHandler == null)
            {
                return;
            }

            markedRegionsHandler.AddHighlightRegion(address, length, color);
        }

        /// <summary>
        /// Remove the highlight region at given address
        /// </summary>
        /// <param name="address"></param>
        public void RemoveHighlightRegion(int address)
        {
            if (markedRegionsHandler != null)
            {
                markedRegionsHandler.RemoveHighlightRegion(address);
            }
        }

        /// <summary>
        /// Remove all highlight regions.
        /// </summary>
        public void RemoveAllHighlightRegions()
        {
            if (markedRegionsHandler != null)
            {
                markedRegionsHandler.RemoveAllHighlightRegions();
            }
        }

        #endregion


        /*****************************************************
         * Properties
         *****************************************************/
        #region Properties

        /// <summary>
        /// The Byte Provider interface used to display the hex data from.
        /// </summary>
        public IByteProvider ByteProvider
        {
            get { return byteProvider; }
            set
            {
                if (value == null)
                {
                    // clear all content
                    this.Clear();
                    return;
                }

                byteProvider = value;
                ReloadVisibleBoxContent();
            }
        }

        /// <summary>
        /// Get or set the readonly property of the whole hexeditbox content.
        /// </summary>
        public bool ReadOnlyContent
        {
            get { return isReadOnlyContent; }
            set
            {
                isPaintProcessingSuppressed = true;
                int tmpPos = this.SelectionStart;
                this.SelectionStart = 0;
                this.SelectionLength = this.Text.Length;
                this.SelectionProtected = value;
                this.SelectionLength = 0;
                this.SelectionStart = tmpPos;
                isPaintProcessingSuppressed = false;
                this.isReadOnlyContent = value;
            }
        }

        /// <summary>
        /// Get the total number of lines.
        /// </summary>
        public int MaxLines
        {
            get
            {
                if (byteProvider == null)
                {
                    return 0;
                }
                else
                {
                    return (int)Math.Ceiling(byteProvider.Length / 16.0);
                }
            }
        }

        /// <summary>
        /// Get the index of the first visible line.
        /// </summary>
        internal int VisibleLineIndexStart
        {
            get { return startVisibleLine; }
        }

        /// <summary>
        /// Get the index of the last visible line.
        /// </summary>
        internal int VisibleLineIndexEnd
        {
            get { return endVisibleLine; }
        }

        /// <summary>
        /// Get the index of the last visible line.
        /// </summary>
        internal int VisibleLineCount
        {
            get { return numOfVisibleLines; }
        }

        /// <summary>
        /// Enables or disables the handling of OnSelectionChanged events.
        /// </summary>
        internal bool OnSelectionChangedDisabled
        {
            get { return isOnSelectionChangeHandlingSuppressed; }
            set { isOnSelectionChangeHandlingSuppressed = value; }
        }

        #endregion

        /*****************************************************
         * Scrollbar Handling
         *****************************************************/
        #region Scrollbar Handling

        /// <summary>
        /// Scrollbar object because the scrollbar of the richeditbox is not used.
        /// </summary>
        private VScrollBar vScrollBar;

        /// <summary>
        /// Create the vertical scrollvar and add it to the richtextbox.
        /// The standard scrollbar cannot be used because only parts of the whole content is loaded at a time.
        /// </summary>
        private void CreateVerticalScrollbar()
        {
            vScrollBar = new VScrollBar();
            vScrollBar.Dock = DockStyle.Right;
            vScrollBar.Minimum = 0;
            vScrollBar.Maximum = 1;
            vScrollBar.Value = 0;
            vScrollBar.SmallChange = 1;
            vScrollBar.LargeChange = 50;
            vScrollBar.Cursor = Cursors.Arrow;
            //vScrollBar.Scroll += new ScrollEventHandler(vScrollBar_Scroll);
            vScrollBar.ValueChanged += new EventHandler(vScrollBar_ValueChanged);
            this.Controls.Add(vScrollBar);
        }

        /// <summary>
        /// Called when the scrollbar position changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            if (byteProvider != null)
            {
                ReloadVisibleBoxContent();
            }
        }

        /// <summary>
        /// Function called when scrollbar position changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (byteProvider != null)
            {
                ReloadVisibleBoxContent();
            }
        }

        /// <summary>
        /// Calculate the maximum scrollbar position value.
        /// </summary>
        private void CalculateScrollbarMaximum()
        {
            this.vScrollBar.LargeChange = 1;    // HACK - do not know how to correctly fix it
            this.vScrollBar.Maximum = MaxLines + this.vScrollBar.LargeChange - numOfVisibleLines - 1;
            if (this.vScrollBar.Maximum >= this.vScrollBar.LargeChange)
            {
                this.vScrollBar.LargeChange = 1;
            }
        }
        #endregion

        /*****************************************************
         * ContextMenu
         *****************************************************/
        #region ContextMenu Handling

        [Description("Enables or disables the context menu.")]
        public bool ContextMenuActive
        {
            get { return (contextMenuHandler != null && contextMenuHandler.Active); }
            set
            {
                if (value)
                {   /* user want to set context menu */
                    if (contextMenuHandler == null)
                    {
                        contextMenuHandler = new ContextMenuHandler(this);
                    }
                    this.ContextMenuStrip = contextMenuHandler.ContextMenu;
                }
                if (contextMenuHandler != null)
                {
                    contextMenuHandler.Active = value;
                }
            }
        }

        #endregion

        /*****************************************************
         * HexEditBox Events
         *****************************************************/
        #region SelectedOffsetChangedEvent Handling

        /// <summary>
        /// Function type that can be registered and used when offset has changed.
        /// </summary>
        /// <param name="sender">hexeditbox which triggered the event that the selected offset has changed</param>
        /// <param name="e">event arguments with the offset and byte values</param>
        public delegate void SelectedOffsetChangedDelegate(object sender, OffsetSelectedChangeEvents e);
        /// <summary>
        /// Event for changed offset selection.
        /// </summary>
        public event SelectedOffsetChangedDelegate SelectedOffsetChanged;
        /// <summary>
        /// internal flag to temporary disable the activation of the selected offset changed event.
        /// This is required because of the internal handling of overwriting bytes and coloring lines,
        /// the selection changes several times by code - those changes must not be reported.
        /// </summary>
        private bool offsetChangeEventSuppressed;

        /// <summary>
        /// Helper struct to hold the positions of a hex and a character byte that belong together.
        /// </summary>
        public struct ByteInfo
        {
            public byte hexbyte;
            public char asciibyte;
        }

        /// <summary>
        /// Event argument class for the selected offet event.
        /// </summary>
        public class OffsetSelectedChangeEvents : EventArgs
        {
            private int offset;
            private ByteInfo byteInfoStruct;

            public OffsetSelectedChangeEvents(int offset, ByteInfo byteInfoStruct)
            {
                this.offset = offset;
                this.byteInfoStruct = byteInfoStruct;
            }

            public int Offset
            {
                get { return offset; }
            }

            public ByteInfo ByteInfo
            {
                get { return byteInfoStruct; }
            }
        }

        /// <summary>
        /// Internal handling of the event when the selection has changed.
        /// </summary>
        private void processOffsetChangedEvents()
        {
            if (SelectedOffsetChanged != null && !offsetChangeEventSuppressed)
            {
                /* calculate offset */
                // base address offset of line
                int curLineTotal = getAbsoluteCurrentLine();
                int baseOffset = curLineTotal * 16;
                int columnInLine = getColumnInCurrentLine();
                if (isCaretInHexByteRange(columnInLine))
                {
                    /* caret is in range of hex bytes */
                    int address = baseOffset + (columnInLine - LINE_OFFSET_ADDRESS_WITH_SPACE) / 3;

                    ByteInfo byteinfo = getByteInfoFromColumn(curLineTotal, columnInLine);
                    OffsetSelectedChangeEvents args = new OffsetSelectedChangeEvents(address, byteinfo);
                    SelectedOffsetChanged(this, args);
                }
                else if (isCaretInAsciiRangeWithoutLineEnd(columnInLine))
                {
                    /* caret is in range of ascii bytes */
                    int address = baseOffset + (columnInLine - LINE_OFFSET_CHARS);
                    
                    ByteInfo byteinfo = getByteInfoFromColumn(curLineTotal, columnInLine);
                    OffsetSelectedChangeEvents args = new OffsetSelectedChangeEvents(address, byteinfo);
                    SelectedOffsetChanged(this, args);
                }

            }
        }

        /// <summary>
        /// Get the information about byte at given line and given column.
        /// The line must be visible.
        /// </summary>
        /// <param name="line">line number to get the information for</param>
        /// <param name="column">column in given line to get the byte information for</param>
        /// <returns></returns>
        private ByteInfo getByteInfoFromColumn(int line, int column)
        {
            // get the index of the visible line
            int internaLine = line - startVisibleLine;
            if (internaLine < 0 || internaLine > numOfVisibleLines)
            {
                throw new ArgumentOutOfRangeException("line parameter is out of range: " + internaLine);
            }

            ByteInfo byteInfo = new ByteInfo();
            if (isCaretInHexByteRange(column))
            {
                /* Hex Byte */
                // start position in current line string of hex byte
                int startPosInStrOfHexByte = column - ((column - LINE_OFFSET_ADDRESS_WITH_SPACE) % 3);
                // extract the hex byte as string
                string byteStr = this.Lines[internaLine].Substring(startPosInStrOfHexByte, 2);
                // convert the hex byte string to a byte
                byteInfo.hexbyte = Byte.Parse(byteStr, System.Globalization.NumberStyles.HexNumber);

                /* Ascii Byte */
                int asciiPos = getAsciiPosFromHexBytePos(startPosInStrOfHexByte);
                string asciiBytestr = this.Lines[internaLine].Substring(asciiPos, 1);
                byteInfo.asciibyte = asciiBytestr[0];
            }
            else if (isCaretInAsciiRangeWithoutLineEnd(column))
            {
                /* Ascii Byte */
                int startPosInStrOfAsciiByte = column;
                string asciiBytestr = this.Lines[internaLine].Substring(startPosInStrOfAsciiByte, 1);
                byteInfo.asciibyte = asciiBytestr[0];

                /* Hex Byte */
                int startPosInStrOfHexByte = getHexBytePosFromAsciiBytePos(startPosInStrOfAsciiByte);
                string byteStr = this.Lines[internaLine].Substring(startPosInStrOfHexByte, 2);
                // convert the hex byte string to a byte
                byteInfo.hexbyte = Byte.Parse(byteStr, System.Globalization.NumberStyles.HexNumber);
            }
            else
            {   // should never happen
                throw new InvalidOperationException("Character position is out of range: " + column);
            }
            return byteInfo;
        }

        /// <summary>
        /// Get the position in line of the ascii character that belongs to the given position of a hex byte.
        /// </summary>
        /// <param name="hexPos">position of hex byte - must be in valid range, no verification is done</param>
        /// <returns>position of ascii character in current line</returns>
        internal int getAsciiPosFromHexBytePos(int hexPos)
        {
            return ( (hexPos - LINE_OFFSET_ADDRESS_WITH_SPACE)/ 3) + LINE_OFFSET_CHARS;
        }

        /// <summary>
        /// Get the position in line of the hex byte that belongs to the given position of a character.
        /// </summary>
        /// <param name="asciiPos"></param>
        /// <returns></returns>
        internal int getHexBytePosFromAsciiBytePos(int asciiPos)
        {
            return (asciiPos - LINE_OFFSET_CHARS) * 3 + LINE_OFFSET_ADDRESS_WITH_SPACE;
        }

        #endregion
    }
}
