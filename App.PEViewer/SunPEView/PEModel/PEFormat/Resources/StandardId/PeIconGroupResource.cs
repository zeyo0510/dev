using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    /// <summary>
    /// Represents one icon group in the resource section.
    /// Information sources:
    /// http://blogs.msdn.com/b/oldnewthing/archive/2012/07/20/10331787.aspx
    /// http://blogs.msdn.com/b/oldnewthing/archive/2010/10/18/10077133.aspx
    /// http://msdn.microsoft.com/en-us/library/ms997538.aspx
    /// </summary>
    class PeIconGroupResource : IPeStandardResource, IEnumerable<PeIconGroupDirEntry>
    {
        /*
            typedef struct
            {
                WORD           idReserved;   // Reserved (must be 0)
                WORD           idType;       // Resource Type (1 for icons)
                WORD           idCount;      // How many images?
                ICONDIRENTRY   idEntries[1]; // An entry for each image (idCount of 'em)
            } ICONDIR, *LPICONDIR;
         */
        public PeElem<ushort> idReserved { get; private set; }
        public PeElem<ushort> idType { get; private set; }
        public PeElem<ushort> idCount { get; private set; }

        /// <summary>
        /// List of all icon directories belonging to this icon group
        /// </summary>
        public List<PeIconGroupDirEntry> IconDirEntries { get; private set; }

        /// <summary>
        /// reference to PE file
        /// </summary>
        public PEFile peFile;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        public PeIconGroupResource(PEFile peFile)
        {
            this.peFile = peFile;
        }

        /// <summary>
        /// Read resource icon group including all child dirs from file
        /// </summary>
        /// <param name="fDataReader"></param>
        /// <param name="fileOffset"></param>
        /// <param name="length"></param>
        public void Read(IFileAccess fDataReader, long fileOffset, long length)
        {
            fDataReader.PushAddress();
            fDataReader.Position = (uint)fileOffset;

            idReserved = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "idReserved (ICONDIR)");
            idType = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "idType (ICONDIR)");
            idCount = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "idCount (ICONDIR)");

            // create list in any case to avoid exception wheh enumerator is called
            IconDirEntries = new List<PeIconGroupDirEntry>();
            
            if (Validate())
            {
                for (int i = 0; i < idCount.Value; i++)
                {
                    PeIconGroupDirEntry iconDir = new PeIconGroupDirEntry(this);
                    iconDir.Read(fDataReader);
                    IconDirEntries.Add(iconDir);
                }

                fDataReader.PopAddress();
            }
            
        }

        /// <summary>
        /// Check the loaded icon group structure for inconsistencies.
        /// </summary>
        private bool Validate()
        {
            if (idReserved.Value != 0)
            {
                PELogger.Instance.Log("ICON GROUP : Invalid reserved ID of " + idReserved.Value + ". (Expected: 0)", PELogger.LoggingLevel.WARNING);
                return false;
            }

            if (idType.Value != 1)
            {
                PELogger.Instance.Log("ICON GROUP : Invalid idType of " + idType.Value + ". (Expected: 1)", PELogger.LoggingLevel.WARNING);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Enumerator for the child icon directories.
        /// </summary>
        /// <returns></returns>
        IEnumerator<PeIconGroupDirEntry> IEnumerable<PeIconGroupDirEntry>.GetEnumerator()
        {
            foreach (PeIconGroupDirEntry iconDir in IconDirEntries)
            {
                yield return iconDir;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.IconDirEntries.GetEnumerator();
        }
    }

    /// <summary>
    /// Class representing one icon directory entry in file.
    /// </summary>
    class PeIconGroupDirEntry : AbstractPeDescriptor
    {
        /*
            typedef struct
            {
                BYTE        bWidth;          // Width, in pixels, of the image
                BYTE        bHeight;         // Height, in pixels, of the image
                BYTE        bColorCount;     // Number of colors in image (0 if >=8bpp)
                BYTE        bReserved;       // Reserved ( must be 0)
                WORD        wPlanes;         // Color Planes
                WORD        wBitCount;       // Bits per pixel
                DWORD       dwBytesInRes;    // How many bytes in this resource?
                // -> only valid for icons on disk !!! DWORD       dwImageOffset;   // Where in the file is this image?
                In resource files, we have nID.
                WORD        nId;
            } ICONDIRENTRY, *LPICONDIRENTRY;
         */
        public PeElem<byte> bWidth { get; private set; }
        public PeElem<byte> bHeight { get; private set; }
        public PeElem<byte> bColorCount { get; private set; }
        public PeElem<byte> bReserved { get; private set; }
        public PeElem<ushort> wPlanes { get; private set; }
        public PeElem<ushort> wBitCount { get; private set; }
        public PeElem<uint> dwBytesInRes { get; private set; }
        public PeElem<ushort> nId { get; private set; }

        /// <summary>
        /// file offset where this icon group directory starts
        /// </summary>
        public uint startFileOffset;
        /// <summary>
        /// reference to parent icon group this icon directory belongs to
        /// </summary>
        private PeIconGroupResource peIconGroupResource;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peIconGroupResource"></param>
        public PeIconGroupDirEntry(PeIconGroupResource peIconGroupResource)
        {
            this.peIconGroupResource = peIconGroupResource;
        }

        /// <summary>
        /// Read a icon directory entry. Correct file reader position expected.
        /// </summary>
        /// <param name="fDataReader"></param>
        public void Read(IFileAccess fDataReader)
        {
            startFileOffset = fDataReader.Position;

            bWidth = new PeElem<byte>(fDataReader.Position, fDataReader.ReadByte(), "bWidth");
            bHeight = new PeElem<byte>(fDataReader.Position, fDataReader.ReadByte(), "bHeight");
            bColorCount = new PeElem<byte>(fDataReader.Position, fDataReader.ReadByte(), "bColorCount");
            bReserved = new PeElem<byte>(fDataReader.Position, fDataReader.ReadByte(), "bReserved");
            wPlanes = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wPlanes");
            wBitCount = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wBitCount");
            dwBytesInRes = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwBytesInRes");
            nId = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "nId");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return bWidth;
            yield return bHeight;
            yield return bColorCount;
            yield return bReserved;
            yield return wPlanes;
            yield return wBitCount;
            yield return dwBytesInRes;
            yield return nId;
        }

        /// <summary>
        /// Get unique description string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Icon Dir @" + StringUtil.GetFormattedHexString(startFileOffset) + " for Icon ID " + nId.Value;
        }

        /// <summary>
        /// Get parent icon group resource
        /// </summary>
        public PeIconGroupResource ParentIconGroup
        {
            get { return peIconGroupResource; }
        }

        /// <summary>
        /// Add hints
        /// </summary>
        protected override void AddDescriptions()
        {
            AddDescriptionEntry(bWidth.Name, "Width, in pixels, of the image");
            AddDescriptionEntry(bHeight.Name, "Height, in pixels, of the image");
            AddDescriptionEntry(bColorCount.Name, "Number of colors in image (0 if >=8bpp)");
            AddDescriptionEntry(bReserved.Name, "Reserved ( must be 0)");
            AddDescriptionEntry(wPlanes.Name, "Color planes");
            AddDescriptionEntry(wBitCount.Name, "Bits per pixel");
            AddDescriptionEntry(dwBytesInRes.Name, "Size in bytes of this resource");
            AddDescriptionEntry(nId.Name, "ID of icon resource to which this directory entry references");
        }

        /// <summary>
        /// Get the icon group dir belonging to this icon.
        /// </summary>
        /// <returns></returns>
        public PeIconResource GetResIcon()
        {
            foreach (PeResourceDirectoryEntry dirEntry in ParentIconGroup.peFile.PeResourceDirectoryTable.GetResourceDirectoryEntries())
            {
                if (dirEntry.IsTopLevelOfTypeID() &&
                    dirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONS)
                {
                    PeIconResource icon = GetResIcon(dirEntry.ChildDirectory);
                    if (icon != null)
                        return icon;
                }
            }
            return null;
        }

        /// <summary>
        /// Helper function to recursively find the icon group dir belonging to this icon.
        /// </summary>
        /// <param name="peResourceDirectoryTable"></param>
        /// <returns></returns>
        private PeIconResource GetResIcon(PeResourceDirectoryTable peResourceDirectoryTable)
        {
            foreach (PeResourceDirectoryEntry dirEntry in peResourceDirectoryTable.GetResourceDirectoryEntries())
            {
                if (dirEntry.IsLeaf())
                {
                    PeResourceDataEntry resDataEntry = dirEntry.DataEntry;
                    if (resDataEntry.IsTopLevelOfTypeID() && resDataEntry.IsStandardID())
                    {
                        if (resDataEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONS)
                        {
                            PeIconResource resIcon = (PeIconResource)resDataEntry.GetStandardResource();
                            if (resIcon != null && resIcon.IconId == nId.Value)
                            {
                                return resIcon;
                            }
                        }
                    }
                }
                else
                {
                    PeIconResource icon = GetResIcon(dirEntry.ChildDirectory);
                    if (icon != null)
                        return icon;
                }
            }
            return null;
        }
    }
}
