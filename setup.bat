@echo off
REM ========================================
REM    Stealerium Setup Script - Windows
REM    🔥 Enhanced by @chocolaid on GitHub
REM ========================================
REM
REM Features Added:
REM - Automated dependency checking and installation
REM - .NET 6.0+ verification
REM - MSBuild detection and configuration
REM - NuGet package restoration
REM - Complete project build automation
REM
REM "Setting up malware development like a fucking professional" 💀
REM
echo ========================================
echo    Stealerium Setup Script (Windows)
echo ========================================
echo.

REM Check if running as administrator
net session >nul 2>&1
if %errorLevel% == 0 (
    echo [INFO] Running with administrator privileges
) else (
    echo [WARNING] Not running as administrator. Some features may not work properly.
    echo [INFO] Consider running as administrator for full functionality.
)
echo.

REM Check for .NET 6.0 runtime specifically
echo [CHECK] Verifying .NET 6.0 runtime installation...
dotnet --list-runtimes | findstr "Microsoft.NETCore.App 6.0" >nul 2>&1
if %errorLevel% == 0 (
    echo [OK] .NET 6.0 runtime detected
) else (
    echo [ERROR] .NET 6.0 runtime is required but not found
    echo [INFO] Download from: https://builds.dotnet.microsoft.com/dotnet/Runtime/6.0.36/dotnet-runtime-6.0.36-win-x64.exe
    echo [INFO] Or visit: https://dotnet.microsoft.com/download/dotnet/6.0
    pause
    exit /b 1
)

REM Check for Visual Studio Build Tools or MSBuild
echo [CHECK] Verifying build tools...
set MSBUILD_PATH=""
where msbuild >nul 2>&1
if %errorLevel% == 0 (
    echo [OK] MSBuild found in PATH
    set MSBUILD_PATH=msbuild
) else (
    echo [CHECK] Looking for MSBuild in Visual Studio directories...
    set MSBUILD_FOUND=0
    
    REM Check Visual Studio 2022
    if exist "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" (
        echo [OK] MSBuild found: Visual Studio 2022 Community
        set "MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
        set MSBUILD_FOUND=1
    )
    if exist "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe" (
        echo [OK] MSBuild found: Visual Studio 2022 Professional
        set "MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
        set MSBUILD_FOUND=1
    )
    if exist "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe" (
        echo [OK] MSBuild found: Visual Studio 2022 Enterprise
        set "MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
        set MSBUILD_FOUND=1
    )
    
    REM Check Visual Studio 2019
    if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe" (
        echo [OK] MSBuild found: Visual Studio 2019 Build Tools
        set "MSBUILD_PATH=C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
        set MSBUILD_FOUND=1
    )
    if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" (
        echo [OK] MSBuild found: Visual Studio 2019 Community
        set "MSBUILD_PATH=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
        set MSBUILD_FOUND=1
    )
    
    if "%MSBUILD_FOUND%"=="0" (
        echo [ERROR] MSBuild not found. Please install Visual Studio Build Tools or Visual Studio.
        echo [INFO] Download from: https://visualstudio.microsoft.com/downloads/
        echo.
        echo [INFO] Press any key to exit...
        pause
        exit /b 1
    )
)

REM Check for NuGet
echo [CHECK] Verifying NuGet...
where nuget >nul 2>&1
if %errorLevel% == 0 (
    echo [OK] NuGet found in PATH
) else (
    echo [INFO] Downloading NuGet CLI...
    powershell -Command "Invoke-WebRequest -Uri 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe' -OutFile 'nuget.exe'"
    if exist "nuget.exe" (
        echo [OK] NuGet downloaded successfully
    ) else (
        echo [ERROR] Failed to download NuGet
        pause
        exit /b 1
    )
)

REM Restore packages
echo [INFO] Restoring NuGet packages...
if exist "nuget.exe" (
    nuget.exe restore Stealerium.sln
) else (
    dotnet restore
)
if %errorLevel% neq 0 (
    echo [ERROR] Failed to restore packages
    pause
    exit /b 1
)
echo [OK] Packages restored successfully

REM Build the solution
echo [INFO] Building Stealerium solution...
if exist "nuget.exe" (
    if "%MSBUILD_PATH%"=="" (
        echo [ERROR] MSBuild path not set
        pause
        exit /b 1
    )
    "%MSBUILD_PATH%" Stealerium.sln /p:Configuration=Release /p:Platform="Any CPU"
) else (
    dotnet build Stealerium.sln --configuration Release
)
if %errorLevel% neq 0 (
    echo [ERROR] Build failed
    echo [INFO] Check the error messages above for details
    echo.
    echo [INFO] Press any key to exit...
    pause
    exit /b 1
)
echo [OK] Build completed successfully

REM Verify output files
echo [INFO] Verifying output files...
if exist "Binaries\Release\net6.0-windows\Builder.exe" (
    echo [OK] Builder.exe found
) else (
    echo [ERROR] Builder.exe not found
    echo [INFO] The build may have failed. Check the build output above.
    echo.
    echo [INFO] Press any key to exit...
    pause
    exit /b 1
)

if exist "Binaries\Release\net6.0-windows\Stub\stub.exe" (
    echo [OK] stub.exe found
) else (
    echo [ERROR] stub.exe not found
    echo [INFO] The build may have failed. Check the build output above.
    echo.
    echo [INFO] Press any key to exit...
    pause
    exit /b 1
)

echo.
echo ========================================
echo    Setup completed successfully!
echo ========================================
echo.
echo [INFO] You can now run builder.bat to launch the Builder
echo [INFO] Output files are in: Binaries\Release\net6.0-windows\
echo.
pause
