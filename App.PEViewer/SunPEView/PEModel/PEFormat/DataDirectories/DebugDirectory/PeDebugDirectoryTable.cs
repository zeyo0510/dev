using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.DebugDirectory
{
    class PeDebugDirectoryTable/* : AbstractPeDescriptor */
    {
        private PEFile peFile;
        /// <summary>
        /// start file offset of debug directory
        /// </summary>
        private long debugDirOffset;
        
        public PeDebugDirectory[] DebugDirectories { get; private set; }

        public int NumberOfDebugDirectories { get; private set; }

        public PeDebugDirectoryTable(PEFile pEFile, long debugDirOffset)
        {
            this.peFile = pEFile;
            this.debugDirOffset = debugDirOffset;

            // calculate number of debug directories. Size given in DataDirectory table of Debug Data divided by size of one debug directory structure.
            uint tableSize = peFile.PeDataDirectoryTable[Enums.EDataDirectory.IMAGE_DIRECTORY_ENTRY_DEBUG].Size.Value;
            NumberOfDebugDirectories = (int)(tableSize / PeDebugDirectory.SIZE_OF_DEBUG_DIR);
            DebugDirectories = new PeDebugDirectory[NumberOfDebugDirectories];
        }

        public void Read(IFileAccess fDataReader)
        {
            fDataReader.Position = (uint)debugDirOffset;

            for (int index = 0; index < NumberOfDebugDirectories; index++)
            {
                PeDebugDirectory debugDir = new PeDebugDirectory(peFile, debugDirOffset + (index * PeDebugDirectory.SIZE_OF_DEBUG_DIR));
                debugDir.Read(fDataReader);
                DebugDirectories[index] = debugDir;
            }
        }

        public IEnumerator<PeDebugDirectory> GetEnumerator()
        {
            foreach (PeDebugDirectory dd in DebugDirectories)
            {
                yield return dd;
            }
        }
    }

    class PeDebugDirectory : AbstractPeDescriptor
    {
        public static int SIZE_OF_DEBUG_DIR = 28;

        private PEFile peFile;
        /// <summary>
        /// start file offset of debug directory
        /// </summary>
        private long debugDirOffset;

        /// <summary>
        /// Characteristics, must always 0.
        /// </summary>
        public PeElem<uint> Characteristics { get; private set; }

        /// <summary>
        /// Time and date that the debug data was created
        /// </summary>
        public PeElem<uint> TimeDateStamp { get; private set; }

        public PeElem<ushort> MajorVersion { get; private set; }
        public PeElem<ushort> MinorVersion { get; private set; }

        /// <summary>
        /// The format of debugging information. 
        /// This field enables support of multiple debuggers. 
        /// </summary>
        public PeElem<uint> Type { get; private set; }

        /// <summary>
        /// The size of the debug data (not including the debug directory itself).
        /// </summary>
        public PeElem<uint> SizeOfData { get; private set; }

        /// <summary>
        /// The address of the debug data when loaded, relative to the image base.
        /// </summary>
        public PeElem<uint> AddressOfRawData { get; private set; }

        /// <summary>
        /// The file pointer to the debug data.
        /// </summary>
        public PeElem<uint> PointerToRawData { get; private set; }

        private int id;
        private static int idCounter;

        public PeDebugDirectory(PEFile pEFile, long debugDirOffset)
        {
            this.peFile = pEFile;
            this.debugDirOffset = debugDirOffset;
            this.id = idCounter++;
        }
        
        public void Read(IFileAccess fDataReader)
        {
            fDataReader.Position = (uint)debugDirOffset;
            Characteristics = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Debug Characteristics");
            TimeDateStamp = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Debug TimeDateStamp");
            MajorVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Debug MajorVersion");
            MinorVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Debug MinorVersion");

            Type = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Debug Type");
            SizeOfData = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Size of Debug Data");
            AddressOfRawData = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "RVA of debug data when loaded");
            PointerToRawData = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Offset to debug data");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return Characteristics;
            yield return TimeDateStamp;
            yield return MajorVersion;
            yield return MinorVersion;
            yield return Type;
            yield return SizeOfData;
            yield return AddressOfRawData;
            yield return PointerToRawData;
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(Characteristics.Name, "Debug Characteristics, always 0");
            AddDescriptionEntry(TimeDateStamp.Name, "Time and date that the debug data was created");
            AddDescriptionEntry(MajorVersion.Name, "Major version of debug data");
            AddDescriptionEntry(MinorVersion.Name, "Minor version of debug data");
            AddDescriptionEntry(Type.Name, "Format of debugging information");
            AddDescriptionEntry(SizeOfData.Name, "The size of the debug data (not including the debug directory itself)");
            AddDescriptionEntry(AddressOfRawData.Name, "The address of the debug data when loaded, relative to the image base");
            AddDescriptionEntry(PointerToRawData.Name, "The file pointer to the debug data");
        }

        public string GetName()
        {
            return new StringBuilder("Debug Directory ").Append(id).ToString();
        }
    }
}
