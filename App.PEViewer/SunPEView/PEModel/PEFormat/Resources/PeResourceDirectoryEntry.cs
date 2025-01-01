using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat.Enums;
using SunPEView.PEModel.Exceptions;

namespace SunPEView.PEModel.PEFormat.Resources
{
    class PeResourceDirectoryEntry : AbstractPeDescriptor
    {
        public enum ResourceDirectoryEntryType
        {
            NAME,
            ID
        }

        /// <summary>
        /// If Name type: The address of a string that gives the Type, Name, or Language ID entry, 
        /// depending on level of table. 
        /// In this case, high bit should be 1, remaining 31 bits indicate the offset of the name.
        /// 
        /// if ID type: A 32-bit integer that identifies the Type, Name, or Language ID entry.
        /// In this case, high bit should be 0, remaining 31 bits indicate the ID.
        /// </summary>
        public PeElem<uint> NameRvaId { get; private set; }
        public static readonly uint IMAGE_RESOURCE_NAME_IS_STRING = 0x80000000;
        public static readonly uint IMAGE_RESOURCE_NAME_OFFSET_MASK = 0x7FFFFFFF;

        /// <summary>
        /// High bit 0. Address of a Resource Data entry (a leaf).
        /// High bit 1. The lower 31 bits are the address of another resource directory table (the next level down).
        /// </summary>
        public PeElem<uint> DataEntrySubdirRva { get; private set; }
        public static readonly uint RESOURCE_DATA_IS_DIRECTORY = 0x80000000;


        /// <summary>
        /// type of this resource entry
        /// </summary>
        public ResourceDirectoryEntryType EntryType { get; private set; }

        /// <summary>
        /// reference to the parent directory. If this is the first level, parentDirectory is null;
        /// </summary>
        public PeResourceDirectoryTable ParentDirectory { get; private set; }

        /// <summary>
        /// reference to the child directory. If this entry is a leaf, childDirectory is null.
        /// </summary>
        public PeResourceDirectoryTable ChildDirectory { get; private set; }

        /// <summary>
        /// reference to actual data entry. Only valid if entry is a leaf.
        /// </summary>
        public PeResourceDataEntry DataEntry { get; private set; }

        /// <summary>
        /// Internal ID of this entry. Refers to index of toplevel entry.
        /// </summary>
        public int BranchIndex { get; private set; }

        /// <summary>
        /// level (depth) of this entry inside branch. Starts at 1.
        /// </summary>
        public int LevelOnBranch { get; private set; }


        /// <summary>
        /// reference to pe file to get access to file location calculator
        /// </summary>
        private PEFile peFile;

        /// <summary>
        /// reference to file data reader
        /// </summary>
        private IFileAccess fDataReader;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        public PeResourceDirectoryEntry(PEFile peFile,
                                        ResourceDirectoryEntryType type,
                                        PeResourceDirectoryTable parentDirectory,
                                        int branchLevel,
                                        int branchIndex
                                        )
        {
            this.peFile = peFile;
            this.EntryType = type;
            this.ParentDirectory = parentDirectory;
            this.LevelOnBranch = branchLevel;
            this.BranchIndex = branchIndex;
        }

        /// <summary>
        /// Read the data from file of this entry structure.
        /// </summary>
        /// <param name="fDataReader"></param>
        public void Read(IFileAccess fDataReader)
        {
            this.fDataReader = fDataReader;

            NameRvaId = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "NameRvaId");
            DataEntrySubdirRva = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "DataEntrySubdirRva");

            if (EntryType == ResourceDirectoryEntryType.NAME)
            {
                ReadEntryNameFromFile();
            }

