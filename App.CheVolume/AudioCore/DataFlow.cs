using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  public enum DataFlow
  {
    Render  = EDataFlow._EDATAFLOW_ERENDER_,
    Capture = EDataFlow._EDATAFLOW_ECAPTURE_,
    All     = EDataFlow._EDATAFLOW_EALL_,
  }
}