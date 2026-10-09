namespace BusinessLogic.Models.User;

/// <summary>
/// Data transfer object for user create information.
/// </summary>
public record UserCreateModel(string Name, string Email, string Password);