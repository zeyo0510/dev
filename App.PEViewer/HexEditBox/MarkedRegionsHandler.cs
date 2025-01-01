using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HexEditBox
{
    class MarkedRegionsHandler
    {
        /// <summary>
        /// Hold information of one colored region
        /// </summary>
        struct MarkedRegion
        {
            /// <summary>
            /// the address / offset in the file where the region starts
            /// </summary>
            public int address;
            public int length;
            /// <summary>
            /// absolute line number in which the region begins
            /// </summary>
            public int lineAbsolute;
            /// <summary>
            /// column in line where the hex byte of the given address starts
            /// </summary>
            public int columnHexByte;
            /// <summary>
            /// column in line where the ascii byte of the given address starts
            /// </summary>
            public int columnAsciiByte;
            /// <summary>
            /// color used to highlight the region
            /// </summary>
            public Color backColor;
        }

        /// <summary>
        /// list that holds all highlight regions
        /// </summary>
        List<MarkedRegion> markedRegions;

        HexEditBox parentBox;

        public MarkedRegionsHandler(HexEditBox parentBox)
        {
            this.parentBox = parentBox;
            markedRegions = new List<MarkedRegion>();
        }

        /// <summary>
        /// Add an address to highlight with given color.
        /// </summary>
        /// <param name="address">address to color</param>
        /// <param name="color">color used for backround highlighting</param>
        public void AddHighlightRegion(int address, int length, Color color)
        {
            /* add the region to the list */
            MarkedRegion region;
            region.address = address;
            region.lineAbsolute = address / 16;
            region.columnHexByte = HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE + (address % 16) * 3;
            region.columnAsciiByte = HexEditBox.LINE_OFFSET_CHARS + (address % 16);
            region.backColor = color;
            region.length = length;
            markedRegions.Add(region);
            /* color this newly added region, no need to color whole content */
            ColorRegions();
        }

        /// <summary>
        /// This method does the coloring of all regions.
        /// </summary>
        internal void ColorRegions()
        {
            if (markedRegions == null)
            {
                return;
            }

            parentBox.OnSelectionChangedDisabled = true;

            /* backup state */
            int startOld = parentBox.SelectionStart;
            int selLengthOld = parentBox.SelectionLength;

            foreach (MarkedRegion reg in markedRegions)
            {
                // first check if region is currently in visible range
                if (reg.lineAbsolute >= parentBox.VisibleLineIndexStart && 
                    reg.lineAbsolute + (reg.length / 16) <= parentBox.VisibleLineIndexEnd)
                {
                    // region is completely visible, that's the easy case
                    ColorRegion(reg);
                }
                else
                {
                    //TODO

                    //int regionStartLine = reg.lineAbsolute;
                    //int regionEndLine = regionStartLine + (reg.length / 16);

                    //int visStartLine = Math.Max(regionStartLine, parentBox.VisibleLineIndexStart);
                    //int visEndLine = Math.Min(regionEndLine - parentBox.VisibleLineIndexStart,
                    //                            parentBox.VisibleLineIndexEnd);

                    
                    //ColorRegion(reg, visStartLine, visEndLine);                
                }
            }

            /* restore state */
            parentBox.SelectionStart = startOld;
            parentBox.SelectionLength = selLengthOld;

            parentBox.OnSelectionChangedDisabled = false;
        }

        private void ColorRegion(MarkedRegion region, int visStartLine, int visEndLine)
        {
            /* start position of line of given address */
            int startPosOfCurLine = parentBox.GetFirstCharIndexFromLine(visStartLine);

            // total column number of bytes
            int curColumnHexByte = startPosOfCurLine + region.columnHexByte;
            int curColumnAsciiByte = startPosOfCurLine + region.columnAsciiByte;

            int curVisibleLine = visStartLine;
            for (int byteIndex = 0; byteIndex < region.length; byteIndex++)
            {

                // check if we reached end of line. In this case move to next line.
                if (((region.address + byteIndex) % 16 == 0) && (byteIndex != 0))
                {
                    // reached end of line, check if there are more visible lines
                    if (curVisibleLine + 1 > visEndLine)
                    {
                        // end of visible content reached, abort coloring
                        break;
                    }

                    // move to next line
                    curVisibleLine++;
                    startPosOfCurLine = parentBox.GetFirstCharIndexFromLine(curVisibleLine);
                    curColumnHexByte = startPosOfCurLine + HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE;
                    curColumnAsciiByte = startPosOfCurLine + HexEditBox.LINE_OFFSET_CHARS;
                }

                // color hex byte region
                parentBox.SelectionStart = curColumnHexByte;
                parentBox.SelectionLength = 2;
                parentBox.SelectionBackColor = region.backColor;
                // color ascii byte region
                parentBox.SelectionStart = curColumnAsciiByte;
                parentBox.SelectionLength = 1;
                parentBox.SelectionBackColor = region.backColor;
                //move to next byte
                curColumnHexByte += 3;
                curColumnAsciiByte++;
            }
        }

        private void ColorRegion(MarkedRegion region)
        {
            // get number of line in visible content
            int visLineNumber = region.lineAbsolute - parentBox.VisibleLineIndexStart;
            /* start position of line of given address */
            int startPosOfCurLine = parentBox.GetFirstCharIndexFromLine(visLineNumber);

            // total column number of bytes
            int curColumnHexByte = startPosOfCurLine + region.columnHexByte;
            int curColumnAsciiByte = startPosOfCurLine + region.columnAsciiByte;

            for (int byteIndex = 0; byteIndex < region.length; byteIndex++)
            {

                // check if we reached end of line. In this case move to next line.
                if (((region.address + byteIndex) % 16 == 0) && (byteIndex != 0) )
                {
                    // reached end of line, check if there are more visible lines
                    if (visLineNumber + 1 > parentBox.VisibleLineIndexEnd)
                    {
                        // end of visible content reached, abort coloring
                        break;
                    }

                    // move to next line
                    visLineNumber++;
                    startPosOfCurLine = parentBox.GetFirstCharIndexFromLine(visLineNumber);
                    curColumnHexByte = startPosOfCurLine + HexEditBox.LINE_OFFSET_ADDRESS_WITH_SPACE;
                    curColumnAsciiByte = startPosOfCurLine + HexEditBox.LINE_OFFSET_CHARS;
                }

                // color hex byte region
                parentBox.SelectionStart = curColumnHexByte;
                parentBox.SelectionLength = 2;
                parentBox.SelectionBackColor = region.backColor;
                // color ascii byte region
                parentBox.SelectionStart = curColumnAsciiByte;
                parentBox.SelectionLength = 1;
                parentBox.SelectionBackColor = region.backColor;
                //move to next byte
                curColumnHexByte += 3;
                curColumnAsciiByte++;
            }
        }

        /// <summary>
        /// Remove the highlight region at given address
        /// </summary>
        /// <param name="address"></param>
        public void RemoveHighlightRegion(int address)
        {
            if (markedRegions != null)
            {
                foreach (MarkedRegion reg in markedRegions)
                {
                    if (reg.address == address)
                    {
                        /* region found, remove it from list and update display */
                        markedRegions.Remove(reg);
                        parentBox.ReloadVisibleBoxContent();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Remove all highlight regions.
        /// </summary>
        public void RemoveAllHighlightRegions()
        {
            markedRegions.Clear();
            parentBox.ReloadVisibleBoxContent();
        }
    }
}
