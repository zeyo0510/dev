$BIN_HOME = "${env:WINDIR}\Microsoft.NET\Framework\v4.0.30319"
##################################################
$msbuild = "$BIN_HOME\MSBuild.exe"
##################################################
& $msbuild "App.Windows.XPMinesweeper.csproj"
##################################################
pause