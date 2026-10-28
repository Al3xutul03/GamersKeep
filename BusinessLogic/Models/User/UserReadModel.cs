using BusinessLogic.Json.Model;

namespace BusinessLogic.Models.User;

/// <summary>
/// Data transfer object for user read information.
/// </summary>
public record UserReadModel(
    ulong Id, 
    string Name, 
    string Email, 
    DateTime CreatedAt, 
    DateTime UpdatedAt, 
    bool IsAdmin, 
    UserSettings Settings);
