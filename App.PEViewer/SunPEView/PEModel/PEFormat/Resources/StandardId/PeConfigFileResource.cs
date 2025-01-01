using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    class PeConfigFileResource : IPeStandardResource
    {
        /// <summary>
        /// The Unicode string L"VS_VERSION_INFO". 
        /// </summary>
        public PeElem<string> fileContentStr { get; private set; }

        public void Read(IFileAccess fDataReader, long fileOffset, long length)
        {
            fDataReader.Position = (uint)fileOffset;
            fileContentStr = new PeElem<string>(fDataReader.Position, 
                fDataReader.ReadAscii((uint)length), "File Content");
        }
    }
}
