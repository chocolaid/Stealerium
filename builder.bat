@echo off
REM ========================================
REM    Stealerium Builder Launcher - Windows
REM    🔥 Enhanced by @chocolaid on GitHub
REM ========================================
REM
REM Features Added:
REM - Automated Builder.exe launcher
REM - Dependency verification
REM - Clean error handling
REM - Professional user experience
REM
REM "Launching malware builders like a fucking professional" 💀
REM
echo ========================================
echo    Stealerium Builder Launcher
echo ========================================
echo.

REM Check if Builder.exe exists
if not exist "Binaries\Release\net6.0-windows\Builder.exe" (
    echo [ERROR] Builder.exe not found!
    echo [INFO] Please run setup.bat first to build the project.
    echo.
    pause
    exit /b 1
)

REM Check if stub.exe exists
if not exist "Binaries\Release\net6.0-windows\Stub\stub.exe" (
    echo [ERROR] stub.exe not found!
    echo [INFO] Please run setup.bat first to build the project.
    echo.
    pause
    exit /b 1
)

REM Change to the binaries directory
cd "Binaries\Release\net6.0-windows"

REM Launch the Builder
echo [INFO] Starting Stealerium Builder...

Builder.exe

cd ..\..\..

echo.
echo [INFO] Builder session ended.
pause
