using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Testcontainers.MySql;

namespace Tests.Integration;

public static class MySqlContainerFixture
{
    private static MySqlContainer? _container;

    public static string ConnectionString { get; private set; } = null!;
    public static ServerVersion ServerVersion { get; private set; } = null!;

    public static async Task StartAsync()
    {
        if (_container is not null)
        {
            return;
        }

        _container = new MySqlBuilder("mysql:8.4")
            .WithDatabase("gamerskeep_test")
            .Build();

        await _container.StartAsync();

        ConnectionString = _container.GetConnectionString();
        ServerVersion = ServerVersion.AutoDetect(ConnectionString);

        await using var context = CreateContext();
        await context.Database.MigrateAsync();
    }

    public static async Task StopAsync()
    {
        if (_container is not null)
        {
            await _container.DisposeAsync();
            _container = null;
        }
    }

    public static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(ConnectionString, ServerVersion)
            .Options;

        return new AppDbContext(options);
    }

    public static async Task ResetAsync()
    {
        await using var context = CreateContext();
        await context.Users.ExecuteDeleteAsync();
    }
}
