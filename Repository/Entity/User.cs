namespace Repository.Entity;

/// <summary>
/// Database model for user information.
/// </summary>
public class User
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public ulong Id { get; set; }

    /// <summary>
    /// The user's username.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The user's email address.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// The hash of the user's password.
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// The date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the user was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// The date and time when the user was deleted, if applicable.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Indicates whether the user has administrative privileges.
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// JSON-serialized <c>UserSettings</c> blob.
    /// Round-tripped via the AutoMapper profile using
    /// <c>AppJsonSettings.Default</c>.
    /// </summary>
    public string Settings { get; set; } = null!;
}