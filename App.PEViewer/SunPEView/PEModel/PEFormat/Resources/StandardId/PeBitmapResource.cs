using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    /// <summary>
    /// Represents one resource bitmap
    /// </summary>
    class PeBitmapResource : IPeStandardResource
    {
        private PEFile peFile;
        private byte[] resourceRawData;
        private Bitmap bmp;
        private int width, height;
        private int numBitsPerPixel;

        public PeBitmapResource(PEFile peFile)
        {
            this.peFile = peFile;
        }

        public void Read(IFileAccess fDataReader, long fileOffset, long length)
        {
            fDataReader.Position = (uint)fileOffset;
            uint dibHeaderSize = fDataReader.ReadUInt32(fDataReader.Position);
            resourceRawData = fDataReader.ReadBytes((uint)fDataReader.Position, (uint)length);
            if (dibHeaderSize == 0x28)
            {
                width = (int)fDataReader.ReadUInt32(fDataReader.Position + 4);
                height = (int)fDataReader.ReadUInt32(fDataReader.Position + 8);
                numBitsPerPixel = (int)fDataReader.ReadUInt16(fDataReader.Position + 14);
                loadBitmap();
            }
            else if (dibHeaderSize == 0x474E5089)
            {
                // bitmap is stored as PNG and not as DIB
                Stream stream = new MemoryStream(resourceRawData);
                Bitmap bmp = new Bitmap(stream);
            }
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        /// <summary>
        /// Load the data from file and builds the actual bitmap object.
        /// </summary>
        private void loadBitmap()
        {
            // build byte array with created header and bmp data
            byte[] bmpData = getBitmapFileBuffer();
            // convert to bitmap via stream
            Stream stream = new MemoryStream(bmpData);
            bmp = new System.Drawing.Bitmap(stream);
        }

        private byte[] getBitmapFileBuffer()
        {
            /* build the bitmap file header */
            /* http://de.wikipedia.org/wiki/Windows_Bitmap
             * Offset   Length  Meaning
             * 0        2       bitmap id, always "BM"
             * 2        4       size of bitmap file
             * 6        4       0
             * 10       4       offset to start of bitmap data, after file header and bitmap header,
             *                  always 54 bytes
             */
            byte[] bmpFileHeader = new byte[14];
            bmpFileHeader[0] = 0x42;    // 'B'
            bmpFileHeader[1] = 0x4D;    // 'M'
            bmpFileHeader[2] = (byte)((resourceRawData.Length & 0x000000FF));     // size of image data
            bmpFileHeader[3] = (byte)((resourceRawData.Length & 0x0000FF00) >> 8);
            bmpFileHeader[4] = (byte)((resourceRawData.Length & 0x00FF0000) >> 16);
            bmpFileHeader[5] = (byte)((resourceRawData.Length & 0xFF000000) >> 24);
            bmpFileHeader[10] = 54;

            // build byte array with created header and bitmap data
            byte[] bmpData = new byte[bmpFileHeader.Length + resourceRawData.Length];
            bmpFileHeader.CopyTo(bmpData, 0);
            resourceRawData.CopyTo(bmpData, bmpFileHeader.Length);

            return bmpData;
        }

        /// <summary>
        /// Get the actual bitmap object, might be null if invalid.
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBitmap()
        {
            return bmp;
        }
    }
}
