$files = Get-ChildItem -Path . -Filter "*.json"
##################################################
$files | ForEach-Object {
  $file = $_.FullName
  ##################################################
  $source = $file
  $target = "$env:APPDATA\Code\User\keybindings.json"
  ##################################################
  Write-Host "Copy $source to $target"
  ##################################################
  Copy-Item -Path $source -Destination $target -Force
}
##################################################
Write-Host "OK"
##################################################
pause