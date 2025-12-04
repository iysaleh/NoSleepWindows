# NoSleepWindows

A lightweight Windows application that prevents your computer from entering sleep mode and keeps the screen active. When running, a system tray icon indicates the application is active. Close the application to restore normal sleep and screen timeout behavior.

## Features

- **Prevents System Sleep**: Blocks the computer from entering sleep mode
- **Keeps Display Active**: Prevents the monitor from turning off or going into standby
- **System Tray Icon**: Runs minimized with a green indicator icon in the system tray
- **Simple Interface**: Left-click the tray icon for status, right-click to exit
- **Lightweight**: Single 135.5 KB executable with minimal resource usage
- **No Configuration**: Works immediately upon launch

## Quick Start

```bash
# Run pre-built executable
bin\Release\NoSleepWindows.exe

# Or build from source with Make
make build
make run

# Or use traditional scripts
build.bat      # Windows
bash build.sh  # Unix/Linux
```

## Releases

Pre-built executables are available on the [GitHub Releases](https://github.com/YOUR_USERNAME/NoSleepWindows/releases) page.

Automated releases are created when you push a version tag:
```bash
git tag v1.0.0
git push origin v1.0.0
```

See [GITHUB_ACTIONS.md](GITHUB_ACTIONS.md) for more details.

## Requirements

- Windows 7 or later
- .NET 8.0 Runtime (included with Windows 11+, or available as free download)
- Make (optional, for building with `make`)

## Installation & Usage

### Option 1: Run Pre-built Executable (Fastest)

1. Run `bin\Release\NoSleepWindows.exe`
2. A green circle icon will appear in your system tray (bottom-right of taskbar)
3. The application is now running - your computer will not sleep
4. To stop, right-click the tray icon and select "Exit"

### Option 2: Build from Source with Make

```bash
make build      # Build Release executable
make release    # Clean and build fresh
make clean      # Remove build artifacts
make run        # Build and run the app
```

### Option 3: Build with Traditional Scripts

**Windows:**
```cmd
build.bat
```

**macOS/Linux:**
```bash
bash build.sh
```

### Option 4: Manual Build

```bash
dotnet publish -c Release -o bin/Release --no-self-contained -r win-x64
```

## How It Works

The application uses Windows API calls (`SetThreadExecutionState`) to:
1. Set `ES_DISPLAY_REQUIRED` to keep the monitor on
2. Set `ES_SYSTEM_REQUIRED` to prevent system sleep
3. Call these functions every 60 seconds to maintain the wake state

This is a native Windows feature and is completely safe to use. Many legitimate applications use the same mechanism.

## Architecture

- **Language**: C# (.NET 8.0)
- **Framework**: Windows Forms
- **Type**: Windows GUI Application (WinExe)
- **Platform**: Windows only
- **Build Output**: Single 135.5 KB executable

## Build System

The project uses a cross-platform Makefile for building:

- `make build` - Builds Release executable
- `make clean` - Removes build artifacts
- `make release` - Clean build
- `make run` - Build and run
- `make help` - Show help

## Technical Details

The application:
- Runs as a background process with no visible window
- Displays only a system tray icon for status indication
- Uses Windows `SetThreadExecutionState` API to prevent sleep
- Sends wake signals every 60 seconds
- Automatically restores normal sleep behavior when closed
- Uses minimal CPU (<1%) and memory (~50 MB) resources
- No external dependencies or system modifications

## Exiting the Application

To stop the application and allow normal sleep/screen behavior:
- Right-click the tray icon and select "Exit"
- Or close the application from Task Manager

Sleep and screen timeout settings will immediately resume their normal behavior.

## Troubleshooting

**Icon not appearing in tray?**
- Check the notification area (hidden icons area in bottom-right)
- The icon should be a green circle
- Click the arrow to reveal hidden tray icons

**Still goes to sleep after running?**
- Ensure the application is still running (check tray icon)
- Some power settings may override this (check Windows Settings > System > Power & sleep)
- Domain Group Policy may restrict this on corporate machines
- Try running as Administrator

**Build fails?**
- Ensure .NET 8.0 SDK is installed: `dotnet --version`
- Try: `make clean` then `make build`
- Delete `bin` and `obj` folders manually and retry

## Files

- `NoSleepWindows.cs` - Main application source code
- `NoSleepWindows.csproj` - .NET project configuration
- `Makefile` - Cross-platform build configuration
- `bin/Release/NoSleepWindows.exe` - Compiled executable (after build)
- `build.bat` - Windows build helper script
- `build.sh` - Unix/Linux build helper script

## License

Free to use and modify.

