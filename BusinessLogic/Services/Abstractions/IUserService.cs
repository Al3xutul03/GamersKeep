using BusinessLogic.Models.User;
using BusinessLogic.Services.Generic;
using Repository.Entity;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// User service implementation for user information from the database
/// </summary>
public interface IUserService
    : IBaseService<User, UserReadModel, UserCreateModel, UserUpdateModel>
{
    /// <summary>
    /// Retrieves a user by their name.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <returns>The found user, or null if not found.</returns>
    public Task<UserReadModel?> GetByNameAsync(string name);
}
