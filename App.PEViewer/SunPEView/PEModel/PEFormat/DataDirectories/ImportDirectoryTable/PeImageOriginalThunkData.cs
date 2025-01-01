using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.Exceptions;

namespace SunPEView.PEModel.PEFormat.ImportDirectoryTable
{
    /// <summary>
    /// Represents one thunk, that is one imported function struct
    /// </summary>
    class PeImageOriginalThunkData
    {
        private readonly static uint ORDINAL_MASK_32  = 0x80000000;
        private readonly static ulong ORDINAL_MASK_64 = 0x8000000000000000ul;

        /// <summary>
        /// Hint, an index into the export name pointer table
        /// </summary>
        public PeElem<ushort> Hint { get; private set; }

        /// <summary>
        /// An ASCII string that contains the name to import
        /// </summary>
        public PeElem<string> Name { get; private set; }

        /// <summary>
        /// A 31-bit RVA of a hint/name table entry.
        /// </summary>
        public ulong ThunkValue { get; private set; }

        private PEFile peFile;

        /// <summary>
        /// file offset of beginning of thunk name struct; this is the same as the file offset of the hint value.
        /// </summary>
        private uint FileOffsetThunkHintNameStruct;

        /// <summary>
        /// file offset of the thunk data RVA.
        /// </summary>
        public uint FileOffsetThunkData { get; private set; }

        public PeImageOriginalThunkData(PEFile peFile, ulong thunkRVA, uint fileOffsetThunkData)
        {
            this.peFile = peFile;
            this.ThunkValue = thunkRVA;
            this.FileOffsetThunkData = fileOffsetThunkData;
        }

        public void Read(IFileAccess fDataReader)
        {
            if (isImportByOrdinal())
            {
                // import by ordinal
                Hint = new PeElem<ushort>(FileOffsetThunkData, 0xFFFF, "Hint");
                if (peFile.isPe64())
                {
                    Name = new PeElem<string>(0xFFFFFFFF, StringUtil.GetFormattedStringUlong(getOrdinalValue()), "Ordinal Import");
                }
                else
                {
                    Name = new PeElem<string>(0xFFFFFFFF, StringUtil.GetFormattedStringUint((uint)getOrdinalValue()), "Ordinal Import");
                }
            }
            else
            {
                FileOffsetThunkHintNameStruct = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA((long)ThunkValue);
                // import by name
                Hint = new PeElem<ushort>(FileOffsetThunkHintNameStruct, fDataReader.ReadUInt16(FileOffsetThunkHintNameStruct), "Hint");
                Name = new PeElem<string>(FileOffsetThunkHintNameStruct + 2,
                    fDataReader.ReadAscii(FileOffsetThunkHintNameStruct + 2, (uint)fDataReader.Length), "Name");
            }
        }

        /// <summary>
        /// Length of the thunk. 
        /// If it's ordinal, it's the length of the thunk value.
        /// If it's be name, it's the sum of the hint value and of the name string length.
        /// </summary>
        public uint ThunkLengthInBytes
        {
            get {
                if (isImportByOrdinal())
                {
                    return (peFile.isPe64() ? 8u : 4u);
                }
                else
                {
                    return (uint)(2 + Name.Value.Length);
                }
            }
        }

        /// <summary>
        /// Returns true if function of this thunk is imported by ordinal, otherwise false.
        /// </summary>
        /// <returns></returns>
        public bool isImportByOrdinal()
        {
            if (peFile.isPe64())
            {
                return ((ThunkValue & ORDINAL_MASK_64) != 0);
            }
            else
            {
                return ((ThunkValue & ORDINAL_MASK_32) != 0);
            }
        }

        /// <summary>
        /// Get the ordinal value. 
        /// If the import is not ordinal, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public ulong getOrdinalValue()
        {
            if (isImportByOrdinal())
            {
                if (peFile.isPe64())
                {
                    return (ThunkValue & 0x00000000FFFFFFFFul);
                }
                else
                {
                    return (ThunkValue & 0x0000FFFF);
                }
            }
            else
            {
                throw new PEModelException();
            }
        }
    }
}
