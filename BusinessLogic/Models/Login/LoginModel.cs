namespace BusinessLogic.Models.Login;

/// <summary>
/// Represents a login DTO for user information.
/// </summary>
public record LoginModel(
    string Username = "",
    string Password = ""
    );