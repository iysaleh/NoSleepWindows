# NoSleepWindows

A lightweight Windows application that prevents your computer from entering sleep mode and keeps the screen active. When running, a system tray icon indicates the application is active. Close the application to restore normal sleep and screen timeout behavior.

## Features

- **Prevents System Sleep**: Blocks the computer from entering sleep mode
- **Keeps Display Active**: Prevents the monitor from turning off or going into standby
- **System Tray Icon**: Runs minimized with a green indicator icon in the system tray
- **Simple Interface**: Left-click the tray icon for status, right-click to exit
- **Lightweight**: Minimal resource usage with periodic wake signals
- **No Configuration**: Works immediately upon launch

## Requirements

- Windows 7 or later
- .NET 6.0 SDK (only needed for building from source)

## Installation & Usage

### Option 1: Use Pre-built Executable (Recommended)

1. Download or build the `NoSleepWindows.exe` file
2. Run the executable
3. A green circle icon will appear in your system tray (bottom-right of taskbar)
4. The application is now running - your computer will not sleep
5. To stop, right-click the tray icon and select "Exit" or close it from the notification area

### Option 2: Build from Source

#### Prerequisites
- Install [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later

#### Windows Build

1. Open Command Prompt in the project directory
2. Run the build script:
   ```bash
   build.bat
   ```
3. The compiled executable will be in: `bin\publish\NoSleepWindows.exe`
4. Run the executable to start the application

#### macOS/Linux Build (for Windows target)

1. Open Terminal in the project directory
2. Run the build script:
   ```bash
   ./build.sh
   ```
3. The compiled executable will be in: `bin/publish/NoSleepWindows.exe`
4. Copy to a Windows machine and run

### Manual Build (All Platforms)

If the build scripts don't work, use these commands directly:

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build -c Release

# Publish as standalone executable
dotnet publish -c Release -o bin/publish --self-contained -r win-x64 /p:PublishReadyToRun=true /p:DebugType=embedded
```

## How It Works

The application uses Windows API calls (`SetThreadExecutionState`) to:
1. Set `ES_DISPLAY_REQUIRED` to keep the monitor on
2. Set `ES_SYSTEM_REQUIRED` to prevent system sleep
3. Call these functions every 60 seconds to maintain the wake state

This is a native Windows feature and is completely safe to use. Many legitimate applications use the same mechanism.

## Architecture

- **Language**: C# (.NET 6.0)
- **Framework**: Windows Forms
- **Type**: Windows Forms Application (WinExe)
- **Platform**: Windows only
- **Size**: ~50 MB (self-contained executable)

## Technical Details

The application:
- Runs as a background process with no visible window
- Displays only a system tray icon for status indication
- Uses Windows `SetThreadExecutionState` API to prevent sleep
- Sends wake signals every 60 seconds
- Automatically restores normal sleep behavior when closed
- Uses minimal CPU and memory resources

## Exiting the Application

To stop the application and allow normal sleep/screen behavior:
- Right-click the tray icon and select "Exit"
- Or click the close button if a window is open

Sleep and screen timeout settings will immediately resume their normal behavior.

## Troubleshooting

**Icon not appearing in tray?**
- Check the notification area (hidden icons area in bottom-right)
- The icon should be a green circle

**Still goes to sleep after running?**
- Ensure the application is still running (check tray icon)
- Some power settings may override this (check Windows Settings > System > Power & sleep)
- Domain Group Policy may restrict this on corporate machines

**Build fails?**
- Ensure .NET 6.0 SDK is installed: `dotnet --version`
- Try manual build commands listed above
- Delete `bin` and `obj` folders and retry

## License

Free to use and modify.
