using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    class PeSectionHeader : AbstractPeDescriptor
    {
        /// <summary>
        /// internal zero-based index
        /// </summary>
        private int index;

        /// <summary>
        /// Name of section, an 8-byte, null-padded ASCII string
        /// </summary>
        public PeElem<string> Name { get; private set; }

        /// <summary>
        /// Total size of the section when loaded into memory. 
        /// If this value is greater than Size of Raw Data, the section is zero-padded
        /// </summary>
        public PeElem<uint> VirtualSize { get; private set; }

        /// <summary>
        /// RVA of the beginning of the section
        /// </summary>
        public PeElem<uint> VirtualAddress { get; private set; }

        /// <summary>
        /// Size of the initialized data on disk (image files). 
        /// For executable image, this must be a multiple of FileAlignment. 
        /// If this is less than VirtualSize the remainder of the section is zero filled. 
        /// Because this field is rounded while the VirtualSize field is not it is possible 
        /// for this to be greater than VirtualSize as well. 
        /// </summary>
        public PeElem<uint> SizeOfRawData { get; private set; }

        /// <summary>
        /// File pointer to the section’s first page. 
        /// In image files, this value should be a multiple of the FileAlignment
        /// </summary>
        public PeElem<uint> PointerToRawData { get; private set; }

        /// <summary>
        /// File pointer to beginning of relocation entries for the section.
        /// 0 for image files.
        /// </summary>
        public PeElem<uint> PointerToRelocations { get; private set; }

        /// <summary>
        /// File pointer to beginning of line-number entries for the section. Set to 0 if there are no COFF line numbers.
        /// </summary>
        public PeElem<uint> PointerToLinenumbers { get; private set; }

        /// <summary>
        /// Number of relocation entries for the section. Set to 0 for executable images.
        /// </summary>
        public PeElem<ushort> NumberOfRelocations { get; private set; }

        /// <summary>
        ///  In managed image files, this field should be set to 0.
        /// </summary>
        public PeElem<ushort> NumberOfLinenumbers { get; private set; }

        /// <summary>
        /// File pointer to beginning of line-number entries for the section. Set to 0 if there are no COFF line numbers.
        /// </summary>
        public PeElem<uint> Characteristics { get; private set; }

        public PeSectionHeader(int index)
        {
            this.index = index;
        }

        public void Read(IFileAccess fDataReader)
        {
            Name = new PeElem<string>(fDataReader.Position, fDataReader.ReadAscii(8), "Name", 8);
            VirtualSize = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Virtual Size");
            VirtualAddress = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Virtual Address");
            SizeOfRawData = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Raw Size");
            PointerToRawData = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Raw Address");
            PointerToRelocations = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "PointerToRelocations");
            PointerToLinenumbers = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "PointerToLinenumbers");
            NumberOfRelocations = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "NumberOfRelocations");
            NumberOfLinenumbers = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "NumberOfLinenumbers");
            Characteristics = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Characteristics");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return Name;
            yield return VirtualSize;
            yield return VirtualAddress;
            yield return SizeOfRawData;
            yield return PointerToRawData;
            yield return PointerToRelocations;
            yield return PointerToLinenumbers;
            yield return NumberOfRelocations;
            yield return NumberOfLinenumbers;
            yield return Characteristics;
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(Name.Name, "Name of the section");
            AddDescriptionEntry(VirtualSize.Name, "Actual (unaligned) size in bytes of the code or data in this section");
            AddDescriptionEntry(VirtualAddress.Name, "RVA of section start");
            AddDescriptionEntry(SizeOfRawData.Name, "Size in bytes of the initialized data on disk, rounded up to a multiple of the FileAlignment");
            AddDescriptionEntry(PointerToRawData.Name, "File offset of section start. (Multiple of the FileAlignment)");
            AddDescriptionEntry(PointerToRelocations.Name, "File offset of relocation entries");
            AddDescriptionEntry(PointerToLinenumbers.Name, "File offset of line-number entries");
            //AddDescriptionEntry(NumberOfRelocations.Name, "Number of relocations");
            //AddDescriptionEntry(NumberOfLinenumbers.Name, "Number of line numbers");
            AddDescriptionEntry(Characteristics.Name, "Characteristics of a section");
        }

        public override string ToString()
        {
            return new StringBuilder().Append(Name.Value).Append(" (").Append(index).Append(")").ToString();
        }

    }
}
