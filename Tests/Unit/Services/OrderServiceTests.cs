using AutoMapper;
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
public class OrderServiceTests
{
    private AppDbContext _context = null!;
    private IMapper _mapper = null!;
    private OrderRepository _orderRepo = null!;
    private ProductRepository _productRepo = null!;
    private StockService _stockService = null!;
    private OrderService _orderService = null!;

    [TestInitialize]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _orderRepo = new OrderRepository(_context);
        _productRepo = new ProductRepository(_context);
        _stockService = new StockService(_orderRepo, _productRepo, NullLogger<StockService>.Instance);

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));
        var provider = services.BuildServiceProvider();
        _mapper = provider.GetRequiredService<IMapper>();

        _orderService = new OrderService(_mapper, _orderRepo, _stockService, NullLogger<OrderService>.Instance);

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
            Stock = 10,
            Description = "Trading game",
            AgeRating = AgeRatingType.SevenPlus,
            CreatedAt = now,
            UpdatedAt = now
        };
        _context.Products.Add(product);

        var order1 = new Order
        {
            Id = 1,
            OrderNumber = "GK-20261103-ORD001",
            UserId = 1,
            Status = OrderStatusType.Paid,
            DeliveryMethodId = 1,
            DeliveryMethodName = "Curier standard",
            DeliveryCost = 15m,
            Subtotal = 300m,
            Total = 315m,
            Currency = "RON",
            ShippingFullName = "Alex Pop",
            ShippingPhone = "0700111222",
            ShippingStreet = "Str. 1",
            ShippingCity = "Iași",
            ShippingCounty = "Iași",
            ShippingPostalCode = "700001",
            StockDeducted = true,
            PaidAt = now.AddHours(-1),
            CreatedAt = now.AddHours(-2),
            UpdatedAt = now.AddHours(-1),
            Items = [new OrderItem { OrderId = 1, ProductId = 1, ProductName = "Catan", UnitPrice = 150m, Quantity = 2 }]
        };

        var order2 = new Order
        {
            Id = 2,
            OrderNumber = "GK-20261103-ORD002",
            UserId = 1,
            Status = OrderStatusType.Unpaid,
            DeliveryMethodId = 1,
            DeliveryMethodName = "Curier standard",
            DeliveryCost = 15m,
            Subtotal = 150m,
            Total = 165m,
            Currency = "RON",
            ShippingFullName = "Alex Pop",
            ShippingPhone = "0700111222",
            ShippingStreet = "Str. 1",
            ShippingCity = "Iași",
            ShippingCounty = "Iași",
            ShippingPostalCode = "700001",
            StockDeducted = false,
            CreatedAt = now,
            UpdatedAt = now,
            Items = [new OrderItem { OrderId = 2, ProductId = 1, ProductName = "Catan", UnitPrice = 150m, Quantity = 1 }]
        };

        _context.Orders.AddRange(order1, order2);
        await _context.SaveChangesAsync();
    }

    [TestMethod]
    public async Task GetUserOrdersAsync_ReturnsOrdersWithDateProductsTotalAndStatus()
    {
        // Requirement: "Customers can view their orders, with date, products, total and status."
        var orders = await _orderService.GetUserOrdersAsync(1);

        Assert.HasCount(2, orders);

        var first = orders[0];
        Assert.AreEqual("GK-20261103-ORD002", first.OrderNumber);
        Assert.AreEqual(OrderStatusType.Unpaid, first.Status);
        Assert.AreEqual(165m, first.Total);
        Assert.HasCount(1, first.Items);

        var second = orders[1];
        Assert.AreEqual("GK-20261103-ORD001", second.OrderNumber);
        Assert.AreEqual(OrderStatusType.Paid, second.Status);
        Assert.AreEqual(315m, second.Total);
    }

    [TestMethod]
    public async Task GetOrderByNumberAsync_EnforcesOwnership()
    {
        var foundForUser1 = await _orderService.GetOrderByNumberAsync(1, "GK-20261103-ORD001");
        var foundForUser2 = await _orderService.GetOrderByNumberAsync(2, "GK-20261103-ORD001");

        Assert.IsNotNull(foundForUser1);
        Assert.IsNull(foundForUser2);
    }

    [TestMethod]
    public async Task CancelOrderAsync_RestoresStockIfPreviouslyDeducted()
    {
        // Order 1 is Paid and has StockDeducted == true, with 2 units of product 1
        var initialStock = (await _context.Products.FindAsync(1ul))!.Stock;
        Assert.AreEqual(10u, initialStock);

        var cancelled = await _orderService.CancelOrderAsync(userId: 1, orderId: 1);
        Assert.IsTrue(cancelled);

        var orderAfter = await _orderRepo.GetWithItemsAsync(1);
        Assert.IsNotNull(orderAfter);
        Assert.AreEqual(OrderStatusType.Cancelled, orderAfter.Status);
        Assert.IsFalse(orderAfter.StockDeducted);

        // Product stock restored from 10 to 12
        var productAfter = await _context.Products.FindAsync(1ul);
        Assert.AreEqual(12u, productAfter!.Stock);
    }
}
