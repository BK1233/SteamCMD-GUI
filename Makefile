# Makefile for SteamCMD GUI (Windows only)
# Requires MSBuild and NuGet to be in PATH

.PHONY: all debug release clean restore help

# Default target
all: debug release

# Configuration variables
SOLUTION = "Source/SteamCMD GUI.sln"
PLATFORM = x86
MSBUILD_FLAGS = /p:Platform=$(PLATFORM) /verbosity:minimal

# Build debug configuration
debug: restore
	@echo Building Debug configuration...
	msbuild $(SOLUTION) /p:Configuration=Debug $(MSBUILD_FLAGS)
	@echo Debug build completed

# Build release configuration  
release: restore
	@echo Building Release configuration...
	msbuild $(SOLUTION) /p:Configuration=Release $(MSBUILD_FLAGS)
	@echo Release build completed

# Restore NuGet packages
restore:
	@echo Restoring NuGet packages...
	cd Source && nuget restore "SteamCMD GUI.sln"

# Clean build outputs
clean:
	@echo Cleaning build outputs...
	msbuild $(SOLUTION) /t:Clean /p:Configuration=Debug $(MSBUILD_FLAGS)
	msbuild $(SOLUTION) /t:Clean /p:Configuration=Release $(MSBUILD_FLAGS)
	@echo Clean completed

# Rebuild (clean + build)
rebuild: clean all

# Show help
help:
	@echo Available targets:
	@echo   all      - Build both Debug and Release configurations (default)
	@echo   debug    - Build Debug configuration only
	@echo   release  - Build Release configuration only
	@echo   restore  - Restore NuGet packages
	@echo   clean    - Clean build outputs
	@echo   rebuild  - Clean and rebuild all configurations
	@echo   help     - Show this help message
	@echo.
	@echo Usage examples:
	@echo   make           # Build both configurations
	@echo   make debug     # Build debug only
	@echo   make release   # Build release only
	@echo   make clean     # Clean outputs
	@echo.
	@echo Requirements:
	@echo   - Windows environment
	@echo   - MSBuild in PATH
	@echo   - NuGet in PATH
	@echo   - .NET Framework 4.8