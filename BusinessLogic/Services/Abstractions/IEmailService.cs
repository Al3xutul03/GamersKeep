using Repository.Entity;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// High-level email service for generating and queuing domain transactional emails
/// (welcome email, order payment confirmation, order cancellation).
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Queues a personalized welcome email upon new user registration.
    /// </summary>
    /// <param name="userEmail">Recipient email address.</param>
    /// <param name="userName">Recipient user display name.</param>
    public Task SendWelcomeEmailAsync(string userEmail, string userName);

    /// <summary>
    /// Queues an itemized order confirmation receipt upon successful payment confirmation.
    /// </summary>
    /// <param name="order">The confirmed order with its items.</param>
    /// <param name="userEmail">Recipient email address.</param>
    public Task SendOrderConfirmationEmailAsync(Order order, string userEmail);

    /// <summary>
    /// Queues an order cancellation notice.
    /// </summary>
    /// <param name="order">The cancelled order.</param>
    /// <param name="userEmail">Recipient email address.</param>
    public Task SendOrderCancellationEmailAsync(Order order, string userEmail);
}
