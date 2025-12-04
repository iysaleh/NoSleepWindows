@echo off
REM Build script for NoSleepWindows using Makefile
REM For faster building, just run: make

if "%1"=="" (
    echo Building NoSleepWindows...
    make build
) else (
    make %*
)

