using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Enums
{
    enum EDebugType
    {
        /* values taken from WinNT.h */

        IMAGE_DEBUG_TYPE_UNKNOWN          = 0,
        IMAGE_DEBUG_TYPE_COFF             = 1,
        IMAGE_DEBUG_TYPE_CODEVIEW         = 2,
        IMAGE_DEBUG_TYPE_FPO              = 3,
        IMAGE_DEBUG_TYPE_MISC             = 4,
        IMAGE_DEBUG_TYPE_EXCEPTION        = 5,
        IMAGE_DEBUG_TYPE_FIXUP            = 6,
        IMAGE_DEBUG_TYPE_OMAP_TO_SRC      = 7,
        IMAGE_DEBUG_TYPE_OMAP_FROM_SRC    = 8,
        IMAGE_DEBUG_TYPE_BORLAND          = 9,
        IMAGE_DEBUG_TYPE_RESERVED10       = 10,
        IMAGE_DEBUG_TYPE_CLSID            = 11
    }
}
