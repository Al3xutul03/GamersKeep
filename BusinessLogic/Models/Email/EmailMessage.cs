namespace BusinessLogic.Models.Email;

/// <summary>
/// Model representing an outbound email message.
/// </summary>
public record EmailMessage(
    string ToEmail,
    string? ToName,
    string Subject,
    string HtmlBody,
    string? PlainTextBody = null);
