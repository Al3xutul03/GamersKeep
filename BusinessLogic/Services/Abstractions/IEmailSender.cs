using BusinessLogic.Models.Email;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Sender abstraction for dispatching email messages via SMTP or email API.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends an email message asynchronously.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken = default);
}
