using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.Exceptions;
using SunPEView.PEModel.PEFormat.Resources.StandardId;

namespace SunPEView.PEModel.PEFormat.Resources
{
    /// <summary>
    /// PE Resource Data Entry class.
    /// </summary>
    class PeResourceDataEntry : AbstractPeDescriptor
    {
        /// <summary>
        /// The address of a unit of resource data in the Resource Data area.
        /// </summary>
        public PeElem<uint> OffsetToData { get; private set; }

        /// <summary>
        /// The size, in bytes, of the resource data that is pointed to by the Data RVA field.
        /// </summary>
        public PeElem<uint> Size { get; private set; }

        /// <summary>
        /// The code page that is used to decode code point values within the resource data.
        /// Typically, the code page would be the Unicode code page.
        /// </summary>
        public PeElem<uint> CodePage { get; private set; }

        /// <summary>
        /// Reserved, must be 0.
        /// </summary>
        public PeElem<uint> Reserved { get; private set; }

        /// <summary>
        /// reference to pe file to get access to file location calculator
        /// </summary>
        private PEFile peFile;

        /// <summary>
        /// reference to direct parent entry
        /// </summary>
        public PeResourceDirectoryEntry ParentEntry { get; private set; }

        /// <summary>
        /// file offset of data entry
        /// </summary>
        private long resDataEntryFileOffset;

        /// <summary>
        /// reference to standard resource object. Only valid if parent entry is a standard ID.
        /// </summary>
        private IPeStandardResource standardResource;

        private IFileAccess fDataReader;

        public PeResourceDataEntry(PEFile peFile, PeResourceDirectoryEntry parentEntry, long fileOffset)
        {
            this.peFile = peFile;
            this.ParentEntry = parentEntry;
            this.resDataEntryFileOffset = fileOffset;
        }

