using AutoMapper;
using BusinessLogic.Models.User;
using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Generic;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Abstractions;

namespace BusinessLogic.Services;

/// <summary>
/// Business logic service for handling user information.
/// </summary>
/// <param name="mapper">The mapper used for mapping operations.</param>
/// <param name="userRepository">The database context to use.</param>
public class UserService(IMapper mapper, IUserRepository userRepository)
    : BaseService<User, UserReadModel, UserCreateModel, UserUpdateModel>(mapper, userRepository), IUserService
{
    private IUserRepository UserRepository => (IUserRepository)_repository;

    public async Task<UserReadModel?> GetByNameAsync(string name)
    {
        User? user = await UserRepository.GetByNameAsync(name, IncludeBehaviour.NoInclude);
        return _mapper.Map<UserReadModel>(user);
    }
}
