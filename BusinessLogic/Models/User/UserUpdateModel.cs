using BusinessLogic.Json.Model;

namespace BusinessLogic.Models.User;

/// <summary>
/// Data transfer object for user update information.
/// </summary>
public record UserUpdateModel(ulong Id, string Name, string Email, string? Password, UserSettings Settings);