using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Mapper;
using BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Context;
using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories;

namespace Tests.Unit.Services;

[TestClass]
[TestCategory("Unit")]
public class CheckoutServiceTests
{
    private AppDbContext _context = null!;
    private IMapper _mapper = null!;
    private CartEntryRepository _cartRepo = null!;
    private ProductRepository _productRepo = null!;
    private AddressRepository _addressRepo = null!;
    private DeliveryMethodRepository _deliveryRepo = null!;
    private OrderRepository _orderRepo = null!;
    private CartService _cartService = null!;
    private CheckoutService _checkoutService = null!;

    [TestInitialize]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _cartRepo = new CartEntryRepository(_context);
        _productRepo = new ProductRepository(_context);
        _addressRepo = new AddressRepository(_context);
        _deliveryRepo = new DeliveryMethodRepository(_context);
        _orderRepo = new OrderRepository(_context);

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));
        var provider = services.BuildServiceProvider();
        _mapper = provider.GetRequiredService<IMapper>();

        _cartService = new CartService(_mapper, _cartRepo, _productRepo);
        _checkoutService = new CheckoutService(
            _cartService,
            _addressRepo,
            _deliveryRepo,
            _orderRepo,
            _mapper,
            NullLogger<CheckoutService>.Instance);

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

        var product = new Product
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
        _context.Products.Add(product);

        var standardDelivery = new DeliveryMethod
        {
            Id = 1,
            Name = "Curier standard",
            Description = "2-4 zile lucrătoare",
            EstimatedDelivery = "2-4 zile",
            Price = 15m,
            IsActive = true,
            SortOrder = 1,
            CreatedAt = now,
            UpdatedAt = now
        };
        var expressDelivery = new DeliveryMethod
        {
            Id = 2,
            Name = "Curier express",
            Description = "1 zi lucrătoare",
            EstimatedDelivery = "1 zi",
            Price = 25m,
            IsActive = true,
            SortOrder = 2,
            CreatedAt = now,
            UpdatedAt = now
        };
        _context.DeliveryMethods.AddRange(standardDelivery, expressDelivery);

        var address = new Address
        {
            Id = 1,
            UserId = 1,
            FullName = "Maria Ionescu",
            Phone = "0711222333",
            Street = "Bd. Stefan cel Mare 1",
            City = "Iași",
            County = "Iași",
            PostalCode = "700001",
            IsDefault = true,
            CreatedAt = now,
            UpdatedAt = now
        };
        _context.Addresses.Add(address);

        await _context.SaveChangesAsync();
    }

    [TestMethod]
    public async Task GetCheckoutSummaryAsync_CalculatesTotalsCorrectly()
    {
        await _cartService.AddOrUpdateItemAsync(userId: 1, productId: 1, quantity: 2);

        var summary = await _checkoutService.GetCheckoutSummaryAsync(userId: 1, addressId: 1, deliveryMethodId: 1);

        Assert.HasCount(1, summary.Items);
        Assert.AreEqual(300m, summary.Subtotal);
        Assert.AreEqual(15m, summary.DeliveryCost);
        Assert.AreEqual(315m, summary.GrandTotal);
        Assert.AreEqual("Maria Ionescu", summary.ShippingAddress.FullName);
        Assert.AreEqual("Curier standard", summary.DeliveryMethod.Name);
    }

    [TestMethod]
    public async Task PlaceOrderAsync_CreatesOrderInUnpaidStatusWithSnapshotsAndUniqueOrderNumber()
    {
        await _cartService.AddOrUpdateItemAsync(userId: 1, productId: 1, quantity: 2);

        var result = await _checkoutService.PlaceOrderAsync(userId: 1, addressId: 1, deliveryMethodId: 1);

        Assert.IsGreaterThan(0ul, result.OrderId);
        Assert.StartsWith("GK-", result.OrderNumber);
        Assert.AreEqual(315m, result.Total);

        var savedOrder = await _orderRepo.GetWithItemsAsync(result.OrderId);
        Assert.IsNotNull(savedOrder);
        Assert.AreEqual(OrderStatusType.Unpaid, savedOrder.Status);
        Assert.AreEqual("Curier standard", savedOrder.DeliveryMethodName);
        Assert.AreEqual(15m, savedOrder.DeliveryCost);
        Assert.AreEqual(300m, savedOrder.Subtotal);
        Assert.AreEqual(315m, savedOrder.Total);
        Assert.AreEqual("Maria Ionescu", savedOrder.ShippingFullName);
        Assert.IsFalse(savedOrder.StockDeducted);

        Assert.HasCount(1, savedOrder.Items);
        var item = savedOrder.Items.First();
        Assert.AreEqual(1ul, item.ProductId);
        Assert.AreEqual("Catan", item.ProductName);
        Assert.AreEqual(150m, item.UnitPrice);
        Assert.AreEqual(2u, item.Quantity);
    }

    [TestMethod]
    public async Task PlaceOrderAsync_EmptyCart_ThrowsInvalidOperationException()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _checkoutService.PlaceOrderAsync(userId: 1, addressId: 1, deliveryMethodId: 1));
    }

    [TestMethod]
    public async Task PlaceOrderAsync_StockInsufficient_ThrowsInsufficientStockException()
    {
        // Add 5 in cart (equal to stock)
        await _cartService.AddOrUpdateItemAsync(userId: 1, productId: 1, quantity: 5);

        // Another customer buys 2, leaving stock = 3
        var product = await _context.Products.FindAsync(1ul);
        product!.Stock = 3;
        await _context.SaveChangesAsync();

        var ex = await Assert.ThrowsAsync<InsufficientStockException>(() =>
            _checkoutService.PlaceOrderAsync(userId: 1, addressId: 1, deliveryMethodId: 1));

        Assert.AreEqual(1ul, ex.ProductId);
        Assert.AreEqual(5u, ex.RequestedQuantity);
        Assert.AreEqual(3u, ex.AvailableStock);
    }
}
