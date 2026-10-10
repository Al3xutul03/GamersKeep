namespace BusinessLogic.Models.Login;

/// <summary>
/// Represents the login response when logging into the application.
/// </summary>
public record LoginResponseModel(
    string Token = "",
    string RefreshToken = ""
    );
