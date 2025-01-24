$SHELL_HOME = ${PSScriptRoot}
##################################################
$file = Get-ChildItem -Path "${SHELL_HOME}" -Filter "${env:USERNAME}.json"
$file = $file.FullName
$data = Get-Content -Path "${file}" -Raw
$json = $data | ConvertFrom-Json
$json | ForEach-Object {
  $code = $_.Code
  $name = $_.Name
  ##################################################
  Write-Host "Installing VSCode Extension: $name"
  ##################################################
  code --install-extension $code --force
}
##################################################
Write-Host "OK"
##################################################
# pause