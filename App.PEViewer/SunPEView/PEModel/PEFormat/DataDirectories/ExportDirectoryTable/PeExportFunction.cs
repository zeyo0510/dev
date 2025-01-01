using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.ExportDirectoryTable
{
    class PeExportFunction
    {
        /// <summary>
        /// ordinal of this function from outside view, so including the base
        /// </summary>
        public uint FunctionOrdinal { get; private set; }

        /// <summary>
        /// ordinal value of this function in file
        /// </summary>
        public uint FileOrdinal { get; private set; }

        /// <summary>
        /// Address (Rva) of function.
        /// </summary>
        public uint AddressValue { get; private set; }

        /// <summary>
        /// function name of this export. May not exist.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Rva which points to the function name.
        /// </summary>
        public uint NameRva { get; private set; }

        /// <summary>
        /// reference to pe file to get access to file location calculator
        /// </summary>
        private PEFile peFile;

        public PeExportFunction(PEFile peFile,
                                uint functionOrdinal,
                                uint fileOrdinal,
                                uint functionAddress,   
                                uint nameRva,
                                string functionName)
        {
            this.peFile = peFile;
            this.FunctionOrdinal = functionOrdinal;
            this.FileOrdinal = fileOrdinal;
            this.AddressValue = functionAddress;
            this.NameRva = nameRva;
            this.Name = functionName;
        }

        public uint getAsciiNameFileOffset()
        {
            return (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(NameRva);
        }
    }
}
