# GamersKeep

An E-Commerce application for Board Game fans and TTRPG enthusiasts.

## Running the Application Locally (Test Environment)

We provide a script that builds and runs an all-in-one testing container, which includes both the ASP.NET application and a persistent MySQL 8.0 database.

### Using PowerShell
From the solution root, run the following helper script:

```powershell
# Build, run, and follow logs:
.\debug.ps1

# Other options:
.\debug.ps1 -NoLogs      # Build + run, but return to the prompt
.\debug.ps1 -ExposeDb    # Also expose the database to localhost:3307
.\debug.ps1 -ResetDb     # Wipe the persistent database volume and start fresh
.\debug.ps1 -Stop        # Stop and remove the container
```

### Debugging with Visual Studio
After starting the container with `.\debug.ps1`:
1. In Visual Studio, go to **Debug** > **Attach to Process** (`Ctrl+Alt+P`).
2. Set **Connection type** to **Docker (Linux Container)**.
3. Select the `gamerskeep` container.
4. Select the `dotnet` process to attach.
