#!/bin/bash
# Build script for NoSleepWindows using Makefile
# For faster building, just run: make

if [ -z "$1" ]; then
    echo "Building NoSleepWindows..."
    make build
else
    make "$@"
fi

