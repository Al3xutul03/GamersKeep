using BusinessLogic.Models.Email;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Background asynchronous queue for outbound email messages.
/// </summary>
public interface IEmailQueue
{
    /// <summary>
    /// Enqueues an email message for background transmission.
    /// </summary>
    public ValueTask EnqueueAsync(EmailMessage message);

    /// <summary>
    /// Reads the next available email message from the queue.
    /// </summary>
    public ValueTask<EmailMessage> DequeueAsync(CancellationToken cancellationToken);
}
