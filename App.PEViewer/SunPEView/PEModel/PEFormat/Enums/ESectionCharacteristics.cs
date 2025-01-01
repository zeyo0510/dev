using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Enums
{
    class ESectionCharacteristics : EnumBitFlagsGeneric
    {
        public static readonly ESectionCharacteristics IMAGE_SCN_TYPE_REG              = new ESectionCharacteristics(0x00000000, "IMAGE_SCN_TYPE_REG"              , "Reserved for future use");
        public static readonly ESectionCharacteristics IMAGE_SCN_TYPE_DSECT            = new ESectionCharacteristics(0x00000001, "IMAGE_SCN_TYPE_DSECT"            , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_TYPE_NOLOAD           = new ESectionCharacteristics(0x00000002, "IMAGE_SCN_TYPE_NOLOAD"           , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_TYPE_GROUP            = new ESectionCharacteristics(0x00000004, "IMAGE_SCN_TYPE_GROUP"            , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_TYPE_NO_PAD           = new ESectionCharacteristics(0x00000008, "IMAGE_SCN_TYPE_NO_PAD"           , "Section should not be padded to next boundary. This is obsolete and replaced by IMAGE_SCN_ALIGN_1BYTES. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_TYPE_COPY             = new ESectionCharacteristics(0x00000010, "IMAGE_SCN_TYPE_COPY"             , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_CNT_CODE              = new ESectionCharacteristics(0x00000020, "IMAGE_SCN_CNT_CODE"              , "Section contains executable code.");
        public static readonly ESectionCharacteristics IMAGE_SCN_CNT_INITIALIZED_DATA  = new ESectionCharacteristics(0x00000040, "IMAGE_SCN_CNT_INITIALIZED_DATA"  , "Section contains initialized data.");
        public static readonly ESectionCharacteristics IMAGE_SCN_CNT_UNINITIALIZED_DATA= new ESectionCharacteristics(0x00000080, "IMAGE_SCN_CNT_UNINITIALIZED_DATA", "Section contains uninitialized data.");
        public static readonly ESectionCharacteristics IMAGE_SCN_LNK_OTHER             = new ESectionCharacteristics(0x00000100, "IMAGE_SCN_LNK_OTHER"             , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_LNK_INFO              = new ESectionCharacteristics(0x00000200, "IMAGE_SCN_LNK_INFO"              , "Section contains comments or other information. The .drectve section has this type. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_TYPE_OVER             = new ESectionCharacteristics(0x00000400, "IMAGE_SCN_TYPE_OVER"             , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_LNK_REMOVE            = new ESectionCharacteristics(0x00000800, "IMAGE_SCN_LNK_REMOVE"            , "Section will not become part of the image. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_LNK_COMDAT            = new ESectionCharacteristics(0x00001000, "IMAGE_SCN_LNK_COMDAT"            , "Section contains COMDAT data. See Section 5.5.6, “COMDAT Sections,” for more information. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_FARDATA           = new ESectionCharacteristics(0x00008000, "IMAGE_SCN_MEM_FARDATA"           , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_PURGEABLE         = new ESectionCharacteristics(0x00020000, "IMAGE_SCN_MEM_PURGEABLE"         , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_16BIT             = new ESectionCharacteristics(0x00020000, "IMAGE_SCN_MEM_16BIT"             , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_LOCKED            = new ESectionCharacteristics(0x00040000, "IMAGE_SCN_MEM_LOCKED"            , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_PRELOAD           = new ESectionCharacteristics(0x00080000, "IMAGE_SCN_MEM_PRELOAD"           , "Reserved for future use.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_1BYTES          = new ESectionCharacteristics(0x00100000, "IMAGE_SCN_ALIGN_1BYTES"          , "Align data on a 1-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_2BYTES          = new ESectionCharacteristics(0x00200000, "IMAGE_SCN_ALIGN_2BYTES"          , "Align data on a 2-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_4BYTES          = new ESectionCharacteristics(0x00300000, "IMAGE_SCN_ALIGN_4BYTES"          , "Align data on a 4-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_8BYTES          = new ESectionCharacteristics(0x00400000, "IMAGE_SCN_ALIGN_8BYTES"          , "Align data on a 8-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_16BYTES         = new ESectionCharacteristics(0x00500000, "IMAGE_SCN_ALIGN_16BYTES"         , "Align data on a 16-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_32BYTES         = new ESectionCharacteristics(0x00600000, "IMAGE_SCN_ALIGN_32BYTES"         , "Align data on a 32-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_64BYTES         = new ESectionCharacteristics(0x00700000, "IMAGE_SCN_ALIGN_64BYTES"         , "Align data on a 64-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_128BYTES        = new ESectionCharacteristics(0x00800000, "IMAGE_SCN_ALIGN_128BYTES"        , "Align data on a 128-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_256BYTES        = new ESectionCharacteristics(0x00900000, "IMAGE_SCN_ALIGN_256BYTES"        , "Align data on a 256-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_512BYTES        = new ESectionCharacteristics(0x00A00000, "IMAGE_SCN_ALIGN_512BYTES"        , "Align data on a 512-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_1024BYTES       = new ESectionCharacteristics(0x00B00000, "IMAGE_SCN_ALIGN_1024BYTES"       , "Align data on a 1024-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_2048BYTES       = new ESectionCharacteristics(0x00C00000, "IMAGE_SCN_ALIGN_2048BYTES"       , "Align data on a 2048-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_4096BYTES       = new ESectionCharacteristics(0x00D00000, "IMAGE_SCN_ALIGN_4096BYTES"       , "Align data on a 4096-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_ALIGN_8192BYTES       = new ESectionCharacteristics(0x00E00000, "IMAGE_SCN_ALIGN_8192BYTES"       , "Align data on a 8192-byte boundary. This is valid for object files only.");
        public static readonly ESectionCharacteristics IMAGE_SCN_LNK_NRELOC_OVFL       = new ESectionCharacteristics(0x01000000, "IMAGE_SCN_LNK_NRELOC_OVFL"       , "Section contains extended relocations.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_DISCARDABLE       = new ESectionCharacteristics(0x02000000, "IMAGE_SCN_MEM_DISCARDABLE"       , "Section can be discarded as needed.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_NOT_CACHED        = new ESectionCharacteristics(0x04000000, "IMAGE_SCN_MEM_NOT_CACHED"        , "Section cannot be cached.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_NOT_PAGED         = new ESectionCharacteristics(0x08000000, "IMAGE_SCN_MEM_NOT_PAGED"         , "Section is not pageable.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_SHARED            = new ESectionCharacteristics(0x10000000, "IMAGE_SCN_MEM_SHARED"            , "Section can be shared in memory.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_EXECUTE           = new ESectionCharacteristics(0x20000000, "IMAGE_SCN_MEM_EXECUTE"           , "Section can be executed as code.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_READ              = new ESectionCharacteristics(0x40000000, "IMAGE_SCN_MEM_READ"              , "Section can be read.");
        public static readonly ESectionCharacteristics IMAGE_SCN_MEM_WRITE             = new ESectionCharacteristics(0x80000000, "IMAGE_SCN_MEM_WRITE"             , "Section can be written to.");
        

        public ESectionCharacteristics(long value, string name, string description) : base(value, name, description)
        {
        }
        
        public override IEnumerable<EnumBitFlagsGeneric> GetValues()
        {
                yield return IMAGE_SCN_TYPE_REG;
                yield return IMAGE_SCN_TYPE_DSECT;
                yield return IMAGE_SCN_TYPE_NOLOAD;
                yield return IMAGE_SCN_TYPE_GROUP;
                yield return IMAGE_SCN_TYPE_NO_PAD;
                yield return IMAGE_SCN_TYPE_COPY;
                yield return IMAGE_SCN_CNT_CODE;
                yield return IMAGE_SCN_CNT_INITIALIZED_DATA;
                yield return IMAGE_SCN_CNT_UNINITIALIZED_DATA;
                yield return IMAGE_SCN_LNK_OTHER;
                yield return IMAGE_SCN_LNK_INFO;
                yield return IMAGE_SCN_TYPE_OVER;
                yield return IMAGE_SCN_LNK_REMOVE;
                yield return IMAGE_SCN_LNK_COMDAT;
                yield return IMAGE_SCN_MEM_FARDATA;
                yield return IMAGE_SCN_MEM_PURGEABLE;
                yield return IMAGE_SCN_MEM_16BIT;
                yield return IMAGE_SCN_MEM_LOCKED;
                yield return IMAGE_SCN_MEM_PRELOAD;
                yield return IMAGE_SCN_ALIGN_1BYTES;
                yield return IMAGE_SCN_ALIGN_2BYTES;
                yield return IMAGE_SCN_ALIGN_4BYTES;
                yield return IMAGE_SCN_ALIGN_8BYTES;
                yield return IMAGE_SCN_ALIGN_16BYTES;
                yield return IMAGE_SCN_ALIGN_32BYTES;
                yield return IMAGE_SCN_ALIGN_64BYTES;
                yield return IMAGE_SCN_ALIGN_128BYTES;
                yield return IMAGE_SCN_ALIGN_256BYTES;
                yield return IMAGE_SCN_ALIGN_512BYTES;
                yield return IMAGE_SCN_ALIGN_1024BYTES;
                yield return IMAGE_SCN_ALIGN_2048BYTES;
                yield return IMAGE_SCN_ALIGN_4096BYTES;
                yield return IMAGE_SCN_ALIGN_8192BYTES;
                yield return IMAGE_SCN_LNK_NRELOC_OVFL;
                yield return IMAGE_SCN_MEM_DISCARDABLE;
                yield return IMAGE_SCN_MEM_NOT_CACHED;
                yield return IMAGE_SCN_MEM_NOT_PAGED;
                yield return IMAGE_SCN_MEM_SHARED;
                yield return IMAGE_SCN_MEM_EXECUTE;
                yield return IMAGE_SCN_MEM_READ;
                yield return IMAGE_SCN_MEM_WRITE;
        }

        public static IEnumerable<ESectionCharacteristics> Values
        {
            get
            {
                yield return IMAGE_SCN_TYPE_REG;
                yield return IMAGE_SCN_TYPE_DSECT;
                yield return IMAGE_SCN_TYPE_NOLOAD;
                yield return IMAGE_SCN_TYPE_GROUP;
                yield return IMAGE_SCN_TYPE_NO_PAD;
                yield return IMAGE_SCN_TYPE_COPY;
                yield return IMAGE_SCN_CNT_CODE;
                yield return IMAGE_SCN_CNT_INITIALIZED_DATA;
                yield return IMAGE_SCN_CNT_UNINITIALIZED_DATA;
                yield return IMAGE_SCN_LNK_OTHER;
                yield return IMAGE_SCN_LNK_INFO;
                yield return IMAGE_SCN_TYPE_OVER;
                yield return IMAGE_SCN_LNK_REMOVE;
                yield return IMAGE_SCN_LNK_COMDAT;
                yield return IMAGE_SCN_MEM_FARDATA;
                yield return IMAGE_SCN_MEM_PURGEABLE;
                yield return IMAGE_SCN_MEM_16BIT;
                yield return IMAGE_SCN_MEM_LOCKED;
                yield return IMAGE_SCN_MEM_PRELOAD;
                yield return IMAGE_SCN_ALIGN_1BYTES;
                yield return IMAGE_SCN_ALIGN_2BYTES;
                yield return IMAGE_SCN_ALIGN_4BYTES;
                yield return IMAGE_SCN_ALIGN_8BYTES;
                yield return IMAGE_SCN_ALIGN_16BYTES;
                yield return IMAGE_SCN_ALIGN_32BYTES;
                yield return IMAGE_SCN_ALIGN_64BYTES;
                yield return IMAGE_SCN_ALIGN_128BYTES;
                yield return IMAGE_SCN_ALIGN_256BYTES;
                yield return IMAGE_SCN_ALIGN_512BYTES;
                yield return IMAGE_SCN_ALIGN_1024BYTES;
                yield return IMAGE_SCN_ALIGN_2048BYTES;
                yield return IMAGE_SCN_ALIGN_4096BYTES;
                yield return IMAGE_SCN_ALIGN_8192BYTES;
                yield return IMAGE_SCN_LNK_NRELOC_OVFL;
                yield return IMAGE_SCN_MEM_DISCARDABLE;
                yield return IMAGE_SCN_MEM_NOT_CACHED;
                yield return IMAGE_SCN_MEM_NOT_PAGED;
                yield return IMAGE_SCN_MEM_SHARED;
                yield return IMAGE_SCN_MEM_EXECUTE;
                yield return IMAGE_SCN_MEM_READ;
                yield return IMAGE_SCN_MEM_WRITE;
            }
        }

    }
}
