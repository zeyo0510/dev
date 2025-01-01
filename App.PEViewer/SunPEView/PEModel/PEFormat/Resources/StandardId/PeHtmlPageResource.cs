using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    /// <summary>
    /// Represents a HTML page from a PE resource.
    /// The html page is just stored as a large ascii string.
    /// </summary>
    class PeHtmlPageResource : IPeStandardResource
    {
        private PEFile peFile;

        /// <summary>
        /// Holds the complete html page string
        /// </summary>
        public string HtmlPageStr { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        public PeHtmlPageResource(PEFile peFile)
        {
            this.peFile = peFile;
        }

        /// <summary>
        /// Read html ascii string from file.
        /// </summary>
        /// <param name="fDataReader"></param>
        /// <param name="fileOffset"></param>
        /// <param name="length"></param>
        public void Read(IFileAccess fDataReader, long fileOffset, long length)
        {
            HtmlPageStr = fDataReader.ReadAscii((uint)fileOffset, (uint)length);
        }
    }
}
