# SteamCMD GUI Build Script (PowerShell)
# This script builds the project using MSBuild and NuGet

param(
    [string]$Configuration = "Both", # Debug, Release, or Both
    [switch]$SkipRestore = $false,
    [switch]$Verbose = $false
)

Write-Host "===============================================" -ForegroundColor Cyan
Write-Host "    SteamCMD GUI Build Script (PowerShell)" -ForegroundColor Cyan
Write-Host "===============================================" -ForegroundColor Cyan
Write-Host ""

# Check if we're in the right directory
if (-not (Test-Path "Source\SteamCMD GUI.sln")) {
    Write-Host "Error: This script must be run from the root directory of the project" -ForegroundColor Red
    Write-Host "Expected to find: Source\SteamCMD GUI.sln" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

# Function to check if a command exists
function Test-Command($command) {
    try {
        Get-Command $command -ErrorAction Stop | Out-Null
        return $true
    }
    catch {
        return $false
    }
}

# Check for required tools
Write-Host "Checking required tools..." -ForegroundColor Yellow

if (-not (Test-Command "msbuild")) {
    Write-Host "Error: MSBuild not found. Please install Visual Studio or Build Tools for Visual Studio." -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

if (-not (Test-Command "nuget") -and -not $SkipRestore) {
    Write-Host "Warning: NuGet not found. Attempting to skip package restore..." -ForegroundColor Yellow
    $SkipRestore = $true
}

Write-Host "✓ MSBuild found" -ForegroundColor Green
if (-not $SkipRestore) {
    Write-Host "✓ NuGet found" -ForegroundColor Green
}

# Change to Source directory
Push-Location "Source"

try {
    # Restore NuGet packages
    if (-not $SkipRestore) {
        Write-Host ""
        Write-Host "Step 1: Restoring NuGet packages..." -ForegroundColor Yellow
        
        $restoreArgs = @("restore", "SteamCMD GUI.sln")
        if ($Verbose) { $restoreArgs += "-Verbosity", "detailed" }
        
        & nuget $restoreArgs
        if ($LASTEXITCODE -ne 0) {
            throw "Failed to restore NuGet packages"
        }
        Write-Host "✓ NuGet packages restored successfully" -ForegroundColor Green
    } else {
        Write-Host "Skipping NuGet package restore..." -ForegroundColor Yellow
    }

    # Build configurations
    $configs = @()
    switch ($Configuration.ToLower()) {
        "debug" { $configs = @("Debug") }
        "release" { $configs = @("Release") }
        "both" { $configs = @("Debug", "Release") }
        default { 
            Write-Host "Invalid configuration: $Configuration. Use Debug, Release, or Both." -ForegroundColor Red
            exit 1
        }
    }

    foreach ($config in $configs) {
        Write-Host ""
        Write-Host "Step: Building $config configuration..." -ForegroundColor Yellow
        
        $buildArgs = @(
            "SteamCMD GUI.sln",
            "/p:Configuration=$config",
            "/p:Platform=x86"
        )
        
        if ($Verbose) {
            $buildArgs += "/verbosity:detailed"
        } else {
            $buildArgs += "/verbosity:minimal"
        }
        
        & msbuild $buildArgs
        if ($LASTEXITCODE -ne 0) {
            throw "$config build failed"
        }
        
        # Check if output file exists
        $outputPath = "SteamCMD GUI\bin\$config\SteamCMD GUI.exe"
        if (Test-Path $outputPath) {
            Write-Host "✓ $config build completed successfully" -ForegroundColor Green
            Write-Host "   Output: $outputPath" -ForegroundColor Gray
        } else {
            Write-Host "⚠ $config build completed but output file not found" -ForegroundColor Yellow
        }
    }

    Write-Host ""
    Write-Host "===============================================" -ForegroundColor Cyan
    Write-Host "    Build completed successfully!" -ForegroundColor Cyan
    Write-Host "===============================================" -ForegroundColor Cyan
    Write-Host ""
    
    Write-Host "Output files:" -ForegroundColor White
    foreach ($config in $configs) {
        $outputPath = "SteamCMD GUI\bin\$config\SteamCMD GUI.exe"
        if (Test-Path $outputPath) {
            $fullPath = Resolve-Path $outputPath
            Write-Host "  $config : $fullPath" -ForegroundColor Gray
        }
    }

} catch {
    Write-Host ""
    Write-Host "Build failed: $($_.Exception.Message)" -ForegroundColor Red
    Pop-Location
    Read-Host "Press Enter to exit"
    exit 1
} finally {
    Pop-Location
}

Write-Host ""
Read-Host "Press Enter to exit"