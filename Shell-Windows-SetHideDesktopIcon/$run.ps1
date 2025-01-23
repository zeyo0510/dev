$file = Get-ChildItem -Path . -Filter "${env:USERNAME}.json"
$file = $file.FullName
$data = Get-Content -Path $file -Raw
$json = $data | ConvertFrom-Json
$json | ForEach-Object {
  $name        = $_.Name
  $description = $_.Description
  $value       = $_.Value
  ##################################################
  Write-Host "Set Hide Desktop Icon: $description = $value"
  ##################################################
  Set-ItemProperty  -Path  "HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel" `
                    -Name  $name                                                                                     `
                    -Type  "DWord"                                                                                   `
                    -Value $value
}
##################################################
Write-Host "OK"
##################################################
pause