using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Enums.Dotnet
{
    class EDotnetHeaderFlags : EnumBitFlagsGeneric
    {
        public static readonly EDotnetHeaderFlags COMIMAGE_FLAGS_ILONLY = new EDotnetHeaderFlags(0x00000001, "COMIMAGE_FLAGS_ILONLY", "");
        public static readonly EDotnetHeaderFlags COMIMAGE_FLAGS_32BITREQUIRED = new EDotnetHeaderFlags(0x00000002, "COMIMAGE_FLAGS_32BITREQUIRED", "");
        public static readonly EDotnetHeaderFlags COMIMAGE_FLAGS_IL_LIBRARY = new EDotnetHeaderFlags(0x00000004, "COMIMAGE_FLAGS_IL_LIBRARY", "");
        public static readonly EDotnetHeaderFlags COMIMAGE_FLAGS_STRONGNAMESIGNED = new EDotnetHeaderFlags(0x00000008, "COMIMAGE_FLAGS_STRONGNAMESIGNED", "");
        public static readonly EDotnetHeaderFlags COMIMAGE_FLAGS_NATIVE_ENTRYPOINT = new EDotnetHeaderFlags(0x00000010, "COMIMAGE_FLAGS_NATIVE_ENTRYPOINT", "");
        public static readonly EDotnetHeaderFlags COMIMAGE_FLAGS_TRACKDEBUGDATA = new EDotnetHeaderFlags(0x00010000, "COMIMAGE_FLAGS_TRACKDEBUGDATA", "");

        public EDotnetHeaderFlags(long value, string name, string description)
            : base(value, name, description)
        {
        }

        public override IEnumerable<EnumBitFlagsGeneric> GetValues()
        {
            yield return COMIMAGE_FLAGS_ILONLY;
            yield return COMIMAGE_FLAGS_32BITREQUIRED;
            yield return COMIMAGE_FLAGS_IL_LIBRARY;
            yield return COMIMAGE_FLAGS_STRONGNAMESIGNED;
            yield return COMIMAGE_FLAGS_NATIVE_ENTRYPOINT;
            yield return COMIMAGE_FLAGS_TRACKDEBUGDATA;
        }
    }
}
