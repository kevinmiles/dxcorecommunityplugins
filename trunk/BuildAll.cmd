@echo off

set MSBUILD=%SystemRoot%\Microsoft.NET\Framework\v3.5\msbuild.exe
set ISCC="%ProgramFiles%\Inno Setup\ISCC.exe"
if not exist %ISCC% set ISCC="%ProgramFiles(x86)%\Inno Setup\ISCC.exe"

:: Build each SLN
for /r %%F in (*.sln) do call :DOBUILD "%%F"

:: Create Setup EXE
if exist %ISCC% (
	call :COMPILE_SETUP 
) else (
	echo InnoSetup compiler not found: install from http://www.jrsoftware.org/
)

goto :EOF

:DOBUILD
	echo * %~n1
	%MSBUILD% /verbosity:quiet /target:Build /property:Configuration=Release;OutDir=%~dp0bin\ /nologo %1
	if %ERRORLEVEL% NEQ 0 (
		echo ================================================================
	)
	goto :EOF


:COMPILE_SETUP
	%ISCC% /Q DXCoreCommunityPluginsSuiteSetup.iss
	if %ERRORLEVEL% NEQ 0 (
		echo.
		echo ================================================================
		echo COMPILE SETUP FAIL
		echo ================================================================
		echo.
	) 
	goto :EOF
