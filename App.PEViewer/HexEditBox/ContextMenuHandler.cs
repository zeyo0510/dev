using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HexEditBox
{
    class ContextMenuHandler
    {
        private HexEditBox hexBox;

        private bool active;

        ContextMenuStrip contextMenuStrip;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentBox"></param>
        public ContextMenuHandler(HexEditBox parentBox)
        {
            this.hexBox = parentBox;
            createContextMenu();
        }

        /// <summary>
        /// Build up the context menu and register event handlers.
        /// </summary>
        private void createContextMenu()
        {
            contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem copyHexBytesItem = new ToolStripMenuItem("Hex Bytes");
            copyHexBytesItem.Click += new EventHandler(copyHexBytesItem_Click);
            ToolStripMenuItem copyAsciiBytesItem = new ToolStripMenuItem("Ascii Bytes");
            copyAsciiBytesItem.Click += new EventHandler(copyAsciiBytesItem_Click);
            ToolStripMenuItem copyDisplayedItem = new ToolStripMenuItem("Displayed Bytes");
            copyDisplayedItem.Click += new EventHandler(copyDisplayedItem_Click);

            ToolStripMenuItem copyClipboardItem = new ToolStripMenuItem("Copy to clipboard");
            copyClipboardItem.DropDownItems.Add(copyHexBytesItem);
            copyClipboardItem.DropDownItems.Add(copyAsciiBytesItem);
            copyClipboardItem.DropDownItems.Add(copyDisplayedItem);

            contextMenuStrip.Items.Add(copyClipboardItem);
            hexBox.ContextMenuStrip = contextMenuStrip;
        }

        /// <summary>
        /// Called when "Copy as displayed" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copyDisplayedItem_Click(object sender, EventArgs e)
        {
            int absStartLine = hexBox.GetLineFromCharIndex(hexBox.SelectionStart);
            
            int startColumnInLine = hexBox.getColumnInCurrentLine();
            int startLineAddress = (absStartLine + hexBox.VisibleLineIndexStart) * 16;

            int startSelAddress;
            if (startColumnInLine >= HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE)
            {   /* in ascii range */
                startSelAddress = startLineAddress + startColumnInLine - HexEditBox.LINE_OFFSET_CHARS;
            }
            else
            {   /* in hex byte range */
                startSelAddress = startLineAddress + (startColumnInLine - HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE) / 3;
            }
            
            int absEndLine = hexBox.GetLineFromCharIndex(hexBox.SelectionStart + hexBox.SelectionLength - 1);
            int endColumnInLine = hexBox.SelectionStart + hexBox.SelectionLength - 1 - hexBox.GetFirstCharIndexFromLine(absEndLine);
            int endLineAddress = (absEndLine + hexBox.VisibleLineIndexStart) * 16;
            int endSelAddress;
            if (endColumnInLine >= HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE)
            {   /* in ascii range */
                if (endColumnInLine == HexEditBox.LINE_OFFSET_CHARS)
                {
                    endSelAddress = endLineAddress;
                }
                else if (endColumnInLine < HexEditBox.LINE_OFFSET_CHARS + 16)
                {
                    endSelAddress = endLineAddress + (endColumnInLine - HexEditBox.LINE_OFFSET_CHARS);
                }
                else if (endColumnInLine == HexEditBox.LINE_OFFSET_CHARS + 16)
                {
                    endSelAddress = endLineAddress + (endColumnInLine - HexEditBox.LINE_OFFSET_CHARS) - 1;
                }
                else
                {
                    endSelAddress = endLineAddress + (endColumnInLine - HexEditBox.LINE_OFFSET_CHARS) - 2;
                }
            }
            else
            {   /* in hex byte range */
                endSelAddress = endLineAddress + (endColumnInLine - HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE) / 3;
            }
            
            byte[] selBytes = GetSelectedHexBytes();
            if (selBytes == null) return;
            int selByteIndex = 0;

            // build the header line with all offsets
            StringBuilder sb = new StringBuilder();
            sb.Append("         ");
            for (int i = 0; i < 16; i++)
            {
                sb.Append(String.Format("{0:X2} ", i));
            }
            sb.Append(Environment.NewLine);
            sb.Append(String.Format("{0:X8} ", startLineAddress));

            // fill spaces till the start byte
            int globalPosIndex = 0;
            for (int address = startLineAddress; address < startSelAddress; address++)
            {
                sb.Append("   ");
                globalPosIndex++;
            }

            int oldIndex = selByteIndex;
            int bytesInCurLine;
            for (int address = startSelAddress; address <= endSelAddress; address++)
            {
                if (globalPosIndex == 16)
                {
                    // fill in ascii bytes
                    sb.Append(" ");
                    bytesInCurLine = Math.Min(16, address - startSelAddress);
                    // if first line fill stuff bytes
                    if (address - startSelAddress <= 16)
                    {
                        for (int i = 0; i < 16 - (address - startSelAddress); i++)
                        {
                            sb.Append(" ");
                        }
                    }
                    for (int asciiAddress = Math.Max(startSelAddress, address-16); 
                             asciiAddress < Math.Max(startSelAddress, address-16) + bytesInCurLine; asciiAddress++)
                    {
                        sb.Append(GetAsciiChar(selBytes[oldIndex++]));
                    }
                    oldIndex = selByteIndex;
                    bytesInCurLine = Math.Min(16, endSelAddress - address);

                    globalPosIndex = 0;
                    sb.Append(Environment.NewLine);
                    sb.Append(String.Format("{0:X8} ", address));
                }

                sb.Append(String.Format("{0:X2} ", selBytes[selByteIndex++]));

                globalPosIndex++;
            }
            // stuff hex bytes of last line
            sb.Append(' ', 3 * (endLineAddress + 16 - endSelAddress - 1));
            sb.Append(" ");
            // insert all ascii bytes in last line
            for (int address = Math.Max(endLineAddress, startSelAddress); address < Math.Min(endLineAddress + 16, endSelAddress); address++)
            {
                sb.Append(GetAsciiChar(selBytes[oldIndex++]));
            }
            //Console.Out.WriteLine(sb.ToString());
            Clipboard.SetText(sb.ToString());
        }

        /// <summary>
        /// Get character to display of given byte.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private char GetAsciiChar(byte b)
        {
            char c = Convert.ToChar(b); 
            return (Char.IsControl(c) ? '.' : c);
        }

        /// <summary>
        /// Called when "Copy Ascii bytes" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copyAsciiBytesItem_Click(object sender, EventArgs e)
        {
            byte[] selBytes = GetSelectedHexBytes();
            if (selBytes != null)
            {
                StringBuilder sb = new StringBuilder();
                int count = 0;
                foreach (byte b in selBytes)
                {
                    sb.Append(Convert.ToChar(b));
                    if (count++ == 16)
                    {
                        sb.Append(Environment.NewLine);
                        count = 0;
                    }
                }
                Clipboard.SetText(sb.ToString());
            }
        }

        /// <summary>
        /// Called when "Copy Hex bytes" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copyHexBytesItem_Click(object sender, EventArgs e)
        {
            byte[] selBytes = GetSelectedHexBytes();
            if (selBytes != null)
            {
                StringBuilder sb = new StringBuilder();
                int count = 0;
                foreach (byte b in selBytes)
                {
                    sb.Append(String.Format("{0:X2} ", b));
                    if (count++ == 16)
                    {
                        sb.Append(Environment.NewLine);
                        count = 0;
                    }
                }
                Clipboard.SetText(sb.ToString());
            }
        }


        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public ContextMenuStrip ContextMenu
        {
            get { return contextMenuStrip; }
        }

        /// <summary>
        /// Returns the number of bytes between the global start position and length.
        /// </summary>
        /// <param name="globalStartCol">global start column</param>
        /// <param name="globalSelLength">global selection length</param>
        /// <returns></returns>
        private int GetNumOfSelectedBytes(int globalStartCol, int globalSelLength)
        {
            int absStartLine = hexBox.GetLineFromCharIndex(globalStartCol);
            int absEndLine = hexBox.GetLineFromCharIndex(globalStartCol + globalSelLength - 1);

            int numOfBytes = 0;
            // handle first line
            if (absStartLine == absEndLine)
            {
                int startColumn = hexBox.getColumnInCurrentLine();
                if (startColumn >= HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE)
                {   /* in ascii range */
                    int endColumn = Math.Min(HexEditBox.LINE_OFFSET_CHARS + 16, startColumn + hexBox.SelectionLength);
                    numOfBytes = endColumn - startColumn;
                }
                else
                {   /* in hex range */
                    int endColumn = Math.Min(HexEditBox.LINE_OFFSET_CHARS - 1, startColumn + hexBox.SelectionLength);
                    numOfBytes = (int)Math.Ceiling((endColumn - startColumn) / 3.0);
                }
            }
            else
            {
                int startColumn = hexBox.getColumnInCurrentLine();
                if (startColumn >= HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE)
                {   /* in ascii range */
                    numOfBytes = HexEditBox.LINE_OFFSET_CHARS + 16 - hexBox.getColumnInCurrentLine();
                }
                else
                {   /* in hex byte range */
                    numOfBytes = (HexEditBox.LINE_OFFSET_CHARS - startColumn) / 3;
                }
            }

            // handle all fully selected lines
            if (absEndLine > absStartLine + 1)
            {
                numOfBytes += (((absEndLine - (absStartLine + 1)) * 16));
            }

            // handle last line
            if (absEndLine > absStartLine)
            {
                int startColumn, endColumn;
                int caretEndPos = hexBox.SelectionStart + hexBox.SelectionLength - hexBox.GetFirstCharIndexFromLine(absEndLine);
                if (caretEndPos >= HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE)
                {   /* in ascii range */
                    int offsetInLine = hexBox.SelectionStart + hexBox.SelectionLength - hexBox.GetFirstCharIndexFromLine(absEndLine);
                    startColumn = HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE;
                    endColumn = Math.Min(HexEditBox.LINE_OFFSET_CHARS - 1,
                        (offsetInLine - HexEditBox.LINE_OFFSET_CHARS) * 3 + HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE);
                }
                else
                {   /* in hex byte range */
                    startColumn = HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE;
                    endColumn = Math.Min(HexEditBox.LINE_OFFSET_CHARS - 1,
                        hexBox.SelectionStart + hexBox.SelectionLength - hexBox.GetFirstCharIndexFromLine(absEndLine));
                    if ((endColumn - startColumn) % 3 == 2) // selection ends at end of hex byte, division below will cut one byte
                        endColumn++;
                }
                numOfBytes += ((endColumn - startColumn) / 3);
            }

            return numOfBytes;
        }

        /// <summary>
        /// Get the currently selected bytes as byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] GetSelectedHexBytes()
        {
            if (hexBox.SelectionLength == 0 || hexBox.ByteProvider == null)
            {
                return null;
            }

            /* backup state */
            int startOld = hexBox.SelectionStart;
            int selLengthOld = hexBox.SelectionLength;

            /* allocate buffer */
            byte[] retBuffer = new byte[GetNumOfSelectedBytes(startOld, selLengthOld)];
            int bufIndex = 0;

            int absStartLine = hexBox.GetLineFromCharIndex(hexBox.SelectionStart);
            int absEndLine = hexBox.GetLineFromCharIndex(hexBox.SelectionStart + hexBox.SelectionLength);
            int startColumn;
            int endColumn;

            // handle first line
            if (absStartLine == absEndLine)
            {
                startColumn = hexBox.getColumnInCurrentLine();
                if (startColumn >= HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE)
                {   /* in ascii range */
                    endColumn = Math.Min(HexEditBox.LINE_OFFSET_CHARS + 16, startColumn + hexBox.SelectionLength);
                }
                else
                {   /* in hex byte range */
                    endColumn = Math.Min(HexEditBox.LINE_OFFSET_CHARS - 1, startColumn + hexBox.SelectionLength);
                }
            }
            else
            {   /* more lines follow, seek until end of line */
                startColumn = hexBox.getColumnInCurrentLine();
                if (startColumn >= HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE)
                {   /* in ascii range */
                    endColumn = HexEditBox.LINE_OFFSET_CHARS + 16;
                }
                else
                {   /* in hex byte range */
                    endColumn = HexEditBox.LINE_OFFSET_CHARS - 1;
                }
            }
            // read all byted from first line
            for (int curColumn = startColumn; curColumn < endColumn; curColumn += 1)
            {
                if (curColumn != HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE &&
                   (hexBox.isCaretAtByteStartPosition(curColumn) || hexBox.isCaretInAsciiRange(curColumn)) )
                {
                    retBuffer[bufIndex++] = getHexByteFromColumn(hexBox.VisibleLineIndexStart + absStartLine, curColumn);
                }
            }

            // handle all full lines
            if (absEndLine > absStartLine + 1)
            {
                for (int visLine = absStartLine + 1; visLine < absEndLine; visLine++)
                {
                    startColumn = HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE;
                    endColumn = HexEditBox.LINE_OFFSET_CHARS;
                    for (int curColumn = startColumn; curColumn < endColumn; curColumn += 3)
                    {
                        if (hexBox.isCaretInHexByteRange(curColumn) && curColumn != HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE)
                        {
                            retBuffer[bufIndex++] = getHexByteFromColumn(hexBox.VisibleLineIndexStart + visLine, curColumn);
                        }
                    }
                }
            }

            // handle last line
            if (absEndLine > absStartLine)
            {
                int caretEndPos = hexBox.SelectionStart + hexBox.SelectionLength - hexBox.GetFirstCharIndexFromLine(absEndLine);
                if (caretEndPos >= HexEditBox.LINE_OFFSET_HEX_BYTES_WITHOUT_SPACE)
                {   /* in ascii range */
                    int offsetInLine = hexBox.SelectionStart + hexBox.SelectionLength - hexBox.GetFirstCharIndexFromLine(absEndLine);
                    startColumn = HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE;
                    endColumn = Math.Min(HexEditBox.LINE_OFFSET_CHARS - 1,
                        (offsetInLine - HexEditBox.LINE_OFFSET_CHARS) * 3 + HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE);
                }
                else
                {   /* in hex byte range */
                    startColumn = HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE;
                    endColumn = Math.Min(HexEditBox.LINE_OFFSET_CHARS - 1,
                        hexBox.SelectionStart + hexBox.SelectionLength - hexBox.GetFirstCharIndexFromLine(absEndLine));
                }
                for (int curColumn = startColumn; curColumn < endColumn; curColumn += 3)
                {
                    if (hexBox.isCaretInHexByteRange(curColumn))
                    {
                        retBuffer[bufIndex++] = getHexByteFromColumn(hexBox.VisibleLineIndexStart + absEndLine, curColumn);
                    }
                }
            }

            /* restore state */
            hexBox.SelectionStart = startOld;
            hexBox.SelectionLength = selLengthOld;

            return retBuffer;
        }

        /// <summary>
        /// Get the byte at given line and given column.
        /// The line must be visible.
        /// </summary>
        /// <param name="line">global line number to get the information for</param>
        /// <param name="column">column in given line to get the byte information for</param>
        /// <returns></returns>
        private byte getHexByteFromColumn(int line, int column)
        {
            // get the index of the visible line
            int internaLine = line - hexBox.VisibleLineIndexStart;
            if (internaLine < 0 || internaLine > hexBox.VisibleLineCount)
            {
                throw new ArgumentOutOfRangeException("line parameter is out of range: " + internaLine);
            }

            byte retByte;
            if (hexBox.isCaretInHexByteRange(column))
            {
                /* Hex Byte */
                // start position in current line string of hex byte
                int startPosInStrOfHexByte = column - ((column - HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE) % 3);
                // extract the hex byte as string
                string byteStr = hexBox.Lines[internaLine].Substring(startPosInStrOfHexByte, 2);
                // convert the hex byte string to a byte
                retByte = Byte.Parse(byteStr, System.Globalization.NumberStyles.HexNumber);
            }
            else if (hexBox.isCaretInAsciiRange(column))
            {
                int startPosInStrOfHexByte = hexBox.getHexBytePosFromAsciiBytePos(column);
                string byteStr = hexBox.Lines[internaLine].Substring(startPosInStrOfHexByte, 2);
                // convert the hex byte string to a byte
                retByte = Byte.Parse(byteStr, System.Globalization.NumberStyles.HexNumber);
            }
            else
            {   // should never happen
                throw new InvalidOperationException("Character position is out of range: " + column);
            }
            return retByte;
        }
    }
}
