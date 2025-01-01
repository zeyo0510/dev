$files = Get-ChildItem -Path . -Filter "*.json"
##################################################
$files | ForEach-Object {
  $file = $_.FullName
  $data = Get-Content -Path $file -Raw
  $json = $data | ConvertFrom-Json
  $json | ForEach-Object {
    $code  = $_.Code
    $name  = $_.Name
    ##################################################
    Write-Host "Install VSCode Extension: $name"
    ##################################################
    code --install-extension $code --force
  }
}
##################################################
Write-Host "OK"
##################################################
pause