#!/bin/bash
# Build script for NoSleepWindows on macOS/Linux

echo "Building NoSleepWindows..."
echo ""

# Check if dotnet is installed
if ! command -v dotnet &> /dev/null
then
    echo "Error: .NET SDK is not installed"
    echo "Please install .NET 6.0 SDK from https://dotnet.microsoft.com/download"
    exit 1
fi

# Restore dependencies
echo "Restoring dependencies..."
dotnet restore

# Build the project
echo "Compiling application..."
dotnet build -c Release

# Publish as self-contained exe
echo "Publishing as standalone executable..."
dotnet publish -c Release -o bin/publish --self-contained -r win-x64 /p:PublishReadyToRun=true /p:DebugType=embedded

echo ""
echo "Build complete!"
echo ""
echo "The executable is available at: bin/publish/NoSleepWindows.exe"
echo ""
