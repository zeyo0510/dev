@echo off
set MSBuild=%windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe
%MSBuild% "System Information II.sln"
pause