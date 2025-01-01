using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Enums
{
    enum EOptionalHeaderMagic
    {
        IMAGE_NT_OPTIONAL_HDR32_MAGIC       = 0x010B,
        IMAGE_NT_OPTIONAL_HDR64_MAGIC       = 0x020B,
        IMAGE_ROM_OPTIONAL_HDR_MAGIC        = 0x0107
    }
}
