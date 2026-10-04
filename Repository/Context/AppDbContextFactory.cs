using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Context;

/// <summary>
/// Design-time factory used by the EF Core tools (<c>dotnet ef migrations</c>,
/// <c>dotnet ef database update</c>). EF instantiates the context through this
/// instead of booting the bot's <c>Program.cs</c>
///
/// <para>
/// <b>Generating</b> a migration never connects to a database, so the
/// placeholder connection string below is fine for <c>migrations add</c>.
/// <b>Applying</b> one (<c>database update</c>) does connect, set the
/// <c>DB_CONNECTION</c> environment variable to your real connection
/// string first, e.g. (PowerShell):
/// <code>$env:DB_CONNECTION = "server=localhost;port=3306;database=app;user=root;password=..."</code>
/// </para>
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Real value comes from the environment so no connection string (and no
        // credentials) is ever committed. The placeholder is only ever used for
        // `migrations add`, which doesn't open a connection.
        var connectionString =
            Environment.GetEnvironmentVariable("DB_CONNECTION")
            ?? "server=localhost;port=3306;database=app;user=root;password=";

        // Pin an explicit server version so the tools don't try to auto-detect
        // (which would require a live connection just to scaffold a migration).
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, serverVersion)
            .Options;

        return new AppDbContext(options);
    }
}