using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.DataDirectories
{
    class PeTLSDirectoryTable : AbstractPeDescriptor
    {
        public PeElem<uint> StartAddressOfRawData32 { get; private set; }
        public PeElem<ulong> StartAddressOfRawData64 { get; private set; }

        public PeElem<uint> EndAddressOfRawData32 { get; private set; }
        public PeElem<ulong> EndAddressOfRawData64 { get; private set; }

        public PeElem<uint> AddressOfIndex32 { get; private set; }
        public PeElem<ulong> AddressOfIndex64 { get; private set; }

        public PeElem<uint> AddressOfCallBacks32 { get; private set; }
        public PeElem<ulong> AddressOfCallBacks64 { get; private set; }

        public PeElem<uint> SizeOfZeroFill { get; private set; }
        public PeElem<uint> Characteristics { get; private set; }

        private PEFile peFile;
        private long tlsDirOffset;        /* file offset of start of TLS table */

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="peFile"></param>
        /// <param name="relocDirOffset"></param>
        public PeTLSDirectoryTable(PEFile peFile, long tlsDirOffset)
        {
            this.peFile = peFile;
            this.tlsDirOffset = tlsDirOffset;
        }

        /// <summary>
        /// Read the TLS directory from file
        /// </summary>
        /// <param name="fDataReader"></param>
        private void Read(IFileAccess fDataReader)
        {
            fDataReader.PushAddress();
            fDataReader.Position = (uint)tlsDirOffset;

            if (peFile.isPe32())
            {
                StartAddressOfRawData32 = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "StartAddressOfRawData");
                EndAddressOfRawData32 = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "EndAddressOfRawData");
                AddressOfIndex32 = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "AddressOfIndex");
                AddressOfCallBacks32 = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "AddressOfCallBacks");
            }
            else if (peFile.isPe64())
            {
                StartAddressOfRawData64 = new PeElem<ulong>(fDataReader.Position, fDataReader.ReadUInt64(), "StartAddressOfRawData");
                EndAddressOfRawData64 = new PeElem<ulong>(fDataReader.Position, fDataReader.ReadUInt64(), "EndAddressOfRawData");
                AddressOfIndex64 = new PeElem<ulong>(fDataReader.Position, fDataReader.ReadUInt64(), "AddressOfIndex");
                AddressOfCallBacks64 = new PeElem<ulong>(fDataReader.Position, fDataReader.ReadUInt64(), "AddressOfCallBacks");
            }
            else
            {
                //throw new NotImplementedException();
            }

            SizeOfZeroFill = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfZeroFill");
            Characteristics = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Characteristics");

            fDataReader.PopAddress();
        }

        /// <summary>
        /// Get all elements
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IPeElem> GetEnumerator()
        {
            if (SizeOfZeroFill == null)
            {   /* lazy loading */
                Read(peFile.FileData);
            }

            if (peFile.isPe32())
            {
                yield return StartAddressOfRawData32;
                yield return EndAddressOfRawData32;
                yield return AddressOfIndex32;
                yield return AddressOfCallBacks32;
            }
            else if (peFile.isPe64())
            {
                yield return StartAddressOfRawData64;
                yield return EndAddressOfRawData64;
                yield return AddressOfIndex64;
                yield return AddressOfCallBacks64;
            }

            yield return SizeOfZeroFill;
            yield return Characteristics;
        }

        protected override void AddDescriptions()
        {
            if (peFile.isPe32())
            {
                AddDescriptionEntry(StartAddressOfRawData32.Name, "Starting address of the TLS template. The template is a block of data used to initialize TLS data");
                AddDescriptionEntry(EndAddressOfRawData32.Name, "Address of the last byte of the TLS, except for the zero fill.");
                AddDescriptionEntry(AddressOfIndex32.Name, "Location to receive the TLS index, which the loader assigns.");
                AddDescriptionEntry(AddressOfCallBacks32.Name, "Pointer to an array of TLS callback functions.");
            }
            else if (peFile.isPe64())
            {
                AddDescriptionEntry(StartAddressOfRawData64.Name, "Starting address of the TLS template. The template is a block of data used to initialize TLS data");
                AddDescriptionEntry(EndAddressOfRawData64.Name, "Address of the last byte of the TLS, except for the zero fill.");
                AddDescriptionEntry(AddressOfIndex64.Name, "Location to receive the TLS index, which the loader assigns.");
                AddDescriptionEntry(AddressOfCallBacks64.Name, "Pointer to an array of TLS callback functions.");
            }
            AddDescriptionEntry(SizeOfZeroFill.Name, "The size in bytes of the template, beyond the initialized data delimited by Raw Data Start VA and Raw Data End VA. The total template size should be the same as the total size of TLS data in the image file.");
            AddDescriptionEntry(Characteristics.Name, "Reserved for possible future use by TLS flags.");
        }
    }
}
