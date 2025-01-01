using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;

namespace SunPEView.PEModel.PEFormat.Resources
{
    class PeResourceDirectoryTable : AbstractPeDescriptor /*: IEnumerable<IPeElem>, IEnumerable<PeResourceDirectoryEntry>*/
    {
        /******************************************************
         * File elements
         ******************************************************/
        #region PE File Elements
        /// <summary>
        /// Resource flags. This field is reserved for future use. 
        /// </summary>
        public PeElem<uint> Characteristics { get; private set; }

        /// <summary>
        /// The time that the resource data was created by the resource compiler.
        /// </summary>
        public PeElem<uint> TimeDateStamp { get; private set; }

        // version numbers set by user
        public PeElem<ushort> MajorVersion { get; private set; }
        public PeElem<ushort> MinorVersion { get; private set; }

        /// <summary>
        /// The number of directory entries immediately following the table 
        /// that use strings to identify Type, Name, or Language entries 
        /// (depending on the level of the table).
        /// </summary>
        public PeElem<ushort> NumberOfNameEntries { get; private set; }

        /// <summary>
        /// The number of directory entries immediately following the Name entries 
        /// that use numeric IDs for Type, Name, or Language entries.
        /// </summary>
        public PeElem<ushort> NumberOfIdEntries { get; private set; }
        #endregion

        /// <summary>
        /// reference to pe file to get access to file location calculator
        /// </summary>
        private PEFile peFile;

        /// <summary>
        /// list of first-level resource directory entries
        /// </summary>
        private List<PeResourceDirectoryEntry> resoureDirEntries;

        /// <summary>
        /// raw starting file offset of this directory.
        /// </summary>
        private long resDirFileOffset;

        /// <summary>
        /// depth level of directory in tree
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// reference to the parent entry. If this directory is the first directory level, parentEntry is null.
        /// </summary>
        public PeResourceDirectoryEntry ParentEntry { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        public PeResourceDirectoryTable(PEFile peFile, long dirFileOffset) : 
            this(peFile, dirFileOffset, 0, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        /// <param name="dirFileOffset"></param>
        /// <param name="level"></param>
        /// <param name="parentEntry"></param>
        public PeResourceDirectoryTable(PEFile peFile, long dirFileOffset, int level, PeResourceDirectoryEntry parentEntry)
        {
            this.peFile = peFile;
            this.resoureDirEntries = new List<PeResourceDirectoryEntry>();
            this.resDirFileOffset = dirFileOffset;
            this.Level = level;
            this.ParentEntry = parentEntry;
        }

        /// <summary>
        /// Read the actual directory data including all child entries.
        /// </summary>
        /// <param name="fDataReader"></param>
        public void Read(IFileAccess fDataReader)
        {
            fDataReader.PushAddress();
            fDataReader.Position = (uint)resDirFileOffset;

            Characteristics = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Resource Characteristics");
            TimeDateStamp = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Resource TimeDateStamp");
            MajorVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Resource MajorVersion");
            MinorVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Resource MinorVersion");
            NumberOfNameEntries = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "#Name Entries");
            NumberOfIdEntries = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "#ID Entries");
        
            int numEntries = NumberOfNameEntries.Value + NumberOfIdEntries.Value;
            for (int firstLevelEntryIndex = 0; firstLevelEntryIndex < numEntries; firstLevelEntryIndex++)
            {
                // get type of entry
                PeResourceDirectoryEntry.ResourceDirectoryEntryType type =
                    firstLevelEntryIndex < NumberOfNameEntries.Value ? 
                    PeResourceDirectoryEntry.ResourceDirectoryEntryType.NAME : 
                    PeResourceDirectoryEntry.ResourceDirectoryEntryType.ID;

                int branchLevel = (ParentEntry == null) ? 1 : ParentEntry.LevelOnBranch + 1;

                if (ParentEntry == null)
                {
                    // this is the root directory, thus we have to specify the index to the entries (they are on first level)
                    PeResourceDirectoryEntry resDirEntry = new PeResourceDirectoryEntry(peFile, type, this, 1, firstLevelEntryIndex + 1);
                    resDirEntry.Read(fDataReader);
                    resoureDirEntries.Add(resDirEntry);
                }
                else
                {
                    // this entry is not on first level, so use branch index of main parent entry
                    PeResourceDirectoryEntry resDirEntry = new PeResourceDirectoryEntry(peFile, type, this, branchLevel, ParentEntry.GetTopBranchIndex());
                    resDirEntry.Read(fDataReader);
                    resoureDirEntries.Add(resDirEntry);
                }


            }

            fDataReader.PopAddress();
        }

        public ReadOnlyCollection<PeResourceDirectoryEntry> GetResourceDirectoryEntries()
        {
            return resoureDirEntries.AsReadOnly();
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return Characteristics;
            yield return TimeDateStamp;
            yield return MajorVersion;
            yield return MinorVersion;
            yield return NumberOfNameEntries;
            yield return NumberOfIdEntries;
        }

        public override string ToString()
        {
            if (ParentEntry == null)
            {
                return "Resource Directory (_," + Level + ") " + StringUtil.GetFormattedHexString(resDirFileOffset);
            }
            else
            {
                return "Resource Directory (" + ParentEntry.GetTopBranchIndex() + "," + Level + ") " + StringUtil.GetFormattedHexString(resDirFileOffset);
            }
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(Characteristics.Name, "Resource flags. This field is reserved for future use.");
            AddDescriptionEntry(TimeDateStamp.Name, "The time that the resource data was created by the resource compiler.");
            AddDescriptionEntry(MajorVersion.Name, "Major version set by user.");
            AddDescriptionEntry(MinorVersion.Name, "Minor version set by user.");
            AddDescriptionEntry(NumberOfNameEntries.Name,
                "The number of directory entries immediately following the table that use strings \n" +
                "to identify Type, Name, or Language entries (depending on the level of the table).");
            AddDescriptionEntry(NumberOfIdEntries.Name,
                "The number of directory entries immediately following the Name entries \n" + 
                "that use numeric IDs for Type, Name, or Language entries.");
        }
    }
}
