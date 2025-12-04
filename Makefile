.PHONY: build clean help release run

help:
	@echo NoSleepWindows Build System
	@echo ============================
	@echo Available targets:
	@echo   make build      - Build the application in Release mode
	@echo   make clean      - Remove build artifacts
	@echo   make release    - Clean and build Release executable
	@echo   make run        - Build and run the application
	@echo   make help       - Show this help message

build:
	dotnet publish -c Release -o bin/Release --no-self-contained -r win-x64
	powershell -NoProfile -Command "if (Test-Path 'bin/Release/net8.0-windows/win-x64/NoSleepWindows.exe') { Copy-Item 'bin/Release/net8.0-windows/win-x64/NoSleepWindows.exe' 'bin/Release/NoSleepWindows.exe' -Force; Write-Host 'Build successful! Executable: bin/Release/NoSleepWindows.exe' -ForegroundColor Green } else { Write-Host 'Build failed' -ForegroundColor Red; exit 1 }"

clean:
	powershell -NoProfile -Command "if (Test-Path 'bin') { Remove-Item -Recurse -Force 'bin' }; if (Test-Path 'obj') { Remove-Item -Recurse -Force 'obj' }; Write-Host 'Clean complete' -ForegroundColor Green"

release: clean build
	@echo Release build complete!

run: build
	@echo.
	@powershell -NoProfile -Command "Start-Process 'bin/Release/NoSleepWindows.exe'"
