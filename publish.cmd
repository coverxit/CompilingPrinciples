@echo off
rmdir /s /q Publish
mkdir Publish
copy Release\*.exe Publish\
copy Release\*.dll Publish\
del /f /q Publish\*.vshost.exe
echo. & echo Done!
pause