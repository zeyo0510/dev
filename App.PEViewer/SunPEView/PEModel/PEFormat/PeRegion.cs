using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    class PeRegion
    {
        public uint Length { get; private set; }

        public PeRegion(uint length)
        {
            Length = length;
        }
    }
}
