using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    class PeDataDirectory
    {
        private PEFile peFile;

        public PeElem<uint> VirtualAddress { get; private set; }
        public PeElem<uint> Size { get; private set; }

        public PeDataDirectory(PEFile peFile)
        {
            this.peFile = peFile;
        }

        public void Read(IFileAccess fDataReader, string dirName)
        {
            VirtualAddress = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), dirName + " RVA");
            Size = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), dirName + " Size");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return VirtualAddress;
            yield return Size;
        }

        /// <summary>
        /// Returns true if the data directory has a valid VirtualAddress and a valid Size.
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            if (peFile.FileLocationCalculator == null)
            {
                return (this.VirtualAddress.Value != 0);
            }
            else
            {
                /* check also the RVA value if FLC is available */
                long fileOffset = peFile.FileLocationCalculator.GetFileOffsetFromRVA(VirtualAddress.Value);
                return (VirtualAddress.Value != 0 && (fileOffset + Size.Value) < peFile.FileSize);
            }
        }
    }
}
