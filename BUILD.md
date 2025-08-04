# Build Instructions for SteamCMD GUI

## Overview

SteamCMD GUI is a .NET Framework 4.8 VB.NET Windows Forms application that provides a graphical interface for the SteamCMD command-line tool.

## Build Requirements

### Windows Environment (Required)
This project **requires** a Windows environment to build successfully because:

- **Target Framework**: .NET Framework 4.8 (Windows-only)
- **UI Framework**: Windows Forms (Windows-only)
- **Language**: Visual Basic .NET with Windows-specific features

### Prerequisites

1. **Windows 10/11** or **Windows Server 2016+**
2. **Visual Studio 2019/2022** with VB.NET support, OR
3. **.NET Framework 4.8 Developer Pack** + **MSBuild Tools**
4. **NuGet Package Manager** (for CoreRCON dependency)

## Build Instructions

### Option 1: Visual Studio (Recommended)

1. Open `Source/SteamCMD GUI.sln` in Visual Studio
2. Restore NuGet packages (should happen automatically)
3. Build the solution using:
   - **Debug**: Build → Build Solution (F6)
   - **Release**: Build → Batch Build → Select Release|x86

### Option 2: Command Line (MSBuild)

```cmd
# Navigate to the source directory
cd "Source"

# Restore NuGet packages
nuget restore "SteamCMD GUI.sln"

# Build the project
msbuild "SteamCMD GUI.sln" /p:Configuration=Release /p:Platform=x86
```

### Option 3: Developer Command Prompt

```cmd
# Open "Developer Command Prompt for VS" from Start Menu
cd "path\to\SteamCMD-GUI\Source"

# Build the solution
devenv "SteamCMD GUI.sln" /build Release
```

## Build Output

Successful builds will produce:
- **Debug**: `Source/SteamCMD GUI/bin/Debug/SteamCMD GUI.exe`
- **Release**: `Source/SteamCMD GUI/bin/Release/SteamCMD GUI.exe`

## Known Issues

### Linux/macOS Build Failure

Building on Linux or macOS will fail with:
```
error MSB3644: The reference assemblies for .NETFramework,Version=v4.8 were not found
```

**Solution**: Use a Windows environment or consider migrating to .NET Core/.NET 5+ (see Migration section below).

### Missing Dependencies

If you encounter missing dependency errors:
1. Ensure NuGet packages are restored: `nuget restore`
2. Check that CoreRCON package is available
3. Verify .NET Framework 4.8 is installed

## Migration to .NET Core/.NET 5+ (Optional)

For cross-platform compatibility, consider migrating to:
- **.NET 6/7/8** with **Windows Forms** support
- **Avalonia UI** for true cross-platform GUI
- **MAUI** for multi-platform applications

### Migration Steps (High-level)
1. Create new .NET 6+ project
2. Migrate VB.NET code (or convert to C#)
3. Update Windows Forms code for .NET Core compatibility
4. Update NuGet packages to .NET Standard/.NET Core versions
5. Test thoroughly on target platforms

## Troubleshooting

### Common Build Errors

| Error | Solution |
|-------|----------|
| MSB3644: Reference assemblies not found | Install .NET Framework 4.8 Developer Pack |
| Package 'CoreRCON' not found | Run `nuget restore` |
| Invalid platform target | Ensure x86 platform is selected |
| File access denied | Run Visual Studio as Administrator |

### Performance Tips

- Use Release configuration for production builds
- Enable code optimization in Release mode
- Consider x64 platform for better performance (requires testing)

## Contributing

When making changes:
1. Test builds in both Debug and Release configurations
2. Ensure no new build warnings are introduced
3. Update this documentation if build process changes
4. Test the built executable on target Windows versions

## Additional Resources

- [.NET Framework 4.8 Download](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)
- [MSBuild Documentation](https://docs.microsoft.com/en-us/visualstudio/msbuild/)