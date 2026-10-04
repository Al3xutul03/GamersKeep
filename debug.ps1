<#
.SYNOPSIS
    Rebuilds and (re)starts the all-in-one GamersKeep container (app + MySQL)
    so you can attach the Visual Studio debugger to it.

.DESCRIPTION
    1. Builds Application/Dockerfile (Debug configuration by default).
    2. Removes any previous container and starts a fresh one (the DB starts empty).
    3. Waits until the app is answering HTTP requests.
    4. Follows the container logs (Ctrl+C stops following, the container keeps running).

    Then in Visual Studio: Debug > Attach to Process > Connection type "Docker (Linux Container)"
    > pick the "gamerskeep" container > attach to the "dotnet" process.

.EXAMPLE
    .\debug.ps1                  # build + run + follow logs
    .\debug.ps1 -NoLogs          # build + run, return to the prompt
    .\debug.ps1 -ExposeDb        # also publish MySQL on localhost:3307
    .\debug.ps1 -ResetDb         # wipe the persistent database volume before starting
    .\debug.ps1 -Stop            # stop and remove the container
#>
param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug',
    [int]$Port = 8080,
    [switch]$ExposeDb,
    [int]$DbPort = 3307,   # 3306 is taken by the MySQL installed on Windows
    [switch]$NoLogs,
    [switch]$ResetDb,
    [switch]$Stop
)

$Name  = 'gamerskeep'
$Image = "gamerskeep:$($Configuration.ToLower())"

# Always run from the solution root (the Docker build context).
Set-Location $PSScriptRoot

function Remove-OldContainer {
    if (docker ps -aq --filter "name=^$Name$") {
        Write-Host "Removing existing '$Name' container..." -ForegroundColor DarkGray
        docker rm -f $Name *> $null
    }
}

if ($Stop) {
    Remove-OldContainer
    Write-Host "Stopped." -ForegroundColor Green
    return
}

docker info *> $null
if ($LASTEXITCODE -ne 0) { Write-Error "Docker isn't running. Start Docker Desktop first."; exit 1 }

Write-Host "Building $Image..." -ForegroundColor Cyan
docker build -f Application/Dockerfile --build-arg "BUILD_CONFIGURATION=$Configuration" -t $Image .
if ($LASTEXITCODE -ne 0) { Write-Error "Build failed."; exit 1 }

Remove-OldContainer

if ($ResetDb) {
    Write-Host "Resetting database volume..." -ForegroundColor DarkGray
    docker volume rm gamerskeep-data *> $null
}

# Fail early with a readable message if a host port is already taken.
$ports = @($Port); if ($ExposeDb) { $ports += $DbPort }
foreach ($p in $ports) {
    $owner = Get-NetTCPConnection -LocalPort $p -State Listen -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($owner) {
        $proc = (Get-Process -Id $owner.OwningProcess -ErrorAction SilentlyContinue).ProcessName
        Write-Error "Port $p is already in use by '$proc' (PID $($owner.OwningProcess)). Pick another with -Port / -DbPort."
        exit 1
    }
}

$runArgs = @('run', '-d', '--name', $Name, '-p', "${Port}:8080", '-v', 'gamerskeep-data:/var/lib/mysql')
if ($ExposeDb) { $runArgs += @('-p', "127.0.0.1:${DbPort}:3306") }
$runArgs += $Image

Write-Host "Starting container..." -ForegroundColor Cyan
docker @runArgs | Out-Null
if ($LASTEXITCODE -ne 0) { Write-Error "Failed to start the container."; exit 1 }

# Wait for the app to answer (MySQL init + EF migrations take ~20-30s).
$url = "http://localhost:$Port"
$deadline = (Get-Date).AddSeconds(120)
$ready = $false
while ((Get-Date) -lt $deadline) {
    $state = docker inspect -f '{{.State.Running}}' $Name 2> $null
    if ($state -ne 'true') { break }
    try {
        Invoke-WebRequest $url -UseBasicParsing -TimeoutSec 3 | Out-Null
        $ready = $true; break
    } catch {
        # A non-2xx response still means the app is up.
        if ($_.Exception.Response) { $ready = $true; break }
        Start-Sleep -Seconds 2
    }
}

if (-not $ready) {
    Write-Host "`nThe app didn't come up. Last log lines:" -ForegroundColor Red
    docker logs --tail 40 $Name
    exit 1
}

$pw = (docker logs $Name 2>&1 | Select-String 'Generated MySQL password' | Select-Object -Last 1)
Write-Host ""
Write-Host "Ready: $url" -ForegroundColor Green
if ($pw) { Write-Host ($pw.Line -replace '^\[start\]\s*', '') -ForegroundColor DarkGray }
if ($ExposeDb) { Write-Host "MySQL: 127.0.0.1:$DbPort" -ForegroundColor DarkGray }
Write-Host "Attach: Debug > Attach to Process > Docker (Linux Container) > '$Name' > dotnet" -ForegroundColor Yellow
Write-Host "Stop:   .\debug.ps1 -Stop" -ForegroundColor DarkGray

if (-not $NoLogs) {
    Write-Host "`nFollowing logs (Ctrl+C to stop following; the container keeps running)...`n" -ForegroundColor DarkGray
    docker logs -f --since 1s $Name
}
