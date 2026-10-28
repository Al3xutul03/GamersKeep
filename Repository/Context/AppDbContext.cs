using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository.Entity;
using Repository.Enums.Types;
using System.Text.RegularExpressions;

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
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;

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

            entity.Property(u => u.RefreshToken)
                  .HasMaxLength(255);

            entity.Property(u => u.RefreshTokenExpiryTime);

            entity.Property(u => u.Settings)
                  .HasColumnType("text")
                  .IsRequired();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                  .ValueGeneratedOnAdd();
            entity.Property(p => p.Name)
                  .HasMaxLength(255)
                  .IsRequired();
            entity.Property(p => p.AgeRating)
                  .HasConversion(
                      v => ToKebabCase(v.ToString()),
                      v => EnumParse<AgeRatingType>(v))
                  .HasMaxLength(50)
                  .IsRequired();
            entity.Property(p => p.Description)
                  .HasColumnType("text")
                  .IsRequired();
            entity.Property(p => p.Price)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();
            entity.Property(p => p.Stock)
                  .IsRequired();
            entity.Property(p => p.CreatedAt)
                  .IsRequired();
            entity.Property(p => p.UpdatedAt)
                  .IsRequired();
            entity.Property(p => p.DeletedAt);
            entity.HasMany(p => p.Categories)
                  .WithOne(pc => pc.Product)
                  .HasForeignKey(pc => pc.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Images)
                  .WithOne(pi => pi.Product)
                  .HasForeignKey(pi => pi.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(c => c.Type);
            entity.Property(c => c.Type)
                  .HasConversion(
                      v => ToKebabCase(v.ToString()),
                      v => EnumParse<CategoryType>(v))
                  .HasMaxLength(100);
            entity.Property(c => c.Description)
                  .HasColumnType("text")
                  .IsRequired();
            entity.Property(c => c.CreatedAt)
                  .IsRequired();
            entity.Property(c => c.UpdatedAt)
                  .IsRequired();
            entity.Property(c => c.DeletedAt);
            entity.HasMany(c => c.Products)
                  .WithOne(pc => pc.Category)
                  .HasForeignKey(pc => pc.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("product_categories");
            entity.HasKey(pc => new { pc.ProductId, pc.CategoryId });
            entity.Property(pc => pc.CategoryId)
                  .HasConversion(
                      v => ToKebabCase(v.ToString()),
                      v => EnumParse<CategoryType>(v))
                  .HasMaxLength(100);
            entity.Property(pc => pc.CreatedAt)
                  .IsRequired();
            entity.Property(pc => pc.UpdatedAt)
                  .IsRequired();
            entity.Property(pc => pc.DeletedAt);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.ToTable("product_images");
            entity.HasKey(pi => pi.ImageUrl);
            entity.Property(pi => pi.ImageUrl)
                  .HasMaxLength(768);
            entity.Property(pi => pi.CreatedAt)
                  .IsRequired();
            entity.Property(pi => pi.UpdatedAt)
                  .IsRequired();
            entity.Property(pi => pi.DeletedAt);
        });

        AppDbContextSeed.SeedData(modelBuilder);
    }

    // Helper to convert PascalCase to kebab-case
    private static string ToKebabCase(string value) =>
        Regex.Replace(value, "(?<!^)([A-Z])", "-$1").ToLower();

    // Helper to convert kebab-case back to Enum
    private static T EnumParse<T>(string value) where T : struct, Enum =>
        Enum.TryParse<T>(value.Replace("-", ""), true, out var result) ? result : default;
}
