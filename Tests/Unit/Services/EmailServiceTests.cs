using BusinessLogic.Models.Email;
using BusinessLogic.Options;
using BusinessLogic.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Entity;
using Repository.Enums.Types;

namespace Tests.Unit.Services;

[TestClass]
[TestCategory("Unit")]
public class EmailServiceTests
{
    private EmailQueue _emailQueue = null!;
    private EmailService _emailService = null!;

    [TestInitialize]
    public void Setup()
    {
        _emailQueue = new EmailQueue();
        _emailService = new EmailService(_emailQueue, NullLogger<EmailService>.Instance);
    }

    [TestMethod]
    public async Task SendWelcomeEmailAsync_EnqueuesPersonalizedMessage()
    {
        await _emailService.SendWelcomeEmailAsync("alex@example.com", "Alex Pop");

        var msg = await _emailQueue.DequeueAsync(CancellationToken.None);

        Assert.IsNotNull(msg);
        Assert.AreEqual("alex@example.com", msg.ToEmail);
        Assert.AreEqual("Alex Pop", msg.ToName);
        Assert.IsTrue(msg.Subject.Contains("Bun venit"));
        Assert.IsTrue(msg.HtmlBody.Contains("Alex Pop"));
        Assert.IsTrue(msg.HtmlBody.Contains("GAMERSKEEP"));
    }

    [TestMethod]
    public async Task SendOrderConfirmationEmailAsync_EnqueuesItemizedReceipt()
    {
        var now = DateTime.UtcNow;
        var order = new Order
        {
            Id = 1,
            OrderNumber = "GK-20261103-EMAIL1",
            UserId = 1,
            Status = OrderStatusType.Paid,
            DeliveryMethodName = "Curier express",
            DeliveryCost = 25m,
            Subtotal = 150m,
            Total = 175m,
            ShippingFullName = "Ioana Radu",
            ShippingPhone = "0712345678",
            ShippingStreet = "Str. Florilor 12",
            ShippingCity = "Cluj-Napoca",
            ShippingCounty = "Cluj",
            ShippingPostalCode = "400001",
            CreatedAt = now,
            UpdatedAt = now,
            Items = [new OrderItem { OrderId = 1, ProductId = 1, ProductName = "Catan", UnitPrice = 150m, Quantity = 1 }]
        };

        await _emailService.SendOrderConfirmationEmailAsync(order, "ioana@example.com");

        var msg = await _emailQueue.DequeueAsync(CancellationToken.None);

        Assert.IsNotNull(msg);
        Assert.AreEqual("ioana@example.com", msg.ToEmail);
        Assert.IsTrue(msg.Subject.Contains("GK-20261103-EMAIL1"));
        Assert.IsTrue(msg.HtmlBody.Contains("GK-20261103-EMAIL1"));
        Assert.IsTrue(msg.HtmlBody.Contains("Catan"));
        Assert.IsTrue(msg.HtmlBody.Contains("175.00 RON"));
        Assert.IsTrue(msg.HtmlBody.Contains("Curier express"));
        Assert.IsTrue(msg.HtmlBody.Contains("Cluj-Napoca"));
    }

    [TestMethod]
    public async Task SendOrderCancellationEmailAsync_EnqueuesCancellationNotice()
    {
        var order = new Order
        {
            Id = 1,
            OrderNumber = "GK-20261103-CANCEL1",
            UserId = 1,
            Status = OrderStatusType.Cancelled,
            DeliveryMethodName = "Curier standard",
            Total = 150m,
            ShippingFullName = "Ioana Radu",
            ShippingPhone = "0712345678",
            ShippingStreet = "Str. 1",
            ShippingCity = "Cluj",
            ShippingCounty = "Cluj",
            ShippingPostalCode = "400001",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _emailService.SendOrderCancellationEmailAsync(order, "ioana@example.com");

        var msg = await _emailQueue.DequeueAsync(CancellationToken.None);

        Assert.IsNotNull(msg);
        Assert.AreEqual("ioana@example.com", msg.ToEmail);
        Assert.IsTrue(msg.Subject.Contains("anulată"));
        Assert.IsTrue(msg.HtmlBody.Contains("GK-20261103-CANCEL1"));
    }

    [TestMethod]
    public async Task MailtrapSmtpEmailSender_SimulationMode_DoesNotThrow()
    {
        var options = Options.Create(new MailtrapOptions
        {
            Host = "sandbox.smtp.mailtrap.io",
            Port = 2525,
            Username = "YOUR_MAILTRAP_USERNAME", // Placeholder activates simulation mode
            Password = "YOUR_MAILTRAP_PASSWORD"
        });

        var sender = new MailtrapSmtpEmailSender(options, NullLogger<MailtrapSmtpEmailSender>.Instance);
        var message = new EmailMessage("test@example.com", "Test User", "Subject", "<p>Body</p>");

        // Does not throw connection failure exception in simulation mode
        await sender.SendEmailAsync(message);
    }
}
