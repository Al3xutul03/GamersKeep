using BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services;

/// <summary>
/// Hosted background worker reading queued email messages from <see cref="IEmailQueue"/>
/// and dispatching them asynchronously via <see cref="IEmailSender"/>.
/// </summary>
public class EmailDispatchWorker(
    IEmailQueue emailQueue,
    IEmailSender emailSender,
    ILogger<EmailDispatchWorker> logger) : BackgroundService
{
    private readonly IEmailQueue _emailQueue = emailQueue;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly ILogger<EmailDispatchWorker> _logger = logger;

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("EmailDispatchWorker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var message = await _emailQueue.DequeueAsync(stoppingToken);

                try
                {
                    await _emailSender.SendEmailAsync(message, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send email to {ToEmail}. Subject: {Subject}", message.ToEmail, message.Subject);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in EmailDispatchWorker processing loop.");
            }
        }

        _logger.LogInformation("EmailDispatchWorker stopped.");
    }
}
