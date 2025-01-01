using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Enums
{
    class ECharacteristics : EnumBitFlagsGeneric
    {
        /* values taken from WinNT.h */

        public static readonly ECharacteristics IMAGE_FILE_RELOCS_STRIPPED          = new ECharacteristics(0x0001, "IMAGE_FILE_RELOCS_STRIPPED"          ,"Relocation info stripped from file.");
        public static readonly ECharacteristics IMAGE_FILE_EXECUTABLE_IMAGE         = new ECharacteristics(0x0002, "IMAGE_FILE_EXECUTABLE_IMAGE"         ,"File is executable  (i.e. no unresolved externel references).");
        public static readonly ECharacteristics IMAGE_FILE_LINE_NUMS_STRIPPED       = new ECharacteristics(0x0004, "IMAGE_FILE_LINE_NUMS_STRIPPED"       ,"Line nunbers stripped from file.");
        public static readonly ECharacteristics IMAGE_FILE_LOCAL_SYMS_STRIPPED      = new ECharacteristics(0x0008, "IMAGE_FILE_LOCAL_SYMS_STRIPPED"      ,"Local symbols stripped from file.");
        public static readonly ECharacteristics IMAGE_FILE_AGGRESIVE_WS_TRIM        = new ECharacteristics(0x0010, "IMAGE_FILE_AGGRESIVE_WS_TRIM"        ,"Agressively trim working set");
        public static readonly ECharacteristics IMAGE_FILE_LARGE_ADDRESS_AWARE      = new ECharacteristics(0x0020, "IMAGE_FILE_LARGE_ADDRESS_AWARE"      ,"App can handle >2gb addresses");
        public static readonly ECharacteristics IMAGE_FILE_BYTES_REVERSED_LO        = new ECharacteristics(0x0080, "IMAGE_FILE_BYTES_REVERSED_LO"        ,"Bytes of machine word are reversed.");
        public static readonly ECharacteristics IMAGE_FILE_32BIT_MACHINE            = new ECharacteristics(0x0100, "IMAGE_FILE_32BIT_MACHINE"            ,"32 bit word machine.");
        public static readonly ECharacteristics IMAGE_FILE_DEBUG_STRIPPED           = new ECharacteristics(0x0200, "IMAGE_FILE_DEBUG_STRIPPED"           ,"Debugging info stripped from file in .DBG file");
        public static readonly ECharacteristics IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP  = new ECharacteristics(0x0400, "IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP"  ,"If Image is on removable media, copy and run from the swap file.");
        public static readonly ECharacteristics IMAGE_FILE_NET_RUN_FROM_SWAP        = new ECharacteristics(0x0800, "IMAGE_FILE_NET_RUN_FROM_SWAP"        ,"If Image is on Net, copy and run from the swap file.");
        public static readonly ECharacteristics IMAGE_FILE_SYSTEM                   = new ECharacteristics(0x1000, "IMAGE_FILE_SYSTEM"                   ,"System File.");
        public static readonly ECharacteristics IMAGE_FILE_DLL                      = new ECharacteristics(0x2000, "IMAGE_FILE_DLL"                      ,"File is a DLL.");
        public static readonly ECharacteristics IMAGE_FILE_UP_SYSTEM_ONLY           = new ECharacteristics(0x4000, "IMAGE_FILE_UP_SYSTEM_ONLY"           ,"File should only be run on a UP machine");
        public static readonly ECharacteristics IMAGE_FILE_BYTES_REVERSED_HI        = new ECharacteristics(0x8000, "IMAGE_FILE_BYTES_REVERSED_HI"        ,"Bytes of machine word are reversed.");

        public ECharacteristics(long value, string name, string description)
            : base(value, name, description)
        {
        }

        public override IEnumerable<EnumBitFlagsGeneric> GetValues()
        {
            yield return IMAGE_FILE_RELOCS_STRIPPED;
            yield return IMAGE_FILE_EXECUTABLE_IMAGE;
            yield return IMAGE_FILE_LINE_NUMS_STRIPPED;
            yield return IMAGE_FILE_LOCAL_SYMS_STRIPPED;
            yield return IMAGE_FILE_AGGRESIVE_WS_TRIM;
            yield return IMAGE_FILE_LARGE_ADDRESS_AWARE;
            yield return IMAGE_FILE_BYTES_REVERSED_LO;
            yield return IMAGE_FILE_32BIT_MACHINE;
            yield return IMAGE_FILE_DEBUG_STRIPPED;
            yield return IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP;
            yield return IMAGE_FILE_NET_RUN_FROM_SWAP;
            yield return IMAGE_FILE_SYSTEM;
            yield return IMAGE_FILE_DLL;
            yield return IMAGE_FILE_UP_SYSTEM_ONLY;
            yield return IMAGE_FILE_BYTES_REVERSED_HI;
        }
    }
}
