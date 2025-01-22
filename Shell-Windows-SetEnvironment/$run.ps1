$files = Get-ChildItem -Path . -Filter "*.json"
##################################################
$files | ForEach-Object {
  $file = $_.FullName
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
}
##################################################
Write-Host "OK"
##################################################
pause