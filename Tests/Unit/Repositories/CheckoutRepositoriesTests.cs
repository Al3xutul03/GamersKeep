using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Context;
using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories;

namespace Tests.Unit.Repositories;

[TestClass]
[TestCategory("Unit")]
public class CheckoutRepositoriesTests
{
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private static Order NewOrder(ulong id, ulong userId, string number, DateTime createdAt) => new()
    {
        Id = id,
        OrderNumber = number,
        UserId = userId,
        DeliveryMethodId = 1,
        DeliveryMethodName = "Curier standard",
        DeliveryCost = 15m,
        Subtotal = 100m,
        Total = 115m,
        ShippingFullName = "Test User",
        ShippingPhone = "0700000000",
        ShippingStreet = "Str. Test 1",
        ShippingCity = "Iasi",
        ShippingCounty = "Iasi",
        ShippingPostalCode = "700000",
        CreatedAt = createdAt,
        UpdatedAt = createdAt,
        Items = [new OrderItem { ProductId = 1, ProductName = "Catan", UnitPrice = 100m, Quantity = 1 }]
    };

    private static Address NewAddress(ulong id, ulong userId, bool isDefault = false, DateTime? deletedAt = null) => new()
    {
        Id = id,
        UserId = userId,
        FullName = "Test User",
        Phone = "0700000000",
        Street = $"Str. Test {id}",
        City = "Iasi",
        County = "Iasi",
        PostalCode = "700000",
        IsDefault = isDefault,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        DeletedAt = deletedAt
    };

    [TestMethod]
    public async Task OrderRepository_GetByUserIdAsync_ReturnsOnlyUserOrdersNewestFirstWithItems()
    {
        var now = DateTime.UtcNow;
        _context.Orders.AddRange(
            NewOrder(1, userId: 1, "GK-A", now.AddDays(-2)),
            NewOrder(2, userId: 1, "GK-B", now),
            NewOrder(3, userId: 2, "GK-C", now.AddDays(-1)));
        await _context.SaveChangesAsync();

        var orders = (await new OrderRepository(_context).GetByUserIdAsync(1)).ToList();

        CollectionAssert.AreEqual(new[] { "GK-B", "GK-A" }, orders.Select(o => o.OrderNumber).ToArray());
        Assert.IsTrue(orders.All(o => o.Items.Count == 1));
    }

    [TestMethod]
    public async Task OrderRepository_GetUserOrderByNumberAsync_DoesNotReturnOtherUsersOrders()
    {
        _context.Orders.Add(NewOrder(1, userId: 2, "GK-OTHER", DateTime.UtcNow));
        await _context.SaveChangesAsync();
        var repo = new OrderRepository(_context);

        Assert.IsNull(await repo.GetUserOrderByNumberAsync(1, "GK-OTHER"));
        Assert.IsNotNull(await repo.GetUserOrderByNumberAsync(2, "GK-OTHER"));
    }

    [TestMethod]
    public async Task OrderRepository_GetByStripeSessionIdAsync_FindsOrder()
    {
        var order = NewOrder(1, userId: 1, "GK-S", DateTime.UtcNow);
        order.StripeCheckoutSessionId = "cs_test_123";
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var found = await new OrderRepository(_context).GetByStripeSessionIdAsync("cs_test_123");

        Assert.IsNotNull(found);
        Assert.AreEqual(OrderStatusType.Unpaid, found.Status);
        Assert.HasCount(1, found.Items);
    }

    [TestMethod]
    public async Task OrderRepository_OrderNumberExistsAsync_ReportsCorrectly()
    {
        _context.Orders.Add(NewOrder(1, userId: 1, "GK-EXISTS", DateTime.UtcNow));
        await _context.SaveChangesAsync();
        var repo = new OrderRepository(_context);

        Assert.IsTrue(await repo.OrderNumberExistsAsync("GK-EXISTS"));
        Assert.IsFalse(await repo.OrderNumberExistsAsync("GK-MISSING"));
    }

    [TestMethod]
    public async Task AddressRepository_GetByUserIdAsync_ExcludesDeletedAndOtherUsers_DefaultFirst()
    {
        _context.Addresses.AddRange(
            NewAddress(1, userId: 1),
            NewAddress(2, userId: 1, isDefault: true),
            NewAddress(3, userId: 1, deletedAt: DateTime.UtcNow),
            NewAddress(4, userId: 2));
        await _context.SaveChangesAsync();

        var addresses = (await new AddressRepository(_context).GetByUserIdAsync(1)).ToList();

        CollectionAssert.AreEqual(new ulong[] { 2, 1 }, addresses.Select(a => a.Id).ToArray());
    }

    [TestMethod]
    public async Task AddressRepository_GetUserAddressAsync_EnforcesOwnership()
    {
        _context.Addresses.Add(NewAddress(1, userId: 2));
        await _context.SaveChangesAsync();
        var repo = new AddressRepository(_context);

        Assert.IsNull(await repo.GetUserAddressAsync(1, 1));
        Assert.IsNotNull(await repo.GetUserAddressAsync(2, 1));
    }

    [TestMethod]
    public async Task DeliveryMethodRepository_GetActiveAsync_ReturnsActiveOrderedBySortOrder()
    {
        var now = DateTime.UtcNow;
        _context.DeliveryMethods.AddRange(
            new DeliveryMethod { Id = 1, Name = "Express", Description = "d", EstimatedDelivery = "1", Price = 25m, SortOrder = 2, IsActive = true, CreatedAt = now, UpdatedAt = now },
            new DeliveryMethod { Id = 2, Name = "Standard", Description = "d", EstimatedDelivery = "3", Price = 15m, SortOrder = 1, IsActive = true, CreatedAt = now, UpdatedAt = now },
            new DeliveryMethod { Id = 3, Name = "Inactive", Description = "d", EstimatedDelivery = "5", Price = 5m, SortOrder = 0, IsActive = false, CreatedAt = now, UpdatedAt = now });
        await _context.SaveChangesAsync();

        var methods = (await new DeliveryMethodRepository(_context).GetActiveAsync()).ToList();

        CollectionAssert.AreEqual(new[] { "Standard", "Express" }, methods.Select(m => m.Name).ToArray());
    }

    [TestMethod]
    public async Task ProcessedStripeEventRepository_ExistsAsync_ReportsCorrectly()
    {
        _context.ProcessedStripeEvents.Add(new ProcessedStripeEvent
        {
            EventId = "evt_1",
            Type = "checkout.session.completed",
            ProcessedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();
        var repo = new ProcessedStripeEventRepository(_context);

        Assert.IsTrue(await repo.ExistsAsync("evt_1"));
        Assert.IsFalse(await repo.ExistsAsync("evt_2"));
    }
}
