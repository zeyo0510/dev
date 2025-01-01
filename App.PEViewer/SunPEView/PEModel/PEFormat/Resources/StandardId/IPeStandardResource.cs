using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    interface IPeStandardResource
    {
        void Read(IFileAccess fDataReader, long fileOffset, long length);

        /// <summary>
        /// the actual value as general object
        /// </summary>
        //Object ObjectValue { get; }
    }
}
