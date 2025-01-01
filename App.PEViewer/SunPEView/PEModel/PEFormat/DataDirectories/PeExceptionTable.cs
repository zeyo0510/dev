using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SunPEView.PEModel.PEFormat.DataDirectories
{
    /*
    For 32-bit MIPS images, function table entries have the following format. 
    Offset	Size	Field	            Description
      0	    4	    Begin Address	    The VA of the corresponding function.
      4	    4	    End Address	        The VA of the end of the function.
      8	    4	    Exception Handler	The pointer to the exception handler to be executed.
    12	    4	    Handler Data	    The pointer to additional information to be passed to the handler.
    16	    4	    Prolog End Address	The VA of the end of the function’s prolog.

    For the ARM, PowerPC, SH3 and SH4 Windows CE platforms, function table entries have the following format.
    Offset	Size	Field	        Description
    0	    4	    Begin Address	The VA of the corresponding function.
    4	    8 bits	Prolog Length	The number of instructions in the function’s prolog.
    4	    22 bits	Function Length	The number of instructions in the function.
    4	    1 bit	32-bit Flag 	If set, the function consists of 32-bit instructions. 
                    If clear, the function consists of 16-bit instructions.
    4	    1 bit	Exception Flag	If set, an exception handler exists for the function. 
                    Otherwise, no exception handler exists.

    For x64 and Itanium platforms, function table entries have the following format.
    Offset	Size	Field	            Description
    0	    4	    Begin Address	    The RVA of the corresponding function.
    4	    4	    End Address	        The RVA of the end of the function.
    8	    4	    Unwind Information	The RVA of the unwind information.
     
    For the ARMv7 platform, function table entries have the following format.
    Offset	Size    Field	            Description
    0	    4	    Begin Address	    The RVA of the corresponding function.
    4	    4	    Unwind Information	The RVA of the unwind information, including function length. 
                    If the low 2 bits are non-zero, then this word represents a compacted inline form of the 
                    unwind information, including function length.
    */
    class PeExceptionTable
    {
        private PEFile peFile;
        private long exceptionDirOffset;

        private List<PeExcepFuncEntryIA3264> funcEntries;

        public PeExceptionTable(PEFile pEFile, long exceptionDirOffset)
        {
            this.peFile = pEFile;
            this.exceptionDirOffset = exceptionDirOffset;
        }

        /// <summary>
        /// Read the whole exception table.
        /// </summary>
        /// <param name="fDataReader"></param>
        public void Read(IFileAccess fDataReader)
        {
            fDataReader.PushAddress();
            fDataReader.Position = (uint)exceptionDirOffset;

            // get size of whole exception table
            uint tableSize = peFile.PeDataDirectoryTable[Enums.EDataDirectory.IMAGE_DIRECTORY_ENTRY_EXCEPTION].Size.Value;
            // number of function entries
            uint NumberOfEntries = tableSize / 12;

            funcEntries = new List<PeExcepFuncEntryIA3264>();
            for (int index = 0; index < NumberOfEntries; index++)
            {
                PeExcepFuncEntryIA3264 entry = new PeExcepFuncEntryIA3264(fDataReader);
                funcEntries.Add(entry);
            }
            fDataReader.PopAddress();
        }

        /// <summary>
        /// Get a list of all function entries of the exception table.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<PeExcepFuncEntryIA3264> GetEntries()
        {
            if (funcEntries == null)
            {
                Read(peFile.FileData);
            }
            return new ReadOnlyCollection<PeExcepFuncEntryIA3264>(funcEntries);
        }

    }

    /// <summary>
    /// Represents one X86/X64 exception entry.
    /// </summary>
    class PeExcepFuncEntryIA3264
    {
        private static readonly string TOOLTIP = "X86/X64 Exception entry information";

        public PeElem<uint> BeginAddress { get; private set; }
        public PeElem<uint> EndAddress { get; private set; }
        /* Extended info about the unwindinfo can be found in 
         * "IA-64 Software Conventions and Runtime Architecture Guide" chapter 11 */
        public PeElem<uint> UnwindInfoAddress { get; private set; }

        /// <summary>
        /// Read one exception entry from table. Position of data reader must be already set to the correct position.
        /// </summary>
        /// <param name="fDataReader"></param>
        public PeExcepFuncEntryIA3264(IFileAccess fDataReader)
        {
            BeginAddress = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Function Begin Address");
            EndAddress = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Function End Address");
            UnwindInfoAddress = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Function UnwindInfoAddress");
        }

        public override string ToString()
        {
            return TOOLTIP;
        }
    }
}
