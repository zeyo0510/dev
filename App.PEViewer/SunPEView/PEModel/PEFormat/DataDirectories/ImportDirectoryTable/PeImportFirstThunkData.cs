using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.ImportDirectoryTable
{
    class PeImportFirstThunkData
    {
        /// <summary>
        /// The actual FT value. Is 4 bytes in PE32, 8 bytes value in PE64.
        /// </summary>
        public PeElem<ulong> FirstThunkRva { get; private set; }

        public PeImportFirstThunkData(ulong firstThunkRva, long fileOffset)
        {
            this.FirstThunkRva = new PeElem<ulong>(fileOffset, firstThunkRva, "FirstThunk Value");
        }
    }
}
