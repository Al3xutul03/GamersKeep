using BusinessLogic.Models.Email;
using BusinessLogic.Options;
using BusinessLogic.Services.Abstractions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BusinessLogic.Services;

/// <summary>
/// Implementation of <see cref="IEmailSender"/> using MailKit and Mailtrap SMTP sandbox.
/// Safely logs in simulation mode if credentials are placeholders or not configured.
/// </summary>
public class MailtrapSmtpEmailSender(
    IOptions<MailtrapOptions> options,
    ILogger<MailtrapSmtpEmailSender> logger) : IEmailSender
{
    private readonly MailtrapOptions _options = options.Value;
    private readonly ILogger<MailtrapSmtpEmailSender> _logger = logger;

    /// <inheritdoc />
    public async Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);

        // 1. Simulation fallback when Mailtrap credentials are not set
        if (!_options.IsConfigured)
        {
            _logger.LogWarning(
                "[Mailtrap Simulation] Email not sent to network because credentials are not configured.\nTo: {ToEmail} ({ToName})\nSubject: {Subject}\nBody length: {Length} characters.",
                message.ToEmail, message.ToName, message.Subject, message.HtmlBody?.Length ?? 0);
            return;
        }

        // 2. Real MailKit SMTP transmission
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_options.FromName, _options.FromEmail));
        mimeMessage.To.Add(new MailboxAddress(message.ToName ?? message.ToEmail, message.ToEmail));
        mimeMessage.Subject = message.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = message.HtmlBody,
            TextBody = message.PlainTextBody
        };
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();

        try
        {
            await client.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.StartTlsWhenAvailable, cancellationToken);
            await client.AuthenticateAsync(_options.Username, _options.Password, cancellationToken);
            await client.SendAsync(mimeMessage, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);

            _logger.LogInformation(
                "Successfully dispatched email via Mailtrap to {ToEmail}. Subject: {Subject}",
                message.ToEmail, message.Subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {ToEmail} via Mailtrap ({Host}:{Port}).", message.ToEmail, _options.Host, _options.Port);
            throw;
        }
    }
}
