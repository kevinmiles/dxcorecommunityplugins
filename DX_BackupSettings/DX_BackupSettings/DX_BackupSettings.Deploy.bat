REM Waste Line
COPY DX_BackupSettings.dll temp.dll
ILMerge.exe /out:DX_BackupSettings.dll temp.dll Ionic.Zip.Reduced.dll
DEL temp.dll
pause