@echo off
set MSBuildPath="%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\"
if not exist %MsBuildPath% (
	echo please donwload Microsoft Visual Studio Build Tools
	start "" https://www.visualstudio.com/fr/thank-you-downloading-visual-studio/?sku=BuildTools&rel=15
) else (
	%MsBuildPath%\MSBuild.exe "MagicRemoteService.sln /p:Configuration=Debug;Platform="Any CPU" /clp:Summary /nologo"
	%MSBuildPath%\MSBuild.exe "MagicRemoteService.sln /p:Configuration=Release;Platform="Any CPU" /clp:Summary /nologo"
)
pause