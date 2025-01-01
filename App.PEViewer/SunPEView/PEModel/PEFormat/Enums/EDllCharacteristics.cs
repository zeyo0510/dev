using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Enums
{
    class EDllCharacteristics : EnumBitFlagsGeneric
    {
        public static readonly EDllCharacteristics DLLCHARACTERISTICS_DYNAMIC_BASE           = new EDllCharacteristics(0x0040, "DLLCHARACTERISTICS_DYNAMIC_BASE"          , "DLL can move.");
        public static readonly EDllCharacteristics DLLCHARACTERISTICS_FORCE_INTEGRITY        = new EDllCharacteristics(0x0080, "DLLCHARACTERISTICS_FORCE_INTEGRITY"       , "Code Integrity Image");
        public static readonly EDllCharacteristics DLLCHARACTERISTICS_NX_COMPAT              = new EDllCharacteristics(0x0100, "DLLCHARACTERISTICS_NX_COMPAT"             , "Image is NX compatible");
        public static readonly EDllCharacteristics DLLCHARACTERISTICS_NO_ISOLATION           = new EDllCharacteristics(0x0200, "DLLCHARACTERISTICS_NO_ISOLATION"          , "Image understands isolation and doesn't want it");
        public static readonly EDllCharacteristics DLLCHARACTERISTICS_NO_SEH                 = new EDllCharacteristics(0x0400, "DLLCHARACTERISTICS_NO_SEH"                , "Image does not use SEH.  No SE handler may reside in this image");
        public static readonly EDllCharacteristics DLLCHARACTERISTICS_NO_BIND                = new EDllCharacteristics(0x0800, "DLLCHARACTERISTICS_NO_BIND"               , "Do not bind this image.");
        public static readonly EDllCharacteristics DLLCHARACTERISTICS_WDM_DRIVER             = new EDllCharacteristics(0x2000, "DLLCHARACTERISTICS_WDM_DRIVER"            , "Driver uses WDM model");
        public static readonly EDllCharacteristics DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE  = new EDllCharacteristics(0x8000, "DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE" , "Terminal server makes no changes to application. Application must not rely on INI files nor write to the HKEY_CURRENT_USER registry during setup");

        public EDllCharacteristics(long value, string name, string description)
            : base(value, name, description)
        {
        }

        public override IEnumerable<EnumBitFlagsGeneric> GetValues()
        {
            yield return DLLCHARACTERISTICS_DYNAMIC_BASE;
            yield return DLLCHARACTERISTICS_FORCE_INTEGRITY;
            yield return DLLCHARACTERISTICS_NX_COMPAT;
            yield return DLLCHARACTERISTICS_NO_ISOLATION;
            yield return DLLCHARACTERISTICS_NO_SEH;
            yield return DLLCHARACTERISTICS_NO_BIND;
            yield return DLLCHARACTERISTICS_WDM_DRIVER;
            yield return DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE;
        }
    }
}
