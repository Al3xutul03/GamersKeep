using AutoMapper;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Abstractions;

namespace Tests.Unit.Services;

[TestClass]
[TestCategory("Unit")]
public class UserServiceTests
{
    private IMapper _mapper = null!;
    private IUserRepository _userRepo = null!;
    private UserService _userService = null!;

    [TestInitialize]
    public void Setup()
    {
        _mapper = Substitute.For<IMapper>();
        _userRepo = Substitute.For<IUserRepository>();
        
        _userService = new UserService(_mapper, _userRepo);
    }

    [TestMethod]
    public async Task GetByNameAsync_ExistingUser_ReturnsMappedDto()
    {
        var user = new User { Id = 1, Name = "FindMe", Settings = "{}" };
        var dto = new UserReadModel(1, "FindMe", "test@test.com", DateTime.UtcNow, DateTime.UtcNow, false, new BusinessLogic.Json.Model.UserSettings());
        
        _userRepo.GetByNameAsync("FindMe", IncludeBehaviour.NoInclude).Returns(user);
        _mapper.Map<UserReadModel>(user).Returns(dto);
        
        var result = await _userService.GetByNameAsync("FindMe");
        
        Assert.IsNotNull(result);
        Assert.AreEqual("FindMe", result!.Name);
        await _userRepo.Received(1).GetByNameAsync("FindMe", IncludeBehaviour.NoInclude);
        _mapper.Received(1).Map<UserReadModel>(user);
    }

    [TestMethod]
    public async Task GetByNameAsync_MissingUser_ReturnsNull()
    {
        _userRepo.GetByNameAsync("Missing", IncludeBehaviour.NoInclude).Returns((User?)null);
        _mapper.Map<UserReadModel>(null).Returns((UserReadModel?)null);
        
        var result = await _userService.GetByNameAsync("Missing");
        
        Assert.IsNull(result);
        await _userRepo.Received(1).GetByNameAsync("Missing", IncludeBehaviour.NoInclude);
        _mapper.Received(1).Map<UserReadModel>(null);
    }

    [TestMethod]
    public async Task GetByIdAsync_ExistingUser_ReturnsMappedDto()
    {
        var user = new User { Id = 1, Name = "FindMe", Settings = "{}" };
        var dto = new UserReadModel(1, "FindMe", "test@test.com", DateTime.UtcNow, DateTime.UtcNow, false, new BusinessLogic.Json.Model.UserSettings());
        
        _userRepo.GetByIdAsync(1, IncludeBehaviour.NoInclude).Returns(user);
        _mapper.Map<UserReadModel>(user).Returns(dto);
        
        var result = await _userService.GetByIdAsync(1);
        
        Assert.IsNotNull(result);
        Assert.AreEqual("FindMe", result!.Name);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnsMappedCollection()
    {
        var users = new List<User> { new User { Id = 1 } };
        var dtos = new List<UserReadModel> { new UserReadModel(1, "FindMe", "test@test.com", DateTime.UtcNow, DateTime.UtcNow, false, new BusinessLogic.Json.Model.UserSettings()) };
        
        _userRepo.GetAllAsync(IncludeBehaviour.NoInclude).Returns(users);
        _mapper.Map<IEnumerable<UserReadModel>>(users).Returns(dtos);
        
        var result = await _userService.GetAllAsync();
        
        Assert.AreEqual(1, result.Count());
    }

    [TestMethod]
    public async Task CreateAsync_MapsAndSaves()
    {
        var createModel = new UserCreateModel("NewUser", "new@test.com", "pass");
        var user = new User { Name = "NewUser" };
        
        _mapper.Map<User>(createModel).Returns(user);
        
        await _userService.CreateAsync(createModel);
        
        await _userRepo.Received(1).AddAsync(user);
        await _userRepo.Received(1).SaveAsync();
    }

    [TestMethod]
    public async Task UpdateAsync_MapsAndUpdates()
    {
        var settings = new BusinessLogic.Json.Model.UserSettings();
        var updateModel = new UserUpdateModel(1, "UpdatedName", "up@test.com", null, settings);
        var user = new User { Id = 1, Name = "UpdatedName" };
        
        _mapper.Map<User>(updateModel).Returns(user);
        
        await _userService.UpdateAsync(updateModel);
        
        _userRepo.Received(1).Update(user);
        await _userRepo.Received(1).SaveAsync();
    }

    [TestMethod]
    public async Task DeleteAsync_ExistingUser_DeletesAndSaves()
    {
        var user = new User { Id = 1 };
        _userRepo.GetByIdAsync(1, IncludeBehaviour.NoInclude).Returns(user);
        
        await _userService.DeleteAsync(1);
        
        _userRepo.Received(1).Delete(user);
        await _userRepo.Received(1).SaveAsync();
    }

    [TestMethod]
    public async Task DeleteAsync_MissingUser_ThrowsException()
    {
        _userRepo.GetByIdAsync(1, IncludeBehaviour.NoInclude).Returns((User?)null);
        try
        {
            await _userService.DeleteAsync(1);
            Assert.Fail("Expected an exception");
        }
        catch (Exception ex)
        {
            Assert.AreEqual("User not found", ex.Message);
        }
    }
}
