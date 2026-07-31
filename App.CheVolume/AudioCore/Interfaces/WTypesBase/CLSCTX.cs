// https://learn.microsoft.com/en-us/windows/win32/api/wtypesbase/ne-wtypesbase-clsctx
namespace AudioCore.Interfaces
{
  [Flags]
  public enum CLSCTX
  {
    INPROC_SERVER          = 0x00001,
    INPROC_HANDLER         = 0x00002,
    LOCAL_SERVER           = 0x00004,
    INPROC_SERVER16        = 0x00008,
    REMOTE_SERVER          = 0x00010,
    INPROC_HANDLER16       = 0x00020,
    NO_CODE_DOWNLOAD       = 0x00400,
    NO_CUSTOM_MARSHAL      = 0x01000,
    ENABLE_CODE_DOWNLOAD   = 0x02000,
    NO_FAILURE_LOG         = 0x04000,
    DISABLE_AAA            = 0x08000,
    ENABLE_AAA             = 0x10000,
    FROM_DEFAULT_CONTEXT   = 0x20000,
    ACTIVATE_32_BIT_SERVER = 0x40000,
    ACTIVATE_64_BIT_SERVER = 0x80000,
    ENABLE_CLOAKING        = 0x100000,
    PS_DLL                 = int.MinValue,
    INPROC                 = INPROC_SERVER | INPROC_HANDLER,
    SERVER                 = INPROC_SERVER | LOCAL_SERVER | REMOTE_SERVER,
    ALL                    = SERVER | INPROC_HANDLER
  }
}