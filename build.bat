@echo off
REM Build script for NoSleepWindows
REM This script builds the application into a standalone exe

echo Building NoSleepWindows...
echo.

REM Check if dotnet is installed
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo Error: .NET SDK is not installed or not in PATH
    echo Please install .NET 6.0 SDK from https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

REM Restore dependencies
echo Restoring dependencies...
dotnet restore

REM Build the project
echo Compiling application...
dotnet build -c Release

REM Publish as self-contained exe
echo Publishing as standalone executable...
dotnet publish -c Release -o bin\publish --self-contained -r win-x64 /p:PublishReadyToRun=true /p:DebugType=embedded

echo.
echo Build complete!
echo.
echo The executable is available at: bin\publish\NoSleepWindows.exe
echo.
pause
