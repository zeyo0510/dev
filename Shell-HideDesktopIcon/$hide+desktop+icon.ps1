$files = Get-ChildItem -Path . -Filter "*.json"
##################################################
$files | ForEach-Object {
  $file = $_.FullName
  $data = Get-Content -Path $file -Raw
  $json = $data | ConvertFrom-Json
  $json | ForEach-Object {
    $code  = $_.Code
    $name  = $_.Name
    $value = $_.Value
    ##################################################
    Write-Host "Set Desktop Icon: $name = $value"
    ##################################################
    Set-ItemProperty  -Path  "HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel" `
                      -Name  $code                                                                                     `
                      -Type  "DWord"                                                                                   `
                      -Value $value
  }
}
##################################################
Write-Host "OK"