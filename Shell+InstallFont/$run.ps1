$files = Get-ChildItem -Path . -Filter "*.json"
##################################################
$files | ForEach-Object {
  $file = $_.FullName
  $data = Get-Content -Path $file -Raw
  $json = $data | ConvertFrom-Json
  $json | ForEach-Object {
    $name = $_.Name
    $path = $_.Path
    ##################################################
    Write-Host "Installing font: $name($path)"
    ##################################################
    Set-ItemProperty  -Path  "HKCU:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts" `
                      -Name  $name                                                      `
                      -Type  "String"                                                   `
                      -Value $path
  }
}
##################################################
Write-Host "OK"
##################################################
pause