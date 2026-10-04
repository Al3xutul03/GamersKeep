using Microsoft.EntityFrameworkCore;

namespace Repository.Context;

/// <summary>
/// DbContext for all information required by the application.
/// </summary>
/// <param name="options">The DbContext options to use for the application.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
