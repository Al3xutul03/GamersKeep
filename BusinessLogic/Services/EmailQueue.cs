using System.Threading.Channels;
using BusinessLogic.Models.Email;
using BusinessLogic.Services.Abstractions;

namespace BusinessLogic.Services;

/// <summary>
/// In-memory unbounded channel implementation of <see cref="IEmailQueue"/>.
/// </summary>
public class EmailQueue : IEmailQueue
{
    private readonly Channel<EmailMessage> _channel = Channel.CreateUnbounded<EmailMessage>(new UnboundedChannelOptions
    {
        SingleReader = true
    });

    /// <inheritdoc />
    public ValueTask EnqueueAsync(EmailMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        return _channel.Writer.WriteAsync(message);
    }

    /// <inheritdoc />
    public ValueTask<EmailMessage> DequeueAsync(CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAsync(cancellationToken);
    }
}
