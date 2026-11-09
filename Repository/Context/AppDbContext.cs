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
    public DbSet<CartEntry> CartEntries { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<ProcessedStripeEvent> ProcessedStripeEvents { get; set; } = null!;

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

            entity.HasMany(u => u.CartEntries)
                  .WithOne(ce => ce.User)
                  .HasForeignKey(ce => ce.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.Addresses)
                  .WithOne(a => a.User)
                  .HasForeignKey(a => a.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Orders are financial records, they must not disappear with the user.
            entity.HasMany(u => u.Orders)
                  .WithOne(o => o.User)
                  .HasForeignKey(o => o.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
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
            entity.HasMany(p => p.CartEntries)
                  .WithOne(ce => ce.Product)
                  .HasForeignKey(ce => ce.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
            // Keep order history intact: a product with orders cannot be hard-deleted.
            entity.HasMany(p => p.OrderItems)
                  .WithOne(oi => oi.Product)
                  .HasForeignKey(oi => oi.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
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

        modelBuilder.Entity<CartEntry>(entity =>
        {
            entity.ToTable("cart_entries");
            entity.HasKey(ce => new { ce.UserId, ce.ProductId });
            entity.Property(ce => ce.Quantity)
                  .IsRequired();
            entity.Property(ce => ce.CreatedAt)
                  .IsRequired();
            entity.Property(ce => ce.UpdatedAt)
                  .IsRequired();
            entity.Property(ce => ce.DeletedAt);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("addresses");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id)
                  .ValueGeneratedOnAdd();
            entity.Property(a => a.Label)
                  .HasMaxLength(50);
            entity.Property(a => a.FullName)
                  .HasMaxLength(150)
                  .IsRequired();
            entity.Property(a => a.Phone)
                  .HasMaxLength(30)
                  .IsRequired();
            entity.Property(a => a.Street)
                  .HasMaxLength(255)
                  .IsRequired();
            entity.Property(a => a.City)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.Property(a => a.County)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.Property(a => a.PostalCode)
                  .HasMaxLength(20)
                  .IsRequired();
            entity.Property(a => a.IsDefault)
                  .IsRequired()
                  .HasDefaultValue(false);
            entity.Property(a => a.CreatedAt)
                  .IsRequired();
            entity.Property(a => a.UpdatedAt)
                  .IsRequired();
            entity.Property(a => a.DeletedAt);
            entity.HasIndex(a => a.UserId);
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.ToTable("delivery_methods");
            entity.HasKey(dm => dm.Id);
            entity.Property(dm => dm.Id)
                  .ValueGeneratedOnAdd();
            entity.Property(dm => dm.Name)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.Property(dm => dm.Description)
                  .HasMaxLength(255)
                  .IsRequired();
            entity.Property(dm => dm.EstimatedDelivery)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.Property(dm => dm.Price)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();
            entity.Property(dm => dm.IsActive)
                  .IsRequired()
                  .HasDefaultValue(true);
            entity.Property(dm => dm.SortOrder)
                  .IsRequired();
            entity.Property(dm => dm.CreatedAt)
                  .IsRequired();
            entity.Property(dm => dm.UpdatedAt)
                  .IsRequired();
            entity.Property(dm => dm.DeletedAt);
            entity.HasMany(dm => dm.Orders)
                  .WithOne(o => o.DeliveryMethod)
                  .HasForeignKey(o => o.DeliveryMethodId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id)
                  .ValueGeneratedOnAdd();
            entity.Property(o => o.OrderNumber)
                  .HasMaxLength(32)
                  .IsRequired();
            entity.HasIndex(o => o.OrderNumber)
                  .IsUnique();
            entity.Property(o => o.Status)
                  .HasConversion(
                      v => ToKebabCase(v.ToString()),
                      v => EnumParse<OrderStatusType>(v))
                  .HasMaxLength(50)
                  .IsRequired();
            entity.Property(o => o.DeliveryMethodName)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.Property(o => o.DeliveryCost)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();
            entity.Property(o => o.Subtotal)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();
            entity.Property(o => o.Total)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();
            entity.Property(o => o.Currency)
                  .HasMaxLength(3)
                  .IsRequired();
            entity.Property(o => o.ShippingFullName)
                  .HasMaxLength(150)
                  .IsRequired();
            entity.Property(o => o.ShippingPhone)
                  .HasMaxLength(30)
                  .IsRequired();
            entity.Property(o => o.ShippingStreet)
                  .HasMaxLength(255)
                  .IsRequired();
            entity.Property(o => o.ShippingCity)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.Property(o => o.ShippingCounty)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.Property(o => o.ShippingPostalCode)
                  .HasMaxLength(20)
                  .IsRequired();
            entity.Property(o => o.StripeCheckoutSessionId)
                  .HasMaxLength(255);
            entity.HasIndex(o => o.StripeCheckoutSessionId)
                  .IsUnique();
            entity.Property(o => o.StripePaymentIntentId)
                  .HasMaxLength(255);
            entity.Property(o => o.StockDeducted)
                  .IsRequired()
                  .HasDefaultValue(false);
            entity.Property(o => o.PaidAt);
            entity.Property(o => o.CancelledAt);
            entity.Property(o => o.RefundedAt);
            entity.Property(o => o.CreatedAt)
                  .IsRequired();
            entity.Property(o => o.UpdatedAt)
                  .IsRequired();
            // Order history: "my orders, newest first".
            entity.HasIndex(o => new { o.UserId, o.CreatedAt });
            entity.HasMany(o => o.Items)
                  .WithOne(oi => oi.Order)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("order_items");
            entity.HasKey(oi => new { oi.OrderId, oi.ProductId });
            entity.Property(oi => oi.ProductName)
                  .HasMaxLength(255)
                  .IsRequired();
            entity.Property(oi => oi.UnitPrice)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();
            entity.Property(oi => oi.Quantity)
                  .IsRequired();
        });

        modelBuilder.Entity<ProcessedStripeEvent>(entity =>
        {
            entity.ToTable("processed_stripe_events");
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.EventId)
                  .HasMaxLength(255);
            entity.Property(e => e.Type)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.Property(e => e.ProcessedAt)
                  .IsRequired();
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
