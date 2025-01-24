$SHELL_HOME = ${PSScriptRoot}
##################################################
$files = Get-ChildItem -Path "${SHELL_HOME}" -Filter "${env:USERNAME}\*.json"
##################################################
$files | ForEach-Object {
  $file = $_.FullName
  $data = Get-Content -Path "${file}" -Raw
  $json = $data | ConvertFrom-Json
  $json | ForEach-Object {
    $name  = $_.Name
    $value = $_.Value
    ##################################################
    Write-Host "Installing Font: $name($value)"
    ##################################################
    Set-ItemProperty  -Path  "HKCU:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts" `
                      -Name  $name                                                      `
                      -Type  "String"                                                   `
                      -Value $value
  }
}
##################################################
Write-Host "OK"
##################################################
# pause