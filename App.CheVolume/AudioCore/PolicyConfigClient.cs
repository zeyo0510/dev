using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
  public class PolicyConfigClient
  {
    public void SetDefaultEndpoint(string P_0, ERole role)
    {
      PolicyConfigClient_ policyConfigClient_ = new PolicyConfigClient_();
      IPolicyConfigVista policyConfigVista = policyConfigClient_ as IPolicyConfigVista;
      int num;
      if (policyConfigVista != null)
      {
        num = policyConfigVista.SetDefaultEndpoint(P_0, role);
      }
      else
      {
        IPolicyConfig2 policyConfig = policyConfigClient_ as IPolicyConfig2;
        num = ((policyConfig == null) ? ((IPolicyConfig3)policyConfigClient_).SetDefaultEndpoint(P_0, role) : policyConfig.SetDefaultEndpoint(P_0, role));
      }
      if (num != 0)
      {
        throw Marshal.GetExceptionForHR(num);
      }
    }
  }
}