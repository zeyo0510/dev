using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    class PeHeader : AbstractPeDescriptor
    {
        /// <summary>
        /// PE signature
        /// </summary>
        public static readonly ushort PE_SIGNATURE = 0x00004550;

        public PeElem<uint> Signature { get; private set; }

        /// <summary>
        /// absolute file offset where PE header starts
        /// </summary>
        public long FileStartOffset { get; private set; }

        public void Read(IFileAccess fDataReader)
        {
            FileStartOffset = fDataReader.Position;
            Signature = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Signature");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return Signature;
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(Signature.Name, "PE header signature");
        }
    }
}