        public void Read(IFileAccess fDataReader)
        {
            this.fDataReader = fDataReader;
            fDataReader.PushAddress();
            fDataReader.Position = (uint)resDataEntryFileOffset;

            OffsetToData = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "OffsetToData");
            Size = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Size");
            CodePage = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "CodePage");
            Reserved = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Reserved");
            fDataReader.PopAddress();
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return OffsetToData;
            yield return Size;
            yield return CodePage;
            yield return Reserved;
        }

        /// <summary>
        /// Get the unique string identifying this resource data entry.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Data Entry (" + ParentEntry.GetTopBranchIndex() + ") " + StringUtil.GetFormattedHexString(resDataEntryFileOffset) ;
        }

        /// <summary>
        /// Get entry ID of toplevel entry.
        /// Throws PEModelAccessException if top level entry is not of type ID. Check with IsTopLevelOfTypeID() first. 
        /// </summary>
        /// <returns></returns>
        public uint GetTopLevelEntryID()
        {
            if (!IsTopLevelOfTypeID())
            {
                throw new PEModelAccessException();
            }
            return ParentEntry.GetTopLevelEntryID();
        }

        /// <summary>
        /// Returns true if the root entry is of type ID.
        /// </summary>
        /// <returns></returns>
        public bool IsTopLevelOfTypeID()
        {
            return ParentEntry.IsTopLevelOfTypeID();
        }


        /**************************************************************************
         * Standard Identifier handling
         **************************************************************************/

        /// <summary>
        /// Check if this data entry belongs to a standard resource element.
        /// </summary>
        /// <exception cref="PEModelAccessException">
        /// Thrown when data entry is not of type ID.
        /// </exception>
        /// <returns>true if the root resource entry is a </returns>
        public bool IsStandardID()
        {
            if (!IsTopLevelOfTypeID())
            {
                throw new PEModelAccessException();
            }
            return PeResourceStandardIdentifier.IsStandardID(GetTopLevelEntryID());
        }

        /// <summary>
        /// Get this data entry as standard resource.
        /// </summary>
        /// <exception cref="PEModelAccessException">
        /// Throws PEModelAccessException if data entry is no standard resource.
        /// </exception>
        /// <returns></returns>
        public IPeStandardResource GetStandardResource()
        {
            if (!IsStandardID())
            {
                throw new PEModelAccessException();
            }

            if (standardResource == null)
            {
                ReadStandardResource();
            }

            return standardResource;
        }

        /// <summary>
        /// Interpret this resource data entry corresponding to the standard ID of the toplevel root entry
        /// and built up the internal model.
        /// </summary>
        private void ReadStandardResource()
        {
            long offsetDataFileOffset = peFile.FileLocationCalculator.GetFileOffsetFromRVA(OffsetToData.Value);

            if (!(fDataReader.IsAddressRangeValid((uint)offsetDataFileOffset, Size.Value)))
            {
                //PELogger.Instance.Log("Wrong address and size. Maybe PE file is compressed", PELogger.LoggingLevel.ERROR);
                return;
            }

            if (GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_VERSIONINFO)
            {
                standardResource = new PeVersionInfoResource();
                standardResource.Read(fDataReader, offsetDataFileOffset, Size.Value);
            }
            else if (GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CONFIGFILES)
            {
                standardResource = new PeConfigFileResource();
                fDataReader.PushAddress();
                standardResource.Read(fDataReader, offsetDataFileOffset, Size.Value);
                fDataReader.PopAddress();
            }
            else if (GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONS)
            {
                uint iconID = this.ParentEntry.ParentDirectory.ParentEntry.getEntryID();
                standardResource = new PeIconResource(peFile, this, iconID);
                fDataReader.PushAddress();
                standardResource.Read(fDataReader, offsetDataFileOffset, Size.Value);
                fDataReader.PopAddress();
            }
            else if (GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONGROUPS)
            {
                standardResource = new PeIconGroupResource(peFile);
                fDataReader.PushAddress();
                standardResource.Read(fDataReader, offsetDataFileOffset, Size.Value);
                fDataReader.PopAddress();
            }
            else if (GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_STRINGTABLES)
            {
                standardResource = new PeStringTableResource(peFile);
                fDataReader.PushAddress();
                standardResource.Read(fDataReader, offsetDataFileOffset, Size.Value);
                fDataReader.PopAddress();
            }
            else if (GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_BITMAPS)
            {
                standardResource = new PeBitmapResource(peFile);
                fDataReader.PushAddress();
                standardResource.Read(fDataReader, offsetDataFileOffset, Size.Value);
                fDataReader.PopAddress();
            }
            else if (GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORS)
            {
                uint cursorID = this.ParentEntry.ParentDirectory.ParentEntry.getEntryID();
                standardResource = new PeCursorResource(peFile, this, cursorID);
                fDataReader.PushAddress();
                standardResource.Read(fDataReader, offsetDataFileOffset, Size.Value);
                fDataReader.PopAddress();
            }
            else if (GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORGROUPS)
            {
                standardResource = new PeCursorGroupResource(peFile);
                fDataReader.PushAddress();
                standardResource.Read(fDataReader, offsetDataFileOffset, Size.Value);
                fDataReader.PopAddress();
            }
            else if (GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_HTMLPAGES)
            {
                standardResource = new PeHtmlPageResource(peFile);
                fDataReader.PushAddress();
                standardResource.Read(fDataReader, offsetDataFileOffset, Size.Value);
                fDataReader.PopAddress();
            }
        }

        /// <summary>
        /// Get the raw file offset of the starting position of the actual data.
        /// </summary>
        /// <returns>raw file offset</returns>
        public long GetFileOffsetToRawData()
        {
            return peFile.FileLocationCalculator.GetFileOffsetFromRVA(OffsetToData.Value);
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(OffsetToData.Name, "Address of a unit of resource data in the Resource Data area");
            AddDescriptionEntry(Size.Name, "The size, in bytes, of the resource data that is pointed to by the Data RVA field");
            AddDescriptionEntry(CodePage.Name, "The code page that is used to decode code point values within the resource data.");
            AddDescriptionEntry(Reserved.Name, "Reserved, must be 0.");
        }
    }
}
