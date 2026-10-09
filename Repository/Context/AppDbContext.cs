using Microsoft.EntityFrameworkCore;
using Repository.Entity;

namespace Repository.Context;

/// <summary>
/// DbContext for all information required by the application.
/// </summary>
/// <param name="options">The DbContext options to use for the application.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// All users known to the bot. Mapped to the <c>users</c> table.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                  .ValueGeneratedOnAdd();

            entity.Property(u => u.Name)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(u => u.Email)
                  .HasMaxLength(255)
                  .IsRequired();

            entity.HasIndex(u => u.Email)
                  .IsUnique();

            entity.Property(u => u.PasswordHash)
                  .IsRequired();

            entity.Property(u => u.CreatedAt)
                  .IsRequired();

            entity.Property(u => u.UpdatedAt)
                  .IsRequired();

            entity.Property(u => u.DeletedAt);

            entity.Property(u => u.IsAdmin)
                  .IsRequired()
                  .HasDefaultValue(false);

            entity.Property(u => u.Settings)
                  .HasColumnType("text")
                  .IsRequired();
        });
    }
}
