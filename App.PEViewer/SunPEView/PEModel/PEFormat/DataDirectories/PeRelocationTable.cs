using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    /// <summary>
    /// Class holding the whole relocation table with all entries and fixups.
    /// </summary>
    class PeRelocationTable /* : AbstractPeDescriptor */
    {
        private PEFile peFile;
        private long relocDirOffset;        /* file offset of start of relocation table */

        public List<PeRelocationEntry> Relocations { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="peFile"></param>
        /// <param name="relocDirOffset"></param>
        public PeRelocationTable(PEFile peFile, long relocDirOffset)
        {
            this.peFile = peFile;
            this.relocDirOffset = relocDirOffset;
        }

        /// <summary>
        /// Read all relocation infos from file.
        /// </summary>
        /// <param name="fDataReader"></param>
        public void Read(IFileAccess fDataReader)
        {
            fDataReader.Position = (uint)relocDirOffset;

            uint tableSize = peFile.PeDataDirectoryTable[Enums.EDataDirectory.IMAGE_DIRECTORY_ENTRY_BASERELOC].Size.Value;

            Relocations = new List<PeRelocationEntry>();
            int relocIndex = 0;
            do
            {
                PeRelocationEntry relocEntry = new PeRelocationEntry(fDataReader.Position, relocIndex);
                relocEntry.Read(fDataReader);
                Relocations.Add(relocEntry);
                relocIndex++;
            } while (fDataReader.Position < relocDirOffset + tableSize);
        }

        /// <summary>
        /// Get all entries.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PeRelocationEntry> GetEnumerator()
        {
            foreach (PeRelocationEntry relocEntry in Relocations)
            {
                yield return relocEntry;
            }
        }
    }

    /// <summary>
    /// Class holding one relocation entry of relocation table.
    /// </summary>
    class PeRelocationEntry
    {
        public PeElem<uint> VirtualAddress { get; private set; }
        public PeElem<uint> SizeOfBlock { get; private set; }

        public List<PeRelocationFixup> FixupsItems  { get; private set; }

        private uint relocDirEntryOffset;
        private int internalIndex;

        public PeRelocationEntry(uint relocDirEntryOffset, int internalIndex)
        {
            this.relocDirEntryOffset = relocDirEntryOffset;
            this.internalIndex = internalIndex;
        }

        public void Read(IFileAccess fDataReader)
        {
            VirtualAddress = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "VirtualAddress");
            SizeOfBlock = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfBlock");

            /* SizeOfBlock holds the size of the whole entry, including VirtualAddress and SizeOfBlock */
            uint numOfFixups = (SizeOfBlock.Value - 8) / 2;
            FixupsItems = new List<PeRelocationFixup>();
            for (uint i = 0; i < numOfFixups; i++)
            {
                FixupsItems.Add(new PeRelocationFixup(fDataReader, VirtualAddress.Value, i));
            }
        }

        public override string ToString()
        {
            return "Relocation Entry " + internalIndex;
        }

        public IEnumerator<PeRelocationFixup> GetEnumerator()
        {
            foreach (PeRelocationFixup fixup in FixupsItems)
            {
                yield return fixup;
            }
        }
    }

    /// <summary>
    /// Class that holds one WORD fixup entry of a relocation entry.
    /// </summary>
    class PeRelocationFixup
    {
        /// <summary>
        /// Actual fixup value.
        /// Top 4 bits: type of relocation
        /// Lower 12 bits: relative offset 
        /// </summary>
        public PeElem<ushort> FixupValue { get; private set; }

        private uint virtualAddressOfEntry;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fDataReader"></param>
        /// <param name="index"></param>
        public PeRelocationFixup(IFileAccess fDataReader, uint virtualAddressOfEntry, uint index)
        {
            FixupValue = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Fixup item " + index);
            this.virtualAddressOfEntry = virtualAddressOfEntry;
        }

        /// <summary>
        /// Get the type of relocation.
        /// </summary>
        /// <returns></returns>
        public byte GetRelocationType()
        {
            return (byte)((FixupValue.Value & 0xF000) >> 12);
        }

        /// <summary>
        /// Get the actual relative offset this fixups contains.
        /// </summary>
        /// <returns></returns>
        public ushort GetOffset()
        {
            return (ushort)(FixupValue.Value & 0x0FFF);
        }

        /// <summary>
        /// Get the type of this relocation as string.
        /// </summary>
        /// <returns></returns>
        public string GetRelocationTypeString()
        {
            EBaseRelocTypes baseRelocType = (EBaseRelocTypes)GetRelocationType();
            return baseRelocType.ToString();
        }

        public uint GetRva()
        {
            return (virtualAddressOfEntry + GetOffset());
        }
    }

    enum EBaseRelocTypes
    {
        /* values taken from WinNT.h */
        IMAGE_REL_BASED_ABSOLUTE        = 0,
        IMAGE_REL_BASED_HIGH            = 1,
        IMAGE_REL_BASED_LOW             = 2,
        IMAGE_REL_BASED_HIGHLOW         = 3,
        IMAGE_REL_BASED_HIGHADJ         = 4,
        IMAGE_REL_BASED_MIPS_JMPADDR    = 5,
        IMAGE_REL_BASED_IA64_IMM64      = 9,
        IMAGE_REL_BASED_DIR64           = 10
    }
}