            fDataReader.PushAddress();
            if (!IsLeaf())
            {
                // calculate file offset to child directory. The DataEntrySubdirRva is relative to the starting address of the resources.
                long startAddressRva = peFile.PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_RESOURCE].VirtualAddress.Value;
                long childDirRva = startAddressRva + (DataEntrySubdirRva.Value & 0x7FFFFFFF);
                long fileOffsetToChildDir = peFile.FileLocationCalculator.GetFileOffsetFromRVA(childDirRva);
                ChildDirectory = new PeResourceDirectoryTable(peFile, fileOffsetToChildDir, ParentDirectory.Level + 1, this);
                ChildDirectory.Read(fDataReader);
                DataEntry = null;
            }
            else
            {
                // leaf node
                ChildDirectory = null;

                long startAddressRva = peFile.PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_RESOURCE].VirtualAddress.Value;
                long childDirRva = startAddressRva + (DataEntrySubdirRva.Value & 0x7FFFFFFF);
                long fileOffsetToDataDir = peFile.FileLocationCalculator.GetFileOffsetFromRVA(childDirRva);
                DataEntry = new PeResourceDataEntry(peFile, this, fileOffsetToDataDir);
                DataEntry.Read(fDataReader);
                
            }
            fDataReader.PopAddress();
        }

        /// <summary>
        /// Get unique string description.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (EntryType == ResourceDirectoryEntryType.ID)
            {
                string idstring = getEntryID() + " / " + String.Format("0x{0:X}", getEntryID());

                if (LevelOnBranch == 1 && PeResourceStandardIdentifier.IsStandardID(getEntryID()))
                {
                    // ID is a standard resource ID, append name */
                    return "Entry [" + GetTopBranchIndex() + "," + LevelOnBranch + "] (" + 
                        EntryType.ToString() + ": " + idstring + " - " + PeResourceStandardIdentifier.Identifier[getEntryID()] + ")";
                }
                else
                {
                    return "Entry [" + GetTopBranchIndex() + "," + LevelOnBranch + "] (" + 
                        EntryType.ToString() + ": " + idstring + ")  " + StringUtil.GetFormattedHexString(this.NameRvaId.Offset);
                }
            }
            else
            {
                return "Entry [" + GetTopBranchIndex() + "," + LevelOnBranch + "] (" + EntryType.ToString() + ": " + getEntryName() + ")";
            }
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return NameRvaId;
            yield return DataEntrySubdirRva;
        }

        /// <summary>
        /// Returns true of entry is a leaf, thus have no child directory, but a child DataEntry.
        /// </summary>
        /// <returns></returns>
        public bool IsLeaf()
        {
            return ((DataEntrySubdirRva.Value & RESOURCE_DATA_IS_DIRECTORY) == 0);
        }

        /// <summary>
        /// Get the "PE" ID of this entry.
        /// </summary>
        /// <returns></returns>
        public uint getEntryID()
        {
            if (EntryType == ResourceDirectoryEntryType.ID)
            {
                return (NameRvaId.Value & 0x7FFFFFFF);
            }
            else
            {
                throw new PEModelAccessException();
            }
        }

        /// <summary>
        /// Get the name of this entry. Call this only if entry is of type NAME.
        /// </summary>
        /// <returns></returns>
        public string getEntryName()
        {
            if (EntryType == ResourceDirectoryEntryType.NAME)
            {
                if ((NameRvaId.Value & IMAGE_RESOURCE_NAME_IS_STRING) == 0)
                {
                    throw new PEModelAccessException();
                }
                else
                {
                    return ReadEntryNameFromFile();
                }
            }
            else
            {
                throw new PEModelAccessException();
            }
        }

        /// <summary>
        /// Read actual name value from file.
        /// </summary>
        /// <returns></returns>
        private string ReadEntryNameFromFile()
        {
            if (fDataReader == null)
                throw new PEModelAccessException();

            // get address to _IMAGE_RESOURCE_DIRECTORY_STRING struct
            long startAddressRva = peFile.PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_RESOURCE].VirtualAddress.Value;
            long nameStructRva = startAddressRva + (NameRvaId.Value & IMAGE_RESOURCE_NAME_OFFSET_MASK);
            long fileOffsetToNameStruct = peFile.FileLocationCalculator.GetFileOffsetFromRVA(nameStructRva);

            // at this point we have the file offset to the following structure

            /*
            // For resource directory entries that have actual string names, the Name
            // field of the directory entry points to an object of the following type.
            // All of these string objects are stored together after the last resource
            // directory entry and before the first resource data object.  This minimizes
            // the impact of these variable length objects on the alignment of the fixed
            // size directory entry objects.
              
             typedef struct _IMAGE_RESOURCE_DIR_STRING_U {
                WORD    Length;
                WCHAR   NameString[ 1 ];
            } IMAGE_RESOURCE_DIR_STRING_U, *PIMAGE_RESOURCE_DIR_STRING_U;
            */

            // read actual 'NameString' from file
            ushort strLength = fDataReader.ReadUInt16((uint)fileOffsetToNameStruct);
            string nameString = fDataReader.ReadUnicode((uint)(fileOffsetToNameStruct + 2), strLength);
            return nameString;
        }

        /// <summary>
        /// Get the internal ID (the ID of the branch) of this entry
        /// </summary>
        /// <returns></returns>
        public int GetTopBranchIndex()
        {
            PeResourceDirectoryEntry entry = this;
            while (entry.ParentDirectory.ParentEntry != null)
            {
                entry = entry.ParentDirectory.ParentEntry;
            }
            return entry.BranchIndex;            
        }

        /// <summary>
        /// Get the "PE" ID of the actual (toplevel) entry
        /// </summary>
        /// <returns></returns>
        public uint GetTopLevelEntryID()
        {
            PeResourceDirectoryEntry entry = this;
            while (entry.ParentDirectory.ParentEntry != null)
            {
                entry = entry.ParentDirectory.ParentEntry;
            }
            return entry.getEntryID();
        }

        /// <summary>
        /// Get the "PE" ID of the actual (toplevel) entry
        /// </summary>
        /// <returns></returns>
        public bool IsTopLevelOfTypeID()
        {
            PeResourceDirectoryEntry entry = this;
            while (entry.ParentDirectory.ParentEntry != null)
            {
                entry = entry.ParentDirectory.ParentEntry;
            }
            return entry.EntryType == ResourceDirectoryEntryType.ID;
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(NameRvaId.Name, 
                "If Name type: The address of a string that gives the Type, Name, or Language ID entry, \n"+ 
                "depending on level of table. \n" + 
                "In this case, high bit should be 1, remaining 31 bits indicate the offset of the name. \n" +
                "if ID type: A 32-bit integer that identifies the Type, Name, or Language ID entry. \n" + 
                "In this case, high bit should be 0, remaining 31 bits indicate the ID.");
            AddDescriptionEntry(DataEntrySubdirRva.Name, 
                "High bit 0. Address of a Resource Data entry (a leaf). " + 
                "High bit 1. The lower 31 bits are the address of another resource directory table (the next level down).");
        }
    }
}
