using BusinessLogic.Exceptions;
using BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Context;
using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories;

namespace Tests.Unit.Services;

[TestClass]
[TestCategory("Unit")]
public class StockServiceTests
{
    private AppDbContext _context = null!;
    private ProductRepository _productRepo = null!;
    private OrderRepository _orderRepo = null!;
    private StockService _stockService = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _productRepo = new ProductRepository(_context);
        _orderRepo = new OrderRepository(_context);
        _stockService = new StockService(_orderRepo, _productRepo, NullLogger<StockService>.Instance);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private static Product CreateProduct(ulong id, string name, uint stock) => new()
    {
        Id = id,
        Name = name,
        Description = "Sample product description",
        Price = 100m,
        Stock = stock,
        AgeRating = AgeRatingType.SevenPlus,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    private static Order CreateOrder(ulong id, string orderNumber, bool stockDeducted, params (ulong ProductId, string Name, uint Qty)[] items)
    {
        var now = DateTime.UtcNow;
        var order = new Order
        {
            Id = id,
            OrderNumber = orderNumber,
            UserId = 1,
            Status = OrderStatusType.Unpaid,
            DeliveryMethodId = 1,
            DeliveryMethodName = "Curier standard",
            DeliveryCost = 15m,
            Subtotal = 200m,
            Total = 215m,
            ShippingFullName = "Ion Popescu",
            ShippingPhone = "0712345678",
            ShippingStreet = "Str. Lalelelor 10",
            ShippingCity = "Bucuresti",
            ShippingCounty = "Bucuresti",
            ShippingPostalCode = "010101",
            StockDeducted = stockDeducted,
            CreatedAt = now,
            UpdatedAt = now
        };

        foreach (var (productId, name, qty) in items)
        {
            order.Items.Add(new OrderItem
            {
                OrderId = id,
                ProductId = productId,
                ProductName = name,
                UnitPrice = 100m,
                Quantity = qty
            });
        }

        return order;
    }

    [TestMethod]
    public async Task DecreaseStockForOrderAsync_SufficientStock_DeductsStockAndSetsFlag()
    {
        var p1 = CreateProduct(1, "Catan", 10);
        var p2 = CreateProduct(2, "Carcassonne", 5);
        _context.Products.AddRange(p1, p2);

        var order = CreateOrder(1, "GK-TEST-01", stockDeducted: false, (1, "Catan", 3), (2, "Carcassonne", 2));
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        await _stockService.DecreaseStockForOrderAsync(1);

        var reloadedP1 = await _context.Products.FindAsync(1ul);
        var reloadedP2 = await _context.Products.FindAsync(2ul);
        var reloadedOrder = await _context.Orders.FindAsync(1ul);

        Assert.IsNotNull(reloadedP1);
        Assert.IsNotNull(reloadedP2);
        Assert.IsNotNull(reloadedOrder);
        Assert.AreEqual(7u, reloadedP1.Stock);
        Assert.AreEqual(3u, reloadedP2.Stock);
        Assert.IsTrue(reloadedOrder.StockDeducted);
    }

    [TestMethod]
    public async Task DecreaseStockForOrderAsync_IsIdempotent_DoesNotDoubleDeduct()
    {
        var p1 = CreateProduct(1, "Catan", 10);
        _context.Products.Add(p1);

        var order = CreateOrder(1, "GK-TEST-02", stockDeducted: true, (1, "Catan", 3));
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        await _stockService.DecreaseStockForOrderAsync(1);

        var reloadedP1 = await _context.Products.FindAsync(1ul);
        Assert.IsNotNull(reloadedP1);
        Assert.AreEqual(10u, reloadedP1.Stock); // Did not deduct because flag was already true
    }

    [TestMethod]
    public async Task DecreaseStockForOrderAsync_InsufficientStock_ThrowsAndLeavesStockUnchanged()
    {
        var p1 = CreateProduct(1, "Catan", 2);
        var p2 = CreateProduct(2, "Carcassonne", 5);
        _context.Products.AddRange(p1, p2);

        // Requested 3 of Catan, but only 2 available
        var order = CreateOrder(1, "GK-TEST-03", stockDeducted: false, (1, "Catan", 3), (2, "Carcassonne", 1));
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var ex = await Assert.ThrowsAsync<InsufficientStockException>(() => _stockService.DecreaseStockForOrderAsync(1));
        Assert.AreEqual(1ul, ex.ProductId);

        var reloadedP1 = await _context.Products.FindAsync(1ul);
        var reloadedP2 = await _context.Products.FindAsync(2ul);
        var reloadedOrder = await _context.Orders.FindAsync(1ul);

        Assert.IsNotNull(reloadedP1);
        Assert.IsNotNull(reloadedP2);
        Assert.IsNotNull(reloadedOrder);
        Assert.AreEqual(2u, reloadedP1.Stock); // Intact
        Assert.AreEqual(5u, reloadedP2.Stock); // Intact, rolled back
        Assert.IsFalse(reloadedOrder.StockDeducted);
    }

    [TestMethod]
    public async Task DecreaseStockForOrderAsync_TwoOrdersCompeteForLastUnit_OnlyOneSucceeds()
    {
        // Requirement: "Two customers cannot purchase the last unit at the same time."
        var p1 = CreateProduct(1, "Catan Limited Edition", 1);
        _context.Products.Add(p1);

        var order1 = CreateOrder(1, "GK-ORD-1", stockDeducted: false, (1, "Catan Limited Edition", 1));
        var order2 = CreateOrder(2, "GK-ORD-2", stockDeducted: false, (1, "Catan Limited Edition", 1));
        _context.Orders.AddRange(order1, order2);
        await _context.SaveChangesAsync();

        // Customer 1's payment is confirmed first
        await _stockService.DecreaseStockForOrderAsync(1);
        var reloadedP1 = await _context.Products.FindAsync(1ul);
        Assert.AreEqual(0u, reloadedP1!.Stock);

        // Customer 2's payment confirmation arrives, but stock is already exhausted
        var ex = await Assert.ThrowsAsync<InsufficientStockException>(() => _stockService.DecreaseStockForOrderAsync(2));
        Assert.AreEqual(1ul, ex.ProductId);

        var finalP1 = await _context.Products.FindAsync(1ul);
        Assert.AreEqual(0u, finalP1!.Stock);
    }

    [TestMethod]
    public async Task RestoreStockForOrderAsync_PreviouslyDeducted_RestoresStockAndResetsFlag()
    {
        // Requirement: "restored when an order is cancelled."
        var p1 = CreateProduct(1, "Catan", 5);
        _context.Products.Add(p1);

        var order = CreateOrder(1, "GK-TEST-05", stockDeducted: true, (1, "Catan", 3));
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        await _stockService.RestoreStockForOrderAsync(1);

        var reloadedP1 = await _context.Products.FindAsync(1ul);
        var reloadedOrder = await _context.Orders.FindAsync(1ul);

        Assert.IsNotNull(reloadedP1);
        Assert.IsNotNull(reloadedOrder);
        Assert.AreEqual(8u, reloadedP1.Stock);
        Assert.IsFalse(reloadedOrder.StockDeducted);
    }

    [TestMethod]
    public async Task RestoreStockForOrderAsync_IsIdempotent_DoesNotDoubleRestore()
    {
        var p1 = CreateProduct(1, "Catan", 5);
        _context.Products.Add(p1);

        var order = CreateOrder(1, "GK-TEST-06", stockDeducted: true, (1, "Catan", 3));
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // First restore
        await _stockService.RestoreStockForOrderAsync(1);
        var afterFirst = (await _context.Products.FindAsync(1ul))!.Stock;
        Assert.AreEqual(8u, afterFirst);

        // Second restore attempt
        await _stockService.RestoreStockForOrderAsync(1);
        var afterSecond = (await _context.Products.FindAsync(1ul))!.Stock;
        Assert.AreEqual(8u, afterSecond); // Still 8, did not double restore
    }

    [TestMethod]
    public async Task RestoreStockForOrderAsync_NeverDeducted_DoesNothing()
    {
        // E.g. cancelled unpaid order where stock was never reduced
        var p1 = CreateProduct(1, "Catan", 10);
        _context.Products.Add(p1);

        var order = CreateOrder(1, "GK-TEST-07", stockDeducted: false, (1, "Catan", 2));
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        await _stockService.RestoreStockForOrderAsync(1);

        var reloadedP1 = await _context.Products.FindAsync(1ul);
        Assert.AreEqual(10u, reloadedP1!.Stock);
    }

    [TestMethod]
    public async Task CheckStockAvailabilityAsync_ReportsCorrectAvailabilityPerItem()
    {
        var p1 = CreateProduct(1, "Catan", 5);
        var p2 = CreateProduct(2, "Carcassonne", 1);
        _context.Products.AddRange(p1, p2);
        await _context.SaveChangesAsync();

        var result = await _stockService.CheckStockAvailabilityAsync([
            (1ul, 3u), // Available (5 >= 3)
            (2ul, 2u)  // Unavailable (1 < 2)
        ]);

        Assert.IsFalse(result.IsAllAvailable);
        Assert.HasCount(2, result.Items);

        var item1 = result.Items.First(i => i.ProductId == 1);
        Assert.IsTrue(item1.IsAvailable);
        Assert.AreEqual(5u, item1.AvailableStock);
        Assert.AreEqual(3u, item1.RequestedQuantity);

        var item2 = result.Items.First(i => i.ProductId == 2);
        Assert.IsFalse(item2.IsAvailable);
        Assert.AreEqual(1u, item2.AvailableStock);
        Assert.AreEqual(2u, item2.RequestedQuantity);
    }

    [TestMethod]
    public async Task IsQuantityAvailableAsync_ReturnsExpectedResult()
    {
        var p1 = CreateProduct(1, "Catan", 5);
        _context.Products.Add(p1);
        await _context.SaveChangesAsync();

        Assert.IsTrue(await _stockService.IsQuantityAvailableAsync(1, 5));
        Assert.IsTrue(await _stockService.IsQuantityAvailableAsync(1, 4));
        Assert.IsFalse(await _stockService.IsQuantityAvailableAsync(1, 6));
        Assert.IsFalse(await _stockService.IsQuantityAvailableAsync(999, 1));
    }
}
