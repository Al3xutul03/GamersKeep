using AutoMapper;
using BusinessLogic.Mapper;
using BusinessLogic.Options;
using BusinessLogic.Services;
using BusinessLogic.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Context;
using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories;

namespace Tests.Unit.Services;

[TestClass]
[TestCategory("Unit")]
public class StripeWebhookServiceTests
{
    private AppDbContext _context = null!;
    private OrderRepository _orderRepo = null!;
    private ProductRepository _productRepo = null!;
    private CartEntryRepository _cartRepo = null!;
    private ProcessedStripeEventRepository _processedEventRepo = null!;
    private StockService _stockService = null!;
    private CartService _cartService = null!;
    private StripeWebhookService _webhookService = null!;
    private StripePaymentGateway _paymentGateway = null!;

    [TestInitialize]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _orderRepo = new OrderRepository(_context);
        _productRepo = new ProductRepository(_context);
        _cartRepo = new CartEntryRepository(_context);
        _processedEventRepo = new ProcessedStripeEventRepository(_context);

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));
        var provider = services.BuildServiceProvider();
        var mapper = provider.GetRequiredService<IMapper>();

        _stockService = new StockService(_orderRepo, _productRepo, NullLogger<StockService>.Instance);
        _cartService = new CartService(mapper, _cartRepo, _productRepo);

        var stripeOptions = Options.Create(new StripeOptions
        {
            PublishableKey = "pk_test_placeholder",
            SecretKey = "sk_test_placeholder",
            WebhookSecret = string.Empty // Empty secret activates raw parsing in test environment
        });

        _webhookService = new StripeWebhookService(
            _orderRepo,
            _cartService,
            _stockService,
            _processedEventRepo,
            stripeOptions,
            NullLogger<StripeWebhookService>.Instance);

        _paymentGateway = new StripePaymentGateway(
            stripeOptions,
            _orderRepo,
            NullLogger<StripePaymentGateway>.Instance);

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
            Description = "Trade and build",
            AgeRating = AgeRatingType.SevenPlus,
            CreatedAt = now,
            UpdatedAt = now
        };
        _context.Products.Add(product);

        // Active cart entry for user 1
        _context.CartEntries.Add(new CartEntry
        {
            UserId = 1,
            ProductId = 1,
            Quantity = 2,
            CreatedAt = now,
            UpdatedAt = now
        });

        // Unpaid order for user 1
        var order = new Order
        {
            Id = 1,
            OrderNumber = "GK-20261103-TEST01",
            UserId = 1,
            Status = OrderStatusType.Unpaid,
            DeliveryMethodId = 1,
            DeliveryMethodName = "Curier standard",
            DeliveryCost = 15m,
            Subtotal = 300m,
            Total = 315m,
            Currency = "RON",
            ShippingFullName = "Ana Pop",
            ShippingPhone = "0700000001",
            ShippingStreet = "Str. Libertatii 1",
            ShippingCity = "Iasi",
            ShippingCounty = "Iasi",
            ShippingPostalCode = "700000",
            StockDeducted = false,
            CreatedAt = now,
            UpdatedAt = now,
            Items = [new OrderItem { OrderId = 1, ProductId = 1, ProductName = "Catan", UnitPrice = 150m, Quantity = 2 }]
        };
        _context.Orders.Add(order);

        await _context.SaveChangesAsync();
    }

    private static string BuildCheckoutSessionCompletedJson(string eventId, string sessionId, ulong orderId, string paymentStatus = "paid") => $$"""
    {
      "id": "{{eventId}}",
      "object": "event",
      "type": "checkout.session.completed",
      "data": {
        "object": {
          "id": "{{sessionId}}",
          "object": "checkout.session",
          "payment_status": "{{paymentStatus}}",
          "payment_intent": "pi_test_12345",
          "client_reference_id": "{{orderId}}",
          "metadata": {
            "orderId": "{{orderId}}",
            "userId": "1"
          }
        }
      }
    }
    """;

    [TestMethod]
    public async Task HandleWebhookEventAsync_ValidPaidCheckoutSession_DecreasesStockMarksPaidAndClearsCart()
    {
        // Initial stock is 10
        var pBefore = await _context.Products.FindAsync(1ul);
        Assert.AreEqual(10u, pBefore!.Stock);

        var json = BuildCheckoutSessionCompletedJson("evt_test_01", "cs_test_01", orderId: 1, paymentStatus: "paid");

        var result = await _webhookService.HandleWebhookEventAsync(json, stripeSignatureHeader: null);

        Assert.AreEqual(WebhookResultStatus.Success, result.Status);

        // 1. Order marked as Paid
        var order = await _orderRepo.GetWithItemsAsync(1);
        Assert.IsNotNull(order);
        Assert.AreEqual(OrderStatusType.Paid, order.Status);
        Assert.IsNotNull(order.PaidAt);
        Assert.AreEqual("pi_test_12345", order.StripePaymentIntentId);
        Assert.IsTrue(order.StockDeducted);

        // 2. Stock atomically decreased (10 - 2 = 8)
        var pAfter = await _context.Products.FindAsync(1ul);
        Assert.AreEqual(8u, pAfter!.Stock);

        // 3. User cart cleared
        var userCart = await _cartRepo.GetByUserIdAsync(1);
        Assert.HasCount(0, userCart);

        // 4. Idempotency event recorded
        Assert.IsTrue(await _processedEventRepo.ExistsAsync("evt_test_01"));
    }

    [TestMethod]
    public async Task HandleWebhookEventAsync_DuplicateEvent_IsIdempotentAndSkipped()
    {
        var json = BuildCheckoutSessionCompletedJson("evt_test_02", "cs_test_02", orderId: 1, paymentStatus: "paid");

        // First delivery
        var firstResult = await _webhookService.HandleWebhookEventAsync(json, stripeSignatureHeader: null);
        Assert.AreEqual(WebhookResultStatus.Success, firstResult.Status);

        var pAfterFirst = (await _context.Products.FindAsync(1ul))!.Stock;
        Assert.AreEqual(8u, pAfterFirst);

        // Second delivery of same event
        var secondResult = await _webhookService.HandleWebhookEventAsync(json, stripeSignatureHeader: null);
        Assert.AreEqual(WebhookResultStatus.AlreadyProcessed, secondResult.Status);

        // Stock was not decremented again
        var pAfterSecond = (await _context.Products.FindAsync(1ul))!.Stock;
        Assert.AreEqual(8u, pAfterSecond);
    }

    [TestMethod]
    public async Task HandleWebhookEventAsync_UnpaidCheckoutSession_LeavesOrderUnpaid()
    {
        // Requirement: "A failed payment leaves the order Unpaid."
        var json = BuildCheckoutSessionCompletedJson("evt_test_03", "cs_test_03", orderId: 1, paymentStatus: "unpaid");

        var result = await _webhookService.HandleWebhookEventAsync(json, stripeSignatureHeader: null);

        Assert.AreEqual(WebhookResultStatus.Success, result.Status);

        var order = await _orderRepo.GetWithItemsAsync(1);
        Assert.IsNotNull(order);
        Assert.AreEqual(OrderStatusType.Unpaid, order.Status);
        Assert.IsFalse(order.StockDeducted);

        // Stock untouched
        var p = await _context.Products.FindAsync(1ul);
        Assert.AreEqual(10u, p!.Stock);
    }

    [TestMethod]
    public async Task HandleWebhookEventAsync_ExpiredCheckoutSession_LeavesOrderUnpaid()
    {
        var json = """
        {
          "id": "evt_test_expired_01",
          "object": "event",
          "type": "checkout.session.expired",
          "data": {
            "object": {
              "id": "cs_test_expired",
              "object": "checkout.session"
            }
          }
        }
        """;

        var result = await _webhookService.HandleWebhookEventAsync(json, stripeSignatureHeader: null);

        Assert.AreEqual(WebhookResultStatus.Success, result.Status);

        var order = await _orderRepo.GetWithItemsAsync(1);
        Assert.IsNotNull(order);
        Assert.AreEqual(OrderStatusType.Unpaid, order.Status);
        Assert.IsFalse(order.StockDeducted);
    }

    [TestMethod]
    public async Task StripePaymentGateway_SimulationMode_GeneratesSessionWithoutThrowing()
    {
        var order = await _orderRepo.GetWithItemsAsync(1);
        Assert.IsNotNull(order);

        var result = await _paymentGateway.CreateCheckoutSessionAsync(order, "customer@example.com");

        Assert.IsNotNull(result);
        Assert.StartsWith("cs_sim_", result.SessionId);
        Assert.Contains(result.SessionId, result.CheckoutUrl);

        var savedOrder = await _orderRepo.GetWithItemsAsync(1);
        Assert.AreEqual(result.SessionId, savedOrder!.StripeCheckoutSessionId);
    }
}
