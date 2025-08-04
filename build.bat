@echo off
REM Build script for SteamCMD GUI
REM This script builds the project using MSBuild

echo ===============================================
echo    SteamCMD GUI Build Script
echo ===============================================
echo.

REM Check if we're in the right directory
if not exist "Source\SteamCMD GUI.sln" (
    echo Error: This script must be run from the root directory of the project
    echo Expected to find: Source\SteamCMD GUI.sln
    echo.
    pause
    exit /b 1
)

REM Change to Source directory
cd Source

echo Step 1: Restoring NuGet packages...
nuget restore "SteamCMD GUI.sln"
if errorlevel 1 (
    echo Error: Failed to restore NuGet packages
    echo Please ensure NuGet is installed and accessible
    echo.
    pause
    exit /b 1
)

echo.
echo Step 2: Building Debug configuration...
msbuild "SteamCMD GUI.sln" /p:Configuration=Debug /p:Platform=x86
if errorlevel 1 (
    echo Error: Debug build failed
    echo.
    pause
    exit /b 1
)

echo.
echo Step 3: Building Release configuration...
msbuild "SteamCMD GUI.sln" /p:Configuration=Release /p:Platform=x86
if errorlevel 1 (
    echo Error: Release build failed
    echo.
    pause
    exit /b 1
)

echo.
echo ===============================================
echo    Build completed successfully!
echo ===============================================
echo.
echo Output files:
echo   Debug:   Source\SteamCMD GUI\bin\Debug\SteamCMD GUI.exe
echo   Release: Source\SteamCMD GUI\bin\Release\SteamCMD GUI.exe
echo.

REM Return to original directory
cd ..

echo Press any key to exit...
pause >nul