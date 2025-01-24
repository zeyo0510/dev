$file = Get-ChildItem -Path ${PSScriptRoot} -Filter "${env:USERNAME}.json"
$file = $file.FullName
$data = Get-Content -Path $file -Raw
$json = $data | ConvertFrom-Json
$json | ForEach-Object {
  $name  = $_.Name
  $value = $_.Value
  ##################################################
  Write-Host "Set Environment: $name($value)"
  ##################################################
  Set-ItemProperty  -Path  "HKCU:\Environment" `
                    -Name  $name               `
                    -Type  "ExpandString"      `
                    -Value $value
}
##################################################
Add-Type -TypeDefinition @"
using System;
using System.Runtime.InteropServices;

public class NativeMethods
{
  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern IntPtr SendMessageTimeout
  (
        IntPtr  hWnd,
        uint    Msg,
        UIntPtr wParam,
        string  lParam,
        uint    fuFlags,
        uint    uTimeout,
    out IntPtr  lpdwResult
  );
}
"@
##################################################
[void][NativeMethods]::SendMessageTimeout([IntPtr]0xFFFF, 0x001A, [UIntPtr]::Zero, "Environment", 0x0002, 500, [ref][IntPtr]::Zero)
##################################################
Write-Host "OK"
##################################################
# pause