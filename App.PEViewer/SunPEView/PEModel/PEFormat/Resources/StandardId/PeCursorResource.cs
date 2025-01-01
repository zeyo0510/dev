using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    /*
     * Class represents one cursor resource
     * 
     * http://msdn.microsoft.com/en-us/library/windows/desktop/ms648007%28v=vs.85%29.aspx
     */
    class PeCursorResource : IPeStandardResource
    {
        private byte[] resourceRawData;

        /// <summary>
        /// handle to the cursor 
        /// </summary>
        private Cursor cursor;

        /// <summary>
        /// Width and height of the cursor, read from the cursor group
        /// </summary>
        private int width, height;

        /// <summary>
        /// hotspot of cursor
        /// </summary>
        private ushort hotspotX, hotspotY;

        /// <summary>
        /// ID of cursor resource
        /// </summary>
        private uint cursorID;

        /// <summary>
        /// reference to PE file
        /// </summary>
        private PEFile peFile;

        /// <summary>
        /// reference to parent resource data object
        /// </summary>
        private PeResourceDataEntry peResourceDataEntry;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        /// <param name="peResourceDataEntry"></param>
        /// <param name="cursorID"></param>
        public PeCursorResource(PEFile peFile, PeResourceDataEntry peResourceDataEntry, uint cursorID)
        {
            this.peFile = peFile;
            this.peResourceDataEntry = peResourceDataEntry;
            this.cursorID = cursorID;
        }

        /// <summary>
        /// Read the actual cursor data from file.
        /// </summary>
        /// <param name="fDataReader"></param>
        /// <param name="fileOffset"></param>
        /// <param name="length"></param>
        public void Read(IFileAccess fDataReader, long fileOffset, long length)
        {
            fDataReader.Position = (uint)fileOffset;

            PeCursorGroupDirEntry cursorDir = GetResCursorGroup();

            /* read hotspot position */
            hotspotX = fDataReader.ReadUInt16(fDataReader.Position);
            hotspotY = fDataReader.ReadUInt16(fDataReader.Position + 2);
            if (hotspotX == 0) hotspotX = 1;
            if (hotspotY == 0) hotspotY = 1;
            
            /* read bmp header */
            uint dibHeaderSize = fDataReader.ReadUInt32(fDataReader.Position + 4);
            resourceRawData = fDataReader.ReadBytes((uint)fDataReader.Position + 4, (uint)length-4);
            if (dibHeaderSize == 0x28)
            {
                if (cursorDir != null)
                {
                    // get the correct width and height values from cursor dir
                    width = cursorDir.Width.Value>>1;
                    height = cursorDir.Height.Value>>1;
                }
                else
                {
                    // use width and height of cursor resource
                    PELogger.Instance.Log("Could not get cursor group directory entry for cursor", PELogger.LoggingLevel.WARNING);
                    width = 0x20;// (int)fDataReader.ReadUInt32(fDataReader.Position + 4);
                    height = 0x20;// (int)fDataReader.ReadUInt32(fDataReader.Position + 8);
                }

                loadCursor();
            }
            else
            {
                cursor = null;
            }
        }

        /// <summary>
        /// Get parent PE resource data entry.
        /// </summary>
        public PeResourceDataEntry getParentResourceDataEntry
        {
            get { return peResourceDataEntry; }
        }

        /// <summary>
        /// Get actual cursor value, created from loaded resource data
        /// </summary>
        /// <returns></returns>
        public Cursor getCursor()
        {
            return cursor;
        }

        /// <summary>
        /// Get width of cursor in pixels
        /// </summary>
        public int CursorWidth { get { return width; } }
        /// <summary>
        /// Get height of cursor in pixels
        /// </summary>
        public int CursorHeight { get { return height; } }
        /// <summary>
        /// Get ID of cursor
        /// </summary>
        public uint CursorId { get { return cursorID; } }

        /// <summary>
        /// Load cursor from memory (not applicable for PNG)
        /// </summary>
        private void loadCursor()
        {
            // build byte array with created header and cursor data
            byte[] cursorData = getCursorFileBuffer();

            Stream stream = new MemoryStream(cursorData);
            cursor = new Cursor(stream);
        }

        /// <summary>
        /// Build the cursor header and append actual cursor data to build complete cursor file in memory.
        /// </summary>
        /// <returns></returns>
        public byte[] getCursorFileBuffer()
        {
            /* http://www.daubnet.com/en/file-format-cur
             * Offset   Length  Meaning
             * Reserved 	2 byte 	=0
             * Type 	    2 byte 	=2
             * Count 	    2 byte 	Number of Cursors in this file
             * Entries 	    Count * 24 	List of cursors
 	 	     * Width 	    1 byte 	Cursor Width (most commonly =32)
 	 	     * Height 	    1 byte 	Cursor Height (most commonly =32)
 	 	     * ColorCount 	1 byte 	=0 !
 	 	     * Reserved 	1 byte 	=0
 	 	     * XHotspot 	2 byte 	Hotspot's X-Position
 	 	     * YHotspot 	2 byte 	Hotspot's Y-Position
 	 	     * SizeInBytes 	4 byte 	Size of (InfoHeader + ANDBitmap + XORBitmap)
 	 	     * FileOffset 	4 byte 	FilePos, where InfoHeader starts
 	           repeated Count times
             * InfoHeader 	40 byte 	Variant of BMP InfoHeader
 	         * Size 	    4 bytes 	Size of InfoHeader structure = 40
 	         * Width 	    4 bytes 	Cursor Width
 	         * Height 	    4 bytes 	Cursor Height (added height of XORbitmap and ANDbitmap)
 	         * Planes 	    2 bytes 	number of planes = 1
 	         * BitCount 	2 bytes 	bits per pixel = 1
 	         * Compression 	4 bytes 	Type of Compression = 0
 	         * ImageSize 	4 bytes 	Size of Image in Bytes = 0 (uncompressed)
 	         * XpixelsPerM 	4 bytes 	unused = 0
 	         * YpixelsPerM 	4 bytes 	unused = 0
 	         * ColorsUsed 	4 bytes 	unused = 0
 	         * ColorsImportant 	4 bytes 	unused = 0
             * Colors 	        8 bytes 	since BitsPerPixel = 1 this will always be 2 entries
 	         * Color 0 Red 	    1 byte 	Background color red component =0
 	         * Color 0 Green 	1 byte 	Background color green component =0
 	         * Color 0 Blue 	1 byte 	Background color blue component =0
 	         * reserved 	    1 byte 	=0
 	         * Color 1 Red 	    1 byte 	Foreground color red component =255
 	         * Color 1 Green 	1 byte 	Foreground color green component =255
 	         * Color 1 Blue 	1 byte 	Foreground color blue component =255
 	         * reserved 	    1 byte 	=0
             * XORbitmap 	see below monochrome bitmap
             * ANDbitmap 	see below monochrome bitmap 
             */
            byte[] cursorHeader = new byte[0x16];
            cursorHeader[0] = 0x00;
            cursorHeader[1] = 0x00;
            cursorHeader[2] = 0x02;   // type
            cursorHeader[3] = 0x00;
            cursorHeader[4] = 0x01;   // #images in file
            cursorHeader[5] = 0x00;
            //Image #1
            cursorHeader[6] = (byte)width;    // width
            cursorHeader[7] = (byte)height;   // height
            cursorHeader[8] = 0x00;              // num of colors in palette
            cursorHeader[9] = 0x00;              // reserved
            cursorHeader[10] = (byte)(hotspotX & 0xFF);             // hotspot x
            cursorHeader[11] = (byte)(hotspotX >> 8);
            cursorHeader[12] = (byte)(hotspotY & 0xFF);             // hotspot y
            cursorHeader[13] = (byte)(hotspotY >> 8);             

            cursorHeader[14] = (byte)((resourceRawData.Length & 0x000000FF));     // size of image data
            cursorHeader[15] = (byte)((resourceRawData.Length & 0x0000FF00) >> 8);
            cursorHeader[16] = (byte)((resourceRawData.Length & 0x00FF0000) >> 16);
            cursorHeader[17] = (byte)((resourceRawData.Length & 0xFF000000) >> 24);

            cursorHeader[18] = 0x16;        // offset bmp data
            cursorHeader[19] = 0x00;
            cursorHeader[20] = 0x00;
            cursorHeader[21] = 0x00;

            // build byte array with created header and cursor data
            byte[] cursorData = new byte[cursorHeader.Length + resourceRawData.Length];
            cursorHeader.CopyTo(cursorData, 0);
            resourceRawData.CopyTo(cursorData, cursorHeader.Length);

            return cursorData;
        }

        /// <summary>
        /// Get the cursor group->cursor directory that belongs to this cursor resource.
        /// The search algorithm use the cursor ID to find a match.
        /// </summary>
        /// <returns>the cursor directory or null if it cannot be found</returns>
        private PeCursorGroupDirEntry GetResCursorGroup()
        {
            foreach (PeResourceDirectoryEntry dirEntry in peFile.PeResourceDirectoryTable.GetResourceDirectoryEntries())
            {
                if (dirEntry.IsTopLevelOfTypeID() &&
                    dirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORGROUPS)
                {
                    PeCursorGroupDirEntry cursorDir = GetResCursorGroup(dirEntry.ChildDirectory);
                    if (cursorDir != null)
                        return cursorDir;
                }
            }
            return null;
        }

        /// <summary>
        /// Helper function to recursively find the cursor group dir belonging to this cursor.
        /// </summary>
        /// <param name="peResourceDirectoryTable"></param>
        /// <returns></returns>
        private PeCursorGroupDirEntry GetResCursorGroup(PeResourceDirectoryTable peResourceDirectoryTable)
        {
            foreach (PeResourceDirectoryEntry dirEntry in peResourceDirectoryTable.GetResourceDirectoryEntries())
            {
                if (dirEntry.IsLeaf())
                {
                    PeResourceDataEntry resDataEntry = dirEntry.DataEntry;
                    if (resDataEntry.IsTopLevelOfTypeID() && resDataEntry.IsStandardID())
                    {
                        if (resDataEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORGROUPS)
                        {
                            PeCursorGroupResource resGroupcursor = (PeCursorGroupResource)resDataEntry.GetStandardResource();
                            if (resGroupcursor != null)
                            {
                                foreach (PeCursorGroupDirEntry cursorDirEntry in resGroupcursor)
                                {
                                    if (cursorDirEntry.Id.Value == cursorID)
                                    {
                                        return cursorDirEntry;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    PeCursorGroupDirEntry cursorDir = GetResCursorGroup(dirEntry.ChildDirectory);
                    if (cursorDir != null)
                        return cursorDir;
                }
            }
            return null;
        }
    }
}
