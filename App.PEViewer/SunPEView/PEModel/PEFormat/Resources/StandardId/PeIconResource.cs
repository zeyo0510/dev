using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    /// <summary>
    /// Represents one icon stored in the resource section.
    /// </summary>
    class PeIconResource : IPeStandardResource
    {
        /// <summary>
        /// handle to the icon 
        /// </summary>
        private Icon icon;
        private byte[] resourceRawData;

        /// <summary>
        /// Width and height of the icon, read from the icon group
        /// </summary>
        private int width, height;
        /// <summary>
        /// bits per pixel, needed to build the icon header
        /// </summary>
        private int numBitsPerPixel;

        private uint iconID;
        private PEFile peFile;
        private PeResourceDataEntry peResourceDataEntry;

        public PeIconResource(PEFile peFile, PeResourceDataEntry peResourceDataEntry, uint iconID)
        {
            this.peFile = peFile;
            this.peResourceDataEntry = peResourceDataEntry;
            this.iconID = iconID;
        }

        public void Read(IFileAccess fDataReader, long fileOffset, long length)
        {
            fDataReader.Position = (uint)fileOffset;

            PeIconGroupDirEntry iconDir = GetResIconGroup();

            uint dibHeaderSize = fDataReader.ReadUInt32(fDataReader.Position);
            resourceRawData = fDataReader.ReadBytes((uint)fDataReader.Position, (uint)length);
            if (dibHeaderSize == 0x28)
            {
                if (iconDir != null)
                {
                    // get the correct width and height values from icon dir
                    width = iconDir.bWidth.Value;
                    height = iconDir.bHeight.Value;
                }
                else
                {
                    // use width and height of icon resource
                    PELogger.Instance.Log("Could not get resource icon group directory entry for icon", PELogger.LoggingLevel.WARNING);
                    width = (int)fDataReader.ReadUInt32(fDataReader.Position + 4);
                    height = (int)fDataReader.ReadUInt32(fDataReader.Position + 8);
                }

                numBitsPerPixel = (int)fDataReader.ReadUInt16(fDataReader.Position + 14);
                loadIcon();
            }
            else if (dibHeaderSize == 0x474E5089)
            {
                // Icon is stored as PNG and not as DIB
                Stream stream = new MemoryStream(resourceRawData);
                Bitmap bmp = new Bitmap(stream);
                IntPtr Hicon = bmp.GetHicon();
                icon = Icon.FromHandle(Hicon);
            }
            else
            {
                icon = null;
            }
        }

        public PeResourceDataEntry getParentResourceDataEntry
        {
            get { return peResourceDataEntry; }
        }

        /// <summary>
        /// Get the icon group->icon directory that belongs to this icon resource.
        /// The search algorithm use the icon ID to find a match.
        /// </summary>
        /// <returns>the icon directory or null if it cannot be found</returns>
        private PeIconGroupDirEntry GetResIconGroup()
        {
            foreach (PeResourceDirectoryEntry dirEntry in peFile.PeResourceDirectoryTable.GetResourceDirectoryEntries())
            {
                if (dirEntry.IsTopLevelOfTypeID() && 
                    dirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONGROUPS)
                {
                    PeIconGroupDirEntry iconDir = GetResIconGroup(dirEntry.ChildDirectory);
                    if (iconDir != null)
                        return iconDir;
                }
            }
            return null;
        }

        /// <summary>
        /// Helper function to recursively find the icon group dir belonging to this icon.
        /// </summary>
        /// <param name="peResourceDirectoryTable"></param>
        /// <returns></returns>
        private PeIconGroupDirEntry GetResIconGroup(PeResourceDirectoryTable peResourceDirectoryTable)
        {
            foreach (PeResourceDirectoryEntry dirEntry in peResourceDirectoryTable.GetResourceDirectoryEntries())
            {
                if (dirEntry.IsLeaf())
                {
                    
                    PeResourceDataEntry resDataEntry = dirEntry.DataEntry;
                    if (resDataEntry.IsTopLevelOfTypeID() && resDataEntry.IsStandardID())
                    {
                        if (resDataEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONGROUPS)
                        {
                            PeIconGroupResource resGroupIcon = (PeIconGroupResource)resDataEntry.GetStandardResource();
                            if (resGroupIcon != null)
                            {
                                foreach (PeIconGroupDirEntry iconDirEntry in resGroupIcon)
                                {
                                    //Console.Out.WriteLine(iconDirEntry.nId.Value);
                                    if (iconDirEntry.nId.Value == iconID)
                                    {
                                        return iconDirEntry;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    PeIconGroupDirEntry iconDir = GetResIconGroup(dirEntry.ChildDirectory);
                    if (iconDir != null)
                        return iconDir;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the icon of this resource.
        /// Returns NULL if icon could not be loaded.
        /// </summary>
        /// <returns></returns>
        public Icon getIcon()
        {
            return icon;
        }

        /// <summary>
        /// Returns true if icon was successfully built from memory and is available.
        /// </summary>
        /// <returns></returns>
        //public bool isIconValid() { return icon != null; }

        public int IconWidth { get { return width; } }
        public int IconHeight { get { return height; } }
        public uint IconId { get { return iconID; } }

        /// <summary>
        /// Load icon from memory (not applicable for PNG)
        /// </summary>
        private void loadIcon()
        {
            // build byte array with created header and icon data
            byte[] iconData = getIconFileBuffer();

            Stream stream = new MemoryStream(iconData);
            icon = new System.Drawing.Icon(stream);

            ///* dirty workaround: I did not manage to create an icon object directly from the byte buffer.
            // * Only working way I figured out was to save the created icon header along with the actual
            // * icon data to file and load it again to get the icon object.
            // * TODO: If time somewhen, improve it!  -> DONE, only here for reference
            // */
            //string tmpFileName = System.IO.Path.GetTempPath() + "SunPeView" + Guid.NewGuid().ToString() + ".ico";
            //Console.Out.WriteLine(tmpFileName);
            //FileStream file = new FileStream(tmpFileName, FileMode.Create);
            //file.Write(iconData, 0, iconData.Length);
            //file.Close();
            //MemoryStream ms = new MemoryStream(iconData);
            //icon = new System.Drawing.Icon(tmpFileName);
            //File.Delete(tmpFileName);
        }

        /// <summary>
        /// Build the icon header and append the actual icon data to get the complete icon memory as byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] getIconFileBuffer()
        {
            /* http://en.wikipedia.org/wiki/ICO_%28file_format%29
             * Offset   Length  Meaning
             * 0        2       Reserved. Must be 0.
             * 2        2       Specifies image type: 1 for icon (.ICO) image, 2 for cursor (.CUR) image. Other values are invalid.
             * 4        2       Specifies number of images in the file.
             * 6        1       Specifies image width in pixels. Can be any number between 0 and 255. Value 0 means image width is 256 pixels.
             * 7        1       Specifies image height in pixels. Can be any number between 0 and 255. Value 0 means image height is 256 pixels.
             * 8        1       Specifies number of colors in the color palette. Should be 0 if the image does not use a color palette.
             * 9        0       Reserved. Must be 0.
             * 10       2       Specifies color planes. Should be 0 or 1.
             * 12       2       In ICO format: Specifies bits per pixel.
             * 14       4       Specifies the size of the image's data in bytes
             * 18       4       Specifies the offset of BMP or PNG data from the beginning of the ICO/CUR file
             * => total size 22 bytes
             */
            byte[] iconHeader = new byte[0x16];
            iconHeader[0] = 0x00;
            iconHeader[1] = 0x00;
            iconHeader[2] = 0x01;   // type
            iconHeader[3] = 0x00;
            iconHeader[4] = 0x01;   // #images in file
            iconHeader[5] = 0x00;
            //Image #1
            iconHeader[6] = (byte)width;    // width
            iconHeader[7] = (byte)height;   // height
            iconHeader[8] = 0x00;              // num of colors in palette
            iconHeader[9] = 0x00;              // reserved
            iconHeader[10] = 0x01;             // color planes LW
            iconHeader[11] = 0x00;             // color planes HW
            iconHeader[12] = (byte)numBitsPerPixel;     // bpp LW
            iconHeader[13] = 0x00;                         // bpp HW

            iconHeader[14] = (byte)((resourceRawData.Length & 0x000000FF));     // size of image data
            iconHeader[15] = (byte)((resourceRawData.Length & 0x0000FF00) >> 8);
            iconHeader[16] = (byte)((resourceRawData.Length & 0x00FF0000) >> 16);
            iconHeader[17] = (byte)((resourceRawData.Length & 0xFF000000) >> 24);

            iconHeader[18] = 0x16;        // offset bmp data
            iconHeader[19] = 0x00;
            iconHeader[20] = 0x00;
            iconHeader[21] = 0x00;

            // build byte array with created header and icon data
            byte[] iconData = new byte[iconHeader.Length + resourceRawData.Length];
            iconHeader.CopyTo(iconData, 0);
            resourceRawData.CopyTo(iconData, iconHeader.Length);

            return iconData;
        }
    }
}
