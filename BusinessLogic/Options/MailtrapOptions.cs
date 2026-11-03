namespace BusinessLogic.Options;

/// <summary>
/// Configuration settings for the Mailtrap SMTP sandbox email service.
/// </summary>
public class MailtrapOptions
{
    public const string SectionName = "Mailtrap";

    public string Host { get; set; } = "sandbox.smtp.mailtrap.io";
    public int Port { get; set; } = 2525;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromEmail { get; set; } = "no-reply@gamerskeep.com";
    public string FromName { get; set; } = "GamersKeep";

    /// <summary>
    /// Indicates whether real Mailtrap sandbox SMTP credentials have been provided.
    /// When false or left as placeholder, the email sender runs in simulation mode
    /// (logging the email contents without attempting network connections).
    /// </summary>
    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(Host) &&
        !string.IsNullOrWhiteSpace(Username) &&
        !string.IsNullOrWhiteSpace(Password) &&
        !Username.Contains("YOUR_", StringComparison.OrdinalIgnoreCase) &&
        !Password.Contains("YOUR_", StringComparison.OrdinalIgnoreCase) &&
        !Username.Contains("placeholder", StringComparison.OrdinalIgnoreCase);
}
