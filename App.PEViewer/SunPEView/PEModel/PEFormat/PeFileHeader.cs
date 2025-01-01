using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    class PeFileHeader : AbstractPeDescriptor
    {
        /// <summary>
        /// Number identifying the type of target machine
        /// </summary>
        public PeElem<ushort> Machine { get; private set; }

        /// <summary>
        /// Number of entries in the section table
        /// </summary>
        public PeElem<ushort> NumberOfSections { get; private set; }

        /// <summary>
        /// Time and date the file was created
        /// </summary>
        public PeElem<uint> TimeDateStamp { get; private set; }

        /// <summary>
        /// File offset of the COFF symbol table
        /// </summary>
        public PeElem<uint> PointerToSymbolTable { get; private set; }

        /// <summary>
        /// Number of entries in the symbol table
        /// </summary>
        public PeElem<uint> NumberOfSymbols { get; private set; }

        /// <summary>
        /// size of an optional header
        /// </summary>
        public PeElem<ushort> SizeOfOptionalHeader { get; private set; }

        /// <summary>
        /// Flags with information about the file
        /// </summary>
        public PeElem<ushort> Characteristics { get; private set; }

        public void Read(IFileAccess fDataReader)
        {
            Machine = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Machine");
            NumberOfSections = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "NumberOfSections");
            TimeDateStamp = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "TimeDateStamp");
            PointerToSymbolTable = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "PointerToSymbolTable");
            NumberOfSymbols = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "NumberOfSymbols");
            SizeOfOptionalHeader = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "SizeOfOptionalHeader");
            Characteristics = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Characteristics");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return Machine;
            yield return NumberOfSections;
            yield return TimeDateStamp;
            yield return PointerToSymbolTable;
            yield return NumberOfSymbols;
            yield return SizeOfOptionalHeader;
            yield return Characteristics;
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(Machine.Name, "Type of machine executable was built for");
            AddDescriptionEntry(NumberOfSections.Name, "Number of section header and bodies");
            AddDescriptionEntry(TimeDateStamp.Name, "Time this header was created. Holds the number of seconds since December 31st, 1969, at 4:00 P.M.");
            AddDescriptionEntry(PointerToSymbolTable.Name, "File offset of the COFF symbol table");
            AddDescriptionEntry(NumberOfSymbols.Name, "The number of symbols in the COFF symbol table");
            AddDescriptionEntry(SizeOfOptionalHeader.Name, "Size of the optional header that can follow this structure.");
            AddDescriptionEntry(Characteristics.Name, "Flags with information about the file");
        }
    }
}
