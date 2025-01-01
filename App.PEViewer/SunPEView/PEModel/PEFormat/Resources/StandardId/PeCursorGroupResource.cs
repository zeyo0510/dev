using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    /// <summary>
    /// Represents one resource group direction in resources.
    /// Information:
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms648023%28v=vs.85%29.aspx
    /// 
    /// </summary>
    class PeCursorGroupResource : IPeStandardResource//, IEnumerable<PeIconGroupDirEntry>
    {
        /*
         * typedef struct {
              WORD Reserved;
              WORD ResType;
              WORD ResCount;
            } NEWHEADER, *NEWHEADER;
         * 
         * One or more RESDIR structures immediately follow the NEWHEADER structure
         */

        public PeElem<ushort> Reserved { get; private set; }
        public PeElem<ushort> ResType { get; private set; }
        public PeElem<ushort> ResCount { get; private set; }

        /// <summary>
        /// List of all cursor directories belonging to this icon group
        /// </summary>
        public List<PeCursorGroupDirEntry> CursorDirEntries { get; private set; }

        /// <summary>
        /// reference to PE file
        /// </summary>
        public PEFile peFile;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        public PeCursorGroupResource(PEFile peFile)
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

            Reserved = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Reserved (NEWHEADER)");
            ResType = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "ResType (NEWHEADER)");
            ResCount = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "ResCount (NEWHEADER)");

            CursorDirEntries = new List<PeCursorGroupDirEntry>();

            if (Validate())
            {   
                for (int i = 0; i < ResCount.Value; i++)
                {
                    PeCursorGroupDirEntry cursorDir = new PeCursorGroupDirEntry(this);
                    cursorDir.Read(fDataReader);
                    CursorDirEntries.Add(cursorDir);
                }

                fDataReader.PopAddress();
            }
            
        }

        /// <summary>
        /// Check the loaded cursor group structure for inconsistencies.
        /// </summary>
        private bool Validate()
        {
            if (Reserved.Value != 0)
            {
                PELogger.Instance.Log("CURSOR GROUP : Invalid reserved ID of " + Reserved.Value + ". (Expected: 0)", PELogger.LoggingLevel.WARNING);
                return false;
            }

            if (ResType.Value != 2)
            {
                PELogger.Instance.Log("CURSOR GROUP : Invalid idType of " + ResType.Value + ". (Expected: 2)", PELogger.LoggingLevel.WARNING);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Enumerator for the child icon directories.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PeCursorGroupDirEntry> GetEnumerator()
        {
            foreach (PeCursorGroupDirEntry cursorDir in CursorDirEntries)
            {
                yield return cursorDir;
            }
        }
    }

    /// <summary>
    /// Class representing one cursor directory entry in file.
    /// </summary>
    class PeCursorGroupDirEntry : AbstractPeDescriptor
    {
        /*
         * typedef struct {
              CURSORDIR Icon;
              WORD        wPlanes;         // Color Planes
              WORD        wBitCount;       // Bits per pixel
              DWORD       dwBytesInRes;    // How many bytes in this resource?
              WORD        nId;
            } RESDIR;
         * 
         * typedef struct {
              WORD Width;
              WORD Height;
            } CURSORDIR;
         */

        public PeElem<ushort> Width { get; private set; }
        public PeElem<ushort> Height { get; private set; }
        public PeElem<ushort> Planes { get; private set; }
        public PeElem<ushort> BitCount { get; private set; }
        public PeElem<uint> BytesInRes { get; private set; }
        public PeElem<ushort> Id { get; private set; }

        /// <summary>
        /// file offset where this icon group directory starts
        /// </summary>
        public uint startFileOffset;
        /// <summary>
        /// reference to parent icon group this icon directory belongs to
        /// </summary>
        private PeCursorGroupResource peCursorGroupResource;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peIconGroupResource"></param>
        public PeCursorGroupDirEntry(PeCursorGroupResource peCursorGroupResource)
        {
            this.peCursorGroupResource = peCursorGroupResource;
        }

        /// <summary>
        /// Read a icon directory entry. Correct file reader position expected.
        /// </summary>
        /// <param name="fDataReader"></param>
        public void Read(IFileAccess fDataReader)
        {
            startFileOffset = fDataReader.Position;

            Width = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Width");
            Height = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Height");
            Planes = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Planes");
            BitCount = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "BitCount");
            BytesInRes = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "BytesInRes");
            Id = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Id");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return Width;
            yield return Height;
            yield return Planes;
            yield return BitCount;
            yield return BytesInRes;
            yield return Id;
        }

        /// <summary>
        /// Get unique description string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Cursor Dir @" + StringUtil.GetFormattedHexString(startFileOffset) + " for Cursor ID " + Id.Value;
        }

        /// <summary>
        /// Get parent icon group resource
        /// </summary>
        public PeCursorGroupResource ParentCursorGroup
        {
            get { return peCursorGroupResource; }
        }

        /// <summary>
        /// Add hints
        /// </summary>
        protected override void AddDescriptions()
        {
            AddDescriptionEntry(Width.Name, "Width, in pixels, of the image");
            AddDescriptionEntry(Height.Name, "Height, in pixels, of the image");
            AddDescriptionEntry(Planes.Name, "Color planes");
            AddDescriptionEntry(BitCount.Name, "Bits per pixel");
            AddDescriptionEntry(BytesInRes.Name, "Size in bytes of this resource");
            AddDescriptionEntry(Id.Name, "ID of cursor resource to which this directory entry references");
        }

        public PeCursorResource GetResCursor()
        {
            foreach (PeResourceDirectoryEntry dirEntry in ParentCursorGroup.peFile.PeResourceDirectoryTable.GetResourceDirectoryEntries())
            {
                if (dirEntry.IsTopLevelOfTypeID() &&
                    dirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORS)
                {
                    PeCursorResource cursorRes = GetResCursor(dirEntry.ChildDirectory);
                    if (cursorRes != null)
                        return cursorRes;
                }
            }
            return null;
        }

        /// <summary>
        /// Helper function to recursively find the icon group dir belonging to this icon.
        /// </summary>
        /// <param name="peResourceDirectoryTable"></param>
        /// <returns></returns>
        private PeCursorResource GetResCursor(PeResourceDirectoryTable peResourceDirectoryTable)
        {
            foreach (PeResourceDirectoryEntry dirEntry in peResourceDirectoryTable.GetResourceDirectoryEntries())
            {
                if (dirEntry.IsLeaf())
                {
                    PeResourceDataEntry resDataEntry = dirEntry.DataEntry;
                    if (resDataEntry.IsTopLevelOfTypeID() && resDataEntry.IsStandardID())
                    {
                        if (resDataEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORS)
                        {
                            PeCursorResource cursorRes = (PeCursorResource)resDataEntry.GetStandardResource();
                            if (cursorRes != null && cursorRes.CursorId == Id.Value)
                            {
                                return cursorRes;
                            }
                        }
                    }
                }
                else
                {
                    PeCursorResource cursorRes = GetResCursor(dirEntry.ChildDirectory);
                    if (cursorRes != null)
                        return cursorRes;
                }
            }
            return null;
        }
    }
}
