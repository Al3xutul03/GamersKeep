using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Mapper;
using BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Context;
using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories;

namespace Tests.Unit.Services;

[TestClass]
[TestCategory("Unit")]
public class CartServiceTests
{
    private AppDbContext _context = null!;
    private IMapper _mapper = null!;
    private CartEntryRepository _cartRepo = null!;
    private ProductRepository _productRepo = null!;
    private CartService _cartService = null!;

    [TestInitialize]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _cartRepo = new CartEntryRepository(_context);
        _productRepo = new ProductRepository(_context);

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));
        var provider = services.BuildServiceProvider();
        _mapper = provider.GetRequiredService<IMapper>();

        _cartService = new CartService(_mapper, _cartRepo, _productRepo);

        await SeedTestDataAsync();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private async Task SeedTestDataAsync()
    {
        var now = DateTime.UtcNow;
        var p1 = new Product
        {
            Id = 1,
            Name = "Catan",
            Price = 150m,
            Stock = 5,
            Description = "Trade and build",
            AgeRating = AgeRatingType.SevenPlus,
            CreatedAt = now,
            UpdatedAt = now
        };
        _context.Products.Add(p1);
        await _context.SaveChangesAsync();
    }

    [TestMethod]
    public async Task AddOrUpdateItemAsync_QuantityAboveStock_ThrowsInsufficientStockException()
    {
        // Requirement: "Quantities above available stock are rejected."
        var ex = await Assert.ThrowsAsync<InsufficientStockException>(() =>
            _cartService.AddOrUpdateItemAsync(userId: 1, productId: 1, quantity: 6));

        Assert.AreEqual(1ul, ex.ProductId);
        Assert.AreEqual(6u, ex.RequestedQuantity);
        Assert.AreEqual(5u, ex.AvailableStock);

        var cart = await _cartService.GetUserCartAsync(1);
        Assert.HasCount(0, cart);
    }

    [TestMethod]
    public async Task AddOrUpdateItemAsync_IncrementalAdditionAboveStock_ThrowsInsufficientStockException()
    {
        // Add 3 first (valid)
        await _cartService.AddOrUpdateItemAsync(userId: 1, productId: 1, quantity: 3);

        // Try to add 3 more (total 6 > stock 5)
        var ex = await Assert.ThrowsAsync<InsufficientStockException>(() =>
            _cartService.AddOrUpdateItemAsync(userId: 1, productId: 1, quantity: 3));

        Assert.AreEqual(1ul, ex.ProductId);
        Assert.AreEqual(6u, ex.RequestedQuantity);
        Assert.AreEqual(5u, ex.AvailableStock);

        var cart = (await _cartService.GetUserCartAsync(1)).ToList();
        Assert.HasCount(1, cart);
        Assert.AreEqual(3u, cart[0].Quantity); // Still 3
    }

    [TestMethod]
    public async Task SetQuantityAsync_Zero_RemovesItemFromCart()
    {
        await _cartService.AddOrUpdateItemAsync(userId: 1, productId: 1, quantity: 2);
        Assert.HasCount(1, await _cartService.GetUserCartAsync(1));

        await _cartService.SetQuantityAsync(userId: 1, productId: 1, quantity: 0);
        Assert.HasCount(0, await _cartService.GetUserCartAsync(1));
    }

    [TestMethod]
    public async Task ClearCartAsync_RemovesAllItems()
    {
        await _cartService.AddOrUpdateItemAsync(userId: 1, productId: 1, quantity: 2);
        Assert.HasCount(1, await _cartService.GetUserCartAsync(1));

        await _cartService.ClearCartAsync(1);
        Assert.HasCount(0, await _cartService.GetUserCartAsync(1));
    }

    [TestMethod]
    public async Task ValidateCartAsync_FlagsItemsExceedingAvailableStock()
    {
        await _cartService.AddOrUpdateItemAsync(userId: 1, productId: 1, quantity: 4);

        // External stock change drops stock from 5 to 3
        var p = await _context.Products.FindAsync(1ul);
        p!.Stock = 3;
        await _context.SaveChangesAsync();

        var result = await _cartService.ValidateCartAsync(1);
        Assert.IsFalse(result.IsAllAvailable);
        Assert.HasCount(1, result.Items);
        Assert.IsFalse(result.Items[0].IsAvailable);
        Assert.AreEqual(4u, result.Items[0].RequestedQuantity);
        Assert.AreEqual(3u, result.Items[0].AvailableStock);
    }
}
