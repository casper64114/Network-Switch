@echo off
cls 

echo Choose a network adapter:
echo ========================
echo 1. Ethernet
echo 2. Ethernet 2
echo 3. WiFi
echo 4. WiFi 2
echo 5. Enter name or IP

set /p choice="Enter choice (1-5): "

if %choice%==1 set adapter=Ethernet
if %choice%==2 set adapter=Ethernet 2
if %choice%==3 set adapter=WiFi
if %choice%==4 set adapter=WiFi 2
if %choice%==5 set /p adapter="Enter name or IP: "

echo.
echo Now choose an application to launch:
echo ====================================  
set /p apppath="Enter full path to app: "

echo.
echo Launching %apppath% on %adapter%...

start %apppath% 

echo.
set /p join="Would you like to join my Discord server? (Y/N): "
if /I "%join%" == "Y" start https://discord.gg/MvR43saFGf

pause
