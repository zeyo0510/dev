$SHELL_HOME = ${PSScriptRoot}
##################################################
$files = Get-ChildItem -Path "${SHELL_HOME}" -Filter "${env:USERNAME}\*.json"
##################################################
$files | ForEach-Object {
  $file = $_.FullName
  ##################################################
  $source = "${file}"
  $target = "${env:APPDATA}\Code\User\"
  ##################################################
  Write-Host "Copy $source to $target"
  ##################################################
  Copy-Item -Path "${source}" -Destination "${target}" -Force
}
##################################################
Write-Host "OK"
##################################################
# pause