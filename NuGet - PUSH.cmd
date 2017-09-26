@echo off
REM NuGet push SymbolSource.Server.Basic.*.nupkg 123 -source "http://vm-swy-nuget:81/nuget"
NuGet push SymbolSource.Server.Basic.*.nupkg 123 -source "https://nuget.credas.local/nuget"
pause