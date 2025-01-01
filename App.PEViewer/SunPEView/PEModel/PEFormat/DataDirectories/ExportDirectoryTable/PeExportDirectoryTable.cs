using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SunPEView.PEModel.PEFormat.ExportDirectoryTable
{
    class PeExportDirectoryTable : AbstractPeDescriptor, IEnumerable<IPeElem>, IEnumerable<PeExportFunction>
    {
        /// <summary>
        /// Characteristics / Export flags, always 0.
        /// </summary>
        public PeElem<uint> Characteristics { get; private set; }

        /// <summary>
        /// Time and date that the export data was created
        /// </summary>
        public PeElem<uint> TimeDateStamp { get; private set; }

        public PeElem<ushort> MajorVersion { get; private set; }
        public PeElem<ushort> MinorVersion { get; private set; }

        /// <summary>
        /// The address of the ASCII string that contains the name of the DLL. This address is relative to the image base.
        /// </summary>
        public PeElem<uint> Name { get; private set; }

        /// <summary>
        /// The starting ordinal number for exports in this image. 
        /// This field specifies the starting ordinal number for the export address table. It is usually set to 1.
        /// </summary>
        public PeElem<uint> Base { get; private set; }

        /// <summary>
        /// The number of entries in the export address table.
        /// </summary>
        public PeElem<uint> NumberOfFunctions { get; private set; }

        /// <summary>
        /// The number of entries in the name pointer table. This is also the number of entries in the ordinal table.
        /// </summary>
        public PeElem<uint> NumberOfNames { get; private set; }

        /// <summary>
        /// The address of the export address table, relative to the image base.
        /// </summary>
        public PeElem<uint> AddressOfFunctions { get; private set; }

        /// <summary>
        /// The address of the export name pointer table, relative to the image base. 
        /// The table size is given by the Number of Name Pointers field.
        /// </summary>
        public PeElem<uint> AddressOfNames { get; private set; }

        /// <summary>
        /// The address of the ordinal table, relative to the image base.
        /// </summary>
        public PeElem<uint> AddressOfNameOrdinals { get; private set; }
        

        /// <summary>
        /// reference to pe file to get access to file location calculator
        /// </summary>
        private PEFile peFile;

        /// <summary>
        /// file access object
        /// </summary>
        private IFileAccess fDataReader;

        private List<PeExportFunction> exportFunctions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        public PeExportDirectoryTable(PEFile peFile)
        {
            this.peFile = peFile;
            exportFunctions = new List<PeExportFunction>();
        }

        public void Read(IFileAccess fDataReader)
        {
            this.fDataReader = fDataReader;
            Characteristics = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Export Characteristics");
            TimeDateStamp = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Export TimeDateStamp");
            MajorVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Export MajorVersion");
            MinorVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Export MinorVersion");
            Name = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Name");
            Base = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Base");
            NumberOfFunctions = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "NumberOfFunctions");
            NumberOfNames = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "NumberOfNames");
            AddressOfFunctions = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "AddressOfFunctions");
            AddressOfNames = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "AddressOfNames");
            AddressOfNameOrdinals = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "AddressOfNameOrdinals");

            fDataReader.PushAddress();
            ReadExportInformation(fDataReader);
            fDataReader.PopAddress();
        
        }

        private void ReadExportInformation(IFileAccess fDataReader)
        {
            /* first step: read all values into array */
            uint[] nameRvas = new uint[NumberOfNames.Value];
            ushort[] ordinalValues = new ushort[NumberOfNames.Value];
            
            // file offset to the begin of the array of RVAs pointing to the exported functions
            uint nameRvaArrayFileOffset = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(AddressOfNames.Value);
            // file offset to the begin of the ordinal array
            uint ordinalArrayFileOffset = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(AddressOfNameOrdinals.Value);
            
            for (int index = 0; index < NumberOfNames.Value; index++)
            {
                nameRvas[index] = fDataReader.ReadUInt32(nameRvaArrayFileOffset);
                ordinalValues[index] = fDataReader.ReadUInt16(ordinalArrayFileOffset);

                ordinalArrayFileOffset += 2;
                nameRvaArrayFileOffset += 4;
            }

            /* second step: iterate over all function ordinals and collect data */

            // file offset to the begin of the function addressess
            uint addressArrayFileOffset = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(AddressOfFunctions.Value);

            for (uint functionOrdinal = Base.Value; functionOrdinal < Base.Value + NumberOfFunctions.Value; functionOrdinal++)
            {
                uint ordinal = functionOrdinal - Base.Value;    // ordinal value in file

                // get file address value
                uint AddressValue = fDataReader.ReadUInt32(addressArrayFileOffset + 4 * ordinal);

                // find if the ordinal exists in file
                bool found = false;
                int foundIndex = -1;
                for (int ordinalArrayIndex = 0; ordinalArrayIndex < NumberOfNames.Value; ordinalArrayIndex++)
                {
                    if (ordinalValues[ordinalArrayIndex] == ordinal)
                    {
                        foundIndex = ordinalArrayIndex;
                        found = true;
                        break;
                    }
                }

                uint nameRvaValue;
                string exportFuncName;
                if (found)
                {
                    nameRvaValue = nameRvas[foundIndex];
                    uint nameAsciiFileOffset = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(nameRvaValue);
                    exportFuncName = fDataReader.ReadAscii(nameAsciiFileOffset, (uint)fDataReader.Length);
                }
                else
                {
                    nameRvaValue = 0u;
                    exportFuncName = "N/A";
                }

                PeExportFunction exportFunc = new PeExportFunction(peFile, functionOrdinal, ordinal,
                                            AddressValue, nameRvaValue, exportFuncName);
                exportFunctions.Add(exportFunc);
            }

            nameRvas = null;
            ordinalValues = null;
        }


        IEnumerator<IPeElem> IEnumerable<IPeElem>.GetEnumerator()
        {
            yield return Characteristics;
            yield return TimeDateStamp;
            yield return MajorVersion;
            yield return MinorVersion;
            yield return Name;
            yield return Base;
            yield return NumberOfFunctions;
            yield return NumberOfNames;
            yield return AddressOfFunctions;
            yield return AddressOfNames;
            yield return AddressOfNameOrdinals;
        }

        IEnumerator<PeExportFunction> IEnumerable<PeExportFunction>.GetEnumerator()
        {
            foreach (PeExportFunction exportFunc in exportFunctions)
            {
                yield return exportFunc;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.exportFunctions.GetEnumerator();
        }

        /// <summary>
        /// Returns the name of the DLL.
        /// </summary>
        /// <returns></returns>
        public string getDllName()
        {
            uint fileOffset = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(Name.Value);
            return fDataReader.ReadAscii(fileOffset, (uint)fDataReader.Length);
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(Characteristics.Name, "Characteristics / Export flags, always 0");
            AddDescriptionEntry(TimeDateStamp.Name, "Time and date that the export data was created");
            AddDescriptionEntry(MajorVersion.Name, "The major version number");
            AddDescriptionEntry(MinorVersion.Name, "The minor version number");
            AddDescriptionEntry(Name.Name, "The address of the ASCII string that contains the name of the DLL. This address is relative to the image base.");
            AddDescriptionEntry(Base.Name, "The starting ordinal number for exports in this image. This field specifies the starting ordinal number for the export address table. It is usually set to 1.");
            AddDescriptionEntry(NumberOfFunctions.Name, "The number of entries in the export address table.");
            AddDescriptionEntry(NumberOfNames.Name, "The number of entries in the name pointer table. This is also the number of entries in the ordinal table.");
            AddDescriptionEntry(AddressOfFunctions.Name, "The address of the export address table, relative to the image base.");
            AddDescriptionEntry(AddressOfNames.Name, "The address of the export name pointer table, relative to the image base. The table size is given by the Number of Name Pointers field.");
            AddDescriptionEntry(AddressOfNameOrdinals.Name, "The address of the ordinal table, relative to the image base.");
        }
    }
}
